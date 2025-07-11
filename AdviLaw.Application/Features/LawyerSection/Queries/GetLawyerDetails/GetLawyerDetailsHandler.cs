using AdviLaw.Application.Basics;
using AdviLaw.Application.Features.JobSection.Commands.CreateJob;
using AdviLaw.Application.Features.JobSection.DTOs;
using AdviLaw.Application.Features.LawyerSection.DTOs;
using AdviLaw.Domain.Entities.UserSection;
using AdviLaw.Domain.UnitOfWork;
using AutoMapper;
using MediatR;
using System.Linq.Expressions;

namespace AdviLaw.Application.Features.LawyerSection.Queries.GetLawyerDetails
{
    public class GetLawyerDetailsHandler(
        IUnitOfWork unitOfWork,
        ResponseHandler responseHandler,
        IMapper mapper
            ) : IRequestHandler<GetLawyerDetailsQuery, Response<LawyerDetailsDTO>>
    {
        private readonly IUnitOfWork unitOfWork = unitOfWork ?? throw new ArgumentNullException(nameof(unitOfWork));
        private readonly ResponseHandler responseHandler = responseHandler ?? throw new ArgumentNullException(nameof(responseHandler));
        private readonly IMapper mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));

        public async Task<Response<LawyerDetailsDTO>> Handle(GetLawyerDetailsQuery request, CancellationToken cancellationToken)
        {
            var lawyer = await unitOfWork.Lawyers.GetByIdIncludesAsync(
                request.Id,
                includes: new List<Expression<Func<Lawyer, object>>>()
                {
                    x => x.User!
                }
            );
            if (lawyer == null)
            {
                return responseHandler.NotFound<LawyerDetailsDTO>("Lawyer not found.");
            }
            var lawyerDetailsDto = mapper.Map<LawyerDetailsDTO>(lawyer);
            var response = responseHandler.Success(lawyerDetailsDto, "Lawyer details retrieved successfully.");
            return response;
        }
    }
}
