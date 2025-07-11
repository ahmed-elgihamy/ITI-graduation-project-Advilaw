using AdviLaw.Application.Basics;
using AdviLaw.Application.Features.LawyerSection.DTOs;
using AdviLaw.Domain.Entities.UserSection;
using AdviLaw.Domain.UnitOfWork;
using MediatR;
using System.Linq.Expressions;

namespace AdviLaw.Application.Features.LawyerSection.Queries.GetHourlyRate
{
    public class GetHourlyRateHandler(IUnitOfWork unitOfWork, ResponseHandler responseHandler) : IRequestHandler<GetHourlyRateQuery, Response<HourlyRateDTO>>
    {
        private readonly IUnitOfWork _unitOfWork = unitOfWork;
        private readonly ResponseHandler _responseHandler = responseHandler;

        public async Task<Response<HourlyRateDTO>> Handle(GetHourlyRateQuery request, CancellationToken cancellationToken)
        {
            
            var lawyer = await _unitOfWork.Lawyers.GetByIdIncludesAsync(request.LawyerId);
            if (lawyer == null)
            {
                return _responseHandler.NotFound<HourlyRateDTO>($"Lawyer with ID {request.LawyerId} not found.");
            }
            var hourlyRate = lawyer.HourlyRate;
            if (hourlyRate == null)
            {
                return _responseHandler.NotFound<HourlyRateDTO>($"Hourly rate for lawyer with ID {request.LawyerId} not found.");
            }
            return _responseHandler.Success(new HourlyRateDTO
            {
                hourlyRate = hourlyRate
            });
        }
    }
}
