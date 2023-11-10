// <autogenerated>
// <copyright file="Auctions.ISearchModels.cs" company="clarity-ventures.com">
// Copyright (c) 2021-2023 clarity-ventures.com. All rights reserved.
// </copyright>
// <summary>Implements the FilterBy Query Extensions generated to provide searching queries.</summary>
// <remarks>This file was auto-generated by FilterBys.tt, changes to this
// file will be overwritten automatically when the T4 template is run again.</remarks>
// </autogenerated>
// ReSharper disable PartialTypeWithSinglePart, RedundantUsingDirective, RegionWithSingleElement
#pragma warning disable 8669 // nullable reference types disabled
#nullable enable
namespace Clarity.Ecommerce.Interfaces.Models
{
    using System;
    using System.Linq;
    using DataModel;
    using Ecommerce.DataModel;
    using Utilities;

    /// <content>The Bid SQL search extensions.</content>
    public static partial class BidSQLSearchExtensions
    {
        /// <summary>An <see cref="IQueryable{Bid}" /> extension method that filters  by each of the properties of
        /// the search model which have been set.</summary>
        /// <remarks>Pass a parameter value of null to not filter by this in a fluid call chain.</remarks>
        /// <param name="query">The query to act on.</param>
        /// <param name="model">The search model to filter by.</param>
        /// <returns>The <see cref="IQueryable{Bid}" /> with an additional Where applied if the parameter has a value.</returns>
        public static IQueryable<Bid> FilterBidsBySearchModel(
                this IQueryable<Bid> query,
                IBidSearchModel model)
        {
            if (model == null)
            {
                return query;
            }
            var query2 = Contract.RequiresNotNull(query)
                .FilterByBaseSearchModel(model)
                .FilterByIAmFilterableByUserSearchModel(model)
                .FilterByHaveAStatusSearchModel<Bid, BidStatus>(model)
                .FilterBidsByWon(model.Won)
                .FilterBidsByMinCurrentBid(model.MinCurrentBid)
                .FilterBidsByMaxCurrentBid(model.MaxCurrentBid)
                .FilterBidsByMatchCurrentBid(model.MatchCurrentBid, model.MatchCurrentBidIncludeNull)
                .FilterBidsByLotID(model.LotID, model.LotIDIncludeNull)
                .FilterBidsByMinMaxBid(model.MinMaxBid)
                .FilterBidsByMaxMaxBid(model.MaxMaxBid)
                .FilterBidsByMatchMaxBid(model.MatchMaxBid, model.MatchMaxBidIncludeNull)
                ;
            return query2;
        }

        #region Won
        /// <summary>An <see cref="IQueryable{TEntity}" /> extension method that filters records by the parameter (a boolean value).</summary>
        /// <remarks>Pass a parameter value of null to not filter by this in a fluid call chain.</remarks>
        /// <typeparam name="TEntity">Type of the entity.</typeparam>
        /// <param name="query">      The query to act on.</param>
        /// <param name="parameter">  The parameter to filter by.</param>
        /// <returns>The <see cref="IQueryable{TEntity}" /> with an additional Where applied if the parameter has a value.</returns>
        public static IQueryable<TEntity> FilterBidsByWon<TEntity>(
                this IQueryable<TEntity> query,
                bool? parameter)
            where TEntity : class, IBid
        {
            if (!parameter.HasValue)
            {
                return query;
            }
            return Contract.RequiresNotNull(query)
                .Where(x => x.Won == parameter);
        }
        #endregion

        #region CurrentBid
        /// <summary>An <see cref="IQueryable{TEntity}" /> extension method that filters records by the parameter (a minimum decimal value).</summary>
        /// <remarks>Pass a parameter value of null to not filter by this in a fluid call chain.</remarks>
        /// <typeparam name="TEntity">Type of the entity.</typeparam>
        /// <param name="query">    The query to act on.</param>
        /// <param name="parameter">The parameter to filter by.</param>
        /// <returns>The <see cref="IQueryable{TEntity}" /> with an additional Where applied if the parameter has a value.</returns>
        public static IQueryable<TEntity> FilterBidsByMinCurrentBid<TEntity>(
                this IQueryable<TEntity> query,
                decimal? parameter)
            where TEntity : class, IBid
        {
            if (!parameter.HasValue)
            {
                return query;
            }
            var search = parameter.Value;
            return Contract.RequiresNotNull(query)
                .Where(x => x.CurrentBid >= search);
        }

        /// <summary>An <see cref="IQueryable{TEntity}" /> extension method that filters records by the parameter (a maximum decimal value).</summary>
        /// <remarks>Pass a parameter value of null to not filter by this in a fluid call chain.</remarks>
        /// <typeparam name="TEntity">Type of the entity.</typeparam>
        /// <param name="query">    The query to act on.</param>
        /// <param name="parameter">The parameter to filter by.</param>
        /// <returns>The <see cref="IQueryable{TEntity}" /> with an additional Where applied if the parameter has a value.</returns>
        public static IQueryable<TEntity> FilterBidsByMaxCurrentBid<TEntity>(
                this IQueryable<TEntity> query,
                decimal? parameter)
            where TEntity : class, IBid
        {
            if (!parameter.HasValue)
            {
                return query;
            }
            return Contract.RequiresNotNull(query)
                .Where(x => x.CurrentBid <= parameter.Value);
        }

