// <copyright file="IProductAssociationModel.extended.cs" company="clarity-ventures.com">
// Copyright (c) 2016-2023 clarity-ventures.com. All rights reserved.
// </copyright>
// <summary>Declares the IProductAssociationModel interface</summary>
namespace Clarity.Ecommerce.Interfaces.Models
{
    /// <summary>Interface for product association model.</summary>
    public partial interface IProductAssociationModel
    {
        #region ProductAssociation Properties
        /// <summary>Gets or sets the quantity.</summary>
        /// <value>The quantity.</value>
        decimal? Quantity { get; set; }

        /// <summary>Gets or sets the unit of measure.</summary>
        /// <value>The unit of measure.</value>
        string? UnitOfMeasure { get; set; }

        /// <summary>Gets or sets the sort order.</summary>
        /// <value>The sort order.</value>
        int? SortOrder { get; set; }
        #endregion

        #region Related Objects
        /// <summary>Gets or sets URL of the master SEO.</summary>
        /// <value>The master SEO URL.</value>
        string? MasterSeoUrl { get; set; }

        /// <summary>Gets or sets the filename of the master primary image file.</summary>
        /// <value>The filename of the master primary image file.</value>
        string? MasterPrimaryImageFileName { get; set; }

        /// <summary>Gets or sets a value indicating whether the master is is visible.</summary>
        /// <value>True if master is visible, false if not.</value>
        bool MasterIsVisible { get; set; }

        /// <summary>Gets or sets the master serializable attributes.</summary>
        /// <value>The master serializable attributes.</value>
        SerializableAttributesDictionary? MasterSerializableAttributes { get; set; }

        /// <summary>Gets or sets URL of the slave SEO.</summary>
        /// <value>The slave SEO URL.</value>
        string? SlaveSeoUrl { get; set; }

        /// <summary>Gets or sets the filename of the slave primary image file.</summary>
        /// <value>The filename of the slave primary image file.</value>
        string? SlavePrimaryImageFileName { get; set; }

        /// <summary>Gets or sets a value indicating whether the slave is is visible.</summary>
        /// <value>True if slave is visible, false if not.</value>
        bool SlaveIsVisible { get; set; }

        /// <summary>Gets or sets the slave serializable attributes.</summary>
        /// <value>The slave serializable attributes.</value>
        SerializableAttributesDictionary? SlaveSerializableAttributes { get; set; }
        #endregion
    }
}
