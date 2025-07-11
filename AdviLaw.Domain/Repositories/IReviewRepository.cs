using AdviLaw.Domain.Entites.SessionUtilities.ReviewSection;
using AdviLaw.Domain.IGenericRepo;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdviLaw.Domain.Repositories
{
 
  
        public interface IReviewRepository : IGenericRepository<Review>
        {
            Task<List<Review>> GetReviewsByLawyerId(Guid lawyerId);
        Task AddAsync(Review review, CancellationToken cancellationToken);
    }

    
}
