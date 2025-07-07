using AdviLaw.Application.Features.EscrowSection.Commands.ConfirmSessionPayment;
using AdviLaw.Application.Features.EscrowSection.Commands.CreateSessionPayment;
using AdviLaw.Application.Features.EscrowSection.Commands.ReleaseSessionFunds;
using AdviLaw.Application.Features.EscrowSection.DTOs;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Stripe.Checkout;
using Microsoft.EntityFrameworkCore;
using AdviLaw.Infrastructure.Persistence;


//Handles escrow payments for legal sessions (client payments, confirming payments, releasing funds).
[ApiController]
[Route("api/[controller]")]
public class EscrowController : ControllerBase
{
    private readonly IMediator _med;

    public EscrowController(IMediator med)
    {
        _med = med;
    }

    [HttpPost("create-session")]
    public async Task<IActionResult> Create([FromBody] CreateSessionPaymentDTO dto)
    {
        var escResp = await _med.Send(new CreateSessionPaymentCommand
        {
            JobId = dto.JobId,
            AppointmentId = dto.AppointmentId,
            ClientId = dto.ClientId
        });

        if (!escResp.Succeeded)
            return BadRequest(escResp.Message);

        var options = new SessionCreateOptions
        {
            PaymentMethodTypes = new List<string> { "card" },
            Mode = "payment",
            LineItems = new List<SessionLineItemOptions>
            {
                new()
                {
                    PriceData = new SessionLineItemPriceDataOptions
                    {
                        UnitAmountDecimal = escResp.Data.Amount * 100,
                        Currency = escResp.Data.Currency,
                        ProductData = new SessionLineItemPriceDataProductDataOptions
                        {
                            Name = $"Job #{dto.JobId} Escrow Payment"
                        },
                    },
                    Quantity = 1
                }
            },
            SuccessUrl = $"http://localhost:4200/payment-success?session_id={{CHECKOUT_SESSION_ID}}&escrow_id={escResp.Data.EscrowId}",
            CancelUrl = "http://localhost:4200/payment-cancel",
            Metadata = new Dictionary<string, string>
            {
                { "EscrowId", escResp.Data.EscrowId.ToString() }
            }
        };

        var svc = new SessionService();
        var session = svc.Create(options);

        // Save Stripe session ID to escrow record
        using (var scope = HttpContext.RequestServices.CreateScope())
        {
            var dbContext = scope.ServiceProvider.GetRequiredService<AdviLawDBContext>();
            var escrow = dbContext.EscrowTransactions.FirstOrDefault(e => e.Id == escResp.Data.EscrowId);
            if (escrow != null)
            {
                escrow.StripeSessionId = session.Id;
                dbContext.SaveChanges();
            }
        }

        return Ok(new
        {
            escResp.Data.EscrowId,
            CheckoutUrl = session.Url
        });
    }

    [HttpPost("confirm-session")]
    public async Task<IActionResult> Confirm([FromBody] ConfirmSessionPaymentDTO dto)
    {
        var result = await _med.Send(new ConfirmSessionPaymentCommand
        {
            StripeSessionId = dto.StripeSessionId
        });

        if (!result.Succeeded)
            return BadRequest(result.Message);

        return Ok(new
        {
            Message = "Escrow marked as completed.",
            SessionId = result.Data
        });
    }


    [HttpPost("release-session-funds")]
    public async Task<IActionResult> ReleaseSessionFunds([FromBody] ReleaseSessionFundsDTO dto)
    {
        var command = new ReleaseSessionFundsCommand
        {
            SessionId = dto.SessionId
        };

        var result = await _med.Send(command);

        if (!result.Succeeded)
            return BadRequest(result.Message);

        return Ok(new
        {
            Message = "Funds released to lawyer successfully",
            PaymentId = result.Data
        });
    }
}

