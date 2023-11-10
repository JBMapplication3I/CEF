using System;
using System.Collections.Generic;

namespace GeneratePdfs.CefEntities
{
    public partial class Wallet
    {
        public int Id { get; set; }
        public string? CustomKey { get; set; }
        public DateTime CreatedDate { get; set; }
        public DateTime? UpdatedDate { get; set; }
        public bool Active { get; set; }
        public string? Name { get; set; }
        public string? Description { get; set; }
        public string? CreditCardNumber { get; set; }
        public int? ExpirationMonth { get; set; }
        public int? ExpirationYear { get; set; }
        public string? Token { get; set; }
        public string? CardType { get; set; }
        public string? CardHolderName { get; set; }
        public int UserId { get; set; }
        public long? Hash { get; set; }
        public string? JsonAttributes { get; set; }
        public string? AccountNumber { get; set; }
        public string? RoutingNumber { get; set; }
        public string? BankName { get; set; }
        public int? CurrencyId { get; set; }
        public bool IsDefault { get; set; }
        public int? AccountContactId { get; set; }

        public virtual AccountContact? AccountContact { get; set; }
        public virtual Currency? Currency { get; set; }
        public virtual User User { get; set; } = null!;
    }
}
