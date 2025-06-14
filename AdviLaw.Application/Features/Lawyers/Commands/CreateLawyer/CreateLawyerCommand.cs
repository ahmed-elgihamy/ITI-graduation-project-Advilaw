using AdviLaw.Application.Basics;
using MediatR;
using System.ComponentModel.DataAnnotations;

namespace AdviLaw.Application.Features.Lawyers.Commands.CreateLawyer
{
    public class CreateLawyerCommand : IRequest<Response<object>>
    {
        [Required]
        public string UserId { get; set; }

        [Required]
        [StringLength(200)]
        public string ProfileHeader { get; set; }

        [Required]
        [StringLength(1000)]
        public string ProfileAbout { get; set; }

        [Required]
        public int LawyerCardID { get; set; }

        [Required]
        [StringLength(1000)]
        public string Bio { get; set; }

        [Required]
        public string BarCardImagePath { get; set; }

        [Required]
        public int BarAssociationCardNumber { get; set; }
    }
} 