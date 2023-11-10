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
    /// Ping Result Model
    /// </summary>
    public class PingResultModel
    {
        /// <summary>
        /// Version number
        /// </summary>
        public string? version { get; set; }

        /// <summary>
        /// Returns true if you provided authentication for this API call; false if you did not.
        /// </summary>
        public bool? authenticated { get; set; }

        /// <summary>
        /// Returns the type of authentication you provided, if authenticated
        /// </summary>
        public AuthenticationTypeId? authenticationType { get; set; }

        /// <summary>
        /// The username of the currently authenticated user, if any.
        /// </summary>
        public string? authenticatedUserName { get; set; }

        /// <summary>
        /// The ID number of the currently authenticated user, if any.
        /// </summary>
        public int? authenticatedUserId { get; set; }

        /// <summary>
        /// The ID number of the currently authenticated user's account, if any.
        /// </summary>
        public int? authenticatedAccountId { get; set; }

        /// <summary>
        /// The connected Salesforce account.
        /// </summary>
        public string? crmid { get; set; }

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
