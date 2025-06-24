namespace AdviLaw.Application.DTOs.Users
{
  public  class AuthResponse
    {
        public bool Succeeded { get; set; }
        public string Token { get; set; }
        public string RefreshToken { get; set; }
        public string Error { get; set; }
        public string Role { get; set; }

        public static AuthResponse Success(string token, string refreshToken, string role) => new AuthResponse
        {
            Succeeded = true,
            Token = token,
            RefreshToken = refreshToken,
            Role = role
        };

        public static AuthResponse Failure(string error) => new AuthResponse
        {
            Succeeded = false,
            Error = error
        };
    }
}
