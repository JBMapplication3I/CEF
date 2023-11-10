// <copyright file="TaxDistrict.cs" company="clarity-ventures.com">
// Copyright (c) 2021-2023 clarity-ventures.com. All rights reserved.
// </copyright>
// <summary>Implements the tax district class</summary>
#nullable enable
namespace Clarity.Ecommerce.Interfaces.DataModel
{
    using Ecommerce.DataModel;

    public interface ITaxDistrict : INameableBase
    {
        /// <summary>Gets or sets the rate.</summary>
        /// <value>The rate.</value>
        decimal Rate { get; set; }

        #region Related Objects
        /// <summary>Gets or sets the identifier of the district.</summary>
        /// <value>The identifier of the district.</value>
        int DistrictID { get; set; }

        /// <summary>Gets or sets the district.</summary>
        /// <value>The district.</value>
        District? District { get; set; }
        #endregion
    }
}

namespace Clarity.Ecommerce.DataModel
{
    using System.ComponentModel;
    using System.ComponentModel.DataAnnotations.Schema;
    using Interfaces.DataModel;
    using Newtonsoft.Json;

    [SqlSchema("Tax", "TaxDistrict")]
    public class TaxDistrict : NameableBase, ITaxDistrict
    {
        /// <inheritdoc/>
        // [Column(TypeName = "numeric"), DecimalPrecision(7, 6)]
        public decimal Rate { get; set; }

        #region Related Objects
        /// <inheritdoc/>
        [InverseProperty(nameof(ID)), ForeignKey(nameof(District)), DefaultValue(0)]
        public int DistrictID { get; set; }

        /// <inheritdoc/>
        [AllowMapInWithRelateWorkflowsButDontAutoGenerate, ForceMapOutWithLite, DefaultValue(null), JsonIgnore]
        public virtual District? District { get; set; }
        #endregion
    }
}
