// <copyright file="SalesOrderPurchaseOrder.cs" company="clarity-ventures.com">
// Copyright (c) 2016-2023 clarity-ventures.com. All rights reserved.
// </copyright>
// <summary>Implements the sales order purchase order class</summary>
#nullable enable
namespace Clarity.Ecommerce.Interfaces.DataModel
{
    using Ecommerce.DataModel;

    public interface ISalesOrderPurchaseOrder : IAmARelationshipTable<SalesOrder, PurchaseOrder>
    {
        #region Related Objects
        /// <summary>Gets or sets the identifier of the purchase order.</summary>
        /// <value>The identifier of the purchase order.</value>
        int PurchaseOrderID { get; set; }

        /// <summary>Gets or sets the purchase order.</summary>
        /// <value>The purchase order.</value>
        PurchaseOrder? PurchaseOrder { get; set; }

        /// <summary>Gets or sets the identifier of the sales order.</summary>
        /// <value>The identifier of the sales order.</value>
        int SalesOrderID { get; set; }

        /// <summary>Gets or sets the sales order.</summary>
        /// <value>The sales order.</value>
        SalesOrder? SalesOrder { get; set; }
        #endregion
    }
}

namespace Clarity.Ecommerce.DataModel
{
    using System.ComponentModel;
    using System.ComponentModel.DataAnnotations.Schema;
    using Interfaces.DataModel;
    using Newtonsoft.Json;

    [SqlSchema("Purchasing", "SalesOrderPurchaseOrder")]
    public class SalesOrderPurchaseOrder : Base, ISalesOrderPurchaseOrder
    {
        #region IAmARelationshipTable<Account, PricePoint>
        /// <inheritdoc/>
        [InverseProperty(nameof(ID)), ForeignKey(nameof(Master)), DefaultValue(null)]
        public int MasterID { get; set; }

        /// <inheritdoc/>
        [DefaultValue(null), JsonIgnore]
        public virtual SalesOrder? Master { get; set; }

        /// <inheritdoc/>
        [InverseProperty(nameof(ID)), ForeignKey(nameof(Slave)), DefaultValue(null)]
        public int SlaveID { get; set; }

        /// <inheritdoc/>
        [ForceMapOutWithLite, DefaultValue(null), JsonIgnore]
        public virtual PurchaseOrder? Slave { get; set; }
        #endregion

        #region Related Objects
        /// <inheritdoc/>
        [NotMapped, JsonIgnore]
        int ISalesOrderPurchaseOrder.SalesOrderID { get => MasterID; set => MasterID = value; }

        /// <inheritdoc/>
        [NotMapped, JsonIgnore]
        SalesOrder? ISalesOrderPurchaseOrder.SalesOrder { get => Master; set => Master = value; }

        /// <inheritdoc/>
        [NotMapped, JsonIgnore]
        int ISalesOrderPurchaseOrder.PurchaseOrderID { get => SlaveID; set => SlaveID = value; }

        /// <inheritdoc/>
        [NotMapped, JsonIgnore]
        PurchaseOrder? ISalesOrderPurchaseOrder.PurchaseOrder { get => Slave; set => Slave = value; }
        #endregion
    }
}
