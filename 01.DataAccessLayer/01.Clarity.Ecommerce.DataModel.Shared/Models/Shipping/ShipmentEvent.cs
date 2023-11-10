// <copyright file="ShipmentEvent.cs" company="clarity-ventures.com">
// Copyright (c) 2016-2023 clarity-ventures.com. All rights reserved.
// </copyright>
// <summary>Implements the shipment event class</summary>
#nullable enable
namespace Clarity.Ecommerce.Interfaces.DataModel
{
    using System;
    using Ecommerce.DataModel;

    public interface IShipmentEvent : IBase
    {
        #region ShipmentEvent Properties
        /// <summary>Gets or sets the note.</summary>
        /// <value>The note.</value>
        string? Note { get; set; }

        /// <summary>Gets or sets the event date.</summary>
        /// <value>The event date.</value>
        DateTime EventDate { get; set; }
        #endregion

        #region Related Objects
        /// <summary>Gets or sets the identifier of the address.</summary>
        /// <value>The identifier of the address.</value>
        int AddressID { get; set; }

        /// <summary>Gets or sets the address.</summary>
        /// <value>The address.</value>
        Address? Address { get; set; }

        /// <summary>Gets or sets the identifier of the shipment.</summary>
        /// <value>The identifier of the shipment.</value>
        int ShipmentID { get; set; }

        /// <summary>Gets or sets the shipment.</summary>
        /// <value>The shipment.</value>
        Shipment? Shipment { get; set; }
        #endregion
    }
}

namespace Clarity.Ecommerce.DataModel
{
    using System;
    using System.ComponentModel;
    using System.ComponentModel.DataAnnotations.Schema;
    using Interfaces.DataModel;
    using Newtonsoft.Json;

    [SqlSchema("Shipping", "ShipmentEvent")]
    public class ShipmentEvent : Base, IShipmentEvent
    {
        #region ShipmentEvent Properties
        /// <inheritdoc/>
        [DefaultValue(null)]
        public string? Note { get; set; }

        /// <inheritdoc/>
        ////[Column(TypeName = "datetime2"), DateTimePrecision(7)]
        public DateTime EventDate { get; set; }
        #endregion

        #region Related Objects
        /// <inheritdoc/>
        [InverseProperty(nameof(ID)), ForeignKey(nameof(Address)), DefaultValue(0)]
        public int AddressID { get; set; }

        /// <inheritdoc/>
        [ForceMapOutWithLite, DefaultValue(null), JsonIgnore]
        public virtual Address? Address { get; set; }

        /// <inheritdoc/>
        [InverseProperty(nameof(ID)), ForeignKey(nameof(Shipment)), DefaultValue(0)]
        public int ShipmentID { get; set; }

        /// <inheritdoc/>
        [DefaultValue(null), JsonIgnore]
        public virtual Shipment? Shipment { get; set; }
        #endregion
    }
}
