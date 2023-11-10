// <copyright file="User.extended.cs" company="clarity-ventures.com">
// Copyright (c) 2017-2023 clarity-ventures.com. All rights reserved.
// </copyright>
// <summary>Implements the user search extensions class</summary>
namespace Clarity.Ecommerce.Interfaces.Models
{
    using System.Linq;
    using DataModel;
    using Utilities;

    /// <summary>A user search extensions.</summary>
    public static class UserSearchExtensions
    {
        /// <summary>An IQueryable{TEntity} extension method that filter users by user online status identifier.</summary>
        /// <typeparam name="TEntity">Type of the entity.</typeparam>
        /// <param name="query">The query to act on.</param>
        /// <param name="id">   The identifier.</param>
        /// <returns>An <see cref="IQueryable{TEntity}"/>.</returns>
        public static IQueryable<TEntity> FilterUsersByUserOnlineStatusID<TEntity>(
                this IQueryable<TEntity> query,
                int? id)
            where TEntity : class, IUser
        {
            if (Contract.CheckInvalidID(id))
            {
                return query;
            }
            return Contract.RequiresNotNull(query)
                .Where(p => p.UserOnlineStatusID == id!.Value);
        }

        /// <summary>An IQueryable{TEntity} extension method that filter users by user name.</summary>
        /// <typeparam name="TEntity">Type of the entity.</typeparam>
        /// <param name="query"> The query to act on.</param>
        /// <param name="source">Source for the.</param>
        /// <param name="strict">True to strict.</param>
        /// <returns>An <see cref="IQueryable{TEntity}"/>.</returns>
        public static IQueryable<TEntity> FilterUsersByUserName<TEntity>(
                this IQueryable<TEntity> query,
                string? source,
                bool strict)
            where TEntity : class, IUser
        {
            if (!Contract.CheckValidKey(source))
            {
                return query;
            }
            if (strict)
            {
                return Contract.RequiresNotNull(query)
                    .Where(x => x.UserName == source);
            }
            var search = source!.Trim().ToLower();
            return Contract.RequiresNotNull(query)
                .Where(x => x.UserName != null && x.UserName.Contains(search));
        }

        /// <summary>An IQueryable{TEntity} extension method that filter users by user name or custom key.</summary>
        /// <typeparam name="TEntity">Type of the entity.</typeparam>
        /// <param name="query"> The query to act on.</param>
        /// <param name="source">Source for the.</param>
        /// <param name="strict">True to strict.</param>
        /// <returns>An <see cref="IQueryable{TEntity}"/>.</returns>
        public static IQueryable<TEntity> FilterUsersByUserNameOrCustomKey<TEntity>(
                this IQueryable<TEntity> query,
                string? source,
                bool strict)
            where TEntity : class, IUser
        {
            if (!Contract.CheckValidKey(source))
            {
                return query;
            }
            var search = source!.Trim().ToLower();
            if (strict)
            {
                return Contract.RequiresNotNull(query)
                    .Where(x => x.CustomKey != null && x.CustomKey == search
                             || x.UserName != null && x.UserName == search);
            }
            return Contract.RequiresNotNull(query)
                .Where(x => x.CustomKey != null && x.CustomKey.Contains(search)
                         || x.UserName != null && x.UserName.Contains(search));
        }

        /// <summary>An IQueryable{TEntity} extension method that filter users by user name or contact name.</summary>
        /// <typeparam name="TEntity">Type of the entity.</typeparam>
        /// <param name="query"> The query to act on.</param>
        /// <param name="source">Source for the.</param>
        /// <param name="strict">True to strict.</param>
        /// <returns>An <see cref="IQueryable{TEntity}"/>.</returns>
        public static IQueryable<TEntity> FilterUsersByUserNameOrContactName<TEntity>(
                this IQueryable<TEntity> query,
                string? source,
                bool strict)
            where TEntity : class, IUser
        {
            if (!Contract.CheckValidKey(source))
            {
                return query;
            }
            var search = source!.Trim().ToLower();
            if (strict)
            {
                return Contract.RequiresNotNull(query)
                    .Where(x => x.UserName != null && x.UserName == search
                             || x.Contact != null && (x.Contact.FirstName == search || x.Contact.LastName == search));
            }
            return Contract.RequiresNotNull(query)
                .Where(x => x.UserName != null && x.UserName.Contains(search)
                         || x.Contact != null && (x.Contact.FirstName!.Contains(search) || x.Contact.LastName!.Contains(search)));
        }

