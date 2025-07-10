using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AdviLaw.Application.Basics;
using AdviLaw.Application.DTOs.Client;
using AdviLaw.Domain.UnitOfWork;
using AutoMapper;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace AdviLaw.Application.Features.AdminSection.Queries
{
    public class GetClientByIdForAdminQueryHandler : IRequestHandler<GetClientByIdForAdminQuery, Response<ClientListDto>>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        private readonly ResponseHandler _responseHandler;

        public GetClientByIdForAdminQueryHandler(IUnitOfWork unitOfWork, IMapper mapper, ResponseHandler responseHandler)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
            _responseHandler = responseHandler;
        }

        public async Task<Response<ClientListDto>> Handle(GetClientByIdForAdminQuery request, CancellationToken cancellationToken)
        {
            var client = await _unitOfWork.Clients
                .GetTableNoTracking()
                .Include(c => c.User)
                .FirstOrDefaultAsync(c => c.Id == request.Id, cancellationToken);

            if (client == null)
            {
                return _responseHandler.NotFound<ClientListDto>("Client not found.");
            }

            var dto = _mapper.Map<ClientListDto>(client);
            return _responseHandler.Success(dto);
        }
    }
}
