// <copyright file="MappingMode.cs" company="clarity-ventures.com">
// Copyright (c) 2019-2023 clarity-ventures.com. All rights reserved.
// </copyright>
// <summary>Implements the mapping mode class</summary>
namespace Clarity.Ecommerce.Mapper
{
    using System.ComponentModel;

    /// <summary>Values that represent mapping modes.</summary>
    public enum MappingMode
    {
        /// <summary>Full Mapping Mode: All properties, including Related (1-0|1) and Associated Objects (1-n). This mode is ideal for when you need the full details of an object, such as in the Product Details page or the Admin Editors.</summary>
        [Description("Full Mapping Mode: All properties, including Related (1-0|1) and Associated Objects (1-n). This mode is ideal for when you need the full details of an object, such as in the Product Details page or the Admin Editors.")]
        Full = 0,

        /// <summary>Lite Mapping Mode: Most properties, excluding Related (1-0|1) and Associated Objects (1-n). Some cases may override to include these properties. This mode is ideal for summarized information, where you don't need the full details.</summary>
        [Description("Lite Mapping Mode: Most properties, excluding Related (1-0|1) and Associated Objects (1-n). Some cases may override to include these properties. This mode is ideal for summarized information, where you don't need the full details.")]
        Lite = 1,

        /// <summary>List Mapping Mode: Very limited properties, excluding Related (1-0|1) and Associated Objects (1-n) and properties that would generally return large data such as descriptions. Some cases may override to include these properties. This mode is ideal for Drop-downs as it contains very little information and is very quick.</summary>
        [Description("List Mapping Mode: Very limited properties, excluding Related (1-0|1) and Associated Objects (1-n) and properties that would generally return large data such as descriptions. Some cases may override to include these properties. This mode is ideal for Drop-downs as it contains very little information and is very quick.")]
        List = 2,
    }
}
