using AdviLaw.Application.Basics;
using AdviLaw.Application.Features.Schedule.DTOs;
using AdviLaw.Domain.UnitOfWork;
using AutoMapper;
using MediatR;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace AdviLaw.Application.Features.Schedule.Queries
{
    public class GetSchedulesByLawyerHandler
        : IRequestHandler<GetSchedulesByLawyerQuery, Response<List<ScheduleDTO>>>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        private readonly ResponseHandler _responseHandler;

        public GetSchedulesByLawyerHandler(IUnitOfWork unitOfWork, IMapper mapper, ResponseHandler responseHandler)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
            _responseHandler = responseHandler;
        }

        public async Task<Response<List<ScheduleDTO>>> Handle(GetSchedulesByLawyerQuery request, CancellationToken cancellationToken)
        {
            var schedules = await _unitOfWork.Schedules.GetSchedulesByLawyerId(request.LawyerId);

            var scheduleDtos = _mapper.Map<List<ScheduleDTO>>(schedules);

            return _responseHandler.Success(scheduleDtos);
        }
    }
}
