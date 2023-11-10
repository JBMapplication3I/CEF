// <copyright file="WeChatChattingProviderConfig.cs" company="clarity-ventures.com">
// Copyright (c) 2017-2023 clarity-ventures.com. All rights reserved.
// </copyright>
// <summary>Implements the we chat chatting provider configuration class</summary>
namespace Clarity.Ecommerce.Providers.Chatting.WeChatInt
{
    using Interfaces.Providers;

    /// <summary>A WeChat chatting provider.</summary>
    internal static class WeChatChattingProviderConfig
    {
        /// <summary>Query if this Config is valid.</summary>
        /// <param name="isDefaultAndActivated">True if this Provider is default and activated.</param>
        /// <returns>true if valid, false if not.</returns>
        internal static bool IsValid(bool isDefaultAndActivated)
            => ProviderConfig.CheckIsEnabledBySettings<WeChatChattingProvider>() || isDefaultAndActivated;
    }
}
