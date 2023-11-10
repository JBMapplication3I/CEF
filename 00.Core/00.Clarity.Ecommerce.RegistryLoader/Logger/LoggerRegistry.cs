// <copyright file="LoggerRegistry.cs" company="clarity-ventures.com">
// Copyright (c) 2018-2023 clarity-ventures.com. All rights reserved.
// </copyright>
// <summary>Implements the logger registry class</summary>
namespace Clarity.Ecommerce
{
#if NET5_0_OR_GREATER
    using Lamar;
#else
    using StructureMap;
    using StructureMap.Pipeline;
#endif

#if NET5_0_OR_GREATER
    /// <summary>A logger registry.</summary>
    /// <seealso cref="ServiceRegistry"/>
    [JetBrains.Annotations.PublicAPI]
    public class LoggerRegistry : ServiceRegistry
#else
    /// <summary>A logger registry.</summary>
    /// <seealso cref="Registry"/>
    [JetBrains.Annotations.PublicAPI]
    public class LoggerRegistry : Registry
#endif
    {
        /// <summary>Initializes a new instance of the <see cref="LoggerRegistry"/> class.</summary>
        public LoggerRegistry()
        {
#if NET5_0_OR_GREATER
            Use<Logger>().Singleton().For<ILogger>();
#else
            For<ILogger>(new SingletonLifecycle()).Use<Logger>();
#endif
        }
    }
}
