// <copyright file="SalesQuoteSalesOrder.cs" company="clarity-ventures.com">
// Copyright (c) 2016-2023 clarity-ventures.com. All rights reserved.
// </copyright>
// <summary>Implements the sales quote sales order class</summary>
#nullable enable
namespace Clarity.Ecommerce.Interfaces.DataModel
{
    using Ecommerce.DataModel;

    public interface ISalesQuoteSalesOrder : IAmARelationshipTable<SalesQuote, SalesOrder>
    {
        #region Related Objects
        /// <summary>Gets or sets the identifier of the sales quote.</summary>
        /// <value>The identifier of the sales quote.</value>
        int SalesQuoteID { get; set; }

        /// <summary>Gets or sets the sales quote.</summary>
        /// <value>The sales quote.</value>
        SalesQuote? SalesQuote { get; set; }

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

    [SqlSchema("Quoting", "SalesQuoteSalesOrder")]
    public class SalesQuoteSalesOrder : Base, ISalesQuoteSalesOrder
    {
        #region IAmARelationshipTable<Account, PricePoint>
        /// <inheritdoc/>
        [InverseProperty(nameof(ID)), ForeignKey(nameof(Master)), DefaultValue(null)]
        public int MasterID { get; set; }

        /// <inheritdoc/>
        [DefaultValue(null), JsonIgnore]
        public virtual SalesQuote? Master { get; set; }

        /// <inheritdoc/>
        [InverseProperty(nameof(ID)), ForeignKey(nameof(Slave)), DefaultValue(null)]
        public int SlaveID { get; set; }

        /// <inheritdoc/>
        [ForceMapOutWithLite, DefaultValue(null), JsonIgnore]
        public virtual SalesOrder? Slave { get; set; }
        #endregion

        #region Related Objects
        /// <inheritdoc/>
        [NotMapped, JsonIgnore]
        int ISalesQuoteSalesOrder.SalesQuoteID { get => MasterID; set => MasterID = value; }

        /// <inheritdoc/>
        [NotMapped, JsonIgnore]
        SalesQuote? ISalesQuoteSalesOrder.SalesQuote { get => Master; set => Master = value; }

        /// <inheritdoc/>
        [NotMapped, JsonIgnore]
        int ISalesQuoteSalesOrder.SalesOrderID { get => SlaveID; set => SlaveID = value; }

        /// <inheritdoc/>
        [NotMapped, JsonIgnore]
        SalesOrder? ISalesQuoteSalesOrder.SalesOrder { get => Slave; set => Slave = value; }
        #endregion
    }
}
