// <copyright file="SalesItemBaseSearchExtensions.cs" company="clarity-ventures.com">
// Copyright (c) 2018-2023 clarity-ventures.com. All rights reserved.
// </copyright>
// <summary>Implements the sales item base search extensions class</summary>
// ReSharper disable RegionWithSingleElement
namespace Clarity.Ecommerce.Interfaces.Models
{
    using System.Linq;
    using DataModel;
    using Utilities;

    /// <summary>The sales item base search extensions.</summary>
    public static class SalesItemBaseSearchExtensions
    {
        /// <summary>An <see cref="IQueryable{TEntity}" /> extension method that filters  by each of the properties of
        /// the search model which have been set.</summary>
        /// <remarks>Pass a parameter value of null to not filter by this in a fluid call chain.</remarks>
        /// <typeparam name="TEntity">  Type of the entity.</typeparam>
        /// <typeparam name="TDiscount">Type of the discount.</typeparam>
        /// <typeparam name="TTarget">  Type of the target.</typeparam>
        /// <param name="query">The query to act on.</param>
        /// <param name="model">The search model to filter by.</param>
        /// <returns>The <see cref="IQueryable{TEntity}" /> with an additional Where applied if the parameter has a value.</returns>
        public static IQueryable<TEntity> FilterBySalesItemBaseSearchModel<TEntity, TDiscount, TTarget>(
                this IQueryable<TEntity> query,
                ISalesItemBaseSearchModel model)
            where TEntity : class, ISalesItemBase<TEntity, TDiscount, TTarget>
            where TDiscount : IAppliedDiscountBase<TEntity, TDiscount>
            where TTarget : ISalesItemTargetBase
        {
            if (model == null)
            {
                return query;
            }
            return Contract.RequiresNotNull(query)
                .FilterSalesItemBasesByMinQuantity<TEntity, TDiscount, TTarget>(model.MinQuantity)
                .FilterSalesItemBasesByMaxQuantity<TEntity, TDiscount, TTarget>(model.MaxQuantity)
                .FilterSalesItemBasesByMatchQuantity<TEntity, TDiscount, TTarget>(model.MatchQuantity)
                .FilterSalesItemBasesByMinUnitCorePrice<TEntity, TDiscount, TTarget>(model.MinUnitCorePrice)
                .FilterSalesItemBasesByMaxUnitCorePrice<TEntity, TDiscount, TTarget>(model.MaxUnitCorePrice)
                .FilterSalesItemBasesByMatchUnitCorePrice<TEntity, TDiscount, TTarget>(model.MatchUnitCorePrice)
                .FilterSalesItemBasesByMasterID<TEntity, TDiscount, TTarget>(model.MasterID)
                .FilterSalesItemBasesByOriginalCurrencyID<TEntity, TDiscount, TTarget>(model.OriginalCurrencyID, model.OriginalCurrencyIDIncludeNull)
                .FilterSalesItemBasesByProductID<TEntity, TDiscount, TTarget>(model.ProductID, model.ProductIDIncludeNull)
                .FilterSalesItemBasesByMinQuantityBackOrdered<TEntity, TDiscount, TTarget>(model.MinQuantityBackOrdered)
                .FilterSalesItemBasesByMaxQuantityBackOrdered<TEntity, TDiscount, TTarget>(model.MaxQuantityBackOrdered)
                .FilterSalesItemBasesByMatchQuantityBackOrdered<TEntity, TDiscount, TTarget>(model.MatchQuantityBackOrdered, model.MatchQuantityBackOrderedIncludeNull)
                .FilterSalesItemBasesByMinQuantityPreSold<TEntity, TDiscount, TTarget>(model.MinQuantityPreSold)
                .FilterSalesItemBasesByMaxQuantityPreSold<TEntity, TDiscount, TTarget>(model.MaxQuantityPreSold)
                .FilterSalesItemBasesByMatchQuantityPreSold<TEntity, TDiscount, TTarget>(model.MatchQuantityPreSold, model.MatchQuantityPreSoldIncludeNull)
                .FilterSalesItemBasesBySellingCurrencyID<TEntity, TDiscount, TTarget>(model.SellingCurrencyID, model.SellingCurrencyIDIncludeNull)
                .FilterSalesItemBasesByMinUnitCorePriceInSellingCurrency<TEntity, TDiscount, TTarget>(model.MinUnitCorePriceInSellingCurrency)
                .FilterSalesItemBasesByMaxUnitCorePriceInSellingCurrency<TEntity, TDiscount, TTarget>(model.MaxUnitCorePriceInSellingCurrency)
                .FilterSalesItemBasesByMatchUnitCorePriceInSellingCurrency<TEntity, TDiscount, TTarget>(model.MatchUnitCorePriceInSellingCurrency, model.MatchUnitCorePriceInSellingCurrencyIncludeNull)
                .FilterSalesItemBasesByMinUnitSoldPrice<TEntity, TDiscount, TTarget>(model.MinUnitSoldPrice)
                .FilterSalesItemBasesByMaxUnitSoldPrice<TEntity, TDiscount, TTarget>(model.MaxUnitSoldPrice)
                .FilterSalesItemBasesByMatchUnitSoldPrice<TEntity, TDiscount, TTarget>(model.MatchUnitSoldPrice, model.MatchUnitSoldPriceIncludeNull)
                .FilterSalesItemBasesByMinUnitSoldPriceInSellingCurrency<TEntity, TDiscount, TTarget>(model.MinUnitSoldPriceInSellingCurrency)
                .FilterSalesItemBasesByMaxUnitSoldPriceInSellingCurrency<TEntity, TDiscount, TTarget>(model.MaxUnitSoldPriceInSellingCurrency)
                .FilterSalesItemBasesByMatchUnitSoldPriceInSellingCurrency<TEntity, TDiscount, TTarget>(model.MatchUnitSoldPriceInSellingCurrency, model.MatchUnitSoldPriceInSellingCurrencyIncludeNull)
                .FilterSalesItemBasesByUserID<TEntity, TDiscount, TTarget>(model.UserID, model.UserIDIncludeNull)
                .FilterSalesItemBasesByForceUniqueLineItemKey<TEntity, TDiscount, TTarget>(model.ForceUniqueLineItemKey, model.ForceUniqueLineItemKeyStrict, model.ForceUniqueLineItemKeyIncludeNull)
                .FilterSalesItemBasesBySku<TEntity, TDiscount, TTarget>(model.Sku, model.SkuStrict, model.SkuIncludeNull)
                .FilterSalesItemBasesByUnitOfMeasure<TEntity, TDiscount, TTarget>(model.UnitOfMeasure, model.UnitOfMeasureStrict, model.UnitOfMeasureIncludeNull);
        }

