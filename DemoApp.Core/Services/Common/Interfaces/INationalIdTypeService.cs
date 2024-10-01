using DemoApp.Core.Dtos.Common;
using DemoApp.Domain.Paging;
using DemoApp.Domain.Paging.Models;

namespace DemoApp.Core.Services.Common.Interfaces
{
    public interface INationalIdTypeService
    {
        Task<PagedListDto<NationalIdTypeDto>> GetNationalIdTypePaged(TableMetadata? tableMetadata);
        Task<List<NationalIdTypeDto>> GetNationalIdTypes();
        Task<int> GetNationalIdTypesCount();
        Task<NationalIdTypeDto> GetNationalIdType(int id);
        Task InsertNationalIdType (NationalIdTypeDto nationalIdTypeDto);
        Task UpdateNationalIdType (NationalIdTypeDto nationalIdTypeDto);
        Task DeleteNationalIdType (int id);
    }
}
