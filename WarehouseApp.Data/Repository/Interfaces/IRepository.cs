using Microsoft.EntityFrameworkCore.Metadata.Internal;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WarehouseApp.Data.Repository.Interfaces
{
    internal interface IRepository 
    {
        TType? GetById<TType, TId>(TId id) where TType : class;

        Task<TType?> GetByIdAsync<TType, TId>(TId id) where TType : class;

        IEnumerable<TType> GetAll<TType>() where TType : class;

        Task<IEnumerable<TType>> GetAllAsync<TType>() where TType : class;

        IEnumerable<TType> GetAllAttached<TType>() where TType : class;

        void Add<TType>(TType item) where TType : class;

        Task AddAsync<TType>(TType item) where TType : class;

        bool Delete<TType,TId>(TId id) where TType : class;

        Task<bool> DeleteAsync<TType, TId>(TId id) where TType : class;

        bool Update<TType>(TType item) where TType : class;

        Task<bool> UpdateAsync<TType>(TType item) where TType : class;
    }
}
