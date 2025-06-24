using System.ComponentModel.DataAnnotations;

namespace AdviLaw.Application.DTOs.Client
{
    public class ClientResponseDto
    {
        [Required]
        public string? UserId { get; set; }
    }
}
