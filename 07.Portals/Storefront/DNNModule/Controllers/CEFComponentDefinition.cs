// <copyright file="CEFComponentDefinition.cs" company="clarity-ventures.com">
// Copyright (c) 2019-2023 clarity-ventures.com. All rights reserved.
// </copyright>
// <summary>Implements the cef component definition class</summary>
namespace Clarity.Ecommerce.DNN.Extensions
{
    using System.Collections.Generic;
    using Newtonsoft.Json;
    using Newtonsoft.Json.Converters;

    public class CEFComponentDefinition
    {
        /// <summary>Gets or sets the name.</summary>
        /// <value>The name.</value>
        public string Name { get; set; }

        /// <summary>Gets or sets the name of the friendly.</summary>
        /// <value>The name of the friendly.</value>
        public string FriendlyName { get; set; }

        /// <summary>Gets or sets the description.</summary>
        /// <value>The description.</value>
        public string Description { get; set; }

        /// <summary>Gets or sets options for controlling the operation.</summary>
        /// <value>The parameters.</value>
        public Dictionary<string, CEFComponentParameter> Parameters { get; set; }
            = new Dictionary<string, CEFComponentParameter>();

        /// <summary>Gets or sets the type of the component.</summary>
        /// <value>The type of the component.</value>
        [JsonConverter(typeof(StringEnumConverter))]
        public CEFComponentType ComponentType { get; set; }

        /// <summary>Gets a value indicating whether this CEFComponentDefinition is user control.</summary>
        /// <value>True if this CEFComponentDefinition is user control, false if not.</value>
        public bool IsUserControl => ComponentType == CEFComponentType.UserControl;

        /// <summary>Gets the full pathname of the control file.</summary>
        /// <value>The full pathname of the control file.</value>
        public string ControlPath => IsUserControl
            ? ComponentController.ComponentPath(Name, ".ascx")
            : ComponentController.ComponentPath(Name, ".html");

        /// <summary>Gets the full pathname of the control virtual file.</summary>
        /// <value>The full pathname of the control virtual file.</value>
        public string ControlVirtualPath => IsUserControl
            ? ComponentController.ComponentVirtualPath(Name, ".ascx")
            : ComponentController.ComponentVirtualPath(Name, ".html");
    }
}