        /// <summary>An IQueryable{TEntity} extension method that filter sales items by master active.</summary>
        /// <typeparam name="TEntity">  Type of the entity.</typeparam>
        /// <typeparam name="TDiscount">Type of the discount.</typeparam>
        /// <typeparam name="TTarget">  Type of the target.</typeparam>
        /// <param name="query">The query to act on.</param>
        /// <returns>An <see cref="IQueryable{TEntity}"/>.</returns>
        public static IQueryable<TEntity> FilterSalesItemsByMasterActive<TEntity, TDiscount, TTarget>(
                this IQueryable<TEntity> query)
            where TEntity : class, ISalesItemBase<TEntity, TDiscount, TTarget>
            where TDiscount : IAppliedDiscountBase<TEntity, TDiscount>
            where TTarget : ISalesItemTargetBase
        {
            return Contract.RequiresNotNull(query)
                .Where(x => x.Master!.Active);
        }

        /// <summary>An IQueryable{TEntity} extension method that filter sales items by master identifier.</summary>
        /// <typeparam name="TEntity">  Type of the entity.</typeparam>
        /// <typeparam name="TDiscount">Type of the discount.</typeparam>
        /// <typeparam name="TTarget">  Type of the target.</typeparam>
        /// <param name="query">The query to act on.</param>
        /// <param name="id">   The identifier.</param>
        /// <returns>An <see cref="IQueryable{TEntity}"/>.</returns>
        public static IQueryable<TEntity> FilterSalesItemsByMasterID<TEntity, TDiscount, TTarget>(
                this IQueryable<TEntity> query,
                int? id)
            where TEntity : class, ISalesItemBase<TEntity, TDiscount, TTarget>
            where TDiscount : IAppliedDiscountBase<TEntity, TDiscount>
            where TTarget : ISalesItemTargetBase
        {
            if (Contract.CheckInvalidID(id))
            {
                return query;
            }
            return Contract.RequiresNotNull(query)
                .Where(x => x.MasterID == id);
        }

        #region Quantity
        /// <summary>An <see cref="IQueryable{TEntity}" /> extension method that filters records by the parameter (a
        /// minimum decimal value).</summary>
        /// <remarks>Pass a parameter value of null to not filter by this in a fluid call chain.</remarks>
        /// <typeparam name="TEntity">  Type of the entity.</typeparam>
        /// <typeparam name="TDiscount">Type of the discount.</typeparam>
        /// <typeparam name="TTarget">  Type of the target.</typeparam>
        /// <param name="query">    The query to act on.</param>
        /// <param name="parameter">The parameter to filter by.</param>
        /// <returns>The <see cref="IQueryable{TEntity}" /> with an additional Where applied if the parameter has a value.</returns>
        public static IQueryable<TEntity> FilterSalesItemBasesByMinQuantity<TEntity, TDiscount, TTarget>(
                this IQueryable<TEntity> query,
                decimal? parameter)
            where TEntity : class, ISalesItemBase<TEntity, TDiscount, TTarget>
            where TDiscount : IAppliedDiscountBase<TEntity, TDiscount>
            where TTarget : ISalesItemTargetBase
        {
            if (!parameter.HasValue)
            {
                return query;
            }
            var search = parameter.Value;
            return Contract.RequiresNotNull(query)
                .Where(x => x.Quantity >= search);
        }

        /// <summary>An <see cref="IQueryable{TEntity}" /> extension method that filters records by the parameter (a
        /// maximum decimal value).</summary>
        /// <remarks>Pass a parameter value of null to not filter by this in a fluid call chain.</remarks>
        /// <typeparam name="TEntity">  Type of the entity.</typeparam>
        /// <typeparam name="TDiscount">Type of the discount.</typeparam>
        /// <typeparam name="TTarget">  Type of the target.</typeparam>
        /// <param name="query">    The query to act on.</param>
        /// <param name="parameter">The parameter to filter by.</param>
        /// <returns>The <see cref="IQueryable{TEntity}" /> with an additional Where applied if the parameter has a value.</returns>
        public static IQueryable<TEntity> FilterSalesItemBasesByMaxQuantity<TEntity, TDiscount, TTarget>(
                this IQueryable<TEntity> query,
                decimal? parameter)
            where TEntity : class, ISalesItemBase<TEntity, TDiscount, TTarget>
            where TDiscount : IAppliedDiscountBase<TEntity, TDiscount>
            where TTarget : ISalesItemTargetBase
        {
            if (!parameter.HasValue)
            {
                return query;
            }
            return Contract.RequiresNotNull(query)
                .Where(x => x.Quantity <= parameter.Value);
        }

        /// <summary>An <see cref="IQueryable{TEntity}" /> extension method that filters records by the parameter (a
        /// decimal value to match exactly).</summary>
        /// <remarks>Pass a parameter value of null to not filter by this in a fluid call chain.</remarks>
        /// <typeparam name="TEntity">  Type of the entity.</typeparam>
        /// <typeparam name="TDiscount">Type of the discount.</typeparam>
        /// <typeparam name="TTarget">  Type of the target.</typeparam>
        /// <param name="query">    The query to act on.</param>
        /// <param name="parameter">The parameter to filter by.</param>
        /// <returns>The <see cref="IQueryable{TEntity}" /> with an additional Where applied if the parameter has a value.</returns>
        public static IQueryable<TEntity> FilterSalesItemBasesByMatchQuantity<TEntity, TDiscount, TTarget>(
                this IQueryable<TEntity> query,
                decimal? parameter)
            where TEntity : class, ISalesItemBase<TEntity, TDiscount, TTarget>
            where TDiscount : IAppliedDiscountBase<TEntity, TDiscount>
            where TTarget : ISalesItemTargetBase
        {
            if (!parameter.HasValue)
            {
                return query;
            }
            return Contract.RequiresNotNull(query)
                .Where(x => x.Quantity == parameter);
        }
        #endregion

