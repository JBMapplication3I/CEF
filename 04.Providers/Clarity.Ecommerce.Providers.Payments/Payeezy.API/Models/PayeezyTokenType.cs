// <copyright file="PayeezyTokenType.cs" company="clarity-ventures.com">
// Copyright (c) 2018-2023 clarity-ventures.com. All rights reserved.
// </copyright>
// <summary>Implements the payeezy token type class</summary>
#pragma warning disable IDE1006 // Naming Styles
namespace Clarity.Ecommerce.Providers.Payments.PayeezyAPI
{
    using System;
    using System.Runtime.Serialization;
    using JetBrains.Annotations;
    using Newtonsoft.Json;
    using ServiceStack;

    /// <summary>(Serializable)a payeezy token type.</summary>
    [PublicAPI]
    [Serializable]
    public class PayeezyTokenType
    {
        /// <summary>Gets or sets the type.</summary>
        /// <value>The type.</value>
        [DataMember(Name = "type"), JsonProperty("type"), ApiMember(Name = nameof(type))]
        public string? type { get; set; }
    }
}
