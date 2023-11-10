// <copyright file="Contact.cs" company="clarity-ventures.com">
// Copyright (c) 2016-2023 clarity-ventures.com. All rights reserved.
// </copyright>
// <summary>Implements the contact class</summary>
#nullable enable
namespace Clarity.Ecommerce.Interfaces.DataModel
{
    using System.Collections.Generic;
    using Ecommerce.DataModel;

    public interface IContact
        : IHaveATypeBase<ContactType>,
            IHaveImagesBase<Contact, ContactImage, ContactImageType>,
            IAmFilterableByStore<Store>,
            IAmFilterableByVendor<Vendor>
    {
        #region Contact Properties
        /// <summary>Gets or sets the person's first name.</summary>
        /// <value>The name of the first.</value>
        string? FirstName { get; set; }

        /// <summary>Gets or sets the person's middle name.</summary>
        /// <value>The name of the middle.</value>
        string? MiddleName { get; set; }

        /// <summary>Gets or sets the person's last name.</summary>
        /// <value>The name of the last.</value>
        string? LastName { get; set; }

        /// <summary>Gets or sets the name of the full.</summary>
        /// <value>The name of the full.</value>
        string? FullName { get; set; }

        /// <summary>Gets or sets the phone 1.</summary>
        /// <value>The phone 1.</value>
        string? Phone1 { get; set; }

        /// <summary>Gets or sets the phone 2.</summary>
        /// <value>The phone 2.</value>
        string? Phone2 { get; set; }

        /// <summary>Gets or sets the phone 3.</summary>
        /// <value>The phone 3.</value>
        string? Phone3 { get; set; }

        /// <summary>Gets or sets the fax 1.</summary>
        /// <value>The fax 1.</value>
        string? Fax1 { get; set; }

        /// <summary>Gets or sets the email 1.</summary>
        /// <value>The email 1.</value>
        string? Email1 { get; set; }

        /// <summary>Gets or sets the website 1.</summary>
        /// <value>The website 1.</value>
        string? Website1 { get; set; }
        #endregion

        #region Related Objects
        /// <summary>Gets or sets the identifier of the address.</summary>
        /// <value>The identifier of the address.</value>
        int? AddressID { get; set; }

        /// <summary>Gets or sets the address.</summary>
        /// <value>The address.</value>
        Address? Address { get; set; }
        #endregion

        #region Associated Objects
        /// <summary>Gets or sets the billing contacts sales invoices.</summary>
        /// <value>The billing contacts sales invoices.</value>
        ICollection<SalesInvoice>? BillingContactsSalesInvoices { get; set; }

        /// <summary>Gets or sets the shipping contacts sales invoices.</summary>
        /// <value>The shipping contacts sales invoices.</value>
        ICollection<SalesInvoice>? ShippingContactsSalesInvoices { get; set; }

        /// <summary>Gets or sets the billing contacts purchase orders.</summary>
        /// <value>The billing contacts purchase orders.</value>
        ICollection<PurchaseOrder>? BillingContactsPurchaseOrders { get; set; }

        /// <summary>Gets or sets the shipping contacts purchase orders.</summary>
        /// <value>The shipping contacts purchase orders.</value>
        ICollection<PurchaseOrder>? ShippingContactsPurchaseOrders { get; set; }

        /// <summary>Gets or sets the billing contacts sales quotes.</summary>
        /// <value>The billing contacts sales quotes.</value>
        ICollection<SalesQuote>? BillingContactsSalesQuotes { get; set; }

        /// <summary>Gets or sets the shipping contacts sales quotes.</summary>
        /// <value>The shipping contacts sales quotes.</value>
        ICollection<SalesQuote>? ShippingContactsSalesQuotes { get; set; }

        /// <summary>Gets or sets the billing contacts sales orders.</summary>
        /// <value>The billing contacts sales orders.</value>
        ICollection<SalesOrder>? BillingContactsSalesOrders { get; set; }

        /// <summary>Gets or sets the shipping contacts sales orders.</summary>
        /// <value>The shipping contacts sales orders.</value>
        ICollection<SalesOrder>? ShippingContactsSalesOrders { get; set; }

        /// <summary>Gets or sets the billing contacts sales returns.</summary>
        /// <value>The billing contacts sales returns.</value>
        ICollection<SalesReturn>? BillingContactsSalesReturns { get; set; }

        /// <summary>Gets or sets the shipping contacts sales returns.</summary>
        /// <value>The shipping contacts sales returns.</value>
        ICollection<SalesReturn>? ShippingContactsSalesReturns { get; set; }

        /// <summary>Gets or sets the origin contacts shipments.</summary>
        /// <value>The origin contacts shipments.</value>
        ICollection<Shipment>? OriginContactsShipments { get; set; }

        /// <summary>Gets or sets destination contacts shipments.</summary>
        /// <value>The destination contacts shipments.</value>
        ICollection<Shipment>? DestinationContactsShipments { get; set; }

        /// <summary>Gets or sets the payments.</summary>
        /// <value>The payments.</value>
        ICollection<Payment>? Payments { get; set; }
        #endregion
    }
}

