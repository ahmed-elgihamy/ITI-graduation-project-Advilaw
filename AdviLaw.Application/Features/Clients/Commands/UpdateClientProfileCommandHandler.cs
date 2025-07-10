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
            var client = await _unitOfWork.Clients.GetByIdIncludesAsync(request.ClientId, includes: new List<Expression<Func<Client, object>>>
                {
                    j => j.User,
                });
            if (client == null || client.User == null)
                return _responseHandler.NotFound<ClientProfileDTO>("Client not found.");

            // Update user fields only if provided (not default/empty)
            if (!string.IsNullOrEmpty(request.UserName))
                client.User.UserName = request.UserName;
            if (!string.IsNullOrEmpty(request.Email))
                client.User.Email = request.Email;
            if (!string.IsNullOrEmpty(request.City))
                client.User.City = request.City;
            if (!string.IsNullOrEmpty(request.Country))
                client.User.Country = request.Country;
            if (!string.IsNullOrEmpty(request.CountryCode))
                client.User.CountryCode = request.CountryCode;
            if (!string.IsNullOrEmpty(request.PostalCode))
                client.User.PostalCode = request.PostalCode;
            if (request.NationalityId != 0)
                client.User.NationalityId = request.NationalityId;
            if (!string.IsNullOrEmpty(request.ImageUrl))
                client.User.ImageUrl = request.ImageUrl;
            if (request.IsActive != default(bool))
                client.User.IsActive = request.IsActive;
            if ((int)request.Gender != 0)
                client.User.Gender = request.Gender;
            client.User.UpdatedAt = System.DateTime.UtcNow;

            _unitOfWork.Update(client.User);
            await _unitOfWork.SaveChangesAsync(cancellationToken);

            var clientDto = _mapper.Map<ClientProfileDTO>(client);
            return _responseHandler.Success(clientDto);
        }
    }
} 