        #region UnitCorePrice
        /// <summary>An <see cref="IQueryable{TEntity}" /> extension method that filters records by the parameter (a
        /// minimum decimal value).</summary>
        /// <remarks>Pass a parameter value of null to not filter by this in a fluid call chain.</remarks>
        /// <typeparam name="TEntity">  Type of the entity.</typeparam>
        /// <typeparam name="TDiscount">Type of the discount.</typeparam>
        /// <typeparam name="TTarget">  Type of the target.</typeparam>
        /// <param name="query">    The query to act on.</param>
        /// <param name="parameter">The parameter to filter by.</param>
        /// <returns>The <see cref="IQueryable{TEntity}" /> with an additional Where applied if the parameter has a value.</returns>
        public static IQueryable<TEntity> FilterSalesItemBasesByMinUnitCorePrice<TEntity, TDiscount, TTarget>(
                this IQueryable<TEntity> query,
                decimal? parameter)
            where TEntity : class, ISalesItemBase<TEntity, TDiscount, TTarget>
            where TDiscount : IAppliedDiscountBase<TEntity, TDiscount>
            where TTarget : ISalesItemTargetBase
        {
            if (!parameter.HasValue)
            {
                return query;
            }
            var search = parameter.Value;
            return Contract.RequiresNotNull(query)
                .Where(x => x.UnitCorePrice >= search);
        }

        /// <summary>An <see cref="IQueryable{TEntity}" /> extension method that filters records by the parameter (a
        /// maximum decimal value).</summary>
        /// <remarks>Pass a parameter value of null to not filter by this in a fluid call chain.</remarks>
        /// <typeparam name="TEntity">  Type of the entity.</typeparam>
        /// <typeparam name="TDiscount">Type of the discount.</typeparam>
        /// <typeparam name="TTarget">  Type of the target.</typeparam>
        /// <param name="query">    The query to act on.</param>
        /// <param name="parameter">The parameter to filter by.</param>
        /// <returns>The <see cref="IQueryable{TEntity}" /> with an additional Where applied if the parameter has a value.</returns>
        public static IQueryable<TEntity> FilterSalesItemBasesByMaxUnitCorePrice<TEntity, TDiscount, TTarget>(
                this IQueryable<TEntity> query,
                decimal? parameter)
            where TEntity : class, ISalesItemBase<TEntity, TDiscount, TTarget>
            where TDiscount : IAppliedDiscountBase<TEntity, TDiscount>
            where TTarget : ISalesItemTargetBase
        {
            if (!parameter.HasValue)
            {
                return query;
            }
            return Contract.RequiresNotNull(query)
                .Where(x => x.UnitCorePrice <= parameter.Value);
        }

        /// <summary>An <see cref="IQueryable{TEntity}" /> extension method that filters records by the parameter (a
        /// decimal value to match exactly).</summary>
        /// <remarks>Pass a parameter value of null to not filter by this in a fluid call chain.</remarks>
        /// <typeparam name="TEntity">  Type of the entity.</typeparam>
        /// <typeparam name="TDiscount">Type of the discount.</typeparam>
        /// <typeparam name="TTarget">  Type of the target.</typeparam>
        /// <param name="query">    The query to act on.</param>
        /// <param name="parameter">The parameter to filter by.</param>
        /// <returns>The <see cref="IQueryable{TEntity}" /> with an additional Where applied if the parameter has a value.</returns>
        public static IQueryable<TEntity> FilterSalesItemBasesByMatchUnitCorePrice<TEntity, TDiscount, TTarget>(
                this IQueryable<TEntity> query,
                decimal? parameter)
            where TEntity : class, ISalesItemBase<TEntity, TDiscount, TTarget>
            where TDiscount : IAppliedDiscountBase<TEntity, TDiscount>
            where TTarget : ISalesItemTargetBase
        {
            if (!parameter.HasValue)
            {
                return query;
            }
            return Contract.RequiresNotNull(query)
                .Where(x => x.UnitCorePrice == parameter);
        }
        #endregion

        #region MasterID
        /// <summary>An <see cref="IQueryable{TEntity}" /> extension method that filters records by the parameter (an
        /// identifier).</summary>
        /// <remarks>Pass a parameter value of null to not filter by this in a fluid call chain.</remarks>
        /// <typeparam name="TEntity">  Type of the entity.</typeparam>
        /// <typeparam name="TDiscount">Type of the discount.</typeparam>
        /// <typeparam name="TTarget">  Type of the target.</typeparam>
        /// <param name="query">    The query to act on.</param>
        /// <param name="parameter">The parameter to filter by.</param>
        /// <returns>The <see cref="IQueryable{TEntity}" /> with an additional Where applied if the parameter has a value.</returns>
        public static IQueryable<TEntity> FilterSalesItemBasesByMasterID<TEntity, TDiscount, TTarget>(
                this IQueryable<TEntity> query,
                int? parameter)
            where TEntity : class, ISalesItemBase<TEntity, TDiscount, TTarget>
            where TDiscount : IAppliedDiscountBase<TEntity, TDiscount>
            where TTarget : ISalesItemTargetBase
        {
            if (Contract.CheckInvalidID(parameter))
            {
                return query;
            }
            return Contract.RequiresNotNull(query)
                .Where(x => x.MasterID == parameter);
        }
        #endregion

        #region OriginalCurrencyID
        /// <summary>An <see cref="IQueryable{TEntity}" /> extension method that filters records by the parameter (an
        /// identifier).</summary>
        /// <remarks>Pass a parameter value of null to not filter by this in a fluid call chain.</remarks>
        /// <typeparam name="TEntity">  Type of the entity.</typeparam>
        /// <typeparam name="TDiscount">Type of the discount.</typeparam>
        /// <typeparam name="TTarget">  Type of the target.</typeparam>
        /// <param name="query">      The query to act on.</param>
        /// <param name="parameter">  The parameter to filter by.</param>
        /// <param name="includeNull">If set, match the parameter even if it's null.</param>
        /// <returns>The <see cref="IQueryable{TEntity}" /> with an additional Where applied if the parameter has a value.</returns>
        public static IQueryable<TEntity> FilterSalesItemBasesByOriginalCurrencyID<TEntity, TDiscount, TTarget>(
                this IQueryable<TEntity> query,
                int? parameter,
                bool? includeNull)
            where TEntity : class, ISalesItemBase<TEntity, TDiscount, TTarget>
            where TDiscount : IAppliedDiscountBase<TEntity, TDiscount>
            where TTarget : ISalesItemTargetBase
        {
            if (Contract.CheckInvalidID(parameter) || includeNull != true)
            {
                return query;
            }
            return Contract.RequiresNotNull(query)
                .Where(x => x.OriginalCurrencyID == parameter);
        }
        #endregion

