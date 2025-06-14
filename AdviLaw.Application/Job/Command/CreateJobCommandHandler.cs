using AdviLaw.Application.Basics;
using AdviLaw.Application.Job.Dtos;
using AdviLaw.Application.JobFields.Command.CreateJobField;
using AdviLaw.Domain.Entites.JobSection;
using AdviLaw.Domain.UnitOfWork;
using AutoMapper;
using MediatR;
using AdviLaw.Application.Basics;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Microsoft.Kiota.Abstractions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace AdviLaw.Application.Job.Command
{
    public class CreateJobCommandHandler : IRequestHandler<CreateJobCommand, Response<JobDto>>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        private readonly IResponseHandler _response;
        private readonly ILogger<CreateJobCommandHandler> _logger;

        public CreateJobCommandHandler(IUnitOfWork unitOfWork, ILogger<CreateJobCommandHandler> logger, IMapper mapper, IResponseHandler response)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
            _response = response;
            _logger = logger;
        }

        public async Task<Response<JobDto>> Handle(CreateJobCommand request, CancellationToken cancellationToken)
        {
            _logger.LogInformation("Creating Job");

            var jobField = await _unitOfWork.JobFields
                .GetTableNoTracking()
                .FirstOrDefaultAsync(x => x.Name == request.Job.JobFieldName, cancellationToken);

            if (jobField == null)
            {
                return Response<JobDto>.BadRequest("Invalid Job Field"); // Corrected method call  
            }

            var job = _mapper.Map<Job>(request.Job);
            job.JobFieldId = jobField.Id;
            job.Status = JobStatus.NotAssigned;
            job.ClientId = int.Parse(request.UserId);

            // Additional logic for saving the job or returning a response might be needed here  
        }
    }
}
