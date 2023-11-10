// <copyright file="SageName.cs" company="clarity-ventures.com">
// Copyright (c) 2018-2023 clarity-ventures.com. All rights reserved.
// </copyright>
// <summary>Implements the sage name class</summary>
#pragma warning disable IDE1006 // Naming Styles
namespace Clarity.Ecommerce.Providers.Payments.Sage
{
    using System;
    using JetBrains.Annotations;

    /// <summary>(Serializable)a sage name.</summary>
    [PublicAPI, Serializable]
    public class SageName
    {
        /// <summary>Gets or sets the first.</summary>
        /// <value>The first.</value>
        public string? first { get; set; }

        /// <summary>Gets or sets the last.</summary>
        /// <value>The last.</value>
        public string? last { get; set; }
    }
}
