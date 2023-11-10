// <copyright file="SalesGroup.extended.cs" company="clarity-ventures.com">
// Copyright (c) 2018-2023 clarity-ventures.com. All rights reserved.
// </copyright>
// <summary>Implements the sales group search extensions class</summary>
namespace Clarity.Ecommerce.Interfaces.Models
{
    using System;
    using System.Linq;
    using DataModel;
    using Utilities;

    /// <summary>The sales group search extensions.</summary>
    public static class SalesGroupSearchExtensions
    {
        /// <summary>An IQueryable{TEntity} extension method that filter sales groups by search model.</summary>
        /// <typeparam name="TEntity">Type of the entity.</typeparam>
        /// <param name="query">The query to act on.</param>
        /// <param name="model">The model.</param>
        /// <returns>An <see cref="IQueryable{TEntity}"/>.</returns>
        public static IQueryable<TEntity> FilterSalesGroupsBySearchModel<TEntity>(
                this IQueryable<TEntity> query,
                ISalesGroupSearchModel model)
              where TEntity : class, ISalesGroup
        {
            if (model == null)
            {
                return query;
            }
            return Contract.RequiresNotNull(query)
                // Dates
                .FilterSalesGroupsByDates(model.MinDate, model.MaxDate)
                // Account
                .FilterSalesGroupsByAccountID(model.AccountID)
                .FilterSalesGroupsByAccountKey(model.AccountKey)
                .FilterSalesGroupsByAccountName(model.AccountName)
                .FilterSalesGroupsByAccountIDOrCustomKeyOrName(model.AccountIDOrCustomKeyOrName)
                // Billing Contact
                .FilterSalesGroupsByBillingContactID(model.BillingContactID)
                .FilterSalesGroupsByBillingContactKey(model.BillingContactKey)
                // Attached Sales Record
                .FilterSalesGroupsBySalesQuoteID(model.SalesQuoteID)
                .FilterSalesGroupsBySalesQuoteKey(model.SalesQuoteKey)
                .FilterSalesGroupsBySalesOrderID(model.SalesOrderID)
                .FilterSalesGroupsBySalesOrderKey(model.SalesOrderKey)
                .FilterSalesGroupsByPurchaseOrderID(model.PurchaseOrderID)
                .FilterSalesGroupsByPurchaseOrderKey(model.PurchaseOrderKey)
                .FilterSalesGroupsBySalesInvoiceID(model.SalesInvoiceID)
                .FilterSalesGroupsBySalesInvoiceKey(model.SalesInvoiceKey)
                .FilterSalesGroupsBySalesReturnID(model.SalesReturnID)
                .FilterSalesGroupsBySalesReturnKey(model.SalesReturnKey);
        }

        /// <summary>An IQueryable{TEntity} extension method that filter sales groups by dates.</summary>
        /// <typeparam name="TEntity">Type of the entity.</typeparam>
        /// <param name="query">  The query to act on.</param>
        /// <param name="minDate">The minimum date.</param>
        /// <param name="maxDate">The maximum date.</param>
        /// <returns>An <see cref="IQueryable{TEntity}"/>.</returns>
        public static IQueryable<TEntity> FilterSalesGroupsByDates<TEntity>(
                this IQueryable<TEntity> query,
                DateTime? minDate,
                DateTime? maxDate)
            where TEntity : class, IBase
        {
            return Contract.RequiresNotNull(query)
                .FilterSalesGroupsByMinDate(minDate)
                .FilterSalesGroupsByMaxDate(maxDate);
        }

        private static IQueryable<TEntity> FilterSalesGroupsByAccountID<TEntity>(
                this IQueryable<TEntity> query,
                int? id)
            where TEntity : class, ISalesGroup
        {
            if (Contract.CheckInvalidID(id))
            {
                return query;
            }
            return Contract.RequiresNotNull(query)
                .Where(x => x.AccountID == id!.Value);
        }

