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
    /// Represents communication between Avalara and the company regarding the processing of a tax notice.
    /// </summary>
    public class NoticeCommentModel
    {
        /// <summary>
        /// The unique ID number of this notice.
        /// </summary>
        public int? id { get; set; }

        /// <summary>
        /// The ID of the notice this comment is attached too
        /// </summary>
        public int noticeId { get; set; }

        /// <summary>
        /// The date this comment was entered
        /// </summary>
        public DateTime? date { get; set; }

        /// <summary>
        /// TaxNoticeComment
        /// </summary>
        public string? comment { get; set; }

        /// <summary>
        /// TaxNoticeCommentUserId
        /// </summary>
        public int? commentUserId { get; set; }

        /// <summary>
        /// TaxNoticeCommentUserName
        /// </summary>
        public string? commentUserName { get; set; }

        /// <summary>
        /// taxNoticeCommentTypeId
        /// </summary>
        public int? commentTypeId { get; set; }

        /// <summary>
        /// taxNoticeCommentType
        /// </summary>
        public CommentType commentType { get; set; }

        /// <summary>
        /// TaxNoticeCommentLink
        /// </summary>
        public string? commentLink { get; set; }

        /// <summary>
        /// TaxNoticeFileName
        /// </summary>
        public string? taxNoticeFileName { get; set; }

        /// <summary>
        /// resourceFileId
        /// </summary>
        public long? resourceFileId { get; set; }

        /// <summary>
        /// The date/time when this record was last modified.
        /// </summary>
        public DateTime? modifiedDate { get; set; }

        /// <summary>
        /// The user ID of the user who last modified this record.
        /// </summary>
        public int? modifiedUserId { get; set; }

        /// <summary>
        /// An attachment to the detail
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
