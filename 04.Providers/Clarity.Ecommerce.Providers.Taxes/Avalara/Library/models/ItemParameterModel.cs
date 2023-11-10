﻿/*
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
    /// Represents a parameter associated with an item.
    /// </summary>
    public class ItemParameterModel
    {
        /// <summary>
        /// The id of the parameter.
        /// </summary>
        public long? id { get; set; }

        /// <summary>
        /// The parameter's name.
        /// </summary>
        public string? name { get; set; }

        /// <summary>
        /// The value for the parameter.
        /// </summary>
        public string? value { get; set; }

        /// <summary>
        /// The unit of measurement code for the parameter.
        /// </summary>
        public string? unit { get; set; }

        /// <summary>
        /// The item id
        /// </summary>
        public long? itemId { get; set; }

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
