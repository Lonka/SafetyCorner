using SafetyCorner.Models.Interface;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Data.Objects;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using SafetyCorner.Models;

namespace SafetyCorner.Models.Repository
{
    public class GenericRepository<TEntity> : DataAccess, IRepository<TEntity> 
        where TEntity : class
    {
        public DbContext _context
        {
            get;
            set;
        }
        public DbSet<TEntity> table
        {
            set { }
            get
            {
                return this._context.Set<TEntity>();
            }
        }

        public GenericRepository()
            : this(new SafetyCornerEntities())
        {
        }

        public GenericRepository(DbContext context)
        {
            if (context == null)
            {
                throw new ArgumentNullException("context");
            }
            this._context = context;
        }

        public GenericRepository(ObjectContext context)
        {
            if (context == null)
            {
                throw new ArgumentNullException("context");
            }
            this._context = new DbContext(context, true);
        }

        public string sql { set; get; }
        public List<object> plist { set; get; }





        /// <summary>
        /// Creates the specified instance.
        /// </summary>
        /// <param name="instance">The instance.</param>
        /// <exception cref="System.ArgumentNullException">instance</exception>
        public TEntity Create(TEntity instance)
        {
            if (instance == null)
            {
                throw new ArgumentNullException("instance");
            }
            else
            {
                TEntity result = this._context.Set<TEntity>().Add(instance);
                
                this.SaveChanges();
                return result;
            }

        }

        /// <summary>
        /// Updates the specified instance.
        /// </summary>
        /// <param name="instance">The instance.</param>
        /// <exception cref="System.NotImplementedException"></exception>
        public void Update(TEntity instance)
        {
            if (instance == null)
            {
                throw new ArgumentNullException("instance");
            }
            else
            {
                this._context.Entry(instance).State = EntityState.Modified;
                this.SaveChanges();
            }
        }

        public void Update(TEntity instance, params Expression<Func<TEntity, object>>[] properties)
        {
            this._context.Set<TEntity>().Attach(instance);
            DbEntityEntry<TEntity> tempInstance = this._context.Entry(instance);
            foreach (var property in properties)
            {
                tempInstance.Property(property).IsModified = true;
            }
            this.SaveChanges();

        }


        /// <summary>
        /// Deletes the specified instance.
        /// </summary>
        /// <param name="instance">The instance.</param>
        /// <exception cref="System.NotImplementedException"></exception>
        public void Delete(TEntity instance)
        {
            if (instance == null)
            {
                throw new ArgumentNullException("instance");
            }
            else
            {
                this._context.Entry(instance).State = EntityState.Deleted;
                this.SaveChanges();
            }
        }

        /// <summary>
        /// Gets the specified predicate.
        /// </summary>
        /// <param name="predicate">The predicate.</param>
        /// <returns></returns>
        public TEntity Get(Expression<Func<TEntity, bool>> predicate)
        {
            return this._context.Set<TEntity>().FirstOrDefault(predicate);
        }



        public IQueryable<TEntity> GetSome(Expression<Func<TEntity, bool>> predicate)
        {
            return (IQueryable<TEntity>)this._context.Set<TEntity>().Where(predicate);
        }

        public IQueryable<TEntity> GetSome(IQueryable<TEntity> source,Expression<Func<TEntity,bool>> predicate )
        {
            return (IQueryable<TEntity>)source.Where(predicate);
        }

        public TEntity Find(int idx)
        {
            return this._context.Set<TEntity>().Find(idx);
        }

        public IEnumerable<TEntity> SqlQuery()
        {
            object[] param = new object[plist.Count];
            for(int i=0; i<plist.Count; i++) param[i] = plist[i];
            return this._context.Database.SqlQuery<TEntity>(sql, param);
        }


        /// <summary>
        /// Gets all.
        /// </summary>
        /// <returns></returns>
        public IQueryable<TEntity> GetAll()
        {
            return (IQueryable<TEntity>)this._context.Set<TEntity>().AsQueryable();
        }

        public void SaveChanges()
        {
            this._context.SaveChanges();
        }

        public void Dispose()
        {
            this.Dispose(true);
            GC.SuppressFinalize(this);
        }

        protected virtual void Dispose(bool disposing)
        {
            if (disposing)
            {
                if (this._context != null)
                {
                    this._context.Dispose();
                    this._context = null;
                }
            }
        }
    }
}
