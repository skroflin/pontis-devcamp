using DemoApp.Core.Dtos.Common;
using DemoApp.Core.Services.Common.Interfaces;
using DemoApp.Domain.Interfaces.Repositories.Common;
using DemoApp.Domain.Paging;
using DemoApp.Domain.Paging.Models;

namespace DemoApp.Core.Services.Common
{
    public class GenderService : IGenderService
    {
        private readonly IGenderRepository _genderRepository;

        public GenderService(IGenderRepository genderRepository) 
        {
            _genderRepository = genderRepository;
        }
        public async Task DeleteGender(int id)
        {
            await _genderRepository.DeleteGender(id);
        }

        public async Task<GenderDto> GetGender(int id)
        {
            var gender = await _genderRepository.GetGender(id);
            return GenderDto.CreateDto(gender);
        }

        public async Task<PagedListDto<GenderDto>> GetGenderPaged(TableMetadata? tableMetadata)
        {
            var count = await _genderRepository.GetGendersCount();
            var genders = await _genderRepository.GetGendersPaged(tableMetadata);
            var genderDtos = new List<GenderDto>();
            foreach (var gender in genders) 
            {
                genderDtos.Add(GenderDto.CreateDto(gender));
            }
            var pagingMetadata = new PagingMetadata(count, tableMetadata.PagingMetadata);

            return new PagedListDto<GenderDto> { PagedData = genderDtos, PagingMetadata = pagingMetadata };
        }

        public async Task<List<GenderDto>> GetGenders()
        {
            var genders = await _genderRepository.GetGenders();
            var gendersDtos = new List<GenderDto>();
            foreach(var gender in genders)
            {
                gendersDtos.Add(GenderDto.CreateDto(gender));
            }
            return gendersDtos;

        }

        public async Task<int> GetGendersCount()
        {
            return await _genderRepository.GetGendersCount();
        }

        public async Task InsertGender(GenderDto genderDto)
        {
            await _genderRepository.InsertGender(GenderDto.ToModel(genderDto));
        }

        public async Task UpdateGender(GenderDto genderDto)
        {
            await _genderRepository.UpdateGender(GenderDto.ToModel(genderDto, true));
        }
    }
}
