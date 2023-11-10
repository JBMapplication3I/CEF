// <copyright file="GeneralAttributePredefinedOption.cs" company="clarity-ventures.com">
// Copyright (c) 2016-2023 clarity-ventures.com. All rights reserved.
// </copyright>
// <summary>Implements the general attribute predefined option class</summary>
#nullable enable
namespace Clarity.Ecommerce.Interfaces.DataModel
{
    using Ecommerce.DataModel;

    public interface IGeneralAttributePredefinedOption : IBase
    {
        #region GeneralAttributePredefinedOption Properties
        /// <summary>Gets or sets the value.</summary>
        /// <value>The value.</value>
        string Value { get; set; }

        /// <summary>Gets or sets the uof m.</summary>
        /// <value>The uof m.</value>
        string UofM { get; set; }

        /// <summary>Gets or sets the sort order.</summary>
        /// <value>The sort order.</value>
        int? SortOrder { get; set; }
        #endregion

        #region Related Objects
        /// <summary>Gets or sets the identifier of the attribute.</summary>
        /// <value>The identifier of the attribute.</value>
        int AttributeID { get; set; }

        /// <summary>Gets or sets the attribute.</summary>
        /// <value>The attribute.</value>
        GeneralAttribute? Attribute { get; set; }
        #endregion
    }
}

namespace Clarity.Ecommerce.DataModel
{
    using System.ComponentModel;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using Interfaces.DataModel;
    using Newtonsoft.Json;

    [SqlSchema("Attributes", "GeneralAttributePredefinedOption")]
    public class GeneralAttributePredefinedOption : Base, IGeneralAttributePredefinedOption
    {
        #region GeneralAttributePredefinedOption Properties
        /// <inheritdoc/>
        [Required, DefaultValue(null)]
        public string Value { get; set; } = null!;

        /// <inheritdoc/>
        [StringIsUnicode(false), StringLength(64), DefaultValue(null)]
        public string UofM { get; set; } = null!;

        /// <inheritdoc/>
        [DefaultValue(null)]
        public int? SortOrder { get; set; } = null;
        #endregion

        #region Related Objects
        /// <inheritdoc/>
        [InverseProperty(nameof(ID)), ForeignKey(nameof(Attribute)), DefaultValue(null)]
        public int AttributeID { get; set; }

        /// <inheritdoc/>
        [DefaultValue(null), JsonIgnore]
        public virtual GeneralAttribute? Attribute { get; set; }
        #endregion
    }
}