        private static IQueryable<TEntity> FilterSalesGroupsByAccountKey<TEntity>(
                this IQueryable<TEntity> query,
                string? key,
                bool strict = false)
            where TEntity : class, ISalesGroup
        {
            if (!Contract.CheckValidKey(key))
            {
                return query;
            }
            if (strict)
            {
                return Contract.RequiresNotNull(query)
                    .Where(x => x.Account != null && x.Account.CustomKey == key);
            }
            var search = key!.Trim().ToLower();
            return Contract.RequiresNotNull(query)
                .Where(x => x.Account != null
                         && x.Account.CustomKey != null
                         && x.Account.CustomKey.Contains(search));
        }

        private static IQueryable<TEntity> FilterSalesGroupsByAccountName<TEntity>(
                this IQueryable<TEntity> query,
                string? name,
                bool strict = false)
            where TEntity : class, ISalesGroup
        {
            if (!Contract.CheckValidKey(name))
            {
                return query;
            }
            if (strict)
            {
                return Contract.RequiresNotNull(query)
                    .Where(x => x.Account != null && x.Account.Name == name);
            }
            var search = name!.Trim().ToLower();
            return Contract.RequiresNotNull(query)
                .Where(x => x.Account != null
                         && x.Account.Name != null
                         && x.Account.Name.Contains(search));
        }

        private static IQueryable<TEntity> FilterSalesGroupsByAccountIDOrCustomKeyOrName<TEntity>(
                this IQueryable<TEntity> query,
                string? name)
            where TEntity : class, ISalesGroup
        {
            if (!Contract.CheckValidKey(name))
            {
                return query;
            }
            var search = name!.Trim().ToLower();
            return Contract.RequiresNotNull(query)
                .Where(x => x.Account != null
                         && (x.Account.ID.ToString().Contains(search)
                         || x.Account.CustomKey != null && x.Account.CustomKey.Contains(search)
                         || x.Account.Name != null && x.Account.Name.Contains(search)));
        }

        private static IQueryable<TEntity> FilterSalesGroupsByBillingContactID<TEntity>(
                this IQueryable<TEntity> query,
                int? id)
            where TEntity : class, ISalesGroup
        {
            if (Contract.CheckInvalidID(id))
            {
                return query;
            }
            return Contract.RequiresNotNull(query)
                .Where(x => x.BillingContactID == id!.Value);
        }

        private static IQueryable<TEntity> FilterSalesGroupsByBillingContactKey<TEntity>(
                this IQueryable<TEntity> query,
                string? key,
                bool strict = false)
            where TEntity : class, ISalesGroup
        {
            if (!Contract.CheckValidKey(key))
            {
                return query;
            }
            if (strict)
            {
                return Contract.RequiresNotNull(query)
                    .Where(x => x.BillingContact != null && x.BillingContact.CustomKey == key);
            }
            var search = key!.Trim().ToLower();
            return Contract.RequiresNotNull(query)
                .Where(x => x.BillingContact != null
                    && x.BillingContact.CustomKey != null
                    && x.BillingContact.CustomKey.Contains(search));
        }

        private static IQueryable<TEntity> FilterSalesGroupsBySalesQuoteID<TEntity>(
                this IQueryable<TEntity> query,
                int? id)
            where TEntity : class, ISalesGroup
        {
            if (Contract.CheckInvalidID(id))
            {
                return query;
            }
            return Contract.RequiresNotNull(query)
                .Where(x => x.SalesQuoteRequestMasters!.Any(y => y.ID == id!.Value)
                         || x.SalesQuoteRequestSubs!.Any(y => y.ID == id!.Value)
                         || x.SalesQuoteResponseMasters!.Any(y => y.ID == id!.Value)
                         || x.SalesQuoteResponseSubs!.Any(y => y.ID == id!.Value));
        }

