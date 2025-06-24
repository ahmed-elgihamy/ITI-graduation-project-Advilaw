
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
        public CreateJobHandler(IUnitOfWork unitOfWork, IMapper mapper, ResponseHandler responseHandler, UserManager<User> userManager)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
            _responseHandler = responseHandler;
            _userManager = userManager;
        }
        public async Task<Response<CreatedJobDTO>> Handle(CreateJobCommand request, CancellationToken cancellationToken)
        {

            var user = await _userManager.Users.Include(u => u.Client).FirstOrDefaultAsync(u => u.Id == request.UserId);
            if (user == null)
            { return _responseHandler.BadRequest<CreatedJobDTO>("Lawyer Not Found"); }
            var job = _mapper.Map<Job>(request);
            if (request.LawyerId != null)
            {
                var lawyer = await _unitOfWork.Lawyers.GetByIdAsync(request.LawyerId.Value);
                if (lawyer != null)
                {
                    job.Lawyer = lawyer;
                    job.LawyerId = lawyer.Id;
                }
            }
            var client = user.Client;
            job.Client = client;
            job.ClientId = client.Id;
            //job.CreatedAt = DateTime.UtcNow;
            try
            {
                var createdJob = await _unitOfWork.Jobs.AddAsync(job);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex); // Or use a proper logger
                throw;
            }
            await _unitOfWork.SaveChangesAsync();
            var createdJobDTO = _mapper.Map<CreatedJobDTO>(job);
            return _responseHandler.Success(createdJobDTO);
        }
    }
}
