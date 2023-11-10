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
    /// The tax region jurisdiction model.
    /// </summary>
    public class TaxRegionJurisdictionModel
    {
        /// <summary>
        /// The id of the jurisdiction.
        /// </summary>
        public int? jurisdictionId { get; set; }

        /// <summary>
        /// The id of the tax region.
        /// </summary>
        public int? taxRegionId { get; set; }

        /// <summary>
        /// The id of the jurisdiction level.
        /// </summary>
        public int? jurisdictionLevelId { get; set; }

        /// <summary>
        /// The rock name.
        /// </summary>
        public string? rockName { get; set; }

        /// <summary>
        /// The report level.
        /// </summary>
        public int? reportLevel { get; set; }

        /// <summary>
        /// The state assigned code.
        /// </summary>
        public string? stateAssignedCode { get; set; }

        /// <summary>
        /// The id of the tax authority.
        /// </summary>
        public int? taxAuthorityId { get; set; }

        /// <summary>
        /// The signature code.
        /// </summary>
        public string? signatureCode { get; set; }

        /// <summary>
        /// The date in which this tax region jurisdiction starts to take effect.
        /// </summary>
        public DateTime? effectiveDate { get; set; }

        /// <summary>
        /// The date in which this tax region jurisdiction stops to take effect.
        /// </summary>
        public DateTime? endDate { get; set; }

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
