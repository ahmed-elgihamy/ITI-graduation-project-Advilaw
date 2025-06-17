using AdviLaw.Application.Basics;
using AdviLaw.Application.Features.SendResetCode;
using AdviLaw.Domain.Entites.Auth;
using AdviLaw.Domain.Entities.UserSection;
using AdviLaw.Domain.Repositories;
using MediatR;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Logging;

public class SendResetCodeHandler(
    IPasswordResetCodeRepository codeRepository,
    IEmailService emailService,
    UserManager<User> _userManager,
    ResponseHandler _responseHandler,
    ILogger<SendResetCodeHandler> logger) : IRequestHandler<SendResetCodeCommand, Response<bool>>
{
    private readonly IPasswordResetCodeRepository _codeRepository = codeRepository;
    private readonly IEmailService _emailService = emailService;
    private readonly ResponseHandler responseHandler = _responseHandler;
    private readonly ILogger<SendResetCodeHandler> _logger = logger;

    public async Task<Response<bool>> Handle(SendResetCodeCommand request, CancellationToken cancellationToken)
    {
   
        var user = await _userManager.FindByEmailAsync(request.Email);

        if (user == null)
        {
            _logger.LogWarning("Password reset requested for non-existing email: {Email}", request.Email);
            return _responseHandler.BadRequest<bool>("Email does not exist");
        }



        await _codeRepository.DeleteByEmailAsync(request.Email);

        var code = $"{Random.Shared.Next(0, 1000000):D6}";

        var resetCode = new PasswordResetCode
        {
            Email = request.Email,
            Code = code,
            Expiry = DateTime.UtcNow.AddMinutes(15)
        };

        await _codeRepository.AddAsync(resetCode);
        await _emailService.SendResetPasswordEmailAsync(request.Email, code);

        _logger.LogInformation("Reset code sent to {Email}", request.Email);

        return _responseHandler.Success(true); ;
    }


}
