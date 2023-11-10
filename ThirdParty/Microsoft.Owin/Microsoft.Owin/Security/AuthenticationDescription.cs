// <copyright file="AuthenticationDescription.cs" company="clarity-ventures.com">
// Copyright (c) 2021-2022 clarity-ventures.com. All rights reserved.
// </copyright>
// <summary>Implements the authentication description class</summary>
namespace Microsoft.Owin.Security
{
    using System;
    using System.Collections.Generic;
    using System.Globalization;

    /// <summary>Contains information describing an authentication provider.</summary>
    public class AuthenticationDescription
    {
        /// <summary>The authentication type property key.</summary>
        private const string AuthenticationTypePropertyKey = "AuthenticationType";

        /// <summary>The caption property key.</summary>
        private const string CaptionPropertyKey = "Caption";

        /// <summary>Initializes a new instance of the <see cref="AuthenticationDescription" />
        /// class.</summary>
        public AuthenticationDescription()
        {
            Properties = new Dictionary<string, object>(StringComparer.Ordinal);
        }

        /// <summary>Initializes a new instance of the <see cref="AuthenticationDescription" />
        /// class.</summary>
        /// <param name="properties">.</param>
        public AuthenticationDescription(IDictionary<string, object> properties)
        {
            Properties = properties ?? throw new ArgumentNullException(nameof(properties));
        }

        /// <summary>Gets or sets the name used to reference the authentication middleware instance.</summary>
        /// <value>The type of the authentication.</value>
        public string AuthenticationType
        {
            get => GetString("AuthenticationType");
            set => Properties["AuthenticationType"] = value;
        }

        /// <summary>Gets or sets the display name for the authentication provider.</summary>
        /// <value>The caption.</value>
        public string Caption
        {
            get => GetString("Caption");
            set => Properties["Caption"] = value;
        }

        /// <summary>Contains metadata about the authentication provider.</summary>
        /// <value>The properties.</value>
        public IDictionary<string, object> Properties
        {
            get;
        }

        /// <summary>Gets a string.</summary>
        /// <param name="name">The name.</param>
        /// <returns>The string.</returns>
        private string GetString(string name)
        {
            if (!Properties.TryGetValue(name, out var obj))
            {
                return null;
            }
            return Convert.ToString(obj, CultureInfo.InvariantCulture);
        }
    }
}
