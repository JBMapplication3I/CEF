// <copyright file="InvoiceLinesResponse.cs" company="clarity-ventures.com">
// Copyright (c) 2018-2023 clarity-ventures.com. All rights reserved.
// </copyright>
// <summary>Implements the InvoiceLinesResponse class</summary>

namespace Clarity.Clients.JBM
{
    using System;
    using System.Collections.Generic;

    public class FusionInvoiceLine
    {
        public long? CustomerTransactionLineId { get; set; }

        public int? LineNumber { get; set; }

        public string? Description { get; set; }

        public decimal? Quantity { get; set; }

        public decimal? UnitSellingPrice { get; set; }

        public string? TaxClassificationCode { get; set; }

        public string? SalesOrder { get; set; }

        public string? AccountingRuleDuration { get; set; }

        public string? RuleEndDate { get; set; }

        public string? RuleStartDate { get; set; }

        public string? AccountingRule { get; set; }

        public string? Warehouse { get; set; }

        public string? MemoLine { get; set; }

        public string? UnitOfMeasure { get; set; }

        public string? ItemNumber { get; set; }

        public string? AllocatedFreightAmount { get; set; }

        public string? AssessableValue { get; set; }

        public string? SalesOrderDate { get; set; }

        public decimal? LineAmount { get; set; }

        public string? CreatedBy { get; set; }

        public DateTime? CreationDate { get; set; }

        public string? LastUpdatedBy { get; set; }

        public DateTime? LastUpdateDate { get; set; }

        public string? TransacationBusinessCategory { get; set; }

        public string? UserDefinedFiscalClassification { get; set; }

        public string? ProductFiscalClassification { get; set; }

        public string? ProductCategory { get; set; }

        public string? ProductType { get; set; }

        public string? LineIntendedUse { get; set; }

        public string? LineAmountIncludesTax { get; set; }

        public string? TaxInvoiceDate { get; set; }

        public string? TaxInvoiceNumber { get; set; }

        public string? TaxExemptionCertificateNumber { get; set; }

        public string? TaxExemptionHandling { get; set; }

        public string? TaxExemptionReason { get; set; }

        public List<Link>? Links { get; set; }
    }

    public class InvoiceLinesResponse : FusionResponseBase
    {
        public List<FusionInvoiceLine>? Items { get; set; }
    }
}