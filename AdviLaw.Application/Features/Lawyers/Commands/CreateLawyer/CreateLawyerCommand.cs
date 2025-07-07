using AdviLaw.Application.Basics;
using MediatR;
using Microsoft.AspNetCore.Http;
using System.ComponentModel.DataAnnotations;

namespace AdviLaw.Application.Features.Lawyers.Commands.CreateLawyer
{
    public class CreateLawyerCommand : IRequest<Response<object>>
    {
        [Required]
        public string? UserId { get; set; }

        public IFormFile NationalIDImage { get; set; } = null!;
        public IFormFile BarCardImage { get; set; } = null!;

        public List<int> FieldIds { get; set; }

    }
} 