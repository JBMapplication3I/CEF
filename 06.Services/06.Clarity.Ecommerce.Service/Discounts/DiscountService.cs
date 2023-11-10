// <copyright file="DiscountService.cs" company="clarity-ventures.com">
// Copyright (c) 2016-2023 clarity-ventures.com. All rights reserved.
// </copyright>
// <summary>Implements the discount service class</summary>
#nullable enable
namespace Clarity.Ecommerce.Service
{
    using System.Collections.Generic;
    using System.Threading.Tasks;
    using Models;
    using ServiceStack;

    public partial class GetDiscounts
    {
        /// <summary>Gets or sets the type of the source.</summary>
        /// <value>The type of the source.</value>
        [ApiMember(Name = nameof(SourceType), DataType = "query", ParameterType = "int", IsRequired = false)]
        public int SourceType { get; set; }

        /// <summary>Gets or sets the identifier of the source.</summary>
        /// <value>The identifier of the source.</value>
        [ApiMember(Name = nameof(SourceId), DataType = "query", ParameterType = "int", IsRequired = false)]
        public int SourceId { get; set; }

        /// <summary>Gets or sets the discount codes.</summary>
        /// <value>The discount codes.</value>
        [ApiMember(Name = nameof(DiscountCodes), DataType = "query", ParameterType = "List<string>", IsRequired = false)]
        public List<string>? DiscountCodes { get; set; }
    }

    [Route("/Discounts/Discount/Definition/{ID}", "GET",
        Summary = "Use to get a specific sales discount definition")]
    public class GetDiscountDefinition : ImplementsIDBase, IReturn<DiscountModel>
    {
    }

    [Route("/Discounts/Discount/Definitions", "GET",
        Summary = "Use to get multiple sales discounts definitions")]
    public class GetDiscountDefinitions : IReturn<List<DiscountModel>>
    {
        /// <summary>Gets or sets the search term.</summary>
        /// <value>The search term.</value>
        [ApiMember(Name = nameof(SearchTerm), DataType = "query", ParameterType = "string", IsRequired = false)]
        public string? SearchTerm { get; set; }

        /// <summary>Gets or sets the active.</summary>
        /// <value>The active.</value>
        [ApiMember(Name = nameof(Active), DataType = "query", ParameterType = "bool", IsRequired = false)]
        public bool? Active { get; set; }
    }

    /// <summary>A Route to change discount end date by ID.</summary>
    /// <seealso cref="DiscountModel"/>
    /// <seealso cref="IReturn{CEFActionResponse_int}"/>
    [Authenticate, RequiredPermission("Discounts.Discount.Update"),
        UsedInAdmin,
        Route("/Discounts/Discount/End/ID/{ID}", "PUT", Priority = 1,
            Summary = "Use to end an existing discount by ID.")]
    public partial class EndDiscountByID : ImplementsIDBase, IReturn<CEFActionResponse<bool>>
    {
    }

    public partial class CEFSharedService
    {
        public async Task<object?> Get(GetDiscountDefinition request)
        {
            return await Workflows.Discounts.GetAsync(request.ID, contextProfileName: null).ConfigureAwait(false);
        }

        public async Task<object?> Get(GetDiscountDefinitions request)
        {
            return await Workflows.Discounts.SearchAsync(request.SearchTerm, request.Active, null).ConfigureAwait(false);
        }

        public async Task<object?> Put(EndDiscountByID request)
        {
            return await Workflows.Discounts.EndDiscountByIDAsync(request.ID, contextProfileName: null).ConfigureAwait(false);
        }
    }
}
