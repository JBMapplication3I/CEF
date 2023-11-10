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
    public class EcmsDetailTaxCodeModel
    {
        /// <summary>
        /// Id of the exempt certificate detail tax code
        /// </summary>
        public int? exemptCertDetailTaxCodeId { get; set; }

        /// <summary>
        /// exempt certificate detail id
        /// </summary>
        public int? exemptCertDetailId { get; set; }

        /// <summary>
        /// tax code id
        /// </summary>
        public int? taxCodeId { get; set; }

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
