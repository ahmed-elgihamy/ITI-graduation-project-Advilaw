using AdviLaw.Application.Basics;
using AdviLaw.Application.Features.Schedule.DTOs;
using AdviLaw.Domain.UnitOfWork;
using AutoMapper;
using MediatR;

namespace AdviLaw.Application.Features.Schedule.Commands.CreateSchedule
{
    public class CreateScheduleHandler(IUnitOfWork unitOfWork, ResponseHandler responseHandler, IMapper mapper) : IRequestHandler<CreateScheduleCommand, Response<List<CreatedScheduleDTO>>>
    {
        private readonly IUnitOfWork _unitOfWork = unitOfWork ?? throw new ArgumentNullException(nameof(unitOfWork));
        private readonly ResponseHandler _responseHandler = responseHandler ?? throw new ArgumentNullException(nameof(responseHandler));
        private readonly IMapper _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));

        public async Task<Response<List<CreatedScheduleDTO>>> Handle(CreateScheduleCommand request, CancellationToken cancellationToken)
        {
            if (
                (request.SchedulesToBeAdded == null || !request.SchedulesToBeAdded.Any()) &&
                (request.SchedulesToBeRemoved == null || !request.SchedulesToBeRemoved.Any())
            )
            {
                return _responseHandler.BadRequest<List<CreatedScheduleDTO>>("No schedules provided.");
            }

            var listToBeAdded = request.SchedulesToBeAdded;
            List<Domain.Entites.ScheduleSection.Schedule> schedules = new();
            if (listToBeAdded.Any())
            {
                schedules = _mapper.Map<List<AdviLaw.Domain.Entites.ScheduleSection.Schedule>>(listToBeAdded);
                schedules.ForEach(schedule =>
                {
                    schedule.UserId = request.UserId;
                });
                await _unitOfWork.Schedules.AddRangeAsync(schedules);
            }

            var schedulesToBeRemoved = request.SchedulesToBeRemoved;
            if (schedulesToBeRemoved.Any())
            {
                var schedulesToRemove = await _unitOfWork.Schedules.GetSchedulesByIds(schedulesToBeRemoved);
                if (schedulesToRemove.Any())
                {
                    await _unitOfWork.Schedules.RemoveRangeAsync(schedulesToRemove);
                }
            }


            await _unitOfWork.SaveChangesAsync(cancellationToken);

            var createdSchedules = _mapper.Map<List<CreatedScheduleDTO>>(schedules ?? []);
            var response = _responseHandler.Success(createdSchedules, "Schedules created successfully.");
            return response;
        }
    }
}
