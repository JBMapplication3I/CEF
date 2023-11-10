// <copyright file="NameableBaseSQLSearchExtensions.cs" company="clarity-ventures.com">
// Copyright (c) 2017-2023 clarity-ventures.com. All rights reserved.
// </copyright>
// <summary>Implements the nameable base SQL search extensions class</summary>
namespace Clarity.Ecommerce.Interfaces.Models
{
    using System;
    using System.Linq;
    using DataModel;
    using Utilities;

    /// <summary>A nameable base SQL search extensions.</summary>
    public static class NameableBaseSQLSearchExtensions
    {
        /// <summary>An IQueryable{TEntity} extension method that filter by nameable base search model.</summary>
        /// <typeparam name="TEntity">Type of the entity.</typeparam>
        /// <param name="query">The query to act on.</param>
        /// <param name="model">The model.</param>
        /// <returns>An <see cref="IQueryable{TEntity}"/>.</returns>
        public static IQueryable<TEntity> FilterByNameableBaseSearchModel<TEntity>(
                this IQueryable<TEntity> query,
                INameableBaseSearchModel model)
            where TEntity : class, INameableBase
        {
            return Contract.RequiresNotNull(query)
                .FilterByBaseSearchModel(model)
                .FilterByName(model.Name, model.NameStrict ?? false)
                .FilterByDescription(model.Description)
                .FilterByCustomKeyOrName(model.CustomKeyOrName, model.NameStrict ?? false)
                .FilterByCustomKeyOrNameOrDescription(model.CustomKeyOrNameOrDescription, model.NameStrict ?? false)
                .FilterByIDOrCustomKeyOrName(model.IDOrCustomKeyOrName, model.NameStrict ?? false)
                .FilterByIDOrCustomKeyOrNameOrDescription(model.IDOrCustomKeyOrNameOrDescription, model.NameStrict ?? false);
        }

        /// <summary>An <see cref="IQueryable{TEntity}"/> extension method that filter by name.</summary>
        /// <exception cref="ArgumentNullException">Thrown when one or more required arguments are null.</exception>
        /// <typeparam name="TEntity">Type of the entity.</typeparam>
        /// <param name="query"> The query to act on.</param>
        /// <param name="name">  The name.</param>
        /// <param name="strict">true to strict.</param>
        /// <returns>An <see cref="IQueryable{TEntity}"/>.</returns>
        public static IQueryable<TEntity> FilterByName<TEntity>(
                this IQueryable<TEntity> query,
                string? name,
                bool strict = false)
            where TEntity : class, INameableBase
        {
            if (!Contract.CheckValidKey(name))
            {
                return query;
            }
            var search = name!.Trim();
            if (strict)
            {
                return Contract.RequiresNotNull(query)
                    .Where(x => x.Name == search);
            }
            search = search.ToLower();
            return Contract.RequiresNotNull(query)
                .Where(x => x.Name != null && x.Name.Contains(search));
        }

        /// <summary>An <see cref="IQueryable{TEntity}"/> extension method that filter by description.</summary>
        /// <exception cref="ArgumentNullException">Thrown when one or more required arguments are null.</exception>
        /// <typeparam name="TEntity">Type of the entity.</typeparam>
        /// <param name="query">      The query to act on.</param>
        /// <param name="description">The description.</param>
        /// <returns>An <see cref="IQueryable{TEntity}"/>.</returns>
        private static IQueryable<TEntity> FilterByDescription<TEntity>(
                this IQueryable<TEntity> query,
                string? description)
            where TEntity : class, INameableBase
        {
            if (!Contract.CheckValidKey(description))
            {
                return query;
            }
            var search = description!.Trim().ToLower();
            return Contract.RequiresNotNull(query)
                .Where(x => x.Description != null && x.Description.Contains(search));
        }

        /// <summary>An <see cref="IQueryable{TEntity}"/> extension method that filter by custom key or name.</summary>
        /// <exception cref="ArgumentNullException">Thrown when one or more required arguments are null.</exception>
        /// <typeparam name="TEntity">Type of the entity.</typeparam>
        /// <param name="query">          The query to act on.</param>
        /// <param name="customKeyOrName">Name of the custom key or.</param>
        /// <param name="strict">         true to strict.</param>
        /// <returns>An <see cref="IQueryable{TEntity}"/>.</returns>
        private static IQueryable<TEntity> FilterByCustomKeyOrName<TEntity>(
                this IQueryable<TEntity> query,
                string? customKeyOrName,
                bool strict = false)
            where TEntity : class, INameableBase
        {
            if (!Contract.CheckValidKey(customKeyOrName))
            {
                return query;
            }
            var search = customKeyOrName!.Trim();
            if (strict)
            {
                return Contract.RequiresNotNull(query)
                    .Where(x => x.CustomKey != null && x.CustomKey == search
                        || x.Name != null && x.Name == search);
            }
            search = search.ToLower();
            return Contract.RequiresNotNull(query)
                .Where(x => x.CustomKey != null && x.CustomKey.Contains(search)
                    || x.Name != null && x.Name.Contains(search));
        }

