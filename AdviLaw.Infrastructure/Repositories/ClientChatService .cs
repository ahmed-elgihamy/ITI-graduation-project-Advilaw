using AdviLaw.Application.DTOs.Client;
using AdviLaw.Application.Features.Clients.Queries.GetChat;
using AdviLaw.Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdviLaw.Infrastructure.Repositories
{
    public class ClientChatService : IClientChatService
    {
        private readonly AdviLawDBContext _context;

        public ClientChatService(AdviLawDBContext context)
        {
            _context = context;
        }

    

        public async Task<List<ClientJobChatDto>> GetClientJobChatsAsync(string? clientIdd)
        {
            // Get clientId from clientGuid
            var client = await _context.Clients
                .FirstOrDefaultAsync(c => c.UserId == clientIdd); // or UserId or whatever field

            if (client == null)
                return new List<ClientJobChatDto>(); // Or throw an exception if needed

            var clientId = client.Id;

            var chats = await _context.Jobs
                .Where(j => j.ClientId == clientId && j.Session != null)
                .Select(j => new ClientJobChatDto
                {
                    JobId = j.Id,
                    JobTitle = j.Header,
                    Status = j.Status.ToString(),
                    LawyerName = j.Lawyer != null ? j.Lawyer.User.UserName : "Unassigned",
                    LawyerImageUrl = j.Lawyer != null ? j.Lawyer.User.ImageUrl : null,
                    SessionId = j.Session.EscrowTransaction.SessionId
                })
                .ToListAsync();

            return chats;
        }

    }

}
