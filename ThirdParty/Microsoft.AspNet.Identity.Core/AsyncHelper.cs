// Decompiled with JetBrains decompiler
// Type: Microsoft.AspNet.Identity.AsyncHelper
// Assembly: Microsoft.AspNet.Identity.Core, Version=2.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35
// MVID: 9E2B2C84-E0DA-4554-B416-8EA71B0D085D
// Assembly location: C:\Users\jotha\.nuget\packages\microsoft.aspnet.identity.core\2.2.3\lib\net45\Microsoft.AspNet.Identity.Core.dll
[assembly: System.Runtime.CompilerServices.InternalsVisibleTo("Microsoft.AspNet.Identity.Owin")]
namespace Microsoft.AspNet.Identity
{
    using System;
    using System.Globalization;
    using System.Threading;
    using System.Threading.Tasks;

    /// <summary>An asynchronous helper.</summary>
    internal static class AsyncHelper
    {
        /// <summary>my task factory.</summary>
        private static readonly TaskFactory MyTaskFactory = new(
            CancellationToken.None,
            TaskCreationOptions.None,
            TaskContinuationOptions.None,
            TaskScheduler.Default);

        /// <summary>Executes the synchronise operation.</summary>
        /// <typeparam name="TResult">Type of the result.</typeparam>
        /// <param name="func">The function.</param>
        /// <returns>A TResult.</returns>
        public static TResult RunSync<TResult>(Func<Task<TResult>> func)
        {
            var cultureUi = CultureInfo.CurrentUICulture;
            var culture = CultureInfo.CurrentCulture;
            return MyTaskFactory.StartNew(
                    () =>
                    {
                        Thread.CurrentThread.CurrentCulture = culture;
                        Thread.CurrentThread.CurrentUICulture = cultureUi;
                        return func();
                    })
                .Unwrap()
                .GetAwaiter()
                .GetResult();
        }

        /// <summary>Executes the synchronise operation.</summary>
        /// <param name="func">The function.</param>
        public static void RunSync(Func<Task> func)
        {
            var cultureUi = CultureInfo.CurrentUICulture;
            var culture = CultureInfo.CurrentCulture;
            MyTaskFactory.StartNew(
                    () =>
                    {
                        Thread.CurrentThread.CurrentCulture = culture;
                        Thread.CurrentThread.CurrentUICulture = cultureUi;
                        return func();
                    })
                .Unwrap()
                .GetAwaiter()
                .GetResult();
        }
    }
}
