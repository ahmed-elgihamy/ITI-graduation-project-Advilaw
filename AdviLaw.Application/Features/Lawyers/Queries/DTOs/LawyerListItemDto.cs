using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdviLaw.Application.Features.Lawyers.Queries.DTOs
{
    public class LawyerListItemDto
    {

        public string Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public List<string> JobFieldNames { get; set; } 
        public int Experience { get; set; }
        public int CaseCount { get; set; }
        public string City { get; set; } = string.Empty;
        public string Country { get; set; } = string.Empty;
        public float Rating { get; set; }
        public string ProfileImageUrl { get; set; } = string.Empty;
    }
}
