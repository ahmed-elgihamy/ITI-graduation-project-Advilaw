using AdviLaw.Domain.Entities.UserSection;
using AdviLaw.Domain.Enums;

namespace AdviLaw.Application.Features.UserSections.DTOs
{
    public class UserListDTO
    {
        public int Id { get; set; }
        public string UserName { get; set; } = string.Empty;
        public string City { get; set; } = string.Empty;
        public string Country { get; set; } = string.Empty;
        public string ImageUrl { get; set; } = string.Empty;
        public UserRole Role { get; set; } = UserRole.Client;
        public Gender Gender { get; set; }
    }
}
