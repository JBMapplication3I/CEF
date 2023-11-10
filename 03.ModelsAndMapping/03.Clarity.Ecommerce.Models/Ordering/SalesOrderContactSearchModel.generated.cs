// <autogenerated>
// <copyright file="SalesOrderContactSearchModel.generated.cs" company="clarity-ventures.com">
// Copyright (c) 2016-2023 clarity-ventures.com. All rights reserved.
// </copyright>
// <summary>Implements the SearchModel Classes generated to provide base setups.</summary>
// <remarks>This file was auto-generated by SearchModels.tt, changes to this
// file will be overwritten automatically when the T4 template is run again.</remarks>
// </autogenerated>
// ReSharper disable MissingXmlDoc, PartialTypeWithSinglePart, RedundantExtendsListEntry, RedundantUsingDirective, UnusedMember.Global
#nullable enable
namespace Clarity.Ecommerce.Models
{
    using System;
    using System.Collections.Generic;
    using Interfaces.Models;
    using ServiceStack;

    /// <summary>A data model for the Sales Order Contact search.</summary>
    public partial class SalesOrderContactSearchModel
        : AmARelationshipTableBaseSearchModel
        , ISalesOrderContactSearchModel
    {
        #region IAmARelationshipTableBaseSearchModel
        #endregion
        #region IHaveAContactBase
        /// <inheritdoc/>
        [ApiMember(Name = nameof(ContactID), DataType = "int?", ParameterType = "query", IsRequired = false,
            Description = "Contact ID for search")]
        public int? ContactID { get; set; }

        /// <inheritdoc/>
        [ApiMember(Name = nameof(ContactIDIncludeNull), DataType = "bool?", ParameterType = "query", IsRequired = false)]
        public bool? ContactIDIncludeNull { get; set; }

        /// <inheritdoc/>
        [ApiMember(Name = nameof(ContactKey), DataType = "string", ParameterType = "query", IsRequired = false,
            Description = "Contact Key for search")]
        public string? ContactKey { get; set; }

        /// <inheritdoc/>
        [ApiMember(Name = nameof(ContactKeyStrict), DataType = "bool?", ParameterType = "query", IsRequired = false)]
        public bool? ContactKeyStrict { get; set; }

        /// <inheritdoc/>
        [ApiMember(Name = nameof(ContactKeyIncludeNull), DataType = "bool?", ParameterType = "query", IsRequired = false)]
        public bool? ContactKeyIncludeNull { get; set; }

        /// <inheritdoc/>
        [ApiMember(Name = nameof(ContactFirstName), DataType = "string", ParameterType = "query", IsRequired = false,
            Description = "Contact First Name for search")]
        public string? ContactFirstName { get; set; }

        /// <inheritdoc/>
        [ApiMember(Name = nameof(ContactFirstNameStrict), DataType = "bool?", ParameterType = "query", IsRequired = false)]
        public bool? ContactFirstNameStrict { get; set; }

        /// <inheritdoc/>
        [ApiMember(Name = nameof(ContactFirstNameIncludeNull), DataType = "bool?", ParameterType = "query", IsRequired = false)]
        public bool? ContactFirstNameIncludeNull { get; set; }

        /// <inheritdoc/>
        [ApiMember(Name = nameof(ContactLastName), DataType = "string", ParameterType = "query", IsRequired = false,
            Description = "Contact Last Name for search")]
        public string? ContactLastName { get; set; }

        /// <inheritdoc/>
        [ApiMember(Name = nameof(ContactLastNameStrict), DataType = "bool?", ParameterType = "query", IsRequired = false)]
        public bool? ContactLastNameStrict { get; set; }

        /// <inheritdoc/>
        [ApiMember(Name = nameof(ContactLastNameIncludeNull), DataType = "bool?", ParameterType = "query", IsRequired = false)]
        public bool? ContactLastNameIncludeNull { get; set; }

        /// <inheritdoc/>
        [ApiMember(Name = nameof(ContactPhone), DataType = "string", ParameterType = "query", IsRequired = false,
            Description = "Contact Phone for search")]
        public string? ContactPhone { get; set; }

        /// <inheritdoc/>
        [ApiMember(Name = nameof(ContactPhoneStrict), DataType = "bool?", ParameterType = "query", IsRequired = false)]
        public bool? ContactPhoneStrict { get; set; }

        /// <inheritdoc/>
        [ApiMember(Name = nameof(ContactPhoneIncludeNull), DataType = "bool?", ParameterType = "query", IsRequired = false)]
        public bool? ContactPhoneIncludeNull { get; set; }

        /// <inheritdoc/>
        [ApiMember(Name = nameof(ContactFax), DataType = "string", ParameterType = "query", IsRequired = false,
            Description = "Contact Fax for search")]
        public string? ContactFax { get; set; }

        /// <inheritdoc/>
        [ApiMember(Name = nameof(ContactFaxStrict), DataType = "bool?", ParameterType = "query", IsRequired = false)]
        public bool? ContactFaxStrict { get; set; }

        /// <inheritdoc/>
        [ApiMember(Name = nameof(ContactFaxIncludeNull), DataType = "bool?", ParameterType = "query", IsRequired = false)]
        public bool? ContactFaxIncludeNull { get; set; }

        /// <inheritdoc/>
        [ApiMember(Name = nameof(ContactEmail), DataType = "string", ParameterType = "query", IsRequired = false,
            Description = "Contact Email for search")]
        public string? ContactEmail { get; set; }

        /// <inheritdoc/>
        [ApiMember(Name = nameof(ContactEmailStrict), DataType = "bool?", ParameterType = "query", IsRequired = false)]
        public bool? ContactEmailStrict { get; set; }

        /// <inheritdoc/>
        [ApiMember(Name = nameof(ContactEmailIncludeNull), DataType = "bool?", ParameterType = "query", IsRequired = false)]
        public bool? ContactEmailIncludeNull { get; set; }
        #endregion
    }
}
