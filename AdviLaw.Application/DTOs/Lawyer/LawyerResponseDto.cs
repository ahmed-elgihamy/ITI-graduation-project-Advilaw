using Microsoft.AspNetCore.Http;
using System.ComponentModel.DataAnnotations;

namespace AdviLaw.Application.DTOs.Lawyer
{
    public class LawyerResponseDto
    {




            public int BarAssociationCardNumber { get; set; }

        public IFormFile BarCardImage { get; set; } = null!;


    }
}