namespace Clarity.Ecommerce.DataModel
{
    using System.Collections.Generic;
    using System.ComponentModel;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using Interfaces.DataModel;
    using Newtonsoft.Json;

    [SqlSchema("Contacts", "Contact")]
    public class Contact : Base, IContact
    {
        private ICollection<ContactImage>? images;
        private ICollection<Store>? stores;
        private ICollection<SalesInvoice>? shippingContactsSalesInvoices;
        private ICollection<SalesInvoice>? billingContactsSalesInvoices;
        private ICollection<SalesQuote>? shippingContactsSalesQuotes;
        private ICollection<SalesQuote>? billingContactsSalesQuotes;
        private ICollection<SalesOrder>? billingContactsSalesOrders;
        private ICollection<SalesOrder>? shippingContactsSalesOrders;
        private ICollection<SalesReturn>? billingContactsSalesReturns;
        private ICollection<SalesReturn>? shippingContactsSalesReturns;
        private ICollection<PurchaseOrder>? shippingContactsPurchaseOrders;
        private ICollection<PurchaseOrder>? billingContactsPurchaseOrders;
        private ICollection<Shipment>? originContactsShipments;
        private ICollection<Shipment>? destinationContactsShipments;
        private ICollection<Payment>? payments;
        private ICollection<Vendor>? vendors;

        public Contact()
        {
            // IHaveImagesBase Properties
            images = new HashSet<ContactImage>();
            // IAmFilterableByStore<Store> Properties
            stores = new HashSet<Store>();
            // IAmFilterableByVendor<Vendor> Properties
            vendors = new HashSet<Vendor>();
            // Contact Properties
            shippingContactsSalesInvoices = new HashSet<SalesInvoice>();
            billingContactsSalesInvoices = new HashSet<SalesInvoice>();
            shippingContactsSalesQuotes = new HashSet<SalesQuote>();
            billingContactsSalesQuotes = new HashSet<SalesQuote>();
            shippingContactsPurchaseOrders = new HashSet<PurchaseOrder>();
            billingContactsPurchaseOrders = new HashSet<PurchaseOrder>();
            billingContactsSalesOrders = new HashSet<SalesOrder>();
            shippingContactsSalesOrders = new HashSet<SalesOrder>();
            billingContactsSalesReturns = new HashSet<SalesReturn>();
            shippingContactsSalesReturns = new HashSet<SalesReturn>();
            originContactsShipments = new HashSet<Shipment>();
            destinationContactsShipments = new HashSet<Shipment>();
            payments = new HashSet<Payment>();
        }

        #region IHaveATypeBase<ContactType>
        /// <inheritdoc/>
        [InverseProperty(nameof(ID)), ForeignKey(nameof(Type)), DefaultValue(0)]
        public int TypeID { get; set; }

        /// <inheritdoc/>
        [DefaultValue(null)]
        public virtual ContactType? Type { get; set; }
        #endregion