        /// <summary>An IQueryable{TEntity} extension method that filter users by user name or custom key or email or
        /// contact name.</summary>
        /// <typeparam name="TEntity">Type of the entity.</typeparam>
        /// <param name="query"> The query to act on.</param>
        /// <param name="source">Source for the.</param>
        /// <param name="strict">True to strict.</param>
        /// <returns>An <see cref="IQueryable{TEntity}"/>.</returns>
        public static IQueryable<TEntity> FilterUsersByUserNameOrCustomKeyOrEmailOrContactName<TEntity>(
                this IQueryable<TEntity> query,
                string? source,
                bool strict)
            where TEntity : class, IUser
        {
            if (!Contract.CheckValidKey(source))
            {
                return query;
            }
            var search = source!.Trim().ToLower();
            if (strict)
            {
                return Contract.RequiresNotNull(query)
                    .Where(x => x.UserName != null && x.UserName == search
                             || x.CustomKey != null && x.CustomKey == search
                             || x.Email != null && x.Email == search
                             || x.Contact != null && (x.Contact.FirstName == search || x.Contact.LastName == search));
            }
            return Contract.RequiresNotNull(query)
                .Where(x => x.UserName != null && x.UserName.Contains(search)
                         || x.CustomKey != null && x.CustomKey.Contains(search)
                         || x.Email != null && x.Email.Contains(search)
                         || x.Contact != null && (x.Contact.FirstName!.Contains(search) || x.Contact.LastName!.Contains(search)));
        }

        /// <summary>An IQueryable{TEntity} extension method that filter users by user id or user name or custom key or
        /// email or contact name.</summary>
        /// <typeparam name="TEntity">Type of the entity.</typeparam>
        /// <param name="query"> The query to act on.</param>
        /// <param name="source">Source for the.</param>
        /// <param name="strict">True to strict.</param>
        /// <returns>An <see cref="IQueryable{TEntity}"/>.</returns>
        public static IQueryable<TEntity> FilterUsersByIDOrUserNameOrCustomKeyOrEmailOrContactName<TEntity>(
            this IQueryable<TEntity> query,
            string? source,
            bool strict)
            where TEntity : class, IUser
        {
            if (!Contract.CheckValidKey(source))
            {
                return query;
            }
            var search = source!.Trim().ToLower();
            if (strict)
            {
                return Contract.RequiresNotNull(query)
                    .Where(x => x.ID.ToString() == search
                        || x.UserName != null && x.UserName == search
                        || x.CustomKey != null && x.CustomKey == search
                        || x.Email != null && x.Email == search
                        || x.Contact != null && (x.Contact.FirstName == search || x.Contact.LastName == search));
            }
            return Contract.RequiresNotNull(query)
                .Where(x => x.ID.ToString().Contains(search)
                    || x.UserName != null && x.UserName.Contains(search)
                    || x.CustomKey != null && x.CustomKey.Contains(search)
                    || x.Email != null && x.Email.Contains(search)
                    || x.Contact != null && (x.Contact.FirstName!.Contains(search) || x.Contact.LastName!.Contains(search)));
        }

        /// <summary>An IQueryable{TEntity} extension method that filter users by user name or custom key or email.</summary>
        /// <typeparam name="TEntity">Type of the entity.</typeparam>
        /// <param name="query"> The query to act on.</param>
        /// <param name="source">Source for the.</param>
        /// <param name="strict">True to strict.</param>
        /// <returns>An <see cref="IQueryable{TEntity}"/>.</returns>
        public static IQueryable<TEntity> FilterUsersByUserNameOrCustomKeyOrEmail<TEntity>(
                this IQueryable<TEntity> query,
                string? source,
                bool strict)
            where TEntity : class, IUser
        {
            if (!Contract.CheckValidKey(source))
            {
                return query;
            }
            var search = source!.Trim().ToLower();
            if (strict)
            {
                return Contract.RequiresNotNull(query)
                    .Where(x => x.CustomKey != null && x.CustomKey == search
                             || x.UserName != null && x.UserName == search
                             || x.Email != null && x.Email == search);
            }
            return Contract.RequiresNotNull(query)
                .Where(x => x.CustomKey != null && x.CustomKey.Contains(search)
                         || x.UserName != null && x.UserName.Contains(search)
                         || x.Email != null && x.Email.Contains(search));
        }

