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
    /// Account Linkage output model
    /// </summary>
    public class FirmClientLinkageOutputModel
    {
        /// <summary>
        /// The unique ID number of firm-client linkage.
        /// </summary>
        public int? id { get; set; }

        /// <summary>
        /// Firm Account to be linked with the firm
        /// </summary>
        public int? firmAccountId { get; set; }

        /// <summary>
        /// FIrm Account name
        /// </summary>
        public string firmAccountName { get; set; }

        /// <summary>
        /// Client Account to be linked with the firm
        /// </summary>
        public int? clientAccountId { get; set; }

        /// <summary>
        /// Client Account name
        /// </summary>
        public string clientAccountName { get; set; }

        /// <summary>
        /// Created date of the linkage
        /// </summary>
        public DateTime? createdDate { get; set; }

        /// <summary>
        /// User who created the linkage
        /// </summary>
        public int? createdUserId { get; set; }

        /// <summary>
        /// Modified date of the linkage
        /// </summary>
        public DateTime? modifiedDate { get; set; }

        /// <summary>
        /// User who modified the linkage
        /// </summary>
        public int? modifiedUserId { get; set; }

        /// <summary>
        /// The status of the account linkage. The following are the available statuses
        /// * Requested - When a linkage is requested
        /// * Approved - When the linkage is approved
        /// * Rejected - When the linkage is rejected
        /// * Revoked - When the linkage is revoked.
        /// </summary>
        public FirmClientLinkageStatus? status { get; set; }

        /// <summary>
        /// This is set to 1 if the linkage is deleted.
        /// </summary>
        public bool? isDeleted { get; set; }

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
