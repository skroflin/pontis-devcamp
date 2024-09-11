using DemoApp.Domain.Models.Common;
using DemoApp.Domain.Paging.Models;

namespace DemoApp.Domain.Interfaces.Repositories.Common
{
    public interface INationalIdTypeRepository
    {
        Task<List<NationalIdType>> GetNationalIdTypesPaged(TableMetadata? tableMetadata = null);
        Task<List<NationalIdType>> GetNationalIdTypes();
        Task<int > GetNationalIdTypesCount();
        Task<NationalIdType> GetNationalIdType(int id);
        Task InsertNationalIdType (NationalIdType nationalIdType);
        Task UpdateNationalIdType (NationalIdType nationalIdType);
        Task DeleteNationalIdType (int id);
    }
}
