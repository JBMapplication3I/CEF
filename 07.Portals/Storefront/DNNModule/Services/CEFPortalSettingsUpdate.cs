// <copyright file="CEFPortalSettingsUpdate.cs" company="clarity-ventures.com">
// Copyright (c) 2019-2023 clarity-ventures.com. All rights reserved.
// </copyright>
// <summary>Implements the cef portal settings update class</summary>
namespace Clarity.Ecommerce.DNN.Extensions.Services
{
    public class CEFPortalSettingsUpdate
    {
        /// <summary>Gets or sets the identifier of the portal.</summary>
        /// <value>The identifier of the portal.</value>
        public int PortalId { get; set; }

        /// <summary>Gets or sets a value indicating whether as service.</summary>
        /// <value>True if as service, false if not.</value>
        public bool AsService { get; set; }

        /// <summary>Gets or sets the service endpoint.</summary>
        /// <value>The service endpoint.</value>
        public string ServiceEndpoint { get; set; }
    }
}
