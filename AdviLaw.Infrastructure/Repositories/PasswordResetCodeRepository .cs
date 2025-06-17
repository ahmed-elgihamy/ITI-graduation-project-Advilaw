using AdviLaw.Domain.Entites.Auth;
using AdviLaw.Domain.Entities;
using AdviLaw.Domain.Repositories;
using AdviLaw.Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;

namespace AdviLaw.Infrastructure.Repositories
{
    public class PasswordResetCodeRepository : IPasswordResetCodeRepository
    {
        private readonly AdviLawDBContext _context;

        public PasswordResetCodeRepository(AdviLawDBContext context)
        {
            _context = context;
        }

        public async Task AddAsync(PasswordResetCode code)
        {
            await _context.PasswordResetCodes.AddAsync(code);
            await _context.SaveChangesAsync();
        }

        public async Task<PasswordResetCode?> GetByEmailAsync(string email)
        {
            return await _context.PasswordResetCodes
                .FirstOrDefaultAsync(c => c.Email == email);
        }

        public async Task DeleteAsync(PasswordResetCode code)
        {
            _context.PasswordResetCodes.Remove(code);
            await _context.SaveChangesAsync();
        }

        public async Task<PasswordResetCode?> GetLatestValidCodeAsync(string email, string code)
        {
            return await _context.PasswordResetCodes
                .Where(x => x.Email == email && x.Code == code)
                .OrderByDescending(x => x.Expiry)
                .FirstOrDefaultAsync();
        }
        public async Task DeleteByEmailAsync(string email)
        {
            var oldCodes = await _context.PasswordResetCodes
                .Where(c => c.Email == email)
                .ToListAsync();

            _context.PasswordResetCodes.RemoveRange(oldCodes);
            await _context.SaveChangesAsync();
        }


    }
}
