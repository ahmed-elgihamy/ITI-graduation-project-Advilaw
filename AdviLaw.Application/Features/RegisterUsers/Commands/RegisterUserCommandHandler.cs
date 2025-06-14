using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AdviLaw.Application.Basics;
using AdviLaw.Application.Features.Lawyers.Commands.CreateLawyer;
using AdviLaw.Domain.Entities.UserSection;
using AdviLaw.Domain.Enums;
using AdviLaw.Domain.UnitOfWork;
using AutoMapper;
using MediatR;
using Microsoft.AspNetCore.Identity;

namespace AdviLaw.Application.Features.RegisterUsers.Commands
{
    public class RegisterUserCommandHandler: IRequestHandler<RegisterUserCommand, Response<object>>
    {
        private readonly UserManager<User> _userManager;
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        private readonly ResponseHandler _responseHandler;
        private readonly IMediator _mediator;
        public RegisterUserCommandHandler(IMediator mediator ,UserManager<User> userManager, IUnitOfWork unitOfWork, IMapper mapper, ResponseHandler responseHandler)
        {
            _userManager = userManager;
            _unitOfWork = unitOfWork;
            _mapper = mapper;
            _responseHandler = responseHandler;
            _mediator = mediator;

        }

        public async Task<Response<object>> Handle(RegisterUserCommand request, CancellationToken cancellationToken)
        {

            var dto= request.Dto;
            //check Email
    

            //Create User 1st
            var user = _mapper.Map<User>(dto);

            var result = await _userManager.CreateAsync(user, dto.Password);
            if (!result.Succeeded)
            {
                return _responseHandler.BadRequest<object>("User creation failed: " + string.Join(", ", result.Errors.Select(e => e.Description)));
            }

            //map from Registerdto to Lawyercommand
            if (dto.Role == Roles.Lawyer)
            {
                var command = _mapper.Map<CreateLawyerCommand>(dto);
                command.UserId = user.Id; // manually set UserId
                await _mediator.Send(command); //triggering lawyer creation
            }
            else if (dto.Role == Roles.Client)
            {
                //not handeled yet
            }
            else
            {
                return _responseHandler.BadRequest<object>("Invalid role selected.");
            }
            return _responseHandler.Success<object>(new { userId = user.Id, email = user.Email, role = dto.Role }, new { timestamp = DateTime.UtcNow });

        }
    }
}
