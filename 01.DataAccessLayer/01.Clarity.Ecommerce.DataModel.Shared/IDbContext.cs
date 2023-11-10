// <copyright file="IDbContext.cs" company="clarity-ventures.com">
// Copyright (c) 2016-2023 clarity-ventures.com. All rights reserved.
// </copyright>
// <summary>Declares the IDbContext interface</summary>
#nullable enable
#if NET5_0_OR_GREATER
[assembly: System.Runtime.CompilerServices.InternalsVisibleTo("Clarity.Ecommerce.Testing.Net5")]
#endif

namespace Clarity.Ecommerce.Interfaces.DataModel
{
    // ReSharper disable UnusedMember.Global
    using System.Collections.Generic;
    using System.Data.Entity;
    using System.Data.Entity.Infrastructure;
    using System.Data.Entity.Validation;
    using System.Threading;
    using System.Threading.Tasks;

    public interface IDbContext : System.IDisposable, IObjectContextAdapter
    {
        DbChangeTracker ChangeTracker { get; }

        DbContextConfiguration Configuration { get; }

        Database Database { get; }

        DbEntityEntry Entry(object entity);

        DbEntityEntry<TEntity> Entry<TEntity>(TEntity entity)
            where TEntity : class;

        System.Type GetType();

        IEnumerable<DbEntityValidationResult> GetValidationErrors();

        int SaveChanges();

        Task<int> SaveChangesAsync();

        Task<int> SaveChangesAsync(CancellationToken cancellationToken);

        DbSet Set(System.Type entityType);

        DbSet<TEntity> Set<TEntity>()
            where TEntity : class;
    }
}