        /// <summary>An <see cref="IQueryable{TEntity}"/> extension method that filter by custom key or name or
        /// description.</summary>
        /// <exception cref="ArgumentNullException">Thrown when one or more required arguments are null.</exception>
        /// <typeparam name="TEntity">Type of the entity.</typeparam>
        /// <param name="query">                       The query to act on.</param>
        /// <param name="customKeyOrNameOrDescription">Information describing the custom key or name or.</param>
        /// <param name="strict">                      true to strict.</param>
        /// <returns>An <see cref="IQueryable{TEntity}"/>.</returns>
        private static IQueryable<TEntity> FilterByCustomKeyOrNameOrDescription<TEntity>(
                this IQueryable<TEntity> query,
                string? customKeyOrNameOrDescription,
                bool strict = false)
            where TEntity : class, INameableBase
        {
            if (!Contract.CheckValidKey(customKeyOrNameOrDescription))
            {
                return query;
            }
            var search = customKeyOrNameOrDescription!.Trim();
            if (strict)
            {
                return Contract.RequiresNotNull(query)
                    .Where(x => x.CustomKey != null && x.CustomKey == search
                        || x.Name != null && x.Name == search
                        || x.Description != null && x.Description == search);
            }
            search = search.ToLower();
            return Contract.RequiresNotNull(query)
                .Where(x => x.CustomKey != null && x.CustomKey.Contains(search)
                    || x.Name != null && x.Name.Contains(search)
                    || x.Description != null && x.Description.Contains(search));
        }

        /// <summary>An <see cref="IQueryable{TEntity}"/> extension method that filter by identifier or custom key
        /// or name.</summary>
        /// <exception cref="ArgumentNullException">Thrown when one or more required arguments are null.</exception>
        /// <typeparam name="TEntity">Type of the entity.</typeparam>
        /// <param name="query">              The query to act on.</param>
        /// <param name="idOrCustomKeyOrName">Name of the identifier or custom key or.</param>
        /// <param name="strict">             true to strict.</param>
        /// <returns>An <see cref="IQueryable{TEntity}"/>.</returns>
        private static IQueryable<TEntity> FilterByIDOrCustomKeyOrName<TEntity>(
                this IQueryable<TEntity> query,
                string? idOrCustomKeyOrName,
                bool strict = false)
            where TEntity : class, INameableBase
        {
            if (!Contract.CheckValidKey(idOrCustomKeyOrName))
            {
                return query;
            }
            var search = idOrCustomKeyOrName!.Trim();
            if (strict)
            {
                return Contract.RequiresNotNull(query)
                    .Where(x => x.ID.ToString() == search
                        || x.CustomKey != null && x.CustomKey == search
                        || x.Name != null && x.Name == search);
            }
            search = search.ToLower();
            return Contract.RequiresNotNull(query)
                .Where(x => x.ID.ToString().Contains(search)
                    || x.CustomKey != null && x.CustomKey.Contains(search)
                    || x.Name != null && x.Name.Contains(search));
        }

        /// <summary>An <see cref="IQueryable{TEntity}"/> extension method that filter by identifier or custom key
        /// or name or description.</summary>
        /// <exception cref="ArgumentNullException">Thrown when one or more required arguments are null.</exception>
        /// <typeparam name="TEntity">Type of the entity.</typeparam>
        /// <param name="query">                           The query to act on.</param>
        /// <param name="idOrCustomKeyOrNameOrDescription">Information describing the identifier or
        ///                                                custom key or name or.</param>
        /// <param name="strict">                          true to strict.</param>
        /// <returns>An <see cref="IQueryable{TEntity}"/>.</returns>
        private static IQueryable<TEntity> FilterByIDOrCustomKeyOrNameOrDescription<TEntity>(
                this IQueryable<TEntity> query,
                string? idOrCustomKeyOrNameOrDescription,
                bool strict = false)
            where TEntity : class, INameableBase
        {
            if (!Contract.CheckValidKey(idOrCustomKeyOrNameOrDescription))
            {
                return query;
            }
            var search = idOrCustomKeyOrNameOrDescription!.Trim();
            if (strict)
            {
                return Contract.RequiresNotNull(query)
                    .Where(x => x.ID.ToString() == search
                        || x.CustomKey != null && x.CustomKey == search
                        || x.Name != null && x.Name == search
                        || x.Description != null && x.Description == search);
            }
            search = search.ToLower();
            return Contract.RequiresNotNull(query)
                .Where(x => x.ID.ToString().Contains(search)
                    || x.CustomKey != null && x.CustomKey.Contains(search)
                    || x.Name != null && x.Name.Contains(search)
                    || x.Description != null && x.Description.Contains(search));
        }
    }
}
