// <copyright file="ChattingProviderRegistry.cs" company="clarity-ventures.com">
// Copyright (c) 2020-2023 clarity-ventures.com. All rights reserved.
// </copyright>
// <summary>Implements the Chatting Provider StructureMap 4 Registry to associate the interfaces with their concretes</summary>
namespace Clarity.Ecommerce.Providers.Chatting
{
    using Interfaces.Providers.Chatting;
    using StructureMap;
    using StructureMap.Pipeline;
    using WeChatInt;

    /// <summary>The chatting provider registry.</summary>
    /// <seealso cref="Registry"/>
    [JetBrains.Annotations.PublicAPI]
    public class ChattingProviderRegistry : Registry
    {
        /// <summary>Initializes a new instance of the <see cref="ChattingProviderRegistry"/> class.</summary>
        public ChattingProviderRegistry()
        {
            For<IChattingProviderBase>(new SingletonLifecycle()).Add<WeChatChattingProvider>();
        }
    }
}
