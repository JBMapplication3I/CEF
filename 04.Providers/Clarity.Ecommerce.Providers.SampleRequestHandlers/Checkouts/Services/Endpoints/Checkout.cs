// <copyright file="Checkout.cs" company="clarity-ventures.com">
// Copyright (c) 2021-2023 clarity-ventures.com. All rights reserved.
// </copyright>
// <summary>Implements the checkout class</summary>
namespace Clarity.Ecommerce.Providers.SampleRequestHandlers.Checkouts.Services.Endpoints
{
    using JetBrains.Annotations;
    using Models;
    using ServiceStack;

    /// <summary>A sample request checkout.</summary>
    /// <seealso cref="CheckoutModel"/>
    /// <seealso cref="IReturn{CheckoutResult}"/>
    [PublicAPI, // UsedInStorefront,
     Route("/Providers/Sampling/Actions/CheckoutSampleRequestCart", "POST",
         Summary = "Checkout the current sample request cart")]
    public class SampleRequestCheckout : CheckoutModel, IReturn<CheckoutResult>
    {
    }
}
