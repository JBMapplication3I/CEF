using System;
using System.Collections.Generic;

namespace GeneratePdfs.CefEntities
{
    public partial class AccountCurrency
    {
        public int Id { get; set; }
        public string? CustomKey { get; set; }
        public DateTime CreatedDate { get; set; }
        public DateTime? UpdatedDate { get; set; }
        public bool Active { get; set; }
        public long? Hash { get; set; }
        public string? JsonAttributes { get; set; }
        public int MasterId { get; set; }
        public int SlaveId { get; set; }
        public string? CustomName { get; set; }
        public bool IsPrimary { get; set; }
        public string? CustomTranslationKey { get; set; }
        public decimal OverrideUnicodeSymbolValue { get; set; }
        public string? OverrideHtmlCharacterCode { get; set; }
        public string? OverrideRawCharacter { get; set; }
        public int? OverrideDecimalPlaceAccuracy { get; set; }
        public bool? OverrideUseSeparator { get; set; }
        public string? OverrideRawDecimalCharacter { get; set; }
        public string? OverrideHtmlDecimalCharacterCode { get; set; }
        public string? OverrideRawSeparatorCharacter { get; set; }
        public string? OverrideHtmlSeparatorCharacterCode { get; set; }

        public virtual Account Master { get; set; } = null!;
        public virtual Currency Slave { get; set; } = null!;
    }
}
