using Microsoft.EntityFrameworkCore;
using Venue_System.Application.Interfaces.Repositories;
using Venue_System.Domain.Entities;
using Venue_System.Infrustrucure.DBContext;

namespace Venue_System.Infrastructure.Repositories
{
    public class CancellationPolicyRepository : GenericRepositoryAsync<CancellationPolicy>, ICancellationPolicyRepository
    {
        private readonly DbSet<CancellationPolicy> _CancellationPolicy;

        public CancellationPolicyRepository(ApplictionDBContext dBContext) : base(dBContext)
        {
            _CancellationPolicy = dBContext.Set<CancellationPolicy>();
        }
    }
}