        #region ProductID
        /// <summary>An <see cref="IQueryable{TEntity}" /> extension method that filters records by the parameter (an
        /// identifier).</summary>
        /// <remarks>Pass a parameter value of null to not filter by this in a fluid call chain.</remarks>
        /// <typeparam name="TEntity">  Type of the entity.</typeparam>
        /// <typeparam name="TDiscount">Type of the discount.</typeparam>
        /// <typeparam name="TTarget">  Type of the target.</typeparam>
        /// <param name="query">      The query to act on.</param>
        /// <param name="parameter">  The parameter to filter by.</param>
        /// <param name="includeNull">If set, match the parameter even if it's null.</param>
        /// <returns>The <see cref="IQueryable{TEntity}" /> with an additional Where applied if the parameter has a value.</returns>
        public static IQueryable<TEntity> FilterSalesItemBasesByProductID<TEntity, TDiscount, TTarget>(
                this IQueryable<TEntity> query,
                int? parameter,
                bool? includeNull)
            where TEntity : class, ISalesItemBase<TEntity, TDiscount, TTarget>
            where TDiscount : IAppliedDiscountBase<TEntity, TDiscount>
            where TTarget : ISalesItemTargetBase
        {
            if (Contract.CheckInvalidID(parameter) || includeNull != true)
            {
                return query;
            }
            return Contract.RequiresNotNull(query)
                .Where(x => x.ProductID == parameter);
        }
        #endregion

        #region QuantityBackOrdered
        /// <summary>An <see cref="IQueryable{TEntity}" /> extension method that filters records by the parameter (a
        /// minimum decimal value).</summary>
        /// <remarks>Pass a parameter value of null to not filter by this in a fluid call chain.</remarks>
        /// <typeparam name="TEntity">  Type of the entity.</typeparam>
        /// <typeparam name="TDiscount">Type of the discount.</typeparam>
        /// <typeparam name="TTarget">  Type of the target.</typeparam>
        /// <param name="query">    The query to act on.</param>
        /// <param name="parameter">The parameter to filter by.</param>
        /// <returns>The <see cref="IQueryable{TEntity}" /> with an additional Where applied if the parameter has a value.</returns>
        public static IQueryable<TEntity> FilterSalesItemBasesByMinQuantityBackOrdered<TEntity, TDiscount, TTarget>(
                this IQueryable<TEntity> query,
                decimal? parameter)
            where TEntity : class, ISalesItemBase<TEntity, TDiscount, TTarget>
            where TDiscount : IAppliedDiscountBase<TEntity, TDiscount>
            where TTarget : ISalesItemTargetBase
        {
            if (!parameter.HasValue)
            {
                return query;
            }
            var search = parameter.Value;
            return Contract.RequiresNotNull(query)
                .Where(x => x.QuantityBackOrdered >= search);
        }

        /// <summary>An <see cref="IQueryable{TEntity}" /> extension method that filters records by the parameter (a
        /// maximum decimal value).</summary>
        /// <remarks>Pass a parameter value of null to not filter by this in a fluid call chain.</remarks>
        /// <typeparam name="TEntity">  Type of the entity.</typeparam>
        /// <typeparam name="TDiscount">Type of the discount.</typeparam>
        /// <typeparam name="TTarget">  Type of the target.</typeparam>
        /// <param name="query">    The query to act on.</param>
        /// <param name="parameter">The parameter to filter by.</param>
        /// <returns>The <see cref="IQueryable{TEntity}" /> with an additional Where applied if the parameter has a value.</returns>
        public static IQueryable<TEntity> FilterSalesItemBasesByMaxQuantityBackOrdered<TEntity, TDiscount, TTarget>(
                this IQueryable<TEntity> query,
                decimal? parameter)
            where TEntity : class, ISalesItemBase<TEntity, TDiscount, TTarget>
            where TDiscount : IAppliedDiscountBase<TEntity, TDiscount>
            where TTarget : ISalesItemTargetBase
        {
            if (!parameter.HasValue)
            {
                return query;
            }
            return Contract.RequiresNotNull(query)
                .Where(x => x.QuantityBackOrdered <= parameter.Value);
        }

        /// <summary>An <see cref="IQueryable{TEntity}" /> extension method that filters records by the parameter (a
        /// decimal value to match exactly).</summary>
        /// <remarks>Pass a parameter value of null to not filter by this in a fluid call chain.</remarks>
        /// <typeparam name="TEntity">  Type of the entity.</typeparam>
        /// <typeparam name="TDiscount">Type of the discount.</typeparam>
        /// <typeparam name="TTarget">  Type of the target.</typeparam>
        /// <param name="query">      The query to act on.</param>
        /// <param name="parameter">  The parameter to filter by.</param>
        /// <param name="includeNull">If set, match the parameter even if it's null.</param>
        /// <returns>The <see cref="IQueryable{TEntity}" /> with an additional Where applied if the parameter has a value.</returns>
        public static IQueryable<TEntity> FilterSalesItemBasesByMatchQuantityBackOrdered<TEntity, TDiscount, TTarget>(
                this IQueryable<TEntity> query,
                decimal? parameter,
                bool? includeNull)
            where TEntity : class, ISalesItemBase<TEntity, TDiscount, TTarget>
            where TDiscount : IAppliedDiscountBase<TEntity, TDiscount>
            where TTarget : ISalesItemTargetBase
        {
            if (!parameter.HasValue || includeNull != true)
            {
                return query;
            }
            return Contract.RequiresNotNull(query)
                .Where(x => x.QuantityBackOrdered == parameter);
        }
        #endregion

