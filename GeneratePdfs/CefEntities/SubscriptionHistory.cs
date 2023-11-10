﻿using System;
using System.Collections.Generic;

namespace GeneratePdfs.CefEntities
{
    public partial class SubscriptionHistory
    {
        public int Id { get; set; }
        public string? CustomKey { get; set; }
        public DateTime CreatedDate { get; set; }
        public DateTime? UpdatedDate { get; set; }
        public bool Active { get; set; }
        public DateTime PaymentDate { get; set; }
        public bool PaymentSuccess { get; set; }
        public string Memo { get; set; } = null!;
        public int BillingPeriodsPaid { get; set; }
        public int SlaveId { get; set; }
        public int MasterId { get; set; }
        public long? Hash { get; set; }
        public string? JsonAttributes { get; set; }

        public virtual Subscription Master { get; set; } = null!;
        public virtual Payment Slave { get; set; } = null!;
    }
}
