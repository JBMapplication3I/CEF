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

    /// <summary>An AvaTax account.</summary>
    public class AccountModel
    {
        /// <summary>The unique ID number assigned to this account.</summary>
        /// <value>The identifier.</value>
        public int id { get; set; }

        /// <summary>For system registrar use only.</summary>
        /// <value>The crmid.</value>
        public string? crmid { get; set; }

        /// <summary>The name of this account.</summary>
        /// <value>The name.</value>
        public string? name { get; set; }

        /// <summary>The earliest date on which this account may be used.</summary>
        /// <value>The effective date.</value>
        public DateTime? effectiveDate { get; set; }

        /// <summary>If this account has been closed, this is the last date the account was open.</summary>
        /// <value>The end date.</value>
        public DateTime? endDate { get; set; }

        /// <summary>The current status of this account.</summary>
        /// <value>The identifier of the account status.</value>
        public AccountStatusId? accountStatusId { get; set; }

        /// <summary>The type of this account.</summary>
        /// <value>The identifier of the account type.</value>
        public AccountTypeId? accountTypeId { get; set; }

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

        /// <summary>Optional: A list of subscriptions granted to this account. To fetch this list, add the query string
        /// "?$include=Subscriptions" to your URL.</summary>
        /// <value>The subscriptions.</value>
        public List<SubscriptionModel>? subscriptions { get; set; }

        /// <summary>Optional: A list of all the users belonging to this account. To fetch this list, add the query
        /// string "?$include=Users" to your URL.</summary>
        /// <value>The users.</value>
        public List<UserModel>? users { get; set; }

        /// <summary>Convert this object to a JSON string of itself.</summary>
        /// <returns>A JSON string of this object.</returns>
        public override string ToString()
        {
            return JsonConvert.SerializeObject(this, new JsonSerializerSettings() { Formatting = Formatting.Indented });
        }
    }
}
