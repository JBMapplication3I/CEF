using System;
using System.Collections.Generic;

namespace GeneratePdfs.CefEntities
{
    public partial class StoredFile
    {
        public StoredFile()
        {
            AccountFiles = new HashSet<AccountFile>();
            CalendarEventFiles = new HashSet<CalendarEventFile>();
            CartFiles = new HashSet<CartFile>();
            CategoryFiles = new HashSet<CategoryFile>();
            EmailQueueAttachments = new HashSet<EmailQueueAttachment>();
            MessageAttachments = new HashSet<MessageAttachment>();
            ProductFiles = new HashSet<ProductFile>();
            PurchaseOrderFiles = new HashSet<PurchaseOrderFile>();
            SalesInvoiceFiles = new HashSet<SalesInvoiceFile>();
            SalesOrderFiles = new HashSet<SalesOrderFile>();
            SalesQuoteFiles = new HashSet<SalesQuoteFile>();
            SalesReturnFiles = new HashSet<SalesReturnFile>();
            SampleRequestFiles = new HashSet<SampleRequestFile>();
            UserFiles = new HashSet<UserFile>();
        }

        public int Id { get; set; }
        public string? CustomKey { get; set; }
        public DateTime CreatedDate { get; set; }
        public DateTime? UpdatedDate { get; set; }
        public bool Active { get; set; }
        public long? Hash { get; set; }
        public string? Name { get; set; }
        public string? Description { get; set; }
        public int? SortOrder { get; set; }
        public string? DisplayName { get; set; }
        public string? SeoTitle { get; set; }
        public string? Author { get; set; }
        public string? Copyright { get; set; }
        public string? FileFormat { get; set; }
        public string? FileName { get; set; }
        public bool IsStoredInDb { get; set; }
        public byte[]? Bytes { get; set; }
        public string? JsonAttributes { get; set; }

        public virtual ICollection<AccountFile> AccountFiles { get; set; }
        public virtual ICollection<CalendarEventFile> CalendarEventFiles { get; set; }
        public virtual ICollection<CartFile> CartFiles { get; set; }
        public virtual ICollection<CategoryFile> CategoryFiles { get; set; }
        public virtual ICollection<EmailQueueAttachment> EmailQueueAttachments { get; set; }
        public virtual ICollection<MessageAttachment> MessageAttachments { get; set; }
        public virtual ICollection<ProductFile> ProductFiles { get; set; }
        public virtual ICollection<PurchaseOrderFile> PurchaseOrderFiles { get; set; }
        public virtual ICollection<SalesInvoiceFile> SalesInvoiceFiles { get; set; }
        public virtual ICollection<SalesOrderFile> SalesOrderFiles { get; set; }
        public virtual ICollection<SalesQuoteFile> SalesQuoteFiles { get; set; }
        public virtual ICollection<SalesReturnFile> SalesReturnFiles { get; set; }
        public virtual ICollection<SampleRequestFile> SampleRequestFiles { get; set; }
        public virtual ICollection<UserFile> UserFiles { get; set; }
    }
}
