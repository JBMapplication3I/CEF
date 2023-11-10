// <copyright file="JBMCustomShippingMethod.cs" company="clarity-ventures.com">
// Copyright (c) 2023 clarity-ventures.com. All rights reserved.
// </copyright>
// <summary>Implements the JBMCustomShippingMethod class</summary>
namespace Clarity.Clients.JBM.JBMModels
{
    public class JBMCustomShippingMethod
    {
        public string? ShippingCarrier { get; set; }

        public long? ShippingCarrierId { get; set; }

        public string? ShippingServiceLevelCode { get; set; }

        public string? ShippingServiceLevel { get; set; }

        public string? ShippingModeCode { get; set; }

        public string? ShippingMode { get; set; }

        public string? ShippingMethodName { get; set; }
    }
}
