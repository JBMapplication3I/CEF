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
    /// NoticeResponsibility Model
    /// </summary>
    public class NoticeResponsibilityModel
    {
        /// <summary>
        /// The unique ID number of this notice responsibility.
        /// </summary>
        public int? id { get; set; }

        /// <summary>
        /// The description name of this notice responsibility
        /// </summary>
        public string? description { get; set; }

        /// <summary>
        /// Defines if the responsibility is active
        /// </summary>
        public bool? isActive { get; set; }

        /// <summary>
        /// The sort order of this responsibility
        /// </summary>
        public int? sortOrder { get; set; }

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
