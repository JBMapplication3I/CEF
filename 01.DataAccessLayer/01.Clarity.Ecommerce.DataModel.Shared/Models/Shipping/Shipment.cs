// <copyright file="Shipment.cs" company="clarity-ventures.com">
// Copyright (c) 2016-2023 clarity-ventures.com. All rights reserved.
// </copyright>
// <summary>Implements the shipment class</summary>
#nullable enable
namespace Clarity.Ecommerce.Interfaces.DataModel
{
    using System;
    using System.Collections.Generic;
    using Ecommerce.DataModel;

    public interface IShipment
        : IHaveATypeBase<ShipmentType>, IHaveAStatusBase<ShipmentStatus>
    {
        #region Shipment Properties
        /// <summary>Gets or sets the reference 1.</summary>
        /// <value>The reference 1.</value>
        string? Reference1 { get; set; }

        /// <summary>Gets or sets the reference 2.</summary>
        /// <value>The reference 2.</value>
        string? Reference2 { get; set; }

        /// <summary>Gets or sets the reference 3.</summary>
        /// <value>The reference 3.</value>
        string? Reference3 { get; set; }

        /// <summary>Gets or sets the tracking number.</summary>
        /// <value>The tracking number.</value>
        string? TrackingNumber { get; set; }

        /// <summary>Gets or sets the Destination for the.</summary>
        /// <value>The destination.</value>
        string? Destination { get; set; }

        /// <summary>Gets or sets the target shipping date.</summary>
        /// <value>The target shipping date.</value>
        DateTime? TargetShippingDate { get; set; }

        /// <summary>Gets or sets the estimated delivery date.</summary>
        /// <value>The estimated delivery date.</value>
        DateTime? EstimatedDeliveryDate { get; set; }

        /// <summary>Gets or sets the ship date.</summary>
        /// <value>The ship date.</value>
        DateTime? ShipDate { get; set; }

        /// <summary>Gets or sets the Date/Time of the date delivered.</summary>
        /// <value>The date delivered.</value>
        DateTime? DateDelivered { get; set; }

        /// <summary>Gets or sets the negotiated rate.</summary>
        /// <value>The negotiated rate.</value>
        decimal? NegotiatedRate { get; set; }

        /// <summary>Gets or sets the published rate.</summary>
        /// <value>The published rate.</value>
        decimal? PublishedRate { get; set; }
        #endregion

        #region Related Objects
        /// <summary>Gets or sets the identifier of the inventory location section.</summary>
        /// <value>The identifier of the inventory location section.</value>
        int? InventoryLocationSectionID { get; set; }

        /// <summary>Gets or sets the inventory location section.</summary>
        /// <value>The inventory location section.</value>
        InventoryLocationSection? InventoryLocationSection { get; set; }

        /// <summary>Gets or sets the identifier of the destination contact.</summary>
        /// <value>The identifier of the destination contact.</value>
        int DestinationContactID { get; set; }

        /// <summary>Gets or sets destination contact.</summary>
        /// <value>The destination contact.</value>
        Contact? DestinationContact { get; set; }

        /// <summary>Gets or sets the identifier of the origin contact.</summary>
        /// <value>The identifier of the origin contact.</value>
        int OriginContactID { get; set; }

        /// <summary>Gets or sets the origin contact.</summary>
        /// <value>The origin contact.</value>
        Contact? OriginContact { get; set; }

        /// <summary>Gets or sets the identifier of the ship carrier.</summary>
        /// <value>The identifier of the ship carrier.</value>
        int? ShipCarrierID { get; set; }

        /// <summary>Gets or sets the ship carrier.</summary>
        /// <value>The ship carrier.</value>
        ShipCarrier? ShipCarrier { get; set; }

        /// <summary>Gets or sets the identifier of the ship carrier method.</summary>
        /// <value>The identifier of the ship carrier method.</value>
        int? ShipCarrierMethodID { get; set; }

        /// <summary>Gets or sets the ship carrier method.</summary>
        /// <value>The ship carrier method.</value>
        ShipCarrierMethod? ShipCarrierMethod { get; set; }

        /// <summary>Gets or sets the identifier of the vendor.</summary>
        /// <value>The identifier of the vendor.</value>
        int? VendorID { get; set; }

        /// <summary>Gets or sets the vendor.</summary>
        /// <value>The vendor.</value>
        Vendor? Vendor { get; set; }

        /// <summary>Gets or sets the identifier of the Sales Group.</summary>
        /// <value>The identifier of the Sales Group.</value>
        int? SalesGroupID { get; set; }

        /// <summary>Gets or sets the identifier of the Sales Order.</summary>
        /// <value>The identifier of the Sales Order.</value>
        int? SalesOrderID { get; set; }

        /// <summary>Gets or sets the identifier of the Sales Invoice.</summary>
        /// <value>The identifier of the Sales Invoice.</value>
        int? SalesInvoiceID { get; set; }
        #endregion

        #region Associated Objects
        /// <summary>Gets or sets the shipment events.</summary>
        /// <value>The shipment events.</value>
        ICollection<ShipmentEvent>? ShipmentEvents { get; set; }

        /// <summary>Gets or sets the shipment lines.</summary>
        /// <value>The shipment lines.</value>
        ICollection<ShipmentLine>? ShipmentLines { get; set; }
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

    [SqlSchema("Shipping", "Shipment")]
    public class Shipment : Base, IShipment
    {
        private ICollection<ShipmentEvent>? shipmentEvents;
        private ICollection<ShipmentLine>? shipmentLines;

        public Shipment()
        {
            shipmentEvents = new HashSet<ShipmentEvent>();
            shipmentLines = new HashSet<ShipmentLine>();
        }

        #region IHaveAType Properties
        /// <inheritdoc/>
        [InverseProperty(nameof(ID)), ForeignKey(nameof(Type)), DefaultValue(0)]
        public int TypeID { get; set; }

        /// <inheritdoc/>
        [ForceMapOutWithLite, DefaultValue(null)]
        public virtual ShipmentType? Type { get; set; }
        #endregion

        #region IHaveAStatus Properties
        /// <inheritdoc/>
        [InverseProperty(nameof(ID)), ForeignKey(nameof(Status)), DefaultValue(0)]
        public int StatusID { get; set; }

        /// <inheritdoc/>
        [ForceMapOutWithLite, DefaultValue(null)]
        public virtual ShipmentStatus? Status { get; set; }
        #endregion

        #region Shipment Properties
        /// <inheritdoc/>
        [StringLength(100), StringIsUnicode(false), DefaultValue(null)]
        public string? Reference1 { get; set; }

        /// <inheritdoc/>
        [StringLength(100), StringIsUnicode(false), DefaultValue(null)]
        public string? Reference2 { get; set; }

        /// <inheritdoc/>
        [StringLength(100), StringIsUnicode(false), DefaultValue(null)]
        public string? Reference3 { get; set; }

        /// <inheritdoc/>
        [StringLength(100), StringIsUnicode(false), DefaultValue(null)]
        public string? TrackingNumber { get; set; }

        /// <inheritdoc/>
        [StringLength(255), StringIsUnicode(false), DefaultValue(null)]
        public string? Destination { get; set; }

        /// <inheritdoc/>
        [/*Column(TypeName = "datetime2"), DateTimePrecision(7),*/ DefaultValue(null)]
        public DateTime? TargetShippingDate { get; set; }

        /// <inheritdoc/>
        [/*Column(TypeName = "datetime2"), DateTimePrecision(7),*/ DefaultValue(null)]
        public DateTime? EstimatedDeliveryDate { get; set; }

        /// <inheritdoc/>
        [/*Column(TypeName = "datetime2"), DateTimePrecision(7),*/ DefaultValue(null)]
        public DateTime? ShipDate { get; set; }

        /// <inheritdoc/>
        [/*Column(TypeName = "datetime2"), DateTimePrecision(7),*/ DefaultValue(null)]
        public DateTime? DateDelivered { get; set; }

        /// <inheritdoc/>
        [DecimalPrecision(18, 4), DefaultValue(null)]
        public decimal? NegotiatedRate { get; set; }

        /// <inheritdoc/>
        [DecimalPrecision(18, 4), DefaultValue(null)]
        public decimal? PublishedRate { get; set; }
        #endregion

        #region Related Objects
        /// <inheritdoc/>
        // ForeignKey Handled in the modelBuilder for cascading
        [DefaultValue(0)]
        public int OriginContactID { get; set; }

        /// <inheritdoc/>
        [AllowMapInWithRelateWorkflowsButDontAutoGenerate, ForceMapOutWithLite, DefaultValue(null), JsonIgnore]
        public virtual Contact? OriginContact { get; set; }

        /// <inheritdoc/>
        // ForeignKey Handled in the modelBuilder for cascading
        [DefaultValue(0)]
        public int DestinationContactID { get; set; }

        /// <inheritdoc/>
        [AllowMapInWithRelateWorkflowsButDontAutoGenerate, ForceMapOutWithLite, DefaultValue(null), JsonIgnore]
        public virtual Contact? DestinationContact { get; set; }

        /// <inheritdoc/>
        [InverseProperty(nameof(ID)), ForeignKey(nameof(InventoryLocationSection)), DefaultValue(null)]
        public int? InventoryLocationSectionID { get; set; }

        /// <inheritdoc/>
        [AllowMapInWithRelateWorkflowsButDontAutoGenerate, DefaultValue(null), JsonIgnore]
        public virtual InventoryLocationSection? InventoryLocationSection { get; set; }

        /// <inheritdoc/>
        [InverseProperty(nameof(ID)), ForeignKey(nameof(ShipCarrier)), DefaultValue(null)]
        public int? ShipCarrierID { get; set; }

        /// <inheritdoc/>
        [AllowMapInWithRelateWorkflowsButDontAutoGenerate, DefaultValue(null), JsonIgnore]
        public virtual ShipCarrier? ShipCarrier { get; set; }

        /// <inheritdoc/>
        [InverseProperty(nameof(ID)), ForeignKey(nameof(ShipCarrierMethod)), DefaultValue(null)]
        public int? ShipCarrierMethodID { get; set; }

        /// <inheritdoc/>
        [AllowMapInWithRelateWorkflowsButDontAutoGenerate, DefaultValue(null), JsonIgnore]
        public virtual ShipCarrierMethod? ShipCarrierMethod { get; set; }

        /// <inheritdoc/>
        [InverseProperty(nameof(ID)), ForeignKey(nameof(Vendor)), DefaultValue(null)]
        public int? VendorID { get; set; }

        /// <inheritdoc/>
        [DontMapOutEver, DontMapInEver, DefaultValue(null), JsonIgnore]
        public virtual Vendor? Vendor { get; set; }

        /// <inheritdoc/>
        [InverseProperty(nameof(ID)), ForeignKey(nameof(SalesGroup)), DefaultValue(null)]
        public int? SalesGroupID { get; set; }

        [DontMapOutEver, DontMapInEver, DefaultValue(null), JsonIgnore]
        public virtual SalesGroup? SalesGroup { get; set; }

        /// <inheritdoc/>
        [InverseProperty(nameof(ID)), ForeignKey(nameof(SalesOrder)), DefaultValue(null)]
        public int? SalesOrderID { get; set; }

        [DontMapOutEver, DontMapInEver, DefaultValue(null), JsonIgnore]
        public virtual SalesOrder? SalesOrder { get; set; }

        /// <inheritdoc/>
        [InverseProperty(nameof(ID)), ForeignKey(nameof(SalesInvoice)), DefaultValue(null)]
        public int? SalesInvoiceID { get; set; }

        [DontMapOutEver, DontMapInEver, DefaultValue(null), JsonIgnore]
        public virtual SalesInvoice? SalesInvoice { get; set; }
        #endregion

        #region Associated Objects
        /// <inheritdoc/>
        [DefaultValue(null), JsonIgnore]
        public virtual ICollection<ShipmentEvent>? ShipmentEvents { get => shipmentEvents; set => shipmentEvents = value; }

        /// <inheritdoc/>
        [DefaultValue(null), JsonIgnore]
        public virtual ICollection<ShipmentLine>? ShipmentLines { get => shipmentLines; set => shipmentLines = value; }
        #endregion
    }
}
