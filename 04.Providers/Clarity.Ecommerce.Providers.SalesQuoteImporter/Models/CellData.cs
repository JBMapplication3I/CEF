// <copyright file="CellData.cs" company="clarity-ventures.com">
// Copyright (c) 2018-2023 clarity-ventures.com. All rights reserved.
// </copyright>
// <summary>Implements the cell data class</summary>
namespace Clarity.Ecommerce.Providers.SalesQuoteImporter
{
    using Interfaces.Models;
    using Newtonsoft.Json;

    /// <summary>A cell data.</summary>
    public class CellData
    {
        /// <summary>Gets or sets the row.</summary>
        /// <value>The row.</value>
        public int Row { get; set; }

        /// <summary>Gets or sets the column.</summary>
        /// <value>The column.</value>
        public int Column { get; set; }

        /// <summary>Gets or sets the cell reference.</summary>
        /// <value>The cell reference.</value>
        public string CellReference { get; set; }

        /// <summary>Gets or sets the header.</summary>
        /// <value>The header.</value>
        public string[] Header { get; set; }

        /// <summary>Gets or sets the header occurrence.</summary>
        /// <value>The header occurrence.</value>
        public int HeaderOccurrence { get; set; }

        /// <summary>Gets or sets the value.</summary>
        /// <value>The value.</value>
        public string Value { get; set; }

        /// <summary>Returns a string that represents the current object.</summary>
        /// <returns>A string that represents the current object.</returns>
        public override string ToString()
        {
            return JsonConvert.SerializeObject(this, SerializableAttributesDictionaryExtensions.JsonSettings);
        }
    }
}
