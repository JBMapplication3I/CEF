// <copyright file="ProductCategory.cs" company="clarity-ventures.com">
// Copyright (c) 2016-2023 clarity-ventures.com. All rights reserved.
// </copyright>
// <summary>Implements the product category class</summary>
#nullable enable
namespace Clarity.Ecommerce.Interfaces.DataModel
{
    using Ecommerce.DataModel;

    public interface IProductCategory
        : IAmAProductRelationshipTableWhereProductIsTheMaster<Category>,
            IAmACategoryRelationshipTableWhereCategoryIsTheSlave<Product>
    {
        /// <summary>Gets or sets the sort order.</summary>
        /// <value>The sort order.</value>
        int? SortOrder { get; set; }
    }
}

namespace Clarity.Ecommerce.DataModel
{
    using System;
    using System.ComponentModel;
    using System.ComponentModel.DataAnnotations.Schema;
    using Interfaces.DataModel;
    using Newtonsoft.Json;

    [SqlSchema("Products", "ProductCategory")]
    public class ProductCategory : Base, IProductCategory
    {
        #region IAmARelationshipTable<Product, Category>
        /// <inheritdoc/>
        [InverseProperty(nameof(ID)), ForeignKey(nameof(Master)), DefaultValue(null)]
        public int MasterID { get; set; }

        /// <inheritdoc/>
        [DefaultValue(null), JsonIgnore]
        public virtual Product? Master { get; set; }

        /// <inheritdoc/>
        [InverseProperty(nameof(ID)), ForeignKey(nameof(Slave)), DefaultValue(null)]
        public int SlaveID { get; set; }

        /// <inheritdoc/>
        [ForceMapOutWithLite, DefaultValue(null), JsonIgnore]
        public virtual Category? Slave { get; set; }

        #region IAmFilterableByProduct
        /// <inheritdoc/>
        [NotMapped, JsonIgnore, Obsolete("Cannot use in queries, use MasterID instead.")]
        int IAmFilterableByProduct.ProductID { get => MasterID; set => MasterID = value; }

        /// <inheritdoc/>
        [NotMapped, JsonIgnore, Obsolete("Cannot use in queries, use Master instead.")]
        Product? IAmFilterableByProduct.Product { get => Master; set => Master = value; }
        #endregion

        #region IAmAProductRelationshipTableWhereProductIsTheMaster
        /// <inheritdoc/>
        [NotMapped, JsonIgnore, Obsolete("Cannot use in queries, use MasterID instead.")]
        int IAmAProductRelationshipTableWhereProductIsTheMaster<Category>.ProductID { get => MasterID; set => MasterID = value; }

        /// <inheritdoc/>
        [NotMapped, JsonIgnore, Obsolete("Cannot use in queries, use Master instead.")]
        Product? IAmAProductRelationshipTableWhereProductIsTheMaster<Category>.Product { get => Master; set => Master = value; }
        #endregion

        #region IAmFilterableByCategory
        /// <inheritdoc/>
        [NotMapped, JsonIgnore, Obsolete("Cannot use in queries, use SlaveID instead.")]
        int IAmFilterableByCategory.CategoryID { get => SlaveID; set => SlaveID = value; }

        /// <inheritdoc/>
        [NotMapped, JsonIgnore, Obsolete("Cannot use in queries, use Slave instead.")]
        Category? IAmFilterableByCategory.Category { get => Slave; set => Slave = value; }
        #endregion

        #region IAmACategoryRelationshipTableWhereCategoryIsTheSlave
        /// <inheritdoc/>
        [NotMapped, JsonIgnore, Obsolete("Cannot use in queries, use SlaveID instead.")]
        int IAmACategoryRelationshipTableWhereCategoryIsTheSlave<Product>.CategoryID { get => SlaveID; set => SlaveID = value; }

        /// <inheritdoc/>
        [NotMapped, JsonIgnore, Obsolete("Cannot use in queries, use Slave instead.")]
        Category? IAmACategoryRelationshipTableWhereCategoryIsTheSlave<Product>.Category { get => Slave; set => Slave = value; }
        #endregion
        #endregion

        /// <inheritdoc/>
        [DefaultValue(null)]
        public int? SortOrder { get; set; }
    }
}
