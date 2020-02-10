using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Linq.Expressions;

namespace MMC.Core.Data.SQLServer
{
    public class BaseRepository<T> where T : class
    {
        protected MMCDbContext _MMCDbContext;
        protected IDbSet<T> dbset;
        public BaseRepository()
        {
            _MMCDbContext = new MMCDbContext();
            this.dbset = _MMCDbContext.Set<T>();
        }
        public virtual IDbSet<T> GetDbSet()
        {
            return this.dbset;
        }

        public virtual IEnumerable<T> GetAll()
        {
            return dbset.AsNoTracking().ToArray();
        }

        public virtual IEnumerable<T> GetAll(Expression<Func<T, bool>> where)
        {
            if (where != null)
                return dbset.AsNoTracking().Where(where).ToList();
            else
                return dbset.AsNoTracking().ToArray();
        }

        public virtual IEnumerable<T> GetAll(int maxRecordCount)
        {
            if (maxRecordCount == -1)
                return dbset.ToList();
            else
                return dbset.Take(maxRecordCount);
        }

        public virtual T GetById(int id)
        {
            return dbset.Find(id);
        }

        public virtual T GetById(Guid id)
        {
            return dbset.Find(id);
        }

        public virtual T Add(T item)
        {
            item = dbset.Add(item);
            _MMCDbContext.SaveChanges();
            return item;
        }

        public virtual int Update(T item)
        {
            this.dbset.Attach(item);
            _MMCDbContext.Entry(item).State = EntityState.Modified;
            return _MMCDbContext.SaveChanges();
        }

        public virtual int Update(T item, params Expression<Func<T, object>>[] properties)
        {
            this.dbset.Attach(item);
            DbEntityEntry<T> entry = _MMCDbContext.Entry(item);
            foreach (var property in properties)
            {
                entry.Property(property).IsModified = true;
            }
            return _MMCDbContext.SaveChanges();
        }

        public virtual void Delete(T item)
        {
            dbset.Remove(item);
            _MMCDbContext.SaveChanges();
        }

        public virtual void Delete(int id)
        {
            dbset.Remove(dbset.Find(id));
            _MMCDbContext.SaveChanges();
        }

        public virtual void Delete(Expression<Func<T, bool>> where)
        {
            IEnumerable<T> objects = dbset.Where<T>(where).AsEnumerable();
            foreach (T obj in objects)
                dbset.Remove(obj);
            _MMCDbContext.SaveChanges();
        }
    }
}