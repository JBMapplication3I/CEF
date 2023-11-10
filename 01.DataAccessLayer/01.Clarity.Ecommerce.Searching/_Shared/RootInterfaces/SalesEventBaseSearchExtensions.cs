// <copyright file="SalesEventBaseSearchExtensions.cs" company="clarity-ventures.com">
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
    public static class SalesEventBaseSearchExtensions
    {
        /// <summary>An <see cref="IQueryable{TEntity}" /> extension method that filters  by each of the properties of
        /// the search model which have been set.</summary>
        /// <remarks>Pass a parameter value of null to not filter by this in a fluid call chain.</remarks>
        /// <typeparam name="TEntity">Type of the entity.</typeparam>
        /// <typeparam name="TMaster">Type of the entity's master.</typeparam>
        /// <typeparam name="TType">  Type of the entity's type.</typeparam>
        /// <param name="query">The query to act on.</param>
        /// <param name="model">The search model to filter by.</param>
        /// <returns>The <see cref="IQueryable{TEntity}" /> with an additional Where applied if the parameter has a value.</returns>
        public static IQueryable<TEntity> FilterBySalesEventBaseSearchModel<TEntity, TMaster, TType>(
                this IQueryable<TEntity> query,
                ISalesEventBaseSearchModel model)
            where TEntity : class, ISalesEventBase<TMaster, TType>
            where TMaster : class, IBase
            where TType : class, ITypableBase
        {
            if (model == null)
            {
                return query;
            }
            return Contract.RequiresNotNull(query)
                .FilterSalesEventsByMasterActive<TEntity, TMaster, TType>()
                .FilterSalesEventBasesByMasterID<TEntity, TMaster, TType>(model.MasterID);
        }

        /// <summary>An IQueryable{TEntity} extension method that filter sales items by master active.</summary>
        /// <typeparam name="TEntity">Type of the entity.</typeparam>
        /// <typeparam name="TMaster">Type of the entity's master.</typeparam>
        /// <typeparam name="TType">  Type of the entity's type.</typeparam>
        /// <param name="query">The query to act on.</param>
        /// <returns>An <see cref="IQueryable{TEntity}"/>.</returns>
        public static IQueryable<TEntity> FilterSalesEventsByMasterActive<TEntity, TMaster, TType>(
                this IQueryable<TEntity> query)
            where TEntity : class, ISalesEventBase<TMaster, TType>
            where TMaster : class, IBase
            where TType : class, ITypableBase
        {
            return Contract.RequiresNotNull(query)
                .Where(x => x.Master!.Active);
        }

        /// <summary>An <see cref="IQueryable{TEntity}" /> extension method that filters records by the parameter (an
        /// identifier).</summary>
        /// <remarks>Pass a parameter value of null to not filter by this in a fluid call chain.</remarks>
        /// <typeparam name="TEntity">Type of the entity.</typeparam>
        /// <typeparam name="TMaster">Type of the entity's master.</typeparam>
        /// <typeparam name="TType">  Type of the entity's type.</typeparam>
        /// <param name="query">    The query to act on.</param>
        /// <param name="parameter">The parameter to filter by.</param>
        /// <returns>The <see cref="IQueryable{TEntity}" /> with an additional Where applied if the parameter has a value.</returns>
        public static IQueryable<TEntity> FilterSalesEventBasesByMasterID<TEntity, TMaster, TType>(
                this IQueryable<TEntity> query,
                int? parameter)
            where TEntity : class, ISalesEventBase<TMaster, TType>
            where TMaster : class, IBase
            where TType : class, ITypableBase
        {
            if (Contract.CheckInvalidID(parameter))
            {
                return query;
            }
            return Contract.RequiresNotNull(query)
                .Where(x => x.MasterID == parameter);
        }
    }
}
