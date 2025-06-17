using AdviLaw.Domain.Entites.Auth;

public interface IPasswordResetCodeRepository
{
    Task AddAsync(PasswordResetCode code);
    Task<PasswordResetCode?> GetByEmailAsync(string email);
    Task DeleteAsync(PasswordResetCode code);
    Task<PasswordResetCode?> GetLatestValidCodeAsync(string email, string code);
    Task DeleteByEmailAsync(string email);


}
