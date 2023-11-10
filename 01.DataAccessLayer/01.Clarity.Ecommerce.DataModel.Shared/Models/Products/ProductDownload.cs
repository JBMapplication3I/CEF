// <copyright file="ProductDownload.cs" company="clarity-ventures.com">
// Copyright (c) 2020-2023 clarity-ventures.com. All rights reserved.
// </copyright>
// <summary>Implements the product download class</summary>
#nullable enable
namespace Clarity.Ecommerce.Interfaces.DataModel
{
    using Ecommerce.DataModel;

    public interface IProductDownload
        : INameableBase,
          IHaveATypeBase<ProductDownloadType>,
          IAmFilterableByProduct
    {
        #region ProductDownload Properties
        /// <summary>Gets or sets a value indicating whether this Download use absolute URL.</summary>
        /// <value>True if this Download uses absolute url, false if not.</value>
        bool IsAbsoluteUrl { get; set; }

        /// <summary>Gets or sets URL of the absolute.</summary>
        /// <value>The absolute URL.</value>
        string? AbsoluteUrl { get; set; }

        /// <summary>Gets or sets URL of the relative.</summary>
        /// <value>The relative URL.</value>
        string? RelativeUrl { get; set; }
        #endregion
    }
}

namespace Clarity.Ecommerce.DataModel
{
    using System.ComponentModel;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using Interfaces.DataModel;
    using Newtonsoft.Json;

    [SqlSchema("Products", "ProductDownload")]
    public class ProductDownload : NameableBase, IProductDownload
    {
        #region IHaveATypeBase<ProductType>
        /// <inheritdoc/>
        [InverseProperty(nameof(ID)), ForeignKey(nameof(Type)), DefaultValue(0)]
        public int TypeID { get; set; }

        /// <inheritdoc/>
        [ForceMapOutWithLite, DefaultValue(null), JsonIgnore]
        public virtual ProductDownloadType? Type { get; set; }
        #endregion

        #region ProductDownload Properties
        /// <inheritdoc/>
        [DefaultValue(false)]
        public bool IsAbsoluteUrl { get; set; }

        /// <inheritdoc/>
        [StringLength(512), StringIsUnicode(false), DefaultValue(null)]
        public string? AbsoluteUrl { get; set; }

        /// <inheritdoc/>
        [StringLength(512), StringIsUnicode(false), DefaultValue(null)]
        public string? RelativeUrl { get; set; }
        #endregion

        #region Related Objects
        /// <inheritdoc/>
        [InverseProperty(nameof(ID)), ForeignKey(nameof(Product)), DefaultValue(0)]
        public int ProductID { get; set; }

        /// <inheritdoc/>
        [DontMapOutEver, AllowMapInWithRelateWorkflowsButDontAutoGenerate, DefaultValue(null), JsonIgnore]
        public virtual Product? Product { get; set; }
        #endregion
    }
}
