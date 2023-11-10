// <copyright file="IImportField.cs" company="clarity-ventures.com">
// Copyright (c) 2019-2023 clarity-ventures.com. All rights reserved.
// </copyright>
// <summary>Declares the IImportField interface</summary>
namespace Clarity.Ecommerce.Interfaces.Models.Import
{
    /// <summary>Interface for import field.</summary>
    public interface IImportField
    {
        /// <summary>Gets or sets the name.</summary>
        /// <value>The name.</value>
        string? Name { get; set; }

        /// <summary>Gets or sets the value.</summary>
        /// <value>The value.</value>
        string? Value { get; set; }
    }
}
