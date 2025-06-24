using System.Linq.Expressions;
using AdviLaw.Domain.Entites.RefreshToken;

public interface IRefreshTokenRepository
{
    Task<RefreshToken?> GetByTokenAsync(string token, CancellationToken cancellationToken = default);
    Task AddAsync(RefreshToken token, CancellationToken cancellationToken = default);
    void Update(RefreshToken token, CancellationToken cancellationToken = default);
    Task<RefreshToken?> FindFirstAsync(Expression<Func<RefreshToken, bool>> predicate);
    Task DeleteAllAsync(Expression<Func<RefreshToken, bool>> predicate);
}