        #region QuantityPreSold
        /// <summary>An <see cref="IQueryable{TEntity}" /> extension method that filters records by the parameter (a
        /// minimum decimal value).</summary>
        /// <remarks>Pass a parameter value of null to not filter by this in a fluid call chain.</remarks>
        /// <typeparam name="TEntity">  Type of the entity.</typeparam>
        /// <typeparam name="TDiscount">Type of the discount.</typeparam>
        /// <typeparam name="TTarget">  Type of the target.</typeparam>
        /// <param name="query">    The query to act on.</param>
        /// <param name="parameter">The parameter to filter by.</param>
        /// <returns>The <see cref="IQueryable{TEntity}" /> with an additional Where applied if the parameter has a value.</returns>
        public static IQueryable<TEntity> FilterSalesItemBasesByMinQuantityPreSold<TEntity, TDiscount, TTarget>(
                this IQueryable<TEntity> query,
                decimal? parameter)
            where TEntity : class, ISalesItemBase<TEntity, TDiscount, TTarget>
            where TDiscount : IAppliedDiscountBase<TEntity, TDiscount>
            where TTarget : ISalesItemTargetBase
        {
            if (!parameter.HasValue)
            {
                return query;
            }
            var search = parameter.Value;
            return Contract.RequiresNotNull(query)
                .Where(x => x.QuantityPreSold >= search);
        }

        /// <summary>An <see cref="IQueryable{TEntity}" /> extension method that filters records by the parameter (a
        /// maximum decimal value).</summary>
        /// <remarks>Pass a parameter value of null to not filter by this in a fluid call chain.</remarks>
        /// <typeparam name="TEntity">  Type of the entity.</typeparam>
        /// <typeparam name="TDiscount">Type of the discount.</typeparam>
        /// <typeparam name="TTarget">  Type of the target.</typeparam>
        /// <param name="query">    The query to act on.</param>
        /// <param name="parameter">The parameter to filter by.</param>
        /// <returns>The <see cref="IQueryable{TEntity}" /> with an additional Where applied if the parameter has a value.</returns>
        public static IQueryable<TEntity> FilterSalesItemBasesByMaxQuantityPreSold<TEntity, TDiscount, TTarget>(
                this IQueryable<TEntity> query,
                decimal? parameter)
            where TEntity : class, ISalesItemBase<TEntity, TDiscount, TTarget>
            where TDiscount : IAppliedDiscountBase<TEntity, TDiscount>
            where TTarget : ISalesItemTargetBase
        {
            if (!parameter.HasValue)
            {
                return query;
            }
            return Contract.RequiresNotNull(query)
                .Where(x => x.QuantityPreSold <= parameter.Value);
        }

        /// <summary>An <see cref="IQueryable{TEntity}" /> extension method that filters records by the parameter (a
        /// decimal value to match exactly).</summary>
        /// <remarks>Pass a parameter value of null to not filter by this in a fluid call chain.</remarks>
        /// <typeparam name="TEntity">  Type of the entity.</typeparam>
        /// <typeparam name="TDiscount">Type of the discount.</typeparam>
        /// <typeparam name="TTarget">  Type of the target.</typeparam>
        /// <param name="query">      The query to act on.</param>
        /// <param name="parameter">  The parameter to filter by.</param>
        /// <param name="includeNull">If set, match the parameter even if it's null.</param>
        /// <returns>The <see cref="IQueryable{TEntity}" /> with an additional Where applied if the parameter has a value.</returns>
        public static IQueryable<TEntity> FilterSalesItemBasesByMatchQuantityPreSold<TEntity, TDiscount, TTarget>(
                this IQueryable<TEntity> query,
                decimal? parameter,
                bool? includeNull)
            where TEntity : class, ISalesItemBase<TEntity, TDiscount, TTarget>
            where TDiscount : IAppliedDiscountBase<TEntity, TDiscount>
            where TTarget : ISalesItemTargetBase
        {
            if (!parameter.HasValue || includeNull != true)
            {
                return query;
            }
            return Contract.RequiresNotNull(query)
                .Where(x => x.QuantityPreSold == parameter);
        }
        #endregion

        #region SellingCurrencyID
        /// <summary>An <see cref="IQueryable{TEntity}" /> extension method that filters records by the parameter (an
        /// identifier).</summary>
        /// <remarks>Pass a parameter value of null to not filter by this in a fluid call chain.</remarks>
        /// <typeparam name="TEntity">  Type of the entity.</typeparam>
        /// <typeparam name="TDiscount">Type of the discount.</typeparam>
        /// <typeparam name="TTarget">  Type of the target.</typeparam>
        /// <param name="query">      The query to act on.</param>
        /// <param name="parameter">  The parameter to filter by.</param>
        /// <param name="includeNull">If set, match the parameter even if it's null.</param>
        /// <returns>The <see cref="IQueryable{TEntity}" /> with an additional Where applied if the parameter has a value.</returns>
        public static IQueryable<TEntity> FilterSalesItemBasesBySellingCurrencyID<TEntity, TDiscount, TTarget>(
                this IQueryable<TEntity> query,
                int? parameter,
                bool? includeNull)
            where TEntity : class, ISalesItemBase<TEntity, TDiscount, TTarget>
            where TDiscount : IAppliedDiscountBase<TEntity, TDiscount>
            where TTarget : ISalesItemTargetBase
        {
            if (Contract.CheckInvalidID(parameter) || includeNull != true)
            {
                return query;
            }
            return Contract.RequiresNotNull(query)
                .Where(x => x.SellingCurrencyID == parameter);
        }
        #endregion

        #region UnitCorePriceInSellingCurrency
        /// <summary>An <see cref="IQueryable{TEntity}" /> extension method that filters records by the parameter (a
        /// minimum decimal value).</summary>
        /// <remarks>Pass a parameter value of null to not filter by this in a fluid call chain.</remarks>
        /// <typeparam name="TEntity">  Type of the entity.</typeparam>
        /// <typeparam name="TDiscount">Type of the discount.</typeparam>
        /// <typeparam name="TTarget">  Type of the target.</typeparam>
        /// <param name="query">    The query to act on.</param>
        /// <param name="parameter">The parameter to filter by.</param>
        /// <returns>The <see cref="IQueryable{TEntity}" /> with an additional Where applied if the parameter has a value.</returns>
        public static IQueryable<TEntity> FilterSalesItemBasesByMinUnitCorePriceInSellingCurrency<TEntity, TDiscount, TTarget>(
                this IQueryable<TEntity> query,
                decimal? parameter)
            where TEntity : class, ISalesItemBase<TEntity, TDiscount, TTarget>
            where TDiscount : IAppliedDiscountBase<TEntity, TDiscount>
            where TTarget : ISalesItemTargetBase
        {
            if (!parameter.HasValue)
            {
                return query;
            }
            var search = parameter.Value;
            return Contract.RequiresNotNull(query)
                .Where(x => x.UnitCorePriceInSellingCurrency >= search);
        }

