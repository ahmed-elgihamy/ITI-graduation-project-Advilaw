using AdviLaw.Application.Basics;
using AdviLaw.Domain.Enums;
using MediatR;

namespace AdviLaw.Application.Features.LawyerSection.Commands.UpdateLawyerProfile
{
    public class UpdateLawyerProfileCommand : IRequest<Response<LawyerProfileDTO>>
    {
        public int LawyerId { get; set; }
        public string UserId { get; set; } = string.Empty;
        public string UserName { get; set; } = string.Empty;
        public string ProfileHeader { get; set; } = string.Empty;
        public string ProfileAbout { get; set; } = string.Empty;
        public string? Bio { get; set; } = string.Empty;
        public decimal HourlyRate { get; set; }
        public string City { get; set; } = string.Empty;
        public string Country { get; set; } = string.Empty;
        public string CountryCode { get; set; } = string.Empty;
        public string PostalCode { get; set; } = string.Empty;

        public long NationalityId { get; set; }
        public Gender Gender { get; set; }
    }
}
