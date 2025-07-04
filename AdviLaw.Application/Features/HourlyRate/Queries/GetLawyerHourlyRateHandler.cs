//using AdviLaw.Application.Basics;
//using AdviLaw.Domain.Entities.UserSection;
//using AdviLaw.Domain.IGenericRepo;
//using MediatR;
//using System.Linq.Expressions;

//namespace AdviLaw.Application.Features.LawyerSection.Queries.GetLawyerHourlyRate
//{
//    public class GetLawyerHourlyRateHandler : IRequestHandler<GetLawyerHourlyRateQuery, Response<decimal>>
//    {
//        private readonly IGenericRepository<Lawyer> _lawyerRepository;
//        private readonly ResponseHandler _responseHandler;

//        public GetLawyerHourlyRateHandler(
//            IGenericRepository<Lawyer> lawyerRepository,
//            ResponseHandler responseHandler)
//        {
//            _lawyerRepository = lawyerRepository;
//            _responseHandler = responseHandler;
//        }

//        public async Task<Response<decimal>> Handle(GetLawyerHourlyRateQuery request, CancellationToken cancellationToken)
//        {
//            var includes = new List<Expression<Func<Lawyer, object>>>
//            {
//                l => l.User
//            };

//            var lawyer = await _lawyerRepository.FindFirstAsync(
//                l => l.UserId == request.LawyerUserId.ToString(),
//                includes
//            );

//            if (lawyer == null)
//                return _responseHandler.NotFound<decimal>("Lawyer not found");

//            return _responseHandler.Success(lawyer.HourlyRate);
//        }
//    }
//}
