// Decompiled with JetBrains decompiler
// Type: Microsoft.AspNet.Identity.RoleManager`2
// Assembly: Microsoft.AspNet.Identity.Core, Version=2.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35
// MVID: 9E2B2C84-E0DA-4554-B416-8EA71B0D085D
// Assembly location: C:\Users\jotha\.nuget\packages\microsoft.aspnet.identity.core\2.2.3\lib\net45\Microsoft.AspNet.Identity.Core.dll

namespace Microsoft.AspNet.Identity
{
    using System;
    using System.Linq;
    using System.Threading.Tasks;

    /// <summary>Exposes role related api which will automatically save changes to the RoleStore.</summary>
    /// <typeparam name="TRole">.</typeparam>
    /// <typeparam name="TKey"> .</typeparam>
    /// <seealso cref="IDisposable"/>
    public class RoleManager<TRole, TKey> : IDisposable
        where TRole : class, IRole<TKey>
        where TKey : IEquatable<TKey>
    {
        /// <summary>True if disposed.</summary>
        private bool _disposed;

        /// <summary>The role validator.</summary>
        private IIdentityValidator<TRole> _roleValidator;

        /// <summary>Constructor.</summary>
        /// <param name="store">The IRoleStore is responsible for commiting changes via the UpdateAsync/CreateAsync
        ///                     methods.</param>
        public RoleManager(IRoleStore<TRole, TKey> store)
        {
            Store = store ?? throw new ArgumentNullException(nameof(store));
            RoleValidator = new RoleValidator<TRole, TKey>(this);
        }

        /// <summary>Returns an IQueryable of roles if the store is an IQueryableRoleStore.</summary>
        /// <value>The roles.</value>
        public virtual IQueryable<TRole> Roles
            => Store is IQueryableRoleStore<TRole, TKey> store
                ? store.Roles
                : throw new NotSupportedException(Resources.StoreNotIQueryableRoleStore);

        /// <summary>Used to validate roles before persisting changes.</summary>
        /// <value>The role validator.</value>
        public IIdentityValidator<TRole> RoleValidator
        {
            get => _roleValidator;
            set => _roleValidator = value ?? throw new ArgumentNullException(nameof(value));
        }

        /// <summary>Persistence abstraction that the Manager operates against.</summary>
        /// <value>The store.</value>
        protected IRoleStore<TRole, TKey> Store { get; }

        /// <summary>Create a role.</summary>
        /// <param name="role">.</param>
        /// <returns>The new asynchronous.</returns>
        public virtual async Task<IdentityResult> CreateAsync(TRole role)
        {
            ThrowIfDisposed();
            if (role == null)
            {
                throw new ArgumentNullException(nameof(role));
            }
            var identityResult = await RoleValidator.ValidateAsync(role).WithCurrentCulture();
            if (!identityResult.Succeeded)
            {
                return identityResult;
            }
            await Store.CreateAsync(role).WithCurrentCulture();
            return IdentityResult.Success;
        }

        /// <summary>Delete a role.</summary>
        /// <param name="role">.</param>
        /// <returns>A Task{IdentityResult}</returns>
        public virtual async Task<IdentityResult> DeleteAsync(TRole role)
        {
            ThrowIfDisposed();
            if (role == null)
            {
                throw new ArgumentNullException(nameof(role));
            }
            await Store.DeleteAsync(role).WithCurrentCulture();
            return IdentityResult.Success;
        }

        /// <summary>Dispose this object.</summary>
        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        /// <summary>Find a role by id.</summary>
        /// <param name="roleId">.</param>
        /// <returns>The found identifier asynchronous.</returns>
        public virtual async Task<TRole> FindByIdAsync(TKey roleId)
        {
            ThrowIfDisposed();
            return await Store.FindByIdAsync(roleId).WithCurrentCulture();
        }

        /// <summary>Find a role by name.</summary>
        /// <param name="roleName">.</param>
        /// <returns>The found name asynchronous.</returns>
        public virtual async Task<TRole> FindByNameAsync(string roleName)
        {
            ThrowIfDisposed();
            if (roleName == null)
            {
                throw new ArgumentNullException(nameof(roleName));
            }
            return await Store.FindByNameAsync(roleName).WithCurrentCulture();
        }

        /// <summary>Returns true if the role exists.</summary>
        /// <param name="roleName">.</param>
        /// <returns>A Task{bool}</returns>
        public virtual async Task<bool> RoleExistsAsync(string roleName)
        {
            ThrowIfDisposed();
            if (roleName == null)
            {
                throw new ArgumentNullException(nameof(roleName));
            }
            return await FindByNameAsync(roleName).WithCurrentCulture() != null;
        }

        /// <summary>Update an existing role.</summary>
        /// <param name="role">.</param>
        /// <returns>A Task{IdentityResult}</returns>
        public virtual async Task<IdentityResult> UpdateAsync(TRole role)
        {
            ThrowIfDisposed();
            if (role == null)
            {
                throw new ArgumentNullException(nameof(role));
            }
            var identityResult = await RoleValidator.ValidateAsync(role).WithCurrentCulture();
            if (!identityResult.Succeeded)
            {
                return identityResult;
            }
            await Store.UpdateAsync(role).WithCurrentCulture();
            return IdentityResult.Success;
        }

        /// <summary>When disposing, actually dipose the store.</summary>
        /// <param name="disposing">.</param>
        protected virtual void Dispose(bool disposing)
        {
            if (disposing && !_disposed)
            {
                Store.Dispose();
            }
            _disposed = true;
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