        #region HaveImagesBase Properties
        /// <inheritdoc/>
        [DefaultValue(null), JsonIgnore]
        public virtual ICollection<ContactImage>? Images { get => images; set => images = value; }
        #endregion

        #region IAmFilterableByStore<Store> Properties
        /// <inheritdoc/>
        [DontMapInWithAssociateWorkflows, DontMapOutEver, DefaultValue(null), JsonIgnore]
        public virtual ICollection<Store>? Stores { get => stores; set => stores = value; }
        #endregion

        #region IAmFilterableByStore<Vendor> Properties
        /// <inheritdoc/>
        [DontMapInWithAssociateWorkflows, DontMapOutEver, DefaultValue(null), JsonIgnore]
        public virtual ICollection<Vendor>? Vendors { get => vendors; set => vendors = value; }
        #endregion

        #region Contact Properties
        /// <inheritdoc/>
        [StringIsUnicode(true), StringLength(0100), DefaultValue(null)]
        public string? FirstName { get; set; }

        /// <inheritdoc/>
        [StringIsUnicode(true), StringLength(0100), DefaultValue(null)]
        public string? MiddleName { get; set; }

        /// <inheritdoc/>
        [StringIsUnicode(true), StringLength(0100), DefaultValue(null)]
        public string? LastName { get; set; }

        /// <inheritdoc/>
        [StringIsUnicode(true), StringLength(0300), DefaultValue(null)]
        public string? FullName { get; set; }

        /// <inheritdoc/>
        [StringIsUnicode(false), StringLength(0050), DefaultValue(null)]
        public string? Phone1 { get; set; }

        /// <inheritdoc/>
        [StringIsUnicode(false), StringLength(0050), DefaultValue(null)]
        public string? Phone2 { get; set; }

        /// <inheritdoc/>
        [StringIsUnicode(false), StringLength(0050), DefaultValue(null)]
        public string? Phone3 { get; set; }

        /// <inheritdoc/>
        [StringIsUnicode(false), StringLength(0050), DefaultValue(null)]
        public string? Fax1 { get; set; }

        /// <inheritdoc/>
        [StringIsUnicode(false), StringLength(1000), DefaultValue(null)]
        public string? Email1 { get; set; }

        /// <inheritdoc/>
        [StringIsUnicode(false), StringLength(1000), DefaultValue(null)]
        public string? Website1 { get; set; }
        #endregion

        #region Related Objects
        /// <inheritdoc/>
        [InverseProperty(nameof(ID)), ForeignKey(nameof(Address))]
        public int? AddressID { get; set; }

        /// <inheritdoc/>
        [ForceMapOutWithListing, DefaultValue(null)]
        public virtual Address? Address { get; set; }
        #endregion

        #region Associated Objects
        #region Don't map these out
        /// <inheritdoc/>
        [DontMapInWithAssociateWorkflows, DontMapOutEver, DefaultValue(null), JsonIgnore]
        public virtual ICollection<SalesInvoice>? ShippingContactsSalesInvoices { get => shippingContactsSalesInvoices; set => shippingContactsSalesInvoices = value; }

        /// <inheritdoc/>
        [DontMapInWithAssociateWorkflows, DontMapOutEver, DefaultValue(null), JsonIgnore]
        public virtual ICollection<SalesInvoice>? BillingContactsSalesInvoices { get => billingContactsSalesInvoices; set => billingContactsSalesInvoices = value; }

        /// <inheritdoc/>
        [DontMapInWithAssociateWorkflows, DontMapOutEver, DefaultValue(null), JsonIgnore]
        public virtual ICollection<SalesQuote>? ShippingContactsSalesQuotes { get => shippingContactsSalesQuotes; set => shippingContactsSalesQuotes = value; }

        /// <inheritdoc/>
        [DontMapInWithAssociateWorkflows, DontMapOutEver, DefaultValue(null), JsonIgnore]
        public virtual ICollection<SalesQuote>? BillingContactsSalesQuotes { get => billingContactsSalesQuotes; set => billingContactsSalesQuotes = value; }