        /// <summary>An IQueryable{TEntity} extension method that filter users by email.</summary>
        /// <typeparam name="TEntity">Type of the entity.</typeparam>
        /// <param name="query"> The query to act on.</param>
        /// <param name="source">Source for the.</param>
        /// <param name="strict">True to strict.</param>
        /// <returns>An <see cref="IQueryable{TEntity}"/>.</returns>
        public static IQueryable<TEntity> FilterUsersByEmail<TEntity>(
                this IQueryable<TEntity> query,
                string? source,
                bool strict)
            where TEntity : class, IUser
        {
            if (!Contract.CheckValidKey(source))
            {
                return query;
            }
            if (strict)
            {
                return Contract.RequiresNotNull(query)
                    .Where(x => x.Email == source);
            }
            var search = source!.Trim().ToLower();
            return Contract.RequiresNotNull(query)
                .Where(x => x.Email != null && x.Email.Contains(search));
        }

        /// <summary>An IQueryable{TEntity} extension method that filter users by user name or email.</summary>
        /// <typeparam name="TEntity">Type of the entity.</typeparam>
        /// <param name="query"> The query to act on.</param>
        /// <param name="source">Source for the.</param>
        /// <param name="strict">True to strict.</param>
        /// <returns>An <see cref="IQueryable{TEntity}"/>.</returns>
        public static IQueryable<TEntity> FilterUsersByUserNameOrEmail<TEntity>(
                this IQueryable<TEntity> query,
                string? source,
                bool strict)
            where TEntity : class, IUser
        {
            if (!Contract.CheckValidKey(source))
            {
                return query;
            }
            if (strict)
            {
                return Contract.RequiresNotNull(query)
                    .Where(x => x.Email == source || x.UserName == source);
            }
            var search = source!.Trim().ToLower();
            return Contract.RequiresNotNull(query)
                .Where(x => x.Email != null && x.Email.Contains(search)
                    || x.UserName != null && x.UserName.Contains(search));
        }

        /// <summary>An IQueryable{TEntity} extension method that filter users by account identifier.</summary>
        /// <typeparam name="TEntity">Type of the entity.</typeparam>
        /// <param name="query">The query to act on.</param>
        /// <param name="id">   The identifier.</param>
        /// <returns>An <see cref="IQueryable{TEntity}"/>.</returns>
        public static IQueryable<TEntity> FilterUsersByAccountID<TEntity>(
                this IQueryable<TEntity> query,
                int? id)
            where TEntity : class, IUser
        {
            if (Contract.CheckInvalidID(id))
            {
                return query;
            }
            return Contract.RequiresNotNull(query)
                .Where(x => x.AccountID == id!.Value);
        }

        /// <summary>An IQueryable{TEntity} extension method that filter users by account name.</summary>
        /// <typeparam name="TEntity">Type of the entity.</typeparam>
        /// <param name="query"> The query to act on.</param>
        /// <param name="source">Source for the.</param>
        /// <param name="strict">True to strict.</param>
        /// <returns>An <see cref="IQueryable{TEntity}"/>.</returns>
        public static IQueryable<TEntity> FilterUsersByAccountName<TEntity>(
                this IQueryable<TEntity> query,
                string? source,
                bool strict)
            where TEntity : class, IUser
        {
            if (!Contract.CheckValidKey(source))
            {
                return query;
            }
            if (strict)
            {
                return Contract.RequiresNotNull(query)
                    .Where(x => x.Account != null
                             && x.Account.Name != null
                             && x.Account.Name == source);
            }
            var search = source!.Trim().ToLower();
            return Contract.RequiresNotNull(query)
                .Where(x => x.Account != null
                         && x.Account.Name != null
                         && x.Account.Name.Contains(search));
        }

        /// <summary>An IQueryable{TEntity} extension method that filter users by account key.</summary>
        /// <typeparam name="TEntity">Type of the entity.</typeparam>
        /// <param name="query"> The query to act on.</param>
        /// <param name="source">Source for the.</param>
        /// <param name="strict">True to strict.</param>
        /// <returns>An <see cref="IQueryable{TEntity}"/>.</returns>
        public static IQueryable<TEntity> FilterUsersByAccountKey<TEntity>(
                this IQueryable<TEntity> query,
                string? source,
                bool strict)
            where TEntity : class, IUser
        {
            if (!Contract.CheckValidKey(source))
            {
                return query;
            }
            if (strict)
            {
                return Contract.RequiresNotNull(query)
                    .Where(x => x.Account != null
                             && x.Account.Name != null
                             && x.Account.Name == source);
            }
            var search = source!.Trim().ToLower();
            return Contract.RequiresNotNull(query)
                .Where(x => x.Account != null
                         && x.Account.Name != null
                         && x.Account.Name.Contains(search));
        }

