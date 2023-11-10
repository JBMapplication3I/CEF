// <copyright file="Tracking.cs" company="clarity-ventures.com">
// Copyright (c) 2016-2023 clarity-ventures.com. All rights reserved.
// </copyright>
// <summary>Implements the tracking class</summary>
// ReSharper disable CollectionNeverUpdated.Global, InconsistentNaming, MemberHidesStaticFromOuterClass, UnusedAutoPropertyAccessor.Global, UnusedMember.Global
namespace Clarity.Ecommerce.Providers.Shipping.UPS.Models
{
    using System;
    using System.Collections.Generic;
    using System.IO;
    using System.Net;
    using System.Text;
    using System.Xml;

    /// <summary>A tracking.</summary>
    public class Tracking
    {
        /// <summary>URL of the tracking end point.</summary>
        private static readonly string TrackingEndPointURL = "https://wwwcie.ups.com/ups.app/xml/Track";

        /// <summary>Information describing the response.</summary>
        private static string responseData = string.Empty;

        /// <summary>Initializes a new instance of the <see cref="Tracking"/> class.</summary>
        // ReSharper disable once EmptyConstructor
        public Tracking()
        {
        }

        /// <summary>Gets track response.</summary>
        /// <param name="trackingNumber">The tracking number.</param>
        /// <returns>The track response.</returns>
        public static TrackResponse GetTrackResponse(string trackingNumber)
        {
            return GetTrackResponse(
                UPSShippingProviderConfig.UserName,
                UPSShippingProviderConfig.Password,
                UPSShippingProviderConfig.AccessLicenseNumber,
                trackingNumber);
        }

        /// <summary>Gets track response.</summary>
        /// <param name="userID">        Identifier for the user.</param>
        /// <param name="password">      The password.</param>
        /// <param name="accessNumber">  The access number.</param>
        /// <param name="trackingNumber">The tracking number.</param>
        /// <returns>The track response.</returns>
        // ReSharper disable UnusedParameter.Global
        public static TrackResponse GetTrackResponse(string userID, string password, string accessNumber, string trackingNumber)
        // ReSharper restore UnusedParameter.Global
        {
            var requestString = $"<?xml version=\"1.0\" ?><AccessRequest xml:lang='en-US'><AccessLicenseNumber>{accessNumber}</AccessLicenseNumber><UserId>{userID}</UserId><Password>{password}</Password></AccessRequest>";
            requestString += $"<?xml version=\"1.0\" ?><TrackRequest><Request><TransactionReference></TransactionReference><RequestAction>Track</RequestAction><RequestOption>1</RequestOption></Request><TrackingNumber>{trackingNumber}</TrackingNumber></TrackRequest>";
            var req = (HttpWebRequest)WebRequest.Create(TrackingEndPointURL);
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
            responseData = string.Empty;
            var resp = (HttpWebResponse)req.GetResponse();
            using (var receiveStream = resp.GetResponseStream())
            {
                if (receiveStream == null)
                {
                    return new() { Response = null, Shipments = new() };
                }
                using var readStream = new StreamReader(receiveStream, Encoding.UTF8);
                responseData = readStream.ReadToEnd();
            }
            resp.Close();
            // Because UPS doesn't wrap arrays in tags, I need to fix it so that serialize will work
            FixUpArray(ref responseData, "/TrackResponse/Response/Error/ErrorLocation", "ErrorLocations");
            FixUpArray(ref responseData, "/TrackResponse/Response/Error/ErrorDigest", "ErrorDigests");
            FixUpArray(ref responseData, "/TrackResponse/Shipment/Activity/ActivityLocation/Address", "Addresses");
            FixUpArray(ref responseData, "/TrackResponse/Shipment/Activity/ActivityLocation", "ActivityLocations");
            FixUpArray(ref responseData, "/TrackResponse/Shipment/Activity", "Activities");
            FixUpArray(ref responseData, "/TrackResponse/Shipment/CarrierActivityInformation", "CarrierActivityInformations");
            FixUpArray(ref responseData, "/TrackResponse/Shipment/Package/Activity", "Activities");
            FixUpArray(ref responseData, "/TrackResponse/Shipment/Package/Message", "Messages");
            FixUpArray(ref responseData, "/TrackResponse/Shipment/Package/ReferenceNumber", "ReferenceNumbers");
            FixUpArray(ref responseData, "/TrackResponse/Shipment", "Shipments");
            var doc = new XmlDocument();
            doc.LoadXml(responseData);
            using var tempStream = new MemoryStream();
            doc.Save(tempStream);
            tempStream.Seek(0, 0);
            var tr = new TrackResponse();
            var reader = new System.Xml.Serialization.XmlSerializer(tr.GetType());
            // Deserialize the content of the file
            tr = (TrackResponse)reader.Deserialize(tempStream)!;
            foreach (var shipment in tr.Shipments!)
            {
                foreach (var activity in shipment.Package!.Activities!)
                {
                    var date = activity.Date!.Insert(4, "/").Insert(7, "/");
                    var time = activity.Time!.Insert(2, ":").Insert(5, ":");
                    activity.DateTime = DateTime.Parse(date + " " + time);
                }
            }
            return tr;
        }

