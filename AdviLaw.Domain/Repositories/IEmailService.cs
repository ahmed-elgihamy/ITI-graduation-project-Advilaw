using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdviLaw.Domain.Repositories
{
    public interface IEmailService
    {
        Task SendEmailConfirmationAsync(string email, string userId, string token);
        Task SendResetPasswordEmailAsync(string email, string token);
    }
}
