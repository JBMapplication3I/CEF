// <copyright file="AddPaymentToSampleRequest.cs" company="clarity-ventures.com">
// Copyright (c) 2021-2023 clarity-ventures.com. All rights reserved.
// </copyright>
// <summary>Implements the add payment to sample request class</summary>
namespace Clarity.Ecommerce.Providers.SampleRequestHandlers.Actions.Services.Endpoints
{
    using JetBrains.Annotations;
    using Models;
    using Service;
    using ServiceStack;

    /// <summary>An add payment to sample request.</summary>
    /// <seealso cref="ImplementsIDOnBodyBase"/>
    /// <seealso cref="IReturn{CEFActionResponse}"/>
    [PublicAPI, // UsedInStorefront,
     Authenticate,
     Route("/Providers/Sampling/Actions/AddPayment", "PATCH",
         Summary = "Take payment information from the customer and perform an Authorize and/or Capture from the"
             + " Payment Provider. If the total of all payments is less than the Balance Due, the status will be set"
             + " to 'Partial Payment Received', otherwise 'Full Payment Received'. An email notification will be sent"
             + " to the customer.")]
    public class AddPaymentToSampleRequest : ImplementsIDOnBodyBase, IReturn<CEFActionResponse>
    {
        /// <summary>Gets or sets the payment.</summary>
        /// <value>The payment.</value>
        [ApiMember(Name = nameof(Payment), DataType = "PaymentModel", ParameterType = "body", IsRequired = true,
            Description = "The Payment information. This will only be Authorized and not Captured with the Payment Provider")]
        public PaymentModel Payment { get; set; } = null!;
    }
}
