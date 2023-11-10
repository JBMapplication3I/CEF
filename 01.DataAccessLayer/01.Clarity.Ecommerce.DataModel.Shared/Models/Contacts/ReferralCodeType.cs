// <copyright file="ReferralCodeType.cs" company="clarity-ventures.com">
// Copyright (c) 2017-2023 clarity-ventures.com. All rights reserved.
// </copyright>
// <summary>Implements the referral code type class</summary>
#nullable enable
namespace Clarity.Ecommerce.Interfaces.DataModel
{
    public interface IReferralCodeType : ITypableBase
    {
    }
}

namespace Clarity.Ecommerce.DataModel
{
    using Interfaces.DataModel;

    [SqlSchema("Contacts", "ReferralCodeType")]
    public class ReferralCodeType : TypableBase, IReferralCodeType
    {
    }
}