        /// <summary>An <see cref="IQueryable{TEntity}" /> extension method that filters records by the parameter (a decimal value to match exactly).</summary>
        /// <remarks>Pass a parameter value of null to not filter by this in a fluid call chain.</remarks>
        /// <typeparam name="TEntity">Type of the entity.</typeparam>
        /// <param name="query">      The query to act on.</param>
        /// <param name="parameter">  The parameter to filter by.</param>
        /// <param name="includeNull">If set, match the parameter even if it's null.</param>
        /// <returns>The <see cref="IQueryable{TEntity}" /> with an additional Where applied if the parameter has a value.</returns>
        public static IQueryable<TEntity> FilterBidsByMatchCurrentBid<TEntity>(
                this IQueryable<TEntity> query,
                decimal? parameter,
                bool? includeNull)
            where TEntity : class, IBid
        {
            if (includeNull != true && !parameter.HasValue)
            {
                return query;
            }
            return Contract.RequiresNotNull(query)
                .Where(x => x.CurrentBid == parameter);
        }
        #endregion

        #region LotID
        /// <summary>An <see cref="IQueryable{TEntity}" /> extension method that filters records by the parameter (an identifier).</summary>
        /// <remarks>Pass a parameter value of null to not filter by this in a fluid call chain.</remarks>
        /// <typeparam name="TEntity">Type of the entity.</typeparam>
        /// <param name="query">      The query to act on.</param>
        /// <param name="parameter">  The parameter to filter by.</param>
        /// <param name="includeNull">If set, match the parameter even if it's null.</param>
        /// <returns>The <see cref="IQueryable{TEntity}" /> with an additional Where applied if the parameter has a value.</returns>
        public static IQueryable<TEntity> FilterBidsByLotID<TEntity>(
                this IQueryable<TEntity> query,
                int? parameter,
                bool? includeNull)
            where TEntity : class, IBid
        {
            if (includeNull != true && Contract.CheckInvalidID(parameter))
            {
                return query;
            }
            return Contract.RequiresNotNull(query)
                .Where(x => x.LotID == parameter);
        }
        #endregion

        #region MaxBid
        /// <summary>An <see cref="IQueryable{TEntity}" /> extension method that filters records by the parameter (a minimum decimal value).</summary>
        /// <remarks>Pass a parameter value of null to not filter by this in a fluid call chain.</remarks>
        /// <typeparam name="TEntity">Type of the entity.</typeparam>
        /// <param name="query">    The query to act on.</param>
        /// <param name="parameter">The parameter to filter by.</param>
        /// <returns>The <see cref="IQueryable{TEntity}" /> with an additional Where applied if the parameter has a value.</returns>
        public static IQueryable<TEntity> FilterBidsByMinMaxBid<TEntity>(
                this IQueryable<TEntity> query,
                decimal? parameter)
            where TEntity : class, IBid
        {
            if (!parameter.HasValue)
            {
                return query;
            }
            var search = parameter.Value;
            return Contract.RequiresNotNull(query)
                .Where(x => x.MaxBid >= search);
        }

        /// <summary>An <see cref="IQueryable{TEntity}" /> extension method that filters records by the parameter (a maximum decimal value).</summary>
        /// <remarks>Pass a parameter value of null to not filter by this in a fluid call chain.</remarks>
        /// <typeparam name="TEntity">Type of the entity.</typeparam>
        /// <param name="query">    The query to act on.</param>
        /// <param name="parameter">The parameter to filter by.</param>
        /// <returns>The <see cref="IQueryable{TEntity}" /> with an additional Where applied if the parameter has a value.</returns>
        public static IQueryable<TEntity> FilterBidsByMaxMaxBid<TEntity>(
                this IQueryable<TEntity> query,
                decimal? parameter)
            where TEntity : class, IBid
        {
            if (!parameter.HasValue)
            {
                return query;
            }
            return Contract.RequiresNotNull(query)
                .Where(x => x.MaxBid <= parameter.Value);
        }

        /// <summary>An <see cref="IQueryable{TEntity}" /> extension method that filters records by the parameter (a decimal value to match exactly).</summary>
        /// <remarks>Pass a parameter value of null to not filter by this in a fluid call chain.</remarks>
        /// <typeparam name="TEntity">Type of the entity.</typeparam>
        /// <param name="query">      The query to act on.</param>
        /// <param name="parameter">  The parameter to filter by.</param>
        /// <param name="includeNull">If set, match the parameter even if it's null.</param>
        /// <returns>The <see cref="IQueryable{TEntity}" /> with an additional Where applied if the parameter has a value.</returns>
        public static IQueryable<TEntity> FilterBidsByMatchMaxBid<TEntity>(
                this IQueryable<TEntity> query,
                decimal? parameter,
                bool? includeNull)
            where TEntity : class, IBid
        {
            if (includeNull != true && !parameter.HasValue)
            {
                return query;
            }
            return Contract.RequiresNotNull(query)
                .Where(x => x.MaxBid == parameter);
        }
        #endregion
    }
}
