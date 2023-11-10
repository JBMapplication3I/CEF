// <copyright file="SalesOrderSalesInvoice.cs" company="clarity-ventures.com">
// Copyright (c) 2016-2023 clarity-ventures.com. All rights reserved.
// </copyright>
// <summary>Implements the sales order sales invoice class</summary>
#nullable enable
namespace Clarity.Ecommerce.Interfaces.DataModel
{
    using Ecommerce.DataModel;

    public interface ISalesOrderSalesInvoice : IAmARelationshipTable<SalesOrder, SalesInvoice>
    {
        #region Related Objects
        /// <summary>Gets or sets the identifier of the sales order.</summary>
        /// <value>The identifier of the sales order.</value>
        int SalesOrderID { get; set; }

        /// <summary>Gets or sets the sales order.</summary>
        /// <value>The sales order.</value>
        SalesOrder? SalesOrder { get; set; }

        /// <summary>Gets or sets the identifier of the sales invoice.</summary>
        /// <value>The identifier of the sales invoice.</value>
        int SalesInvoiceID { get; set; }

        /// <summary>Gets or sets the sales invoice.</summary>
        /// <value>The sales invoice.</value>
        SalesInvoice? SalesInvoice { get; set; }
        #endregion
    }
}

namespace Clarity.Ecommerce.DataModel
{
    using System.ComponentModel;
    using System.ComponentModel.DataAnnotations.Schema;
    using Interfaces.DataModel;
    using Newtonsoft.Json;

    [SqlSchema("Invoicing", "SalesOrderSalesInvoice")]
    public class SalesOrderSalesInvoice : Base, ISalesOrderSalesInvoice
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
        [ForceMapOutWithListing, ForceMapOutWithLite, DefaultValue(null), JsonIgnore]
        public virtual SalesInvoice? Slave { get; set; }
        #endregion

        #region Related Objects
        /// <inheritdoc/>
        [NotMapped, JsonIgnore]
        int ISalesOrderSalesInvoice.SalesOrderID { get => MasterID; set => MasterID = value; }

        /// <inheritdoc/>
        [NotMapped, JsonIgnore]
        SalesOrder? ISalesOrderSalesInvoice.SalesOrder { get => Master; set => Master = value; }

        /// <inheritdoc/>
        [NotMapped, JsonIgnore]
        int ISalesOrderSalesInvoice.SalesInvoiceID { get => SlaveID; set => SlaveID = value; }

        /// <inheritdoc/>
        [NotMapped, JsonIgnore]
        SalesInvoice? ISalesOrderSalesInvoice.SalesInvoice { get => Slave; set => Slave = value; }
        #endregion
    }
}
