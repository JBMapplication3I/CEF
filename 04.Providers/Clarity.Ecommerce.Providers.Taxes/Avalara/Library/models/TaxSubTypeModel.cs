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
    /// Represents a tax subtype
    /// </summary>
    public class TaxSubTypeModel
    {
        /// <summary>
        /// The unique ID number of this tax sub-type.
        /// </summary>
        public int id { get; set; }

        /// <summary>
        /// The unique human readable Id of this tax sub-type.
        /// </summary>
        public string? taxSubType { get; set; }

        /// <summary>
        /// The description of this tax sub-type.
        /// </summary>
        public string? description { get; set; }

        /// <summary>
        /// The upper level group of tax types.
        /// </summary>
        public string? taxTypeGroup { get; set; }

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
