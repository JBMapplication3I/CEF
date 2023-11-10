// <copyright file="StoreContact.extended.cs" company="clarity-ventures.com">
// Copyright (c) 2018-2023 clarity-ventures.com. All rights reserved.
// </copyright>
// <summary>Implements the store contact search extensions class</summary>
namespace Clarity.Ecommerce.Interfaces.Models
{
    using System.Linq;
    using DataModel;
    using Utilities;

    /// <summary>A store contact search extensions.</summary>
    public static class StoreContactSearchExtensions
    {
        /// <summary>An IQueryable{TEntity} extension method that filter store contacts by active.</summary>
        /// <typeparam name="TEntity">Type of the entity.</typeparam>
        /// <param name="query">The query to act on.</param>
        /// <returns>An <see cref="IQueryable{TEntity}"/>.</returns>
        public static IQueryable<TEntity> FilterStoreContactsByActive<TEntity>(
                this IQueryable<TEntity> query)
            where TEntity : class, IStoreContact
        {
            return Contract.RequiresNotNull(query)
                .Where(x => x.Active
                         && x.Master != null
                         && x.Master.Active
                         && x.Contact != null
                         && x.Contact.Address != null
                         && x.Contact.Address.Active);
        }

        /// <summary>An IQueryable{TEntity} extension method that filter store contacts by contact identifier.</summary>
        /// <typeparam name="TEntity">Type of the entity.</typeparam>
        /// <param name="query">The query to act on.</param>
        /// <param name="id">   The identifier.</param>
        /// <returns>An <see cref="IQueryable{TEntity}"/>.</returns>
        public static IQueryable<TEntity> FilterStoreContactsByContactID<TEntity>(
                this IQueryable<TEntity> query,
                int? id)
            where TEntity : class, IStoreContact
        {
            if (Contract.CheckInvalidID(id))
            {
                return query;
            }
            return Contract.RequiresNotNull(query)
                .Where(x => x.ContactID == id!.Value);
        }

        /// <summary>An IQueryable{TEntity} extension method that filter store contacts by address identifier.</summary>
        /// <typeparam name="TEntity">Type of the entity.</typeparam>
        /// <param name="query">The query to act on.</param>
        /// <param name="id">   The identifier.</param>
        /// <returns>An <see cref="IQueryable{TEntity}"/>.</returns>
        public static IQueryable<TEntity> FilterStoreContactsByAddressID<TEntity>(
                this IQueryable<TEntity> query,
                int? id)
            where TEntity : class, IStoreContact
        {
            if (Contract.CheckInvalidID(id))
            {
                return query;
            }
            return Contract.RequiresNotNull(query)
                .Where(x => x.Contact != null
                         && x.Contact.AddressID == id!.Value);
        }
    }
}
