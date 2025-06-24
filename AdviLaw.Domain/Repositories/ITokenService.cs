using AdviLaw.Domain.Entites.RefreshToken;
using AdviLaw.Domain.Entities.UserSection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdviLaw.Domain.Repositories
{
    public interface ITokenService
    {
        string GenerateAccessToken(User user);
        RefreshToken GenerateRefreshToken();
    }
}
