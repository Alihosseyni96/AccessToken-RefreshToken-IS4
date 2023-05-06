using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace ProjectApi.Data.Models.IRepositories
{
    public interface IGenericRepository<TEntity> where TEntity : class
    {
        Task AddEntity(TEntity entity);
        Task UpdateEntity(TEntity entity);
        Task<TEntity> GetEntity(Expression<Func<TEntity, bool>> where = null, Expression<Func<TEntity, object>> navigationProperty = null);
        IQueryable<TEntity> Get(Expression<Func<TEntity, bool>> where = null);
        Task<bool> IsExist(Expression<Func<TEntity, bool>> where = null);
        Task Delete(TEntity entity);
    }
}
