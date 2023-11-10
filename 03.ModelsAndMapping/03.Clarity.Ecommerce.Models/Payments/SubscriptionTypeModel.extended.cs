// <copyright file="SubscriptionTypeModel.extended.cs" company="clarity-ventures.com">
// Copyright (c) 2016-2023 clarity-ventures.com. All rights reserved.
// </copyright>
// <summary>Implements the subscription Type model class</summary>
namespace Clarity.Ecommerce.Models
{
    using System.Collections.Generic;
    using System.Linq;
    using Interfaces.Models;

    public partial class SubscriptionTypeModel
    {
        /// <inheritdoc cref="ISubscriptionTypeModel.ProductSubscriptionTypes"/>
        public List<ProductSubscriptionTypeModel>? ProductSubscriptionTypes { get; set; }

        /// <inheritdoc/>
        List<IProductSubscriptionTypeModel>? ISubscriptionTypeModel.ProductSubscriptionTypes { get => ProductSubscriptionTypes?.ToList<IProductSubscriptionTypeModel>(); set => ProductSubscriptionTypes = value?.Cast<ProductSubscriptionTypeModel>().ToList(); }

        /// <inheritdoc cref="ISubscriptionTypeModel.SubscriptionTypeRepeatTypes"/>
        public List<SubscriptionTypeRepeatTypeModel>? SubscriptionTypeRepeatTypes { get; set; }

        /// <inheritdoc/>
        List<ISubscriptionTypeRepeatTypeModel>? ISubscriptionTypeModel.SubscriptionTypeRepeatTypes { get => SubscriptionTypeRepeatTypes?.ToList<ISubscriptionTypeRepeatTypeModel>(); set => SubscriptionTypeRepeatTypes = value?.Cast<SubscriptionTypeRepeatTypeModel>().ToList(); }
    }
}
