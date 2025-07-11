using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdviLaw.Application.Features.SessionSection.DTOs
{
   public class SessionDetailsDto
    {
        public int SessionId { get; set; }
        public string JobHeader { get; set; }
        public decimal Budget { get; set; }
        public string LawyerName { get; set; }
        public string LawyerId { get; set; }    
        public string ClientId { get; set; }     

        public string ClientName { get; set; }
        public DateTime AppointmentTime { get; set; }
        public double DurationHours { get; set; }
        public DateTime CreatedAt { get; set; }
    }
}
