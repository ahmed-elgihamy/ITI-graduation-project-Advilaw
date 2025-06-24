using AdviLaw.Application.Basics;
using AdviLaw.Application.JobFields.Dtos;
using AdviLaw.Application.JobFields.Query.GetJobFieldById;
using AdviLaw.Domain.UnitOfWork;
using AutoMapper;
using MediatR;
using Microsoft.Extensions.Logging;
using System.Threading;
using System.Threading.Tasks;

namespace AdviLaw.Application.JobFields.Query.GetById
{
    public class GetJobFieldByIdHandler
        : IRequestHandler<GetJobFieldByIdQuery, Response<JobFieldDto>>
    {
        private readonly IUnitOfWork _uow;
        private readonly IMapper _mapper;
        private readonly ResponseHandler _response;
        private readonly ILogger<GetJobFieldByIdHandler> _logger;

        public GetJobFieldByIdHandler(
            IUnitOfWork uow,
            IMapper mapper,
            ResponseHandler response,
            ILogger<GetJobFieldByIdHandler> logger)
        {
            _uow = uow;
            _mapper = mapper;
            _response = response;
            _logger = logger;
        }

        public async Task<Response<JobFieldDto>> Handle(
            GetJobFieldByIdQuery request,
            CancellationToken cancellationToken)
        {
            _logger.LogInformation("Fetching JobField with Id {Id}", request.Id);

            var entity = await _uow.JobFields.GetByIdAsync(request.Id);
            if (entity == null)
            {
                _logger.LogWarning("JobField with Id {Id} not found", request.Id);
                return _response.NotFound<JobFieldDto>();
            }

            var dto = _mapper.Map<JobFieldDto>(entity);
            return _response.Success(dto);
        }
    }
}
