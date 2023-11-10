// <copyright file="BaseSearchExtensions.cs" company="clarity-ventures.com">
// Copyright (c) 2017-2023 clarity-ventures.com. All rights reserved.
// </copyright>
// <summary>Implements the base search extensions class</summary>
#pragma warning disable SA1118 // Parameter should not span multiple lines
namespace Clarity.Ecommerce.Interfaces.Models
{
    using System;
    using System.Collections.Generic;
    using System.Data.Entity;
    using System.Linq;
    using System.Linq.Expressions;
    using System.Threading.Tasks;
    using DataModel;
    using LinqKit;
    using Utilities;

    /// <summary>A base search extensions.</summary>
    public static class BaseSearchExtensions
    {
        /// <summary>An IQueryable{TEntity} extension method that filter by base search model.</summary>
        /// <typeparam name="TEntity">Type of the entity.</typeparam>
        /// <param name="query">The query to act on.</param>
        /// <param name="model">The model.</param>
        /// <returns>An <see cref="IQueryable{TEntity}"/>.</returns>
        public static IQueryable<TEntity> FilterByBaseSearchModel<TEntity>(
                this IQueryable<TEntity> query,
                IBaseSearchModel model)
            where TEntity : class, IBase
        {
            return Contract.RequiresNotNull(query)
                .FilterByID(Contract.RequiresNotNull(model).ID)
                .FilterByExcludedID(model.ExcludedID)
                .FilterByActive(model.Active)
                .FilterByIDs(model.IDs)
                .FilterByExcludedIDs(model.NotIDs)
                .FilterByCustomKey(model.CustomKey, model.CustomKeyStrict ?? false)
                .FilterByCustomKeys(model.CustomKeys, model.CustomKeysStrict ?? false)
                .FilterByIDOrCustomKey(model.IDOrCustomKey, model.CustomKeyStrict ?? false)
                .FilterByModifiedSince(model.ModifiedSince)
                .FilterByUpdatedOrCreatedDate(model.MinUpdatedOrCreatedDate, model.MaxUpdatedOrCreatedDate)
                .FilterObjectsWithJsonAttributesByValues(model.JsonAttributes);
        }

        /// <summary>An <see cref="IQueryable{TEntity}"/> extension method that filter by identifier.</summary>
        /// <exception cref="ArgumentNullException">Thrown when one or more required arguments are null.</exception>
        /// <typeparam name="TEntity">Type of the entity.</typeparam>
        /// <param name="query">The query to act on.</param>
        /// <param name="id">   The identifier.</param>
        /// <returns>An <see cref="IQueryable{TEntity}"/>.</returns>
        public static IQueryable<TEntity> FilterByID<TEntity>(
                this IQueryable<TEntity> query,
                int? id)
            where TEntity : class, IBase
        {
            if (Contract.CheckInvalidID(id))
            {
                return query;
            }
            return Contract.RequiresNotNull(query)
                .Where(x => x.ID == id!.Value);
        }

        /// <summary>An IQueryable{TEntity} extension method that filter by IDs.</summary>
        /// <typeparam name="TEntity">Type of the entity.</typeparam>
        /// <param name="query">The query to act on.</param>
        /// <param name="ids">  The identifiers.</param>
        /// <returns>An <see cref="IQueryable{TEntity}"/>.</returns>
        public static IQueryable<TEntity> FilterByIDs<TEntity>(
                this IQueryable<TEntity> query,
                IEnumerable<int>? ids)
            where TEntity : class, IBase
        {
            var enumerable = ids?.ToList();
            if (enumerable?.Any(x => Contract.CheckValidID(x)) != true)
            {
                return query;
            }
            return Contract.RequiresNotNull(query)
                .AsExpandable()
                .Where(BuildIDsPredicate<TEntity>(enumerable));
        }