        /// <summary>An <see cref="IQueryable{TEntity}" /> extension method that filters records by the parameter (a
        /// maximum decimal value).</summary>
        /// <remarks>Pass a parameter value of null to not filter by this in a fluid call chain.</remarks>
        /// <typeparam name="TEntity">  Type of the entity.</typeparam>
        /// <typeparam name="TDiscount">Type of the discount.</typeparam>
        /// <typeparam name="TTarget">  Type of the target.</typeparam>
        /// <param name="query">    The query to act on.</param>
        /// <param name="parameter">The parameter to filter by.</param>
        /// <returns>The <see cref="IQueryable{TEntity}" /> with an additional Where applied if the parameter has a value.</returns>
        public static IQueryable<TEntity> FilterSalesItemBasesByMaxUnitCorePriceInSellingCurrency<TEntity, TDiscount, TTarget>(
                this IQueryable<TEntity> query,
                decimal? parameter)
            where TEntity : class, ISalesItemBase<TEntity, TDiscount, TTarget>
            where TDiscount : IAppliedDiscountBase<TEntity, TDiscount>
            where TTarget : ISalesItemTargetBase
        {
            if (!parameter.HasValue)
            {
                return query;
            }
            return Contract.RequiresNotNull(query)
                .Where(x => x.UnitCorePriceInSellingCurrency <= parameter.Value);
        }

        /// <summary>An <see cref="IQueryable{TEntity}" /> extension method that filters records by the parameter (a
        /// decimal value to match exactly).</summary>
        /// <remarks>Pass a parameter value of null to not filter by this in a fluid call chain.</remarks>
        /// <typeparam name="TEntity">  Type of the entity.</typeparam>
        /// <typeparam name="TDiscount">Type of the discount.</typeparam>
        /// <typeparam name="TTarget">  Type of the target.</typeparam>
        /// <param name="query">      The query to act on.</param>
        /// <param name="parameter">  The parameter to filter by.</param>
        /// <param name="includeNull">If set, match the parameter even if it's null.</param>
        /// <returns>The <see cref="IQueryable{TEntity}" /> with an additional Where applied if the parameter has a value.</returns>
        public static IQueryable<TEntity> FilterSalesItemBasesByMatchUnitCorePriceInSellingCurrency<TEntity, TDiscount, TTarget>(
                this IQueryable<TEntity> query,
                decimal? parameter,
                bool? includeNull)
            where TEntity : class, ISalesItemBase<TEntity, TDiscount, TTarget>
            where TDiscount : IAppliedDiscountBase<TEntity, TDiscount>
            where TTarget : ISalesItemTargetBase
        {
            if (!parameter.HasValue || includeNull != true)
            {
                return query;
            }
            return Contract.RequiresNotNull(query)
                .Where(x => x.UnitCorePriceInSellingCurrency == parameter);
        }
        #endregion

        #region UnitSoldPrice
        /// <summary>An <see cref="IQueryable{TEntity}" /> extension method that filters records by the parameter (a
        /// minimum decimal value).</summary>
        /// <remarks>Pass a parameter value of null to not filter by this in a fluid call chain.</remarks>
        /// <typeparam name="TEntity">  Type of the entity.</typeparam>
        /// <typeparam name="TDiscount">Type of the discount.</typeparam>
        /// <typeparam name="TTarget">  Type of the target.</typeparam>
        /// <param name="query">    The query to act on.</param>
        /// <param name="parameter">The parameter to filter by.</param>
        /// <returns>The <see cref="IQueryable{TEntity}" /> with an additional Where applied if the parameter has a value.</returns>
        public static IQueryable<TEntity> FilterSalesItemBasesByMinUnitSoldPrice<TEntity, TDiscount, TTarget>(
                this IQueryable<TEntity> query,
                decimal? parameter)
            where TEntity : class, ISalesItemBase<TEntity, TDiscount, TTarget>
            where TDiscount : IAppliedDiscountBase<TEntity, TDiscount>
            where TTarget : ISalesItemTargetBase
        {
            if (!parameter.HasValue)
            {
                return query;
            }
            var search = parameter.Value;
            return Contract.RequiresNotNull(query)
                .Where(x => x.UnitSoldPrice >= search);
        }

        /// <summary>An <see cref="IQueryable{TEntity}" /> extension method that filters records by the parameter (a
        /// maximum decimal value).</summary>
        /// <remarks>Pass a parameter value of null to not filter by this in a fluid call chain.</remarks>
        /// <typeparam name="TEntity">  Type of the entity.</typeparam>
        /// <typeparam name="TDiscount">Type of the discount.</typeparam>
        /// <typeparam name="TTarget">  Type of the target.</typeparam>
        /// <param name="query">    The query to act on.</param>
        /// <param name="parameter">The parameter to filter by.</param>
        /// <returns>The <see cref="IQueryable{TEntity}" /> with an additional Where applied if the parameter has a value.</returns>
        public static IQueryable<TEntity> FilterSalesItemBasesByMaxUnitSoldPrice<TEntity, TDiscount, TTarget>(
                this IQueryable<TEntity> query,
                decimal? parameter)
            where TEntity : class, ISalesItemBase<TEntity, TDiscount, TTarget>
            where TDiscount : IAppliedDiscountBase<TEntity, TDiscount>
            where TTarget : ISalesItemTargetBase
        {
            if (!parameter.HasValue)
            {
                return query;
            }
            return Contract.RequiresNotNull(query)
                .Where(x => x.UnitSoldPrice <= parameter.Value);
        }

