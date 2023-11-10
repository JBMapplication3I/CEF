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
    /// Represents a batch of uploaded documents.
    /// </summary>
    public class BatchModel
    {
        /// <summary>
        /// The unique ID number of this batch.
        /// </summary>
        public int? id { get; set; }

        /// <summary>
        /// The user-friendly readable name for this batch.
        /// </summary>
        public string? name { get; set; }

        /// <summary>
        /// The Account ID number of the account that owns this batch.
        /// </summary>
        public int? accountId { get; set; }

        /// <summary>
        /// The Company ID number of the company that owns this batch.
        /// </summary>
        public int? companyId { get; set; }

        /// <summary>
        /// The type of this batch.
        /// </summary>
        public BatchType type { get; set; }

        /// <summary>
        /// This batch's current processing status
        /// </summary>
        public BatchStatus? status { get; set; }

        /// <summary>
        /// Any optional flags provided for this batch
        /// </summary>
        public string? options { get; set; }

        /// <summary>
        /// The agent used to create this batch
        /// </summary>
        public string? batchAgent { get; set; }

        /// <summary>
        /// The date/time when this batch started processing
        /// </summary>
        public DateTime? startedDate { get; set; }

        /// <summary>
        /// The number of records in this batch; determined by the server
        /// </summary>
        public int? recordCount { get; set; }

        /// <summary>
        /// The current record being processed
        /// </summary>
        public int? currentRecord { get; set; }

        /// <summary>
        /// The date/time when this batch was completely processed
        /// </summary>
        public DateTime? completedDate { get; set; }

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
        /// The list of files contained in this batch.
        /// </summary>
        public List<BatchFileModel>? files { get; set; }

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
