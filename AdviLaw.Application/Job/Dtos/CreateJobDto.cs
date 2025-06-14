using AdviLaw.Domain.Entites.JobSection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdviLaw.Application.Job.Dtos
{
  public class CreateJobDto
    {
        public string Header { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;
        public int Budget { get; set; }
        public string JobFieldName { get; set; } = string.Empty;
        public bool IsAnonymous { get; set; }
    }
}
