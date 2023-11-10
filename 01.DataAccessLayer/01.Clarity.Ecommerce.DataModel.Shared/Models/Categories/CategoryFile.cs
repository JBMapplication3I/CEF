// <copyright file="CategoryFile.cs" company="clarity-ventures.com">
// Copyright (c) 2016-2023 clarity-ventures.com. All rights reserved.
// </copyright>
// <summary>Implements the category file class</summary>
#nullable enable
namespace Clarity.Ecommerce.Interfaces.DataModel
{
    using Ecommerce.DataModel;

    public interface ICategoryFile
        : IAmAStoredFileRelationshipTable<Category>,
            IAmACategoryRelationshipTableWhereCategoryIsTheMaster<StoredFile>
    {
    }
}

namespace Clarity.Ecommerce.DataModel
{
    using System;
    using System.ComponentModel;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using Interfaces.DataModel;
    using Newtonsoft.Json;

    [SqlSchema("Categories", "CategoryFile")]
    public class CategoryFile : NameableBase, ICategoryFile
    {
        #region IHaveSeoBase Properties
        /// <inheritdoc/>
        [StringLength(512), StringIsUnicode(false), DefaultValue(null)]
        public string? SeoUrl { get; set; }

        /// <inheritdoc/>
        [StringLength(512), StringIsUnicode(false), DefaultValue(null), DontMapOutWithListing]
        public string? SeoKeywords { get; set; }

        /// <inheritdoc/>
        [StringLength(75), StringIsUnicode(false), DefaultValue(null), DontMapOutWithListing]
        public string? SeoPageTitle { get; set; }

        /// <inheritdoc/>
        [StringLength(256), StringIsUnicode(false), DefaultValue(null), DontMapOutWithListing]
        public string? SeoDescription { get; set; }

        /// <inheritdoc/>
        [StringLength(512), StringIsUnicode(false), DefaultValue(null), DontMapOutWithListing]
        public string? SeoMetaData { get; set; }
        #endregion

        #region IAmAStoredFileRelationshipTable<Category>
        /// <inheritdoc/>
        [InverseProperty(nameof(ID)), ForeignKey(nameof(Master)), DefaultValue(0)]
        public int MasterID { get; set; }

        /// <inheritdoc/>
        [DontMapInWithRelateWorkflows, DontMapOutEver, DefaultValue(null), JsonIgnore]
        public virtual Category? Master { get; set; }

        /// <inheritdoc/>
        [InverseProperty(nameof(ID)), ForeignKey(nameof(Slave)), DefaultValue(0)]
        public int SlaveID { get; set; }

        /// <inheritdoc/>
        [DefaultValue(null), JsonIgnore]
        public virtual StoredFile? Slave { get; set; }

        /// <inheritdoc/>
        [DefaultValue(0)]
        public int FileAccessTypeID { get; set; }

        /// <inheritdoc/>
        [DefaultValue(null)]
        public int? SortOrder { get; set; }
        #endregion

        #region IAmACategoryRelationshipTableWhereCategoryIsTheMaster<StoredFile>
        /// <inheritdoc/>
        [NotMapped, JsonIgnore, Obsolete("Cannot use in queries, use MasterID instead.", true)]
        int IAmFilterableByCategory.CategoryID { get => MasterID; set => MasterID = value; }

        /// <inheritdoc/>
        [NotMapped, JsonIgnore, Obsolete("Cannot use in queries, use Master instead.", true)]
        Category? IAmFilterableByCategory.Category { get => Master; set => Master = value; }

        /// <inheritdoc/>
        [NotMapped, JsonIgnore, Obsolete("Cannot use in queries, use MasterID instead.", true)]
        int IAmACategoryRelationshipTableWhereCategoryIsTheMaster<StoredFile>.CategoryID { get => MasterID; set => MasterID = value; }

        /// <inheritdoc/>
        [NotMapped, JsonIgnore, Obsolete("Cannot use in queries, use Master instead.", true)]
        Category? IAmACategoryRelationshipTableWhereCategoryIsTheMaster<StoredFile>.Category { get => Master; set => Master = value; }
        #endregion
    }
}
