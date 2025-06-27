using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdviLaw.Application.Features.Reviews.DTOs
{
 public class ReviewDTO
    {
        public int Id { get; set; }
        public string Text { get; set; } = string.Empty;
        public int Rate { get; set; }
        public string ReviewerName { get; set; } = string.Empty;
        public string ReviewerPhotoUrl { get; set; } = string.Empty;
        public DateTime CreatedAt { get; set; }
    }
}
