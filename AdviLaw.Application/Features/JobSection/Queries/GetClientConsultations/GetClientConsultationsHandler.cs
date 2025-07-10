using AdviLaw.Application.Basics;
using AdviLaw.Application.Features.JobSection.DTOs;
using AdviLaw.Application.Features.Shared.DTOs;
using AdviLaw.Domain.Entites.JobSection;
using AdviLaw.Domain.UnitOfWork;
using AutoMapper;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace AdviLaw.Application.Features.JobSection.Queries.GetClientConsultations
{
    public class GetClientConsultationsHandler : IRequestHandler<GetClientConsultationsQuery, Response<PagedResponse<ClientConsultationDTO>>>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        private readonly ResponseHandler _responseHandler;

        public GetClientConsultationsHandler(IUnitOfWork unitOfWork, IMapper mapper, ResponseHandler responseHandler)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
            _responseHandler = responseHandler;
        }

        public async Task<Response<PagedResponse<ClientConsultationDTO>>> Handle(GetClientConsultationsQuery request, CancellationToken cancellationToken)
        {
            // Only show consultations with lawyers (LawyerProposal)
            var query = await _unitOfWork.Jobs.GetAllAsync(
                filter: j => j.ClientId == request.ClientId && j.Type == JobType.LawyerProposal,
                includes: new List<Expression<Func<Job, object>>>
                {
                    j => j.Lawyer,
                    j => j.Lawyer.User,
                    j => j.JobField
                }
            );

            var totalCount = await query.CountAsync(cancellationToken);

            var pagedJobs = await query
                .Skip((request.PageNumber - 1) * request.PageSize)
                .Take(request.PageSize)
                .ToListAsync(cancellationToken);

            var dtoList = _mapper.Map<List<ClientConsultationDTO>>(pagedJobs);
            
            // Debug logging
            Console.WriteLine($"GetClientConsultationsHandler: Found {pagedJobs.Count} jobs");
            if (dtoList.Any())
            {
                var firstDto = dtoList.First();
                Console.WriteLine($"First DTO: Id={firstDto.Id}, Header={firstDto.Header}, Description={firstDto.Description}, StatusLabel={firstDto.StatusLabel}");
            }
            
            var pagedResponse = new PagedResponse<ClientConsultationDTO>(dtoList, request.PageSize, totalCount, request.PageNumber);

            return _responseHandler.Success(pagedResponse);
        }
    }
}