        private static IQueryable<TEntity> FilterSalesGroupsBySalesQuoteKey<TEntity>(
                this IQueryable<TEntity> query,
                string? key,
                bool strict = false)
            where TEntity : class, ISalesGroup
        {
            if (!Contract.CheckValidKey(key))
            {
                return query;
            }
            if (strict)
            {
                return Contract.RequiresNotNull(query)
                    .Where(x => x.SalesQuoteRequestMasters!.Any(y => y.CustomKey != null && y.CustomKey == key)
                             || x.SalesQuoteRequestSubs!.Any(y => y.CustomKey != null && y.CustomKey == key)
                             || x.SalesQuoteResponseMasters!.Any(y => y.CustomKey != null && y.CustomKey == key)
                             || x.SalesQuoteResponseSubs!.Any(y => y.CustomKey != null && y.CustomKey == key));
            }
            var search = key!.Trim().ToLower();
            return Contract.RequiresNotNull(query)
                .Where(x => x.SalesQuoteRequestMasters!.Any(y => y.CustomKey != null && y.CustomKey.Contains(search))
                         || x.SalesQuoteRequestSubs!.Any(y => y.CustomKey != null && y.CustomKey.Contains(search))
                         || x.SalesQuoteResponseMasters!.Any(y => y.CustomKey != null && y.CustomKey.Contains(search))
                         || x.SalesQuoteResponseSubs!.Any(y => y.CustomKey != null && y.CustomKey.Contains(search)));
        }

        private static IQueryable<TEntity> FilterSalesGroupsBySalesOrderID<TEntity>(
                this IQueryable<TEntity> query,
                int? id)
            where TEntity : class, ISalesGroup
        {
            if (Contract.CheckInvalidID(id))
            {
                return query;
            }
            return Contract.RequiresNotNull(query)
                .Where(x => x.SalesOrderMasters!.Any(y => y.ID == id!.Value)
                         || x.SubSalesOrders!.Any(y => y.ID == id!.Value));
        }

        private static IQueryable<TEntity> FilterSalesGroupsBySalesOrderKey<TEntity>(
                this IQueryable<TEntity> query,
                string? key,
                bool strict = false)
            where TEntity : class, ISalesGroup
        {
            if (!Contract.CheckValidKey(key))
            {
                return query;
            }
            if (strict)
            {
                return Contract.RequiresNotNull(query)
                    .Where(x => x.SalesOrderMasters!.Any(y => y.CustomKey != null && y.CustomKey == key)
                        || x.SubSalesOrders!.Any(y => y.CustomKey != null && y.CustomKey == key));
            }
            var search = key!.Trim().ToLower();
            return Contract.RequiresNotNull(query)
                .Where(x => x.SalesOrderMasters!.Any(y => y.CustomKey != null && y.CustomKey.Contains(search))
                    || x.SubSalesOrders!.Any(y => y.CustomKey != null && y.CustomKey.Contains(search)));
        }

        private static IQueryable<TEntity> FilterSalesGroupsByPurchaseOrderID<TEntity>(
                this IQueryable<TEntity> query,
                int? id)
            where TEntity : class, ISalesGroup
        {
            if (Contract.CheckInvalidID(id))
            {
                return query;
            }
            return Contract.RequiresNotNull(query)
                .Where(x => x.PurchaseOrders!.Any(y => y.ID == id!.Value));
        }

        private static IQueryable<TEntity> FilterSalesGroupsByPurchaseOrderKey<TEntity>(
                this IQueryable<TEntity> query,
                string? key,
                bool strict = false)
            where TEntity : class, ISalesGroup
        {
            if (!Contract.CheckValidKey(key))
            {
                return query;
            }
            if (strict)
            {
                return Contract.RequiresNotNull(query)
                    .Where(x => x.PurchaseOrders!
                        .Any(y => y.CustomKey != null
                            && y.CustomKey == key));
            }
            var search = key!.Trim().ToLower();
            return Contract.RequiresNotNull(query)
                .Where(x => x.PurchaseOrders!
                    .Any(y => y.CustomKey != null
                        && y.CustomKey.Contains(search)));
        }

