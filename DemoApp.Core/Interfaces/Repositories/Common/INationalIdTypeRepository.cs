using DemoApp.Core.Models.Common;
using DemoApp.Domain.Paging.Models;

namespace DemoApp.Core.Interfaces.Repositories.Common
{
    public interface INationalIdTypeRepository
    {
        Task<List<NationalIdType>> GetNationalIdTypesPaged(TableMetadata? tableMetadata = null);
        Task<List<NationalIdType>> GetNationalIds();
        Task<int > GetNationalIdTypesCount();
        Task<NationalIdType> GetNationalIdType(int id);
        Task insertNationalIdType (NationalIdType nationalIdType);
        Task updateNationalIdType (NationalIdType nationalIdType);
        Task deleteNationalIdType (int id);
    }
}