        /// <summary>Fix up array.</summary>
        /// <param name="respData"> Information describing the resp.</param>
        /// <param name="itemPath"> Full pathname of the item file.</param>
        /// <param name="arrayName">Name of the array.</param>
        private static void FixUpArray(ref string respData, string itemPath, string arrayName)
        {
            ////var parentPath = itemPath.Substring(0, itemPath.LastIndexOf("/", StringComparison.Ordinal));
            ////var parentItemTag = "<" + parentPath.Substring(parentPath.LastIndexOf("/", StringComparison.Ordinal) + 1) + ">";
            ////var parentItemTagEnd = parentItemTag.Replace("<", "</");
            var itemTag = "<" + itemPath[(itemPath.LastIndexOf("/", StringComparison.Ordinal) + 1)..] + ">";
            var itemTagClose = itemTag.Replace("<", "</");
            var arrayTag = "<" + arrayName + ">";
            var arrayNameClose = arrayTag.Replace("<", "</");
            var doc = new XmlDocument();
            doc.LoadXml(respData);
            ////XmlNodeList nodes = doc.SelectNodes(parentPath);
            var nodes = doc.SelectNodes(itemPath);
            if (nodes == null)
            {
                return;
            }
            if (nodes.Count > 0)
            {
                var parentNode = nodes[0]!.ParentNode;
                if (parentNode == null)
                {
                    return;
                }
                var tempInner = parentNode.InnerXml;
                var pos = tempInner.IndexOf(itemTag, StringComparison.Ordinal);
                while (pos >= 0)
                {
                    tempInner = tempInner.Insert(pos, arrayTag);
                    pos = tempInner.LastIndexOf(itemTagClose, StringComparison.Ordinal);
                    tempInner = tempInner.Insert(pos + itemTagClose.Length, arrayNameClose);
                    pos = tempInner.IndexOf(itemTag, pos + 1, StringComparison.Ordinal);
                }
                parentNode.InnerXml = tempInner;
            }
            respData = doc.OuterXml;
        }

        /// <summary>(Serializable)a track response.</summary>
        [Serializable]
        public class TrackResponse
        {
            /// <summary>Gets or sets the response.</summary>
            /// <value>The response.</value>
            public Response? Response { get; set; }

            /// <summary>Gets or sets the shipments.</summary>
            /// <value>The shipments.</value>
            public List<Shipment>? Shipments { get; set; }
        }

        /// <summary>(Serializable)a response.</summary>
        [Serializable]
        public class Response
        {
            /// <summary>Gets or sets the transaction reference.</summary>
            /// <value>The transaction reference.</value>
            public TransactionReference? TransactionReference { get; set; }

            /// <summary>Gets or sets the response status code.</summary>
            /// <value>The response status code.</value>
            public string? ResponseStatusCode { get; set; }

            /// <summary>Gets or sets information describing the response status.</summary>
            /// <value>Information describing the response status.</value>
            public string? ResponseStatusDescription { get; set; }

            /// <summary>Gets or sets the error.</summary>
            /// <value>The error.</value>
            public Error? Error { get; set; }
        }

        /// <summary>(Serializable)a transaction reference.</summary>
        [Serializable]
        public class TransactionReference
        {
            /// <summary>Gets or sets a context for the customer.</summary>
            /// <value>The customer context.</value>
            public string? CustomerContext { get; set; }

            /// <summary>Gets or sets the identifier of the transaction.</summary>
            /// <value>The identifier of the transaction.</value>
            public string? TransactionIdentifier { get; set; }

            /// <summary>Gets or sets the XPCI version.</summary>
            /// <value>The XPCI version.</value>
            public string? XpciVersion { get; set; }
        }

        /// <summary>(Serializable)a error.</summary>
        [Serializable]
        public class Error
        {
            /// <summary>Gets or sets the error severity.</summary>
            /// <value>The error severity.</value>
            public string? ErrorSeverity { get; set; }

