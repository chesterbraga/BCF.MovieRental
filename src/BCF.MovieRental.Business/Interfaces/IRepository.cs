using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading.Tasks;
using BCF.MovieRental.Business.Models;

namespace BCF.MovieRental.Business.Interfaces
{
    public interface IRepository<TEntity> : IDisposable where TEntity : Entity
    {
        Task Add(TEntity entity);
        Task<TEntity> Get(int id);
        Task<IEnumerable<TEntity>> Get();        
        Task Update(TEntity entity);
        Task Remove(int id);
        Task<IEnumerable<TEntity>> Get(Expression<Func<TEntity, bool>> predicate);
        Task<int> SaveChanges();
    }
}