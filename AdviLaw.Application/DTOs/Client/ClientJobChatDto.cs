using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdviLaw.Application.DTOs.Client
{
    public class ClientJobChatDto
    {
        public int JobId { get; set; }
        public int? SessionId { get; set; }
        public string JobTitle { get; set; }
        public string Status { get; set; }
        public string LawyerName { get; set; }
        public string LawyerImageUrl { get; set; }
    }


}
