// <copyright file="IHaveSalesEventsBaseModel.cs" company="clarity-ventures.com">
// Copyright (c) 2021-2023 clarity-ventures.com. All rights reserved.
// </copyright>
// <summary>Declares the IHaveSalesEventsBaseModel interface</summary>
namespace Clarity.Ecommerce.Interfaces.Models
{
    using System.Collections.Generic;

    /// <summary>Interface for have sales events base model.</summary>
    /// <typeparam name="TISalesEventModel">Type of the sales event model's interface.</typeparam>
    public interface IHaveSalesEventsBaseModel<TISalesEventModel>
        : IBaseModel
        where TISalesEventModel : ISalesEventBaseModel
    {
        #region Associated Objects
        /// <summary>Gets or sets the sales events.</summary>
        /// <value>The sales events.</value>
        List<TISalesEventModel>? SalesEvents { get; set; }
        #endregion
    }
}
