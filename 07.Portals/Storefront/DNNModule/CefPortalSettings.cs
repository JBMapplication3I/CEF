// <copyright file="CEFPortalSettings.cs" company="clarity-ventures.com">
// Copyright (c) 2019-2023 clarity-ventures.com. All rights reserved.
// </copyright>
// <summary>Implements the CEF portal settings class</summary>
namespace Clarity.Ecommerce.DNN.Extensions
{
    /// <summary>A CEF portal settings.</summary>
    public class CEFPortalSettings
    {
        /// <summary>Gets or sets the identifier of the portal.</summary>
        /// <value>The identifier of the portal.</value>
        public int PortalId { get; set; }

        /// <summary>Gets or sets a value indicating whether this CEFPortalSettings is enabled.</summary>
        /// <value>True if enabled, false if not.</value>
        public bool Enabled { get; set; }

        /// <summary>Gets or sets a value indicating whether as service.</summary>
        /// <value>True if as service, false if not.</value>
        public bool AsService { get; set; }

        /// <summary>Gets or sets the service endpoint.</summary>
        /// <value>The service endpoint.</value>
        public string ServiceEndpoint { get; set; }

        /// <summary>Gets the API endpoint.</summary>
        /// <value>The API endpoint.</value>
        public string APIEndpoint => $"{ServiceBase}/api";

        /// <summary>Gets the endpoint.</summary>
        /// <value>The user interface endpoint.</value>
        public string UIEndpoint => $"{ServiceBase}/ui";

        /// <summary>Gets or sets the admin tab.</summary>
        /// <value>The admin tab.</value>
        public CEFTabSettings AdminTab { get; set; }

        /// <summary>Gets the service base.</summary>
        /// <value>The service base.</value>
        private string ServiceBase => AsService ? ServiceEndpoint : "/DesktopModules/ClarityEcommerce";
    }
}
