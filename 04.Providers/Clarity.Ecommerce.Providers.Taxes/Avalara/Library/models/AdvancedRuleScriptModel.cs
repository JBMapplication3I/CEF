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
    /// Represents an advanced rule script
    /// </summary>
    public class AdvancedRuleScriptModel
    {
        /// <summary>
        /// The unique ID of the script
        /// </summary>
        public long id { get; set; }

        /// <summary>
        /// Account ID
        /// </summary>
        public int? accountId { get; set; }

        /// <summary>
        /// How to proceed if the rule crashes
        /// </summary>
        public AdvancedRuleCrashBehavior? crashBehavior { get; set; }

        /// <summary>
        /// The type of script - request or response
        /// </summary>
        public AdvancedRuleScriptType? scriptType { get; set; }

        /// <summary>
        /// The JavaScript rule
        /// </summary>
        public string? script { get; set; }

        /// <summary>
        /// The rule has been approved
        /// </summary>
        public bool? isApproved { get; set; }

        /// <summary>
        /// The rule has been disabled
        /// </summary>
        public bool? isDisabled { get; set; }

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
