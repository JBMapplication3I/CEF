// <copyright file="HaveAContactBaseSQLSearchExtensions.cs" company="clarity-ventures.com">
// Copyright (c) 2021-2023 clarity-ventures.com. All rights reserved.
// </copyright>
// <summary>Implements the have a contact base SQL search extensions class</summary>
namespace Clarity.Ecommerce.Interfaces.Models
{
    using System.Linq;
    using DataModel;
    using Utilities;

    /// <summary>A have a contact base SQL search extensions.</summary>
    public static class HaveAContactBaseSQLSearchExtensions
    {
        /// <summary>An IQueryable{TEntity} extension method that filter i have a contact base by search model.</summary>
        /// <typeparam name="TEntity">Type of the entity.</typeparam>
        /// <param name="query">The query to act on.</param>
        /// <param name="model">The model.</param>
        /// <returns>An IQueryable{TEntity}.</returns>
        public static IQueryable<TEntity> FilterIHaveAContactBaseBySearchModel<TEntity>(
                this IQueryable<TEntity> query,
                IHaveAContactBaseSearchModel model)
            where TEntity : class, IHaveAContactBase
        {
            return Contract.RequiresNotNull(query)
                .FilterIHaveAContactBaseByContactID(model.ContactID)
                .FilterIHaveAContactBaseByContactKey(model.ContactKey, model.ContactKeyStrict, model.ContactKeyIncludeNull)
                .FilterIHaveAContactBaseByContactFirstName(model.ContactFirstName, model.ContactFirstNameStrict, model.ContactFirstNameIncludeNull)
                .FilterIHaveAContactBaseByContactLastName(model.ContactLastName, model.ContactLastNameStrict, model.ContactLastNameIncludeNull)
                .FilterIHaveAContactBaseByContactPhone(model.ContactPhone, model.ContactPhoneStrict, model.ContactPhoneIncludeNull)
                .FilterIHaveAContactBaseByContactFax(model.ContactFax, model.ContactFaxStrict, model.ContactFaxIncludeNull)
                .FilterIHaveAContactBaseByContactEmail(model.ContactEmail, model.ContactEmailStrict, model.ContactEmailIncludeNull);
        }

        /// <summary>An IQueryable{TEntity} extension method that filter i have a contact base by contact identifier.</summary>
        /// <typeparam name="TEntity">Type of the entity.</typeparam>
        /// <param name="query">      The query to act on.</param>
        /// <param name="parameter">  The parameter.</param>
        /// <returns>An IQueryable{TEntity}.</returns>
        private static IQueryable<TEntity> FilterIHaveAContactBaseByContactID<TEntity>(
                this IQueryable<TEntity> query,
                int? parameter)
            where TEntity : class, IHaveAContactBase
        {
            if (Contract.CheckInvalidID(parameter))
            {
                return query;
            }
            return Contract.RequiresNotNull(query)
                .Where(x => x.ContactID == parameter!.Value);
        }

        /// <summary>An IQueryable{TEntity} extension method that filter i have a contact base by contact key.</summary>
        /// <typeparam name="TEntity">Type of the entity.</typeparam>
        /// <param name="query">      The query to act on.</param>
        /// <param name="parameter">  The parameter.</param>
        /// <param name="strict">     The strict.</param>
        /// <param name="includeNull">The include null.</param>
        /// <returns>An IQueryable{TEntity}.</returns>
        private static IQueryable<TEntity> FilterIHaveAContactBaseByContactKey<TEntity>(
                this IQueryable<TEntity> query,
                string? parameter,
                bool? strict,
                bool? includeNull)
            where TEntity : class, IHaveAContactBase
        {
            if (includeNull != true && !Contract.CheckValidKey(parameter))
            {
                return query;
            }
            var search = parameter?.Trim();
            if (strict == true)
            {
                return Contract.RequiresNotNull(query)
                    .Where(x => x.Contact!.CustomKey == search);
            }
            if (includeNull == true && !Contract.CheckValidKey(search))
            {
                return Contract.RequiresNotNull(query)
                    .Where(x => x.Contact!.CustomKey == null);
            }
            search = search!.ToLower();
            return Contract.RequiresNotNull(query)
                .Where(x => x.Contact!.CustomKey != null && x.Contact.CustomKey.Contains(search));
        }

        /// <summary>Filter i have a contact base by contact the first name.</summary>
        /// <typeparam name="TEntity">Type of the entity.</typeparam>
        /// <param name="query">      The query to act on.</param>
        /// <param name="parameter">  The parameter.</param>
        /// <param name="strict">     The strict.</param>
        /// <param name="includeNull">The include null.</param>
        /// <returns>An IQueryable{TEntity}.</returns>
        private static IQueryable<TEntity> FilterIHaveAContactBaseByContactFirstName<TEntity>(
                this IQueryable<TEntity> query,
                string? parameter,
                bool? strict,
                bool? includeNull)
            where TEntity : class, IHaveAContactBase
        {
            if (includeNull != true && !Contract.CheckValidKey(parameter))
            {
                return query;
            }
            var search = parameter?.Trim();
            if (strict == true)
            {
                return Contract.RequiresNotNull(query)
                    .Where(x => x.Contact!.FirstName == search);
            }
            if (includeNull == true && !Contract.CheckValidKey(search))
            {
                return Contract.RequiresNotNull(query)
                    .Where(x => x.Contact!.FirstName == null);
            }
            search = search!.ToLower();
            return Contract.RequiresNotNull(query)
                .Where(x => x.Contact!.FirstName != null && x.Contact.FirstName.Contains(search));
        }

