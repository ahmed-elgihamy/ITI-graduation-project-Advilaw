using AdviLaw.Application.Basics;
using AdviLaw.Domain.Entites.JobSection;
using AdviLaw.Domain.Entites.ScheduleSection;
using AdviLaw.Domain.Entities.UserSection;
using AdviLaw.Domain.UnitOfWork;
using MediatR;

namespace AdviLaw.Application.Features.AppointmentSection.Commands.AcceptAppointment
{
    public class AcceptAppointmentHandler(IUnitOfWork unitOfWork, ResponseHandler responseHandler) : IRequestHandler<AcceptAppointmentCommand, Response<bool>>
    {
        private readonly IUnitOfWork _unitOfWork = unitOfWork ?? throw new ArgumentNullException(nameof(unitOfWork));
        private readonly ResponseHandler _responseHandler = responseHandler ?? throw new ArgumentNullException(nameof(responseHandler));

        public async Task<Response<bool>> Handle(AcceptAppointmentCommand request, CancellationToken cancellationToken)
        {
            if (request.AppointmentId <= 0)
            {
                return _responseHandler.BadRequest<bool>("Invalid appointment ID.");
            }
            var appointment = await _unitOfWork.Appointments.GetByIdAsync(request.AppointmentId);
            if (appointment == null)
            {
                return _responseHandler.NotFound<bool>("Appointment not found.");
            }
            if(
                appointment.Type == ScheduleType.FromLawyer && request.UserRole != UserRole.Client ||
                appointment.Type == ScheduleType.FromClient && request.UserRole != UserRole.Lawyer
            )
            { return _responseHandler.Unauthorized<bool>("You do not have permission to accept this appointment."); }
            appointment.Status = ScheduleStatus.Accepted;
            //await _unitOfWork.Appointments.UpdateAsync(appointment);
            var job = await _unitOfWork.Jobs.GetByIdAsync(appointment.JobId);
            if (job == null)
            {
                return _responseHandler.NotFound<bool>("Job not found for the appointment.");
            }
            if (
                job.Status == JobStatus.WaitingAppointment || 
                job.Status == JobStatus.LawyerRequestedAppointment ||
                job.Status == JobStatus.ClientRequestedAppointment
            )
            {
                job.Status = JobStatus.WaitingPayment;
            }
            else
            {
                return _responseHandler.BadRequest<bool>("Job is not in a valid state to accept the appointment.");
            }
            //job.AppointmentId = appointment.Id;
            //await _unitOfWork.Jobs.UpdateAsync(job);
            await _unitOfWork.SaveChangesAsync(cancellationToken);
            return _responseHandler.Success(true, "Appointment accepted successfully.");
        }
    }
}
