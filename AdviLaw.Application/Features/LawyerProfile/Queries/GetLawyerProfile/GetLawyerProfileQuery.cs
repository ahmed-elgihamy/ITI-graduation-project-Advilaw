using AdviLaw.Application.Basics;
using AdviLaw.Application.Features.LawyerProfile.DTOs;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdviLaw.Application.Features.LawyerProfile.Queries.GetLawyerProfile
{
    public class GetLawyerProfileQuery : IRequest<Response<LawyerProfileDTO>>
    {
        public string LawyerId { get; set; } 

        public GetLawyerProfileQuery(string lawyerId)
        {
            LawyerId = lawyerId;
        }
    }


}
