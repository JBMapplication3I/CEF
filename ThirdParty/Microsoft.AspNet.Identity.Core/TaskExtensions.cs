// Decompiled with JetBrains decompiler
// Type: Microsoft.AspNet.Identity.TaskExtensions
// Assembly: Microsoft.AspNet.Identity.Core, Version=2.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35
// MVID: 9E2B2C84-E0DA-4554-B416-8EA71B0D085D
// Assembly location: C:\Users\jotha\.nuget\packages\microsoft.aspnet.identity.core\2.2.3\lib\net45\Microsoft.AspNet.Identity.Core.dll

namespace Microsoft.AspNet.Identity
{
    using System;
    using System.Runtime.CompilerServices;
    using System.Threading;
    using System.Threading.Tasks;

    /// <summary>A task extensions.</summary>
    internal static class TaskExtensions
    {
        /// <summary>A Task{T} extension method that with current culture.</summary>
        /// <typeparam name="T">Generic type parameter.</typeparam>
        /// <param name="task">The task to act on.</param>
        /// <returns>A CultureAwaiter{T}</returns>
        public static CultureAwaiter<T> WithCurrentCulture<T>(this Task<T> task)
        {
            return new(task);
        }

        /// <summary>A Task extension method that with current culture.</summary>
        /// <param name="task">The task to act on.</param>
        /// <returns>A CultureAwaiter.</returns>
        public static CultureAwaiter WithCurrentCulture(this Task task)
        {
            return new(task);
        }

        /// <summary>A culture awaiter.</summary>
        /// <typeparam name="T">Generic type parameter.</typeparam>
        public struct CultureAwaiter<T> : ICriticalNotifyCompletion, INotifyCompletion
        {
            /// <summary>The task.</summary>
            private readonly Task<T> _task;

            /// <summary>Initializes a new instance of the <see cref="Microsoft.AspNet.Identity.TaskExtensions" /> class.</summary>
            /// <param name="task">The task.</param>
            public CultureAwaiter(Task<T> task)
            {
                _task = task;
            }

            /// <summary>Gets the awaiter.</summary>
            /// <returns>The awaiter.</returns>
            public CultureAwaiter<T> GetAwaiter()
            {
                return this;
            }

            /// <summary>Gets a value indicating whether this TaskExtensions is completed.</summary>
            /// <value>True if this TaskExtensions is completed, false if not.</value>
            public bool IsCompleted => _task.IsCompleted;

            /// <summary>Gets the result.</summary>
            /// <returns>The result.</returns>
            public T GetResult()
            {
                return _task.GetAwaiter().GetResult();
            }

            /// <inheritdoc/>
            public void OnCompleted(Action continuation)
            {
                throw new NotImplementedException();
            }

            /// <inheritdoc/>
            public void UnsafeOnCompleted(Action continuation)
            {
                var currentCulture = Thread.CurrentThread.CurrentCulture;
                var currentUiCulture = Thread.CurrentThread.CurrentUICulture;
                _task.ConfigureAwait(false)
                    .GetAwaiter()
                    .UnsafeOnCompleted(
                        () =>
                        {
                            var currentCulture1 = Thread.CurrentThread.CurrentCulture;
                            var currentUiCulture1 = Thread.CurrentThread.CurrentUICulture;
                            Thread.CurrentThread.CurrentCulture = currentCulture;
                            Thread.CurrentThread.CurrentUICulture = currentUiCulture;
                            try
                            {
                                continuation();
                            }
                            finally
                            {
                                Thread.CurrentThread.CurrentCulture = currentCulture1;
                                Thread.CurrentThread.CurrentUICulture = currentUiCulture1;
                            }
                        });
            }
        }

        /// <summary>A culture awaiter.</summary>
        public struct CultureAwaiter : ICriticalNotifyCompletion, INotifyCompletion
        {
            /// <summary>The task.</summary>
            private readonly Task _task;

            /// <summary>Initializes a new instance of the <see cref="Microsoft.AspNet.Identity.TaskExtensions" /> class.</summary>
            /// <param name="task">The task.</param>
            public CultureAwaiter(Task task)
            {
                _task = task;
            }

            /// <summary>Gets the awaiter.</summary>
            /// <returns>The awaiter.</returns>
            public CultureAwaiter GetAwaiter()
            {
                return this;
            }

            /// <summary>Gets a value indicating whether this TaskExtensions is completed.</summary>
            /// <value>True if this TaskExtensions is completed, false if not.</value>
            public bool IsCompleted => _task.IsCompleted;

            /// <summary>Gets the result.</summary>
            public void GetResult()
            {
                _task.GetAwaiter().GetResult();
            }

            /// <inheritdoc/>
            public void OnCompleted(Action continuation)
            {
                throw new NotImplementedException();
            }

            /// <inheritdoc/>
            public void UnsafeOnCompleted(Action continuation)
            {
                var currentCulture = Thread.CurrentThread.CurrentCulture;
                var currentUiCulture = Thread.CurrentThread.CurrentUICulture;
                _task.ConfigureAwait(false)
                    .GetAwaiter()
                    .UnsafeOnCompleted(
                        () =>
                        {
                            var currentCulture1 = Thread.CurrentThread.CurrentCulture;
                            var currentUiCulture1 = Thread.CurrentThread.CurrentUICulture;
                            Thread.CurrentThread.CurrentCulture = currentCulture;
                            Thread.CurrentThread.CurrentUICulture = currentUiCulture;
                            try
                            {
                                continuation();
                            }
                            finally
                            {
                                Thread.CurrentThread.CurrentCulture = currentCulture1;
                                Thread.CurrentThread.CurrentUICulture = currentUiCulture1;
                            }
                        });
            }
        }
    }
}
