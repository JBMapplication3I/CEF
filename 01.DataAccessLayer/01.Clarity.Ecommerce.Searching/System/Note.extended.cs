// <copyright file="Note.extended.cs" company="clarity-ventures.com">
// Copyright (c) 2018-2023 clarity-ventures.com. All rights reserved.
// </copyright>
// <summary>Implements the note search extensions class</summary>
namespace Clarity.Ecommerce.Interfaces.Models
{
    using System.Linq;
    using DataModel;
    using Utilities;

    /// <summary>A note search extensions.</summary>
    public static class NoteSearchExtensions
    {
        /// <summary>An IQueryable{TEntity} extension method that filter notes by purchase order identifier.</summary>
        /// <typeparam name="TEntity">Type of the entity.</typeparam>
        /// <param name="query">The query to act on.</param>
        /// <param name="id">   The identifier.</param>
        /// <returns>An <see cref="IQueryable{TEntity}"/>.</returns>
        public static IQueryable<TEntity> FilterNotesByPurchaseOrderID<TEntity>(
                this IQueryable<TEntity> query,
                int? id)
            where TEntity : class, INote
        {
            if (Contract.CheckInvalidID(id))
            {
                return query;
            }
            return Contract.RequiresNotNull(query)
                .Where(n => n.PurchaseOrderID == id!.Value);
        }

        /// <summary>An IQueryable{TEntity} extension method that filter notes by sample request identifier.</summary>
        /// <typeparam name="TEntity">Type of the entity.</typeparam>
        /// <param name="query">The query to act on.</param>
        /// <param name="id">   The identifier.</param>
        /// <returns>An <see cref="IQueryable{TEntity}"/>.</returns>
        public static IQueryable<TEntity> FilterNotesBySampleRequestID<TEntity>(
                this IQueryable<TEntity> query,
                int? id)
            where TEntity : class, INote
        {
            if (Contract.CheckInvalidID(id))
            {
                return query;
            }
            return Contract.RequiresNotNull(query)
                .Where(n => n.SampleRequestID == id!.Value);
        }

        /// <summary>An IQueryable{TEntity} extension method that filter notes by manufacturer identifier.</summary>
        /// <typeparam name="TEntity">Type of the entity.</typeparam>
        /// <param name="query">The query to act on.</param>
        /// <param name="id">   The identifier.</param>
        /// <returns>An <see cref="IQueryable{TEntity}"/>.</returns>
        public static IQueryable<TEntity> FilterNotesByManufacturerID<TEntity>(
                this IQueryable<TEntity> query,
                int? id)
            where TEntity : class, INote
        {
            if (Contract.CheckInvalidID(id))
            {
                return query;
            }
            return Contract.RequiresNotNull(query)
                .Where(n => n.ManufacturerID == id!.Value);
        }

        /// <summary>An IQueryable{TEntity} extension method that filter notes by vendor identifier.</summary>
        /// <typeparam name="TEntity">Type of the entity.</typeparam>
        /// <param name="query">The query to act on.</param>
        /// <param name="id">   The identifier.</param>
        /// <returns>An <see cref="IQueryable{TEntity}"/>.</returns>
        public static IQueryable<TEntity> FilterNotesByVendorID<TEntity>(
                this IQueryable<TEntity> query,
                int? id)
            where TEntity : class, INote
        {
            if (Contract.CheckInvalidID(id))
            {
                return query;
            }
            return Contract.RequiresNotNull(query)
                .Where(n => n.VendorID == id!.Value);
        }

        /// <summary>An IQueryable{TEntity} extension method that filter notes by sales order identifier.</summary>
        /// <typeparam name="TEntity">Type of the entity.</typeparam>
        /// <param name="query">The query to act on.</param>
        /// <param name="id">   The identifier.</param>
        /// <returns>An <see cref="IQueryable{TEntity}"/>.</returns>
        public static IQueryable<TEntity> FilterNotesBySalesOrderID<TEntity>(
                this IQueryable<TEntity> query,
                int? id)
            where TEntity : class, INote
        {
            if (Contract.CheckInvalidID(id))
            {
                return query;
            }
            return Contract.RequiresNotNull(query)
                .Where(n => n.SalesOrderID == id!.Value);
        }

