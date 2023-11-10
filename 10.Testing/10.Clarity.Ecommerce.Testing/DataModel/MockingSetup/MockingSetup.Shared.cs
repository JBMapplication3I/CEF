// <copyright file="MockingSetup.cs" company="clarity-ventures.com">
// Copyright (c) 2016-2023 clarity-ventures.com. All rights reserved.
// </copyright>
// <summary>Implements the mocking setup class</summary>
#pragma warning disable AsyncFixer02 // Long-running or blocking operations inside an async method
namespace Clarity.Ecommerce.Testing
{
    using System;
    using System.Collections.Concurrent;
    using System.Collections.Generic;
    using System.Data.Entity;
    using System.Data.Entity.Infrastructure;
    using System.Diagnostics;
    using System.Linq;
    using System.Linq.Expressions;
    using System.Threading;
    using System.Threading.Tasks;
    using Interfaces.DataModel;
    using Microsoft.Win32;
    using Moq;
    using Utilities;

    public partial class MockingSetup
    {
        private static readonly DateTime CreatedDate = new(2023, 1, 1);

        public int SaveChangesResult { private get; set; }

        public bool DoAll { private get; set; }

        public bool DoInactives { private get; set; }

        public bool DoInactiveSalesItems { private get; set; }

        public bool SetupComplete { get; private set; }

        public bool SetupStarted { get; private set; }

        public ConcurrentDictionary<Type, int> TableRecordCounts { get; } = new();

        public ConcurrentDictionary<Type, int> TableFirstRecordIDs { get; } = new();

        public ConcurrentDictionary<Type, string?> TableFirstRecordCustomKeys { get; } = new();

        public ConcurrentDictionary<Type, string?> TableFirstRecordNames { get; } = new();

        public ConcurrentDictionary<Type, string?> TableFirstRecordDisplayNames { get; } = new();

        public Mock<IClarityEcommerceEntities> MockContext { get; private set; } = new Mock<IClarityEcommerceEntities>();

        public async Task<Mock<IClarityEcommerceEntities>> DoMockingSetupForContextAsync(string contextProfileName)
        {
            if (SetupComplete) { return MockContext; }
            SetupComplete = false;
            SetupStarted = true;
            MockContext = await DoMockingSetupForContextRunnerAsync(MockContext).ConfigureAwait(false);
            await DoAssignMocksAsync(MockContext).ConfigureAwait(false);
            MockContext.Setup(m => m.ContextProfileName).Returns(() => contextProfileName);
            MockContext.Setup(m => m.SaveChanges()).Returns(() => SaveChangesResult);
            MockContext.Setup(m => m.SaveChangesAsync()).ReturnsAsync(() =>
            {
                DoAssignMocksAsync(MockContext, true).Wait(10_000);
                return SaveChangesResult;
            });
            SetupComplete = true;
            return MockContext;
        }

        private async Task<TEntity> AddAsync<TEntity>(TEntity entity, ICollection<Mock<TEntity>>? data)
            where TEntity : class, IBase
        {
            Contract.RequiresNotNull(data);
            // Auto-Increment ID
            entity.ID = data.Select(x => x.Object.ID).DefaultIfEmpty(0).Max() + 1;
            var mock = Mock.Get(entity);
            data!.Add(mock);
            MarkDirty<TEntity>();
            await DoAssignMocksAsync(MockContext, true).ConfigureAwait(false);
            MarkPristine();
            return entity;
        }

        private async Task<List<TEntity>> AddRangeAsync<TEntity>(List<TEntity> entities, List<Mock<TEntity>>? data)
            where TEntity : class, IBase
        {
            // Auto-Increment ID for each entity with no ID on it
            Contract.RequiresNotNull(data);
            var maxID = data.Select(x => x.Object.ID).DefaultIfEmpty(0).Max() + 1;
            foreach (var entity in entities.Where(entity => entity.ID <= 0))
            {
                entity.ID = ++maxID;
            }
            var mocks = entities.Select(Mock.Get);
            data!.AddRange(mocks);
            MarkDirty<TEntity>();
            await DoAssignMocksAsync(MockContext, true).ConfigureAwait(false);
            MarkPristine();
            return entities;
        }

        private async Task<TEntity> RemoveAsync<TEntity>(TEntity entity, ICollection<Mock<TEntity>>? data)
            where TEntity : class, IBase
        {
            Contract.RequiresNotNull(data);
            var mock = Mock.Get(entity);
            data!.Remove(mock);
            MarkDirty<TEntity>();
            await DoAssignMocksAsync(MockContext, true).ConfigureAwait(false);
            MarkPristine();
            return entity;
        }

