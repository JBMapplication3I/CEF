// <copyright file="EndUserEventModel.cs" company="clarity-ventures.com">
// Copyright (c) 2017-2023 clarity-ventures.com. All rights reserved.
// </copyright>
// <summary>Implements the end user event model class</summary>
namespace Clarity.Ecommerce.Models
{
    using Interfaces.Models;

    /// <summary>A data Model for the end user event.</summary>
    /// <seealso cref="T:Clarity.Ecommerce.Interfaces.Models.IEndUserEventModel"/>
    public class EndUserEventModel : IEndUserEventModel
    {
        /// <inheritdoc/>
        public int? VisitorID { get; set; }

        /// <inheritdoc/>
        public string? VisitorKey { get; set; }

        /// <inheritdoc/>
        public string? VisitorName { get; set; }

        /// <inheritdoc cref="IEndUserEventModel.Visitor"/>
        public VisitorModel? Visitor { get; set; }

        /// <inheritdoc/>
        IVisitorModel? IEndUserEventModel.Visitor { get => Visitor; set => Visitor = (VisitorModel?)value; }

        /// <inheritdoc/>
        public int? VisitID { get; set; }

        /// <inheritdoc/>
        public string? VisitKey { get; set; }

        /// <inheritdoc/>
        public string? VisitName { get; set; }

        /// <inheritdoc cref="IEndUserEventModel.Visit"/>
        public VisitModel? Visit { get; set; }

        /// <inheritdoc/>
        IVisitModel? IEndUserEventModel.Visit { get => Visit; set => Visit = (VisitModel?)value; }

        /// <inheritdoc/>
        public int? EventID { get; set; }

        /// <inheritdoc/>
        public string? EventKey { get; set; }

        /// <inheritdoc/>
        public string? EventName { get; set; }

        /// <inheritdoc cref="IEndUserEventModel.Event"/>
        public EventModel? Event { get; set; }

        /// <inheritdoc/>
        IEventModel? IEndUserEventModel.Event { get => Event; set => Event = (EventModel?)value; }

        /// <inheritdoc/>
        public int? PageViewID { get; set; }

        /// <inheritdoc/>
        public string? PageViewKey { get; set; }

        /// <inheritdoc/>
        public string? PageViewName { get; set; }

        /// <inheritdoc cref="IEndUserEventModel.PageView"/>
        public PageViewModel? PageView { get; set; }

        /// <inheritdoc/>
        IPageViewModel? IEndUserEventModel.PageView { get => PageView; set => PageView = (PageViewModel?)value; }

        /// <inheritdoc/>
        public int? PageViewEventID { get; set; }

        /// <inheritdoc/>
        public string? PageViewEventKey { get; set; }

        /// <inheritdoc cref="IEndUserEventModel.PageViewEvent"/>
        public PageViewEventModel? PageViewEvent { get; set; }

        /// <inheritdoc/>
        IPageViewEventModel? IEndUserEventModel.PageViewEvent { get => PageViewEvent; set => PageViewEvent = (PageViewEventModel?)value; }
    }
}
