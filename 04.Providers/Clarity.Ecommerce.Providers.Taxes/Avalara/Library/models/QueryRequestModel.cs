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
    /// Represents a complex query request to parse using query filter guidelines from Microsoft REST standards
    /// </summary>
    public class QueryRequestModel
    {
        /// <summary>
        /// A list of conditions to filter objects.
        /// </summary>
        public string? filter { get; set; }

        /// <summary>
        /// A list of included commands for this fetch operation.
        /// </summary>
        public string? include { get; set; }

        /// <summary>
        /// For pagination: This is the maximum number of results to return.
        /// </summary>
        public int? maxResults { get; set; }

        /// <summary>
        /// For pagination: This is the index of the first result.
        /// </summary>
        public int? startIndex { get; set; }

        /// <summary>
        /// Sorts the resulting objects in a specific manner.
        /// </summary>
        public string? sortBy { get; set; }

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
