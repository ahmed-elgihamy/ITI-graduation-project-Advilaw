using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdviLaw.Application.DTOs.Users
{
  public  class AuthResponse
    {
        public bool Succeeded { get; set; }
        public string Token { get; set; }
        public string RefreshToken { get; set; }
        public string Error { get; set; }

        public static AuthResponse Success(string token, string refreshToken) => new AuthResponse
        {
            Succeeded = true,
            Token = token,
            RefreshToken = refreshToken
        };

        public static AuthResponse Failure(string error) => new AuthResponse
        {
            Succeeded = false,
            Error = error
        };
    }
}