        private async Task<IEnumerable<TEntity>> RemoveAllAsync<TEntity>(ICollection<TEntity> entities, List<Mock<TEntity>>? data)
            where TEntity : class, IBase
        {
            Contract.RequiresNotNull(data);
            var mocks = entities.Select(Mock.Get);
            data!.RemoveAll(mocks.Contains);
            MarkDirty<TEntity>();
            await DoAssignMocksAsync(MockContext, true).ConfigureAwait(false);
            MarkPristine();
            return entities;
        }

        [DebuggerStepThrough]
        public async Task InitializeMockSetFromListAsync<TEntity>(Mock<DbSet<TEntity>>? mockSet, List<Mock<TEntity>>? data)
            where TEntity : class, IBase
        {
            Contract.RequiresNotNull(mockSet);
            Contract.RequiresNotNull(data);
            mockSet!.Setup(m => m.Local).CallBase();
            mockSet!.Setup(m => m.FindAsync(It.IsAny<object[]>())).Callback<object[]>(arr => Task.FromResult(data?.Find(x => x.Object.ID == (int)arr[0]).Object));
            mockSet!.Setup(m => m.FindAsync(It.IsAny<CancellationToken>(), It.IsAny<object[]>())).Callback<CancellationToken, object[]>((_, arr) => Task.FromResult(data?.Find(x => x.Object.ID == (int)arr[0]).Object));
            mockSet!.Setup(m => m.Find()).CallBase();
            mockSet!.Setup(m => m.Attach(It.IsAny<TEntity>())).CallBase();
            mockSet!.Setup(m => m.Create()).Returns(() => RegistryLoaderWrapper.GetInstance<TEntity>(MockContext.Object.ContextProfileName));
            await Task.Run(() =>
            {
                // Read
                mockSet!.As<IQueryable<TEntity>>().Setup(m => m.Provider).Returns(new TestDbAsyncQueryProvider<TEntity>(data.Select(x => x.Object).AsQueryable().Provider));
                mockSet.As<IQueryable<TEntity>>().Setup(m => m.Expression).Returns(() => data.Select(x => x.Object).AsQueryable().Expression);
                mockSet.As<IQueryable<TEntity>>().Setup(m => m.ElementType).Returns(() => data.Select(x => x.Object).AsQueryable().ElementType);
                mockSet.As<IQueryable<TEntity>>().Setup(m => m.GetEnumerator()).Returns(() => data.Select(x => x.Object).AsQueryable().GetEnumerator());
                mockSet.As<IDbAsyncEnumerable>().Setup(m => m.GetAsyncEnumerator()).Returns(() => new TestDbAsyncEnumerator<TEntity>(data.Select(x => x.Object).AsQueryable().GetEnumerator()));
                mockSet.As<IDbAsyncEnumerable<TEntity>>().Setup(m => m.GetAsyncEnumerator()).Returns(() => new TestDbAsyncEnumerator<TEntity>(data.Select(x => x.Object).AsQueryable().GetEnumerator()));
            })
            .ConfigureAwait(false);
            await Task.WhenAll(
                    // CUD
                    // ReSharper disable AsyncConverter.AsyncWait
                    Task.Run(() => mockSet!.Setup(m => m.Add(It.IsAny<TEntity>())).Returns((TEntity x) => AddAsync(x, data).Result)),
                    Task.Run(() => mockSet!.Setup(m => m.Remove(It.IsAny<TEntity>())).Returns((TEntity x) => RemoveAsync(x, data).Result)),
                    Task.Run(() => mockSet!.Setup(m => m.AddRange(It.IsAny<IEnumerable<TEntity>>())).Returns((List<TEntity> x) => AddRangeAsync(x, data).Result)),
                    Task.Run(() => mockSet!.Setup(m => m.RemoveRange(It.IsAny<IEnumerable<TEntity>>())).Returns((List<TEntity> x) => RemoveAllAsync(x, data).Result)),
                    // ReSharper restore AsyncConverter.AsyncWait
                    // Extra
                    Task.Run(() => mockSet!.Setup(m => m.Include(It.IsAny<string>())).Returns(() => mockSet.Object)),
                    Task.Run(() => mockSet!.Setup(m => m.AsNoTracking()).Returns(() => mockSet.Object)))
                .ConfigureAwait(false);
        }

