using AdviLaw.Application.Basics;
using AdviLaw.Domain.UnitOfWork;
using MediatR;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdviLaw.Application.JobFields.Command.DeleteJobField
{
   public class DeleteJobFieldHandler : IRequestHandler<DeleteJobFieldCommand, Response<object>>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly ResponseHandler _responseHandler;
        private readonly ILogger<DeleteJobFieldHandler> _logger;

        public DeleteJobFieldHandler(
            IUnitOfWork unitOfWork,
            ResponseHandler responseHandler,
            ILogger<DeleteJobFieldHandler> logger)
        {
            _unitOfWork = unitOfWork;
            _responseHandler = responseHandler;
            _logger = logger;
        }

        public async Task<Response<object>> Handle(DeleteJobFieldCommand request, CancellationToken cancellationToken)
        {
            _logger.LogInformation("Attempting to delete JobField with Id {Id}", request.Id);

            var entity = await _unitOfWork.JobFields.GetByIdAsync(request.Id);
            if (entity == null)
            {
                _logger.LogWarning("JobField with Id {Id} not found", request.Id);
                return _responseHandler.NotFound<object>($"JobField with Id {request.Id} not found");
            }

            await _unitOfWork.JobFields.DeleteAsync(entity);
            await _unitOfWork.SaveChangesAsync();

            _logger.LogInformation("Deleted JobField with Id {Id}", request.Id);
            return _responseHandler.Deleted<object>();
        }
    }
}
