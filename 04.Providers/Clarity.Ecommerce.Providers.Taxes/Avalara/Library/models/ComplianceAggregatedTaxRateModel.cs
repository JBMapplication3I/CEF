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
    /// A model for aggregated rates.
    /// </summary>
    public class ComplianceAggregatedTaxRateModel
    {
        /// <summary>
        /// The compontent rate.
        /// </summary>
        public decimal? rate { get; set; }

        /// <summary>
        /// The stack rate based on the aggregation method.
        /// </summary>
        public decimal? stackRate { get; set; }

        /// <summary>
        /// The date this rate is starts to take effect.
        /// </summary>
        public DateTime? effectiveDate { get; set; }

        /// <summary>
        /// The date this rate is no longer active.
        /// </summary>
        public DateTime? endDate { get; set; }

        /// <summary>
        /// The tax type of the rate.
        /// </summary>
        public string? taxTypeId { get; set; }

        /// <summary>
        /// The rate type of the rate.
        /// </summary>
        public string? rateTypeId { get; set; }

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
