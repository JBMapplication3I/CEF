// <copyright file="Favorites.extended.cs" company="clarity-ventures.com">
// Copyright (c) 2018-2023 clarity-ventures.com. All rights reserved.
// </copyright>
// <summary>Implements the favorites search extensions class</summary>
namespace Clarity.Ecommerce.Interfaces.Models
{
    using System.Linq;
    using DataModel;
    using Utilities;

    /// <summary>The favorites search extensions.</summary>
    public static class FavoritesSearchExtensions
    {
        /// <summary>An IQueryable{TEntity} extension method that filter favorites by favorite identifier key or name.</summary>
        /// <typeparam name="TEntity">  Type of the entity.</typeparam>
        /// <typeparam name="TFavorite">Type of the favorite.</typeparam>
        /// <param name="query">The query to act on.</param>
        /// <param name="id">   The identifier.</param>
        /// <param name="key">  The key.</param>
        /// <param name="name"> The name.</param>
        /// <returns>An <see cref="IQueryable{TEntity}"/>.</returns>
        public static IQueryable<TEntity> FilterFavoritesByFavoriteIDKeyOrName<TEntity, TFavorite>(
                this IQueryable<TEntity> query,
                int? id,
                string? key,
                string? name)
            where TEntity : class, IAmAFavoriteRelationshipTable<TFavorite>
            where TFavorite : class, INameableBase
        {
            Contract.RequiresNotNull(query);
            if (Contract.CheckValidID(id))
            {
                query = query.Where(x => x.SlaveID == id);
            }
            if (Contract.CheckValidKey(key))
            {
                query = query
                    .Where(x => x.Slave!.CustomKey != null
                             && x.Slave.CustomKey.Contains(key!));
            }
            if (Contract.CheckValidKey(name))
            {
                query = query
                    .Where(x => x.Slave!.Name != null
                             && x.Slave.Name.Contains(name!));
            }
            return query;
        }
    }
}
