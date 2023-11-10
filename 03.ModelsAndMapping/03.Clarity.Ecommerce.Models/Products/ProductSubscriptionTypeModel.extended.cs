// <copyright file="ProductSubscriptionTypeModel.extended.cs" company="clarity-ventures.com">
// Copyright (c) 2016-2023 clarity-ventures.com. All rights reserved.
// </copyright>
// <summary>Implements the product SubscriptionType model class</summary>
namespace Clarity.Ecommerce.Models
{
    using System.Collections.Generic;
    using System.Linq;
    using Interfaces.Models;

    /// <summary>A data Model for the product category.</summary>
    /// <seealso cref="BaseModel"/>
    /// <seealso cref="IProductSubscriptionTypeModel"/>
    public partial class ProductSubscriptionTypeModel
    {
        /// <inheritdoc/>
        public int? SortOrder { get; set; }

        #region Related Objects
        /// <inheritdoc/>
        public int SubscriptionTypeRepeatTypeID { get; set; }

        /// <inheritdoc/>
        public string? SubscriptionTypeRepeatTypeKey { get; set; }

        /// <inheritdoc cref="IProductSubscriptionTypeModel.SubscriptionTypeRepeatType"/>
        public SubscriptionTypeRepeatTypeModel? SubscriptionTypeRepeatType { get; set; }

        /// <inheritdoc/>
        ISubscriptionTypeRepeatTypeModel? IProductSubscriptionTypeModel.SubscriptionTypeRepeatType { get => SubscriptionTypeRepeatType; set => SubscriptionTypeRepeatType = (SubscriptionTypeRepeatTypeModel?)value; }

        /// <inheritdoc cref="IProductSubscriptionTypeModel.Subscriptions"/>
        public List<SubscriptionModel>? Subscriptions { get; set; }

        /// <inheritdoc/>
        List<ISubscriptionModel>? IProductSubscriptionTypeModel.Subscriptions { get => Subscriptions?.ToList<ISubscriptionModel>(); set => Subscriptions = value?.Cast<SubscriptionModel>().ToList(); }
        #endregion
    }
}
