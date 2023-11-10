// <copyright file="ItemUOMResponse.cs" company="clarity-ventures.com">
// Copyright (c) 2018-2023 clarity-ventures.com. All rights reserved.
// </copyright>
// <summary>Implements the ItemUOMResponse class</summary>

namespace Clarity.Clients.JBM
{
    using System;
    using System.Collections.Generic;

    public class ItemUOM
    {
        public string? InventoryItemId { get; set; }

        public string? ConversionId { get; set; }

        public int IntraclassConversion { get; set; }

        public DateTime? IntraclassConversionEndDate { get; set; }

        public string? ItemNumber { get; set; }

        public string? CreatedBy { get; set; }

        public DateTime? CreationDate { get; set; }

        public DateTime? LastUpdateDate { get; set; }

        public string? LastUpdatedBy { get; set; }

        public List<Link>? Links { get; set; }
    }

    public class ItemUOMResponse : FusionResponseBase
    {
        public List<ItemUOM>? ItemUOMs { get; set; }
    }
}