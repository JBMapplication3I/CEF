﻿// <copyright file="SerializableAttributeObject.cs" company="clarity-ventures.com">
// Copyright (c) 2016-2023 clarity-ventures.com. All rights reserved.
// </copyright>
// <summary>Implements the serializable attribute object class</summary>
// ReSharper disable UnusedAutoPropertyAccessor.Global
namespace GeneratePdfs.Models
{
    using System.ComponentModel;

    /// <summary>A serializable attribute object.</summary>
    public class SerializableAttributeObject
    {
        /// <summary>Gets or sets the identifier of the General Attribute that this value is for.</summary>
        /// <value>The identifier of the General Attribute that this value is for.</value>
        [DefaultValue(0)]
        public int ID { get; set; }

        /// <summary>Gets or sets the Key of the General Attribute that this value is for.</summary>
        /// <value>The Key of the General Attribute that this value is for.</value>
        [DefaultValue(null)]
        public string Key { get; set; } = null!;

        /// <summary>Gets or sets the value.</summary>
        /// <value>The value.</value>
        public string Value { get; set; } = null!;

        /// <summary>Gets or sets the unit of measure.</summary>
        /// <value>The unit of measure.</value>
        [DefaultValue(null)]
        public string UofM { get; set; } = string.Empty;

        /// <summary>Gets or sets the sort order.</summary>
        /// <value>The sort order.</value>
        [DefaultValue(null)]
        public int? SortOrder { get; set; } = null;
    }
}
