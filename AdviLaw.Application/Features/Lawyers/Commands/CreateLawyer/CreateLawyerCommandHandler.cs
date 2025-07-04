using AdviLaw.Application.Basics;
using AdviLaw.Application.DTOs.Lawyer;
using AdviLaw.Domain.Entites.JobSection;
using AdviLaw.Domain.Entities.UserSection;
using AdviLaw.Domain.UnitOfWork;
using AutoMapper;
using MediatR;
using Microsoft.AspNetCore.Identity;

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



            var uploadsPath = Path.Combine(Directory.GetCurrentDirectory(), "Uploads");
            Directory.CreateDirectory(uploadsPath);

            var nationalIdImagePath = Path.Combine(uploadsPath, request.NationalIDImage.FileName);
            var barCardImagePath = Path.Combine(uploadsPath, request.BarCardImage.FileName);

            using (var stream = new FileStream(nationalIdImagePath, FileMode.Create))
                await request.NationalIDImage.CopyToAsync(stream);

            using (var stream = new FileStream(barCardImagePath, FileMode.Create))
                await request.BarCardImage.CopyToAsync(stream);

            var lawyer = _mapper.Map<Lawyer>(request);
            lawyer.IsApproved = false;
            lawyer.BarCardImagePath = "/Uploads/" + request.BarCardImage.FileName;
            lawyer.NationalIDImagePath = "/Uploads/" + request.NationalIDImage.FileName;
            lawyer.Fields = request.FieldIds
                .Select(id => new LawyerJobField
                {
                    JobFieldId = id 
                }).ToList();


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