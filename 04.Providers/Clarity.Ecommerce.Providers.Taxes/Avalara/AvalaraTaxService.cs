// <copyright file="AvalaraTaxService.cs" company="clarity-ventures.com">
// Copyright (c) 2017-2023 clarity-ventures.com. All rights reserved.
// </copyright>
// <summary>Implements the avalara tax service class</summary>
namespace Clarity.Ecommerce.Providers.Taxes.AvalaraInt
{
    using System;
    using System.IO;
    using System.Net;
    using System.Text;
    using System.Xml;
    using System.Xml.Serialization;
    using Models;
    using Utilities;

    /// <summary>An avalara tax service.</summary>
    public class AvalaraTaxService
    {
        private static string? accountNum;
        private static string? license;
        private static string? svcURL;

        /// <summary>Initializes a new instance of the <see cref="AvalaraTaxService"/> class.</summary>
        /// <exception cref="ArgumentNullException">Thrown when a method Contract has been broken.</exception>
        /// <param name="accountNumber">The account number.</param>
        /// <param name="licenseKey">   The license key.</param>
        /// <param name="serviceURL">   URL of the service.</param>
        public AvalaraTaxService(string accountNumber, string licenseKey, string serviceURL)
        {
            Contract.RequiresNotNull(serviceURL);
            accountNum = accountNumber;
            license = licenseKey;
            svcURL = serviceURL.TrimEnd('/') + "/1.0/";
        }

        /// <summary>This actually calls the service to perform the tax calculation, and returns the calculation result.</summary>
        /// <exception cref="ArgumentNullException">Thrown when a method Contract has been broken.</exception>
        /// <param name="req">The request.</param>
        /// <returns>The tax.</returns>
        public static GetTaxResult GetTax(GetTaxRequest req)
        {
            Contract.RequiresNotNull(req);
            // ConvertToCartItemModel the request to XML
            var namesp = new XmlSerializerNamespaces();
            namesp.Add(string.Empty, string.Empty);
            var settings = new XmlWriterSettings { OmitXmlDeclaration = true };
            var x = new XmlSerializer(req.GetType());
            var sb = new StringBuilder();
            using var writer = XmlWriter.Create(sb, settings);
            x.Serialize(writer, req, namesp);
            var doc = new XmlDocument();
            ////var test = "<GetTaxRequest><DocDate>2017-05-18</DocDate><CustomerCode>C-1075</CustomerCode><Addresses><Address><AddressCode>DEST-01</AddressCode><Line1>345 N Lake Blvd</Line1><Line2 /><City>Tahoe City</City><Region>TX</Region><PostalCode>96145</PostalCode><Country>US</Country><AddressType p4:nil=\"true\" xmlns:p4=\"http://www.w3.org/2001/XMLSchema-instance\" /><Latitude p4:nil=\"true\" xmlns:p4=\"http://www.w3.org/2001/XMLSchema-instance\" /><Longitude p4:nil=\"true\" xmlns:p4=\"http://www.w3.org/2001/XMLSchema-instance\" /></Address><Address><AddressCode>1</AddressCode><Line1>187 Sgt. Prentiss Drive</Line1><City> Natchez</City><PostalCode>39120</PostalCode><Country>US</Country><AddressType p4:nil=\"true\" xmlns:p4=\"http://www.w3.org/2001/XMLSchema-instance\" /><Latitude p4:nil=\"true\" xmlns:p4=\"http://www.w3.org/2001/XMLSchema-instance\" /><Longitude p4:nil=\"true\" xmlns:p4=\"http://www.w3.org/2001/XMLSchema-instance\" /></Address></Addresses><Lines><Line><LineNo>23540</LineNo><DestinationCode>DEST-01</DestinationCode><OriginCode>1</OriginCode><ItemCode>3434990</ItemCode><Qty>1.0000</Qty><Amount>3.990000</Amount><TaxCode /><Description>Amerock Inspirations BP1586G10 3-Ring Round Cabinet Knob, 25.4 mm Projection, 1-3/8 in Dia</Description><Discounted>false</Discounted><TaxIncluded>false</TaxIncluded></Line><Line p3:nil=\"true\" xmlns:p3=\"http://www.w3.org/2001/XMLSchema-instance\" /></Lines><Client>a0o330000048hNX</Client><DocCode>DOC-170518-1075</DocCode><DocType>SalesOrder</DocType><CompanyCode>HHC</CompanyCode><Commit>false</Commit><DetailLevel>Tax</DetailLevel><CustomerUsageType /><ExemptionNo /><Discount>0</Discount><LocationCode>1</LocationCode></GetTaxRequest>";
            doc.LoadXml(sb.ToString());
            // Call the service
            var address = new Uri(svcURL + "tax/get");
            var request = (HttpWebRequest)WebRequest.Create(address);
            ////request.Headers.Add(HttpRequestHeader.Authorization, "Basic " + Convert.ToBase64String(Encoding.ASCII.GetBytes("1100110323" + ":" + "2A1888D69B185758")));
            request.Headers.Add(HttpRequestHeader.Authorization, "Basic " + Convert.ToBase64String(Encoding.ASCII.GetBytes(accountNum + ":" + license)));
            request.Method = "POST";
            request.ContentType = "text/xml";
            request.ContentLength = sb.Length;
            var newStream = request.GetRequestStream();
            newStream.Write(Encoding.ASCII.GetBytes(sb.ToString()), 0, sb.Length);
            var result = new GetTaxResult();
            try
            {
                var response = request.GetResponse();
                var r = new XmlSerializer(result.GetType());
                // ReSharper disable once AssignNullToNotNullAttribute
                result = (GetTaxResult)r.Deserialize(response.GetResponseStream())!;
            }
            catch (WebException ex)
            {
                var r = new XmlSerializer(result.GetType());
                // ReSharper disable once AssignNullToNotNullAttribute
                result = (GetTaxResult)r.Deserialize(((HttpWebResponse)ex.Response!).GetResponseStream())!;
            }
            return result;
        }

