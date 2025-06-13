using MediatR;
using System.ComponentModel.DataAnnotations;

namespace AdviLaw.Application.Features.Users.Commands.CreateUser
{
    public class CreateUserCommand : IRequest<object>
    {
        [Required]
        [EmailAddress]
        public string Email { get; set; }

        [Required]
        [StringLength(100)]
        public string FirstName { get; set; }

        [Required]
        [StringLength(100)]
        public string LastName { get; set; }

        [Required]
        [Phone]
        public string PhoneNumber { get; set; }

        [Required]
        [StringLength(100, MinimumLength = 6)]
        public string Password { get; set; }
    }
}