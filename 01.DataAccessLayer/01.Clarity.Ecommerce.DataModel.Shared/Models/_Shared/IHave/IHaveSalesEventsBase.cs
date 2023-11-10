// <copyright file="IHaveSalesEventsBase.cs" company="clarity-ventures.com">
// Copyright (c) 2020-2023 clarity-ventures.com. All rights reserved.
// </copyright>
// <summary>Declares the IHaveSalesEventsBase interface</summary>
#nullable enable
namespace Clarity.Ecommerce.Interfaces.DataModel
{
    using System.Collections.Generic;

    /// <summary>Interface for have sales events base.</summary>
    /// <typeparam name="TMaster">        Type of the master entity.</typeparam>
    /// <typeparam name="TSalesEvent">    Type of the sales event entity.</typeparam>
    /// <typeparam name="TSalesEventType">Type of the sales event type entity.</typeparam>
    public interface IHaveSalesEventsBase<TMaster, TSalesEvent, TSalesEventType>
        : IBase
        where TMaster : IHaveSalesEventsBase<TMaster, TSalesEvent, TSalesEventType>
        where TSalesEvent : ISalesEventBase<TMaster, TSalesEventType>
        where TSalesEventType : ITypableBase
    {
        /// <summary>Gets or sets the sales events.</summary>
        /// <value>The sales events.</value>
        ICollection<TSalesEvent>? SalesEvents { get; set; }
    }
}
