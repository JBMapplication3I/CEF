// <copyright file="UPSQuantumRepository.cs" company="clarity-ventures.com">
// Copyright (c) 2016-2023 clarity-ventures.com. All rights reserved.
// </copyright>
// <summary>Implements the ups quantum repository class</summary>
namespace Clarity.Ecommerce.Workflow
{
    using System;
    using System.Collections.Generic;
    using System.IO;
    using System.Linq;
    using System.Net;
    using System.Text;
    using System.Xml;
    using JSConfigs;
    using Providers.Shipping.UPS;

    /// <summary>The ups quantum repository.</summary>
    public class UPSQuantumRepository
    {
        private const string QuantumEndPointURL = "https://onlinetools.ups.com/ups.app/xml/QVEvents";

        /// <summary>Values that represent last error code types.</summary>
        public enum LastErrorCodeType
        {
            /// <summary>An enum constant representing the none option.</summary>
            None = 0,

            /// <summary>An enum constant representing the no new records option.</summary>
            NoNewRecords = 1,

            /// <summary>An enum constant representing the no data option.</summary>
            NoData = 2,

            /// <summary>An enum constant representing the bad user password option.</summary>
            BadUserPassword = 3,
        }

        /// <summary>Gets or sets information describing the response.</summary>
        /// <value>Information describing the response.</value>
        // ReSharper disable once StyleCop.SA1401
        public static string ResponseData { get; set; } = "<QuantumViewResponse><Response><TransactionReference></TransactionReference><ResponseStatusCode>1</ResponseStatusCode><ResponseStatusDescription>Success</ResponseStatusDescription></Response><QuantumViewEvents><SubscriberID>XXXX</SubscriberID><SubscriptionEvents><Name>TestBillingSub1</Name><Number>49C2FA297ACFEB4E</Number><SubscriptionStatus><Code>A</Code><Description>Active</Description></SubscriptionStatus><SubscriptionFile><FileName>130107_215137001</FileName><StatusType><Code>U</Code><Description>Unread</Description></StatusType><Origin><PackageReferenceNumber><Value>813635-1</Value></PackageReferenceNumber><ShipmentReferenceNumber><Value>813635-1</Value></ShipmentReferenceNumber><ShipperNumber>X13412</ShipperNumber><TrackingNumber>1ZX134120355884866</TrackingNumber><Date>20130107</Date><Time>204921</Time><ActivityLocation><AddressArtifactFormat><PoliticalDivision2>DOVER-DOVER</PoliticalDivision2><PoliticalDivision1>NH</PoliticalDivision1><CountryCode>US</CountryCode></AddressArtifactFormat></ActivityLocation><BillToAccount><Option>03</Option><Number>03A4F7</Number></BillToAccount></Origin><Origin><PackageReferenceNumber><Value>813645-1</Value></PackageReferenceNumber><ShipmentReferenceNumber><Value>813645-1</Value></ShipmentReferenceNumber><ShipperNumber>X13412</ShipperNumber><TrackingNumber>1ZX134121254304637</TrackingNumber><Date>20130107</Date><Time>204621</Time><ActivityLocation><AddressArtifactFormat><PoliticalDivision2>DOVER-DOVER</PoliticalDivision2><PoliticalDivision1>NH</PoliticalDivision1><CountryCode>US</CountryCode></AddressArtifactFormat></ActivityLocation><BillToAccount><Option>03</Option><Number>03A4F7</Number></BillToAccount></Origin><Origin><PackageReferenceNumber><Value>813643-1</Value></PackageReferenceNumber><ShipmentReferenceNumber><Value>813643-1</Value></ShipmentReferenceNumber><ShipperNumber>X13412</ShipperNumber><TrackingNumber>1ZX134121254413724</TrackingNumber><Date>20130107</Date><Time>204234</Time><ActivityLocation><AddressArtifactFormat><PoliticalDivision2>DOVER-DOVER</PoliticalDivision2><PoliticalDivision1>NH</PoliticalDivision1><CountryCode>US</CountryCode></AddressArtifactFormat></ActivityLocation><BillToAccount><Option>03</Option><Number>03A4F7</Number></BillToAccount></Origin><Origin><PackageReferenceNumber><Value>813642-1</Value></PackageReferenceNumber><ShipmentReferenceNumber><Value>813642-1</Value></ShipmentReferenceNumber><ShipperNumber>X13412</ShipperNumber><TrackingNumber>1ZX134121255326853</TrackingNumber><Date>20130107</Date><Time>204046</Time><ActivityLocation><AddressArtifactFormat><PoliticalDivision2>DOVER-DOVER</PoliticalDivision2><PoliticalDivision1>NH</PoliticalDivision1><CountryCode>US</CountryCode></AddressArtifactFormat></ActivityLocation><BillToAccount><Option>03</Option><Number>03A4F7</Number></BillToAccount></Origin></SubscriptionFile></SubscriptionEvents></QuantumViewEvents></QuantumViewResponse>";

