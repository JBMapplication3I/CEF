// <copyright file="ProductKitsService.cs" company="clarity-ventures.com">
// Copyright (c) 2016-2023 clarity-ventures.com. All rights reserved.
// </copyright>
// <summary>Implements the product kits service class</summary>
#nullable enable
namespace Clarity.Ecommerce.Service
{
    using System.Collections.Generic;
    using System.Threading.Tasks;
    using Models;
    using ServiceStack;

    [Route("/Products/Kits/ComponentBOM/ID/{ID}", "GET")]
    public class KitComponentBOMByID : ImplementsIDBase, IReturn<List<ProductModel>>
    {
    }

    [Route("/Products/Kits/ComponentBOM/Key/{Key}", "GET")]
    public class KitComponentBOMByKey : ImplementsKeyBase, IReturn<List<ProductModel>>
    {
    }

    [Route("/Products/Kits/ComponentBOMUp/ID/{ID}", "GET")]
    public class KitComponentBOMUpByID : ImplementsIDBase, IReturn<List<ProductModel>>
    {
    }

    [Route("/Products/Kits/ComponentBOMUp/Key/{Key}", "GET")]
    public class KitComponentBOMUpByKey : ImplementsKeyBase, IReturn<List<ProductModel>>
    {
    }

    [Route("/Products/Kits/ComponentBOMDown/ID/{ID}", "GET")]
    public class KitComponentBOMDownByID : ImplementsIDBase, IReturn<List<ProductModel>>
    {
    }

    [Route("/Products/Kits/ComponentBOMDown/Key/{Key}", "GET")]
    public class KitComponentBOMDownByKey : ImplementsKeyBase, IReturn<List<ProductModel>>
    {
    }

    public class KitMaintenanceBaseWithID
    {
        /// <summary>Gets or sets the identifier.</summary>
        /// <value>The identifier.</value>
        [ApiMember(Name = nameof(ID), DataType = "int", ParameterType = "body", IsRequired = true)]
        public int ID { get; set; }

        /// <summary>Gets or sets the quantity.</summary>
        /// <value>The quantity.</value>
        [ApiMember(Name = nameof(Quantity), DataType = "int?", ParameterType = "body", IsRequired = false,
            Description = "Defaults to 1 if not set")]
        public int? Quantity { get; set; }

        /// <summary>Gets or sets the identifier of the location section.</summary>
        /// <value>The identifier of the location section.</value>
        [ApiMember(Name = nameof(LocationSectionID), DataType = "int?", ParameterType = "body", IsRequired = false,
            Description = "Defaults to first for the product if not set")]
        public int? LocationSectionID { get; set; }
    }

    /// <summary>A kit maintenance base with key.</summary>
    public class KitMaintenanceBaseWithKey
    {
        /// <summary>Gets or sets the key.</summary>
        /// <value>The key.</value>
        [ApiMember(Name = nameof(Key), DataType = "string", ParameterType = "body", IsRequired = true)]
        public string Key { get; set; } = null!;

        /// <summary>Gets or sets the quantity.</summary>
        /// <value>The quantity.</value>
        [ApiMember(Name = nameof(Quantity), DataType = "int?", ParameterType = "body", IsRequired = false,
            Description = "Defaults to 1 if not set")]
        public int? Quantity { get; set; }

        /// <summary>Gets or sets the identifier of the location section.</summary>
        /// <value>The identifier of the location section.</value>
        [ApiMember(Name = nameof(LocationSectionID), DataType = "int?", ParameterType = "body", IsRequired = false,
            Description = "Defaults to first for the product if not set")]
        public int? LocationSectionID { get; set; }
    }

    [Route("/Products/Kits/AssembleInventoryByID", "PATCH")]
    public class AssembleKitInventoryByID : KitMaintenanceBaseWithID, IReturn<bool>
    {
    }

    [Route("/Products/Kits/AssembleInventoryByKey", "PATCH")]
    public class AssembleKitInventoryByKey : KitMaintenanceBaseWithKey, IReturn<bool>
    {
    }

    [Route("/Products/Kits/BreakInventoryApartByID", "PATCH")]
    public class BreakKitInventoryApartByID : KitMaintenanceBaseWithID, IReturn<bool>
    {
    }

    [Route("/Products/Kits/BreakInventoryApartByKey", "PATCH")]
    public class BreakKitInventoryApartByKey : KitMaintenanceBaseWithKey, IReturn<bool>
    {
    }

    [Route("/Products/Kits/ReassembleBrokenInventoryByID", "PATCH")]
    public class ReassembleKitInventoryByID : KitMaintenanceBaseWithID, IReturn<bool>
    {
    }

    [Route("/Products/Kits/ReassembleBrokenInventoryByKey", "PATCH")]
    public class ReassembleKitInventoryByKey : KitMaintenanceBaseWithKey, IReturn<bool>
    {
    }

    public partial class CEFSharedService
    {
        public async Task<object?> Get(KitComponentBOMByID request)
        {
            return await Workflows.ProductKits.KitComponentBOMFullAsync(request.ID, null).ConfigureAwait(false);
        }

        public async Task<object?> Get(KitComponentBOMByKey request)
        {
            return await Workflows.ProductKits.KitComponentBOMFullAsync(request.Key, null).ConfigureAwait(false);
        }

        public async Task<object?> Get(KitComponentBOMUpByID request)
        {
            return await Workflows.ProductKits.KitComponentBOMUpAsync(request.ID, null).ConfigureAwait(false);
        }

        public async Task<object?> Get(KitComponentBOMUpByKey request)
        {
            return await Workflows.ProductKits.KitComponentBOMUpAsync(request.Key, null).ConfigureAwait(false);
        }

        public async Task<object?> Get(KitComponentBOMDownByID request)
        {
            return await Workflows.ProductKits.KitComponentBOMDownAsync(request.ID, null).ConfigureAwait(false);
        }

        public async Task<object?> Get(KitComponentBOMDownByKey request)
        {
            return await Workflows.ProductKits.KitComponentBOMDownAsync(request.Key, null).ConfigureAwait(false);
        }

        public async Task<object?> Patch(AssembleKitInventoryByID request)
        {
            return await Workflows.ProductKits.AssembleKitInventoryAsync(request.ID, request.Quantity, request.LocationSectionID, null).ConfigureAwait(false);
        }

        public async Task<object?> Patch(AssembleKitInventoryByKey request)
        {
            return await Workflows.ProductKits.AssembleKitInventoryAsync(request.Key, request.Quantity, request.LocationSectionID, null).ConfigureAwait(false);
        }

        public async Task<object?> Patch(BreakKitInventoryApartByID request)
        {
            return await Workflows.ProductKits.BreakKitInventoryApartAsync(request.ID, request.Quantity, request.LocationSectionID, null).ConfigureAwait(false);
        }

        public async Task<object?> Patch(BreakKitInventoryApartByKey request)
        {
            return await Workflows.ProductKits.BreakKitInventoryApartAsync(request.Key, request.Quantity, request.LocationSectionID, null).ConfigureAwait(false);
        }

        public async Task<object?> Patch(ReassembleKitInventoryByID request)
        {
            return await Workflows.ProductKits.ReassembleBrokenKitInventoryAsync(request.ID, request.Quantity, request.LocationSectionID, null).ConfigureAwait(false);
        }

        public async Task<object?> Patch(ReassembleKitInventoryByKey request)
        {
            return await Workflows.ProductKits.ReassembleBrokenKitInventoryAsync(request.Key, request.Quantity, request.LocationSectionID, null).ConfigureAwait(false);
        }
    }
}
