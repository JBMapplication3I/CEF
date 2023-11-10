using System;
using System.Collections.Generic;
using Newtonsoft.Json;

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
    /// <summary>
    /// Model for Advanced Rules when full details are requested
    /// </summary>
    public class AdvancedRuleFullDetailsModel
    {
        /// <summary>
        /// The code script for the rule
        /// </summary>
        public string script { get; set; }

        /// <summary>
        /// Script run for validating customer data
        /// </summary>
        public string customerDataValidatorScript { get; set; }

        /// <summary>
        /// Has the rule been approved
        /// </summary>
        public bool? isApproved { get; set; }

        /// <summary>
        /// Creator of the rule
        /// </summary>
        public string createdBy { get; set; }

        /// <summary>
        /// When the rule was created
        /// </summary>
        public string createdOn { get; set; }

        /// <summary>
        /// Last updater of the rule
        /// </summary>
        public string modifiedBy { get; set; }

        /// <summary>
        /// When the rule was last updated
        /// </summary>
        public string modifiedOn { get; set; }

        /// <summary>
        /// Approver of the rule
        /// </summary>
        public string approvedBy { get; set; }

        /// <summary>
        /// Is this a system rule as opposed to customer-facing
        /// </summary>
        public bool? isSystemRule { get; set; }

        /// <summary>
        /// Is the rule displayed in the CUP UI
        /// </summary>
        public bool? isVisibleInCUP { get; set; }

        /// <summary>
        /// Is this a rule created for testing
        /// </summary>
        public bool? isTest { get; set; }

        /// <summary>
        /// The JSON schema for customer data if it is required for the rule
        /// </summary>
        public string customerDataSchema { get; set; }

        /// <summary>
        /// The version of the rule
        /// </summary>
        public int? version { get; set; }

        /// <summary>
        /// Unique identifier for a rule
        /// </summary>
        public string ruleId { get; set; }

        /// <summary>
        /// Rule name
        /// </summary>
        public string name { get; set; }

        /// <summary>
        /// Description of the rule
        /// </summary>
        public string description { get; set; }

        /// <summary>
        /// Convert this object to a JSON string of itself
        /// </summary>
        /// <returns>A JSON string of this object</returns>
        public override string ToString()
        {
            return JsonConvert.SerializeObject(this, new JsonSerializerSettings() { Formatting = Formatting.Indented });
        }
    }
}
