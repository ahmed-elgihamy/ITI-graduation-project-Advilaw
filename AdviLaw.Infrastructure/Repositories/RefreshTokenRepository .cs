﻿using AdviLaw.Domain.Entites.RefreshToken;
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
}
