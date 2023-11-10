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
    /// An attachment associated with a filing return
    /// </summary>
    public class FilingReturnCreditModel
    {
        /// <summary>
        /// The resourceFileId used to retrieve the attachment
        /// </summary>
        public decimal? totalSales { get; set; }

        /// <summary>
        /// The resourceFileId used to retrieve the attachment
        /// </summary>
        public decimal? totalExempt { get; set; }

        /// <summary>
        /// The resourceFileId used to retrieve the attachment
        /// </summary>
        public decimal? totalTaxable { get; set; }

        /// <summary>
        /// The resourceFileId used to retrieve the attachment
        /// </summary>
        public decimal? totalTax { get; set; }

        /// <summary>
        /// The excluded carry over credit documents
        /// </summary>
        public List<WorksheetDocument>? transactionDetails { get; set; }

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