        /// <summary>An IQueryable{TEntity} extension method that filter users by accessible from account identifier.</summary>
        /// <typeparam name="TEntity">Type of the entity.</typeparam>
        /// <param name="query">The query to act on.</param>
        /// <param name="id">   The identifier.</param>
        /// <returns>An <see cref="IQueryable{TEntity}"/>.</returns>
        public static IQueryable<TEntity> FilterUsersByAccessibleFromAccountID<TEntity>(
                this IQueryable<TEntity> query,
                int? id)
            where TEntity : class, IUser
        {
            if (Contract.CheckInvalidID(id))
            {
                return query;
            }
            return Contract.RequiresNotNull(query)
                .Where(x => x.AccountID == id!.Value
                    // Look up one level to see if the id was an "owning/affiliate" account
                    || x.Account != null
                    && x.Account.Active
                    && // The account is associated to (not from) for single direction checks
                        x.Account.AccountsAssociatedWith!
                            .Any(y => y.Active
                                && (y.MasterID == id.Value
                                    || y.Master != null
                                    && y.Master.Active
                                    && y.Master.AccountsAssociatedWith!
                                        .Any(z => z.Active
                                                && (z.MasterID == id.Value
                                                    || z.Master != null
                                                    && z.Master.Active
                                                    && z.Master.AccountsAssociatedWith!
                                                        .Any(a => a.Active && a.MasterID == id.Value))))));
        }

        /// <summary>An IQueryable{TEntity} extension method that filter users by contact name.</summary>
        /// <typeparam name="TEntity">Type of the entity.</typeparam>
        /// <param name="query">The query to act on.</param>
        /// <param name="name"> The name.</param>
        /// <returns>An <see cref="IQueryable{TEntity}"/>.</returns>
        public static IQueryable<TEntity> FilterUsersByContactName<TEntity>(
                this IQueryable<TEntity> query,
                string? name)
            where TEntity : class, IUser
        {
            if (!Contract.CheckValidKey(name))
            {
                return query;
            }
            var search = name!.Trim().ToLower();
            return Contract.RequiresNotNull(query)
                .Where(x => x.Contact!.FirstName != null && x.Contact.FirstName.Contains(search)
                    || x.Contact.LastName != null && x.Contact.LastName.Contains(search));
        }

        /// <summary>An IQueryable{TEntity} extension method that filter users by email or contact email.</summary>
        /// <typeparam name="TEntity">Type of the entity.</typeparam>
        /// <param name="query">The query to act on.</param>
        /// <param name="email">The email.</param>
        /// <returns>An <see cref="IQueryable{TEntity}"/>.</returns>
        public static IQueryable<TEntity> FilterUsersByEmailOrContactEmail<TEntity>(
                this IQueryable<TEntity> query,
                string? email)
            where TEntity : class, IUser
        {
            if (!Contract.CheckValidKey(email))
            {
                return query;
            }
            var search = email!.Trim().ToLower();
            return Contract.RequiresNotNull(query)
                .Where(x => x.Email != null && x.Email.Contains(search)
                    || x.Contact != null && x.Contact.Email1 != null && x.Contact.Email1.Contains(search));
        }

        /// <summary>An IQueryable{TEntity} extension method that filter users by contact email.</summary>
        /// <typeparam name="TEntity">Type of the entity.</typeparam>
        /// <param name="query">The query to act on.</param>
        /// <param name="email">The email.</param>
        /// <returns>An <see cref="IQueryable{TEntity}"/>.</returns>
        public static IQueryable<TEntity> FilterUsersByContactEmail<TEntity>(
                this IQueryable<TEntity> query,
                string? email)
            where TEntity : class, IUser
        {
            if (!Contract.CheckValidKey(email))
            {
                return query;
            }
            var search = email!.Trim().ToLower();
            return Contract.RequiresNotNull(query)
                .Where(x => x.Contact!.Email1 != null
                    && x.Contact.Email1.Contains(search));
        }
    }
}
