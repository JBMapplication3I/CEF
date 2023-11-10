// <copyright file="CurrencyImageType.cs" company="clarity-ventures.com">
// Copyright (c) 2016-2023 clarity-ventures.com. All rights reserved.
// </copyright>
// <summary>Implements the currency image type class</summary>
#nullable enable
namespace Clarity.Ecommerce.Interfaces.DataModel
{
    public interface ICurrencyImageType : ITypableBase
    {
    }
}

namespace Clarity.Ecommerce.DataModel
{
    using Interfaces.DataModel;

    [SqlSchema("Currencies", "CurrencyImageType")]
    public class CurrencyImageType : TypableBase, ICurrencyImageType
    {
    }
}