        /// <summary>Gets or sets the last error code.</summary>
        /// <value>The last error code.</value>
        // ReSharper disable once StyleCop.SA1401
        public static LastErrorCodeType LastErrorCode { get; set; } = LastErrorCodeType.None;

        /// <summary>Gets origin list.</summary>
        /// <returns>The origin list.</returns>
        public static List<Providers.Shipping.UPS.Models.Shipment> GetOriginList()
        {
            var userID = UPSShippingProviderConfig.UserName;
            var password = UPSShippingProviderConfig.Password;
            var accessNumber = UPSShippingProviderConfig.AccessLicenseNumber;
            if (string.IsNullOrWhiteSpace(userID) || string.IsNullOrWhiteSpace(password))
            {
                LastErrorCode = LastErrorCodeType.BadUserPassword;
                return new();
            }
            return GetOriginList(userID, password, accessNumber, string.Empty, string.Empty);
        }

        /// <summary>Gets origin list.</summary>
        /// <param name="userID">      Identifier for the user.</param>
        /// <param name="password">    The password.</param>
        /// <param name="accessNumber">The access number.</param>
        /// <param name="dateRange">   The date range.</param>
        /// <param name="bookmark">    The bookmark.</param>
        /// <returns>The origin list.</returns>
        public static List<Providers.Shipping.UPS.Models.Shipment> GetOriginList(
            string userID,
            string password,
            string accessNumber,
            string dateRange,
            string bookmark)
        {
            var output = new List<Providers.Shipping.UPS.Models.Shipment>();
            var commonSeName = string.Empty;
            var commonSeNumber = string.Empty;
            var strDay = CEFConfigDictionary.ShippingTrackingDayRolling;
            var days = !string.IsNullOrWhiteSpace(strDay) ? int.Parse(strDay!) : 6;
            var endDate = DateExtensions.GenDateTime;
            var startDate = endDate.AddDays(-days);
            var dateRangeParameter = $"<SubscriptionRequest><DateTimeRange><BeginDateTime>{startDate:yyyyMMddhhmmss}"
                + $"</BeginDateTime><EndDateTime>{endDate:yyyyMMddhhmmss}</EndDateTime></DateTimeRange></SubscriptionRequest>";
            var parameter = string.IsNullOrWhiteSpace(bookmark) ? dateRangeParameter : dateRange + bookmark;
            if (!GetQuantumFeed(userID, password, accessNumber, parameter))
            {
                return output;
            }
            var document = new XmlDocument();
            document.LoadXml(ResponseData);
            var se = document.GetElementsByTagName("SubscriptionEvents");
            for (var i = 0; i < se.Count; i++)
            {
                var node = se.Item(i);
                if (node == null)
                {
                    continue;
                }
                commonSeName = node.ChildNodes
                    .Cast<XmlNode>()
                    .Aggregate(
                        commonSeName,
                        (c, inNode) => ProcessInNode(inNode, c, output, ref commonSeNumber));
            }
            var bookmarkNode = document.GetElementsByTagName("Bookmark");
            for (var i = 0; i < bookmarkNode.Count; i++)
            {
                var nodeValue = bookmarkNode.Item(i);
                if (string.IsNullOrWhiteSpace(nodeValue?.InnerText))
                {
                    continue;
                }
                output.AddRange(
                    GetOriginList(
                        userID,
                        password,
                        accessNumber,
                        string.IsNullOrWhiteSpace(dateRange) ? dateRangeParameter : dateRange,
                        "<Bookmark>" + nodeValue!.InnerText + "</Bookmark>"));
            }
            return output;
        }

        /// <summary>Gets quantum feed.</summary>
        /// <returns>True if it succeeds, false if it fails.</returns>
        public static bool GetQuantumFeed()
        {
            var userID = UPSShippingProviderConfig.UserName;
            var password = UPSShippingProviderConfig.Password;
            var accessNumber = UPSShippingProviderConfig.AccessLicenseNumber;
            if (userID == null || password == null || userID.Length == 0 || password.Length == 0)
            {
                LastErrorCode = LastErrorCodeType.BadUserPassword;
                return false;
            }
            return GetQuantumFeed(userID, password, accessNumber, string.Empty);
        }

