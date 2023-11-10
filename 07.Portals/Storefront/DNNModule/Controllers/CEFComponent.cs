// <copyright file="CEFComponent.cs" company="clarity-ventures.com">
// Copyright (c) 2019-2023 clarity-ventures.com. All rights reserved.
// </copyright>
// <summary>Implements the cef component class</summary>
namespace Clarity.Ecommerce.DNN.Extensions
{
    using System.Collections.Generic;

    public class CEFComponent
    {
        /// <summary>Initializes a new instance of the <see cref="CEFComponent"/> class.</summary>
        /// <param name="definition">The definition.</param>
        /// <param name="parameters">Options for controlling the operation.</param>
        public CEFComponent(
            CEFComponentDefinition definition, Dictionary<string, CEFComponentParameter> parameters = null)
        {
            Definition = definition;
            if (parameters?.Count > 0)
            {
                Parameters = parameters;
            }
        }

        /// <summary>Gets or sets the definition.</summary>
        /// <value>The definition.</value>
        public CEFComponentDefinition Definition { get; set; }

        /// <summary>Gets or sets options for controlling the operation.</summary>
        /// <value>The parameters.</value>
        public Dictionary<string, CEFComponentParameter> Parameters { get; set; }
            = new Dictionary<string, CEFComponentParameter>();

        /// <summary>Gets a value indicating whether the initialized.</summary>
        /// <value>True if initialized, false if not.</value>
        public bool Initialized => Definition != null;

        /// <summary>Gets a parameter.</summary>
        /// <param name="key">The key.</param>
        /// <returns>The parameter.</returns>
        public CEFComponentParameter GetParameter(string key)
        {
            return Parameters.ContainsKey(key)
                ? Parameters[key]
                : null;
        }
    }
}
