using AdviLaw.Domain.Entites.SessionUtilities.ReviewSection;
using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdviLaw.Application.Features.Reviews.DTOs.Profiling
{
   public class ReviewProfile :Profile
    {
        public ReviewProfile()
        {
            CreateMap<Review, ReviewDTO>()
                .ForMember(dest => dest.ReviewerName, opt => opt.MapFrom(src => src.Reviewer.UserName))
                .ForMember(dest => dest.ReviewerPhotoUrl, opt => opt.MapFrom(src => src.Reviewer.ImageUrl))
                .ForMember(dest => dest.CreatedAt, opt => opt.MapFrom(src => src.CreatedAt));
        }

    }
}
