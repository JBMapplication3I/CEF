// <copyright file="ProductAssociationModel.extended.cs" company="clarity-ventures.com">
// Copyright (c) 2016-2023 clarity-ventures.com. All rights reserved.
// </copyright>
// <summary>Implements the product association model class</summary>
namespace Clarity.Ecommerce.Models
{
    using Interfaces.Models;

    /// <summary>A data Model for the product association.</summary>
    /// <seealso cref="BaseModel"/>
    /// <seealso cref="IProductAssociationModel"/>
    public partial class ProductAssociationModel
    {
        #region ProductAssociation Properties
        /// <inheritdoc/>
        public decimal? Quantity { get; set; }

        /// <inheritdoc/>
        public string? UnitOfMeasure { get; set; }

        /// <inheritdoc/>
        public int? SortOrder { get; set; }
        #endregion

        #region Related Objects
        /// <inheritdoc/>
        public string? MasterSeoUrl { get; set; }

        /// <inheritdoc/>
        public string? MasterPrimaryImageFileName { get; set; }

        /// <inheritdoc/>
        public bool MasterIsVisible { get; set; }

        /// <inheritdoc/>
        public SerializableAttributesDictionary? MasterSerializableAttributes { get; set; }

        /// <inheritdoc/>
        public string? SlaveSeoUrl { get; set; }

        /// <inheritdoc/>
        public string? SlavePrimaryImageFileName { get; set; }

        /// <inheritdoc/>
        public bool SlaveIsVisible { get; set; }

        /// <inheritdoc/>
        public SerializableAttributesDictionary? SlaveSerializableAttributes { get; set; }
        #endregion
    }
}
