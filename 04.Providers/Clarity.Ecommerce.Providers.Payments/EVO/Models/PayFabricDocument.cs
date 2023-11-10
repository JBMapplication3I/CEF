// <copyright file="PayFabricDocument.cs" company="clarity-ventures.com">
// Copyright (c) 2020-2023 clarity-ventures.com. All rights reserved.
// </copyright>
// <summary>Implements the pay fabric document class</summary>
namespace Clarity.Ecommerce.Providers.Payments.EVO
{
    /// <summary>A pay fabric document.</summary>
    public class PayFabricDocument
    {
        /// <summary>Gets or sets the default bill to.</summary>
        /// <value>The default bill to.</value>
        public object? DefaultBillTo { get; set; }

        /// <summary>Gets or sets the head.</summary>
        /// <value>The head.</value>
        public PayFabricHead[]? Head { get; set; }

        /// <summary>Gets or sets the lines.</summary>
        /// <value>The lines.</value>
        public object[]? Lines { get; set; }

        /// <summary>Gets or sets the user defined.</summary>
        /// <value>The user defined.</value>
        public object[]? UserDefined { get; set; }
    }
}
