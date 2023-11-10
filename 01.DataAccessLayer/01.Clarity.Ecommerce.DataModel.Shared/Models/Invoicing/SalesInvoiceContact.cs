// <copyright file="SalesInvoiceContact.cs" company="clarity-ventures.com">
// Copyright (c) 2017-2023 clarity-ventures.com. All rights reserved.
// </copyright>
// <summary>Implements the sales invoice contact class</summary>
#nullable enable
namespace Clarity.Ecommerce.Interfaces.DataModel
{
    using Ecommerce.DataModel;

    /// <summary>Interface for sales invoice contact.</summary>
    public interface ISalesInvoiceContact : IAmAContactRelationshipTable<SalesInvoice, SalesInvoiceContact>
    {
    }
}

namespace Clarity.Ecommerce.DataModel
{
    using System.ComponentModel;
    using System.ComponentModel.DataAnnotations.Schema;
    using Interfaces.DataModel;
    using Newtonsoft.Json;

    [SqlSchema("Invoicing", "SalesInvoiceContact")]
    public class SalesInvoiceContact : Base, ISalesInvoiceContact
    {
        #region IAmAContactRelationshipTable Properties
        /// <inheritdoc/>
        [InverseProperty(nameof(ID)), ForeignKey(nameof(Master)), DefaultValue(null)]
        public int MasterID { get; set; }

        /// <inheritdoc/>
        [DontMapInEver, DefaultValue(null), JsonIgnore]
        public virtual SalesInvoice? Master { get; set; }

        /// <inheritdoc/>
        [InverseProperty(nameof(ID)), ForeignKey(nameof(Slave)), DefaultValue(null)]
        public int SlaveID { get; set; }

        /// <inheritdoc/>
        [ForceMapOutWithLite, DefaultValue(null), JsonIgnore]
        public virtual Contact? Slave { get; set; }
        #endregion

        #region IHaveAContactBase Properties
        /// <inheritdoc/>
        [NotMapped, JsonIgnore]
        int IHaveAContactBase.ContactID { get => SlaveID; set => SlaveID = value; }

        /// <inheritdoc/>
        [NotMapped, JsonIgnore]
        Contact? IHaveAContactBase.Contact { get => Slave; set => Slave = value; }
        #endregion
    }
}
