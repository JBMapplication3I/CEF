// <copyright file="DiscountCodeService.cs" company="clarity-ventures.com">
// Copyright (c) 2016-2023 clarity-ventures.com. All rights reserved.
// </copyright>
// <summary>Implements the discount code service class</summary>
#nullable enable
namespace Clarity.Ecommerce.Service
{
    using System.Threading.Tasks;
    using JetBrains.Annotations;
    using ServiceStack;
    using Utilities;

    [PublicAPI,
        Authenticate,
        Route("/Discounts/DiscountCodes/ExistsByCode", "GET",
            Summary = "Use to determine if a discount code already exists")]
    public partial class CheckDiscountCodeExistsByCode : IReturn<int?>
    {
        [ApiMember(Name = nameof(Code), DataType = "string", ParameterType = "query", IsRequired = true,
            Description = "The discount code to check")]
        public string Code { get; set; } = null!;
    }

    [PublicAPI]
    public partial class DiscountCodeService
    {
        public async Task<object?> Get(CheckDiscountCodeExistsByCode request)
        {
            // TODO: Cached Research
            return await Workflows.DiscountCodes.CheckExistsByCodeAsync(
                    Contract.RequiresValidKey(request.Code),
                    null)
                .ConfigureAwait(false);
        }
    }
}
