using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdviLaw.Application.DTOs.Client
{
    public class ClientResponseDto
    {
        [Required]
        public string UserId { get; set; }
    }
}
