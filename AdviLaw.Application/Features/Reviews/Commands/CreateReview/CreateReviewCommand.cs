using AdviLaw.Application.Basics;
using AdviLaw.Application.Features.Reviews.DTOs;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdviLaw.Application.Features.Reviews.Commands.CreateReview
{
  public class CreateReviewCommand : IRequest<Response<CreatedReviewDTO>>
    {
        public string UserId { get; set; } = string.Empty;
        public int SessionId { get; set; } 
        public CreateReviewDTO DTO { get; set; } = new();
    }
}
