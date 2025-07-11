using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading;
using System.Threading.Tasks;
using AdviLaw.Application.DTOs.Client;
using AdviLaw.Domain.Entities.UserSection;
using AdviLaw.Domain.UnitOfWork;
using MediatR;

namespace AdviLaw.Application.Features.Clients.Queries
{
    public class GetAllClientsQueryHandler : IRequestHandler<GetAllClientsQuery, List<ClientListDto>>
    {
        private readonly IUnitOfWork _unitOfWork;
        public GetAllClientsQueryHandler(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }
        public async Task<List<ClientListDto>> Handle(GetAllClientsQuery request, CancellationToken cancellationToken)
        {
            var clients = await _unitOfWork.Clients.GetAllAsync(c => c.IsApproved == false, null, new List<Expression<System.Func<Client, object>>> { c => c.User });
            var clientList = clients.Select(c => new ClientListDto
            {
                Id = c.Id,
                UserId = c.UserId,
                UserName = c.User.UserName,
                Email = c.User.Email,
                NationalityId = c.User.NationalityId.ToString(),
                NationalIDImagePath = c.NationalIDImagePath,
                IsApproved = c.IsApproved
            }).ToList();
            return clientList;
        }
    }
} 