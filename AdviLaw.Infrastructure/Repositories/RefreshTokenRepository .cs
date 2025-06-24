using System.Linq.Expressions;
using AdviLaw.Domain.Entites.RefreshToken;
using AdviLaw.Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;

public class RefreshTokenRepository : IRefreshTokenRepository
{
    private readonly AdviLawDBContext _context;

    public RefreshTokenRepository(AdviLawDBContext context)
    {
        _context = context;
    }

    public async Task<RefreshToken?> GetByTokenAsync(string token, CancellationToken cancellationToken = default)
    {
        return await _context.RefreshTokens
            .Include(r => r.User) 
            .FirstOrDefaultAsync(r => r.Token == token, cancellationToken);
    }

    public async Task AddAsync(RefreshToken token, CancellationToken cancellationToken = default)
    {
        await _context.RefreshTokens.AddAsync(token, cancellationToken);
    }

    public async Task<RefreshToken?> FindFirstAsync(Expression<Func<RefreshToken, bool>> predicate)
    {
        return await _context.RefreshTokens.FirstOrDefaultAsync(predicate);
    }

    public void Update(RefreshToken token, CancellationToken cancellationToken = default)
    {
        _context.RefreshTokens.Update(token);

    }

    public async Task DeleteAllAsync(Expression<Func<RefreshToken, bool>> predicate)
    {
        var tokens = await _context.RefreshTokens.Where(predicate).ToListAsync();
        _context.RefreshTokens.RemoveRange(tokens);
    }
}
