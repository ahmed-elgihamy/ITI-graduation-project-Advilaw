using AdviLaw.Application.Basics;
using AdviLaw.Application.Features.Clients.DTOs;
using AdviLaw.Domain.Entities.UserSection;
using AdviLaw.Domain.UnitOfWork;
using AutoMapper;
using MediatR;
using System.Linq.Expressions;

namespace AdviLaw.Application.Features.Clients.Queries.GetProfile
{
    public class GetProfileHandler(IUnitOfWork unitOfWork, IMapper mapper, ResponseHandler responseHandler) : IRequestHandler<GetProfileQuery, Response<ClientProfileDTO>>
    {
        private readonly IUnitOfWork _unitOfWork = unitOfWork;
        private readonly IMapper _mapper = mapper;
        private readonly ResponseHandler _responseHandler = responseHandler;

        public async Task<Response<ClientProfileDTO>> Handle(GetProfileQuery request, CancellationToken cancellationToken)
        {
            var client = await _unitOfWork.Clients.GetByIdIncludesAsync(
                request.UserId,
                includes: new List<Expression<Func<Client, object>>>
                {
                    j => j.User,
                }
            );  
            if (client == null)
                return _responseHandler.NotFound<ClientProfileDTO>("Client profile not found.");
            var clientDto = _mapper.Map<ClientProfileDTO>(client);
            var response = _responseHandler.Success(clientDto);
            return response;
        }
    }
}
