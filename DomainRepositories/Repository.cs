using System;
using System.Linq;
using Context.Context;
using Repository.Contracts;

namespace DomainRepositories
{
    public abstract class Repository<TEntity> : IRepository<TEntity>
        where TEntity : class, IEntity
    {
        protected PhotoBookContext PhotoBookContext
        {
            get
            {
                return PhotoBookContext.Current;
            }
        }
        public abstract IQueryable<TEntity> GetAll();

        public abstract TEntity GetById(int id);

        public abstract TEntity Create(TEntity entity);

        public abstract void Update(TEntity entity);

        public abstract void RemoveById(int id);

        public abstract void Remove(TEntity entity);

        public IQueryable<TEntity> GetBy(Func<TEntity, bool> predicate)
        {
            return PhotoBookContext.Set<TEntity>().Where(predicate).AsQueryable();
        }
    }
}