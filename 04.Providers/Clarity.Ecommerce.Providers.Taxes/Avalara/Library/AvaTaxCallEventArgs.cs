namespace Avalara.AvaTax.RestClient
{
    using System;
    using System.Net;

    /// <summary>Information about an API call.</summary>
    /// <seealso cref="EventArgs"/>
    public class AvaTaxCallEventArgs : EventArgs
    {
        /// <summary>The HTTP verb that was used for the API call.</summary>
        /// <value>The HTTP verb.</value>
        public string? HttpVerb { get; set; }

        /// <summary>The full request URI that was sent.</summary>
        /// <value>The request URI.</value>
        public Uri? RequestUri { get; set; }

        /// <summary>The response code for this request.</summary>
        /// <value>The code.</value>
        public HttpStatusCode Code { get; set; }

        /// <summary>The body of the request that was sent.</summary>
        /// <value>The request body.</value>
        public string? RequestBody { get; set; }

        /// <summary>The response body, if the response was received as a string.</summary>
        /// <value>The response string.</value>
        public string? ResponseString { get; set; }

        /// <summary>The response body, if the response was received as a file download attachment.</summary>
        /// <value>The response body.</value>
        public byte[]? ResponseBody { get; set; }

        /// <summary>Information about the length of time this API call took to complete.</summary>
        /// <value>The duration.</value>
        public CallDuration? Duration { get; set; }
    }
}
