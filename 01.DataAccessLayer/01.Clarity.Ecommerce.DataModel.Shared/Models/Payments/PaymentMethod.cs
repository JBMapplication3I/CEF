// <copyright file="PaymentMethod.cs" company="clarity-ventures.com">
// Copyright (c) 2016-2023 clarity-ventures.com. All rights reserved.
// </copyright>
// <summary>Implements the payment method class</summary>
#nullable enable
namespace Clarity.Ecommerce.Interfaces.DataModel
{
    public interface IPaymentMethod : INameableBase
    {
    }
}

namespace Clarity.Ecommerce.DataModel
{
    using Interfaces.DataModel;

    [SqlSchema("Payments", "PaymentMethod")]
    public class PaymentMethod : NameableBase, IPaymentMethod
    {
    }
}
