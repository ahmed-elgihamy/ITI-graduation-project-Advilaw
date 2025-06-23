//using AdviLaw.Application.Basics;
//using AdviLaw.Application.Job.Dtos;
//using AdviLaw.Domain.Entites.JobSection;
//using AdviLaw.Domain.UnitOfWork;
//using AutoMapper;
//using MediatR;
//using Microsoft.EntityFrameworkCore;
//using Microsoft.Extensions.Logging;
//using System.Threading;
//using System.Threading.Tasks;

//namespace AdviLaw.Application.Job.Command
//{
//    public class CreateJobCommandHandler : IRequestHandler<CreateJobCommand, Response<JobDto>>
//    {
//        private readonly IUnitOfWork _unitOfWork;
//        private readonly IMapper _mapper;
//        private readonly ResponseHandler _responseHandler;
//        private readonly ILogger<CreateJobCommandHandler> _logger;

//        public CreateJobCommandHandler(
//            IUnitOfWork unitOfWork,
//            ILogger<CreateJobCommandHandler> logger,
//            IMapper mapper,
//            ResponseHandler responseHandler)
//        {
//            _unitOfWork = unitOfWork;
//            _mapper = mapper;
//            _responseHandler = responseHandler;
//            _logger = logger;
//        }

//        public async Task<Response<JobDto>> Handle(CreateJobCommand request, CancellationToken cancellationToken)
//        {
//            _logger.LogInformation("Starting job creation process for client ID {UserId}", request.UserId);

            
//            var jobField = await _unitOfWork.JobFields
//                .GetTableNoTracking()
//                .FirstOrDefaultAsync(x => x.Name == request.Job.JobFieldName, cancellationToken);

//            if (jobField == null)
//            {
//                _logger.LogWarning("Invalid Job Field: {FieldName}", request.Job.JobFieldName);
//                return _responseHandler.BadRequest<JobDto>("Invalid Job Field");
//            }

//            var job = _mapper.Map<Domain.Entites.JobSection.Job>(request.Job);
//            job.JobFieldId = jobField.Id;
//            job.Status = JobStatus.NotAssigned;
//            job.ClientId = int.Parse(request.UserId);

//            if (request.Job.LawyerId.HasValue)
//            {
//                var lawyer = await _unitOfWork.Lawyers
//                    .GetTableNoTracking()
//                    .FirstOrDefaultAsync(x => x.Id == request.Job.LawyerId.Value, cancellationToken);

//                if (lawyer == null)
//                {
//                    _logger.LogWarning("Lawyer not found with ID: {LawyerId}", request.Job.LawyerId);
//                    return _responseHandler.BadRequest<JobDto>("Invalid Lawyer ID");
//                }

//                job.LawyerId = lawyer.Id;
//                job.Type = JobType.LawyerProposal;
//            }
//            else
//            {
//                job.Type = JobType.ClientPublishing;
//            }

//            await _unitOfWork.Jobs.AddAsync(job);
//            await _unitOfWork.SaveChangesAsync();

//            var result = _mapper.Map<JobDto>(job);
//            _logger.LogInformation("Job created successfully with ID {JobId}", job.Id);

//            return _responseHandler.Created(result);
//        }
//    }
//}
