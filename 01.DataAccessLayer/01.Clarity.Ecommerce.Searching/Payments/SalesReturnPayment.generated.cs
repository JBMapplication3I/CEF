// <autogenerated>
// <copyright file="Payments.ISearchModels.cs" company="clarity-ventures.com">
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

    /// <content>The Sales Return Payment SQL search extensions.</content>
    public static partial class SalesReturnPaymentSQLSearchExtensions
    {
        /// <summary>An <see cref="IQueryable{SalesReturnPayment}" /> extension method that filters  by each of the properties of
        /// the search model which have been set.</summary>
        /// <remarks>Pass a parameter value of null to not filter by this in a fluid call chain.</remarks>
        /// <param name="query">The query to act on.</param>
        /// <param name="model">The search model to filter by.</param>
        /// <returns>The <see cref="IQueryable{SalesReturnPayment}" /> with an additional Where applied if the parameter has a value.</returns>
        public static IQueryable<SalesReturnPayment> FilterSalesReturnPaymentsBySearchModel(
                this IQueryable<SalesReturnPayment> query,
                ISalesReturnPaymentSearchModel model)
        {
            if (model == null)
            {
                return query;
            }
            var query2 = Contract.RequiresNotNull(query)
                .FilterByBaseSearchModel(model)
                .FilterByIAmARelationshipTableBaseSearchModel<SalesReturnPayment, SalesReturn, Payment>(model)
                ;
            return query2;
        }
    }
}
