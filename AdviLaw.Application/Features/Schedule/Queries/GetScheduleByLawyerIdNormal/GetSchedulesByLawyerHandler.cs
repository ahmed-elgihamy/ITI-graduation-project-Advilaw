using AdviLaw.Application.Basics;
using AdviLaw.Application.Features.Schedule.DTOs;
using AdviLaw.Domain.UnitOfWork;
using AutoMapper;
using MediatR;

namespace AdviLaw.Application.Features.Schedule.Queries.GetScheduleByLawyerIdNormal
{
    public class GetSchedulesByLawyerNormalHandler(IUnitOfWork unitOfWork, ResponseHandler responseHandler, IMapper mapper) : IRequestHandler<GetSchedulesByLawyerNormalQuery, Response<List<CreatedScheduleDTO>>>
    {
        private readonly IUnitOfWork _unitOfWork = unitOfWork;
        private readonly ResponseHandler _responseHandler = responseHandler;
        private readonly IMapper _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));

        public async Task<Response<List<CreatedScheduleDTO>>> Handle(GetSchedulesByLawyerNormalQuery request, CancellationToken cancellationToken)
        {
            var schedules = await _unitOfWork.Schedules.GetSchedulesByLawyerId(request.LawyerId);

            //var grouped = schedules
            //    .Where(s => s.StartTime != TimeSpan.Zero && s.EndTime != TimeSpan.Zero)
            //    .GroupBy(s => s.Day)
            //    .Select(g => new LawyerScheduleDTO
            //    {
            //        Day = g.Key,
            //        TimeRanges = g
            //            .Where(s => s.StartTime != TimeSpan.Zero && s.EndTime != TimeSpan.Zero)
            //            .Select(s => new TimeRangeDTO
            //            {
            //                Start = s.StartTime.ToString(@"hh\:mm"),
            //                End = s.EndTime.ToString(@"hh\:mm")
            //            })
            //            .ToList()
            //    })
            //    .ToList();

            var schedulesDTO = _mapper.Map<List<CreatedScheduleDTO>>(schedules);

            return _responseHandler.Success(schedulesDTO);
        }   

    }
}
