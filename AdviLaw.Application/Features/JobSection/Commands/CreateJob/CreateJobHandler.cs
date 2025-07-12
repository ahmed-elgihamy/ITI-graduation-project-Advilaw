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
                .FirstOrDefaultAsync(u => u.Id == request.UserId, cancellationToken);

            if (user?.Client == null)
                return _responseHandler.BadRequest<CreatedJobDTO>("Client not found");

           
            var job = new Job
            {
                Header = request.Header,
                Description = request.Description,
                Type = request.Type,
                IsAnonymus = request.IsAnonymus,
                JobFieldId = request.JobFieldId,
                ClientId = user.Client.Id,
                Status = request.Type == JobType.LawyerProposal
                                 ? JobStatus.WaitingAppointment
                                 : JobStatus.NotAssigned,
                CreatedAt = DateTime.UtcNow
            };


            if (request.Type == JobType.LawyerProposal)
            {
                if (request.LawyerId == null ||
                    request.AppointmentTime == null ||
                    request.DurationHours == null)
                {
                    return _responseHandler.BadRequest<CreatedJobDTO>(
                        "LawyerId, AppointmentTime and DurationHours are required for LawyerProposal");
                }

                var lawyer = await _unitOfWork.Lawyers.GetByIdAsync(request.LawyerId.Value);

                if (lawyer == null)
                    return _responseHandler.BadRequest<CreatedJobDTO>("Lawyer not found");


                //job.Budget = (int)Math.Ceiling((decimal)(request.DurationHours.Value) * lawyer.HourlyRate);
                if (!request.AppointmentTime.HasValue)
                {
                    return _responseHandler.BadRequest<CreatedJobDTO>("AppointmentTime is required.");
                }
                if (lawyer.HourlyRate <= 0)
                    return _responseHandler.BadRequest<CreatedJobDTO>("Lawyer's hourly rate must be set and greater than 0");

                if (request.DurationHours.Value <= 0)
                    return _responseHandler.BadRequest<CreatedJobDTO>("Duration must be greater than 0");

                //job.AppointmentTime = DateTime.SpecifyKind(request.AppointmentTime.Value, DateTimeKind.Utc);
                job.LawyerId = lawyer.Id;
                job.DurationHours = request.DurationHours;
                job.AppointmentTime = request.AppointmentTime.Value;
                //job.Budget = request.Budget;
                job.Status = JobStatus.WaitingApproval;
                job.Budget = (int)Math.Ceiling(
                    (decimal)request.DurationHours.Value * lawyer.HourlyRate);
            }
        
            else // ClientPublishing
            {

                if (request.Budget <= 0)
                    return _responseHandler.BadRequest<CreatedJobDTO>("Budget must be greater than 0 for public jobs.");
                // → Budget must come from request and be positive
                if (request.Budget <= 0)
                    return _responseHandler.BadRequest<CreatedJobDTO>(
                        "Budget must be greater than 0 for public jobs");

                job.Budget = request.Budget;
            }


        
            try
            {
                await _unitOfWork.Jobs.AddAsync(job);
                await _unitOfWork.SaveChangesAsync(cancellationToken);
            }
            catch
            {
                return _responseHandler.BadRequest<CreatedJobDTO>(
                    "Something went wrong while saving job");
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
