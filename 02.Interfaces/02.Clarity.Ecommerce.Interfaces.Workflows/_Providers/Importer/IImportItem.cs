// <copyright file="IImportItem.cs" company="clarity-ventures.com">
// Copyright (c) 2019-2023 clarity-ventures.com. All rights reserved.
// </copyright>
// <summary>Declares the IImportItem interface</summary>
namespace Clarity.Ecommerce.Interfaces.Models.Import
{
    using System.Collections.Generic;

    /// <summary>Interface for import item.</summary>
    public interface IImportItem
    {
        /// <summary>Gets or sets the fields.</summary>
        /// <value>The fields.</value>
        List<IImportField>? Fields { get; set; }
    }
}
