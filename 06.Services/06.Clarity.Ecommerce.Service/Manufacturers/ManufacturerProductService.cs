// <copyright file="ManufacturerProductService.cs" company="clarity-ventures.com">
// Copyright (c) 2016-2023 clarity-ventures.com. All rights reserved.
// </copyright>
// <summary>Implements the manufacturer service class</summary>
#nullable enable
namespace Clarity.Ecommerce.Service
{
    using System;
    using System.Collections.Generic;
    using System.Threading.Tasks;
    using Interfaces.Models;
    using JetBrains.Annotations;
    using Models;
    using ServiceStack;

    [Authenticate]
    [Route("/Manufacturers/ManufacturerProducts/ByProductID/{ProductID}", "GET",
        Summary = "Get Manufacturers By ProductID")]
    public class GetManufacturerProductsByProduct : IReturn<List<ManufacturerProductModel>>
    {
        [ApiMember(Name = nameof(ProductID), DataType = "int", ParameterType = "path", IsRequired = true)]
        public int ProductID { get; set; }
    }

    [PublicAPI,
        Authenticate,
        Route("/Manufacturers/Manufacturer/Current/Administration", "GET",
            Summary = "Use to get the manufacturer that the current user has administrative rights to (limited to manufacturer admins)")]
    public partial class GetCurrentManufacturerAdministration : IReturn<CEFActionResponse<ManufacturerModel>>
    {
    }

    public partial class CEFSharedService
    {
        public async Task<object?> Get(GetManufacturerProductsByProduct request)
        {
            // TODO: Cached Research
            return await GetPagedResultsAsync<IManufacturerProductModel, ManufacturerProductModel, IManufacturerProductSearchModel, ManufacturerProductPagedResults>(
                    new ManufacturerProductSearchModel { ProductID = request.ProductID },
                    false,
                    Workflows.ManufacturerProducts)
                .ConfigureAwait(false);
        }

        // Manufacturer Administration
        public async Task<object?> Get(GetCurrentManufacturerAdministration _)
        {
            // NOTE: Never cached, for admins only
            try
            {
                var result = (ManufacturerModel?)await Workflows.Manufacturers.GetAsync(
                        await CurrentManufacturerForManufacturerAdminIDOrThrow401Async().ConfigureAwait(false),
                        contextProfileName: null)
                    .ConfigureAwait(false);
                return result.WrapInPassingCEFARIfNotNull();
            }
            catch (Exception ex)
            {
                await Logger.LogErrorAsync(
                        name: "GetCurrentManufacturerAdministration Error",
                        message: ex.Message,
                        ex: ex,
                        contextProfileName: null)
                    .ConfigureAwait(false);
                return CEFAR.FailingCEFAR<ManufacturerModel>(
                    "Unable to locate current Manufacturer the user would be administrator of");
            }
        }
    }
}
