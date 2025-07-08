using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;

namespace AdviLaw.Application.Features.Clients.DTOs
{
    public class UplaodClientImageDTO
    {
        [Required]
        public IFormFile Image { get; set; }
    }
}
