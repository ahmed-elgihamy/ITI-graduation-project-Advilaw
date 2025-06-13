using AdviLaw.Application.Basics;
using AdviLaw.Domain.Entities.UserSection;
using AdviLaw.Domain.Enums;
using AdviLaw.Domain.IGenericRepo;
using AdviLaw.Domain.UnitOfWork;
using AutoMapper;
using MediatR;
using Microsoft.AspNetCore.Identity;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace AdviLaw.Application.Features.Users.Commands.CreateUser
{
    public class CreateUserCommandHandler : IRequestHandler<CreateUserCommand, object>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        private readonly UserManager<User> _userManager;
        private readonly ResponseHandler _responseHandler;


        public CreateUserCommandHandler(IUnitOfWork unitOfWork,IMapper mapper,UserManager<User> userManager, ResponseHandler responseHandler)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
            _userManager = userManager;
            _responseHandler = responseHandler;
        }

        public async Task<object> Handle(CreateUserCommand request, CancellationToken cancellationToken)
        {
            var user = _mapper.Map<User>(request);
            user.UserName = request.Email; // Using email as username since username is required


            var result = await _userManager.CreateAsync(user, request.Password);
            if (!result.Succeeded)
            {
                return _responseHandler.BadRequest("User creation failed: " + string.Join(", ", result.Errors.Select(e => e.Description)));
            }

            //validate the role
            if (!Enum.IsDefined(typeof(Roles), request.Role))
                return _responseHandler.BadRequest("Invalid role Selected.");



            return _responseHandler.Success(new { userId = user.Id, email = user.Email }, new { timestamp = DateTime.UtcNow });
        }
    }
}