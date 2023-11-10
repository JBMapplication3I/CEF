// <copyright file="ProcessPaymentResponse.cs" company="clarity-ventures.com">
// Copyright (c) 2021-2023 clarity-ventures.com. All rights reserved.
// </copyright>
// <summary>Implements the process payment response class</summary>
namespace Clarity.Ecommerce.Providers.Checkouts
{
    using Interfaces.Providers.Payments;
    using Payments;

    /// <inheritdoc/>
    public class ProcessPaymentResponse : IProcessPaymentResponse
    {
        /// <summary>A Default Constructor</summary>
        public ProcessPaymentResponse()
        {
            PaymentResponse = new();
        }

        /// <summary>A quick set constructor</summary>
        /// <param name="token">         The Token.</param>
        /// <param name="transactionID"> The TransactionID.</param>
        /// <param name="makeInvoice">   The Make Invoice.</param>
        /// <param name="balanceDue">    The Balance Due.</param>
        /// <param name="paymentResponse">The Payment Response</param>
        public ProcessPaymentResponse(
            string? token,
            string? transactionID,
            bool makeInvoice,
            decimal balanceDue,
            IPaymentResponse? paymentResponse = null)
        {
            Token = token;
            TransactionID = transactionID;
            MakeInvoice = makeInvoice;
            BalanceDue = balanceDue;
            PaymentResponse = paymentResponse as PaymentResponse ?? new PaymentResponse();
        }

        /// <inheritdoc/>
        public string? Token { get; set; }

        /// <inheritdoc/>
        public string? TransactionID { get; set; }

        /// <inheritdoc/>
        public bool MakeInvoice { get; set; }

        /// <inheritdoc/>
        public decimal BalanceDue { get; set; }

        /// <inheritdoc cref="IProcessPaymentResponse.PaymentResponse"/>
        public PaymentResponse PaymentResponse { get; set; }

        /// <inheritdoc/>
        IPaymentResponse IProcessPaymentResponse.PaymentResponse { get => PaymentResponse; set => PaymentResponse = (PaymentResponse)value; }
    }
}
