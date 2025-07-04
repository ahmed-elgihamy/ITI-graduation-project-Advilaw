namespace AdviLaw.Application.DTOs.Client
{
    public class ClientListDto
    {
        public int Id { get; set; }
        public string UserId { get; set; }
        public string UserName { get; set; }
        public string Email { get; set; }
        public string NationalityId { get; set; }
        public string NationalIDImagePath { get; set; } = string.Empty;
        public bool IsApproved { get; set; }
    }
} 