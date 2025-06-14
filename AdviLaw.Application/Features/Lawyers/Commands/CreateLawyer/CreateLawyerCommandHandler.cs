using AdviLaw.Application.Basics;
using AdviLaw.Application.DTOs.Lawyer;
using AdviLaw.Domain.Entities.UserSection;
using AdviLaw.Domain.IGenericRepo;
using AdviLaw.Domain.UnitOfWork;
using AutoMapper;
using MediatR;
using Microsoft.AspNetCore.Identity;
using Microsoft.Kiota.Abstractions;
using System.Threading;
using System.Threading.Tasks;

namespace AdviLaw.Application.Features.Lawyers.Commands.CreateLawyer
{
    public class CreateLawyerCommandHandler : IRequestHandler<CreateLawyerCommand, Response<object>>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        private readonly ResponseHandler _responseHandler;
        private readonly UserManager<User> _userManager;
        

        public CreateLawyerCommandHandler(IUnitOfWork unitOfWork, IMapper mapper, ResponseHandler responseHandler, UserManager<User> userManager)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
            _responseHandler = responseHandler;
            _userManager = userManager;
        }

        public async Task<Response<object>> Handle(CreateLawyerCommand request, CancellationToken cancellationToken)
        {
            //check user existence
            var user = await _userManager.FindByIdAsync(request.UserId);
            if (user == null)
                return _responseHandler.NotFound<object>("User not found");


            //if lawyer already exists
            var existingLawyer = await _unitOfWork.GenericLawyers.FindFirstAsync(l => l.UserId == request.UserId);
            if (existingLawyer != null)
                return _responseHandler.BadRequest<object>("Lawyer profile already exists for this user");


            //mapping the request to the Lawyer entity
            var lawyer = _mapper.Map<Lawyer>(request);
            lawyer.IsApproved = false; // must be approved by admin

            var result = await _unitOfWork.GenericLawyers.AddAsync(lawyer);
            await _unitOfWork.SaveChangesAsync();

            if(result==null)
             
               return _responseHandler.BadRequest<object>("Lawyer creation failed. Please try again.");

            if (result == null)
            {
                return _responseHandler.BadRequest<object>("Lawyer creation failed. Please try again.");

            }

            //return dto to avoid circular reference issues

            var lawyerDto = _mapper.Map<LawyerResponseDto>(result);
            return _responseHandler.Created<object>(lawyerDto);

        }
    }
} 