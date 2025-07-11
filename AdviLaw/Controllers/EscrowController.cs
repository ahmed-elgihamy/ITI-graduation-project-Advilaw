using AdviLaw.Application.Features.EscrowSection.Commands.ConfirmSessionPayment;
using AdviLaw.Application.Features.EscrowSection.Commands.CreateSessionPayment;
using AdviLaw.Application.Features.EscrowSection.Commands.ReleaseSessionFunds;
using AdviLaw.Application.Features.EscrowSection.DTOs;
using AdviLaw.Application.Features.EscrowSection.Queries.GetCompletedSessionsForAdmin;
using AdviLaw.Application.Features.EscrowSection.Queries.GetSessionHistoryForAdmin;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Stripe.Checkout;
using Microsoft.EntityFrameworkCore;
using AdviLaw.Infrastructure.Persistence;
using AdviLaw.Domain.Entites.SessionSection;
using AdviLaw.Domain.Entites.EscrowTransactionSection;


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

        string checkoutUrl = null;
        using (var scope = HttpContext.RequestServices.CreateScope())
        {
            var dbContext = scope.ServiceProvider.GetRequiredService<AdviLawDBContext>();
            var escrow = dbContext.EscrowTransactions.FirstOrDefault(e => e.Id == escResp.Data.EscrowId);
            if (escrow != null && !string.IsNullOrEmpty(escrow.StripeSessionId))
            {

                    var svc = new SessionService();
                    var session = svc.Get(escrow.StripeSessionId);
                    if (session != null && !string.IsNullOrEmpty(session.Url))
                    {
                        checkoutUrl = session.Url;
                    }

            }
        }

        if (string.IsNullOrEmpty(checkoutUrl))
        {
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

            var svcNew = new SessionService();
            var sessionNew = svcNew.Create(options);
            checkoutUrl = sessionNew.Url;

            // Save Stripe session ID to escrow record
            using (var scope = HttpContext.RequestServices.CreateScope())
            {
                var dbContext = scope.ServiceProvider.GetRequiredService<AdviLawDBContext>();
                var escrow = dbContext.EscrowTransactions.FirstOrDefault(e => e.Id == escResp.Data.EscrowId);
                if (escrow != null)
                {
                    escrow.StripeSessionId = sessionNew.Id;
                    dbContext.SaveChanges();
                }
            }
        }

        return Ok(new
        {
            escResp.Data.EscrowId,
            CheckoutUrl = checkoutUrl
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

    [HttpGet("my-escrow")]
    public async Task<IActionResult> GetMyEscrow([FromServices] AdviLawDBContext dbContext)
    {
        var userId = User.FindFirst(System.Security.Claims.ClaimTypes.NameIdentifier)?.Value;
        if (string.IsNullOrEmpty(userId))
            return Unauthorized();

        var escrows = await dbContext.EscrowTransactions
            .Include(e => e.Job)
            .Where(e => e.SenderId == userId)
            .OrderByDescending(e => e.CreatedAt)
            .Select(e => new {
                e.Id,
                e.Amount,
                e.Status,
                e.JobId,
                JobTitle = e.Job.Header,
                e.CreatedAt,
                e.ReleasedAt
            })
            .ToListAsync();

        return Ok(escrows);
    }

    [HttpGet("admin/completed-sessions")]
    public async Task<IActionResult> GetCompletedSessionsForAdmin()
    {
        // Check if user is admin
        var userRole = User.FindFirst(System.Security.Claims.ClaimTypes.Role)?.Value;
        if (userRole != "Admin" && userRole != "SuperAdmin")
            return Forbid("Only admins can access this endpoint");

        var query = new GetCompletedSessionsForAdminQuery();
        var result = await _med.Send(query);

        if (!result.Succeeded)
            return BadRequest(result.Message);

        return Ok(result.Data);
    }

    [HttpGet("admin/session-history")]
    public async Task<IActionResult> GetSessionHistoryForAdmin()
    {
        // Check if user is admin
        var userRole = User.FindFirst(System.Security.Claims.ClaimTypes.Role)?.Value;
        if (userRole != "Admin" && userRole != "SuperAdmin")
            return Forbid("Only admins can access this endpoint");

        var query = new GetSessionHistoryForAdminQuery();
        var result = await _med.Send(query);

        if (!result.Succeeded)
            return BadRequest(result.Message);

        return Ok(result.Data);
    }
}

