using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Stripe;
using Stripe.Identity;
using AdviLaw.Domain.Entities.UserSection;

//Handles all endpoints related to lawyer payouts, Stripe Connect onboarding, and account status.
[ApiController]
    [Route("api/[controller]")]
    public class LawyerPaymentsController : ControllerBase
    {
        private readonly IConfiguration _config;
        private readonly UserManager<User> _userManager;


    public LawyerPaymentsController(IConfiguration config, UserManager<User> userManager)
        {
            _config = config;
            _userManager = userManager;
    }

  
        [HttpPost("create-stripe-account")]
        public async Task<IActionResult> CreateStripeAccount()
        {
            var lawyerUserId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            var lawyerEmail = User.FindFirstValue(ClaimTypes.Email);

            if (string.IsNullOrEmpty(lawyerUserId) || string.IsNullOrEmpty(lawyerEmail))
                return Unauthorized("User not authenticated.");

            var user = await _userManager.FindByIdAsync(lawyerUserId);
            if (user == null)
                return NotFound("User not found.");

            if (!string.IsNullOrEmpty(user.StripeAccountId))
                return Conflict("Stripe account already exists for this user.");

            try
            {
                StripeConfiguration.ApiKey = _config["Stripe:SecretKey"];
                var accountService = new AccountService();
                var accountOptions = new AccountCreateOptions
                {
                    Type = "express",
                    Country = "US",
                    Email = lawyerEmail,
                    Capabilities = new AccountCapabilitiesOptions
                    {
                        Transfers = new AccountCapabilitiesTransfersOptions { Requested = true }
                    }
                };
                var account = await accountService.CreateAsync(accountOptions);

                user.StripeAccountId = account.Id;
                await _userManager.UpdateAsync(user);

                return Ok(new { StripeAccountId = account.Id });
            }
            catch (StripeException ex)
            {
                // Log ex
                return StatusCode(502, $"Stripe error: {ex.Message}");
            }
            catch (Exception ex)
            {
                // Log ex
                return StatusCode(500, "An unexpected error occurred.");
            }
        }


        [HttpGet("stripe-onboarding-link")]
        public async Task<IActionResult> GetStripeOnboardingLink()
        {
            var lawyerUserId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            var user = await _userManager.FindByIdAsync(lawyerUserId);
            if (user == null)
                return NotFound("User not found.");
            if (string.IsNullOrEmpty(user.StripeAccountId))
                return BadRequest("Stripe account not created yet.");

            try
            {
                StripeConfiguration.ApiKey = _config["Stripe:SecretKey"];
                var accountLinkService = new AccountLinkService();
                var options = new AccountLinkCreateOptions
                {
                    Account = user.StripeAccountId,
                    RefreshUrl = "http://localhost:4200/stripe-onboarding-refresh",
                    ReturnUrl = "http://localhost:4200/stripe-onboarding-complete",
                    Type = "account_onboarding"
                };
                var accountLink = await accountLinkService.CreateAsync(options);
                return Ok(new { Url = accountLink.Url });
            }
            catch (StripeException ex)
            {
                return StatusCode(502, $"Stripe error: {ex.Message}");
            }
            catch (Exception ex)
            {
                return StatusCode(500, "An unexpected error occurred.");
            }
        }


        [HttpGet("stripe-account-status")]
        public async Task<IActionResult> GetStripeAccountStatus()
        {
            var lawyerUserId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            var user = await _userManager.FindByIdAsync(lawyerUserId);
            if (user == null)
                return NotFound("User not found.");
            if (string.IsNullOrEmpty(user.StripeAccountId))
                return BadRequest("Stripe account not created yet.");

            try
            {
                StripeConfiguration.ApiKey = _config["Stripe:SecretKey"];
                var accountService = new AccountService();
                var account = await accountService.GetAsync(user.StripeAccountId);
                return Ok(new
                {
                    account.Id,
                    account.Email,
                    account.Capabilities,
                    account.DetailsSubmitted,
                    account.PayoutsEnabled,
                    account.Requirements,
                    account.ChargesEnabled
                });
            }
            catch (StripeException ex)
            {
                return StatusCode(502, $"Stripe error: {ex.Message}");
            }
            catch (Exception ex)
            {
                return StatusCode(500, "An unexpected error occurred.");
            }
        }


        [HttpGet("stripe-dashboard-link")]
        public async Task<IActionResult> GetStripeDashboardLink()
        {
            var lawyerUserId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            var user = await _userManager.FindByIdAsync(lawyerUserId);
            if (user == null)
                return NotFound("User not found.");
            if (string.IsNullOrEmpty(user.StripeAccountId))
                return BadRequest("Stripe account not created yet.");

            try
            {
                StripeConfiguration.ApiKey = _config["Stripe:SecretKey"];
                var loginLinkService = new AccountLoginLinkService();
                var loginLink = await loginLinkService.CreateAsync(user.StripeAccountId);
                return Ok(new { Url = loginLink.Url });
            }
            catch (StripeException ex)
            {
                return StatusCode(502, $"Stripe error: {ex.Message}");
            }
            catch (Exception ex)
            {
                return StatusCode(500, "An unexpected error occurred.");
            }
        }

    }
