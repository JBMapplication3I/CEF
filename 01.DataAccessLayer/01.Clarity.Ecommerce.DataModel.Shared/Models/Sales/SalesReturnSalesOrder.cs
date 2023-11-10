// <copyright file="SalesReturnSalesOrder.cs" company="clarity-ventures.com">
// Copyright (c) 2017-2023 clarity-ventures.com. All rights reserved.
// </copyright>
// <summary>Implements the sales return sales order class</summary>
#nullable enable
namespace Clarity.Ecommerce.Interfaces.DataModel
{
    using Ecommerce.DataModel;

    public interface ISalesReturnSalesOrder : IAmARelationshipTable<SalesReturn, SalesOrder>
    {
        #region Related Objects
        /// <summary>Gets or sets the identifier of the sales return.</summary>
        /// <value>The identifier of the sales return.</value>
        int SalesReturnID { get; set; }

        /// <summary>Gets or sets the sales return.</summary>
        /// <value>The sales return.</value>
        SalesReturn? SalesReturn { get; set; }

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

    [SqlSchema("Returning", "SalesReturnSalesOrder")]
    public class SalesReturnSalesOrder : Base, ISalesReturnSalesOrder
    {
        #region IAmARelationshipTable<Account, PricePoint>
        /// <inheritdoc/>
        [InverseProperty(nameof(ID)), ForeignKey(nameof(Master)), DefaultValue(null)]
        public int MasterID { get; set; }

        /// <inheritdoc/>
        [DefaultValue(null), JsonIgnore]
        public virtual SalesReturn? Master { get; set; }

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
        int ISalesReturnSalesOrder.SalesReturnID { get => MasterID; set => MasterID = value; }

        /// <inheritdoc/>
        [NotMapped, JsonIgnore]
        SalesReturn? ISalesReturnSalesOrder.SalesReturn { get => Master; set => Master = value; }

        /// <inheritdoc/>
        [NotMapped, JsonIgnore]
        int ISalesReturnSalesOrder.SalesOrderID { get => SlaveID; set => SlaveID = value; }

        /// <inheritdoc/>
        [NotMapped, JsonIgnore]
        SalesOrder? ISalesReturnSalesOrder.SalesOrder { get => Slave; set => Slave = value; }
        #endregion
    }
}
