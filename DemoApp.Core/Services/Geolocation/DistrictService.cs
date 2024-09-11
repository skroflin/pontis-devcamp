using DemoApp.Core.Dtos.Geolocation;
using DemoApp.Core.Services.Geolocation.Interfaces;
using DemoApp.Domain.Interfaces.Repositories.Geolocation;
using DemoApp.Domain.Paging;
using DemoApp.Domain.Paging.Models;

namespace DemoApp.Core.Services.Geolocation
{
    public class DistrictService : IDistrictService
    {
        private readonly IDistrictRepository _districtRepository;

        public DistrictService(IDistrictRepository districtRepository)
        {
            _districtRepository = districtRepository;
        }

        public async Task DeleteDistrict(int id)
        {
            await _districtRepository.DeleteDistrict(id);
        }

        public async Task<DistrictDto> GetDistrict(int id)
        {
            var district = await _districtRepository.GetDistrict(id);
            return DistrictDto.CreateDto(district);
        }

        public async Task<PagedListDto<DistrictDto>> GetDistrictPaged(TableMetadata? tableMetadata)
        {
            var count = await _districtRepository.GetDistrictCount();
            var districts = await _districtRepository.GetDistrictsPaged(tableMetadata);
            var districtDtos = new List<DistrictDto>();
            foreach (var district in districts) 
            {
                districtDtos.Add(DistrictDto.CreateDto(district));
            }
            var pagingMetadata = new PagingMetadata(count, tableMetadata.PagingMetadata);

            return new PagedListDto<DistrictDto> { PagedData = districtDtos, PagingMetadata = pagingMetadata };
        }

        public async Task<List<DistrictDto>> GetDistricts()
        {
            var districts = await _districtRepository.GetDistricts();
            var districtDtos = new List<DistrictDto>();
            foreach(var district in districts)
            {
                districtDtos.Add(DistrictDto.CreateDto(district));
            }
            return districtDtos;

        }

        public async Task<int> GetDistrictsCount()
        {
            return await _districtRepository.GetDistrictCount();
        }

        public async Task InsertDistrict(DistrictDto districtDto)
        {
            await _districtRepository.InsertDistrict(DistrictDto.ToModel(districtDto));
        }

        public async Task UpdateDistrict(DistrictDto districtDto)
        {
            await _districtRepository.UpdateDistrict(DistrictDto.ToModel(districtDto, true));
        }
    }
}
