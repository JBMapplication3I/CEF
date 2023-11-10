// Decompiled with Telerik decompiler
// Type: Microsoft.AspNet.Identity.EntityFramework.EntityStore`1
// Assembly: Microsoft.AspNet.Identity.EntityFramework, Version=2.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35
// MVID: 67CFF174-6DA7-49B9-BAF6-D5DB9B7CBCAA
// Assembly location: C:\Users\jotha\.nuget\packages\microsoft.aspnet.identity.entityframework\2.2.3\lib\net45\Microsoft.AspNet.Identity.EntityFramework.dll

namespace Microsoft.AspNet.Identity.EntityFramework
{
    using System.Data.Entity;
    using System.Linq;
    using System.Threading.Tasks;

    /// <summary>EntityFramework based IIdentityEntityStore that allows query/manipulation of a TEntity set.</summary>
    /// <typeparam name="TEntity">Concrete entity type, i.e .User.</typeparam>
    internal class EntityStore<TEntity>
        where TEntity : class
    {
        /// <summary>Constructor that takes a Context.</summary>
        /// <param name="context">.</param>
        public EntityStore(DbContext context)
        {
            Context = context;
            DbEntitySet = /*(DbSet<TEntity>)*/context.Set<TEntity>();
        }

        /// <summary>Context for the store.</summary>
        /// <value>The context.</value>
        public DbContext Context { get; }

        /// <summary>EntitySet for this store.</summary>
        /// <value>The database entity set.</value>
        public DbSet<TEntity> DbEntitySet { get; }

        /// <summary>Used to query the entities.</summary>
        /// <value>The entity set.</value>
        public IQueryable<TEntity> EntitySet => /*(IQueryable<TEntity>)*/DbEntitySet;

        /// <summary>Insert an entity.</summary>
        /// <param name="entity">.</param>
        public void Create(TEntity entity)
        {
            DbEntitySet.Add(entity);
        }

        /// <summary>Mark an entity for deletion.</summary>
        /// <param name="entity">.</param>
        public void Delete(TEntity entity)
        {
            DbEntitySet.Remove(entity);
        }

        /// <summary>FindAsync an entity by ID.</summary>
        /// <param name="id">.</param>
        /// <returns>The by identifier asynchronous.</returns>
        public virtual Task<TEntity> GetByIdAsync(object id)
        {
            return DbEntitySet.FindAsync(id);
        }

        /// <summary>Update an entity.</summary>
        /// <param name="entity">.</param>
        public virtual void Update(TEntity entity)
        {
            if (entity != null)
            {
                Context.Entry(entity).State = (EntityState)16;
            }
        }
    }
}
