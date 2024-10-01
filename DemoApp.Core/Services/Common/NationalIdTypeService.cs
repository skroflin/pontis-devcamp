using DemoApp.Core.Dtos.Common;
using DemoApp.Core.Services.Common.Interfaces;
using DemoApp.Domain.Interfaces.Repositories.Common;
using DemoApp.Domain.Paging;
using DemoApp.Domain.Paging.Models;

namespace DemoApp.Core.Services.Common
{
    public class NationalIdTypeService : INationalIdTypeService
    {
        private readonly INationalIdTypeRepository _nationalIdTypeRepository;

        public NationalIdTypeService(INationalIdTypeRepository nationalIdTypeRepository)
        {
            _nationalIdTypeRepository = nationalIdTypeRepository;
        }

        public async Task DeleteNationalIdType(int id)
        {
            await _nationalIdTypeRepository.DeleteNationalIdType(id);
        }

        public async Task<NationalIdTypeDto> GetNationalIdType(int id)
        {
            var nationalIdType = await _nationalIdTypeRepository.GetNationalIdType(id);
            return NationalIdTypeDto.CreateDto(nationalIdType);
        }

        public async Task<PagedListDto<NationalIdTypeDto>> GetNationalIdTypePaged(TableMetadata? tableMetadata)
        {
            var count = await _nationalIdTypeRepository.GetNationalIdTypesCount();
            var nationalIdTypes = await _nationalIdTypeRepository.GetNationalIdTypesPaged(tableMetadata);
            var nationalIdTypeDtos = new List<NationalIdTypeDto>();
            foreach (var nationalIdType in nationalIdTypes) 
            {
                nationalIdTypeDtos.Add(NationalIdTypeDto.CreateDto(nationalIdType));
            }
            var pagingMetadata = new PagingMetadata(count, tableMetadata.PagingMetadata);

            return new PagedListDto<NationalIdTypeDto> { PagedData = nationalIdTypeDtos, PagingMetadata = pagingMetadata };
        }

        public async Task<List<NationalIdTypeDto>> GetNationalIdTypes()
        {
            var nationalIdTypes = await _nationalIdTypeRepository.GetNationalIdTypes();
            var nationalIdTypeDtos = new List<NationalIdTypeDto>();
            foreach(var nationalIdType in nationalIdTypes)
            {
                nationalIdTypeDtos.Add(NationalIdTypeDto.CreateDto(nationalIdType));
            }
            return nationalIdTypeDtos;

        }

        public async Task<int> GetNationalIdTypesCount()
        {
            return await _nationalIdTypeRepository.GetNationalIdTypesCount();
        }

        public async Task InsertNationalIdType(NationalIdTypeDto nationalIdTypeDto)
        {
            await _nationalIdTypeRepository.InsertNationalIdType(NationalIdTypeDto.ToModel(nationalIdTypeDto));
        }

        public async Task UpdateNationalIdType(NationalIdTypeDto nationalIdTypeDto)
        {
            await _nationalIdTypeRepository.UpdateNationalIdType(NationalIdTypeDto.ToModel(nationalIdTypeDto, true));
        }
    }
}
