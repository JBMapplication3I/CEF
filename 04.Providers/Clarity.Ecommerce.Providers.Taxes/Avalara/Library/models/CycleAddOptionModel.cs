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
    /// Model with options for adding a new filing calendar
    /// </summary>
    public class CycleAddOptionModel
    {
        /// <summary>
        /// True if this form can be added and filed for the current cycle. "Current cycle" is considered one month before the month of today's date.
        /// </summary>
        public bool? available { get; set; }

        /// <summary>
        /// The period start date for the customer's first transaction in the jurisdiction being added
        /// </summary>
        public DateTime? transactionalPeriodStart { get; set; }

        /// <summary>
        /// The period end date for the customer's last transaction in the jurisdiction being added
        /// </summary>
        public DateTime? transactionalPeriodEnd { get; set; }

        /// <summary>
        /// The jurisdiction-assigned due date for the form
        /// </summary>
        public DateTime? filingDueDate { get; set; }

        /// <summary>
        /// A descriptive name of the cycle and due date of form.
        /// </summary>
        public string? cycleName { get; set; }

        /// <summary>
        /// The filing frequency of the form
        /// </summary>
        public string? frequencyName { get; set; }

        /// <summary>
        /// A code assigned to the filing frequency
        /// </summary>
        public string? filingFrequencyCode { get; set; }

        /// <summary>
        /// The filing frequency of the request
        /// </summary>
        public FilingFrequencyId? filingFrequencyId { get; set; }

        /// <summary>
        /// An explanation for why this form cannot be added for the current cycle
        /// </summary>
        public string? cycleUnavailableReason { get; set; }

        /// <summary>
        /// A list of outlet codes that can be assigned to this form for the current cycle
        /// </summary>
        public List<string>? availableLocationCodes { get; set; }

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
