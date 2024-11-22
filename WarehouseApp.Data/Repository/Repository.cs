using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Formats.Tar;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;
using WarehouseApp.Data.Repository.Interfaces;

namespace WarehouseApp.Data.Repository
{
    internal class Repository : IRepository
    {

        private readonly WarehouseDbContext dbContext;
        
        public Repository(WarehouseDbContext _dbContext)
        {
            dbContext = _dbContext;
        }

        private DbSet<T> DbSet<T>() where T : class
        {
            return dbContext.Set<T>();
        }

        public TType? GetById<TType, TId>(TId id) where TType : class
        {
            TType? entity = DbSet<TType>().Find(id);
            return entity;
        }

        public async Task<TType?> GetByIdAsync<TType, TId>(TId id) where TType : class
        {
            TType? entity = await DbSet<TType>().FindAsync(id);
            return entity;
        }

        public IEnumerable<TType> GetAll<TType>() where TType : class
        {
            return DbSet<TType>().ToArray();
        }

        public async Task<IEnumerable<TType>> GetAllAsync<TType>() where TType : class
        {
            return await DbSet<TType>().ToArrayAsync();
        }

        public IEnumerable<TType> GetAllAttached<TType>() where TType : class
        {
            return DbSet<TType>();//asQueryable
        }

        public void Add<TType>(TType item) where TType : class
        {
            DbSet<TType>().Add(item);
            dbContext.SaveChanges();
        }

        public async Task AddAsync<TType>(TType item) where TType : class
        {
            await DbSet<TType>().AddAsync(item);
            await dbContext.SaveChangesAsync();
        }

        public bool Delete<TType,TId>(TId id) where TType : class
        {
            TType? entity = GetById<TType,TId>(id);

            if (entity != null)
            { 
                return false;
            }
            DbSet<TType>().Remove(entity);
            dbContext.SaveChanges();
            return true;
        }

        public async Task<bool> DeleteAsync<TType,TId>(TId id) where TType : class
        {
            TType? entity = await GetByIdAsync<TType, TId>(id);

            if (entity != null)
            {
                return false;
            }
            DbSet<TType>().Remove(entity);
            await dbContext.SaveChangesAsync();
            return true;
        }

        public bool Update<TType>(TType item) where TType : class
        {
            try
            {
                DbSet<TType>().Attach(item);
                dbContext.Entry(item).State = EntityState.Modified;
                dbContext.SaveChanges();
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }

        public async Task<bool> UpdateAsync<TType>(TType item) where TType : class
        {
            try
            {
                DbSet<TType>().Attach(item);
                dbContext.Entry(item).State = EntityState.Modified;
                await dbContext.SaveChangesAsync();
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }   
    }
}
