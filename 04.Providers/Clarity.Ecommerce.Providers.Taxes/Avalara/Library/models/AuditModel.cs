/*
 * AvaTax API Client Library
 *
 * (c) 2004-2019 Avalara, Inc.
 *
 * For the full copyright and license information, please view the LICENSE
 * file that was distributed with this source code.
 *
 * @author Genevieve Conty
 * @author Greg Hester
 */

namespace Avalara.AvaTax.RestClient
{
    using System;
    using System.Collections.Generic;
    using Newtonsoft.Json;

    /// <summary>
    /// Provides detailed information about an API call.
    ///  
    /// The information on this record was captured by AvaTax when your API call was made. If you are unsure why you
    /// received an error, you can fetch these audit objects and examine the `RequestUrl`, `RequestBody`, and `ErrorMessage`
    /// fields to determine root cause for the error.
    /// </summary>
    public class AuditModel
    {
        /// <summary>
        /// A unique ID number referring to this individual API call.
        /// </summary>
        public long? transactionId { get; set; }

        /// <summary>
        /// The unique ID number of the account to which the user belongs.
        /// </summary>
        public int? accountId { get; set; }

        /// <summary>
        /// The unique ID number of the user that performed this API call.
        /// </summary>
        public int? userId { get; set; }

        /// <summary>
        /// The origin IP address from which AvaTax received this API call. If you use a proxy layer or other
        /// gateway, this IP address may be the address of the gateway. This information is not guaranteed
        /// to be accurate and may change based on network conditions between your site and AvaTax.
        /// </summary>
        public string? ipAddress { get; set; }

        /// <summary>
        /// If your API call specified a `MachineName` in the [Client Profile Headers](https://developer.avalara.com/avatax/client-headers/), this
        /// variable will contain its value. This information is self-reported by the client and is not guaranteed to be present.
        /// </summary>
        public string? machineName { get; set; }

        /// <summary>
        /// If your API call specified a `ClientName` in the [Client Profile Headers](https://developer.avalara.com/avatax/client-headers/), this
        /// variable will contain its value. This information is self-reported by the client and is not guaranteed to be present.
        /// </summary>
        public string? clientName { get; set; }

        /// <summary>
        /// If your API call specified a `ClientVersion` in the [Client Profile Headers](https://developer.avalara.com/avatax/client-headers/), this
        /// variable will contain its value. This information is self-reported by the client and is not guaranteed to be present.
        /// </summary>
        public string? clientVersion { get; set; }

        /// <summary>
        /// If your API call specified a `AdapterName` in the [Client Profile Headers](https://developer.avalara.com/avatax/client-headers/), this
        /// variable will contain its value. This information is self-reported by the client and is not guaranteed to be present.
        /// </summary>
        public string? adapterName { get; set; }

        /// <summary>
        /// If your API call specified a `AdapterVersion` in the [Client Profile Headers](https://developer.avalara.com/avatax/client-headers/), this
        /// variable will contain its value. This information is self-reported by the client and is not guaranteed to be present.
        /// </summary>
        public string? adapterVersion { get; set; }

        /// <summary>
        /// The server name of the AvaTax server that responded to this API call.
        /// </summary>
        public string? serverName { get; set; }

        /// <summary>
        /// The software version number of the currently deployed AvaTax API software on the server that responded to this API call.
        /// </summary>
        public string? serverVersion { get; set; }

        /// <summary>
        /// A context-dependent reference ID for this API call. This reference ID is not guaranteed to contain a specific value
        /// and may be used differently by various API calls.
        /// </summary>
        public long? referenceId { get; set; }

        /// <summary>
        /// If the API contained a specific type of error code, this value would contain the unique ID number of the severity level
        /// of the response returned to the client.
        /// </summary>
        public int? severityLevelId { get; set; }

        /// <summary>
        /// The server timestamp, in UTC, of the time when the server received this API call.
        /// </summary>
        public DateTime? serverTimestamp { get; set; }

        /// <summary>
        /// The number of milliseconds that the AvaTax server took to respond to this API call.
        /// </summary>
        public int? serverDuration { get; set; }

        /// <summary>
        /// The service name of the AvaTax API service that responded to this API call.
        /// </summary>
        public string? serviceName { get; set; }

        /// <summary>
        /// The operation name of this API call.
        /// </summary>
        public string? operation { get; set; }

        /// <summary>
        /// A context-dependent reference code for this API call. This reference code is not guaranteed to contain a specific value
        /// and may be used differently by various API calls.
        /// </summary>
        public string? referenceCode { get; set; }

        /// <summary>
        /// If this API call reported an error, this contains the name of the error that was returned. You can look up more
        /// information about AvaTax error messages on the [AvaTax REST Error Page](https://developer.avalara.com/avatax/errors/).
        /// </summary>
        public string? errorMessage { get; set; }

        /// <summary>
        /// Reserved for Avalara internal usage.
        /// </summary>
        public string? auditMessage { get; set; }

        /// <summary>
        /// Reserved for Avalara internal usage.
        /// </summary>
        public int? loadBalancerDuration { get; set; }

        /// <summary>
        /// If this API call returned an array of information, this value contains the number of records returned.
        /// </summary>
        public int? recordCount { get; set; }

        /// <summary>
        /// Reserved for Avalara internal usage.
        /// </summary>
        public string? referenceAuthorization { get; set; }

        /// <summary>
        /// Reserved for Avalara internal usage.
        /// </summary>
        public bool? isQueued { get; set; }

        /// <summary>
        /// If this API call included requests made to any of the AvaTax data layers, this contains the number of requests that were traced.
        ///  
        /// Please note that not all data layers support this measurement.
        /// </summary>
        public int? databaseCallCount { get; set; }

        /// <summary>
        /// If this API call included requests made to any of the AvaTax data layers, this contains the total duration time measured for all the requests.
        ///  
        /// Please note that not all data layers support this measurement.
        /// </summary>
        public string? databaseCallDuration { get; set; }

        /// <summary>
        /// If this API call included interoperation with other Avalara services, this contains the amount of time taken by those services.
        /// </summary>
        public string? remoteCallDuration { get; set; }

        /// <summary>
        /// Reserved for Avalara internal usage.
        /// </summary>
        public List<AuditEvent>? events { get; set; }

        /// <summary>
        /// The original request URL as provided by the client.
        /// </summary>
        public string? requestUrl { get; set; }

        /// <summary>
        /// If this request was an HTTP request that included a body such as a POST or a PUT, this will contain the request body sent by the client.
        ///  
        /// This request body is represented as a string, exactly as it was received from the client. Regardless of whether the request was JSON,
        /// Base64 encoded bytes, or a CSV file, this contains the exact contents of the request body.
        /// </summary>
        public string? requestBody { get; set; }

        /// <summary>
        /// The HTTP response code that was sent by the server.
        /// </summary>
        public int? responseStatus { get; set; }

        /// <summary>
        /// The entire response body sent from the AvaTax server to the client. This value is returned as a string regardless of whether the results
        /// were JSON-formatted text, CSV files, or raw strings.
        /// </summary>
        public string? responseBody { get; set; }

        /// <summary>
        /// Reserved for Avalara internal usage.
        /// </summary>
        public List<AuditModel>? remoteCalls { get; set; }

        /// <summary>
        /// Convert this object to a JSON string of itself
        /// </summary>
        /// <returns>A JSON string of this object.</returns>
        public override string ToString()
        {
            return JsonConvert.SerializeObject(this, new JsonSerializerSettings() { Formatting = Formatting.Indented });
        }
    }
}
