// <copyright file="AccountContact.extended.cs" company="clarity-ventures.com">
// Copyright (c) 2019-2023 clarity-ventures.com. All rights reserved.
// </copyright>
// <summary>Implements the account contact search extensions class</summary>
namespace Clarity.Ecommerce.Interfaces.Models
{
    using System;
    using System.Linq;
    using DataModel;
    using Utilities;

    /// <summary>An account contact search extensions.</summary>
    public static class AccountContactSearchExtensions
    {
        /// <summary>An IQueryable{TEntity} extension method that filter account contacts by active.</summary>
        /// <typeparam name="TEntity">Type of the entity.</typeparam>
        /// <param name="query">The query to act on.</param>
        /// <returns>An <see cref="IQueryable{TEntity}"/>.</returns>
        public static IQueryable<TEntity> FilterAccountContactsByActive<TEntity>(this IQueryable<TEntity> query)
            where TEntity : class, IAccountContact
        {
            return Contract.RequiresNotNull(query)
                .Where(x => x.Active
                         && x.Master != null!
                         && x.Master.Active
                         && x.Slave != null!
                         && x.Slave.Active
                         && x.Slave.Address != null!
                         && x.Slave.Address.Active);
        }

        /// <summary>An IQueryable{TEntity} extension method that filter account contacts by account identifier.</summary>
        /// <typeparam name="TEntity">Type of the entity.</typeparam>
        /// <param name="query">The query to act on.</param>
        /// <param name="id">   The identifier.</param>
        /// <returns>An <see cref="IQueryable{TEntity}"/>.</returns>
        public static IQueryable<TEntity> FilterAccountContactsByAccountID<TEntity>(this IQueryable<TEntity> query, int? id)
            where TEntity : class, IAccountContact
        {
            if (Contract.CheckInvalidID(id))
            {
                return query;
            }
            return Contract.RequiresNotNull(query)
                .Where(x => x.MasterID == id!.Value);
        }

        /// <summary>An IQueryable{TEntity} extension method that filter account contacts by end date.</summary>
        /// <typeparam name="TEntity">Type of the entity.</typeparam>
        /// <param name="query">The query to act on.</param>
        /// <param name="date">To override filtering by current date.</param>
        /// <returns>An <see cref="IQueryable{TEntity}"/>.</returns>
        public static IQueryable<TEntity> FilterAccountContactsByEndDate<TEntity>(this IQueryable<TEntity> query, DateTime? date = null)
            where TEntity : class, IAccountContact
        {
            if (Contract.CheckNull(date))
            {
                date = DateExtensions.GenDateTime;
            }
            return Contract.RequiresNotNull(query)
                .Where(x => x.EndDate > date || x.EndDate == null);
        }

        /// <summary>An IQueryable{TEntity} extension method that filter account contacts by contact identifier.</summary>
        /// <typeparam name="TEntity">Type of the entity.</typeparam>
        /// <param name="query">The query to act on.</param>
        /// <param name="id">   The identifier.</param>
        /// <returns>An <see cref="IQueryable{TEntity}"/>.</returns>
        public static IQueryable<TEntity> FilterAccountContactsByContactID<TEntity>(this IQueryable<TEntity> query, int? id)
            where TEntity : class, IAccountContact
        {
            if (Contract.CheckInvalidID(id))
            {
                return query;
            }
            return Contract.RequiresNotNull(query)
                .Where(x => x.MasterID == id!.Value);
        }

        /// <summary>An IQueryable{TEntity} extension method that filter account contacts by address identifier.</summary>
        /// <typeparam name="TEntity">Type of the entity.</typeparam>
        /// <param name="query">The query to act on.</param>
        /// <param name="id">   The identifier.</param>
        /// <returns>An <see cref="IQueryable{TEntity}"/>.</returns>
        public static IQueryable<TEntity> FilterAccountContactsByAddressID<TEntity>(this IQueryable<TEntity> query, int? id)
            where TEntity : class, IAccountContact
        {
            if (Contract.CheckInvalidID(id))
            {
                return query;
            }
            return Contract.RequiresNotNull(query)
                .Where(x => x.Slave != null && x.Slave.AddressID == id!.Value);
        }
    }
}
