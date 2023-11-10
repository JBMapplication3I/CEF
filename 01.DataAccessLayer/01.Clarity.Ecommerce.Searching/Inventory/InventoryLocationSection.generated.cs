// <autogenerated>
// <copyright file="Inventory.ISearchModels.cs" company="clarity-ventures.com">
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

    /// <content>The Inventory Location Section SQL search extensions.</content>
    public static partial class InventoryLocationSectionSQLSearchExtensions
    {
        /// <summary>An <see cref="IQueryable{InventoryLocationSection}" /> extension method that filters  by each of the properties of
        /// the search model which have been set.</summary>
        /// <remarks>Pass a parameter value of null to not filter by this in a fluid call chain.</remarks>
        /// <param name="query">The query to act on.</param>
        /// <param name="model">The search model to filter by.</param>
        /// <returns>The <see cref="IQueryable{InventoryLocationSection}" /> with an additional Where applied if the parameter has a value.</returns>
        public static IQueryable<InventoryLocationSection> FilterInventoryLocationSectionsBySearchModel(
                this IQueryable<InventoryLocationSection> query,
                IInventoryLocationSectionSearchModel model)
        {
            if (model == null)
            {
                return query;
            }
            var query2 = Contract.RequiresNotNull(query)
                .FilterByNameableBaseSearchModel(model)
                .FilterInventoryLocationSectionsByInventoryLocationID(model.InventoryLocationID)
                ;
            return query2;
        }

        #region InventoryLocationID
        /// <summary>An <see cref="IQueryable{TEntity}" /> extension method that filters records by the parameter (an identifier).</summary>
        /// <remarks>Pass a parameter value of null to not filter by this in a fluid call chain.</remarks>
        /// <typeparam name="TEntity">Type of the entity.</typeparam>
        /// <param name="query">      The query to act on.</param>
        /// <param name="parameter">  The parameter to filter by.</param>
        /// <returns>The <see cref="IQueryable{TEntity}" /> with an additional Where applied if the parameter has a value.</returns>
        public static IQueryable<TEntity> FilterInventoryLocationSectionsByInventoryLocationID<TEntity>(
                this IQueryable<TEntity> query,
                int? parameter)
            where TEntity : class, IInventoryLocationSection
        {
            if (Contract.CheckInvalidID(parameter))
            {
                return query;
            }
            return Contract.RequiresNotNull(query)
                .Where(x => x.InventoryLocationID == parameter);
        }
        #endregion
    }
}