            /// <summary>Gets or sets the error code.</summary>
            /// <value>The error code.</value>
            public string? ErrorCode { get; set; }

            /// <summary>Gets or sets information describing the error.</summary>
            /// <value>Information describing the error.</value>
            public string? ErrorDescription { get; set; }

            /// <summary>Gets or sets the minimum retry seconds.</summary>
            /// <value>The minimum retry seconds.</value>
            public string? MinimumRetrySeconds { get; set; }

            /// <summary>Gets or sets the error locations.</summary>
            /// <value>The error locations.</value>
            public List<ErrorLocation>? ErrorLocations { get; set; }

            /// <summary>Gets or sets the error digests.</summary>
            /// <value>The error digests.</value>
            public List<string>? ErrorDigests { get; set; }
        }

        /// <summary>(Serializable)a error location.</summary>
        [Serializable]
        public class ErrorLocation
        {
            /// <summary>Gets or sets the name of the error location element.</summary>
            /// <value>The name of the error location element.</value>
            public string? ErrorLocationElementName { get; set; }

            /// <summary>Gets or sets the name of the error location attribute.</summary>
            /// <value>The name of the error location attribute.</value>
            public string? ErrorLocationAttributeName { get; set; }
        }

        /// <summary>(Serializable)a shipment.</summary>
        [Serializable]
        public class Shipment
        {
            /// <summary>Gets or sets the inquiry number.</summary>
            /// <value>The inquiry number.</value>
            public CodeDescriptionValue? InquiryNumber { get; set; }

            /// <summary>Gets or sets the shipment identification number.</summary>
            /// <value>The shipment identification number.</value>
            public string? ShipmentIdentificationNumber { get; set; }

            /// <summary>Gets or sets the type of the shipment.</summary>
            /// <value>The type of the shipment.</value>
            public CodeDescription? ShipmentType { get; set; }

            /// <summary>Gets or sets the candidate bookmark.</summary>
            /// <value>The candidate bookmark.</value>
            public string? CandidateBookmark { get; set; }

            /// <summary>Gets or sets the shipper.</summary>
            /// <value>The shipper.</value>
            public Shipper? Shipper { get; set; }

            /// <summary>Gets or sets the ship to.</summary>
            /// <value>The ship to.</value>
            public ShipTo? ShipTo { get; set; }

            /// <summary>Gets or sets the shipment weight.</summary>
            /// <value>The shipment weight.</value>
            public PackageWeight? ShipmentWeight { get; set; }

            /// <summary>Gets or sets the service.</summary>
            /// <value>The service.</value>
            public CodeDescription? Service { get; set; }

            /// <summary>Gets or sets the reference number.</summary>
            /// <value>The reference number.</value>
            public CodeDescriptionValue? ReferenceNumber { get; set; }

            /// <summary>Gets or sets the current status.</summary>
            /// <value>The current status.</value>
            public CodeDescription? CurrentStatus { get; set; }

            /// <summary>Gets or sets the pickup date.</summary>
            /// <value>The pickup date.</value>
            public string? PickupDate { get; set; }

            /// <summary>Gets or sets the delivery details.</summary>
            /// <value>The delivery details.</value>
            public DeliveryDetails? DeliveryDetails { get; set; }

            /// <summary>Gets or sets a list of times of the deliver dates.</summary>
            /// <value>A list of times of the deliver dates.</value>
            public List<DeliveryDateTime>? DeliverDateTimes { get; set; }

            /// <summary>Gets or sets the volume.</summary>
            /// <value>The volume.</value>
            public CodeDescriptionValue? Volume { get; set; }

            /// <summary>Gets or sets the name of the bill to.</summary>
            /// <value>The name of the bill to.</value>
            public string? BillToName { get; set; }

            /// <summary>Gets or sets the pick up service center.</summary>
            /// <value>The pick up service center.</value>
            public ServiceCenter? PickUpServiceCenter { get; set; }

            /// <summary>Gets or sets the number of pieces.</summary>
            /// <value>The total number of pieces.</value>
            public string? NumberOfPieces { get; set; }

            /// <summary>Gets or sets the number of pallets.</summary>
            /// <value>The total number of pallets.</value>
            public string? NumberOfPallets { get; set; }

            /// <summary>Gets or sets the shipment service option.</summary>
            /// <value>The shipment service option.</value>
            public ShipmentServiceOption? ShipmentServiceOption { get; set; }

