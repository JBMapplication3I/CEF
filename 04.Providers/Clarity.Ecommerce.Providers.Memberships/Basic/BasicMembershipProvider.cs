// <copyright file="BasicMembershipProvider.cs" company="clarity-ventures.com">
// Copyright (c) 2016-2023 clarity-ventures.com. All rights reserved.
// </copyright>
// <summary>Implements the basic membership provider class</summary>
namespace Clarity.Ecommerce.Providers.Memberships.Basic
{
    /// <summary>A basic membership provider.</summary>
    /// <seealso cref="MembershipProviderBase"/>
    public class BasicMembershipProvider : MembershipProviderBase
    {
        /// <inheritdoc/>
        public override bool HasValidConfiguration
            => BasicMembershipProviderConfig.IsValid(IsDefaultProvider && IsDefaultProviderActivated);

        /// <inheritdoc/>
        public override bool IsDefaultProvider => true;
    }
}
