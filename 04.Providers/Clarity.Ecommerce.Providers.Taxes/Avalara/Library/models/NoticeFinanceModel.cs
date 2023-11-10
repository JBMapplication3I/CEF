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
    /// Represents estimated financial results from responding to a tax notice.
    /// </summary>
    public class NoticeFinanceModel
    {
        /// <summary>
        /// The Unique Id of the Finance Model
        /// </summary>
        public int? id { get; set; }

        /// <summary>
        /// The unique ID of the the tax notice associated with the the finance detail
        /// </summary>
        public int? noticeId { get; set; }

        /// <summary>
        /// The date of the notice
        /// </summary>
        public DateTime? noticeDate { get; set; }

        /// <summary>
        /// The due date of the notice
        /// </summary>
        public DateTime? dueDate { get; set; }

        /// <summary>
        /// The sequential number of the notice
        /// </summary>
        public string? noticeNumber { get; set; }

        /// <summary>
        /// The amount of tax due on the notice
        /// </summary>
        public decimal? taxDue { get; set; }

        /// <summary>
        /// The amound of penalty listed on the notice
        /// </summary>
        public decimal? penalty { get; set; }

        /// <summary>
        /// The amount of interest listed on the notice
        /// </summary>
        public decimal? interest { get; set; }

        /// <summary>
        /// The amount of credits listed on the notice
        /// </summary>
        public decimal? credits { get; set; }

        /// <summary>
        /// The amount of tax abated on the notice
        /// </summary>
        public decimal? taxAbated { get; set; }

        /// <summary>
        /// The amount of customer penalty on the notice
        /// </summary>
        public decimal? customerPenalty { get; set; }

        /// <summary>
        /// The amount of customer interest on the notice
        /// </summary>
        public decimal? customerInterest { get; set; }

        /// <summary>
        /// The amount of CSP Fee Refund on the notice
        /// </summary>
        public decimal? cspFeeRefund { get; set; }

        /// <summary>
        /// The name of the file attached to the finance detail
        /// </summary>
        public string? fileName { get; set; }

        /// <summary>
        /// The ResourceFileId of the finance detail attachment
        /// </summary>
        public long? resourceFileId { get; set; }

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
        /// An attachment to the finance detail
        /// </summary>
        public ResourceFileUploadRequestModel? attachmentUploadRequest { get; set; }

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
