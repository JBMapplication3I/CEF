// <copyright file="GlobalAddressRequestModel.cs" company="clarity-ventures.com">
// Copyright (c) 2020-2023 clarity-ventures.com. All rights reserved.
// </copyright>
// <summary>Implements the global address request model class</summary>
namespace Clarity.Ecommerce.Providers.AddressValidation.MelissaData.Models
{
    using System.Collections.Generic;

    /// <summary>Model for the Melissa Data Request.</summary>
    public class GlobalAddressRequestModel
    {
        /// <summary>The internal reference ID that is given back to reference the correct request.</summary>
        /// <value>The transmission reference.</value>
        public string? TransmissionReference { get; set; }

        /// <summary>The Key provided by the client for authorization.</summary>
        /// <value>The license key.</value>
        public string? LicenseKey { get; set; }

        /// <summary>Options - most common: Key = OutputScript Value = NOCHANGE, LATN, NATIVE We will probably use the
        /// Native option most of the time.</summary>
        /// <value>The options.</value>
        public Dictionary<string, string>? Options { get; set; }

        /// <summary>This is a string used to delimit line in the formattedAddress output field. This string can be one
        /// or multiple characters.</summary>
        /// <value>The format.</value>
        public string? Format { get; set; }

        /// <summary>Unique ID if processing multiple records.</summary>
        /// <value>The identifier of the record.</value>
        public string? RecordID { get; set; }

        /// <summary>The organization name associated with the address record.</summary>
        /// <value>The organization.</value>
        public string? Organization { get; set; }

        /// <summary>The input field for the address. This should contain the delivery address information (house number,
        /// thoroughfare, building, suite, etc.)
        /// but should not contain locality information (locality, administrative area, postal code, etc.)
        /// which have their own inputs.</summary>
        /// <value>The address line 1.</value>
        public string? AddressLine1 { get; set; }

        /// <summary>The input field for the address. This should contain the delivery address information (house number,
        /// thoroughfare, building, suite, etc.)
        /// but should not contain locality information (locality, administrative area, postal code, etc.)
        /// which have their own inputs.</summary>
        /// <value>The address line 2.</value>
        public string? AddressLine2 { get; set; }

        /// <summary>The input field for the address. This should contain the delivery address information (house number,
        /// thoroughfare, building, suite, etc.)
        /// but should not contain locality information (locality, administrative area, postal code, etc.)
        /// which have their own inputs.</summary>
        /// <value>The address line 3.</value>
        public string? AddressLine3 { get; set; }

        /// <summary>The input field for the address. This should contain the delivery address information (house number,
        /// thoroughfare, building, suite, etc.)
        /// but should not contain locality information (locality, administrative area, postal code, etc.)
        /// which have their own inputs.</summary>
        /// <value>The address line 4.</value>
        public string? AddressLine4 { get; set; }

        /// <summary>The input field for the address. This should contain the delivery address information (house number,
        /// thoroughfare, building, suite, etc.)
        /// but should not contain locality information (locality, administrative area, postal code, etc.)
        /// which have their own inputs.</summary>
        /// <value>The address line 5.</value>
        public string? AddressLine5 { get; set; }

        /// <summary>The input field for the address. This should contain the delivery address information (house number,
        /// thoroughfare, building, suite, etc.)
        /// but should not contain locality information (locality, administrative area, postal code, etc.)
        /// which have their own inputs.</summary>
        /// <value>The address line 6.</value>
        public string? AddressLine6 { get; set; }

        /// <summary>The input field for the address. This should contain the delivery address information (house number,
        /// thoroughfare, building, suite, etc.)
        /// but should not contain locality information (locality, administrative area, postal code, etc.)
        /// which have their own inputs.</summary>
        /// <value>The address line 7.</value>
        public string? AddressLine7 { get; set; }

        /// <summary>The input field for the address. This should contain the delivery address information (house number,
        /// thoroughfare, building, suite, etc.)
        /// but should not contain locality information (locality, administrative area, postal code, etc.)
        /// which have their own inputs.</summary>
        /// <value>The address line 8.</value>
        public string? AddressLine8 { get; set; }

        /// <summary>The smallest population center data element. This depends on the Locality and DependentLocality
        /// elements.</summary>
        /// <value>The double dependent locality.</value>
        public string? DoubleDependentLocality { get; set; }

        /// <summary>The smaller population center data element. This depends on the Locality element. US TERM:
        /// Urbanization n terms of US Addresses, this element applies only to Puerto Rican addresses. It is used to
        /// break ties when a ZIP Code is linked to multiple instances of the same address.</summary>
        /// <value>The dependent locality.</value>
        public string? DependentLocality { get; set; }

        /// <summary>Required The most common population center data element CEF term: City.</summary>
        /// <value>The locality.</value>
        public string? Locality { get; set; }

        /// <summary>The smallest geographic data element CEF Term: County.</summary>
        /// <value>The sub administrative area.</value>
        public string? SubAdministrativeArea { get; set; }

        /// <summary>Required The most common geographic data element CEF term: Region.</summary>
        /// <value>The administrative area.</value>
        public string? AdministrativeArea { get; set; }

        /// <summary>Required The complete postal code for a particular delivery point CEF term: PostalCode If all three
        /// elements are provided and the PostalCode is incorrect, it can be corrected from the data on the Locality and
        /// AdministrativeArea.</summary>
        /// <value>The postal code.</value>
        public string? PostalCode { get; set; }

        /// <summary>The administrative region within a country on an arbitrary level below that of the sovereign state.</summary>
        /// <value>The sub national area.</value>
        public string? SubNationalArea { get; set; }

        /// <summary>Required The country name, abbreviation, or code.</summary>
        /// <value>The country.</value>
        public string? Country { get; set; }
    }
}
