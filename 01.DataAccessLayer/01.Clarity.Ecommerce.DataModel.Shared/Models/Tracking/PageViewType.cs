﻿// <copyright file="PageViewType.cs" company="clarity-ventures.com">
// Copyright (c) 2016-2023 clarity-ventures.com. All rights reserved.
// </copyright>
// <summary>Implements the page view type class</summary>
#nullable enable
namespace Clarity.Ecommerce.Interfaces.DataModel
{
    public interface IPageViewType : ITypableBase
    {
    }
}

namespace Clarity.Ecommerce.DataModel
{
    using Interfaces.DataModel;

    [SqlSchema("Tracking", "PageViewType")]
    public class PageViewType : TypableBase, IPageViewType
    {
    }
}
