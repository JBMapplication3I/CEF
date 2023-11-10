// <copyright file="PaymentService.cs" company="clarity-ventures.com">
// Copyright (c) 2016-2023 clarity-ventures.com. All rights reserved.
// </copyright>
// <summary>Implements the payment service class</summary>
#nullable enable
#pragma warning disable AsyncFixer01 // Unnecessary async/await usage
namespace Clarity.Ecommerce.Service
{
    using System;
    using System.Threading.Tasks;
    using Interfaces.Providers.Payments;
    using JetBrains.Annotations;
    using Models;
    using ServiceStack;

    /// <summary>Retrieves a Braintree token.</summary>
    /// <seealso cref="IReturn{CEFActionResponse_string}"/>
    [PublicAPI,
        Route("/Payments/GetPaymentsProviderAuthenticationToken", "GET",
            Summary = "Retrieve a payments provider authentication token.")]
    public partial class GetPaymentsProviderAuthenticationToken : IReturn<CEFActionResponse<string>>
    {
        [ApiMember(Name = nameof(PaymentsProviderName), ParameterType = "body", DataType = "string", IsRequired = true)]
        public string PaymentsProviderName { get; set; } = null!;
    }

    /// <summary>Retrieves a payments provider transaction report.</summary>
    /// <seealso cref="IReturn{CEFActionResponse_ITransactionResponse}"/>
    [PublicAPI,
        Route("/Payments/GetPaymentTransactionsReport", "GET",
            Summary = "Retrieve a payments provider transaction report by id or date range.")]
    public partial class GetPaymentTransactionReport : IReturn<CEFActionResponse<ITransactionResponse>>
    {
        [ApiMember(Name = nameof(StartDate), ParameterType = "query", DataType = "DateTime", IsRequired = false)]
        public DateTime? StartDate { get; set; }

        [ApiMember(Name = nameof(EndDate), ParameterType = "query", DataType = "DateTime", IsRequired = false)]
        public DateTime? EndDate { get; set; }

        [ApiMember(Name = nameof(TransactionID), ParameterType = "query", DataType = "string", IsRequired = false)]
        public string? TransactionID { get; set; }
    }

    public partial class PaymentService
    {
        public async Task<object?> Get(GetPaymentsProviderAuthenticationToken request)
        {
            return await Workflows.Payments.GetPaymentsProviderAuthenticationTokenAsync(
                    request.PaymentsProviderName,
                    ServiceContextProfileName)
                .ConfigureAwait(false);
        }

        public async Task<object?> Get(GetPaymentTransactionReport request)
        {
            return await Workflows.Payments.GetPaymentTransactionsReportAsync(
                    request.StartDate,
                    request.EndDate,
                    request.TransactionID,
                    ServiceContextProfileName)
                .ConfigureAwait(false);
        }
    }
}
