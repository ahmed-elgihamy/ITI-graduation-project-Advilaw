using AdviLaw.Application.Basics;
using AdviLaw.Application.Features.ResetPassword;
using AdviLaw.Domain.Entities.UserSection;
using AdviLaw.Domain.Repositories;
using MediatR;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Logging;
using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

public class ResetPasswordHandler(
    IPasswordResetCodeRepository codeRepository,
    UserManager<User> userManager,
    ILogger<ResetPasswordHandler> logger,
    ResponseHandler responseHandler) : IRequestHandler<ResetPasswordCommand, Response<bool>>
{
    public async Task<Response<bool>> Handle(ResetPasswordCommand request, CancellationToken cancellationToken)
    {
        logger.LogInformation("Starting reset password process for email: {Email}", request.Email);

        var codeEntry = await codeRepository.GetLatestValidCodeAsync(request.Email, request.Code);
        if (codeEntry == null)
        {
            logger.LogWarning("Invalid or missing reset code for email: {Email}", request.Email);
            return responseHandler.BadRequest<bool>("Invalid reset code.");
        }

        if (codeEntry.Expiry < DateTime.UtcNow)
        {
            logger.LogWarning("Reset code expired for email: {Email}. Expired at: {Expiry}", request.Email, codeEntry.Expiry);
            return responseHandler.BadRequest<bool>("Reset code has expired.");
        }

        var user = await userManager.FindByEmailAsync(request.Email);
        if (user == null)
        {
            logger.LogWarning("User not found with email: {Email}", request.Email);
            return responseHandler.NotFound<bool>("User not found.");
        }

        var resetToken = await userManager.GeneratePasswordResetTokenAsync(user);
        var result = await userManager.ResetPasswordAsync(user, resetToken, request.NewPassword);

        if (result.Succeeded)
        {
            await codeRepository.DeleteByEmailAsync(request.Email);
            logger.LogInformation("Password reset successful for email: {Email}", request.Email);
            return responseHandler.Success(true, new { message = "Password reset successful" });
        }

        var errors = string.Join(", ", result.Errors.Select(e => e.Description));
        logger.LogError("Failed to reset password for email: {Email}. Errors: {Errors}", request.Email, errors);

        return responseHandler.BadRequest<bool>("Password reset failed: " + errors);
    }
}
