using DemoApp.Domain.Interfaces.Repositories.Geolocation;
using DemoApp.Domain.Models.Geolocation;
using DemoApp.Domain.Paging.Models;
using Microsoft.EntityFrameworkCore;

namespace DemoApp.Persistence.Repositories.Geolocation
{
    public class PlaceRepository : IPlaceRepository
    {
        private readonly CoreDbContext _context;
        public PlaceRepository(CoreDbContext context)
        {
            _context = context;
        }

        public async Task<List<Place>> GetPlacesPaged(TableMetadata? tableMetadata)
        {
            var pagedIndex = tableMetadata?.PagingMetadata.PageIndex == 0 ? 1 : tableMetadata.PagingMetadata.PageIndex;
            var pagingMetadata = tableMetadata.PagingMetadata;
            var query = _context.Places
                .Skip((pagedIndex - 1) * pagingMetadata.PageSize)
                .Take(pagingMetadata.PageSize);

            return await query.ToListAsync();
        }

        public async Task<List<Place>> GetPlaces()
        {
            return await _context.Places.ToListAsync();
        }

        public async Task<int> GetPlacesCount()
        {
            return await _context.Places.CountAsync();
        }

        public async Task<Place> GetPlace(int id)
        {
            return await _context.Places.FindAsync(id);
        }

        public async Task InsertPlace(Place place)
        {
            _context.Places.Add(place);
            await _context.SaveChangesAsync();
        }

        public async Task UpdatePlace(Place place)
        {
            var existingCountry = await _context.Places.FindAsync(place.Id);
            if (existingCountry != null)
            {
                _context.Entry(existingCountry).CurrentValues.SetValues(place);
                await _context.SaveChangesAsync();
            }
        }

        public async Task DeletePlace(int id)
        {
            var country = await _context.Places.FindAsync(id);
            if (country != null)
            {
                _context.Places.Remove(country);
                await _context.SaveChangesAsync();
            }
        }
    }
}