        /// <summary>An <see cref="IQueryable{TEntity}" /> extension method that filters records by the parameter (a
        /// decimal value to match exactly).</summary>
        /// <remarks>Pass a parameter value of null to not filter by this in a fluid call chain.</remarks>
        /// <typeparam name="TEntity">  Type of the entity.</typeparam>
        /// <typeparam name="TDiscount">Type of the discount.</typeparam>
        /// <typeparam name="TTarget">  Type of the target.</typeparam>
        /// <param name="query">      The query to act on.</param>
        /// <param name="parameter">  The parameter to filter by.</param>
        /// <param name="includeNull">If set, match the parameter even if it's null.</param>
        /// <returns>The <see cref="IQueryable{TEntity}" /> with an additional Where applied if the parameter has a value.</returns>
        public static IQueryable<TEntity> FilterSalesItemBasesByMatchUnitSoldPrice<TEntity, TDiscount, TTarget>(
                this IQueryable<TEntity> query,
                decimal? parameter,
                bool? includeNull)
            where TEntity : class, ISalesItemBase<TEntity, TDiscount, TTarget>
            where TDiscount : IAppliedDiscountBase<TEntity, TDiscount>
            where TTarget : ISalesItemTargetBase
        {
            if (!parameter.HasValue || includeNull != true)
            {
                return query;
            }
            return Contract.RequiresNotNull(query)
                .Where(x => x.UnitSoldPrice == parameter);
        }
        #endregion

        #region UnitSoldPriceInSellingCurrency
        /// <summary>An <see cref="IQueryable{TEntity}" /> extension method that filters records by the parameter (a
        /// minimum decimal value).</summary>
        /// <remarks>Pass a parameter value of null to not filter by this in a fluid call chain.</remarks>
        /// <typeparam name="TEntity">  Type of the entity.</typeparam>
        /// <typeparam name="TDiscount">Type of the discount.</typeparam>
        /// <typeparam name="TTarget">  Type of the target.</typeparam>
        /// <param name="query">    The query to act on.</param>
        /// <param name="parameter">The parameter to filter by.</param>
        /// <returns>The <see cref="IQueryable{TEntity}" /> with an additional Where applied if the parameter has a value.</returns>
        public static IQueryable<TEntity> FilterSalesItemBasesByMinUnitSoldPriceInSellingCurrency<TEntity, TDiscount, TTarget>(
                this IQueryable<TEntity> query,
                decimal? parameter)
            where TEntity : class, ISalesItemBase<TEntity, TDiscount, TTarget>
            where TDiscount : IAppliedDiscountBase<TEntity, TDiscount>
            where TTarget : ISalesItemTargetBase
        {
            if (!parameter.HasValue)
            {
                return query;
            }
            var search = parameter.Value;
            return Contract.RequiresNotNull(query)
                .Where(x => x.UnitSoldPriceInSellingCurrency >= search);
        }

        /// <summary>An <see cref="IQueryable{TEntity}" /> extension method that filters records by the parameter (a
        /// maximum decimal value).</summary>
        /// <remarks>Pass a parameter value of null to not filter by this in a fluid call chain.</remarks>
        /// <typeparam name="TEntity">  Type of the entity.</typeparam>
        /// <typeparam name="TDiscount">Type of the discount.</typeparam>
        /// <typeparam name="TTarget">  Type of the target.</typeparam>
        /// <param name="query">    The query to act on.</param>
        /// <param name="parameter">The parameter to filter by.</param>
        /// <returns>The <see cref="IQueryable{TEntity}" /> with an additional Where applied if the parameter has a value.</returns>
        public static IQueryable<TEntity> FilterSalesItemBasesByMaxUnitSoldPriceInSellingCurrency<TEntity, TDiscount, TTarget>(
                this IQueryable<TEntity> query,
                decimal? parameter)
            where TEntity : class, ISalesItemBase<TEntity, TDiscount, TTarget>
            where TDiscount : IAppliedDiscountBase<TEntity, TDiscount>
            where TTarget : ISalesItemTargetBase
        {
            if (!parameter.HasValue)
            {
                return query;
            }
            return Contract.RequiresNotNull(query)
                .Where(x => x.UnitSoldPriceInSellingCurrency <= parameter.Value);
        }

        /// <summary>An <see cref="IQueryable{TEntity}" /> extension method that filters records by the parameter (a
        /// decimal value to match exactly).</summary>
        /// <remarks>Pass a parameter value of null to not filter by this in a fluid call chain.</remarks>
        /// <typeparam name="TEntity">  Type of the entity.</typeparam>
        /// <typeparam name="TDiscount">Type of the discount.</typeparam>
        /// <typeparam name="TTarget">  Type of the target.</typeparam>
        /// <param name="query">      The query to act on.</param>
        /// <param name="parameter">  The parameter to filter by.</param>
        /// <param name="includeNull">If set, match the parameter even if it's null.</param>
        /// <returns>The <see cref="IQueryable{TEntity}" /> with an additional Where applied if the parameter has a value.</returns>
        public static IQueryable<TEntity> FilterSalesItemBasesByMatchUnitSoldPriceInSellingCurrency<TEntity, TDiscount, TTarget>(
                this IQueryable<TEntity> query,
                decimal? parameter,
                bool? includeNull)
            where TEntity : class, ISalesItemBase<TEntity, TDiscount, TTarget>
            where TDiscount : IAppliedDiscountBase<TEntity, TDiscount>
            where TTarget : ISalesItemTargetBase
        {
            if (!parameter.HasValue || includeNull != true)
            {
                return query;
            }
            return Contract.RequiresNotNull(query)
                .Where(x => x.UnitSoldPriceInSellingCurrency == parameter);
        }
        #endregion

        #region UserID
        /// <summary>An <see cref="IQueryable{TEntity}" /> extension method that filters records by the parameter (an
        /// identifier).</summary>
        /// <remarks>Pass a parameter value of null to not filter by this in a fluid call chain.</remarks>
        /// <typeparam name="TEntity">  Type of the entity.</typeparam>
        /// <typeparam name="TDiscount">Type of the discount.</typeparam>
        /// <typeparam name="TTarget">  Type of the target.</typeparam>
        /// <param name="query">      The query to act on.</param>
        /// <param name="parameter">  The parameter to filter by.</param>
        /// <param name="includeNull">If set, match the parameter even if it's null.</param>
        /// <returns>The <see cref="IQueryable{TEntity}" /> with an additional Where applied if the parameter has a value.</returns>
        public static IQueryable<TEntity> FilterSalesItemBasesByUserID<TEntity, TDiscount, TTarget>(
                this IQueryable<TEntity> query,
                int? parameter,
                bool? includeNull)
            where TEntity : class, ISalesItemBase<TEntity, TDiscount, TTarget>
            where TDiscount : IAppliedDiscountBase<TEntity, TDiscount>
            where TTarget : ISalesItemTargetBase
        {
            if (Contract.CheckInvalidID(parameter) || includeNull != true)
            {
                return query;
            }
            return Contract.RequiresNotNull(query)
                .Where(x => x.UserID == parameter);
        }
        #endregion

