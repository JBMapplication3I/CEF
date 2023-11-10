// <copyright file="ServiceArea.cs" company="clarity-ventures.com">
// Copyright (c) 2021-2023 clarity-ventures.com. All rights reserved.
// </copyright>
// <summary>Implements the ServiceArea class</summary>
#nullable enable
namespace Clarity.Ecommerce.Interfaces.DataModel
{
    using Ecommerce.DataModel;

    public interface IServiceArea : IBase
    {
        #region ServiceArea Properties
        /// <summary>Gets or sets the radius (in miles) of this service area.</summary>
        /// <value>The radius (in miles) of this service area.</value>
        decimal? Radius { get; set; }
        #endregion

        #region Related Objects
        /// <summary>Gets or sets the ID of the contractor.</summary>
        /// <value>The ID of the contractor.</value>
        int ContractorID { get; set; }

        /// <summary>Gets or sets the contractor.</summary>
        /// <value>The contractor.</value>
        Contractor? Contractor { get; set; }

        /// <summary>Gets or sets the ID of the address where this service area is centered.</summary>
        /// <value>The ID of the address where this service area is centered.</value>
        int AddressID { get; set; }

        /// <summary>Gets or sets the address where this service area is centered.</summary>
        /// <value>The address where this service area is centered.</value>
        Address? Address { get; set; }
        #endregion
    }
}

namespace Clarity.Ecommerce.DataModel
{
    using System.ComponentModel;
    using System.ComponentModel.DataAnnotations.Schema;
    using Interfaces.DataModel;
    using Newtonsoft.Json;

    [SqlSchema("Accounts", "ServiceArea")]
    public class ServiceArea : Base, IServiceArea
    {
        #region ServiceArea Properties
        /// <inheritdoc/>
        public decimal? Radius { get; set; }
        #endregion

        #region Related Objects
        /// <inheritdoc/>
        [InverseProperty(nameof(ID)), ForeignKey(nameof(Contractor)), DefaultValue(null)]
        public int ContractorID { get; set; }

        /// <inheritdoc/>
        [JsonIgnore, DefaultValue(null)]
        public virtual Contractor? Contractor { get; set; }

        /// <inheritdoc/>
        [InverseProperty(nameof(ID)), ForeignKey(nameof(Address)), DefaultValue(null)]
        public int AddressID { get; set; }

        /// <inheritdoc/>
        [JsonIgnore, DefaultValue(null)]
        public virtual Address? Address { get; set; }
        #endregion
    }
}
