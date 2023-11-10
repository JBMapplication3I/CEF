// <copyright file="AmATrackedEventModel.cs" company="clarity-ventures.com">
// Copyright (c) 2019-2023 clarity-ventures.com. All rights reserved.
// </copyright>
// <summary>Implements the am a tracked event base model class</summary>
namespace Clarity.Ecommerce.Models
{
    using System;
    using Interfaces.Models;
    using ServiceStack;

    /// <summary>A data Model for the am a tracked event.</summary>
    /// <seealso cref="AmALitelyTrackedEventModel"/>
    /// <seealso cref="IAmATrackedEventBaseModel{IStatusModel}"/>
    public abstract class AmATrackedEventModel : AmALitelyTrackedEventModel, IAmATrackedEventBaseModel<IStatusModel>
    {
        #region IHaveANullableContact
        /// <inheritdoc/>
        public int? ContactID { get; set; }

        /// <inheritdoc cref="IHaveANullableContactBaseModel.Contact"/>
        public ContactModel? Contact { get; set; }

        /// <inheritdoc/>
        IContactModel? IHaveANullableContactBaseModel.Contact { get => Contact; set => Contact = (ContactModel?)value; }

        /// <inheritdoc/>
        public string? ContactKey { get; set; }

        /// <inheritdoc/>
        public string? ContactPhone { get; set; }

        /// <inheritdoc/>
        public string? ContactFax { get; set; }

        /// <inheritdoc/>
        public string? ContactEmail { get; set; }

        /// <inheritdoc/>
        public string? ContactFirstName { get; set; }

        /// <inheritdoc/>
        public string? ContactLastName { get; set; }
        #endregion

        #region IHaveAStatusBaseModel<IStatusModel>
        /// <inheritdoc/>
        [ApiMember(Name = nameof(StatusID), DataType = "int", ParameterType = "body", IsRequired = true,
            Description = "Identifier for the Status of this object, required if no StatusModel present")]
        public int StatusID { get; set; }

        /// <inheritdoc cref="IHaveAStatusBaseModel{IStatusModel}.Status"/>
        [ApiMember(Name = nameof(Status), DataType = "StatusModel", ParameterType = "body", IsRequired = true,
            Description = "Model for Status of this object, required if no StatusID present")]
        public StatusModel? Status { get; set; }

        /// <inheritdoc/>
        IStatusModel? IHaveAStatusBaseModel<IStatusModel>.Status { get => Status; set => Status = (StatusModel?)value; }

        /// <inheritdoc/>
        [ApiMember(Name = nameof(StatusKey), DataType = "string", ParameterType = "body", IsRequired = true,
            Description = "Key for the Status of this object, read-only")]
        public string? StatusKey { get; set; }

        /// <inheritdoc/>
        [ApiMember(Name = nameof(StatusName), DataType = "string", ParameterType = "body", IsRequired = true,
            Description = "Name for the Status of this object, read-only")]
        public string? StatusName { get; set; }

        /// <inheritdoc/>
        [ApiMember(Name = nameof(StatusDisplayName), DataType = "string", ParameterType = "body", IsRequired = true,
            Description = "Display Name for the Status of this object, read-only")]
        public string? StatusDisplayName { get; set; }

        /// <inheritdoc/>
        [ApiMember(Name = nameof(StatusTranslationKey), DataType = "string", ParameterType = "body", IsRequired = true,
            Description = "Translation Key for the Status of this object, read-only")]
        public string? StatusTranslationKey { get; set; }

        /// <inheritdoc/>
        [ApiMember(Name = nameof(StatusSortOrder), DataType = "int?", ParameterType = "body", IsRequired = true,
            Description = "Sort Order for the Status of this object, read-only")]
        public int? StatusSortOrder { get; set; }
        #endregion

        /// <inheritdoc/>
        public bool? DidBounce { get; set; }

        /// <inheritdoc/>
        public string? OperatingSystem { get; set; }

        /// <inheritdoc/>
        public string? Browser { get; set; }

        /// <inheritdoc/>
        public string? Language { get; set; }

        /// <inheritdoc/>
        public bool? ContainsSocialProfile { get; set; }

        /// <inheritdoc/>
        public int? Delta { get; set; }

        /// <inheritdoc/>
        public int? Duration { get; set; }

        /// <inheritdoc/>
        public DateTime? StartedOn { get; set; }

        /// <inheritdoc/>
        public DateTime? EndedOn { get; set; }

        /// <inheritdoc/>
        public string? Time { get; set; }

        /// <inheritdoc/>
        public string? EntryPage { get; set; }

        /// <inheritdoc/>
        public string? ExitPage { get; set; }

        /// <inheritdoc/>
        public bool? IsFirstTrigger { get; set; }

        /// <inheritdoc/>
        public string? Flash { get; set; }

        /// <inheritdoc/>
        public string? Keywords { get; set; }

        /// <inheritdoc/>
        public string? PartitionKey { get; set; }

        /// <inheritdoc/>
        public string? Referrer { get; set; }

        /// <inheritdoc/>
        public string? ReferringHost { get; set; }

        /// <inheritdoc/>
        public string? RowKey { get; set; }

        /// <inheritdoc/>
        public int? Source { get; set; }

        /// <inheritdoc/>
        public int? TotalTriggers { get; set; }

        // Related Objects

        /// <inheritdoc/>
        public int? CampaignID { get; set; }

        /// <inheritdoc cref="IAmATrackedEventBaseModel.Campaign"/>
        public CampaignModel? Campaign { get; set; }

        /// <inheritdoc/>
        ICampaignModel? IAmATrackedEventBaseModel.Campaign { get => Campaign; set => Campaign = (CampaignModel?)value; }

        /// <inheritdoc/>
        public string? CampaignKey { get; set; }

        /// <inheritdoc/>
        public string? CampaignName { get; set; }

        /// <inheritdoc/>
        public int? SiteDomainID { get; set; }

        /// <inheritdoc cref="IAmATrackedEventBaseModel.SiteDomain"/>
        public SiteDomainModel? SiteDomain { get; set; }

        /// <inheritdoc/>
        ISiteDomainModel? IAmATrackedEventBaseModel.SiteDomain { get => SiteDomain; set => SiteDomain = (SiteDomainModel?)value; }

        /// <inheritdoc/>
        public string? SiteDomainKey { get; set; }

        /// <inheritdoc/>
        public string? SiteDomainName { get; set; }

        /// <inheritdoc/>
        public int? VisitorID { get; set; }

        /// <inheritdoc cref="IAmATrackedEventBaseModel.Visitor"/>
        public VisitorModel? Visitor { get; set; }

        /// <inheritdoc/>
        IVisitorModel? IAmATrackedEventBaseModel.Visitor { get => Visitor; set => Visitor = (VisitorModel?)value; }

        /// <inheritdoc/>
        public string? VisitorKey { get; set; }

        /// <inheritdoc/>
        public string? VisitorName { get; set; }
    }
}
