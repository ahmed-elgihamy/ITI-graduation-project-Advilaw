using AdviLaw.Application.Basics;
using AdviLaw.Application.Features.Reviews.DTOs;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdviLaw.Application.Features.Reviews.Queries
{
    public class GetReviewsByLawyerQuery : IRequest<Response<List<ReviewDTO>>>
    {
        public Guid LawyerId { get; set; }

        public GetReviewsByLawyerQuery(Guid lawyerId)
        {
            LawyerId = lawyerId;
        }
    }

}