            /// <summary>Gets or sets the estimated delivery details.</summary>
            /// <value>The estimated delivery details.</value>
            public EstimatedDeliveryDetails? EstimatedDeliveryDetails { get; set; }

            /// <summary>Gets or sets the name of the signed for by.</summary>
            /// <value>The name of the signed for by.</value>
            public string? SignedForByName { get; set; }

            /// <summary>Gets or sets the activities.</summary>
            /// <value>The activities.</value>
            public List<ShipmentActivity>? Activities { get; set; }

            /// <summary>Gets or sets the origin port details.</summary>
            /// <value>The origin port details.</value>
            public OriginPortDetails? OriginPortDetails { get; set; }

            /// <summary>Gets or sets destination port details.</summary>
            /// <value>The destination port details.</value>
            public DestinationPortDetails? DestinationPortDetails { get; set; }

            /// <summary>Gets or sets the description of goods.</summary>
            /// <value>The description of goods.</value>
            public string? DescriptionOfGoods { get; set; }

            /// <summary>Gets or sets the cargo ready.</summary>
            /// <value>The cargo ready.</value>
            public DateAndTime? CargoReady { get; set; }

            /// <summary>Gets or sets the manifest.</summary>
            /// <value>The manifest.</value>
            public DateAndTime? Manifest { get; set; }

            /// <summary>Gets or sets information describing the carrier activity.</summary>
            /// <value>Information describing the carrier activity.</value>
            public List<CarrierActivityInformation>? CarrierActivityInformation { get; set; }

            /// <summary>Gets or sets the scheduled delivery date.</summary>
            /// <value>The scheduled delivery date.</value>
            public string? ScheduledDeliveryDate { get; set; }

            /// <summary>Gets or sets the scheduled delivery time.</summary>
            /// <value>The scheduled delivery time.</value>
            public string? ScheduledDeliveryTime { get; set; }

            /// <summary>Gets or sets the file number.</summary>
            /// <value>The file number.</value>
            public string? FileNumber { get; set; }

            /// <summary>Gets or sets the appointment.</summary>
            /// <value>The appointment.</value>
            public Appointment? Appointment { get; set; }

            /// <summary>Gets or sets the package.</summary>
            /// <value>The package.</value>
            public Package? Package { get; set; }
        }

        /// <summary>(Serializable)a code description value.</summary>
        [Serializable]
        public class CodeDescriptionValue
        {
            /// <summary>Gets or sets the code.</summary>
            /// <value>The code.</value>
            public string? Code { get; set; }

            /// <summary>Gets or sets the description.</summary>
            /// <value>The description.</value>
            public string? Description { get; set; }

            /// <summary>Gets or sets the value.</summary>
            /// <value>The value.</value>
            public string? Value { get; set; }
        }

        /// <summary>(Serializable)description of the code.</summary>
        [Serializable]
        public class CodeDescription
        {
            /// <summary>Gets or sets the code.</summary>
            /// <value>The code.</value>
            public string? Code { get; set; }

            /// <summary>Gets or sets the description.</summary>
            /// <value>The description.</value>
            public string? Description { get; set; }
        }

        /// <summary>(Serializable)a shipper.</summary>
        [Serializable]
        public class Shipper
        {
            /// <summary>Gets or sets the shipper number.</summary>
            /// <value>The shipper number.</value>
            public string? ShipperNumber { get; set; }

            /// <summary>Gets or sets the address.</summary>
            /// <value>The address.</value>
            public Address? Address { get; set; }
        }

        /// <summary>(Serializable)a address.</summary>
        [Serializable]
        public class Address
        {
            /// <summary>Gets or sets the address line 1.</summary>
            /// <value>The address line 1.</value>
            public string? AddressLine1 { get; set; }

            /// <summary>Gets or sets the address line 2.</summary>
            /// <value>The address line 2.</value>
            public string? AddressLine2 { get; set; }

            /// <summary>Gets or sets the address line 3.</summary>
            /// <value>The address line 3.</value>
            public string? AddressLine3 { get; set; }

            /// <summary>Gets or sets the city.</summary>
            /// <value>The city.</value>
            public string? City { get; set; }

            /// <summary>Gets or sets the state province code.</summary>
            /// <value>The state province code.</value>
            public string? StateProvinceCode { get; set; }

            /// <summary>Gets or sets the postal code.</summary>
            /// <value>The postal code.</value>
            public string? PostalCode { get; set; }

            /// <summary>Gets or sets the country code.</summary>
            /// <value>The country code.</value>
            public string? CountryCode { get; set; }
        }

