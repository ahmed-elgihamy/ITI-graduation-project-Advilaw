using System.Threading;
using System.Threading.Tasks;
using AdviLaw.Application.Basics;
using MediatR;
using AdviLaw.Domain.UnitOfWork;
using AdviLaw.Domain.Entites.ScheduleSection;
using System;
using AdviLaw.Application.Features.ScheduleSection.Commands.CreateSchedule;
using AdviLaw.Application.Features.Schedule.Commands.CreateSchedule;



namespace AdviLaw.Application.Features.ScheduleSection.Commands.CreateSchedule
{
    //public class CreateScheduleCommandHandler : IRequestHandler<CreateScheduleCommand, Response<int>>
    //{
    //    private readonly IUnitOfWork _unitOfWork;
    //    private readonly ResponseHandler _responseHandler;

    //    public CreateScheduleCommandHandler(IUnitOfWork unitOfWork, ResponseHandler responseHandler)
    //    {
    //        _unitOfWork = unitOfWork;
    //        _responseHandler = responseHandler;
    //    }

    //    public async Task<Response<int>> Handle(CreateScheduleCommand request, CancellationToken cancellationToken)
    //    {
    //        var schedule = new AdviLaw.Domain.Entites.ScheduleSection.Schedule
    //        {
    //            //JobId = request.JobId,
    //            StartTime = request.StartTime.TimeOfDay,
    //            EndTime = request.EndTime.TimeOfDay,
    //            //Day = request.Day,
    //            //Content = request.Content,
    //            //Type = ScheduleType.FromClient,
    //            //Status = ScheduleStatus.None
    //        };

    //        await _unitOfWork.Schedules.AddAsync(schedule);
    //        await _unitOfWork.SaveChangesAsync(cancellationToken);
    //        return _responseHandler.Success(schedule.Id);
    //    }
    //}
} 