        /// <summary>Filter i have a contact base by contact the last name.</summary>
        /// <typeparam name="TEntity">Type of the entity.</typeparam>
        /// <param name="query">      The query to act on.</param>
        /// <param name="parameter">  The parameter.</param>
        /// <param name="strict">     The strict.</param>
        /// <param name="includeNull">The include null.</param>
        /// <returns>An IQueryable{TEntity}.</returns>
        private static IQueryable<TEntity> FilterIHaveAContactBaseByContactLastName<TEntity>(
                this IQueryable<TEntity> query,
                string? parameter,
                bool? strict,
                bool? includeNull)
            where TEntity : class, IHaveAContactBase
        {
            if (includeNull != true && !Contract.CheckValidKey(parameter))
            {
                return query;
            }
            var search = parameter?.Trim();
            if (strict == true)
            {
                return Contract.RequiresNotNull(query)
                    .Where(x => x.Contact!.LastName == search);
            }
            if (includeNull == true && !Contract.CheckValidKey(search))
            {
                return Contract.RequiresNotNull(query)
                    .Where(x => x.Contact!.LastName == null);
            }
            search = search!.ToLower();
            return Contract.RequiresNotNull(query)
                .Where(x => x.Contact!.LastName != null && x.Contact.LastName.Contains(search));
        }

        /// <summary>An IQueryable{TEntity} extension method that filter i have a contact base by contact phone.</summary>
        /// <typeparam name="TEntity">Type of the entity.</typeparam>
        /// <param name="query">      The query to act on.</param>
        /// <param name="parameter">  The parameter.</param>
        /// <param name="strict">     The strict.</param>
        /// <param name="includeNull">The include null.</param>
        /// <returns>An IQueryable{TEntity}.</returns>
        private static IQueryable<TEntity> FilterIHaveAContactBaseByContactPhone<TEntity>(
                this IQueryable<TEntity> query,
                string? parameter,
                bool? strict,
                bool? includeNull)
            where TEntity : class, IHaveAContactBase
        {
            if (includeNull != true && !Contract.CheckValidKey(parameter))
            {
                return query;
            }
            var search = parameter?.Trim();
            if (strict == true)
            {
                return Contract.RequiresNotNull(query)
                    .Where(x => x.Contact!.Phone1 == search);
            }
            if (includeNull == true && !Contract.CheckValidKey(search))
            {
                return Contract.RequiresNotNull(query)
                    .Where(x => x.Contact!.Phone1 == null);
            }
            search = search!.ToLower();
            return Contract.RequiresNotNull(query)
                .Where(x => x.Contact!.Phone1 != null && x.Contact.Phone1.Contains(search));
        }

        /// <summary>An IQueryable{TEntity} extension method that filter i have a contact base by contact fax.</summary>
        /// <typeparam name="TEntity">Type of the entity.</typeparam>
        /// <param name="query">      The query to act on.</param>
        /// <param name="parameter">  The parameter.</param>
        /// <param name="strict">     The strict.</param>
        /// <param name="includeNull">The include null.</param>
        /// <returns>An IQueryable{TEntity}.</returns>
        private static IQueryable<TEntity> FilterIHaveAContactBaseByContactFax<TEntity>(
                this IQueryable<TEntity> query,
                string? parameter,
                bool? strict,
                bool? includeNull)
            where TEntity : class, IHaveAContactBase
        {
            if (includeNull != true && !Contract.CheckValidKey(parameter))
            {
                return query;
            }
            var search = parameter?.Trim();
            if (strict == true)
            {
                return Contract.RequiresNotNull(query)
                    .Where(x => x.Contact!.Fax1 == search);
            }
            if (includeNull == true && !Contract.CheckValidKey(search))
            {
                return Contract.RequiresNotNull(query)
                    .Where(x => x.Contact!.Fax1 == null);
            }
            search = search!.ToLower();
            return Contract.RequiresNotNull(query)
                .Where(x => x.Contact!.Fax1 != null && x.Contact.Fax1.Contains(search));
        }

        /// <summary>An IQueryable{TEntity} extension method that filter i have a contact base by contact email.</summary>
        /// <typeparam name="TEntity">Type of the entity.</typeparam>
        /// <param name="query">      The query to act on.</param>
        /// <param name="parameter">  The parameter.</param>
        /// <param name="strict">     The strict.</param>
        /// <param name="includeNull">The include null.</param>
        /// <returns>An IQueryable{TEntity}.</returns>
        private static IQueryable<TEntity> FilterIHaveAContactBaseByContactEmail<TEntity>(
                this IQueryable<TEntity> query,
                string? parameter,
                bool? strict,
                bool? includeNull)
            where TEntity : class, IHaveAContactBase
        {
            if (includeNull != true && !Contract.CheckValidKey(parameter))
            {
                return query;
            }
            var search = parameter?.Trim();
            if (strict == true)
            {
                return Contract.RequiresNotNull(query)
                    .Where(x => x.Contact!.Email1 == search);
            }
            if (includeNull == true && !Contract.CheckValidKey(search))
            {
                return Contract.RequiresNotNull(query)
                    .Where(x => x.Contact!.Email1 == null);
            }
            search = search!.ToLower();
            return Contract.RequiresNotNull(query)
                .Where(x => x.Contact!.Email1 != null && x.Contact.Email1.Contains(search));
        }
    }
}