        /// <summary>(Serializable)a ship to.</summary>
        [Serializable]
        public class ShipTo
        {
            /// <summary>Gets or sets the address.</summary>
            /// <value>The address.</value>
            public Address? Address { get; set; }
        }

        /// <summary>(Serializable)a package weight.</summary>
        [Serializable]
        public class PackageWeight
        {
            /// <summary>Gets or sets the unit of measurement.</summary>
            /// <value>The unit of measurement.</value>
            public CodeDescription? UnitOfMeasurement { get; set; }

            /// <summary>Gets or sets the weight.</summary>
            /// <value>The weight.</value>
            public string? Weight { get; set; }
        }

        /// <summary>(Serializable)a delivery details.</summary>
        [Serializable]
        public class DeliveryDetails
        {
            /// <summary>Gets or sets the delivery date.</summary>
            /// <value>The delivery date.</value>
            public DateAndTime? DeliveryDate { get; set; }

            /// <summary>Gets or sets the service center.</summary>
            /// <value>The service center.</value>
            public ServiceCenter? ServiceCenter { get; set; }
        }

        /// <summary>(Serializable)a date and time.</summary>
        [Serializable]
        public class DateAndTime
        {
            /// <summary>Gets or sets the date.</summary>
            /// <value>The date.</value>
            public string? Date { get; set; }

            /// <summary>Gets or sets the time.</summary>
            /// <value>The time.</value>
            public string? Time { get; set; }
        }

        /// <summary>(Serializable)a service center.</summary>
        [Serializable]
        public class ServiceCenter
        {
            /// <summary>Gets or sets the city.</summary>
            /// <value>The city.</value>
            public string? City { get; set; }

            /// <summary>Gets or sets the state province code.</summary>
            /// <value>The state province code.</value>
            public string? StateProvinceCode { get; set; }
        }

        /// <summary>(Serializable)a delivery date time.</summary>
        [Serializable]
        public class DeliveryDateTime
        {
            /// <summary>Gets or sets the type.</summary>
            /// <value>The type.</value>
            public CodeDescription? Type { get; set; }

            /// <summary>Gets or sets the date.</summary>
            /// <value>The date.</value>
            public string? Date { get; set; }

            /// <summary>Gets or sets the time.</summary>
            /// <value>The time.</value>
            public string? Time { get; set; }
        }

        /// <summary>(Serializable)a shipment service option.</summary>
        [Serializable]
        public class ShipmentServiceOption
        {
            /// <summary>Gets or sets the cod.</summary>
            /// <value>The cod.</value>
            public COD? COD { get; set; }
        }

        /// <summary>(Serializable)a cod.</summary>
        [Serializable]
        public class COD
        {
            /// <summary>Gets or sets the cod amount.</summary>
            /// <value>The cod amount.</value>
            public CODAmount? CODAmount { get; set; }

            /// <summary>Gets or sets the status.</summary>
            /// <value>The status.</value>
            public CodeDescription? Status { get; set; }

            /// <summary>Gets or sets the control number.</summary>
            /// <value>The control number.</value>
            public string? ControlNumber { get; set; }
        }

        /// <summary>(Serializable)a cod amount.</summary>
        [Serializable]
        public class CODAmount
        {
            /// <summary>Gets or sets the currency code.</summary>
            /// <value>The currency code.</value>
            public string? CurrencyCode { get; set; }

            /// <summary>Gets or sets the monetary value.</summary>
            /// <value>The monetary value.</value>
            public string? MonetaryValue { get; set; }
        }

        /// <summary>(Serializable)a estimated delivery details.</summary>
        [Serializable]
        public class EstimatedDeliveryDetails
        {
            /// <summary>Gets or sets the date.</summary>
            /// <value>The date.</value>
            public string? Date { get; set; }

            /// <summary>Gets or sets the service center.</summary>
            /// <value>The service center.</value>
            public ServiceCenter? ServiceCenter { get; set; }
        }

        /// <summary>(Serializable)a shipment activity.</summary>
        [Serializable]
        public class ShipmentActivity
        {
            /// <summary>Gets or sets the activity locations.</summary>
            /// <value>The activity locations.</value>
            public List<ShipmentActivityLocation>? ActivityLocations { get; set; }

            /// <summary>Gets or sets the description.</summary>
            /// <value>The description.</value>
            public string? Description { get; set; }

            /// <summary>Gets or sets the date.</summary>
            /// <value>The date.</value>
            public string? Date { get; set; }

            /// <summary>Gets or sets the time.</summary>
            /// <value>The time.</value>
            public string? Time { get; set; }

