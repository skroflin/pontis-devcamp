using DemoApp.Core.Dtos.Common;
using DemoApp.Domain.Paging;
using DemoApp.Domain.Paging.Models;

namespace DemoApp.Core.Services.Common.Interfaces
{
    public interface IGenderService
    {
        Task<PagedListDto<GenderDto>> GetGenderPaged(TableMetadata? tableMetadata);
        Task<List<GenderDto>> GetGenders();
        Task<int> GetGendersCount();
        Task<GenderDto> GetGender(int id);
        Task InsertGender (GenderDto genderDto);
        Task UpdateGender(GenderDto genderDto);
        Task DeleteGender(int id);
    }
}
