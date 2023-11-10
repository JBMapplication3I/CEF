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
    /// 
    /// </summary>
    public class AuditEvent
    {
        /// <summary>
        /// 
        /// </summary>
        public long? auditEventId { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public long? transactionId { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public int? auditEventLevelId { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public DateTime? eventTimestamp { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public string? source { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public string? summary { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public string? details { get; set; }

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
