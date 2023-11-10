// <copyright file="AwardSalesQuoteLineItem.cs" company="clarity-ventures.com">
// Copyright (c) 2021-2023 clarity-ventures.com. All rights reserved.
// </copyright>
// <summary>Implements the award sales quote line item class</summary>
namespace Clarity.Ecommerce.Providers.SalesQuoteHandlers.Actions.Services.Endpoints
{
    using JetBrains.Annotations;
    using Models;
    using Service;
    using ServiceStack;

    /// <summary>An award sales quote line item.</summary>
    /// <seealso cref="IReturn{CEFActionResponse}"/>
    [PublicAPI, UsedInAdmin,
     Authenticate, RequiredPermission("Quoting.SalesQuote.AwardLineItem"),
     Route("/Providers/Quoting/Actions/AwardItem/{OriginalItemID}/{ResponseItemID}", "PATCH",
        Summary = "Marks the original line item as awarded out using the response item")]
    public class AwardSalesQuoteLineItem : IReturn<CEFActionResponse>
    {
        /// <summary>Gets or sets the identifier of the original item.</summary>
        /// <value>The identifier of the original item.</value>
        [ApiMember(Name = nameof(OriginalItemID), DataType = "int", ParameterType = "path", IsRequired = true)]
        public int OriginalItemID { get; set; }

        /// <summary>Gets or sets the identifier of the response item.</summary>
        /// <value>The identifier of the response item.</value>
        [ApiMember(Name = nameof(ResponseItemID), DataType = "int", ParameterType = "path", IsRequired = true)]
        public int ResponseItemID { get; set; }
    }
}
