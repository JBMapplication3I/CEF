// <copyright file="EmailQueue.extended.cs" company="clarity-ventures.com">
// Copyright (c) 2018-2023 clarity-ventures.com. All rights reserved.
// </copyright>
// <summary>Implements the email queue search extensions class</summary>
namespace Clarity.Ecommerce.Interfaces.Models
{
    using System.Linq;
    using DataModel;
    using Utilities;

    /// <summary>An email queue search extensions.</summary>
    public static class EmailQueueSearchExtensions
    {
        /// <summary>An IQueryable{TEntity} extension method that filter email queues by addresses to and Cc and Bcc.</summary>
        /// <typeparam name="TEntity">Type of the entity.</typeparam>
        /// <param name="query">The query to act on.</param>
        /// <param name="email">The email.</param>
        /// <returns>An <see cref="IQueryable{TEntity}"/>.</returns>
        public static IQueryable<TEntity> FilterEmailQueuesByAddressesToAndCCAndBCC<TEntity>(this IQueryable<TEntity> query, string? email)
            where TEntity : class, IEmailQueue
        {
            Contract.RequiresNotNull(query);
            if (!Contract.CheckValidKey(email))
            {
                return query;
            }
            var search = email!.Trim().ToLower();
            return query.Where(x => x.AddressesTo != null && x.AddressesTo.ToLower().Contains(search)
                || x.AddressesCc != null && x.AddressesCc.ToLower().Contains(search)
                || x.AddressesBcc != null && x.AddressesBcc.ToLower().Contains(search));
        }
    }
}
