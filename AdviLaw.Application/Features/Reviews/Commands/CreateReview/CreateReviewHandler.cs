using AdviLaw.Application.Basics;
using AdviLaw.Application.Features.Reviews.DTOs;
using AdviLaw.Domain.UnitOfWork;
using AutoMapper;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdviLaw.Application.Features.Reviews.Commands.CreateReview
{
    public class CreateReviewHandler : IRequestHandler<CreateReviewCommand, Response<CreatedReviewDTO>>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly ResponseHandler _responseHandler;
        private readonly IMapper _mapper;

        public CreateReviewHandler(IUnitOfWork unitOfWork, ResponseHandler responseHandler, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _responseHandler = responseHandler;
            _mapper = mapper;
        }
        public Task<Response<CreatedReviewDTO>> Handle(CreateReviewCommand request, CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }
    }
}
