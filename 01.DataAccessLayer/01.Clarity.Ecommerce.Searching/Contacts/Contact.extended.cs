// <copyright file="Contact.extended.cs" company="clarity-ventures.com">
// Copyright (c) 2018-2023 clarity-ventures.com. All rights reserved.
// </copyright>
// <summary>Implements the contact search extensions class</summary>
namespace Clarity.Ecommerce.Interfaces.Models
{
    using System.Linq;
    using DataModel;
    using Utilities;

    /// <summary>A contact search extensions.</summary>
    public static class ContactSearchExtensions
    {
        /// <summary>Filter contacts by first or the last name.</summary>
        /// <typeparam name="TEntity">Type of the entity.</typeparam>
        /// <param name="query">The query to act on.</param>
        /// <param name="name"> The name.</param>
        /// <returns>An <see cref="IQueryable{TEntity}"/>.</returns>
        public static IQueryable<TEntity> FilterContactsByFirstOrLastName<TEntity>(
                this IQueryable<TEntity> query,
                string? name)
            where TEntity : class, IHaveAContactBase
        {
            if (!Contract.CheckValidKey(name))
            {
                return query;
            }
            var search = name!.Trim().ToLower();
            return Contract.RequiresNotNull(query)
                .Where(x => x.Contact != null
                    && (x.Contact.FirstName != null && x.Contact.FirstName.Contains(search)
                        || x.Contact.LastName != null && x.Contact.LastName.Contains(search)));
        }

        /// <summary>Filter contacts by the first name.</summary>
        /// <typeparam name="TEntity">Type of the entity.</typeparam>
        /// <param name="query">The query to act on.</param>
        /// <param name="name"> The name.</param>
        /// <returns>An <see cref="IQueryable{TEntity}"/>.</returns>
        public static IQueryable<TEntity> FilterContactsByFirstName<TEntity>(
                this IQueryable<TEntity> query,
                string? name)
            where TEntity : class, IHaveAContactBase
        {
            if (!Contract.CheckValidKey(name))
            {
                return query;
            }
            var search = name!.Trim().ToLower();
            return Contract.RequiresNotNull(query)
                .Where(x => x.Contact != null
                    && x.Contact.FirstName != null
                    && x.Contact.FirstName.Contains(search));
        }

        /// <summary>Filter contacts by the last name.</summary>
        /// <typeparam name="TEntity">Type of the entity.</typeparam>
        /// <param name="query">The query to act on.</param>
        /// <param name="name"> The name.</param>
        /// <returns>An <see cref="IQueryable{TEntity}"/>.</returns>
        public static IQueryable<TEntity> FilterContactsByLastName<TEntity>(
                this IQueryable<TEntity> query,
                string? name)
            where TEntity : class, IHaveAContactBase
        {
            if (!Contract.CheckValidKey(name))
            {
                return query;
            }
            var search = name!.Trim().ToLower();
            return Contract.RequiresNotNull(query)
                .Where(x => x.Contact != null
                    && x.Contact.LastName != null
                    && x.Contact.LastName.Contains(search));
        }

        /// <summary>Filter nullable contacts by the first name.</summary>
        /// <typeparam name="TEntity">Type of the entity.</typeparam>
        /// <param name="query">The query to act on.</param>
        /// <param name="name"> The name.</param>
        /// <returns>An <see cref="IQueryable{TEntity}"/>.</returns>
        public static IQueryable<TEntity> FilterNullableContactsByFirstName<TEntity>(
                this IQueryable<TEntity> query,
                string? name)
            where TEntity : class, IHaveANullableContactBase
        {
            if (!Contract.CheckValidKey(name))
            {
                return query;
            }
            var search = name!.Trim().ToLower();
            return Contract.RequiresNotNull(query)
                .Where(x => x.Contact != null
                    && x.Contact.FirstName != null
                    && x.Contact.FirstName.Contains(search));
        }

        /// <summary>Filter nullable contacts by the last name.</summary>
        /// <typeparam name="TEntity">Type of the entity.</typeparam>
        /// <param name="query">The query to act on.</param>
        /// <param name="name"> The name.</param>
        /// <returns>An <see cref="IQueryable{TEntity}"/>.</returns>
        public static IQueryable<TEntity> FilterNullableContactsByLastName<TEntity>(
                this IQueryable<TEntity> query,
                string? name)
            where TEntity : class, IHaveANullableContactBase
        {
            if (!Contract.CheckValidKey(name))
            {
                return query;
            }
            var search = name!.Trim().ToLower();
            return Contract.RequiresNotNull(query)
                .Where(x => x.Contact != null
                    && x.Contact.LastName != null
                    && x.Contact.LastName.Contains(search));
        }

        /// <summary>An IContactModel extension method that convert contact to comparable hashed value.</summary>
        /// <param name="model">The model to act on.</param>
        /// <returns>The contact converted to comparable hashed value.</returns>
        public static long? ConvertContactToComparableHashedValue(this IContactModel model)
        {
            if (model == null)
            {
                return null;
            }
            var clone = (IContactModel)model.DeepCopy();
            clone.SameAsBilling = false;
            return Digest.Crc64(clone.ToHashableString());
        }
    }
}
