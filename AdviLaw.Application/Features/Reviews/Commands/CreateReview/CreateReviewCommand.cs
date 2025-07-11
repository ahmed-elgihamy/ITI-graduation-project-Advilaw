using AdviLaw.Application.Basics;
using AdviLaw.Application.Features.Reviews.DTOs;
using AdviLaw.Domain.Entites.SessionUtilities.ReviewSection;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdviLaw.Application.Features.Reviews.Commands.CreateReview
{
    public class CreateReviewCommand : IRequest<int>
    {
        public string Text { get; set; } = string.Empty;
        public int Rate { get; set; }
        public int SessionId { get; set; }
        public string? ReviewerId { get; set; }
        public string? RevieweeId { get; set; }
        public ReviewType Type { get; set; }
    }

}
