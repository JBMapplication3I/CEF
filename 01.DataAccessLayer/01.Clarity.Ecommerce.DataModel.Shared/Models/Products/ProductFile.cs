// <copyright file="ProductFile.cs" company="clarity-ventures.com">
// Copyright (c) 2016-2023 clarity-ventures.com. All rights reserved.
// </copyright>
// <summary>Implements the product file class</summary>
#nullable enable
namespace Clarity.Ecommerce.Interfaces.DataModel
{
    using Ecommerce.DataModel;

    public interface IProductFile : IAmAStoredFileRelationshipTable<Product>
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

    [SqlSchema("Products", "ProductFile")]
    public class ProductFile : NameableBase, IProductFile
    {
        #region IHaveSeoBase Properties
        // TODO: Display All of these properties in the editor of Admin UI

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

        #region IAmAStoredFileRelationshipTable<Product>
        // TODO: Display All of these properties in the editor of Admin UI

        /// <inheritdoc/>
        [InverseProperty(nameof(ID)), ForeignKey(nameof(Master))]
        public int MasterID { get; set; }

        /// <inheritdoc/>
        [DontMapInWithRelateWorkflows, DontMapOutEver, DefaultValue(null), JsonIgnore]
        public virtual Product? Master { get; set; }

        /// <inheritdoc/>
        [InverseProperty(nameof(ID)), ForeignKey(nameof(Slave)), DefaultValue(0)]
        public int SlaveID { get; set; }

        /// <inheritdoc/>
        [ForceMapOutWithListing, DefaultValue(null), JsonIgnore]
        public virtual StoredFile? Slave { get; set; }

        /// <inheritdoc/>
        [DefaultValue(0)]
        public int FileAccessTypeID { get; set; }

        /// <inheritdoc/>
        [DefaultValue(null)]
        public int? SortOrder { get; set; }
        #endregion
    }
}
