using AdviLaw.Application.Basics;
using AdviLaw.Application.Features.AdminSection.DTOs;
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

namespace AdviLaw.Application.Features.AdminSection.Queries
{
    public class GetAdminProfileHandler : IRequestHandler<GetAdminProfileQuery, Response<AdminProfileDTO>>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        private readonly ResponseHandler _responseHandler;

        public GetAdminProfileHandler(IUnitOfWork unitOfWork, IMapper mapper, ResponseHandler responseHandler)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
            _responseHandler = responseHandler;
        }

        public async Task<Response<AdminProfileDTO>> Handle(GetAdminProfileQuery request, CancellationToken cancellationToken)
        {
            var includes = new List<Expression<Func<Admin, object>>>
            {
                a => a.User
            };

            var admin = await _unitOfWork.GenericAdmins
                .GetByIdIncludesAsync(request.AdminId, null, includes);

            if (admin == null)
                return _responseHandler.NotFound<AdminProfileDTO>("Admin not found");

            var dto = _mapper.Map<AdminProfileDTO>(admin);
            return _responseHandler.Success(dto);
        }
    }
} 