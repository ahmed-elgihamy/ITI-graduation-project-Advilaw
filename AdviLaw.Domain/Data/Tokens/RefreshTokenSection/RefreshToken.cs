using server.Data.Entites.UserSection;

namespace server.Data.Tokens.RefreshTokenSection
{
    public class RefreshToken
    {
        public int Id { get; set; }
        public string Token { get; set; } = null!;
        public DateTime ExpiresAt { get; set; }
        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
        public string? CreatedByIp { get; set; }
        public bool IsRevoked { get; set; } = false;

        public int UserId { get; set; } = null!;
        public User User { get; set; } = null!;
    }

}
