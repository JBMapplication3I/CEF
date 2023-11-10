// <copyright file="DynamicsAXPackageService.cs" company="clarity-ventures.com">
// Copyright (c) 2019-2023 clarity-ventures.com. All rights reserved.
// </copyright>
// <summary>Implements the Dynamics AX packaging service class</summary>
namespace Clarity.Ecommerce.Providers.Packaging.DynamicsAx
{
    using System.Net;
    using Models;
    using Newtonsoft.Json;
    using Newtonsoft.Json.Linq;
    using Utilities;

    /// <summary>An Dynamics AX package service.</summary>
    public static class DynamicsAXPackageService
    {
        /// <summary>Gets or sets URL of the service.</summary>
        /// <value>The service URL.</value>
        private static string ServiceUrl { get; set; } = null!;

        /// <summary>Sets service URL.</summary>
        /// <param name="serviceUrl">URL of the service.</param>
        public static void SetServiceUrl(string? serviceUrl)
        {
            ServiceUrl = Contract.RequiresNotNull(serviceUrl).TrimEnd('/');
        }

        /// <summary>This calls the service to perform the package calculation, and returns the package result.</summary>
        /// <param name="request">           The request.</param>
        /// <returns>The packages.</returns>
        public static FreightDimResponseDataContract GetPackage(FreightDimRequestDataContract[] request)
        {
            Contract.RequiresValidKey(ServiceUrl);
            // Permit invalid certificates
            ServicePointManager.ServerCertificateValidationCallback += (_, _, _, _) => true;
            using var client = new WebClient();
            var encodedJson = JsonConvert.SerializeObject(Contract.RequiresNotNull(request));
            client.Headers.Add("Content-Type:application/json");
            var contract = new FreightDimResponseDataContract
            {
                Packages = new(),
            };
            var jsonResponse = client.UploadString(ServiceUrl, encodedJson);
            var response = JArray.Parse(jsonResponse);
            foreach (var o in response.Children<JObject>())
            {
                foreach (var p in o.Properties())
                {
                    switch (p.Name)
                    {
                        case "ErrorMessage":
                        {
                            contract.ErrorMessage = (string?)p;
                            break;
                        }
                        case "PackageWeightDim":
                        {
                            // ReSharper disable once PossibleInvalidCastExceptionInForeachLoop
                            foreach (JObject jsonPackage in JArray.Parse(p.Value.ToString()))
                            {
                                contract.Packages.Add(MapFromJObject(jsonPackage));
                            }
                            break;
                        }
                        default:
                        {
                            continue;
                        }
                    }
                }
            }
            return contract;
        }

        /// <summary>The map from product.</summary>
        /// <param name="package">The package.</param>
        /// <returns>A ShipCostCalculatorDataContract.</returns>
        private static ShipCostCalculatorDataContract MapFromJObject(JObject package)
        {
            return new()
            {
                ErrorMessage = (string?)package["ErrorMessage"],
                PackageDepth = (decimal?)package["PackageDepth"],
                PackageHazardClass = (string?)package["PackageHazardClass"],
                PackageHeight = (decimal?)package["PackageHeight"],
                PackageItemId = (string?)package["PackageItemId"],
                PackageWeight = (decimal?)package["PackageWeight"],
                PackageWidth = (decimal?)package["PackageWidth"],
                Success = (bool?)package["Success"],
            };
        }
    }
}