        private static IQueryable<TEntity> FilterSalesGroupsBySalesInvoiceID<TEntity>(
                this IQueryable<TEntity> query,
                int? id)
            where TEntity : class, ISalesGroup
        {
            if (Contract.CheckInvalidID(id))
            {
                return query;
            }
            return Contract.RequiresNotNull(query)
                .Where(x => x.SalesInvoices!.Any(y => y.ID == id!.Value));
        }

        private static IQueryable<TEntity> FilterSalesGroupsBySalesInvoiceKey<TEntity>(
                this IQueryable<TEntity> query,
                string? key,
                bool strict = false)
            where TEntity : class, ISalesGroup
        {
            if (!Contract.CheckValidKey(key))
            {
                return query;
            }
            if (strict)
            {
                return Contract.RequiresNotNull(query)
                    .Where(x => x.SalesInvoices!
                                 .Any(y => y.CustomKey != null
                                        && y.CustomKey == key));
            }
            var search = key!.Trim().ToLower();
            return Contract.RequiresNotNull(query)
                .Where(x => x.SalesInvoices!
                             .Any(y => y.CustomKey != null
                                    && y.CustomKey.Contains(search)));
        }

        private static IQueryable<TEntity> FilterSalesGroupsBySalesReturnID<TEntity>(
                this IQueryable<TEntity> query,
                int? id)
            where TEntity : class, ISalesGroup
        {
            if (Contract.CheckInvalidID(id))
            {
                return query;
            }
            return Contract.RequiresNotNull(query)
                .Where(x => x.SalesReturns!.Any(y => y.ID == id!.Value));
        }

        private static IQueryable<TEntity> FilterSalesGroupsBySalesReturnKey<TEntity>(
                this IQueryable<TEntity> query,
                string? key,
                bool strict = false)
            where TEntity : class, ISalesGroup
        {
            if (!Contract.CheckValidKey(key))
            {
                return query;
            }
            if (strict)
            {
                return Contract.RequiresNotNull(query)
                    .Where(x => x.SalesReturns!
                                 .Any(y => y.CustomKey != null
                                        && y.CustomKey == key));
            }
            var search = key!.Trim().ToLower();
            return Contract.RequiresNotNull(query)
                .Where(x => x.SalesReturns!
                             .Any(y => y.CustomKey != null
                                    && y.CustomKey.Contains(search)));
        }

        /// <summary>An IQueryable{TEntity} extension method that filter sales groups by minimum date.</summary>
        /// <typeparam name="TEntity">Type of the entity.</typeparam>
        /// <param name="query">  The query to act on.</param>
        /// <param name="minDate">The minimum date.</param>
        /// <returns>An <see cref="IQueryable{TEntity}"/>.</returns>
        private static IQueryable<TEntity> FilterSalesGroupsByMinDate<TEntity>(
            this IQueryable<TEntity> query,
            DateTime? minDate)
            where TEntity : class, IBase
        {
            if (!Contract.CheckValidDate(minDate))
            {
                return query;
            }
            // Strip time zone offset info that come from JavaScript
            var date = minDate!.Value.StripTime();
            return Contract.RequiresNotNull(query)
                .Where(x => x.CreatedDate >= date);
        }

        /// <summary>An IQueryable{TEntity} extension method that filter sales groups by maximum date.</summary>
        /// <typeparam name="TEntity">Type of the entity.</typeparam>
        /// <param name="query">  The query to act on.</param>
        /// <param name="maxDate">The maximum date.</param>
        /// <returns>An <see cref="IQueryable{TEntity}"/>.</returns>
        private static IQueryable<TEntity> FilterSalesGroupsByMaxDate<TEntity>(
            this IQueryable<TEntity> query,
            DateTime? maxDate)
            where TEntity : class, IBase
        {
            if (!Contract.CheckValidDate(maxDate))
            {
                return query;
            }
            // Strip time zone offset info that come from JavaScript
            var date = maxDate!.Value.StripTime().AddDays(1); // Basically make it 23:59:59
            return Contract.RequiresNotNull(query)
                .Where(x => x.CreatedDate < date);
        }
    }
}
