
using AdviLaw.Application.Basics;
using AdviLaw.Application.Features.JobSection.DTOs;
using AdviLaw.Domain.Entites.JobSection;
using AdviLaw.Domain.UnitOfWork;
using AutoMapper;
using MediatR;

namespace AdviLaw.Application.Features.JobSection.Commands.CreateJob
{
    public class CreateJobHandler : IRequestHandler<CreateJobCommand, Response<CreatedJobDTO>>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        private readonly ResponseHandler _responseHandler;
        public CreateJobHandler(IUnitOfWork unitOfWork, IMapper mapper, ResponseHandler responseHandler)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
            _responseHandler = responseHandler;
        }
        public async Task<Response<CreatedJobDTO>> Handle(CreateJobCommand request, CancellationToken cancellationToken)
        {
            var job = _mapper.Map<Job>(request);
            //job.CreatedAt = DateTime.UtcNow;
            try
            {
                var createdJob = await _unitOfWork.Jobs.AddAsync(job);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex); // Or use a proper logger
                throw;
            }
            await _unitOfWork.SaveChangesAsync();
            var createdJobDTO = _mapper.Map<CreatedJobDTO>(job);
            return _responseHandler.Success(createdJobDTO);
        }
    }
}
