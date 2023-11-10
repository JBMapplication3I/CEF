// <copyright file="IAmATrackedEventBaseModel.cs" company="clarity-ventures.com">
// Copyright (c) 2017-2023 clarity-ventures.com. All rights reserved.
// </copyright>
// <summary>Declares the IAmATrackedEventBaseModel interface</summary>
namespace Clarity.Ecommerce.Interfaces.Models
{
    using System;

    /// <summary>Interface for am a tracked event base model.</summary>
    /// <typeparam name="TStatusModel">Type of the status model.</typeparam>
    public interface IAmATrackedEventBaseModel<TStatusModel>
        : IHaveAStatusBaseModel<TStatusModel>, IAmATrackedEventBaseModel
        where TStatusModel : IStatusableBaseModel
    {
    }

    /// <summary>Interface for am a tracked event base model.</summary>
    public interface IAmATrackedEventBaseModel
        : IAmALitelyTrackedEventBaseModel, IHaveANullableContactBaseModel
    {
        /// <summary>Gets or sets the did bounce.</summary>
        /// <value>The did bounce.</value>
        bool? DidBounce { get; set; }

        /// <summary>Gets or sets the operating system.</summary>
        /// <value>The operating system.</value>
        string? OperatingSystem { get; set; }

        /// <summary>Gets or sets the browser.</summary>
        /// <value>The browser.</value>
        string? Browser { get; set; }

        /// <summary>Gets or sets the language.</summary>
        /// <value>The language.</value>
        string? Language { get; set; }

        /// <summary>Gets or sets the contains social profile.</summary>
        /// <value>The contains social profile.</value>
        bool? ContainsSocialProfile { get; set; }

        /// <summary>Gets or sets the delta.</summary>
        /// <value>The delta.</value>
        int? Delta { get; set; }

        /// <summary>Gets or sets the duration.</summary>
        /// <value>The duration.</value>
        int? Duration { get; set; }

        /// <summary>Gets or sets the Date/Time of the started on.</summary>
        /// <value>The started on.</value>
        DateTime? StartedOn { get; set; }

        /// <summary>Gets or sets the Date/Time of the ended on.</summary>
        /// <value>The ended on.</value>
        DateTime? EndedOn { get; set; }

        /// <summary>Gets or sets the time.</summary>
        /// <value>The time.</value>
        string? Time { get; set; }

        /// <summary>Gets or sets the entry page.</summary>
        /// <value>The entry page.</value>
        string? EntryPage { get; set; }

        /// <summary>Gets or sets the exit page.</summary>
        /// <value>The exit page.</value>
        string? ExitPage { get; set; }

        /// <summary>Gets or sets the is first trigger.</summary>
        /// <value>The is first trigger.</value>
        bool? IsFirstTrigger { get; set; }

        /// <summary>Gets or sets the flash.</summary>
        /// <value>The flash.</value>
        string? Flash { get; set; }

        /// <summary>Gets or sets the keywords.</summary>
        /// <value>The keywords.</value>
        string? Keywords { get; set; }

        /// <summary>Gets or sets the partition key.</summary>
        /// <value>The partition key.</value>
        string? PartitionKey { get; set; }

        /// <summary>Gets or sets the referrer.</summary>
        /// <value>The referrer.</value>
        string? Referrer { get; set; }

        /// <summary>Gets or sets the referring host.</summary>
        /// <value>The referring host.</value>
        string? ReferringHost { get; set; }

        /// <summary>Gets or sets the row key.</summary>
        /// <value>The row key.</value>
        string? RowKey { get; set; }

        /// <summary>Gets or sets the source for the.</summary>
        /// <value>The source.</value>
        int? Source { get; set; }

        /// <summary>Gets or sets the total number of triggers.</summary>
        /// <value>The total number of triggers.</value>
        int? TotalTriggers { get; set; }

        #region Related Objects
        /// <summary>Gets or sets the identifier of the campaign.</summary>
        /// <value>The identifier of the campaign.</value>
        int? CampaignID { get; set; }

        /// <summary>Gets or sets the campaign key.</summary>
        /// <value>The campaign key.</value>
        string? CampaignKey { get; set; }

        /// <summary>Gets or sets the name of the campaign.</summary>
        /// <value>The name of the campaign.</value>
        string? CampaignName { get; set; }

        /// <summary>Gets or sets the campaign.</summary>
        /// <value>The campaign.</value>
        ICampaignModel? Campaign { get; set; }

        /// <summary>Gets or sets the identifier of the site domain.</summary>
        /// <value>The identifier of the site domain.</value>
        int? SiteDomainID { get; set; }

        /// <summary>Gets or sets the site domain key.</summary>
        /// <value>The site domain key.</value>
        string? SiteDomainKey { get; set; }

        /// <summary>Gets or sets the name of the site domain.</summary>
        /// <value>The name of the site domain.</value>
        string? SiteDomainName { get; set; }

        /// <summary>Gets or sets the site domain.</summary>
        /// <value>The site domain.</value>
        ISiteDomainModel? SiteDomain { get; set; }

        /// <summary>Gets or sets the identifier of the visitor.</summary>
        /// <value>The identifier of the visitor.</value>
        int? VisitorID { get; set; }

        /// <summary>Gets or sets the visitor key.</summary>
        /// <value>The visitor key.</value>
        string? VisitorKey { get; set; }

        /// <summary>Gets or sets the name of the visitor.</summary>
        /// <value>The name of the visitor.</value>
        string? VisitorName { get; set; }

        /// <summary>Gets or sets the visitor.</summary>
        /// <value>The visitor.</value>
        IVisitorModel? Visitor { get; set; }
        #endregion
    }
}
