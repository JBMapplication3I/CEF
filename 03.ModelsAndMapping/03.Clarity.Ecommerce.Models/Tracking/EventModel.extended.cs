// <copyright file="EventModel.extended.cs" company="clarity-ventures.com">
// Copyright (c) 2016-2023 clarity-ventures.com. All rights reserved.
// </copyright>
// <summary>Implements the event model class</summary>
namespace Clarity.Ecommerce.Models
{
    using System.Collections.Generic;
    using System.Linq;
    using Interfaces.Models;
    using Newtonsoft.Json;

    /// <summary>A data Model for the event.</summary>
    /// <seealso cref="NameableBaseModel"/>
    /// <seealso cref="IEventModel"/>
    public partial class EventModel
    {
        #region Related Objects
        /// <inheritdoc/>
        public int? VisitID { get; set; }

        /// <inheritdoc/>
        public string? VisitKey { get; set; }

        /// <inheritdoc/>
        public string? VisitName { get; set; }

        /// <inheritdoc cref="IEventModel.Visit"/>
        public VisitModel? Visit { get; set; }

        /// <inheritdoc/>
        IVisitModel? IEventModel.Visit { get => Visit; set => Visit = (VisitModel?)value; }
        #endregion

        #region Associated Objects
        /// <inheritdoc cref="IEventModel.PageViewEvents"/>
        [JsonIgnore]
        public List<PageViewEventModel>? PageViewEvents { get; set; }

        /// <inheritdoc/>
        List<IPageViewEventModel>? IEventModel.PageViewEvents { get => PageViewEvents?.ToList<IPageViewEventModel>(); set => PageViewEvents = value?.Cast<PageViewEventModel>().ToList(); }
        #endregion
    }
}
