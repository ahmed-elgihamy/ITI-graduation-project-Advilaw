using AdviLaw.Application.Basics;
using AdviLaw.Application.Features.AppointmentSection.DTOs;
using AdviLaw.Domain.Entites.AppointmentSection;
using AdviLaw.Domain.Entites.JobSection;
using AdviLaw.Domain.Entites.ScheduleSection;
using AdviLaw.Domain.Entities.UserSection;
using AdviLaw.Domain.UnitOfWork;
using AutoMapper;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace AdviLaw.Application.Features.AppointmentSection.Commands.CreateSchedule
{
    public class CreateAppointmentHandler(IUnitOfWork unitOfWork, ResponseHandler responseHandler, IMapper mapper) : IRequestHandler<CreateAppointmentCommand, Response<AppointmentDetailsDTO>>
    {
        private readonly IUnitOfWork _unitOfWork = unitOfWork ?? throw new ArgumentNullException(nameof(unitOfWork));
        private readonly ResponseHandler _responseHandler = responseHandler ?? throw new ArgumentNullException(nameof(responseHandler));
        private readonly IMapper _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));

        public async Task<Response<AppointmentDetailsDTO>> Handle(CreateAppointmentCommand request, CancellationToken cancellationToken)
        {

            if (request.JobId <= 0)
            {
                return _responseHandler.BadRequest<AppointmentDetailsDTO>("Invalid job ID.");
            }
            var job = await _unitOfWork.Jobs.GetByIdAsync(request.JobId);
            if (job == null)
            {
                return _responseHandler.NotFound<AppointmentDetailsDTO>("Job not found.");
            }

            if (job.Status != JobStatus.WaitingAppointment &&
                job.Status != JobStatus.LawyerRequestedAppointment &&
                job.Status != JobStatus.ClientRequestedAppointment)
            {
                return _responseHandler.BadRequest<AppointmentDetailsDTO>("Job is not in a valid state to create an appointment.");
            }
            if (request.Date < DateTime.UtcNow)
            {
                return _responseHandler.BadRequest<AppointmentDetailsDTO>("Appointment date cannot be in the past.");
            }
            var lastAppointment = await _unitOfWork.Appointments.GetLastAppointmentAsync(request.JobId);
            if (lastAppointment != null)
            {
                if (lastAppointment.Type == ScheduleType.FromLawyer && request.UserRole == UserRole.Lawyer ||
                   lastAppointment.Type == ScheduleType.FromClient && request.UserRole == UserRole.Client)
                {
                    return _responseHandler.BadRequest<AppointmentDetailsDTO>("User cannot create Two Consequence Appointments.");
                }
            }
            if (lastAppointment == null && request.UserRole == UserRole.Lawyer)
            {
                return _responseHandler.BadRequest<AppointmentDetailsDTO>("Lawyer cannot create an appointment without a previous client appointment.");
            }
            var appointmentsQuery = await _unitOfWork.Appointments.GetAllAsync(
                filter: a =>
                    a.Status == ScheduleStatus.Accepted &&
                    a.Date.Date > DateTime.UtcNow.Date &&
                    a.Job.LawyerId == job.LawyerId,
                includes: new List<Expression<Func<Appointment, object>>>
                {
                    a => a.Job
                }
            );

            DateTime requestedStart = request.Date;
            DateTime requestedEnd = request.Date.AddHours(2);

            var appointments = await appointmentsQuery.ToListAsync();
            bool hasConflict = appointments.Any(a =>
            {
                DateTime existingStart = a.Date;
                DateTime existingEnd = a.Date.AddHours(2);

                return existingStart < requestedEnd && requestedStart < existingEnd;
            });

            if (hasConflict)
            {
                return _responseHandler.BadRequest<AppointmentDetailsDTO>("There is already an appointment during the requested time.");
            }

            var appointment = _mapper.Map<Appointment>(request);
            ScheduleType AppointmentType = request.UserRole == UserRole.Lawyer ? ScheduleType.FromLawyer : ScheduleType.FromClient;
            appointment.Type = AppointmentType;

            await _unitOfWork.Appointments.AddAsync(appointment);
            job.Status = request.UserRole == UserRole.Lawyer ? JobStatus.LawyerRequestedAppointment : JobStatus.ClientRequestedAppointment;
            await _unitOfWork.SaveChangesAsync(cancellationToken);

            var appointmentDetails = _mapper.Map<AppointmentDetailsDTO>(appointment);
            var response = _responseHandler.Success(appointmentDetails, "Appointment created successfully.");
            return response;
        }
    }
}
