// <copyright file="SalesInvoiceLateFee.cs" company="clarity-ventures.com">
// Copyright (c) 2021-2023 clarity-ventures.com. All rights reserved.
// </copyright>
// <summary>Implements the sales invoice late fee class</summary>
namespace Clarity.Ecommerce.JSConfigs
{
    /// <summary>The sales invoice late fee.</summary>
    public class SalesInvoiceLateFee
    {
        /// <summary>Initializes a new instance of the <see cref="SalesInvoiceLateFee"/> class.</summary>
        public SalesInvoiceLateFee()
        {
        }

        /// <summary>Initializes a new instance of the <see cref="SalesInvoiceLateFee"/> class.</summary>
        /// <param name="day">   The day.</param>
        /// <param name="amount">The amount.</param>
        /// <param name="kind">  The kind.</param>
        public SalesInvoiceLateFee(int day, decimal amount, char kind)
        {
            Day = day;
            Amount = amount;
            Kind = kind;
        }

        /// <summary>Gets or sets the day.</summary>
        /// <value>The day.</value>
        public int Day { get; set; }

        /// <summary>Gets or sets the amount.</summary>
        /// <value>The amount.</value>
        public decimal Amount { get; set; }

        /// <summary>Gets or sets the kind.</summary>
        /// <value>The kind.</value>
        public char Kind { get; set; }
    }
}
