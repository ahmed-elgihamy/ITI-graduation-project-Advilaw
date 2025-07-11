using AdviLaw.Application.Features.PlatformSubscriptionSection.DTOs;
using AdviLaw.Application.Features.PlatformSubscriptionSection.Commans.BuyPlatformSubscription;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using Stripe.Checkout;

// Handles subscription payments (lawyer subscriptions to the platform). 

[ApiController]
[Route("api/[controller]")]
public class CheckoutController : ControllerBase
{
    private readonly IMediator _mediator;

    public CheckoutController(IMediator mediator)
    {
        _mediator = mediator;
    }

    [HttpPost("create-lawyer-subscription-session")]
    public IActionResult CreateSession([FromBody] CreateLawyerSubscriptionDTO dto)
    {
        var lineItems = dto.Subscriptions.Select(sub => new SessionLineItemOptions
        {
            PriceData = new SessionLineItemPriceDataOptions
            {
                UnitAmountDecimal = sub.Amount * 100,
                Currency = "usd",
                ProductData = new SessionLineItemPriceDataProductDataOptions
                {
                    Name = sub.SubscriptionName
                }
            },
            Quantity = 1
        }).ToList();

        var options = new SessionCreateOptions
        {
            PaymentMethodTypes = new List<string> { "card" },
            Mode = "payment",
            SuccessUrl = "http://localhost:4200/subscription-success?session_id={CHECKOUT_SESSION_ID}",
            CancelUrl = "http://localhost:4200/subscription-cancel",
            LineItems = lineItems,
            Metadata = new Dictionary<string, string>
            {
                { "LawyerId", dto.LawyerId },
                { "Subscriptions", JsonConvert.SerializeObject(dto.Subscriptions) }
            }
        };

        var service = new SessionService();
        var session = service.Create(options);
        return Ok(new { url = session.Url });
    }

    [HttpPost("confirm-session")]
    public async Task<IActionResult> ConfirmSession([FromBody] ConfirmSessionDTO dto)
    {
        var service = new SessionService();
        var session = await service.GetAsync(dto.SessionId);

        if (session.PaymentStatus == "paid")
        {
            var lawyerId = session.Metadata["LawyerId"];
            var subs = JsonConvert.DeserializeObject<List<SingleSubscriptionDTO>>(session.Metadata["Subscriptions"]);

            var results = new List<CreatedSubscriptionResultDTO>();

            foreach (var sub in subs)
            {
                var result = await _mediator.Send(new BuyPlatformSubscriptionCommand
                {
                    LawyerId = lawyerId,
                    SubscriptionTypeId = sub.SubscriptionTypeId,
                    SubscriptionName = sub.SubscriptionName
                });

                if (result.Succeeded && result.Data != null)
                {
                    results.Add(result.Data);
                }
                else
                {
                    return BadRequest(result.Message);
                }
            }

            // Fetch the lawyer's updated points
            var lawyerProfileResult = await _mediator.Send(
                new AdviLaw.Application.Features.LawyerProfile.Queries.GetLawyerProfile.GetLawyerProfileQuery(lawyerId)
            );
            int updatedPoints = 0;
            if (lawyerProfileResult.Succeeded && lawyerProfileResult.Data != null)
            {
                updatedPoints = lawyerProfileResult.Data.Points;
            }

            return Ok(new { results, updatedPoints });
        }

        return BadRequest("Payment not successful.");
    }
}
