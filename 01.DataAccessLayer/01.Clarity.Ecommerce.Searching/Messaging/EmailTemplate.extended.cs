// <copyright file="EmailTemplate.extended.cs" company="clarity-ventures.com">
// Copyright (c) 2018-2023 clarity-ventures.com. All rights reserved.
// </copyright>
// <summary>Implements the email template search extensions class</summary>
namespace Clarity.Ecommerce.Interfaces.Models
{
    using System.Linq;
    using DataModel;
    using Utilities;

    /// <summary>An email template search extensions.</summary>
    public static class EmailTemplateSearchExtensions
    {
        /// <summary>An IQueryable{TEntity} extension method that filter email templates by subject.</summary>
        /// <typeparam name="TEntity">Type of the entity.</typeparam>
        /// <param name="query">  The query to act on.</param>
        /// <param name="subject">The subject.</param>
        /// <returns>An <see cref="IQueryable{TEntity}"/>.</returns>
        public static IQueryable<TEntity> FilterEmailTemplatesBySubject<TEntity>(
                this IQueryable<TEntity> query,
                string? subject)
            where TEntity : class, IEmailTemplate
        {
            if (!Contract.CheckValidKey(subject))
            {
                return query;
            }
            var search = subject!.Trim().ToLower();
            return Contract.RequiresNotNull(query)
                .Where(x => x.Subject != null && x.Subject.Contains(search));
        }

        /// <summary>An IQueryable{TEntity} extension method that filter email templates by body.</summary>
        /// <typeparam name="TEntity">Type of the entity.</typeparam>
        /// <param name="query">The query to act on.</param>
        /// <param name="body"> The body.</param>
        /// <returns>An <see cref="IQueryable{TEntity}"/>.</returns>
        public static IQueryable<TEntity> FilterEmailTemplatesByBody<TEntity>(
                this IQueryable<TEntity> query,
                string? body)
            where TEntity : class, IEmailTemplate
        {
            if (!Contract.CheckValidKey(body))
            {
                return query;
            }
            var search = body!.Trim().ToLower();
            return Contract.RequiresNotNull(query)
                .Where(x => x.Body != null && x.Body.Contains(search));
        }
    }
}