        /// <summary>An IQueryable{TEntity} extension method that filter by not IDs.</summary>
        /// <typeparam name="TEntity">Type of the entity.</typeparam>
        /// <param name="query">The query to act on.</param>
        /// <param name="ids">  The identifiers.</param>
        /// <returns>An <see cref="IQueryable{TEntity}"/>.</returns>
        public static IQueryable<TEntity> FilterByExcludedIDs<TEntity>(
                this IQueryable<TEntity> query,
                IEnumerable<int>? ids)
            where TEntity : class, IBase
        {
            var enumerable = ids?.ToList();
            if (enumerable?.Any(x => Contract.CheckValidID(x)) != true)
            {
                return query;
            }
            return Contract.RequiresNotNull(query)
                .AsExpandable()
                .Where(BuildExcludedIDsPredicate<TEntity>(enumerable));
        }

        /// <summary>An <see cref="IQueryable{TEntity}"/> extension method that filter by active.</summary>
        /// <exception cref="ArgumentNullException">Thrown when one or more required arguments are null.</exception>
        /// <typeparam name="TEntity">Type of the entity.</typeparam>
        /// <param name="query">The query to act on.</param>
        /// <param name="active">[Nullable] The active state to look for.</param>
        /// <returns>An <see cref="IQueryable{TEntity}"/>.</returns>
        public static IQueryable<TEntity> FilterByActive<TEntity>(
                this IQueryable<TEntity> query,
                bool? active)
            where TEntity : class, IBase
        {
            if (!active.HasValue)
            {
                return query;
            }
            var search = active.Value;
            return Contract.RequiresNotNull(query)
                .Where(x => x.Active == search);
        }

        /// <summary>An <see cref="IQueryable{TEntity}"/> extension method that filter by custom key.</summary>
        /// <exception cref="ArgumentNullException">Thrown when one or more required arguments are null.</exception>
        /// <typeparam name="TEntity">Type of the entity.</typeparam>
        /// <param name="query">    The query to act on.</param>
        /// <param name="key">The custom key.</param>
        /// <param name="strict">   true to strict.</param>
        /// <returns>An <see cref="IQueryable{TEntity}"/>.</returns>
        public static IQueryable<TEntity> FilterByCustomKey<TEntity>(
                this IQueryable<TEntity> query,
                string? key,
                bool strict = false)
            where TEntity : class, IBase
        {
            if (!Contract.CheckValidKey(key))
            {
                return query;
            }
            var search = key!.Trim();
            if (strict)
            {
                return Contract.RequiresNotNull(query)
                    .Where(x => x.CustomKey != null && x.CustomKey == search);
            }
            search = search.ToLower();
            return Contract.RequiresNotNull(query)
                .Where(x => x.CustomKey!.Contains(search));
        }

        /// <summary>An IQueryable{TEntity} extension method that filter by custom keys.</summary>
        /// <typeparam name="TEntity">Type of the entity.</typeparam>
        /// <param name="query"> The query to act on.</param>
        /// <param name="keys">  The keys.</param>
        /// <param name="strict">True to strict.</param>
        /// <returns>An IQueryable{TEntity}.</returns>
        public static IQueryable<TEntity> FilterByCustomKeys<TEntity>(
                this IQueryable<TEntity> query,
                IEnumerable<string?>? keys,
                bool strict)
            where TEntity : class, IBase
        {
            var enumerable = keys?.ToList();
            if (Contract.CheckEmpty(enumerable))
            {
                return query;
            }
            if (strict)
            {
                return Contract.RequiresNotNull(query)
                    .Where(x => x.CustomKey != null)
                    .AsExpandable()
                    .Where(BuildCustomKeysPredicate<TEntity>(enumerable!, true));
            }
            return Contract.RequiresNotNull(query)
                .AsExpandable()
                .Where(BuildCustomKeysPredicate<TEntity>(enumerable!, false));
        }

