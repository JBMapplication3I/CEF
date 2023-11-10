// <copyright file="TaxRegion.cs" company="clarity-ventures.com">
// Copyright (c) 2016-2023 clarity-ventures.com. All rights reserved.
// </copyright>
// <summary>Implements the tax region class</summary>
#nullable enable
namespace Clarity.Ecommerce.Interfaces.DataModel
{
    using Ecommerce.DataModel;

    public interface ITaxRegion : INameableBase
    {
        /// <summary>Gets or sets the rate.</summary>
        /// <value>The rate.</value>
        decimal Rate { get; set; }

        #region Related Objects
        /// <summary>Gets or sets the identifier of the region.</summary>
        /// <value>The identifier of the region.</value>
        int RegionID { get; set; }

        /// <summary>Gets or sets the region.</summary>
        /// <value>The region.</value>
        Region? Region { get; set; }
        #endregion
    }
}

namespace Clarity.Ecommerce.DataModel
{
    using System.ComponentModel;
    using System.ComponentModel.DataAnnotations.Schema;
    using Interfaces.DataModel;
    using Newtonsoft.Json;

    [SqlSchema("Tax", "TaxRegion")]
    public class TaxRegion : NameableBase, ITaxRegion
    {
        /// <inheritdoc/>
        // [Column(TypeName = "numeric"), DecimalPrecision(7, 6)]
        public decimal Rate { get; set; }

        #region Related Objects
        /// <inheritdoc/>
        [InverseProperty(nameof(ID)), ForeignKey(nameof(Region)), DefaultValue(0)]
        public int RegionID { get; set; }

        /// <inheritdoc/>
        [AllowMapInWithRelateWorkflowsButDontAutoGenerate, ForceMapOutWithLite, DefaultValue(null), JsonIgnore]
        public virtual Region? Region { get; set; }
        #endregion
    }
}
