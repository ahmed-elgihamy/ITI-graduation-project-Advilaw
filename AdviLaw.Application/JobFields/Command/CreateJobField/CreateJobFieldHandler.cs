using AdviLaw.Application.Basics;
using AdviLaw.Domain.Entites.JobSection;
using AdviLaw.Domain.UnitOfWork;
using AutoMapper;
using MediatR;
using Microsoft.Extensions.Logging;

namespace AdviLaw.Application.JobFields.Command.CreateJobField
{
    public class CreateJobFieldHandler : IRequestHandler<CreateJobFieldCommand, Response<object>>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        private readonly ResponseHandler _responseHandler;
        private readonly ILogger<CreateJobFieldHandler> _logger;

        public CreateJobFieldHandler(IUnitOfWork unitOfWork, IMapper mapper, ResponseHandler responseHandler, ILogger<CreateJobFieldHandler> logger)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
            _responseHandler = responseHandler;
            _logger = logger;
        }

        public async Task<Response<object>> Handle(CreateJobFieldCommand request, CancellationToken cancellationToken)
        {
            _logger.LogInformation("Creating JobField");

            var entity = _mapper.Map<JobField>(request);
            await _unitOfWork.JobFields.AddAsync(entity);
            await _unitOfWork.SaveChangesAsync();

         
            return _responseHandler.Created<object>(entity);
        }
    }
}
