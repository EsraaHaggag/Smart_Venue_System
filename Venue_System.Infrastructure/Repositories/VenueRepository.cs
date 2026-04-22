using AutoMapper;
using AutoMapper.QueryableExtensions;
using Microsoft.EntityFrameworkCore;
using Venue_System.Application.Features.Venues.DTO;
using Venue_System.Application.Interfaces.Repositories;
using Venue_System.Infrustrucure.DBContext;

namespace Venue_System.Infrastructure.Repositories
{
    public class VenueRepository
    : GenericRepositoryAsync<Venue>, IVenueRepository
    {
        private readonly DbSet<Venue> _Venue;

        public VenueRepository(ApplictionDBContext dBContext) : base(dBContext)
        {
            _Venue = dBContext.Set<Venue>();
        }

        public async Task<Venue?> GetByIdWithRulesAsync(Guid id, CancellationToken cancellationToken)
        {
            return await _Venue
                .Include(v => v.Rules.Where(r => !r.IsDeleted))
                .FirstOrDefaultAsync(v => v.Id == id && !v.IsDeleted, cancellationToken);
        }

        public async Task<Venue?> GetByIdWithWorkingHourAsync(Guid id, CancellationToken cancellationToken)
        {
            return await _Venue
                .Include(v => v.WorkingHours)
                .FirstOrDefaultAsync(v => v.Id == id && !v.IsDeleted, cancellationToken);
        }

        public async Task<List<VenueDTo>> GetActiveDtosAsync(IMapper mapper)
        {
            return await _Venue
                .Where(v => v.IsActive && v.IsDeleted == false)
                .Include(v => v.Rules.Where(r => !r.IsDeleted))
                .Include(v => v.WorkingHours)
                .ProjectTo<VenueDTo>(mapper.ConfigurationProvider)
                .ToListAsync();
        }

        public async Task<VenueDTo?> GetActiveDtosByIdAsync(IMapper mapper, Guid Id)
        {
            return await _Venue
                .Where(v => v.IsActive && v.Id == Id && v.IsDeleted == false)
                .Include(v => v.Rules.Where(r => !r.IsDeleted))
                .Include(v => v.WorkingHours)
                .ProjectTo<VenueDTo>(mapper.ConfigurationProvider)
                .FirstOrDefaultAsync();
        }

        public IQueryable<Venue> GetQueryable()
        {
            return _Venue.Where(v => v.IsActive && v.IsDeleted == false).AsQueryable();
        }

    }
}
