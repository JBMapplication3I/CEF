// <copyright file="PersonalizationProviderBase.cs" company="clarity-ventures.com">
// Copyright (c) 2017-2023 clarity-ventures.com. All rights reserved.
// </copyright>
// <summary>Implements the personalization provider base class</summary>
namespace Clarity.Ecommerce.Providers.Personalization
{
    using System.Collections.Generic;
    using System.Threading.Tasks;
    using Interfaces.Providers.Personalization;

    /// <summary>A personalization provider base.</summary>
    /// <seealso cref="ProviderBase"/>
    /// <seealso cref="IPersonalizationProviderBase"/>
    public abstract class PersonalizationProviderBase : ProviderBase, IPersonalizationProviderBase
    {
        /// <inheritdoc/>
        public override Enums.ProviderType ProviderType => Enums.ProviderType.Personalization;

        /// <inheritdoc/>
        public override bool HasDefaultProvider => false;

        /// <inheritdoc/>
        public override bool IsDefaultProvider => false;

        /// <inheritdoc/>
        public override bool IsDefaultProviderActivated { get; set; }

        /// <inheritdoc/>
        public abstract Task<List<int>> GetResultingProductIDsForUserIDAsync(
            int? userID, int minimum, string? contextProfileName);

        /// <inheritdoc/>
        public abstract Task<List<int>> GetResultingCategoryIDsForUserIDAsync(
            int? userID, int minimum, string? contextProfileName);

        /// <inheritdoc/>
        public abstract Task<Dictionary<int /*categoryID*/, List<int> /*productIDs*/>> GetResultingFeedForUserIDAsync(
            int? userID, int minimum, string? contextProfileName);
    }
}