            /// <summary>Gets or sets the trailer.</summary>
            /// <value>The trailer.</value>
            public string? Trailer { get; set; }
        }

        /// <summary>(Serializable)a shipment activity location.</summary>
        [Serializable]
        public class ShipmentActivityLocation
        {
            /// <summary>Gets or sets the addresses.</summary>
            /// <value>The addresses.</value>
            public List<ShortAddress>? Addresses { get; set; }
        }

        /// <summary>(Serializable)a short address.</summary>
        [Serializable]
        public class ShortAddress
        {
            /// <summary>Gets or sets the city.</summary>
            /// <value>The city.</value>
            public string? City { get; set; }

            /// <summary>Gets or sets the state province code.</summary>
            /// <value>The state province code.</value>
            public string? StateProvinceCode { get; set; }

            /// <summary>Gets or sets the postal code.</summary>
            /// <value>The postal code.</value>
            public string? PostalCode { get; set; }

            /// <summary>Gets or sets the country code.</summary>
            /// <value>The country code.</value>
            public string? CountryCode { get; set; }
        }

        /// <summary>(Serializable)a origin port details.</summary>
        [Serializable]
        public class OriginPortDetails
        {
            /// <summary>Gets or sets the origin port.</summary>
            /// <value>The origin port.</value>
            public string? OriginPort { get; set; }

            /// <summary>Gets or sets the estimated departure.</summary>
            /// <value>The estimated departure.</value>
            public DateAndTime? EstimatedDeparture { get; set; }
        }

        /// <summary>(Serializable)a destination port details.</summary>
        [Serializable]
        public class DestinationPortDetails
        {
            /// <summary>Gets or sets destination port.</summary>
            /// <value>The destination port.</value>
            public string? DestinationPort { get; set; }

            /// <summary>Gets or sets the estimated arrival.</summary>
            /// <value>The estimated arrival.</value>
            public DateAndTime? EstimatedArrival { get; set; }
        }

        /// <summary>(Serializable)information about the carrier activity.</summary>
        [Serializable]
        public class CarrierActivityInformation
        {
            /// <summary>Gets or sets the identifier of the carrier.</summary>
            /// <value>The identifier of the carrier.</value>
            public string? CarrierId { get; set; }

            /// <summary>Gets or sets the description.</summary>
            /// <value>The description.</value>
            public string? Description { get; set; }

            /// <summary>Gets or sets the status.</summary>
            /// <value>The status.</value>
            public string? Status { get; set; }

            /// <summary>Gets or sets the arrival.</summary>
            /// <value>The arrival.</value>
            public DateAndTime? Arrival { get; set; }

            /// <summary>Gets or sets the departure.</summary>
            /// <value>The departure.</value>
            public DateAndTime? Departure { get; set; }

            /// <summary>Gets or sets the origin port.</summary>
            /// <value>The origin port.</value>
            public string? OriginPort { get; set; }

            /// <summary>Gets or sets destination port.</summary>
            /// <value>The destination port.</value>
            public string? DestinationPort { get; set; }
        }

        /// <summary>(Serializable)a appointment.</summary>
        [Serializable]
        public class Appointment
        {
            /// <summary>Gets or sets the made.</summary>
            /// <value>The made.</value>
            public DateAndTime? Made { get; set; }

            /// <summary>Gets or sets the requested.</summary>
            /// <value>The requested.</value>
            public DateAndTime? Requested { get; set; }

            /// <summary>Gets or sets the begin time.</summary>
            /// <value>The begin time.</value>
            public string? BeginTime { get; set; }

            /// <summary>Gets or sets the end time.</summary>
            /// <value>The end time.</value>
            public string? EndTime { get; set; }
        }

        /// <summary>(Serializable)a package.</summary>
        [Serializable]
        public class Package
        {
            /// <summary>Gets or sets the tracking number.</summary>
            /// <value>The tracking number.</value>
            public string? TrackingNumber { get; set; }

            /// <summary>Gets or sets the rescheduled delivery date.</summary>
            /// <value>The rescheduled delivery date.</value>
            public string? RescheduledDeliveryDate { get; set; }

            /// <summary>Gets or sets the rescheduled delivery time.</summary>
            /// <value>The rescheduled delivery time.</value>
            public string? RescheduledDeliveryTime { get; set; }

            /// <summary>Gets or sets the reroute.</summary>
            /// <value>The reroute.</value>
            public AddressOnly? Reroute { get; set; }

            /// <summary>Gets or sets the return to.</summary>
            /// <value>The return to.</value>
            public AddressOnly? ReturnTo { get; set; }

