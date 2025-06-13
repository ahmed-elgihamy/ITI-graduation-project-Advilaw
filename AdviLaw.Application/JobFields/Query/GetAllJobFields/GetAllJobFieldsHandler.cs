using AdviLaw.Application.Basics;
using AdviLaw.Application.JobFields.Dtos;
using AdviLaw.Application.JobFields.Query.GetAllJobFields;
using AdviLaw.Domain.UnitOfWork;
using AutoMapper;
using MediatR;
using Microsoft.Extensions.Logging;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace AdviLaw.Application.JobFields.Query.GetAll
{
    public class GetAllJobFieldsHandler : IRequestHandler<GetAllJobFieldsQuery, Response<List<JobFieldDto>>>
    {
        private readonly IUnitOfWork _uow;
        private readonly IMapper _mapper;
        private readonly ResponseHandler _response;
        private readonly ILogger<GetAllJobFieldsHandler> _logger;

        public GetAllJobFieldsHandler(IUnitOfWork uow, IMapper mapper, ResponseHandler response, ILogger<GetAllJobFieldsHandler> logger)
        {
            _uow = uow;
            _mapper = mapper;
            _response = response;
            _logger = logger;
        }

        public async Task<Response<List<JobFieldDto>>> Handle(GetAllJobFieldsQuery request, CancellationToken cancellationToken)
        {
            _logger.LogInformation("Fetching all JobFields");

            var entities = _uow.JobFields.GetTableNoTracking().ToList();
            var dtos = _mapper.Map<List<JobFieldDto>>(entities);
            return _response.Success(dtos);

        }
    }
}