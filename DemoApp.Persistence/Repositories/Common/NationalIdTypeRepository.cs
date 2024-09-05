using DemoApp.Domain.Interfaces.Repositories.Common;
using DemoApp.Domain.Models.Common;
using DemoApp.Domain.Paging.Models;

namespace DemoApp.Persistence.Repositories.Common
{
    public interface NationalIdTypeRepository : INationalIdTypeRepository
    {
        Task<List<NationalIdType>> GetNationalIdTypesPaged(TableMetadata? tableMetadata = null);
        Task<List<NationalIdType>> GetNationalIdTypes();
        Task<int> GetNationalIdTypesCount();
        Task<NationalIdType> GetNationalId(int id);
        Task InsertNationalIdType(NationalIdType nationalIdType);
        Task UpdateNationalIdType(NationalIdType nationalIdType);
        Task DeleteNationalIdType(int id);
    }
}
