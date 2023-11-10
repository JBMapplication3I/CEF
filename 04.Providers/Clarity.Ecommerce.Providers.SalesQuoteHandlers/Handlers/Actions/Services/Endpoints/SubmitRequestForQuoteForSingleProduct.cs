// <copyright file="SubmitRequestForQuoteForSingleProduct.cs" company="clarity-ventures.com">
// Copyright (c) 2021-2023 clarity-ventures.com. All rights reserved.
// </copyright>
// <summary>Implements the submit request for quote for single product class</summary>
namespace Clarity.Ecommerce.Providers.SalesQuoteHandlers.Actions.Services.Endpoints
{
    using JetBrains.Annotations;
    using Models;
    using Service;
    using ServiceStack;

    /// <summary>A submit request for quote for single product.</summary>
    /// <seealso cref="SalesQuoteModel"/>
    /// <seealso cref="IReturn{CEFActionResponse}"/>
    [PublicAPI, UsedInStorefront,
     Authenticate,
     Route("/Providers/Quoting/Actions/SubmitForSingleProduct", "POST",
         Summary = "Submit a quote for processing by the store")]
    public class SubmitRequestForQuoteForSingleProduct : SalesQuoteModel, IReturn<CEFActionResponse>
    {
        /// <summary>Gets or sets the do share business card with supplier.</summary>
        /// <value>The do share business card with supplier.</value>
        [ApiMember(Name = nameof(DoShareBusinessCardWithSupplier), DataType = "bool?", ParameterType = "body", IsRequired = false)]
        public bool? DoShareBusinessCardWithSupplier { get; set; }

        /// <summary>Gets or sets the do recommend other suppliers.</summary>
        /// <value>The do recommend other suppliers.</value>
        [ApiMember(Name = nameof(DoRecommendOtherSuppliers), DataType = "bool?", ParameterType = "body", IsRequired = false)]
        public bool? DoRecommendOtherSuppliers { get; set; }
    }
}
