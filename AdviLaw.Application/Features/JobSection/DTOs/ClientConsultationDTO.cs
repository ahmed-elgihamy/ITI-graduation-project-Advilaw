using System;

namespace AdviLaw.Application.Features.JobSection.DTOs
{
    public class ClientConsultationDTO
    {
        public int Id { get; set; }
        public string Header { get; set; }
        public string Description { get; set; }
        public int Budget { get; set; }
        public string StatusLabel { get; set; }
        public string LawyerName { get; set; }
        public string LawyerProfilePictureUrl { get; set; }
        public int? Duration { get; set; }
        public DateTime? AppointmentTime { get; set; }
    }
} 