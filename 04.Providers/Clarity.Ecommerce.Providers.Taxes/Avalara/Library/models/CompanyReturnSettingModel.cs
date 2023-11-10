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

    /// <summary>A data Model for the company return setting.</summary>
    public class CompanyReturnSettingModel
    {
        /// <summary>The unique ID of this CompanyReturnsSetting.</summary>
        /// <value>The identifier.</value>
        public long? id { get; set; }

        /// <summary>The CompanyReturn Id.</summary>
        /// <value>The identifier of the company return.</value>
        public long companyReturnId { get; set; }

        /// <summary>The TaxFormCatalog filingQuestionId.</summary>
        /// <value>The identifier of the filing question.</value>
        public long filingQuestionId { get; set; }

        /// <summary>Filing question code as defined in TaxFormCatalog.</summary>
        /// <value>The filing question code.</value>
        public string? filingQuestionCode { get; set; }

        /// <summary>The value of this setting.</summary>
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

        /// <inheritdoc/>
        public override string ToString()
        {
            return JsonConvert.SerializeObject(this, new JsonSerializerSettings() { Formatting = Formatting.Indented });
        }
    }
}
