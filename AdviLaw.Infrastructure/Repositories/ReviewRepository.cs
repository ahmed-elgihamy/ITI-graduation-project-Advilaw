using AdviLaw.Domain.Entites.SessionUtilities.ReviewSection;
using AdviLaw.Domain.Repositories;
using AdviLaw.Infrastructure.GenericRepo;
using AdviLaw.Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdviLaw.Infrastructure.Repositories
{
    public class ReviewRepository : GenericRepository<Review>, IReviewRepository
    {

        private readonly AdviLawDBContext _dbContext;
        public ReviewRepository(AdviLawDBContext dbContext) : base(dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<List<Review>> GetReviewsByLawyerId(int lawyerId)
        {
            return await _dbContext.Reviews
                .Include(r => r.Reviewer)
                .Include(r => r.Reviewee)
                .Where(r => r.Reviewee != null && r.Reviewee.LawyerId == lawyerId)
                .OrderByDescending(r => r.CreatedAt)
                .ToListAsync();
        }


    }
}
