// <autogenerated>
// <copyright file="Franchises.ISearchModels.cs" company="clarity-ventures.com">
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

    /// <content>The Franchise District SQL search extensions.</content>
    public static partial class FranchiseDistrictSQLSearchExtensions
    {
        /// <summary>An <see cref="IQueryable{FranchiseDistrict}" /> extension method that filters  by each of the properties of
        /// the search model which have been set.</summary>
        /// <remarks>Pass a parameter value of null to not filter by this in a fluid call chain.</remarks>
        /// <param name="query">The query to act on.</param>
        /// <param name="model">The search model to filter by.</param>
        /// <returns>The <see cref="IQueryable{FranchiseDistrict}" /> with an additional Where applied if the parameter has a value.</returns>
        public static IQueryable<FranchiseDistrict> FilterFranchiseDistrictsBySearchModel(
                this IQueryable<FranchiseDistrict> query,
                IFranchiseDistrictSearchModel model)
        {
            if (model == null)
            {
                return query;
            }
            var query2 = Contract.RequiresNotNull(query)
                .FilterByBaseSearchModel(model)
                .FilterByIAmARelationshipTableBaseSearchModel<FranchiseDistrict, Franchise, District>(model)
                ;
            return query2;
        }
    }
}
