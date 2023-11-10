// <copyright file="ImportField.cs" company="clarity-ventures.com">
// Copyright (c) 2019-2023 clarity-ventures.com. All rights reserved.
// </copyright>
// <summary>Implements the import field class</summary>
namespace Clarity.Ecommerce.Models.Import
{
    using Interfaces.Models;
    using Interfaces.Models.Import;
    using Newtonsoft.Json;

    /// <summary>An import field.</summary>
    /// <seealso cref="IImportField"/>
    public class ImportField : IImportField
    {
        /// <summary>Gets or sets the name.</summary>
        /// <value>The name.</value>
        public string? Name { get; set; }

        /// <summary>Gets or sets the value.</summary>
        /// <value>The value.</value>
        public string? Value { get; set; }

        /// <inheritdoc/>
        public override string ToString()
        {
            return JsonConvert.SerializeObject(this, SerializableAttributesDictionaryExtensions.JsonSettings);
        }
    }
}
