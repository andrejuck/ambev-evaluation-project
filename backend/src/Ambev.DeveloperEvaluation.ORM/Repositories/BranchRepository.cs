using Ambev.DeveloperEvaluation.Domain.Entities;
using Ambev.DeveloperEvaluation.Domain.Repositories;
using Microsoft.EntityFrameworkCore;


namespace Ambev.DeveloperEvaluation.ORM.Repositories
{
    public class BranchRepository : IBranchRepository
    {
        private readonly DefaultContext _context;

        /// <summary>
        /// Initializes a new instance of BranchRepository
        /// </summary>
        /// <param name="context">The database context</param>
        public BranchRepository(DefaultContext context)
        {
            _context = context;
        }

        /// <summary>
        /// Retrieves a branch by their unique identifier
        /// </summary>
        /// <param name="id">The unique identifier of the branch</param>
        /// <param name="cancellationToken">Cancellation token</param>
        /// <returns>The branch if found, null otherwise</returns>
        public async Task<Branch?> GetByIdAsync(Guid id, CancellationToken cancellationToken = default)
        {
            return await _context.Branches
                .FirstOrDefaultAsync(o => o.Id == id, cancellationToken);
        }
    }
}
