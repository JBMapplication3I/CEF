// Decompiled with JetBrains decompiler
// Type: Microsoft.AspNet.Identity.RoleValidator`2
// Assembly: Microsoft.AspNet.Identity.Core, Version=2.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35
// MVID: 9E2B2C84-E0DA-4554-B416-8EA71B0D085D
// Assembly location: C:\Users\jotha\.nuget\packages\microsoft.aspnet.identity.core\2.2.3\lib\net45\Microsoft.AspNet.Identity.Core.dll

namespace Microsoft.AspNet.Identity
{
    using System;
    using System.Collections.Generic;
    using System.Globalization;
    using System.Threading.Tasks;

    /// <summary>Validates roles before they are saved.</summary>
    /// <typeparam name="TRole">.</typeparam>
    /// <typeparam name="TKey"> .</typeparam>
    /// <seealso cref="IIdentityValidator{TRole}"/>
    /// <seealso cref="IIdentityValidator{TRole}"/>
    public class RoleValidator<TRole, TKey> : IIdentityValidator<TRole>
        where TRole : class, IRole<TKey>
        where TKey : IEquatable<TKey>
    {
        /// <summary>Constructor.</summary>
        /// <param name="manager">.</param>
        public RoleValidator(RoleManager<TRole, TKey> manager)
        {
            Manager = manager ?? throw new ArgumentNullException(nameof(manager));
        }

        /// <summary>Gets the manager.</summary>
        /// <value>The manager.</value>
        private RoleManager<TRole, TKey> Manager { get; }

        /// <summary>Validates a role before saving.</summary>
        /// <param name="item">.</param>
        /// <returns>A Task{IdentityResult}</returns>
        public virtual async Task<IdentityResult> ValidateAsync(TRole item)
        {
            if (item == null)
            {
                throw new ArgumentNullException(nameof(item));
            }
            var errors = new List<string>();
            await ValidateRoleName(item, errors).WithCurrentCulture();
            return errors.Count <= 0 ? IdentityResult.Success : IdentityResult.Failed(errors.ToArray());
        }

        /// <summary>Validates the role name.</summary>
        /// <param name="role">  The role.</param>
        /// <param name="errors">The errors.</param>
        /// <returns>A Task.</returns>
#pragma warning disable IDE1006 // Naming Styles
        private async Task ValidateRoleName(TRole role, List<string> errors)
#pragma warning restore IDE1006 // Naming Styles
        {
            if (string.IsNullOrWhiteSpace(role.Name))
            {
                errors.Add(string.Format(CultureInfo.CurrentCulture, Resources.PropertyTooShort, "Name"));
            }
            else
            {
                var role1 = await Manager.FindByNameAsync(role.Name).WithCurrentCulture();
                if (role1 == null || EqualityComparer<TKey>.Default.Equals(role1.Id, role.Id))
                {
                    return;
                }
                errors.Add(string.Format(CultureInfo.CurrentCulture, Resources.DuplicateName, role.Name));
            }
        }
    }
}