            /// <summary>Gets or sets the package service option.</summary>
            /// <value>The package service option.</value>
            public PackageServiceOption? PackageServiceOption { get; set; }

            /// <summary>Gets or sets the activities.</summary>
            /// <value>The activities.</value>
            public List<Activity>? Activities { get; set; }

            /// <summary>Gets or sets the messages.</summary>
            /// <value>The messages.</value>
            public List<CodeDescription>? Messages { get; set; }

            /// <summary>Gets or sets the package weight.</summary>
            /// <value>The package weight.</value>
            public PackageWeight? PackageWeight { get; set; }

            /// <summary>Gets or sets the reference numbers.</summary>
            /// <value>The reference numbers.</value>
            public List<CodeValue>? ReferenceNumbers { get; set; }

            /// <summary>Gets or sets the type of the product.</summary>
            /// <value>The type of the product.</value>
            public CodeDescription? ProductType { get; set; }

            /// <summary>Gets or sets the location assured.</summary>
            /// <value>The location assured.</value>
            public string? LocationAssured { get; set; }

            /// <summary>Gets or sets the alternate tracking number.</summary>
            /// <value>The alternate tracking number.</value>
            public string? AlternateTrackingNumber { get; set; }

            /// <summary>Gets or sets the alternate tracking infos.</summary>
            /// <value>The alternate tracking infos.</value>
            public List<AlternateTrackingInfo>? AlternateTrackingInfos { get; set; }
        }

        /// <summary>(Serializable)a address only.</summary>
        [Serializable]
        public class AddressOnly
        {
            /// <summary>Gets or sets the address.</summary>
            /// <value>The address.</value>
            public Address? Address { get; set; }
        }

        /// <summary>(Serializable)a package service option.</summary>
        [Serializable]
        public class PackageServiceOption
        {
            /// <summary>Gets or sets the signature required.</summary>
            /// <value>The signature required.</value>
            public CodeDescription? SignatureRequired { get; set; }

            /// <summary>Gets or sets the import control.</summary>
            /// <value>The import control.</value>
            public string? ImportControl { get; set; }

            /// <summary>Gets or sets the commercial invoice removal.</summary>
            /// <value>The commercial invoice removal.</value>
            public string? CommercialInvoiceRemoval { get; set; }

            /// <summary>Gets or sets the up scarbonneutral.</summary>
            /// <value>The up scarbonneutral.</value>
            public string? UPScarbonneutral { get; set; }

            /// <summary>Gets or sets the USPS pic number.</summary>
            /// <value>The USPS pic number.</value>
            public string? USPSPICNumber { get; set; }

            /// <summary>Gets or sets the exchange based.</summary>
            /// <value>The exchange based.</value>
            public string? ExchangeBased { get; set; }

            /// <summary>Gets or sets the pack and collect.</summary>
            /// <value>The pack and collect.</value>
            public string? PackAndCollect { get; set; }
        }

        /// <summary>(Serializable)a activity.</summary>
        [Serializable]
        public class Activity
        {
            /// <summary>Gets or sets the alternate tracking infos.</summary>
            /// <value>The alternate tracking infos.</value>
            public List<AlternateTrackingInfo>? AlternateTrackingInfos { get; set; }

            /// <summary>Gets or sets the activity location.</summary>
            /// <value>The activity location.</value>
            public ActivityLocation? ActivityLocation { get; set; }

            /// <summary>Gets or sets the status.</summary>
            /// <value>The status.</value>
            public Status? Status { get; set; }

            /// <summary>Gets or sets the date.</summary>
            /// <value>The date.</value>
            public string? Date { get; set; }

            /// <summary>Gets or sets the time.</summary>
            /// <value>The time.</value>
            public string? Time { get; set; }

            /// <summary>Gets or sets the date time.</summary>
            /// <value>The date time.</value>
            public DateTime DateTime { get; set; }
        }

        /// <summary>(Serializable)information about the alternate tracking.</summary>
        [Serializable]
        public class AlternateTrackingInfo
        {
            /// <summary>Gets or sets the type.</summary>
            /// <value>The type.</value>
            public string? Type { get; set; }

            /// <summary>Gets or sets the description.</summary>
            /// <value>The description.</value>
            public string? Description { get; set; }

            /// <summary>Gets or sets the value.</summary>
            /// <value>The value.</value>
            public string? Value { get; set; }
        }

        /// <summary>(Serializable)a activity location.</summary>
        [Serializable]
        public class ActivityLocation
        {
            /// <summary>Gets or sets the address.</summary>
            /// <value>The address.</value>
            public Address? Address { get; set; }

