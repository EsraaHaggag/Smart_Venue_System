using AutoMapper;
using Venue_System.Application.Features.Venues.DTO;

namespace Venue_System.Application.Interfaces.Repositories
{
    public interface IVenueRepository : IGenericRepositoryAsync<Venue>
    {
        Task<Venue?> GetByIdWithRulesAsync(Guid id, CancellationToken cancellationToken);

        Task<List<VenueDTo>> GetActiveDtosAsync(IMapper mapper);

        Task<VenueDTo> GetActiveDtosByIdAsync(IMapper mapper, Guid Id);

        Task<Venue?> GetByIdWithWorkingHourAsync(Guid id, CancellationToken cancellationToken);

        IQueryable<Venue> GetQueryable();
    }
}
