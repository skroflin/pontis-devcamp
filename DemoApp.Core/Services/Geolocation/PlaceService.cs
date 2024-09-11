using DemoApp.Core.Dtos.Geolocation;
using DemoApp.Core.Services.Geolocation.Interfaces;
using DemoApp.Domain.Interfaces.Repositories.Geolocation;
using DemoApp.Domain.Paging;
using DemoApp.Domain.Paging.Models;

namespace DemoApp.Core.Services.Geolocation
{
    public class PlaceService : IPlaceService
    {
        private readonly IPlaceRepository _placeRepository;

        public PlaceService(IPlaceRepository placeRepository)
        {
            _placeRepository = placeRepository;
        }

        public async Task DeletePlace(int id)
        {
            await _placeRepository.DeletePlace(id);
        }

        public async Task<PagedListDto<PlaceDto>> GetPlacePaged(TableMetadata? tableMetadata)
        {
            var count = await _placeRepository.GetPlacesCount();
            var places = await _placeRepository.GetPlacesPaged(tableMetadata);
            var placeDtos = new List<PlaceDto>();
            foreach (var place in places)
            {
                placeDtos.Add(PlaceDto.CreateDto(place));   
            }
            var pagingMetadata = new PagingMetadata(count, tableMetadata.PagingMetadata);

            return new PagedListDto<PlaceDto> { PagedData = placeDtos, PagingMetadata = pagingMetadata };
        }

        public async Task<List<PlaceDto>> GetPlaces()
        {
            var places = await _placeRepository.GetPlace();
            var placeDtos = new List<PlaceDto>();
            foreach (var place in places)
            {
                placeDtos.Add(PlaceDto.CreateDto(place));
            }
            return placeDtos;

        }

        public async Task<PlaceDto> GetPlace(int id)
        {
            var place = await _placeRepository.GetPlace(id);
            return PlaceDto.CreateDto(place);
        }

        public async Task<int> GetPlacesCount()
        {
            return await _placeRepository.GetPlacesCount();
        }

        public async Task InsertPlace(PlaceDto placeDto)
        {
            await _placeRepository.InsertPlace(PlaceDto.ToModel(placeDto));
        }

        public async Task UpdatePlace(PlaceDto placeDto)
        {
            await _placeRepository.UpdatePlace(PlaceDto.ToModel(placeDto, true));
        }
    }
}