            /// <summary>Gets or sets the address artifact format.</summary>
            /// <value>The address artifact format.</value>
            public AddressArtifactFormat? AddressArtifactFormat { get; set; }

            /// <summary>Gets or sets the transport facility.</summary>
            /// <value>The transport facility.</value>
            public TransportFacility? TransportFacility { get; set; }

            /// <summary>Gets or sets the code.</summary>
            /// <value>The code.</value>
            public string? Code { get; set; }

            /// <summary>Gets or sets the description.</summary>
            /// <value>The description.</value>
            public string? Description { get; set; }

            /// <summary>Gets or sets the name of the signed for by.</summary>
            /// <value>The name of the signed for by.</value>
            public string? SignedForByName { get; set; }

            /// <summary>Gets or sets the pod letter.</summary>
            /// <value>The pod letter.</value>
            public PODLetter? PODLetter { get; set; }

            /// <summary>Gets or sets the electronic delivery notification.</summary>
            /// <value>The electronic delivery notification.</value>
            public ElectronicDeliveryNotification? ElectronicDeliveryNotification { get; set; }
        }

        /// <summary>(Serializable)a address artifact format.</summary>
        [Serializable]
        public class AddressArtifactFormat
        {
            /// <summary>Gets or sets the street number low.</summary>
            /// <value>The street number low.</value>
            public string? StreetNumberLow { get; set; }

            /// <summary>Gets or sets the street prefix.</summary>
            /// <value>The street prefix.</value>
            public string? StreetPrefix { get; set; }

            /// <summary>Gets or sets the name of the street.</summary>
            /// <value>The name of the street.</value>
            public string? StreetName { get; set; }

            /// <summary>Gets or sets the street suffix.</summary>
            /// <value>The street suffix.</value>
            public string? StreetSuffix { get; set; }

            /// <summary>Gets or sets the type of the street.</summary>
            /// <value>The type of the street.</value>
            public string? StreetType { get; set; }

            /// <summary>Gets or sets the political division 2.</summary>
            /// <value>The political division 2.</value>
            public string? PoliticalDivision2 { get; set; }

            /// <summary>Gets or sets the political division 1.</summary>
            /// <value>The political division 1.</value>
            public string? PoliticalDivision1 { get; set; }

            /// <summary>Gets or sets the postcode primary low.</summary>
            /// <value>The postcode primary low.</value>
            public string? PostcodePrimaryLow { get; set; }

            /// <summary>Gets or sets the country code.</summary>
            /// <value>The country code.</value>
            public string? CountryCode { get; set; }
        }

        /// <summary>(Serializable)a transport facility.</summary>
        [Serializable]
        public class TransportFacility
        {
            /// <summary>Gets or sets the type.</summary>
            /// <value>The type.</value>
            public string? Type { get; set; }

            /// <summary>Gets or sets the code.</summary>
            /// <value>The code.</value>
            public string? Code { get; set; }
        }

        /// <summary>(Serializable)a pod letter.</summary>
        [Serializable]
        public class PODLetter
        {
            /// <summary>Gets or sets the HTML image.</summary>
            /// <value>The HTML image.</value>
            public string? HTMLImage { get; set; }
        }

        /// <summary>(Serializable)a electronic delivery notification.</summary>
        [Serializable]
        public class ElectronicDeliveryNotification
        {
            /// <summary>Gets or sets the name.</summary>
            /// <value>The name.</value>
            public string? Name { get; set; }
        }

        /// <summary>(Serializable)a status.</summary>
        [Serializable]
        public class Status
        {
            /// <summary>Gets or sets the type of the status.</summary>
            /// <value>The type of the status.</value>
            public CodeDescription? StatusType { get; set; }

            /// <summary>Gets or sets the status code.</summary>
            /// <value>The status code.</value>
            public StatusCode? StatusCode { get; set; }
        }

        /// <summary>(Serializable)a status code.</summary>
        [Serializable]
        public class StatusCode
        {
            /// <summary>Gets or sets the code.</summary>
            /// <value>The code.</value>
            public string? Code { get; set; }
        }

        /// <summary>(Serializable)a code value.</summary>
        [Serializable]
        public class CodeValue
        {
            /// <summary>Gets or sets the code.</summary>
            /// <value>The code.</value>
            public string? Code { get; set; }

            /// <summary>Gets or sets the value.</summary>
            /// <value>The value.</value>
            public string? Value { get; set; }
        }
    }
}
