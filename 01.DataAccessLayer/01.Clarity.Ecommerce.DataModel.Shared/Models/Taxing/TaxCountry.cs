// <copyright file="TaxCountry.cs" company="clarity-ventures.com">
// Copyright (c) 2016-2023 clarity-ventures.com. All rights reserved.
// </copyright>
// <summary>Implements the tax country class</summary>
#nullable enable
namespace Clarity.Ecommerce.Interfaces.DataModel
{
    using Ecommerce.DataModel;

    public interface ITaxCountry : INameableBase
    {
        /// <summary>Gets or sets the rate.</summary>
        /// <value>The rate.</value>
        decimal Rate { get; set; }

        #region Related Objects
        /// <summary>Gets or sets the identifier of the country.</summary>
        /// <value>The identifier of the country.</value>
        int CountryID { get; set; }

        /// <summary>Gets or sets the country.</summary>
        /// <value>The country.</value>
        Country? Country { get; set; }
        #endregion
    }
}

namespace Clarity.Ecommerce.DataModel
{
    using System.ComponentModel;
    using System.ComponentModel.DataAnnotations.Schema;
    using Interfaces.DataModel;
    using Newtonsoft.Json;

    [SqlSchema("Tax", "TaxCountry")]
    public class TaxCountry : NameableBase, ITaxCountry
    {
        /// <inheritdoc/>
        // [Column(TypeName = "numeric"), DecimalPrecision(7, 6)]
        public decimal Rate { get; set; }

        #region Related Objects
        /// <inheritdoc/>
        [InverseProperty(nameof(ID)), ForeignKey(nameof(Country)), DefaultValue(0)]
        public int CountryID { get; set; }

        /// <inheritdoc/>
        [AllowMapInWithRelateWorkflowsButDontAutoGenerate, ForceMapOutWithLite, DefaultValue(null), JsonIgnore]
        public virtual Country? Country { get; set; }
        #endregion
    }
}
