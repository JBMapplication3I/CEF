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
    /// Represents information about a newly created account
    /// </summary>
    public class NewAccountModel
    {
        /// <summary>
        /// This is the ID number of the account that was created
        /// </summary>
        public int? accountId { get; set; }

        /// <summary>
        /// This is the email address to which credentials were mailed
        /// </summary>
        public string? accountDetailsEmailedTo { get; set; }

        /// <summary>
        /// The date and time when this account was created
        /// </summary>
        public DateTime? createdDate { get; set; }

        /// <summary>
        /// The date and time when account information was emailed to the user
        /// </summary>
        public DateTime? emailedDate { get; set; }

        /// <summary>
        /// If this account includes any limitations, specify them here
        /// </summary>
        public string? limitations { get; set; }

        /// <summary>
        /// The license key of the account that was created
        /// </summary>
        public string? licenseKey { get; set; }

        /// <summary>
        /// The payment url where the payment method can be set up
        /// </summary>
        public string? paymentUrl { get; set; }

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
