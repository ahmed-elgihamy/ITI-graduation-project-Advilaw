using AdviLaw.Application.Basics;
using AdviLaw.Application.Features.Schedule.DTOs;
using AdviLaw.Application.Features.ScheduleSection.DTOs;
using AdviLaw.Domain.UnitOfWork;
using MediatR;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace AdviLaw.Application.Features.Schedule.Queries
{
    public class GetSchedulesByLawyerHandler : IRequestHandler<GetSchedulesByLawyerQuery, Response<List<LawyerScheduleDTO>>>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly ResponseHandler _responseHandler;

        public GetSchedulesByLawyerHandler(IUnitOfWork unitOfWork, ResponseHandler responseHandler)
        {
            _unitOfWork = unitOfWork;
            _responseHandler = responseHandler;
        }
        public async Task<Response<List<LawyerScheduleDTO>>> Handle(GetSchedulesByLawyerQuery request, CancellationToken cancellationToken)
        {
            var schedules = await _unitOfWork.Schedules.GetSchedulesByLawyerId(request.LawyerId);

            var grouped = schedules
                .Where(s => s.StartTime != TimeSpan.Zero && s.EndTime != TimeSpan.Zero)
                .GroupBy(s => s.Day)
                .Select(g => new LawyerScheduleDTO
                {
                    Day = g.Key,
                    TimeRanges = g
                        .Where(s => s.StartTime != TimeSpan.Zero && s.EndTime != TimeSpan.Zero)
                        .Select(s => new TimeRangeDTO
                        {
                            Start = s.StartTime.ToString(@"hh\:mm"),
                            End = s.EndTime.ToString(@"hh\:mm")
                        })
                        .ToList()
                })
                .ToList();

            return _responseHandler.Success(grouped);
        }

    }
}
