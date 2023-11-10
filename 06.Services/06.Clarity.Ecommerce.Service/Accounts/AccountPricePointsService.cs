// <copyright file="AccountPricePointsService.cs" company="clarity-ventures.com">
// Copyright (c) 2016-2023 clarity-ventures.com. All rights reserved.
// </copyright>
// <summary>Implements the account price points service class</summary>
#nullable enable
namespace Clarity.Ecommerce.Service
{
    using System.Threading.Tasks;
    using JetBrains.Annotations;
    using ServiceStack;
    using Utilities;

    /// <summary>A get account price point by keys.</summary>
    [PublicAPI,
     Authenticate,
     Route("/Accounts/AccountPricePoint/Keys/{AccountKey}/{PricePointKey}", "GET",
        Summary = "Use to get a specific Account Price Point")]
    public class GetAccountPricePointByKeys
    {
        /// <summary>Gets or sets the account key.</summary>
        /// <value>The account key.</value>
        [ApiMember(Name = nameof(AccountKey), DataType = "string", ParameterType = "path", IsRequired = true)]
        public string AccountKey { get; set; } = null!;

        /// <summary>Gets or sets the price point key.</summary>
        /// <value>The price point key.</value>
        [ApiMember(Name = nameof(PricePointKey), DataType = "string", ParameterType = "path", IsRequired = true)]
        public string PricePointKey { get; set; } = null!;
    }

    public partial class CEFSharedService
    {
        public async Task<object?> Get(GetAccountPricePointByKeys request)
        {
            if (!Contract.CheckValidKey(request.AccountKey))
            {
                request.AccountKey = (await CurrentAccountPricePointKeyAsync().ConfigureAwait(false))!;
            }
            return await Workflows.AccountPricePoints.GetAsync(
                    keys: (request.AccountKey, request.PricePointKey),
                    contextProfileName: ServiceContextProfileName)
                .ConfigureAwait(false);
        }
    }
}
