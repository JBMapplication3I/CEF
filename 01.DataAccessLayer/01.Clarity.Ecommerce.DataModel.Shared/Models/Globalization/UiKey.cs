// <copyright file="UiKey.cs" company="clarity-ventures.com">
// Copyright (c) 2017-2023 clarity-ventures.com. All rights reserved.
// </copyright>
// <summary>Implements the key class</summary>
#nullable enable
namespace Clarity.Ecommerce.Interfaces.DataModel
{
    using System.Collections.Generic;
    using Ecommerce.DataModel;

    public interface IUiKey
        : IBase
    {
        #region UiKey properties
        /// <summary>Gets or sets the type.</summary>
        /// <value>The type.</value>
        string? Type { get; set; }
        #endregion

        #region Associated Objects
        /// <summary>Gets or sets the translations.</summary>
        /// <value>The user interface translations.</value>
        ICollection<UiTranslation>? UiTranslations { get; set; }
        #endregion
    }
}

namespace Clarity.Ecommerce.DataModel
{
    using System.Collections.Generic;
    using System.ComponentModel;
    using Interfaces.DataModel;
    using Newtonsoft.Json;

    [SqlSchema("Globalization", "UIKey")]
    public class UiKey : Base, IUiKey
    {
        private ICollection<UiTranslation>? uiTranslations;

        public UiKey()
        {
            uiTranslations = new HashSet<UiTranslation>();
        }

        #region UiKey properties
        /// <inheritdoc/>
        public string? Type { get; set; }
        #endregion

        #region Associated Objects
        /// <inheritdoc/>
        [DontMapInWithAssociateWorkflows, DefaultValue(null), JsonIgnore]
        public virtual ICollection<UiTranslation>? UiTranslations { get => uiTranslations; set => uiTranslations = value; }
        #endregion
    }
}
