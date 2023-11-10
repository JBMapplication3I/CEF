// <copyright file="IVisitModel.extended.cs" company="clarity-ventures.com">
// Copyright (c) 2017-2023 clarity-ventures.com. All rights reserved.
// </copyright>
// <summary>Declares the IVisitModel interface</summary>
namespace Clarity.Ecommerce.Interfaces.Models
{
    using System.Collections.Generic;

    public partial interface IVisitModel
    {
        #region Related Objects
        /// <summary>Gets or sets the events.</summary>
        /// <value>The events.</value>
        List<IEventModel>? Events { get; set; }
        #endregion
    }
}
