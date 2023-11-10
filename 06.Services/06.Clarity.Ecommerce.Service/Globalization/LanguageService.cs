// <copyright file="LanguageService.cs" company="clarity-ventures.com">
// Copyright (c) 2017-2023 clarity-ventures.com. All rights reserved.
// </copyright>
// <summary>Implements the language service class</summary>
#nullable enable
#pragma warning disable AsyncFixer01 // Unnecessary async/await usage
namespace Clarity.Ecommerce.Service
{
    using System.Threading.Tasks;

    public partial class LanguageService
    {
        public override async Task<object?> Get(GetLanguageByKey request)
        {
            request.Key = request.Key.Replace("%22", string.Empty);
            return await base.Get(request).ConfigureAwait(false);
        }

        public override async Task<object?> Get(CheckLanguageExistsByKey request)
        {
            request.Key = request.Key.Replace("%22", string.Empty);
            return await base.Get(request).ConfigureAwait(false);
        }

        public override async Task<object?> Delete(DeleteLanguageByKey request)
        {
            request.Key = request.Key.Replace("%22", string.Empty);
            return await base.Delete(request).ConfigureAwait(false);
        }

        public override async Task<object?> Patch(DeactivateLanguageByKey request)
        {
            request.Key = request.Key.Replace("%22", string.Empty);
            return await base.Patch(request).ConfigureAwait(false);
        }

        public override async Task<object?> Patch(ReactivateLanguageByKey request)
        {
            request.Key = request.Key.Replace("%22", string.Empty);
            return await base.Patch(request).ConfigureAwait(false);
        }
    }
}
