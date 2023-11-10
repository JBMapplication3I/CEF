// <copyright file="LanguageCRUDWorkflow.extended.cs" company="clarity-ventures.com">
// Copyright (c) 2016-2023 clarity-ventures.com. All rights reserved.
// </copyright>
// <summary>Implements the language workflow class</summary>
namespace Clarity.Ecommerce.Workflow
{
    using System.Threading.Tasks;
    using DataModel;
    using Interfaces.DataModel;
    using Models;

    /// <summary>A language workflow.</summary>
    public partial class LanguageWorkflow
    {
        /// <inheritdoc/>
        protected override async Task<CEFActionResponse> DeleteAsync(
            Language? entity,
            IClarityEcommerceEntities context)
        {
            if (entity == null)
            {
                return CEFAR.PassingCEFAR();
            }
            await DeleteAssociatedImagesAsync<LanguageImage>(entity.ID, context).ConfigureAwait(false);
            return await base.DeleteAsync(entity, context).ConfigureAwait(false);
        }
    }
}
