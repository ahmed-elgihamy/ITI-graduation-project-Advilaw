using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AdviLaw.Application.Basics;
using MediatR;
using Microsoft.AspNetCore.Http;

namespace AdviLaw.Application.Features.Clients.Commands.CreateClient
{
    public class CreateClientCommand : IRequest<Response<object>>
    {
        [Required]
        public string? UserId { get; set; }
        public IFormFile NationalIDImage { get; set; } = null!;
    }
}
