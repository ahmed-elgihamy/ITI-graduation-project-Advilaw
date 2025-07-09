using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AdviLaw.Application.Basics;
using AdviLaw.Domain.Entities.UserSection;
using AdviLaw.Domain.UnitOfWork;
using AutoMapper;
using MediatR;
using Microsoft.AspNetCore.Identity;

namespace AdviLaw.Application.Features.AdminSection.Commands
{
    public class ApproveClientCommandHandler: IRequestHandler<ApproveClientCommand, Response<object>>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly ResponseHandler _responseHandler;
      

        public ApproveClientCommandHandler( IUnitOfWork unitOfWork, ResponseHandler responseHandler)
        {
            _unitOfWork = unitOfWork;
            _responseHandler = responseHandler;
       
        }

        async Task<Response<object>> IRequestHandler<ApproveClientCommand, Response<object>>.Handle(ApproveClientCommand request, CancellationToken cancellationToken)
        {
            var client = await _unitOfWork.Clients.FindFirstAsync(c => c.Id == request.ClientId);
            if (client == null)
            {
                return _responseHandler.NotFound<object>("Client not found");
            }
            if (client.IsApproved == true)
                return _responseHandler.BadRequest<object>("already approved");

            client.IsApproved = true;
            _unitOfWork.Update(client);
            await _unitOfWork.SaveChangesAsync(cancellationToken);

            return _responseHandler.Success<object>("Client approved successfully");
        }
    }
    
}
