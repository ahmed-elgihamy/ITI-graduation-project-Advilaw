using System.Linq.Expressions;
using System.Threading;
using System.Threading.Tasks;
using AdviLaw.Application.Basics;
using AdviLaw.Application.Features.Clients.DTOs;
using AdviLaw.Domain.Entities.UserSection;
using AdviLaw.Domain.UnitOfWork;
using AutoMapper;
using MediatR;

namespace AdviLaw.Application.Features.Clients.Commands
{
    public class UpdateClientProfileCommandHandler : IRequestHandler<UpdateClientProfileCommand, Response<ClientProfileDTO>>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        private readonly ResponseHandler _responseHandler;

        public UpdateClientProfileCommandHandler(IUnitOfWork unitOfWork, IMapper mapper, ResponseHandler responseHandler)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
            _responseHandler = responseHandler;
        }

        public async Task<Response<ClientProfileDTO>> Handle(UpdateClientProfileCommand request, CancellationToken cancellationToken)
        {
            var client = await _unitOfWork.GenericClients.GetByIdIncludesAsync(request.ClientId, includes: new List<Expression<Func<Client, object>>>
                {
                    j => j.User,
                });
            if (client == null || client.User == null)
                return _responseHandler.NotFound<ClientProfileDTO>("Client not found.");

            // Update user fields
            client.User.UserName = request.UserName;
            client.User.Email = request.Email;
            client.User.City = request.City;
            client.User.Country = request.Country;
            client.User.CountryCode = request.CountryCode;
            client.User.PostalCode = request.PostalCode;
            client.User.NationalityId = request.NationalityId;
            client.User.ImageUrl = request.ImageUrl;
            client.User.IsActive = request.IsActive;
            client.User.Gender = request.Gender;
            client.User.UpdatedAt = System.DateTime.UtcNow;

            _unitOfWork.Update(client.User);
            await _unitOfWork.SaveChangesAsync(cancellationToken);

            var clientDto = _mapper.Map<ClientProfileDTO>(client);
            return _responseHandler.Success(clientDto);
        }
    }
} 