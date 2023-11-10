// <copyright file="IEndUserEventModel.cs" company="clarity-ventures.com">
// Copyright (c) 2017-2023 clarity-ventures.com. All rights reserved.
// </copyright>
// <summary>Declares the IEndUserEventModel interface</summary>
namespace Clarity.Ecommerce.Interfaces.Models
{
    /// <summary>Interface for end user event model.</summary>
    public interface IEndUserEventModel
    {
        /// <summary>Gets or sets the identifier of the visitor.</summary>
        /// <value>The identifier of the visitor.</value>
        int? VisitorID { get; set; }

        /// <summary>Gets or sets the visitor.</summary>
        /// <value>The visitor.</value>
        IVisitorModel? Visitor { get; set; }

        /// <summary>Gets or sets the visitor key.</summary>
        /// <value>The visitor key.</value>
        string? VisitorKey { get; set; }

        /// <summary>Gets or sets the name of the visitor.</summary>
        /// <value>The name of the visitor.</value>
        string? VisitorName { get; set; }

        /// <summary>Gets or sets the identifier of the visit.</summary>
        /// <value>The identifier of the visit.</value>
        int? VisitID { get; set; }

        /// <summary>Gets or sets the visit.</summary>
        /// <value>The visit.</value>
        IVisitModel? Visit { get; set; }

        /// <summary>Gets or sets the visit key.</summary>
        /// <value>The visit key.</value>
        string? VisitKey { get; set; }

        /// <summary>Gets or sets the name of the visit.</summary>
        /// <value>The name of the visit.</value>
        string? VisitName { get; set; }

        /// <summary>Gets or sets the identifier of the event.</summary>
        /// <value>The identifier of the event.</value>
        int? EventID { get; set; }

        /// <summary>Gets or sets the event.</summary>
        /// <value>The event.</value>
        IEventModel? Event { get; set; }

        /// <summary>Gets or sets the event key.</summary>
        /// <value>The event key.</value>
        string? EventKey { get; set; }

        /// <summary>Gets or sets the name of the event.</summary>
        /// <value>The name of the event.</value>
        string? EventName { get; set; }

        /// <summary>Gets or sets the identifier of the page view.</summary>
        /// <value>The identifier of the page view.</value>
        int? PageViewID { get; set; }

        /// <summary>Gets or sets the page view.</summary>
        /// <value>The page view.</value>
        IPageViewModel? PageView { get; set; }

        /// <summary>Gets or sets the page view key.</summary>
        /// <value>The page view key.</value>
        string? PageViewKey { get; set; }

        /// <summary>Gets or sets the name of the page view.</summary>
        /// <value>The name of the page view.</value>
        string? PageViewName { get; set; }

        /// <summary>Gets or sets the identifier of the page view event.</summary>
        /// <value>The identifier of the page view event.</value>
        int? PageViewEventID { get; set; }

        /// <summary>Gets or sets the page view event.</summary>
        /// <value>The page view event.</value>
        IPageViewEventModel? PageViewEvent { get; set; }

        /// <summary>Gets or sets the page view event key.</summary>
        /// <value>The page view event key.</value>
        string? PageViewEventKey { get; set; }
    }
}
