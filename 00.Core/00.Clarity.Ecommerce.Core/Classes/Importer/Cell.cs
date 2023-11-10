// <copyright file="Cell.cs" company="clarity-ventures.com">
// Copyright (c) 2017-2023 clarity-ventures.com. All rights reserved.
// </copyright>
// <summary>Implements the cell class</summary>
namespace Clarity.Ecommerce.Interfaces.Providers.Importer
{
    /// <summary>A cell.</summary>
    public class Cell
    {
        /// <summary>Gets or sets the column.</summary>
        /// <value>The column.</value>
        public uint Column { get; set; }

        /// <summary>Gets or sets the value.</summary>
        /// <value>The value.</value>
        public string? Value { get; set; }

        /// <summary>Gets or sets the unit of measure.</summary>
        /// <value>The unit of measure.</value>
        public string? UofM { get; set; }
    }
}
