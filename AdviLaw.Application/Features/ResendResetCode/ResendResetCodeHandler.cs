using AdviLaw.Application.Basics;
using AdviLaw.Application.Features.ResendReset;
using AdviLaw.Domain.Entites.Auth;
using AdviLaw.Domain.Entities.UserSection;
using AdviLaw.Domain.Repositories;
using MediatR;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Logging;

public class ResendResetCodeHandler(
    IPasswordResetCodeRepository codeRepository,
    IEmailService emailService,
        UserManager<User> userManager, 
    ILogger<ResendResetCodeHandler> logger,
    ResponseHandler responseHandler

) : IRequestHandler<ResendResetCodeCommand, Response<bool>>
{
    public async Task<Response<bool>> Handle(ResendResetCodeCommand request, CancellationToken cancellationToken)
    {

        var user = await userManager.FindByEmailAsync(request.Email);
        if (user == null)
        {
            logger.LogWarning("Resend reset requested for non-existing email: {Email}", request.Email);
            return responseHandler.BadRequest<bool>("Email does not exist");
        }


        await codeRepository.DeleteByEmailAsync(request.Email);

        var code = $"{Random.Shared.Next(0, 1000000):D6}";

        var resetCode = new PasswordResetCode
        {
            Email = request.Email,
            Code = code,
            Expiry = DateTime.UtcNow.AddMinutes(15)
        };

        await codeRepository.AddAsync(resetCode);
        await emailService.SendResetPasswordEmailAsync(request.Email, code);

        logger.LogInformation("Reset code re-sent to {Email}", request.Email);

        return responseHandler.Success(true, new { request.Email });
    }
}
