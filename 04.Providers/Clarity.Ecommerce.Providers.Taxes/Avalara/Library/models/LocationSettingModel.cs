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
    /// Represents the answer to one local jurisdiction question for a location.
    /// </summary>
    public class LocationSettingModel
    {
        /// <summary>
        /// The unique ID number of the location question answered.
        /// </summary>
        public int? questionId { get; set; }

        /// <summary>
        /// The name of the question
        /// </summary>
        public string? questionName { get; set; }

        /// <summary>
        /// The answer the user provided.
        /// </summary>
        public string? value { get; set; }

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
