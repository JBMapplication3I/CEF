// <copyright file="ImportItem.cs" company="clarity-ventures.com">
// Copyright (c) 2019-2023 clarity-ventures.com. All rights reserved.
// </copyright>
// <summary>Implements the import item class</summary>
namespace Clarity.Ecommerce.Models.Import
{
    using System.Collections.Generic;
    using Interfaces.Models;
    using Interfaces.Models.Import;
    using Newtonsoft.Json;

    /// <summary>An import item.</summary>
    /// <seealso cref="IImportItem"/>
    public class ImportItem : IImportItem
    {
        /// <summary>Gets or sets the fields.</summary>
        /// <value>The fields.</value>
        public List<IImportField>? Fields { get; set; }

        /// <inheritdoc/>
        public override string ToString()
        {
            return JsonConvert.SerializeObject(this, SerializableAttributesDictionaryExtensions.JsonSettings);
        }
    }
}