        private async Task InitializeMockSetFromListNonIBaseAsync<TEntity>(Mock<DbSet<TEntity>>? mockSet, List<Mock<TEntity>>? data)
            where TEntity : class
        {
            Contract.RequiresNotNull(mockSet);
            Contract.RequiresNotNull(data);
            mockSet!.Setup(m => m.Local).CallBase();
            mockSet!.Setup(m => m.FindAsync()).CallBase();
            mockSet!.Setup(m => m.FindAsync(It.IsAny<CancellationToken>())).CallBase();
            mockSet!.Setup(m => m.Find()).CallBase();
            mockSet!.Setup(m => m.Attach(It.IsAny<TEntity>())).CallBase();
            mockSet!.Setup(m => m.Create()).Returns(() => RegistryLoaderWrapper.GetInstance<TEntity>(MockContext.Object.ContextProfileName));
            await Task.Run(() =>
            {
                // Read
                mockSet!.As<IQueryable<TEntity>>().Setup(m => m.Provider).Returns(() => new TestDbAsyncQueryProvider<TEntity>(data.Select(x => x.Object).AsQueryable().Provider));
                mockSet.As<IQueryable<TEntity>>().Setup(m => m.Expression).Returns(() => data.Select(x => x.Object).AsQueryable().Expression);
                mockSet.As<IQueryable<TEntity>>().Setup(m => m.ElementType).Returns(() => data.Select(x => x.Object).AsQueryable().ElementType);
                mockSet.As<IQueryable<TEntity>>().Setup(m => m.GetEnumerator()).Returns(() => data.Select(x => x.Object).AsQueryable().GetEnumerator());
                mockSet.As<IDbAsyncEnumerable>().Setup(m => m.GetAsyncEnumerator()).Returns(() => new TestDbAsyncEnumerator<TEntity>(data.Select(x => x.Object).AsQueryable().GetEnumerator()));
                mockSet.As<IDbAsyncEnumerable<TEntity>>().Setup(m => m.GetAsyncEnumerator()).Returns(() => new TestDbAsyncEnumerator<TEntity>(data.Select(x => x.Object).AsQueryable().GetEnumerator()));
            }).ConfigureAwait(false);
            await Task.WhenAll(
                    // CUD
                    // ReSharper disable AsyncConverter.AsyncWait
                    Task.Run(() => mockSet!.Setup(m => m.Add(It.IsAny<TEntity>())).Returns((TEntity x) => AddNonIBaseAsync(x, data).Result)),
                    Task.Run(() => mockSet!.Setup(m => m.Remove(It.IsAny<TEntity>())).Returns((TEntity x) => RemoveNonIBaseAsync(x, data!).Result)),
                    Task.Run(() => mockSet!.Setup(m => m.AddRange(It.IsAny<IEnumerable<TEntity>>())).Returns((List<TEntity> x) => AddRangeNonIBaseAsync(x, data!).Result)),
                    Task.Run(() => mockSet!.Setup(m => m.RemoveRange(It.IsAny<IEnumerable<TEntity>>())).Returns((List<TEntity> x) => RemoveAllNonIBaseAsync(x, data!).Result)),
                    // ReSharper restore AsyncConverter.AsyncWait
                    // Extra
                    Task.Run(() => mockSet!.Setup(m => m.Include(It.IsAny<string>())).Returns(() => mockSet.Object)),
                    Task.Run(() => mockSet!.Setup(m => m.AsNoTracking()).Returns(() => mockSet.Object)))
                .ConfigureAwait(false);
        }

        /*
        private async Task InitializeMockSetNonIBaseAsync<TEntity>(
                Mock<IClarityEcommerceEntities> context,
                List<TEntity> data,
                Mock<DbSet<TEntity>> dbSet,
                Func<bool> skipCondition,
                Expression<Func<IClarityEcommerceEntities, IDbSet<TEntity>>> contextPropertySelectionExpression)
            where TEntity : class
        {
            if (skipCondition()) { return; }
            if (dbSet == null)
            {
                dbSet = new Mock<DbSet<TEntity>>();
                await InitializeMockSetFromListNonIBaseAsync(dbSet, new List<Mock<TEntity>>()).ConfigureAwait(false);
            }
            await InitializeMockSetFromListNonIBaseAsync(dbSet, data).ConfigureAwait(false);
            context.Setup(contextPropertySelectionExpression).Returns(dbSet.Object);
        }
        */

