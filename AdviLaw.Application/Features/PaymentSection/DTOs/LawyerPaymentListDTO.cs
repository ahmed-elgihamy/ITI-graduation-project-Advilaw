using AdviLaw.Domain.Entites.PaymentSection;

namespace AdviLaw.Application.Features.PaymentSection.DTOs
{
    public class LawyerPaymentListDTO
    {
        public int Id { get; set; }
        public PaymentType Type { get; set; } = PaymentType.SessionPayment;
        public int Amount { get; set; } //mapped
        public string? SenderId { get; set; }
        public string SenderName { get; set; } = string.Empty; //mapped
        public string SenderImgUrl { get; set; } = string.Empty; //mapped
        public DateTime CreatedAt { get; set; }
    }
}