        /// <summary>An <see cref="IQueryable{TEntity}"/> extension method that filter by modified since.</summary>
        /// <exception cref="ArgumentNullException">Thrown when one or more required arguments are null.</exception>
        /// <typeparam name="TEntity">Type of the entity.</typeparam>
        /// <param name="query">        The query to act on.</param>
        /// <param name="modifiedSince">The modified since.</param>
        /// <returns>An <see cref="IQueryable{TEntity}"/>.</returns>
        public static IQueryable<TEntity> FilterByModifiedSince<TEntity>(
                this IQueryable<TEntity> query,
                DateTime? modifiedSince)
            where TEntity : class, IBase
        {
            if (!Contract.CheckValidDate(modifiedSince))
            {
                return query;
            }
            return Contract.RequiresNotNull(query)
                .Where(x => x.UpdatedDate >= modifiedSince!.Value
                    || x.CreatedDate >= modifiedSince.Value);
        }

        /// <summary>Filter by updated or created date.</summary>
        /// <typeparam name="TEntity">Type of the entity.</typeparam>
        /// <param name="query">                  The query to act on.</param>
        /// <param name="minUpdatedOrCreatedDate">The minimum updated or created date.</param>
        /// <param name="maxUpdatedOrCreatedDate">The maximum updated or created date.</param>
        /// <returns>An IQueryable{TEntity}.</returns>
        public static IQueryable<TEntity> FilterByUpdatedOrCreatedDate<TEntity>(
                this IQueryable<TEntity> query,
                DateTime? minUpdatedOrCreatedDate,
                DateTime? maxUpdatedOrCreatedDate)
            where TEntity : class, IBase
        {
            if (!Contract.CheckValidDate(minUpdatedOrCreatedDate)
                && !Contract.CheckValidDate(maxUpdatedOrCreatedDate))
            {
                return query;
            }
            if (!Contract.CheckValidDate(minUpdatedOrCreatedDate))
            {
                return Contract.RequiresNotNull(query)
                    .Where(x => x.UpdatedDate != null && x.UpdatedDate <= maxUpdatedOrCreatedDate!.Value
                        || x.CreatedDate <= maxUpdatedOrCreatedDate!.Value);
            }
            if (!Contract.CheckValidDate(maxUpdatedOrCreatedDate))
            {
                return Contract.RequiresNotNull(query)
                    .Where(x => x.UpdatedDate != null && x.UpdatedDate >= minUpdatedOrCreatedDate!.Value
                        || x.CreatedDate >= minUpdatedOrCreatedDate!.Value);
            }
            return Contract.RequiresNotNull(query)
                .Where(x => x.UpdatedDate != null
                    && x.UpdatedDate >= minUpdatedOrCreatedDate!.Value
                    && x.UpdatedDate <= maxUpdatedOrCreatedDate!.Value
                    || x.CreatedDate >= minUpdatedOrCreatedDate!.Value
                    && x.CreatedDate <= maxUpdatedOrCreatedDate!.Value);
        }

        /// <summary>An IQueryable{TEntity} extension method that duplicate check.</summary>
        /// <typeparam name="TEntity">Type of the entity.</typeparam>
        /// <param name="query">The query to act on.</param>
        /// <param name="id">   The identifier.</param>
        /// <param name="key">  The key.</param>
        /// <returns>A Task{bool}.</returns>
        public static Task<bool> DuplicateCheckAsync<TEntity>(
                this IQueryable<TEntity> query,
                int? id,
                string? key)
            where TEntity : class, IBase
        {
            // ReSharper disable once RedundantArgumentDefaultValue (NOTE: Required)
            return Contract.RequiresNotNull(query)
                .FilterByActive(true)
                .FilterByExcludedID(id)
                .FilterByCustomKey(key, true)
                .AnyAsync();
        }

        /// <summary>An IQueryable{TEntity} extension method that filter objects with JSON attributes by values.</summary>
        /// <typeparam name="TEntity">Type of the entity.</typeparam>
        /// <param name="query">         The query to act on.</param>
        /// <param name="jsonAttributes">The JSON attributes.</param>
        /// <returns>An <see cref="IQueryable{TEntity}"/>.</returns>
        public static IQueryable<TEntity> FilterObjectsWithJsonAttributesByValues<TEntity>(
                this IQueryable<TEntity> query,
                Dictionary<string, string?[]?>? jsonAttributes)
            where TEntity : class, IBase
        {
            if (jsonAttributes == null || jsonAttributes.Count == 0)
            {
                return query;
            }
            return Contract.RequiresNotNull(query)
                .Where(BuildJsonAttributePredicateForValues<TEntity>(jsonAttributes));
        }

