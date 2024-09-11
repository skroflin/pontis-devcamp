using DemoApp.Core.Dtos.Geolocation;
using DemoApp.Core.Services.Geolocation.Interfaces;
using DemoApp.Domain.Interfaces.Repositories.Geolocation;
using DemoApp.Domain.Paging;
using DemoApp.Domain.Paging.Models;

namespace DemoApp.Core.Services.Geolocation
{
    public class RegionService : IRegionService
    {
        private readonly IRegionRepository _regionRepository;

        public RegionService(IRegionRepository regionRepository) 
        {
            _regionRepository = regionRepository;
        }
        public async Task DeleteRegion(int id)
        {
            await _regionRepository.DeleteRegion(id);
        }

        public async Task<RegionDto> GetRegion(int id)
        {
            var region = await _regionRepository.GetRegion(id);
            return RegionDto.CreateDto(region);
        }

        public async Task<PagedListDto<RegionDto>> GetRegionPaged(TableMetadata? tableMetadata)
        {
            var count = await _regionRepository.GetRegionsCount();
            var regions = await _regionRepository.GetRegionsPaged(tableMetadata);
            var regionDtos = new List<RegionDto>();
            foreach (var region in regions) 
            {
                regionDtos.Add(RegionDto.CreateDto(region));
            }
            var pagingMetadata = new PagingMetadata(count, tableMetadata.PagingMetadata);

            return new PagedListDto<RegionDto> { PagedData = regionDtos, PagingMetadata = pagingMetadata };
        }

        public async Task<List<RegionDto>> GetRegions()
        {
            var regions = await _regionRepository.GetRegion();
            var regionDtos = new List<RegionDto>();
            foreach (var region in regions)
            {
                regionDtos.Add(RegionDto.CreateDto(region));
            }
            return regionDtos;
        }

        public async Task<int> GetRegionsCount()
        {
            return await _regionRepository.GetRegionsCount();
        }

        public async Task InsertRegion(RegionDto regionDto)
        {
            await _regionRepository.InsertRegion(RegionDto.ToModel(regionDto));
        }

        public async Task UpdateRegion(RegionDto regionDto)
        {
            await _regionRepository.UpdateRegion(RegionDto.ToModel(regionDto, true));
        }
    }
}
