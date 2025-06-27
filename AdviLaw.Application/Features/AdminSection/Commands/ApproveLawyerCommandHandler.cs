using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AdviLaw.Application.Basics;
using AdviLaw.Domain.UnitOfWork;
using MediatR;

namespace AdviLaw.Application.Features.AdminSection.Commands
{
    public class ApproveLawyerCommandHandler : IRequestHandler<ApproveLawyerCommand, Response<object>>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly ResponseHandler _responseHandler;

        public ApproveLawyerCommandHandler(IUnitOfWork unitOfWork, ResponseHandler responseHandler)
        {
            _unitOfWork = unitOfWork;
            _responseHandler = responseHandler;
        }

        public async Task<Response<object>> Handle(ApproveLawyerCommand request, CancellationToken cancellationToken)
        {
            var lawyer = await _unitOfWork.GenericLawyers.FindFirstAsync(c => c.Id == request.lawyerId);
            if (lawyer == null)
                return _responseHandler.NotFound<object>("Lawyer not found");
            if (lawyer.IsApproved == true)
                return _responseHandler.BadRequest<object>("already approved");
            lawyer.IsApproved=true;
            _unitOfWork.Update(lawyer);
            await _unitOfWork.SaveChangesAsync(cancellationToken);
            return _responseHandler.Success<object>("Lawyer approved successfully");


        }
    }
}
