// <copyright file="VincarioResponse.cs" company="clarity-ventures.com">
// Copyright (c) clarity-ventures.com. All rights reserved.
// </copyright>
namespace Clarity.Ecommerce.Providers.VinLookup.Vincario
{
    public class VincarioResponse
    {
        public Decode[]? Decode { get; set; }

        public bool Error { get; set; }

        public string? Message { get; set; }

        public string[]? Reasons { get; set; }
    }
}
