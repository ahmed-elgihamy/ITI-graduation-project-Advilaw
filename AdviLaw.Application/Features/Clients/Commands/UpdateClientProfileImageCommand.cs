using MediatR;
using Microsoft.AspNetCore.Http;
using AdviLaw.Application.Basics;
using AdviLaw.Application.Features.Clients.DTOs;

namespace AdviLaw.Application.Features.Clients.Commands
{
    public class UpdateClientProfileImageCommand : IRequest<Response<ClientProfileDTO>>
    {
        public int ClientId { get; set; }
        public IFormFile Image { get; set; } = null!;
    }
} 