using DAL.Abstract;
using DAL.Entities;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.Concrete
{
    public class SqlRepository : ISqlRepository
    {
        private readonly IAppDBContext context;

        public SqlRepository(IAppDBContext context)
        {
            this.context = context;
        }

        private IDbSet<TEntity> GetEntities<TEntity>() where TEntity : class
        {
            return this.context.Set<TEntity>();
        }

        public IQueryable<TEntity> GetAll<TEntity>() where TEntity : class
        {
            return GetEntities<TEntity>().AsQueryable();
        }

        public void Insert<TEntity>(TEntity entity) where TEntity : class
        {
            GetEntities<TEntity>().Add(entity);
        }

        public void Delete<TEntity>(TEntity entity) where TEntity : class
        {
            GetEntities<TEntity>().Remove(entity);
        }

        public void SaveChanges()
        {
            this.context.SaveChanges();
        }

        public void Dispose()
        {
            if (this.context != null)
            {
                this.context.Dispose();
            }
        }


        public TEntity GetById<TEntity>(int id) where TEntity : BaseModel<int>
        {
            return (from item in GetAll<TEntity>()
                    where item.Id == id
                    select item).SingleOrDefault();
        }
    }
}
