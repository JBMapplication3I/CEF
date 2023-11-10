// <copyright file="SubmitRequestForQuoteForGenericProducts.cs" company="clarity-ventures.com">
// Copyright (c) 2021-2023 clarity-ventures.com. All rights reserved.
// </copyright>
// <summary>Implements the submit request for quote for generic products class</summary>
namespace Clarity.Ecommerce.Providers.SalesQuoteHandlers.Actions.Services.Endpoints
{
    using JetBrains.Annotations;
    using Models;
    using Service;
    using ServiceStack;

    /// <summary>A submit request for quote for generic products.</summary>
    /// <seealso cref="SalesQuoteModel"/>
    /// <seealso cref="IReturn{CEFActionResponse}"/>
    [PublicAPI, UsedInStorefront,
     Authenticate,
     Route("/Providers/Quoting/Actions/SubmitForGenericProducts", "POST",
         Summary = "Submit a quote for processing by the store")]
    public class SubmitRequestForQuoteForGenericProducts : SalesQuoteModel, IReturn<CEFActionResponse>
    {
        /// <summary>Gets or sets the do share business card with supplier.</summary>
        /// <value>The do share business card with supplier.</value>
        [ApiMember(Name = nameof(DoShareBusinessCardWithSupplier), DataType = "bool?", ParameterType = "body", IsRequired = false)]
        public bool? DoShareBusinessCardWithSupplier { get; set; }
    }
}
