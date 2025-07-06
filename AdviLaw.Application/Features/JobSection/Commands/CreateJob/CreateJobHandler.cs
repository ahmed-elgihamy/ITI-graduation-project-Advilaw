using AdviLaw.Application.Basics;
using AdviLaw.Application.Features.JobSection.DTOs;
using AdviLaw.Domain.Entites.JobSection;
using AdviLaw.Domain.Entities.UserSection;
using AdviLaw.Domain.UnitOfWork;
using AutoMapper;
using MediatR;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace AdviLaw.Application.Features.JobSection.Commands.CreateJob
{
    public class CreateJobHandler : IRequestHandler<CreateJobCommand, Response<CreatedJobDTO>>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        private readonly ResponseHandler _responseHandler;
        private readonly UserManager<User> _userManager;

        public CreateJobHandler(
            IUnitOfWork unitOfWork,
            IMapper mapper,
            ResponseHandler responseHandler,
            UserManager<User> userManager)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
            _responseHandler = responseHandler;
            _userManager = userManager;
        }

        public async Task<Response<CreatedJobDTO>> Handle(CreateJobCommand request, CancellationToken cancellationToken)
        {
            var user = await _userManager.Users
                .Include(u => u.Client)
                .FirstOrDefaultAsync(u => u.Id == request.UserId);

            if (user == null || user.Client == null)
                return _responseHandler.BadRequest<CreatedJobDTO>("Client not found");

            var job = new Job
            {
                Header = request.Header,
                Description = request.Description,
                Budget = request.budget,
                Type = request.Type,
                IsAnonymus = request.IsAnonymus,
                JobFieldId = request.JobFieldId,
                Client = user.Client,
                ClientId = user.Client.Id
            };

            if (request.Type == JobType.LawyerProposal)
            {
                if (request.LawyerId == null)
                    return _responseHandler.BadRequest<CreatedJobDTO>("Lawyer is required for this job type");

                var lawyer = await _unitOfWork.Lawyers.GetByIdAsync(request.LawyerId.Value);
                if (lawyer == null)
                    return _responseHandler.BadRequest<CreatedJobDTO>("Lawyer not found");

                job.Lawyer = lawyer;
                job.LawyerId = lawyer.Id;
                job.AppointmentTime = request.AppointmentTime;
                job.DurationHours = request.DurationHours;
            }

            try
            {
                await _unitOfWork.Jobs.AddAsync(job);
                await _unitOfWork.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
              
            }

            var createdJobDTO = _mapper.Map<CreatedJobDTO>(job);
            return _responseHandler.Success(createdJobDTO);
        }
    }
}

