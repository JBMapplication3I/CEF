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
    /// A model for return adjustments.
    /// </summary>
    public class FilingAdjustmentModel
    {
        /// <summary>
        /// The unique ID number for the adjustment.
        /// </summary>
        public long? id { get; set; }

        /// <summary>
        /// The filing return id that this applies too
        /// </summary>
        public long? filingId { get; set; }

        /// <summary>
        /// The adjustment amount.
        /// </summary>
        public decimal amount { get; set; }

        /// <summary>
        /// The filing period the adjustment is applied to.
        /// </summary>
        public AdjustmentPeriodTypeId period { get; set; }

        /// <summary>
        /// The type of the adjustment.
        /// </summary>
        public string? type { get; set; }

        /// <summary>
        /// Whether or not the adjustment has been calculated.
        /// </summary>
        public bool? isCalculated { get; set; }

        /// <summary>
        /// The account type of the adjustment.
        /// </summary>
        public PaymentAccountTypeId accountType { get; set; }

        /// <summary>
        /// A descriptive reason for creating this adjustment.
        /// </summary>
        public string? reason { get; set; }

        /// <summary>
        /// The date when this record was created.
        /// </summary>
        public DateTime? createdDate { get; set; }

        /// <summary>
        /// The User ID of the user who created this record.
        /// </summary>
        public int? createdUserId { get; set; }

        /// <summary>
        /// The date/time when this record was last modified.
        /// </summary>
        public DateTime? modifiedDate { get; set; }

        /// <summary>
        /// The user ID of the user who last modified this record.
        /// </summary>
        public int? modifiedUserId { get; set; }

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
