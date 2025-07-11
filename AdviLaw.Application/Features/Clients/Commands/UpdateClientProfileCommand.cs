using MediatR;
using AdviLaw.Application.Basics;
using AdviLaw.Application.Features.Clients.DTOs;

namespace AdviLaw.Application.Features.Clients.Commands
{
    public class UpdateClientProfileCommand : IRequest<Response<ClientProfileDTO>>
    {
        public int ClientId { get; set; }
        public string UserName { get; set; } = string.Empty;
        public string Email { get; set; } = string.Empty;
        public string City { get; set; } = string.Empty;
        public string Country { get; set; } = string.Empty;
        public string CountryCode { get; set; } = string.Empty;
        public string PostalCode { get; set; } = string.Empty;
        public long NationalityId { get; set; }
        public string ImageUrl { get; set; } = string.Empty;
        public bool IsActive { get; set; }
        public AdviLaw.Domain.Enums.Gender Gender { get; set; }
    }
} 