// <copyright file="VisitModel.extended.cs" company="clarity-ventures.com">
// Copyright (c) 2017-2023 clarity-ventures.com. All rights reserved.
// </copyright>
// <summary>Implements the visit model class</summary>
namespace Clarity.Ecommerce.Models
{
    using System.Collections.Generic;
    using System.Linq;
    using Interfaces.Models;
    using Newtonsoft.Json;

    public partial class VisitModel
    {
        /// <inheritdoc cref="IVisitModel.Events"/>
        [JsonIgnore]
        public List<EventModel>? Events { get; set; }

        /// <inheritdoc/>
        List<IEventModel>? IVisitModel.Events { get => Events?.ToList<IEventModel>(); set => Events = value?.Cast<EventModel>().ToList(); }
    }
}
