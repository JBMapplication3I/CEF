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
    using Newtonsoft.Json;

    /// <summary>Represents one configuration setting for this account.</summary>
    public class AccountConfigurationModel
    {
        /// <summary>The unique ID number of the account to which this setting applies.</summary>
        /// <value>The identifier of the account.</value>
        public int? accountId { get; set; }

        /// <summary>The category of the configuration setting. Avalara-defined categories include `AddressServiceConfig`
        /// and `TaxServiceConfig`. Customer-defined categories begin with `X-`.</summary>
        /// <value>The category.</value>
        public string? category { get; set; }

        /// <summary>The name of the configuration setting.</summary>
        /// <value>The name.</value>
        public string? name { get; set; }

        /// <summary>The current value of the configuration setting.</summary>
        /// <value>The value.</value>
        public string? value { get; set; }

        /// <summary>The date when this record was created.</summary>
        /// <value>The created date.</value>
        public DateTime? createdDate { get; set; }

        /// <summary>The User ID of the user who created this record.</summary>
        /// <value>The identifier of the created user.</value>
        public int? createdUserId { get; set; }

        /// <summary>The date/time when this record was last modified.</summary>
        /// <value>The modified date.</value>
        public DateTime? modifiedDate { get; set; }

        /// <summary>The user ID of the user who last modified this record.</summary>
        /// <value>The identifier of the modified user.</value>
        public int? modifiedUserId { get; set; }

        /// <summary>Convert this object to a JSON string of itself.</summary>
        /// <returns>A JSON string of this object.</returns>
        public override string ToString()
        {
            return JsonConvert.SerializeObject(this, new JsonSerializerSettings() { Formatting = Formatting.Indented });
        }
    }
}
