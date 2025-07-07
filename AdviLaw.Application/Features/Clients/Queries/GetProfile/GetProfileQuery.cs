using AdviLaw.Application.Basics;
using AdviLaw.Application.Features.Clients.DTOs;
using MediatR;

namespace AdviLaw.Application.Features.Clients.Queries.GetProfile
{
    public class GetProfileQuery : IRequest<Response<ClientProfileDTO>>
    {
        public int UserId { get; set; }
        public GetProfileQuery(int userId)
        {
            UserId = userId;
        }
    }
}
