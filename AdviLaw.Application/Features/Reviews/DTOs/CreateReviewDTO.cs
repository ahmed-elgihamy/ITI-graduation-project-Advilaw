using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdviLaw.Application.Features.Reviews.DTOs
{
  public class CreateReviewDTO
    {
        public string Text { get; set; } = string.Empty;
        public int Rate { get; set; } 
    }
}
