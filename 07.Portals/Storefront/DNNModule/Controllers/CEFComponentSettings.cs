// <copyright file="CEFComponentSettings.cs" company="clarity-ventures.com">
// Copyright (c) 2019-2023 clarity-ventures.com. All rights reserved.
// </copyright>
// <summary>Implements the cef component settings class</summary>
namespace Clarity.Ecommerce.DNN.Extensions
{
    using System.Collections.Generic;

    public class CEFComponentSettings
    {
        /// <summary>Initializes a new instance of the <see cref="CEFComponentSettings"/> class.</summary>
        public CEFComponentSettings() { }

        /// <summary>Initializes a new instance of the <see cref="CEFComponentSettings"/> class.</summary>
        /// <param name="moduleId">Identifier for the module.</param>
        public CEFComponentSettings(int moduleId)
        {
            ModuleId = moduleId;
        }

        /// <summary>Gets or sets the identifier of the module.</summary>
        /// <value>The identifier of the module.</value>
        public int ModuleId { get; set; }

        /// <summary>Gets or sets the component definition.</summary>
        /// <value>The component definition.</value>
        public string ComponentDefinition { get; set; }

        /// <summary>Gets or sets options for controlling the operation.</summary>
        /// <value>The parameters.</value>
        public Dictionary<string, CEFComponentParameter> Parameters { get; set; }
            = new Dictionary<string, CEFComponentParameter>();
    }
}
