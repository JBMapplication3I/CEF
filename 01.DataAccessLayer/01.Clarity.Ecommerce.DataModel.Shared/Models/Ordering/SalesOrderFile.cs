﻿// <copyright file="SalesOrderFile.cs" company="clarity-ventures.com">
// Copyright (c) 2016-2023 clarity-ventures.com. All rights reserved.
// </copyright>
// <summary>Implements the sales order file class</summary>
#nullable enable
namespace Clarity.Ecommerce.Interfaces.DataModel
{
    using Ecommerce.DataModel;

    public interface ISalesOrderFile : IAmAStoredFileRelationshipTable<SalesOrder>
    {
    }
}

namespace Clarity.Ecommerce.DataModel
{
    using System.ComponentModel;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using Interfaces.DataModel;
    using Newtonsoft.Json;

    [SqlSchema("Ordering", "SalesOrderFile")]
    public class SalesOrderFile : NameableBase, ISalesOrderFile
    {
        #region IHaveSeoBase Properties
        /// <inheritdoc/>
        [StringLength(512), StringIsUnicode(false), DefaultValue(null)]
        public string? SeoUrl { get; set; }

        /// <inheritdoc/>
        [StringLength(512), StringIsUnicode(false), DontMapOutWithListing, DefaultValue(null)]
        public string? SeoKeywords { get; set; }

        /// <inheritdoc/>
        [StringLength(75), StringIsUnicode(false), DontMapOutWithListing, DefaultValue(null)]
        public string? SeoPageTitle { get; set; }

        /// <inheritdoc/>
        [StringLength(256), StringIsUnicode(false), DontMapOutWithListing, DefaultValue(null)]
        public string? SeoDescription { get; set; }

        /// <inheritdoc/>
        [StringLength(512), StringIsUnicode(false), DontMapOutWithListing, DefaultValue(null)]
        public string? SeoMetaData { get; set; }
        #endregion

        #region IAmAStoredFileRelationshipTable<SalesOrder>
        [InverseProperty(nameof(ID)), ForeignKey(nameof(Master))]
        public int MasterID { get; set; }

        [DontMapInWithRelateWorkflows, DontMapOutEver, DefaultValue(null), JsonIgnore]
        public virtual SalesOrder? Master { get; set; }

        [InverseProperty(nameof(ID)), ForeignKey(nameof(Slave)), DefaultValue(0)]
        public int SlaveID { get; set; }

        [ForceMapOutWithListing, DefaultValue(null), JsonIgnore]
        public virtual StoredFile? Slave { get; set; }

        [DefaultValue(0)]
        public int FileAccessTypeID { get; set; }

        [DefaultValue(null)]
        public int? SortOrder { get; set; }
        #endregion
    }
}
