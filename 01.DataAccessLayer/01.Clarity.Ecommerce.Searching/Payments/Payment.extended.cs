// <copyright file="Payment.extended.cs" company="clarity-ventures.com">
// Copyright (c) 2017-2023 clarity-ventures.com. All rights reserved.
// </copyright>
// <summary>Implements the payment search extensions class</summary>
namespace Clarity.Ecommerce.Interfaces.Models
{
    using System.Linq;
    using DataModel;
    using Utilities;

    /// <summary>A payment search extensions.</summary>
    public static class PaymentSearchExtensions
    {
        /// <summary>An IQueryable{TEntity} extension method that filter payments by transaction number.</summary>
        /// <typeparam name="TEntity">Type of the entity.</typeparam>
        /// <param name="query">The query to act on.</param>
        /// <param name="id">   The identifier.</param>
        /// <returns>An <see cref="IQueryable{TEntity}"/>.</returns>
        public static IQueryable<TEntity> FilterPaymentsByTransactionNumber<TEntity>(this IQueryable<TEntity> query, string? id)
            where TEntity : class, IPayment
        {
            Contract.RequiresNotNull(query);
            return string.IsNullOrEmpty(id) ? query : query.Where(x => x.TransactionNumber == id);
        }
    }
}
