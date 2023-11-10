// <copyright file="ProfanityFilter.cs" company="clarity-ventures.com">
// Copyright (c) 2016-2023 clarity-ventures.com. All rights reserved.
// </copyright>
// <summary>Implements the profanity filter class</summary>
#nullable enable
namespace Clarity.Ecommerce.Interfaces.DataModel
{
    public interface IProfanityFilter : IBase
    {
    }
}

namespace Clarity.Ecommerce.DataModel
{
    using Interfaces.DataModel;

    [SqlSchema("Contacts", "ProfanityFilter")]
    public class ProfanityFilter : Base, IProfanityFilter
    {
    }
}