        /// <inheritdoc/>
        [DontMapInWithAssociateWorkflows, DontMapOutEver, DefaultValue(null), JsonIgnore]
        public virtual ICollection<SalesOrder>? BillingContactsSalesOrders { get => billingContactsSalesOrders; set => billingContactsSalesOrders = value; }

        /// <inheritdoc/>
        [DontMapInWithAssociateWorkflows, DontMapOutEver, DefaultValue(null), JsonIgnore]
        public virtual ICollection<SalesOrder>? ShippingContactsSalesOrders { get => shippingContactsSalesOrders; set => shippingContactsSalesOrders = value; }

        /// <inheritdoc/>
        [DontMapInWithAssociateWorkflows, DontMapOutEver, DefaultValue(null), JsonIgnore]
        public virtual ICollection<SalesReturn>? BillingContactsSalesReturns { get => billingContactsSalesReturns; set => billingContactsSalesReturns = value; }

        /// <inheritdoc/>
        [DontMapInWithAssociateWorkflows, DontMapOutEver, DefaultValue(null), JsonIgnore]
        public virtual ICollection<SalesReturn>? ShippingContactsSalesReturns { get => shippingContactsSalesReturns; set => shippingContactsSalesReturns = value; }

        /// <inheritdoc/>
        [DontMapInWithAssociateWorkflows, DontMapOutEver, DefaultValue(null), JsonIgnore]
        public virtual ICollection<PurchaseOrder>? ShippingContactsPurchaseOrders { get => shippingContactsPurchaseOrders; set => shippingContactsPurchaseOrders = value; }

        /// <inheritdoc/>
        [DontMapInWithAssociateWorkflows, DontMapOutEver, DefaultValue(null), JsonIgnore]
        public virtual ICollection<PurchaseOrder>? BillingContactsPurchaseOrders { get => billingContactsPurchaseOrders; set => billingContactsPurchaseOrders = value; }

        /// <inheritdoc/>
        [DontMapInWithAssociateWorkflows, DontMapOutEver, DefaultValue(null), JsonIgnore]
        public virtual ICollection<Shipment>? OriginContactsShipments { get => originContactsShipments; set => originContactsShipments = value; }

        /// <inheritdoc/>
        [DontMapInWithAssociateWorkflows, DontMapOutEver, DefaultValue(null), JsonIgnore]
        public virtual ICollection<Shipment>? DestinationContactsShipments { get => destinationContactsShipments; set => destinationContactsShipments = value; }

        /// <inheritdoc/>
        [DontMapInWithAssociateWorkflows, DontMapOutEver, DefaultValue(null), JsonIgnore]
        public virtual ICollection<Payment>? Payments { get => payments; set => payments = value; }
        #endregion
        #endregion

        #region ICloneable Functions
        /// <inheritdoc/>
        public override string ToHashableString()
        {
            var builder = new System.Text.StringBuilder(base.ToHashableString());
            // Contact
            builder.Append("CK: ").AppendLine(CustomKey ?? string.Empty); // Contact Needs the key in case multiple address have same data
            builder.Append("FN: ").AppendLine(FirstName ?? string.Empty);
            builder.Append("MN: ").AppendLine(MiddleName ?? string.Empty);
            builder.Append("LN: ").AppendLine(LastName ?? string.Empty);
            builder.Append("Fl: ").AppendLine(FullName ?? string.Empty);
            builder.Append("P1: ").AppendLine(Phone1 ?? string.Empty);
            builder.Append("P2: ").AppendLine(Phone2 ?? string.Empty);
            builder.Append("P3: ").AppendLine(Phone3 ?? string.Empty);
            builder.Append("Fx: ").AppendLine(Fax1 ?? string.Empty);
            builder.Append("E1: ").AppendLine(Email1 ?? string.Empty);
            builder.Append("W1: ").AppendLine(Website1 ?? string.Empty);
            // Related Objects
            builder.Append("T: ").AppendLine(Type?.ToHashableString() ?? $"No Type={TypeID}");
            builder.Append("Ad: ").AppendLine(Address?.ToHashableString() ?? $"No Ad={AddressID}");
            // Return
            return builder.ToString();
        }
        #endregion
    }
}
