// <copyright file="BridgePayPaymentsProviderExtensions.cs" company="clarity-ventures.com">
// Copyright (c) 2018-2023 clarity-ventures.com. All rights reserved.
// </copyright>
// <summary>Implements the bridge pay payments provider extensions class</summary>
namespace Clarity.Ecommerce.Providers.Payments.BridgePay
{
    using System.Xml;
    using Interfaces.Providers.Payments;
    using Utilities;

    /// <summary>A bridge pay payments provider extensions.</summary>
    public static class BridgePayPaymentsProviderExtensions
    {
        /// <summary>Converts an XML string to a payment response.</summary>
        /// <param name="xml">The XML string.</param>
        /// <returns>XML as an IPaymentResponse.</returns>
        public static IPaymentResponse ToPaymentResponse(string xml)
        {
            Contract.RequiresValidKey(xml);
            var retVal = new PaymentResponse();
            var xmlDoc = new XmlDocument();
            xmlDoc.LoadXml(xml);
            var nodeList = xmlDoc.SelectSingleNode("Auth")!.SelectSingleNode("responseMessage");
            var node = nodeList!.SelectSingleNode("AuthorizationCode");
            if (node != null)
            {
                retVal.AuthorizationCode = node.InnerText;
            }
            node = nodeList.SelectSingleNode("GatewayTransID");
            if (node != null)
            {
                retVal.TransactionID = node.InnerText;
            }
            node = nodeList.SelectSingleNode("OriginalAmount");
            if (node != null)
            {
                retVal.Amount = decimal.Parse(node.InnerText) / 100;
            }
            node = xmlDoc.SelectSingleNode("Auth")?.SelectSingleNode("ResponseCode");
            if (node != null)
            {
                retVal.ResponseCode = node.InnerText;
            }
            retVal.Approved = retVal.ResponseCode == "00000";
            return retVal;
        }

        /// <summary>Converts values to a payment response.</summary>
        /// <param name="message"> The message.</param>
        /// <param name="approved">True if approved.</param>
        /// <returns>XML as an IPaymentResponse.</returns>
        public static IPaymentResponse ToPaymentResponse(string message, bool approved)
        {
            return new PaymentResponse
            {
                Approved = approved,
                ResponseCode = message,
            };
        }
    }
}
