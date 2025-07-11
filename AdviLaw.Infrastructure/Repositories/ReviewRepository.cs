using AdviLaw.Domain.Entites.SessionUtilities.ReviewSection;
using AdviLaw.Domain.Repositories;
using AdviLaw.Infrastructure.GenericRepo;
using AdviLaw.Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;

public class ReviewRepository : GenericRepository<Review>, IReviewRepository
{
    private readonly AdviLawDBContext _dbContext;

    public ReviewRepository(AdviLawDBContext dbContext) : base(dbContext)
    {
        _dbContext = dbContext;
    }

    public async Task AddAsync(Review review, CancellationToken cancellationToken)
    {


        try
        {
            await _dbContext.Reviews.AddAsync(review, cancellationToken);
            await _dbContext.SaveChangesAsync(cancellationToken);
        }
        catch (DbUpdateException ex)
        {
            Console.WriteLine("❌ Failed to save review: " + ex.InnerException?.Message ?? ex.Message);

            throw new ApplicationException("Unable to save the review. Please ensure both users exist.");
        }
    
}

public async Task<List<Review>> GetReviewsByLawyerId(Guid lawyerId)
    {
        var lawyerUserId = lawyerId.ToString();

        return await _dbContext.Reviews
            .Include(r => r.Reviewer)
            .Include(r => r.Reviewee)
            .Where(r =>
                r.Reviewee != null &&
                _dbContext.Lawyers.Any(l => l.UserId == lawyerUserId && l.UserId == r.Reviewee.Id)
            )
            .OrderByDescending(r => r.CreatedAt)
            .ToListAsync();
    }


}

