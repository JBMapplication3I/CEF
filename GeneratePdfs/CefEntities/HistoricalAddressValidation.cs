using System;
using System.Collections.Generic;

namespace GeneratePdfs.CefEntities
{
    public partial class HistoricalAddressValidation
    {
        public int Id { get; set; }
        public string? CustomKey { get; set; }
        public DateTime CreatedDate { get; set; }
        public DateTime? UpdatedDate { get; set; }
        public bool Active { get; set; }
        public long? Hash { get; set; }
        public string? JsonAttributes { get; set; }
        public long? AddressHash { get; set; }
        public DateTime OnDate { get; set; }
        public bool IsValid { get; set; }
        public string? Provider { get; set; }
        public string? SerializedRequest { get; set; }
        public string? SerializedResponse { get; set; }
    }
}