        /// <summary>An IQueryable{TEntity} extension method that filter notes by account identifier.</summary>
        /// <typeparam name="TEntity">Type of the entity.</typeparam>
        /// <param name="query">The query to act on.</param>
        /// <param name="id">   The identifier.</param>
        /// <returns>An <see cref="IQueryable{TEntity}"/>.</returns>
        public static IQueryable<TEntity> FilterNotesByAccountID<TEntity>(
                this IQueryable<TEntity> query,
                int? id)
            where TEntity : class, INote
        {
            if (Contract.CheckInvalidID(id))
            {
                return query;
            }
            return Contract.RequiresNotNull(query)
                .Where(n => n.AccountID == id!.Value);
        }

        /// <summary>An IQueryable{TEntity} extension method that filter notes by user identifier.</summary>
        /// <typeparam name="TEntity">Type of the entity.</typeparam>
        /// <param name="query">The query to act on.</param>
        /// <param name="id">   The identifier.</param>
        /// <returns>An <see cref="IQueryable{TEntity}"/>.</returns>
        public static IQueryable<TEntity> FilterNotesByUserID<TEntity>(
                this IQueryable<TEntity> query,
                int? id)
            where TEntity : class, INote
        {
            if (Contract.CheckInvalidID(id))
            {
                return query;
            }
            return Contract.RequiresNotNull(query)
                .Where(n => n.UserID == id!.Value);
        }

        /// <summary>An IQueryable{TEntity} extension method that filter notes by sales invoice identifier.</summary>
        /// <typeparam name="TEntity">Type of the entity.</typeparam>
        /// <param name="query">The query to act on.</param>
        /// <param name="id">   The identifier.</param>
        /// <returns>An <see cref="IQueryable{TEntity}"/>.</returns>
        public static IQueryable<TEntity> FilterNotesBySalesInvoiceID<TEntity>(
                this IQueryable<TEntity> query,
                int? id)
            where TEntity : class, INote
        {
            if (Contract.CheckInvalidID(id))
            {
                return query;
            }
            return Contract.RequiresNotNull(query)
                .Where(n => n.SalesInvoiceID == id!.Value);
        }

        /// <summary>An IQueryable{TEntity} extension method that filter notes by sales quote identifier.</summary>
        /// <typeparam name="TEntity">Type of the entity.</typeparam>
        /// <param name="query">The query to act on.</param>
        /// <param name="id">   The identifier.</param>
        /// <returns>An <see cref="IQueryable{TEntity}"/>.</returns>
        public static IQueryable<TEntity> FilterNotesBySalesQuoteID<TEntity>(
                this IQueryable<TEntity> query,
                int? id)
            where TEntity : class, INote
        {
            if (Contract.CheckInvalidID(id))
            {
                return query;
            }
            return Contract.RequiresNotNull(query)
                .Where(n => n.SalesQuoteID == id!.Value);
        }

        /// <summary>An IQueryable{TEntity} extension method that filter notes by cart identifier.</summary>
        /// <typeparam name="TEntity">Type of the entity.</typeparam>
        /// <param name="query">The query to act on.</param>
        /// <param name="id">   The identifier.</param>
        /// <returns>An <see cref="IQueryable{TEntity}"/>.</returns>
        public static IQueryable<TEntity> FilterNotesByCartID<TEntity>(
                this IQueryable<TEntity> query,
                int? id)
            where TEntity : class, INote
        {
            if (Contract.CheckInvalidID(id))
            {
                return query;
            }
            return Contract.RequiresNotNull(query)
                .Where(n => n.CartID == id!.Value);
        }

        /// <summary>An IQueryable{TEntity} extension method that filter notes by sales group identifier.</summary>
        /// <typeparam name="TEntity">Type of the entity.</typeparam>
        /// <param name="query">The query to act on.</param>
        /// <param name="id">   The identifier.</param>
        /// <returns>An <see cref="IQueryable{TEntity}"/>.</returns>
        public static IQueryable<TEntity> FilterNotesBySalesGroupID<TEntity>(
                this IQueryable<TEntity> query,
                int? id)
            where TEntity : class, INote
        {
            if (Contract.CheckInvalidID(id))
            {
                return query;
            }
            return Contract.RequiresNotNull(query)
                .Where(n => n.SalesGroupID == id!.Value);
        }
    }
}
