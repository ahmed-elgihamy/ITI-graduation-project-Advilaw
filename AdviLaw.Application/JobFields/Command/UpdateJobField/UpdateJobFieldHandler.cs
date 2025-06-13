using AdviLaw.Application.Basics;
using AdviLaw.Domain.UnitOfWork;
using MediatR;
using Microsoft.Extensions.Logging;

namespace AdviLaw.Application.JobFields.Command.UpdateJobField
{
    public class UpdateJobFieldHandler : IRequestHandler<UpdateJobFieldRequest, Response<object>>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly ResponseHandler _responseHandler;
        private readonly ILogger<UpdateJobFieldHandler> _logger;

        public UpdateJobFieldHandler(IUnitOfWork unitOfWork, ResponseHandler responseHandler, ILogger<UpdateJobFieldHandler> logger)
        {
            _unitOfWork = unitOfWork;
            _responseHandler = responseHandler;
            _logger = logger;
        }

        public async Task<Response<object>> Handle(UpdateJobFieldRequest request, CancellationToken cancellationToken)
        {
            _logger.LogInformation($"Updating JobField with Id {request.Id}");

            var jobField = await _unitOfWork.JobFields.GetByIdAsync(request.Id);
            if (jobField is null)
            {
                return _responseHandler.NotFound<object>("JobField not found");
            }

            jobField.Name = request.Command.Name;
            jobField.Description = request.Command.Description;

            await _unitOfWork.JobFields.UpdateAsync(jobField);
            await _unitOfWork.SaveChangesAsync();

           
            return _responseHandler.Success<object>((object)jobField);
        }
    }
}