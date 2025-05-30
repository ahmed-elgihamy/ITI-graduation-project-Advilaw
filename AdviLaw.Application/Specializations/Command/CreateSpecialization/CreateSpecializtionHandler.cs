using AdviLaw.Domain.Entities;
using AdviLaw.Domain.Repositories;
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

    public class CreateSpecializtionHandler(
         ILogger<CreateSpecializtionHandler> _logger,
         IMapper _mapper,
         ISpecializationRepository specializationRepository) : IRequestHandler<CreateSpeciallizationCommand, int>
    {
        public async Task<int> Handle(CreateSpeciallizationCommand request, CancellationToken cancellationToken)
        {
            _logger.LogInformation("Create new specialization...");


            var specializationDto = _mapper.Map<Specialization>(request);
            int id = await specializationRepository.Create(specializationDto);
            return id;
        }

    }
}
