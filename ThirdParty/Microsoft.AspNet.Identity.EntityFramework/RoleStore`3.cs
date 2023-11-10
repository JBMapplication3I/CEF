// Decompiled with Telerik decompiler
// Type: Microsoft.AspNet.Identity.EntityFramework.RoleStore`3
// Assembly: Microsoft.AspNet.Identity.EntityFramework, Version=2.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35
// MVID: 67CFF174-6DA7-49B9-BAF6-D5DB9B7CBCAA
// Assembly location: C:\Users\jotha\.nuget\packages\microsoft.aspnet.identity.entityframework\2.2.3\lib\net45\Microsoft.AspNet.Identity.EntityFramework.dll

namespace Microsoft.AspNet.Identity.EntityFramework
{
    using System;
    using System.Data.Entity;
    using System.Linq;
    using System.Threading.Tasks;
    using TaskExtensions = System.Data.Entity.SqlServer.Utilities.TaskExtensions;

    /// <summary>EntityFramework based implementation.</summary>
    /// <typeparam name="TRole">    .</typeparam>
    /// <typeparam name="TKey">     .</typeparam>
    /// <typeparam name="TUserRole">.</typeparam>
    /// <seealso cref="IQueryableRoleStore{TRole,TKey}"/>
    /// <seealso cref="IRoleStore{TRole,TKey}"/>
    /// <seealso cref="IDisposable"/>
    /// <seealso cref="IQueryableRoleStore{TRole,TKey}"/>
    /// <seealso cref="IRoleStore{TRole,TKey}"/>
    public class RoleStore<TRole, TKey, TUserRole>
        : IQueryableRoleStore<TRole, TKey>, IRoleStore<TRole, TKey>, IDisposable
        where TRole : IdentityRole<TKey, TUserRole>, new()
        where TUserRole : IdentityUserRole<TKey>, new()
    {
        /// <summary>True if disposed.</summary>
        private bool _disposed;

        /// <summary>The role store.</summary>
        private EntityStore<TRole> _roleStore;

        /// <summary>Constructor which takes a db context and wires up the stores with default instances using the
        /// context.</summary>
        /// <param name="context">.</param>
        public RoleStore(DbContext context)
        {
            Context = context ?? throw new ArgumentNullException(nameof(context));
            _roleStore = new EntityStore<TRole>(context);
        }

        /// <summary>Context for the store.</summary>
        /// <value>The context.</value>
        public DbContext Context { get; private set; }

        /// <summary>If true will call dispose on the DbContext during Dipose.</summary>
        /// <value>True if dispose context, false if not.</value>
        public bool DisposeContext { get; set; }

        /// <summary>Returns an IQueryable of users.</summary>
        /// <value>The roles.</value>
        public IQueryable<TRole> Roles => _roleStore.EntitySet;

        /// <summary>Insert an entity.</summary>
        /// <param name="role">.</param>
        /// <returns>The new asynchronous.</returns>
        public virtual async Task CreateAsync(TRole role)
        {
            ThrowIfDisposed();
            if (role == null)
            {
                throw new ArgumentNullException(nameof(role));
            }
            _roleStore.Create(role);
            await TaskExtensions.WithCurrentCulture(Context.SaveChangesAsync());
        }

        /// <summary>Mark an entity for deletion.</summary>
        /// <param name="role">.</param>
        /// <returns>A Task.</returns>
        public virtual async Task DeleteAsync(TRole role)
        {
            ThrowIfDisposed();
            if (role == null)
            {
                throw new ArgumentNullException(nameof(role));
            }
            _roleStore.Delete(role);
            await TaskExtensions.WithCurrentCulture(Context.SaveChangesAsync());
        }

        /// <summary>Dispose the store.</summary>
        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        /// <summary>Find a role by id.</summary>
        /// <param name="roleId">.</param>
        /// <returns>The found identifier asynchronous.</returns>
        public Task<TRole> FindByIdAsync(TKey roleId)
        {
            ThrowIfDisposed();
            return _roleStore.GetByIdAsync(roleId);
        }

        /// <summary>Find a role by name.</summary>
        /// <param name="roleName">.</param>
        /// <returns>The found name asynchronous.</returns>
        public Task<TRole> FindByNameAsync(string roleName)
        {
            ThrowIfDisposed();
            return _roleStore.EntitySet.FirstOrDefaultAsync(u => u.Name.ToUpper() == roleName.ToUpper());
        }

        /// <summary>Update an entity.</summary>
        /// <param name="role">.</param>
        /// <returns>A Task.</returns>
        public virtual async Task UpdateAsync(TRole role)
        {
            ThrowIfDisposed();
            if (role == null)
            {
                throw new ArgumentNullException(nameof(role));
            }
            _roleStore.Update(role);
            await TaskExtensions.WithCurrentCulture(Context.SaveChangesAsync());
        }

        /// <summary>If disposing, calls dispose on the Context.  Always nulls out the Context.</summary>
        /// <param name="disposing">.</param>
        protected virtual void Dispose(bool disposing)
        {
            if (DisposeContext & disposing && Context != null)
            {
                Context.Dispose();
            }
            _disposed = true;
            Context = null;
            _roleStore = null;
        }

        /// <summary>Throw if disposed.</summary>
        private void ThrowIfDisposed()
        {
            if (_disposed)
            {
                throw new ObjectDisposedException(GetType().Name);
            }
        }
    }
}