        /// <summary>A Dictionary{string,string[]} extension method that builds JSON attribute predicate for values.</summary>
        /// <typeparam name="TEntity">Type of the entity.</typeparam>
        /// <param name="jsonAttributes">The jsonAttributes to act on.</param>
        /// <returns>An Expression{Func{TEntity,bool}}.</returns>
        public static Expression<Func<TEntity, bool>> BuildJsonAttributePredicateForValues<TEntity>(
                this Dictionary<string, string?[]?> jsonAttributes)
            where TEntity : class, IBase
        {
            // ReSharper disable StringIndexOfIsCultureSpecific.1, StringIndexOfIsCultureSpecific.2, StyleCop.SA1001, StyleCop.SA1009, StyleCop.SA1111, StyleCop.SA1113, StyleCop.SA1115, StyleCop.SA1116, StyleCop.SA1118
            var result = PredicateBuilder.New<TEntity>(true);
            if (jsonAttributes == null)
            {
                return result;
            }
            // Example:
            // {"Payoneer-Order-ID":{"ID":1,"Key":"Payoneer-Order-ID","Value":"12345678910123","UofM":""}}
            // ReSharper disable once ReplaceWithStringIsNullOrEmpty
            var iterateList = jsonAttributes
                .Where(kvp => kvp.Value?.Any(x => x != null && x != string.Empty) == true)
                .ToList();
            if (iterateList.Count == 0)
            {
                return result;
            }
            result = result.And(x => x.JsonAttributes != null && x.JsonAttributes != "{}");
            foreach (var kvp in iterateList)
            {
                // Key/Value Pair we are looking for
                var key = $"\"Key\":\"{kvp.Key}\",";
                var keySkip = key.Length;
                // Group is normally not stored
                ////const string? ValueStarter = "\"Group\":\"\",\"Value\":\"";
                const string? ValueStarter = "\"Value\":\"";
                var valueStarterSkip = ValueStarter.Length;
                // ReSharper disable once ReplaceWithStringIsNullOrEmpty
                var allowedValues = kvp.Value!.Where(x => x != null && x != string.Empty);
                // NOTE: We don't have a JSON search option in EF, this is the closest we could come up with
                result = result
                    // Skip if the key isn't anywhere in the string
                    .And(x => x.JsonAttributes!.Contains(key))
                    // Check Each Allowed Value as 'Or'
                    .And(x => allowedValues.Any(allowedValue => x.JsonAttributes!
                        // Given original full string:
                        // ...{"Payoneer-Order-ID":{"ID":1,"Key":"Payoneer-Order-ID","Value":"12345678910123","UofM":""}}...
                        .Substring(
                            x.JsonAttributes.IndexOf(key) + keySkip + valueStarterSkip,
                            x.JsonAttributes.Length - x.JsonAttributes.IndexOf(key) + keySkip + valueStarterSkip + allowedValue!.Length + 1 > 0
                                ? allowedValue.Length + 1
                                // Handles case where actual value and JSON are shorter
                                : x.JsonAttributes.Length - x.JsonAttributes.IndexOf(key) + keySkip + valueStarterSkip > 0
                                    ? x.JsonAttributes.Length - x.JsonAttributes.IndexOf(key) + keySkip + valueStarterSkip
                                    // Handles case where actual value is blank and no closing JSON. Should never happen. This is a safety path
                                    : 0)
                        // Narrowed to:
                        // 12345678910123"
                        // Or some other value if the value does not match
                        .Equals(allowedValue + "\"")));
            }
            return result;
            // ReSharper restore StringIndexOfIsCultureSpecific.1, StringIndexOfIsCultureSpecific.2, StyleCop.SA1001, StyleCop.SA1009, StyleCop.SA1111, StyleCop.SA1113, StyleCop.SA1115, StyleCop.SA1116, StyleCop.SA1118
        }

