// <copyright file="DiscountCodeCRUDWorkflow.extended.cs" company="clarity-ventures.com">
// Copyright (c) 2018-2023 clarity-ventures.com. All rights reserved.
// </copyright>
// <summary>Implements the discount code workflow class</summary>
namespace Clarity.Ecommerce.Workflow
{
    using System.Data.Entity;
    using System.Linq;
    using System.Threading.Tasks;
    using Interfaces.Models;

    public partial class DiscountCodeWorkflow
    {
        /// <inheritdoc/>
        public async Task<int?> CheckExistsByCodeAsync(string code, string? contextProfileName)
        {
            using var context = RegistryLoaderWrapper.GetContext(contextProfileName);
            return await context.DiscountCodes
                .FilterByActive(true)
                .FilterDiscountCodesByCode(code)
                .Select(x => x.ID)
                .SingleOrDefaultAsync()
                .ConfigureAwait(false);
        }
    }
}
