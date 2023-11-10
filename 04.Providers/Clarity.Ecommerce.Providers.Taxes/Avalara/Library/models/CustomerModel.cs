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
    /// Represents a customer to whom you sell products and/or services.
    /// </summary>
    public class CustomerModel
    {
        /// <summary>
        /// Unique ID number of this customer.
        /// </summary>
        public int? id { get; set; }

        /// <summary>
        /// The unique ID number of the AvaTax company that recorded this customer.
        /// </summary>
        public int companyId { get; set; }

        /// <summary>
        /// The unique code identifying this customer. Must be unique within your company.
        ///  
        /// This code should be used in the `customerCode` field of any call that creates or adjusts a transaction
        /// in order to ensure that all exemptions that apply to this customer are correctly considered.
        ///  
        /// Note: This field is case sensitive.
        /// </summary>
        public string? customerCode { get; set; }

        /// <summary>
        /// A customer-configurable alternate ID number for this customer. You may set this value to match any
        /// other system that would like to reference this customer record.
        /// </summary>
        public string? alternateId { get; set; }

        /// <summary>
        /// A friendly name identifying this customer.
        /// </summary>
        public string? name { get; set; }

        /// <summary>
        /// Indicates the "Attn:" component of the address for this customer, if this customer requires mailings to be shipped
        /// to the attention of a specific person or department name.
        /// </summary>
        public string? attnName { get; set; }

        /// <summary>
        /// First line of the street address of this customer.
        /// </summary>
        public string? line1 { get; set; }

        /// <summary>
        /// Second line of the street address of this customer.
        /// </summary>
        public string? line2 { get; set; }

        /// <summary>
        /// City component of the street address of this customer.
        /// </summary>
        public string? city { get; set; }

        /// <summary>
        /// Postal Code / Zip Code component of the address of this customer.
        /// </summary>
        public string? postalCode { get; set; }

        /// <summary>
        /// The main phone number for this customer.
        /// </summary>
        public string? phoneNumber { get; set; }

        /// <summary>
        /// The fax phone number for this customer, if any.
        /// </summary>
        public string? faxNumber { get; set; }

        /// <summary>
        /// The main email address for this customer.
        /// </summary>
        public string? emailAddress { get; set; }

        /// <summary>
        /// The name of the main contact person for this customer.
        /// </summary>
        public string? contactName { get; set; }

        /// <summary>
        /// Date when this customer last executed a transaction.
        /// </summary>
        public DateTime? lastTransaction { get; set; }

        /// <summary>
        /// The date when this record was created.
        /// </summary>
        public DateTime? createdDate { get; set; }

        /// <summary>
        /// The date/time when this record was last modified.
        /// </summary>
        public DateTime? modifiedDate { get; set; }

        /// <summary>
        /// Name or ISO 3166 code identifying the country.
        ///  
        /// This field supports many different country identifiers:
        ///  * Two character ISO 3166 codes
        ///  * Three character ISO 3166 codes
        ///  * Fully spelled out names of the country in ISO supported languages
        ///  * Common alternative spellings for many countries
        ///  
        /// For a full list of all supported codes and names, please see the Definitions API `ListCountries`.
        /// </summary>
        public string? country { get; set; }

        /// <summary>
        /// ISO 3166 code identifying the region within the country.
        /// Two and three character ISO 3166 region codes.
        ///  
        /// For a full list of all supported codes, please see the Definitions API `ListRegions`.
        /// </summary>
        public string? region { get; set; }

        /// <summary>
        /// True if this customer record is specifically used for bill-to purposes.
        /// </summary>
        public bool? isBill { get; set; }

        /// <summary>
        /// True if this customer record is specifically used for ship-to purposes.
        /// </summary>
        public bool? isShip { get; set; }

        /// <summary>
        /// For customers in the United States, this field is the federal taxpayer ID number. For businesses, this is
        /// a Federal Employer Identification Number. For individuals, this will be a Social Security Number.
        /// </summary>
        public string? taxpayerIdNumber { get; set; }

        /// <summary>
        /// A list of exemption certficates that apply to this customer. You can fetch this data by specifying
        /// `$include=certificates` when calling a customer fetch API.
        /// </summary>
        public List<CertificateModel>? certificates { get; set; }

        /// <summary>
        /// A list of custom fields defined on this customer.
        ///  
        /// For more information about custom fields, see the [Avalara Help Center article about custom fields](https://help.avalara.com/0021_Avalara_CertCapture/All_About_CertCapture/Edit_or_Remove_Details_about_Customers).
        /// </summary>
        public List<CustomFieldModel>? customFields { get; set; }

        /// <summary>
        /// A list of exposure zones where you do business with this customer.
        ///  
        /// To keep track of certificates that are needed for each customer, set this value to a list of all exposure zones where you
        /// sell products to this customer. You can find a list of exposure zones by calling `ListExposureZones`.
        ///  
        /// This field is often called "Ship-To States" or "Ship-To Zones", since it generally refers to locations where you ship products
        /// when this customer makes a purchase.
        ///  
        /// This field is useful for audit purposes since it helps you ensure you have the necessary certificates for each customer.
        /// </summary>
        public List<ExposureZoneModel>? exposureZones { get; set; }

        /// <summary>
        /// A list of ship-to customer records that are connected to this bill-to customer.
        ///  
        /// Customer records represent businesses or individuals who can provide exemption certificates. Some customers
        /// may have certificates that are linked to their shipping address or their billing address. To group these
        /// customer records together, you may link multiple bill-to and ship-to addresses together to represent a single
        /// entity that has multiple different addresses of different kinds.
        /// </summary>
        public List<CustomerModel>? shipTos { get; set; }

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