        /// <summary>An <see cref="IQueryable{TEntity}"/> extension method that filter by excluded identifier.</summary>
        /// <exception cref="ArgumentNullException">Thrown when one or more required arguments are null.</exception>
        /// <typeparam name="TEntity">Type of the entity.</typeparam>
        /// <param name="query">The query to act on.</param>
        /// <param name="id">   The identifier.</param>
        /// <returns>An <see cref="IQueryable{TEntity}"/>.</returns>
        public static IQueryable<TEntity> FilterByExcludedID<TEntity>(
                this IQueryable<TEntity> query,
                int? id)
            where TEntity : class, IBase
        {
            if (Contract.CheckInvalidID(id))
            {
                return query;
            }
            return Contract.RequiresNotNull(query)
                .Where(x => x.ID != id!.Value);
        }

        /// <summary>An <see cref="IQueryable{TEntity}"/> extension method that filter by identifier or custom
        /// key.</summary>
        /// <exception cref="ArgumentNullException">Thrown when one or more required arguments are null.</exception>
        /// <typeparam name="TEntity">Type of the entity.</typeparam>
        /// <param name="query">        The query to act on.</param>
        /// <param name="idOrCustomKey">The identifier or custom key.</param>
        /// <param name="strict">       true to strict.</param>
        /// <returns>An <see cref="IQueryable{TEntity}"/>.</returns>
        private static IQueryable<TEntity> FilterByIDOrCustomKey<TEntity>(
                this IQueryable<TEntity> query,
                string? idOrCustomKey,
                bool strict = false)
            where TEntity : class, IBase
        {
            if (!Contract.CheckValidKey(idOrCustomKey))
            {
                return query;
            }
            var search = idOrCustomKey!.Trim();
            if (strict)
            {
                return Contract.RequiresNotNull(query)
                    .Where(x => x.ID.ToString() == idOrCustomKey
                        || x.CustomKey != null && x.CustomKey == search);
            }
            search = search.ToLower();
            return Contract.RequiresNotNull(query)
                .Where(x => x.ID.ToString().Contains(search)
                    || x.CustomKey!.Contains(search));
        }

        /// <summary>Builds IDs predicate.</summary>
        /// <typeparam name="TEntity">Type of the entity.</typeparam>
        /// <param name="ids">The identifiers.</param>
        /// <returns>An Expression{Func{TEntity,bool}}.</returns>
        private static Expression<Func<TEntity, bool>> BuildIDsPredicate<TEntity>(IEnumerable<int> ids)
            where TEntity : class, IBase
        {
            return ids.Aggregate(PredicateBuilder.New<TEntity>(false), (c, id) => c.Or(p => p.ID == id));
        }

        /// <summary>Builds not IDs predicate.</summary>
        /// <typeparam name="TEntity">Type of the entity.</typeparam>
        /// <param name="ids">The identifiers.</param>
        /// <returns>An Expression{Func{TEntity,bool}}.</returns>
        private static Expression<Func<TEntity, bool>> BuildExcludedIDsPredicate<TEntity>(IEnumerable<int> ids)
            where TEntity : class, IBase
        {
            return ids.Aggregate(PredicateBuilder.New<TEntity>(true), (c, id) => c.And(p => p.ID != id));
        }

        /// <summary>Builds Custom Keys predicate.</summary>
        /// <typeparam name="TEntity">Type of the entity.</typeparam>
        /// <param name="keys">  The custom keys.</param>
        /// <param name="strict">True to strict.</param>
        /// <returns>An Expression{Func{TEntity,bool}}.</returns>
        private static Expression<Func<TEntity, bool>> BuildCustomKeysPredicate<TEntity>(IEnumerable<string> keys, bool strict)
            where TEntity : class, IBase
        {
            return strict
                ? keys.Aggregate(PredicateBuilder.New<TEntity>(false), (c, key) => c.Or(p => p.CustomKey == key))
                : keys.Aggregate(PredicateBuilder.New<TEntity>(false), (c, key) => c.Or(p => p.CustomKey!.Contains(key)));
        }
    }
}
