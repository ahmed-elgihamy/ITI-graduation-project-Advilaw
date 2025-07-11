using AdviLaw.Application.Basics;
using AdviLaw.Application.Features.JobSection.DTOs;
using AdviLaw.Domain.Entites.AppointmentSection;
using AdviLaw.Domain.Entites.JobSection;
using AdviLaw.Domain.Entites.ScheduleSection;
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
                Type = request.Type,
                IsAnonymus = request.IsAnonymus,
                JobFieldId = request.JobFieldId,
                ClientId = user.Client.Id
            };


            if (request.Type == JobType.LawyerProposal)
            {
                if (request.LawyerId is null || request.AppointmentTime is null || request.DurationHours is null)
                    return _responseHandler.BadRequest<CreatedJobDTO>("LawyerId, AppointmentTime, and DurationHours are required for LawyerProposal");

                var lawyer = await _unitOfWork.Lawyers.GetByIdAsync(request.LawyerId.Value);
                if (lawyer == null)
                    return _responseHandler.BadRequest<CreatedJobDTO>("Lawyer not found");


                //job.Budget = (int)Math.Ceiling((decimal)(request.DurationHours.Value) * lawyer.HourlyRate);
                if (!request.AppointmentTime.HasValue)
                {
                    return _responseHandler.BadRequest<CreatedJobDTO>("AppointmentTime is required.");
                }

                //job.AppointmentTime = DateTime.SpecifyKind(request.AppointmentTime.Value, DateTimeKind.Utc);
                job.LawyerId = lawyer.Id;
                job.DurationHours = request.DurationHours;
                job.Budget = request.Budget;
                job.Status = JobStatus.WaitingApproval;
            }
            else if (request.Type == JobType.ClientPublishing)
            {

                if (request.Budget <= 0)
                    return _responseHandler.BadRequest<CreatedJobDTO>("Budget must be greater than 0 for public jobs.");

                job.Budget = request.Budget;
            }


            try
            {
                await _unitOfWork.Jobs.AddAsync(job);
                await _unitOfWork.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                return _responseHandler.BadRequest<CreatedJobDTO>("Something went wrong while saving job.");
            }

            if (request.Type == JobType.LawyerProposal)
            {
                var appointment = new Appointment
                {
                    JobId = job.Id,
                    Date = DateTime.SpecifyKind(request.AppointmentTime.Value, DateTimeKind.Utc),
                    Type = ScheduleType.FromClient,
                };
                try
                {
                    await _unitOfWork.Appointments.AddAsync(appointment);
                    await _unitOfWork.SaveChangesAsync();
                }
                catch (Exception ex)
                {
                    return _responseHandler.BadRequest<CreatedJobDTO>("Something went wrong while saving Appointment.");
                }
            }



            var dto = _mapper.Map<CreatedJobDTO>(job);
            return _responseHandler.Success(dto);
        }

    }
}