        /// <summary>Estimate tax.</summary>
        /// <param name="latitude">  The latitude.</param>
        /// <param name="longitude"> The longitude.</param>
        /// <param name="saleAmount">The sale amount.</param>
        /// <returns>A GeoTaxResult.</returns>
        public static GeoTaxResult EstimateTax(decimal latitude, decimal longitude, decimal saleAmount)
        {
            // Call the service
            var address = new Uri(svcURL + "tax/" + latitude + "," + longitude + "/get.xml?saleamount=" + saleAmount);
            var request = (HttpWebRequest)WebRequest.Create(address);
            request.Headers.Add(HttpRequestHeader.Authorization, "Basic " + Convert.ToBase64String(Encoding.ASCII.GetBytes(accountNum + ":" + license)));
            request.Method = "GET";
            var result = new GeoTaxResult();
            try
            {
                var response = request.GetResponse();
                var r = new XmlSerializer(result.GetType());
                // ReSharper disable once AssignNullToNotNullAttribute
                result = (GeoTaxResult)r.Deserialize(response.GetResponseStream())!;
            }
            catch (WebException ex)
            {
                var responseStream = ((HttpWebResponse)ex.Response!).GetResponseStream();
                // ReSharper disable once AssignNullToNotNullAttribute
                var reader = new StreamReader(responseStream);
                var responseString = reader.ReadToEnd();
                // The service returns some error messages in JSON for authentication/unhandled errors.
                if (responseString.StartsWith("{") || responseString.StartsWith("["))
                {
                    result = new() { ResultCode = SeverityLevel.Error };
                    var msg = new Message
                    {
                        Severity = result.ResultCode,
                        Summary = "The request was unable to be successfully serviced, please try again or contact Customer Service.",
                        Source = "Avalara.Web.REST",
                    };
                    if (!((HttpWebResponse)ex.Response).StatusCode.Equals(HttpStatusCode.InternalServerError))
                    {
                        msg.Summary = "The user or account could not be authenticated.";
                        msg.Source = "Avalara.Web.Authorization";
                    }
                    result.Messages = new[] { msg };
                }
                else
                {
                    var r = new XmlSerializer(result.GetType());
                    var temp = Encoding.ASCII.GetBytes(responseString);
                    var stream = new MemoryStream(temp);
                    result = (GeoTaxResult)r.Deserialize(stream)!; // Inelegant, but the deserializer only takes streams, and we already read ours out.
                }
            }
            return result;
        }

        /// <summary>Gets the ping.</summary>
        /// <returns>A GeoTaxResult.</returns>
        public static GeoTaxResult Ping()
        {
            return EstimateTax(47.627935m, -122.51702m, 10m);
        }

        /// <summary>This calls CancelTax to void a transaction specified in tax request.</summary>
        /// <exception cref="ArgumentNullException">Thrown when a method Contract has been broken.</exception>
        /// <param name="cancelTaxRequest">The cancel tax request.</param>
        /// <returns>A CancelTaxResult.</returns>
        public static CancelTaxResult? CancelTax(CancelTaxRequest cancelTaxRequest)
        {
            Contract.RequiresNotNull(cancelTaxRequest);
            // ConvertToCartItemModel the request to XML
            var namesp = new XmlSerializerNamespaces();
            namesp.Add(string.Empty, string.Empty);
            var settings = new XmlWriterSettings { OmitXmlDeclaration = true };
            var x = new XmlSerializer(cancelTaxRequest.GetType());
            var sb = new StringBuilder();
            using var writer = XmlWriter.Create(sb, settings);
            x.Serialize(writer, cancelTaxRequest, namesp);
            var doc = new XmlDocument();
            doc.LoadXml(sb.ToString());
            // Call the service
            var address = new Uri(svcURL + "tax/cancel");
            var request = (HttpWebRequest)WebRequest.Create(address);
            request.Headers.Add(HttpRequestHeader.Authorization, "Basic " + Convert.ToBase64String(Encoding.ASCII.GetBytes(accountNum + ":" + license)));
            request.Method = "POST";
            request.ContentType = "text/xml";
            request.ContentLength = sb.Length;
            var newStream = request.GetRequestStream();
            newStream.Write(Encoding.ASCII.GetBytes(sb.ToString()), 0, sb.Length);
            var cancelResponse = new CancelTaxResponse();
            try
            {
                var response = request.GetResponse();
                var r = new XmlSerializer(cancelResponse.GetType());
                // ReSharper disable once AssignNullToNotNullAttribute
                cancelResponse = (CancelTaxResponse)r.Deserialize(response.GetResponseStream())!;
            }
            catch (WebException ex)
            {
                var r = new XmlSerializer(cancelResponse.GetType());
                // ReSharper disable once AssignNullToNotNullAttribute
                cancelResponse = (CancelTaxResponse)r.Deserialize(((HttpWebResponse)ex.Response!).GetResponseStream())!;
                // If the error is returned at the cancelResponse level, translate it to the cancelResult.
                if (cancelResponse.ResultCode.Equals(SeverityLevel.Error))
                {
                    cancelResponse.CancelTaxResult = new()
                    {
                        ResultCode = cancelResponse.ResultCode,
                        Messages = cancelResponse.Messages,
                    };
                }
            }
            return cancelResponse.CancelTaxResult;
        }
    }
}
