namespace server.Data.Tokens
{
    public class JwtSetting
    {
        public string ValidAudience { get; set; }
        public string ValidIssuer { get; set; }
        public string SecurityKey { get; set; }
        public int ExpireInMinutes { get; set; }
        public int RefreshTokenValidityIn { get; set; }
    }
}