        #region ForceUniqueLineItemKey
        /// <summary>An <see cref="IQueryable{TEntity}" /> extension method that filters records by the parameter (an
        /// identifier).</summary>
        /// <remarks>Pass a parameter value of null to not filter by this in a fluid call chain.</remarks>
        /// <typeparam name="TEntity">  Type of the entity.</typeparam>
        /// <typeparam name="TDiscount">Type of the discount.</typeparam>
        /// <typeparam name="TTarget">  Type of the target.</typeparam>
        /// <param name="query">      The query to act on.</param>
        /// <param name="parameter">  The parameter to filter by.</param>
        /// <param name="strict">     If set, must match the string? exactly (false or null will use a Contains/LIKE).</param>
        /// <param name="includeNull">If set, match the parameter even if it's null.</param>
        /// <returns>The <see cref="IQueryable{TEntity}" /> with an additional Where applied if the parameter has a value.</returns>
        public static IQueryable<TEntity> FilterSalesItemBasesByForceUniqueLineItemKey<TEntity, TDiscount, TTarget>(
                this IQueryable<TEntity> query,
                string? parameter,
                bool? strict,
                bool? includeNull)
            where TEntity : class, ISalesItemBase<TEntity, TDiscount, TTarget>
            where TDiscount : IAppliedDiscountBase<TEntity, TDiscount>
            where TTarget : ISalesItemTargetBase
        {
            if (includeNull != true && !Contract.CheckValidKey(parameter))
            {
                return query;
            }
            var search = parameter?.Trim();
            if (strict == true)
            {
                return Contract.RequiresNotNull(query)
                    .Where(x => x.ForceUniqueLineItemKey == search);
            }
            if (includeNull == true && !Contract.CheckValidKey(search))
            {
                return Contract.RequiresNotNull(query)
                    .Where(x => x.ForceUniqueLineItemKey == null || x.ForceUniqueLineItemKey == string.Empty);
            }
            search = search?.Trim().ToLower();
            return Contract.RequiresNotNull(query)
                .Where(x => x.ForceUniqueLineItemKey != null && x.ForceUniqueLineItemKey.Contains(search!));
        }
        #endregion

        #region Sku
        /// <summary>An <see cref="IQueryable{TEntity}" /> extension method that filters records by the parameter (an
        /// identifier).</summary>
        /// <remarks>Pass a parameter value of null to not filter by this in a fluid call chain.</remarks>
        /// <typeparam name="TEntity">  Type of the entity.</typeparam>
        /// <typeparam name="TDiscount">Type of the discount.</typeparam>
        /// <typeparam name="TTarget">  Type of the target.</typeparam>
        /// <param name="query">      The query to act on.</param>
        /// <param name="parameter">  The parameter to filter by.</param>
        /// <param name="strict">     If set, must match the string? exactly (false or null will use a Contains/LIKE).</param>
        /// <param name="includeNull">If set, match the parameter even if it's null.</param>
        /// <returns>The <see cref="IQueryable{TEntity}" /> with an additional Where applied if the parameter has a value.</returns>
        public static IQueryable<TEntity> FilterSalesItemBasesBySku<TEntity, TDiscount, TTarget>(
                this IQueryable<TEntity> query,
                string? parameter,
                bool? strict,
                bool? includeNull)
            where TEntity : class, ISalesItemBase<TEntity, TDiscount, TTarget>
            where TDiscount : IAppliedDiscountBase<TEntity, TDiscount>
            where TTarget : ISalesItemTargetBase
        {
            if (includeNull != true && !Contract.CheckValidKey(parameter))
            {
                return query;
            }
            var search = parameter?.Trim();
            if (strict == true)
            {
                return Contract.RequiresNotNull(query)
                    .Where(x => x.Sku == search);
            }
            if (includeNull == true && !Contract.CheckValidKey(search))
            {
                return Contract.RequiresNotNull(query)
                    .Where(x => x.Sku == null || x.Sku == string.Empty);
            }
            search = search?.ToLower();
            return Contract.RequiresNotNull(query)
                .Where(x => x.Sku != null && x.Sku.Contains(search!));
        }
        #endregion

        #region UnitOfMeasure
        /// <summary>An <see cref="IQueryable{TEntity}" /> extension method that filters records by the parameter (an
        /// identifier).</summary>
        /// <remarks>Pass a parameter value of null to not filter by this in a fluid call chain.</remarks>
        /// <typeparam name="TEntity">  Type of the entity.</typeparam>
        /// <typeparam name="TDiscount">Type of the discount.</typeparam>
        /// <typeparam name="TTarget">  Type of the target.</typeparam>
        /// <param name="query">      The query to act on.</param>
        /// <param name="parameter">  The parameter to filter by.</param>
        /// <param name="strict">     If set, must match the string? exactly (false or null will use a Contains/LIKE).</param>
        /// <param name="includeNull">If set, match the parameter even if it's null.</param>
        /// <returns>The <see cref="IQueryable{TEntity}" /> with an additional Where applied if the parameter has a value.</returns>
        public static IQueryable<TEntity> FilterSalesItemBasesByUnitOfMeasure<TEntity, TDiscount, TTarget>(
                this IQueryable<TEntity> query,
                string? parameter,
                bool? strict,
                bool? includeNull)
            where TEntity : class, ISalesItemBase<TEntity, TDiscount, TTarget>
            where TDiscount : IAppliedDiscountBase<TEntity, TDiscount>
            where TTarget : ISalesItemTargetBase
        {
            if (includeNull != true && !Contract.CheckValidKey(parameter))
            {
                return query;
            }
            var search = parameter?.Trim();
            if (strict == true)
            {
                return Contract.RequiresNotNull(query)
                    .Where(x => x.UnitOfMeasure == search);
            }
            if (includeNull == true && !Contract.CheckValidKey(search))
            {
                return Contract.RequiresNotNull(query)
                    .Where(x => x.UnitOfMeasure == null || x.UnitOfMeasure == string.Empty);
            }
            search = search?.ToLower();
            return Contract.RequiresNotNull(query)
                .Where(x => x.UnitOfMeasure != null && x.UnitOfMeasure.Contains(search!));
        }
        #endregion
    }
}
