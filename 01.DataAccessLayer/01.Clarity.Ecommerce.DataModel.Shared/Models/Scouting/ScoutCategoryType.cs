// <copyright file="ScoutCategoryType.cs" company="clarity-ventures.com">
// Copyright (c) 2020-2023 clarity-ventures.com. All rights reserved.
// </copyright>
// <summary>Implements the scout category type class</summary>
#nullable enable
namespace Clarity.Ecommerce.Interfaces.DataModel
{
    public interface IScoutCategoryType : ITypableBase
    {
    }
}

namespace Clarity.Ecommerce.DataModel
{
    using Interfaces.DataModel;

    [SqlSchema("Scouting", "ScoutCategoryType")]
    public class ScoutCategoryType : TypableBase, IScoutCategoryType
    {
    }
}
