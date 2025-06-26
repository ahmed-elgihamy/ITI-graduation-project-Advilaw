using AdviLaw.Application.Basics;
using AdviLaw.Application.Features.LawyerProfile.DTOs;
using AdviLaw.Domain.Entities.UserSection;
using AdviLaw.Domain.UnitOfWork;
using AutoMapper;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace AdviLaw.Application.Features.LawyerProfile.Queries.GetLawyerProfile
{
   public class GetLawyerProfileHandler : IRequestHandler<GetLawyerProfileQuery, Response<LawyerProfileDTO>>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        private readonly ResponseHandler _responseHandler;

        public GetLawyerProfileHandler(IUnitOfWork unitOfWork, IMapper mapper, ResponseHandler responseHandler)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
            _responseHandler = responseHandler;
        }

        public async Task<Response<LawyerProfileDTO>> Handle(GetLawyerProfileQuery request, CancellationToken cancellationToken)
        {
            var includes = new List<Expression<Func<Lawyer, object>>>
    {
        l => l.User
    };

            var lawyer = await _unitOfWork.Lawyers
                .GetByIdIncludesAsync(request.LawyerId, null, includes);

            if (lawyer == null)
                return _responseHandler.NotFound<LawyerProfileDTO>("Lawyer not found");

            var dto = _mapper.Map<LawyerProfileDTO>(lawyer);
            return _responseHandler.Success(dto);
        }

    }
}
