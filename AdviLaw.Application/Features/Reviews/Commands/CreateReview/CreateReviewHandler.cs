// CreateReviewCommandHandler.cs
using AdviLaw.Application.Features.Reviews.Commands.CreateReview;
using AdviLaw.Domain.Entites.SessionUtilities.ReviewSection;
using AdviLaw.Domain.Repositories;
using MediatR;

public class CreateReviewCommandHandler : IRequestHandler<CreateReviewCommand, int>
{
    private readonly IReviewRepository _reviewRepository;

    public CreateReviewCommandHandler(IReviewRepository reviewRepository)
    {
        _reviewRepository = reviewRepository;
    }

    public async Task<int> Handle(CreateReviewCommand request, CancellationToken cancellationToken)
    {
        var review = new Review
        {
            Text = request.Text,
            Rate = request.Rate,
            SessionId = request.SessionId,
            ReviewerId = request.ReviewerId,
            RevieweeId = request.RevieweeId,
            Type = request.Type,
            CreatedAt = DateTime.UtcNow
        };
 

        try
        {
            await _reviewRepository.AddAsync(review, cancellationToken);
            return review.Id;
        }
        catch (Exception ex)
        {
            Console.WriteLine("❌ Exception in handler: " + ex.Message);
            throw;
        }

   
    }
}
