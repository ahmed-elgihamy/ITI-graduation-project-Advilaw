using AdviLaw.Application.Basics;
using AdviLaw.Domain.Entites;
using AdviLaw.Domain.Repositories;
using AdviLaw.Domain.UnitOfWork;
using AutoMapper;
using MediatR;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdviLaw.Application.Specializations.Command.CreateSpecialization
{

    public class CreateSpecializtionHandler : IRequestHandler<CreateSpeciallizationCommand, Response<object>>
    {
        private readonly ILogger<CreateSpecializtionHandler> _logger;
        private readonly IMapper _mapper;
        private readonly ResponseHandler _responseHandler;
        private readonly IUnitOfWork _unitOfWork;
      
        public CreateSpecializtionHandler(
            ILogger<CreateSpecializtionHandler> logger,
            IMapper mapper,
            ResponseHandler responseHandler,
            IUnitOfWork unitOfWork)
        {
            _logger = logger;
            _mapper = mapper;
            _responseHandler = responseHandler;
            _unitOfWork = unitOfWork;
        }

        public async Task<Response<object>> Handle(CreateSpeciallizationCommand request, CancellationToken cancellationToken)
        {
            _logger.LogInformation("Creating a new specialization...");

            var specialization = _mapper.Map<Specialization>(request);
            var addedSpecialization = await _unitOfWork.Specializations.AddAsync(specialization);
            await _unitOfWork.SaveChangesAsync();
            return _responseHandler.Created<object>(addedSpecialization);
        }
    }
}
