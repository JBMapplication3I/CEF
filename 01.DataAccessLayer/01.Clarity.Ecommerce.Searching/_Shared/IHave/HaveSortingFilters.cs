// <copyright file="HaveSortingFilters.cs" company="clarity-ventures.com">
// Copyright (c) 2016-2023 clarity-ventures.com. All rights reserved.
// </copyright>
// <summary>Implements the have sorting filters class</summary>
namespace Clarity.Ecommerce.Interfaces.Models
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Linq.Expressions;
    using System.Reflection;
    using DataModel;
    using Utilities;

    /// <summary>A have sorting filters.</summary>
    public static class HaveSortingFilters
    {
        private static Dictionary<string, MethodInfo> StaticOrderingMethods { get; } = new Dictionary<string, MethodInfo>
        {
            ["OrderBy"] = typeof(Queryable).GetMethods().Single(method => method.Name == "OrderBy" && method.IsGenericMethodDefinition && method.GetGenericArguments().Length == 2 && method.GetParameters().Length == 2),
            ["OrderByDescending"] = typeof(Queryable).GetMethods().Single(method => method.Name == "OrderByDescending" && method.IsGenericMethodDefinition && method.GetGenericArguments().Length == 2 && method.GetParameters().Length == 2),
            ["ThenBy"] = typeof(Queryable).GetMethods().Single(method => method.Name == "ThenBy" && method.IsGenericMethodDefinition && method.GetGenericArguments().Length == 2 && method.GetParameters().Length == 2),
            ["ThenByDescending"] = typeof(Queryable).GetMethods().Single(method => method.Name == "ThenByDescending" && method.IsGenericMethodDefinition && method.GetGenericArguments().Length == 2 && method.GetParameters().Length == 2),
        };

        /// <summary>An IQueryable{TEntity} extension method that applies the sorting.</summary>
        /// <typeparam name="TEntity">Type of the entity.</typeparam>
        /// <param name="query">             The query to act on.</param>
        /// <param name="sorts">             The sorts.</param>
        /// <param name="groupings">         The groupings.</param>
        /// <param name="contextProfileName">Name of the context profile.</param>
        /// <returns>An <see cref="IQueryable{TEntity}"/>.</returns>
        public static IQueryable<TEntity> ApplySorting<TEntity>(
                this IQueryable<TEntity> query,
                Sort[]? sorts,
                Grouping[]? groupings,
                string? contextProfileName)
            where TEntity : class, IBase
        {
            if (Contract.CheckEmpty(sorts) && Contract.CheckEmpty(groupings))
            {
                // Always provide at least some kind of sort so Paging works
                return Contract.RequiresNotNull(query)
                    .OrderBy(OrderByID<TEntity>());
            }
            var isFirst = true;
            var fullSorts = new List<Sort>();
            var processedFields = new List<string>();
            if (Contract.CheckNotEmpty(groupings))
            {
                fullSorts.AddRange(groupings!.Select(x => new Sort
                {
                    Field = x.Field,
                    Dir = x.Dir,
                    Order = x.Order ?? x.SortOrder ?? int.MinValue,
                }));
                processedFields.AddRange(fullSorts.Select(x => x.Field)!);
            }
            if (Contract.CheckNotEmpty(sorts))
            {
                // Sorts must always come after groups when set
                var highest = fullSorts
                    .Select(x => x.Order)
                    .Where(x => x.HasValue)
                    .DefaultIfEmpty(0)
                    .Max();
                if (highest < 0)
                {
                    highest = 0;
                }
                fullSorts.AddRange(sorts!.Where(x => !processedFields.Contains(x.Field!)).Select(x => new Sort
                {
                    Field = x.Field,
                    Dir = x.Dir,
                    Order = (x.Order ?? 0) + highest + 1,
                }));
            }
            foreach (var sort in fullSorts.Where(x => Contract.CheckValidKey(x.Field)).OrderBy(x => x.Order))
            {
                try
                {
                    query = Contract.RequiresNotNull(query)
                        .OrderByPropertyName(
                            sort.Field!,
                            sort.Dir == Enums.SortDirection.desc.ToString(),
                            !isFirst);
                    if (isFirst)
                    {
                        isFirst = false;
                    }
                }
                catch (Exception ex)
                {
                    RegistryLoaderWrapper.GetInstance<ILogger>(contextProfileName).LogError(
                        name: $"{nameof(HaveSortingFilters)}.Error",
                        message: ex.Message,
                        ex: ex,
                        contextProfileName: contextProfileName);
                }
            }
            return query;
        }

        private static IQueryable<TEntity> OrderByPropertyName<TEntity>(
            this IQueryable<TEntity> query,
            string? propertyName,
            bool isDescending,
            bool isThenBy)
        {
            return Contract.RequiresValidKey(propertyName).Contains(".")
                ? NestedSortResults(
                    Contract.RequiresNotNull(query),
                    Contract.RequiresValidKey(propertyName),
                    isDescending,
                    isThenBy,
                    typeof(TEntity))
                : SortResults(
                    Contract.RequiresNotNull(query),
                    Contract.RequiresValidKey(propertyName),
                    isDescending,
                    isThenBy,
                    typeof(TEntity));
        }

        private static IQueryable<TEntity> SortResults<TEntity>(
            IQueryable<TEntity> query,
            string? propertyName,
            bool isDescending,
            bool isThenBy,
            Type coreEntityType)
        {
            var parameter = Expression.Parameter(coreEntityType, "x");
            var propertyInfo = coreEntityType.GetProperty(propertyName!);
            // ReSharper disable once InvertIf
            if (propertyInfo == null)
            {
                var split = Contract.RequiresValidKey(propertyName).SplitCamelCase().Split(' ');
                if (split.Length > 1)
                {
                    return NestedSortResults(query, split.Aggregate((c, n) => $"{c}.{n}"), isDescending, isThenBy, typeof(TEntity));
                }
                // TODO: Aggregate issues on to a list (like in CEFAR)
                // throw new InvalidOperationException($"Property '{propertyName}' does not exist on type '{coreEntityType.Name}'");
                return query;
            }
            return (IQueryable<TEntity>)StaticOrderingMethods[(isThenBy ? "ThenBy" : "OrderBy") + (isDescending ? "Descending" : string.Empty)]
                .MakeGenericMethod(typeof(TEntity), propertyInfo.PropertyType)
                .Invoke(
                    null,
                    new object[] { query, Expression.Lambda(Expression.Property(parameter, propertyInfo), parameter) })!;
        }

        private static IQueryable<TEntity> NestedSortResults<TEntity>(
            IQueryable<TEntity> query,
            string? propertyName,
            bool isDescending,
            bool isThenBy,
            Type coreEntityType)
        {
            var initialPoints = Contract.RequiresValidKey(propertyName)
                .Split(new[] { '.' }, StringSplitOptions.RemoveEmptyEntries)
                .Select(x => x.Trim())
                .ToList();
            initialPoints.Insert(0, "root");
            var points = initialPoints.ToArray();
            var pathPointType = new Type[points.Length + 1];
            pathPointType[0] = coreEntityType;
            for (var i = 1; i < points.Length; i++)
            {
                var pathPointPropertyInfo = pathPointType[i - 1].GetProperty(points[i])
                    ?? throw new InvalidOperationException(
                        $"Property '{points[i]}' does not exist on type '{pathPointType[i - 1].Name}', orig path: '{propertyName}'");
                pathPointType[i] = pathPointPropertyInfo.PropertyType;
            }
            var parameter = Expression.Parameter(coreEntityType, "x");
            return (IQueryable<TEntity>)StaticOrderingMethods[(isThenBy ? "ThenBy" : "OrderBy") + (isDescending ? "Descending" : string.Empty)]
                .MakeGenericMethod(typeof(TEntity), pathPointType[points.Length - 1])
                .Invoke(
                    null,
                    new object[] { query, Expression.Lambda(points.Skip(1).Aggregate<string, Expression>(parameter, Expression.PropertyOrField), parameter) })!;
        }

        private static Expression<Func<TEntity, int>> OrderByID<TEntity>()
            where TEntity : class, IBase
        {
            return x => x.ID;
        }
    }
}
