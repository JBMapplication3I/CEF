// <copyright file="ShipCarrier.cs" company="clarity-ventures.com">
// Copyright (c) 2016-2023 clarity-ventures.com. All rights reserved.
// </copyright>
// <summary>Implements the ship carrier class</summary>
#nullable enable
namespace Clarity.Ecommerce.Interfaces.DataModel
{
    using System;
    using System.Collections.Generic;
    using Ecommerce.DataModel;

    public interface IShipCarrier : INameableBase, IHaveANullableContactBase
    {
        #region ShipCarrier Properties
        /// <summary>Gets or sets the point of contact.</summary>
        /// <value>The point of contact.</value>
        string? PointOfContact { get; set; }

        /// <summary>Gets or sets a value indicating whether this IShipCarrier is inbound.</summary>
        /// <value>True if this IShipCarrier is inbound, false if not.</value>
        bool IsInbound { get; set; }

        /// <summary>Gets or sets a value indicating whether this IShipCarrier is outbound.</summary>
        /// <value>True if this IShipCarrier is outbound, false if not.</value>
        bool IsOutbound { get; set; }

        /// <summary>Gets or sets the username.</summary>
        /// <value>The username.</value>
        string? Username { get; set; }

        /// <summary>Gets or sets the encrypted password.</summary>
        /// <value>The encrypted password.</value>
        string? EncryptedPassword { get; set; }

        /// <summary>Gets or sets the authentication.</summary>
        /// <value>The authentication.</value>
        string? Authentication { get; set; }

        /// <summary>Gets or sets the account number.</summary>
        /// <value>The account number.</value>
        string? AccountNumber { get; set; }

        /// <summary>Gets or sets the sales rep.</summary>
        /// <value>The sales rep.</value>
        string? SalesRep { get; set; }

        /// <summary>Gets or sets the pickup time.</summary>
        /// <value>The pickup time.</value>
        DateTime? PickupTime { get; set; }
        #endregion

        #region Associated Objects
        /// <summary>Gets or sets the purchase orders.</summary>
        /// <value>The purchase orders.</value>
        ICollection<PurchaseOrder>? PurchaseOrders { get; set; }

        /// <summary>Gets or sets the ship carrier methods.</summary>
        /// <value>The ship carrier methods.</value>
        ICollection<ShipCarrierMethod>? ShipCarrierMethods { get; set; }

        /// <summary>Gets or sets the shipments.</summary>
        /// <value>The shipments.</value>
        ICollection<Shipment>? Shipments { get; set; }
        #endregion
    }
}

namespace Clarity.Ecommerce.DataModel
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using Interfaces.DataModel;
    using Newtonsoft.Json;

    [SqlSchema("Shipping", "ShipCarrier")]
    public class ShipCarrier : NameableBase, IShipCarrier
    {
        private ICollection<PurchaseOrder>? purchaseOrders;
        private ICollection<ShipCarrierMethod>? shipCarrierMethods;
        private ICollection<Shipment>? shipments;

        public ShipCarrier()
        {
            purchaseOrders = new HashSet<PurchaseOrder>();
            shipCarrierMethods = new HashSet<ShipCarrierMethod>();
            shipments = new HashSet<Shipment>();
        }

        #region IHaveANullableContactBase Properties
        /// <inheritdoc/>
        [InverseProperty(nameof(ID)), ForeignKey(nameof(Contact)), DefaultValue(null)]
        public int? ContactID { get; set; }

        /// <inheritdoc/>
        [ForceMapOutWithLite, DefaultValue(null), JsonIgnore]
        public virtual Contact? Contact { get; set; }
        #endregion

        #region ShipCarrier Properties
        /// <inheritdoc/>
        [StringLength(1000), StringIsUnicode(false), DefaultValue(null)]
        public string? PointOfContact { get; set; }

        /// <inheritdoc/>
        [DefaultValue(false)]
        public bool IsInbound { get; set; }

        /// <inheritdoc/>
        [DefaultValue(false)]
        public bool IsOutbound { get; set; }

        /// <inheritdoc/>
        [StringLength(75), DefaultValue(null)]
        public string? Username { get; set; }

        /// <inheritdoc/>
        [StringLength(1024), DontMapOutWithLite, DefaultValue(null)]
        public string? EncryptedPassword { get; set; }

        /// <inheritdoc/>
        [StringLength(128), DefaultValue(null)]
        public string? Authentication { get; set; }

        /// <inheritdoc/>
        [StringLength(128), DefaultValue(null)]
        public string? AccountNumber { get; set; }

        /// <inheritdoc/>
        [StringLength(128), DefaultValue(null)]
        public string? SalesRep { get; set; }

        /// <inheritdoc/>
        [/*Column(TypeName = "datetime2"), DateTimePrecision(7),*/ DefaultValue(null)]
        public DateTime? PickupTime { get; set; }
        #endregion

        #region Associated Objects
        /// <inheritdoc/>
        [DontMapInEver, DontMapOutEver, DefaultValue(null), JsonIgnore]
        public virtual ICollection<PurchaseOrder>? PurchaseOrders { get => purchaseOrders; set => purchaseOrders = value; }

        /// <inheritdoc/>
        [DefaultValue(null), JsonIgnore]
        public virtual ICollection<ShipCarrierMethod>? ShipCarrierMethods { get => shipCarrierMethods; set => shipCarrierMethods = value; }

        /// <inheritdoc/>
        [DefaultValue(null), JsonIgnore]
        public virtual ICollection<Shipment>? Shipments { get => shipments; set => shipments = value; }
        #endregion
    }
}
