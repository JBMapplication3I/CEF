// <copyright file="GeneralAttributeSearchModel.extended.cs" company="clarity-ventures.com">
// Copyright (c) 2017-2023 clarity-ventures.com. All rights reserved.
// </copyright>
// <summary>Implements the general attribute search model class</summary>
namespace Clarity.Ecommerce.Models
{
    using ServiceStack;

    /// <content>A data Model for the general attribute search.</content>
    public partial class GeneralAttributeSearchModel
    {
        /// <summary>Gets or sets the name of the include general with type.</summary>
        /// <value>The name of the include general with type.</value>
        /// <seealso cref="Interfaces.Models.IGeneralAttributeSearchModel.IncludeGeneralWithTypeName"/>
        [ApiMember(Name = nameof(IncludeGeneralWithTypeName), DataType = "bool?", ParameterType = "query", IsRequired = false)]
        public bool? IncludeGeneralWithTypeName { get; set; }

        /// <summary>Gets or sets the group.</summary>
        /// <value>The group.</value>
        /// <seealso cref="Interfaces.Models.IGeneralAttributeSearchModel.Group"/>
        public string? Group { get; set; }

        /// <summary>Gets or sets the attribute group key.</summary>
        /// <value>The attribute group key.</value>
        /// <seealso cref="Interfaces.Models.IGeneralAttributeSearchModel.AttributeGroupKey"/>
        public string? AttributeGroupKey { get; set; }

        /// <summary>Gets or sets the name of the attribute group.</summary>
        /// <value>The name of the attribute group.</value>
        /// <seealso cref="Interfaces.Models.IGeneralAttributeSearchModel.AttributeGroupName"/>
        public string? AttributeGroupName { get; set; }

        /// <summary>Gets or sets the name of the attribute group display.</summary>
        /// <value>The name of the attribute group display.</value>
        /// <seealso cref="Interfaces.Models.IGeneralAttributeSearchModel.AttributeGroupDisplayName"/>
        public string? AttributeGroupDisplayName { get; set; }

        /// <summary>Gets or sets the attribute tab key.</summary>
        /// <value>The attribute tab key.</value>
        /// <seealso cref="Interfaces.Models.IGeneralAttributeSearchModel.AttributeTabKey"/>
        public string? AttributeTabKey { get; set; }

        /// <summary>Gets or sets the name of the attribute tab.</summary>
        /// <value>The name of the attribute tab.</value>
        /// <seealso cref="Interfaces.Models.IGeneralAttributeSearchModel.AttributeTabName"/>
        public string? AttributeTabName { get; set; }

        /// <summary>Gets or sets the name of the attribute tab display.</summary>
        /// <value>The name of the attribute tab display.</value>
        /// <seealso cref="Interfaces.Models.IGeneralAttributeSearchModel.AttributeTabDisplayName"/>
        public string? AttributeTabDisplayName { get; set; }
    }
}