        private async Task<TEntity> AddNonIBaseAsync<TEntity>(TEntity entity, ICollection<Mock<TEntity>>? data)
            where TEntity : class
        {
            Contract.RequiresNotNull(data);
            // Auto-Increment ID
            // entity.ID = data.Select(x => x.Object.ID).DefaultIfEmpty(0).Max() + 1;
            var mock = Mock.Get(entity);
            data!.Add(mock);
            MarkDirty<TEntity>();
            await DoAssignMocksAsync(MockContext, true).ConfigureAwait(false);
            MarkPristine();
            return entity;
        }

        private async Task<List<TEntity>> AddRangeNonIBaseAsync<TEntity>(List<TEntity> entities, List<Mock<TEntity>>? data)
            where TEntity : class
        {
            // Auto-Increment ID for each entity with no ID on it
            Contract.RequiresNotNull(data);
            // var maxID = data.Select(x => x.Object.ID).DefaultIfEmpty(0).Max() + 1;
            // foreach (var entity in entities.Where(entity => entity.ID <= 0))
            // {
            //     entity.ID = ++maxID;
            // }
            var mocks = entities.Select(Mock.Get);
            data!.AddRange(mocks);
            MarkDirty<TEntity>();
            await DoAssignMocksAsync(MockContext, true).ConfigureAwait(false);
            MarkPristine();
            return entities;
        }

        private async Task<TEntity> RemoveNonIBaseAsync<TEntity>(TEntity entity, ICollection<Mock<TEntity>>? data)
            where TEntity : class
        {
            Contract.RequiresNotNull(data);
            var mock = Mock.Get(entity);
            data!.Remove(mock);
            MarkDirty<TEntity>();
            await DoAssignMocksAsync(MockContext, true).ConfigureAwait(false);
            MarkPristine();
            return entity;
        }

        private async Task<IEnumerable<TEntity>> RemoveAllNonIBaseAsync<TEntity>(ICollection<TEntity> entities, List<Mock<TEntity>> data)
            where TEntity : class
        {
            Contract.RequiresNotNull(data);
            var mocks = entities.Select(Mock.Get);
            data.RemoveAll(mocks.Contains);
            MarkDirty<TEntity>();
            await DoAssignMocksAsync(MockContext, true).ConfigureAwait(false);
            MarkPristine();
            return entities;
        }

        internal class TestDbAsyncQueryProvider<TEntity> : IDbAsyncQueryProvider
        {
            private readonly IQueryProvider inner;

            internal TestDbAsyncQueryProvider(IQueryProvider inner)
            {
                this.inner = inner;
            }

            public IQueryable CreateQuery(Expression expression)
            {
                return new TestDbAsyncEnumerable<TEntity>(expression);
            }

            public IQueryable<TElement> CreateQuery<TElement>(Expression expression)
            {
                return new TestDbAsyncEnumerable<TElement>(expression);
            }

            public object? Execute(Expression expression)
            {
                return inner.Execute(expression);
            }

            public TResult Execute<TResult>(Expression expression)
            {
                return inner.Execute<TResult>(expression);
            }

            public Task<object?> ExecuteAsync(Expression expression, CancellationToken cancellationToken)
            {
                return Task.FromResult(Execute(expression));
            }

            public Task<TResult> ExecuteAsync<TResult>(Expression expression, CancellationToken cancellationToken)
            {
                return Task.FromResult(Execute<TResult>(expression));
            }
        }

        internal class TestDbAsyncEnumerable<T> : EnumerableQuery<T>, IDbAsyncEnumerable<T>, IQueryable<T>
        {
            public TestDbAsyncEnumerable(IEnumerable<T> enumerable) : base(enumerable)
            {
            }

            public TestDbAsyncEnumerable(Expression expression) : base(expression)
            {
            }

            public IDbAsyncEnumerator<T> GetAsyncEnumerator()
            {
                return new TestDbAsyncEnumerator<T>(this.AsEnumerable().GetEnumerator());
            }

            IDbAsyncEnumerator IDbAsyncEnumerable.GetAsyncEnumerator()
            {
                return GetAsyncEnumerator();
            }

            IQueryProvider IQueryable.Provider => new TestDbAsyncQueryProvider<T>(this);
        }

        internal class TestDbAsyncEnumerator<T> : IDbAsyncEnumerator<T>
        {
            private readonly IEnumerator<T> inner;

            public TestDbAsyncEnumerator(IEnumerator<T> inner)
            {
                this.inner = inner;
            }

            public void Dispose()
            {
                inner.Dispose();
            }

            public Task<bool> MoveNextAsync(CancellationToken cancellationToken)
            {
                return Task.FromResult(inner.MoveNext());
            }

            public T Current => inner.Current;

            object? IDbAsyncEnumerator.Current => Current;
        }
    }
}
