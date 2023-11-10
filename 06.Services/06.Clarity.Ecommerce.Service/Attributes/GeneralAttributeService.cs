// <copyright file="GeneralAttributeService.cs" company="clarity-ventures.com">
// Copyright (c) 2020-2023 clarity-ventures.com. All rights reserved.
// </copyright>
// <summary>Implements the general attribute service class</summary>
#nullable enable
namespace Clarity.Ecommerce.Service
{
    using System.Threading.Tasks;
    using JetBrains.Annotations;

    [PublicAPI]
    public partial class GeneralAttributeService
    {
        /// <inheritdoc/>
        public override Task<object?> Get(GetGeneralAttributes request)
        {
            if (CurrentAPIKind == Enums.APIKind.Storefront)
            {
                request.HideFromStorefront = false;
            }
            return base.Get(request);
        }
    }
}