        /// <summary>Gets quantum feed.</summary>
        /// <param name="userID">      Identifier for the user.</param>
        /// <param name="password">    The password.</param>
        /// <param name="accessNumber">The access number.</param>
        /// <param name="parameters">  Options for controlling the operation.</param>
        /// <returns>True if it succeeds, false if it fails.</returns>
        public static bool GetQuantumFeed(string userID, string password, string accessNumber, string parameters)
        {
            var requestString = $"<?xml version=\"1.0\" ?><AccessRequest xml:lang='en-US'><AccessLicenseNumber>{accessNumber}</AccessLicenseNumber><UserId>{userID}</UserId><Password>{password}</Password></AccessRequest>"
                + $"<?xml version=\"1.0\" ?><QuantumViewRequest xml:lang='en-US'><Request><TransactionReference></TransactionReference><RequestAction>QVEvents</RequestAction></Request>{parameters}</QuantumViewRequest>";
            var req = (HttpWebRequest)WebRequest.Create(QuantumEndPointURL);
            req.ContentType = "application/x-www-form-urlencoded";
            req.Method = "POST";
            // write the post values
            var buffer = Encoding.UTF8.GetBytes(requestString);
            req.ContentLength = buffer.Length;
            using (var writer = req.GetRequestStream())
            {
                writer.Write(buffer, 0, buffer.Length);
            }
            // get the response
            ResponseData = string.Empty;
            var encode = Encoding.GetEncoding("utf-8");
            using var resp = (HttpWebResponse)req.GetResponse();
            using var receiveStream = resp.GetResponseStream() ?? throw new NullReferenceException();
            using var readStream = new StreamReader(receiveStream, encode);
            ResponseData = readStream.ReadToEnd();
            if (ResponseData.Contains("There are no unread files for the given Subscriber ID."))
            {
                LastErrorCode = LastErrorCodeType.NoNewRecords;
                return false;
            }
            if (ResponseData.Contains("The XML document is well formed but the document is not valid"))
            {
                LastErrorCode = LastErrorCodeType.NoData;
                return false;
            }
            return true;
        }

        private static string ProcessInNode(XmlNode inNode, string commonSeName, List<Providers.Shipping.UPS.Models.Shipment> output, ref string commonSeNumber)
        {
            if (inNode.Name == "Name")
            {
                commonSeName = inNode.FirstChild!.Value!;
            }
            if (inNode.Name == "Number")
            {
                commonSeNumber = inNode.FirstChild!.Value!;
            }
            if (inNode.Name != "SubscriptionFile")
            {
                return commonSeName;
            }
            var commonSeFileName = inNode.FirstChild!.FirstChild!.Value;
            foreach (XmlNode originNode in inNode.ChildNodes)
            {
                ProcessOriginNode(originNode, commonSeName, commonSeNumber, commonSeFileName!, output);
            }
            return commonSeName;
        }

        private static void ProcessOriginNode(XmlNode originNode, string commonSeName, string commonSeNumber, string commonSeFileName, ICollection<Providers.Shipping.UPS.Models.Shipment> output)
        {
            if (originNode.Name != "Origin")
            {
                return;
            }
            var thisOrigin = new Providers.Shipping.UPS.Models.Shipment
            {
                SubscriptionEventName = commonSeName,
                SubscriptionEventNumber = commonSeNumber,
                SubscriptionFileName = commonSeFileName,
            };
            ParseDetailNodes(originNode, thisOrigin);
            output.Add(thisOrigin);
        }

        private static void ParseDetailNodes(XmlNode originNode, Providers.Shipping.UPS.Models.Shipment thisOrigin)
        {
            foreach (XmlNode detail in originNode.ChildNodes)
            {
                if (detail.Name == "PackageReferenceNumber")
                {
                    thisOrigin.PackageReferenceNumber = detail.FirstChild!.FirstChild!.Value;
                }
                if (detail.Name == "ShipmentReferenceNumber")
                {
                    thisOrigin.ShipmentReferenceNumber = detail.FirstChild!.FirstChild!.Value;
                }
                if (detail.Name == "ShipperNumber")
                {
                    thisOrigin.ShipperNumber = detail.FirstChild!.Value;
                }
                if (detail.Name == "TrackingNumber")
                {
                    thisOrigin.TrackingNumber = detail.FirstChild!.Value;
                }
                if (detail.Name == "Date")
                {
                    thisOrigin.DateTime = DateTime.Parse(detail.FirstChild!.Value!.Insert(4, "/").Insert(7, "/"));
                }
                if (detail.Name == "Time")
                {
                    thisOrigin.DateTime = DateTime.Parse(thisOrigin.DateTime.ToShortDateString() + " " + detail.FirstChild!.Value!.Insert(2, ":").Insert(5, ":"));
                }
            }
        }
    }
}
