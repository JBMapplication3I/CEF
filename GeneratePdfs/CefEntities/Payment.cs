using System;
using System.Collections.Generic;

namespace GeneratePdfs.CefEntities
{
    public partial class Payment
    {
        public Payment()
        {
            SalesInvoicePayments = new HashSet<SalesInvoicePayment>();
            SalesOrderPayments = new HashSet<SalesOrderPayment>();
            SalesReturnPayments = new HashSet<SalesReturnPayment>();
            SubscriptionHistories = new HashSet<SubscriptionHistory>();
        }

        public int Id { get; set; }
        public string? CustomKey { get; set; }
        public DateTime CreatedDate { get; set; }
        public DateTime? UpdatedDate { get; set; }
        public bool Active { get; set; }
        public int? StoreId { get; set; }
        public decimal? Amount { get; set; }
        public string? PaymentData { get; set; }
        public DateTime? StatusDate { get; set; }
        public bool? Authorized { get; set; }
        public string? AuthCode { get; set; }
        public DateTime? AuthDate { get; set; }
        public bool? Received { get; set; }
        public DateTime? ReceivedDate { get; set; }
        public string? ReferenceNo { get; set; }
        public string? Response { get; set; }
        public string? ExternalCustomerId { get; set; }
        public string? ExternalPaymentId { get; set; }
        public int? CardTypeId { get; set; }
        public string? CardMask { get; set; }
        public string? Cvv { get; set; }
        public string? Last4CardDigits { get; set; }
        public int? ExpirationMonth { get; set; }
        public int? ExpirationYear { get; set; }
        public string? TransactionNumber { get; set; }
        public int? BillingContactId { get; set; }
        public int? PaymentMethodId { get; set; }
        public int StatusId { get; set; }
        public int TypeId { get; set; }
        public int? CurrencyId { get; set; }
        public long? Hash { get; set; }
        public string? JsonAttributes { get; set; }
        public string? CheckNumber { get; set; }
        public string? RoutingNumberLast4 { get; set; }
        public string? AccountNumberLast4 { get; set; }
        public string? BankName { get; set; }
        public int? BrandId { get; set; }

        public virtual Contact? BillingContact { get; set; }
        public virtual Brand? Brand { get; set; }
        public virtual Currency? Currency { get; set; }
        public virtual PaymentMethod? PaymentMethod { get; set; }
        public virtual PaymentStatus Status { get; set; } = null!;
        public virtual Store? Store { get; set; }
        public virtual PaymentType Type { get; set; } = null!;
        public virtual ICollection<SalesInvoicePayment> SalesInvoicePayments { get; set; }
        public virtual ICollection<SalesOrderPayment> SalesOrderPayments { get; set; }
        public virtual ICollection<SalesReturnPayment> SalesReturnPayments { get; set; }
        public virtual ICollection<SubscriptionHistory> SubscriptionHistories { get; set; }
    }
}
