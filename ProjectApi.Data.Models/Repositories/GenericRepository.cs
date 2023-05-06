using Microsoft.EntityFrameworkCore;
using ProjectApi.Data.Models.Data;
using ProjectApi.Data.Models.IRepositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace ProjectApi.Data.Models.Repositories
{
    public class GenericRepository<TEntity> : IGenericRepository<TEntity> where TEntity : class
    {
        private ProjectApiContext db;
        private DbSet<TEntity> dbset;
        public GenericRepository(ProjectApiContext context)
        {
            db = context;
            dbset = context.Set<TEntity>();
        }


        public async Task AddEntity(TEntity entity)
        {
            await dbset.AddAsync(entity);
            await db.SaveChangesAsync();
        }

        public async Task Delete(TEntity entity)
        {
            dbset.Remove(entity);
            await db.SaveChangesAsync();
        }



        public IQueryable<TEntity> Get(Expression<Func<TEntity, bool>> where = null)
        {
            if (where != null)
            {
                return dbset.Where(where).AsQueryable();
            }
            return dbset.AsQueryable();
        }

        public async Task<TEntity> GetEntity(Expression<Func<TEntity, bool>> where = null, Expression<Func<TEntity, object>> navigationProperty = null)
        {
            if (navigationProperty != null)
            {
                return await dbset.Include(navigationProperty).SingleOrDefaultAsync(where);
            }
            return await dbset.SingleOrDefaultAsync(where);

        }

        public async Task<bool> IsExist(Expression<Func<TEntity, bool>> where = null)
        {
            return await dbset.AnyAsync(where);
        }

        public async Task UpdateEntity(TEntity entity)
        {
            dbset.Update(entity);
            await db.SaveChangesAsync();
        }




    }
}
