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
    /// Tax Notice Type Model
    /// </summary>
    public class NoticeTypeModel
    {
        /// <summary>
        /// The unique ID number of this tax notice customer type.
        /// </summary>
        public int id { get; set; }

        /// <summary>
        /// The description name of this tax authority type.
        /// </summary>
        public string? description { get; set; }

        /// <summary>
        /// A flag if the type is active
        /// </summary>
        public bool? activeFlag { get; set; }

        /// <summary>
        /// sort order of the types
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
