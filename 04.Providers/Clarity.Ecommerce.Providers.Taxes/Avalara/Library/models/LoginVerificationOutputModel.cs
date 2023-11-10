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
    /// This is the output model coming from skyscraper services
    /// </summary>
    public class LoginVerificationOutputModel
    {
        /// <summary>
        /// The job Id returned from skyscraper
        /// </summary>
        public int jobId { get; set; }

        /// <summary>
        /// The operation status of the job
        /// </summary>
        public string? operationStatus { get; set; }

        /// <summary>
        /// The message returned from the job
        /// </summary>
        public string? message { get; set; }

        /// <summary>
        /// Indicates if the login was successful
        /// </summary>
        public bool? loginSuccess { get; set; }

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
