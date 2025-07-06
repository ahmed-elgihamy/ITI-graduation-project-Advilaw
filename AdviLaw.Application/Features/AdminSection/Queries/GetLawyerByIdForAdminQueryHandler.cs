using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AdviLaw.Application.Basics;
using AdviLaw.Application.Features.AdminSection.DTOs;
using AdviLaw.Domain.UnitOfWork;
using AutoMapper;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace AdviLaw.Application.Features.AdminSection.Queries
{
    public class GetLawyerByIdForAdminQueryHandler : IRequestHandler<GetLawyerByIdForAdminQuery, Response<LawyerForAdminDto>>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        private readonly ResponseHandler _responseHandler;

        public GetLawyerByIdForAdminQueryHandler(IUnitOfWork unitOfWork, IMapper mapper, ResponseHandler responseHandler)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
            _responseHandler = responseHandler;
        }

        public async Task<Response<LawyerForAdminDto>> Handle(GetLawyerByIdForAdminQuery request, CancellationToken cancellationToken)
        {
            var lawyer = await _unitOfWork.Lawyers
                .GetTableNoTracking()
                .Include(l => l.User)
                .FirstOrDefaultAsync(l => l.Id == request.Id, cancellationToken);

            if (lawyer == null)
                return _responseHandler.NotFound<LawyerForAdminDto>("Lawyer not found.");

            var dto = _mapper.Map<LawyerForAdminDto>(lawyer);
            return _responseHandler.Success(dto);
        }
    }
}
