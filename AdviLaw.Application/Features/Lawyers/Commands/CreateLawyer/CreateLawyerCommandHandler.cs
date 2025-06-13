using AdviLaw.Application.Basics;
using AdviLaw.Domain.Entities.UserSection;
using AdviLaw.Domain.IGenericRepo;
using AdviLaw.Domain.UnitOfWork;
using AutoMapper;
using MediatR;
using System.Threading;
using System.Threading.Tasks;

namespace AdviLaw.Application.Features.Lawyers.Commands.CreateLawyer
{
    public class CreateLawyerCommandHandler : IRequestHandler<CreateLawyerCommand, Response<object>>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        private readonly ResponseHandler _responseHandler;


        public CreateLawyerCommandHandler(IUnitOfWork unitOfWork, IMapper mapper, ResponseHandler responseHandler)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
            _responseHandler = responseHandler;
        }

        public async Task<Response<object>> Handle(CreateLawyerCommand request, CancellationToken cancellationToken)
        {
            //mapping the request to the Lawyer entity
            var lawyer = _mapper.Map<Lawyer>(request);

            var result = await _unitOfWork.GenericLawyers.AddAsync(lawyer);
            await _unitOfWork.SaveChangesAsync();
            if(result==null)
            { 
               return _responseHandler.BadRequest("Lawyer creation failed. Please try again.");
            }

            return _responseHandler.Created(result);

        }
    }
} 