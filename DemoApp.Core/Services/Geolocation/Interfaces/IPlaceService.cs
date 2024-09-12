using DemoApp.Core.Dtos.Geolocation;
using DemoApp.Domain.Paging;
using DemoApp.Domain.Paging.Models;

namespace DemoApp.Core.Services.Geolocation.Interfaces
{
    public interface IPlaceService
    {
        Task<PagedListDto<PlaceDto>> GetPlacePaged (TableMetadata? tableMetadata);
        Task<List<PlaceDto>> GetPlaces();
        Task<int> GetPlacesCount();
        Task<PlaceDto> GetPlace(int id);
        Task InsertPlace(PlaceDto placeDto);
        Task UpdatePlace(PlaceDto placeDto);
        Task DeletePlace(int id);
    }
}
