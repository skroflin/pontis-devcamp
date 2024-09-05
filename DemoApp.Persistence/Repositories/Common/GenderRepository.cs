﻿using DemoApp.Domain.Interfaces.Repositories.Common;
using DemoApp.Domain.Models.Common;
using DemoApp.Domain.Paging.Models;

namespace DemoApp.Persistence.Repositories.Common
{
    public interface GenderRepository : IGenderRepository
    {
        Task<List<Gender>> GetGendersPaged(TableMetadata? tableMetadata = null);
        Task<List<Gender>> GetGenders();
        Task<int> GetGendersCount();
        Task<Gender> GetGender(int id);
        Task InsertGender(Gender gender);
        Task UpdateGender(Gender gender);
        Task DeleteGender(int id);
    }
}
