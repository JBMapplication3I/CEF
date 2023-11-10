// <copyright file="Record.cs" company="clarity-ventures.com">
// Copyright (c) 2019-2023 clarity-ventures.com. All rights reserved.
// </copyright>
// <summary>Implements the record class</summary>
// ReSharper disable InconsistentNaming
namespace Clarity.Ecommerce.Providers.AddressValidation.MelissaData.Models
{
    /// <summary>Record Model for the Validation Results</summary>
    [JetBrains.Annotations.PublicAPI]
    public class Record
    {
        /// <summary>This is a string value that is a unique identifier for the current record if one was sent in the
        /// request. Use this element to match a request record and the corresponding response record.</summary>
        /// <value>The identifier of the record.</value>
        public string? RecordID { get; set; }

        /// <summary>This is a string value with comma delimited status, error codes, and change codes for the record.
        /// For a complete list of codes, see Global Address Verification Result Codes.</summary>
        /// <value>The results.</value>
        public string? Results { get; set; }

        /// <summary>This is a string value that is the address in the correct format for mailing from the country
        /// specified in the CountryOfOrigin element. This includes the Organization as the first line, one or more
        /// lines in the origin country’s format, and the destination country (if required). Separate lines will be
        /// delimited by what is specified in the option. See LineSeparator for more information.</summary>
        /// <value>The formatted address.</value>
        public string? FormattedAddress { get; set; }

        /// <summary>This is a string value that matches the Organization request element. It is not modified or
        /// populated by the Cloud Service.</summary>
        /// <value>The organization.</value>
        public string? Organization { get; set; }

        /// <summary>These are the string values that will return the standardized or corrected contents of the input
        /// address. These lines will include the entire address including the locality, administrative area, and postal
        /// code. If the DeliveryLines option is turned on, only the address up to the dependent locality will be
        /// returned.</summary>
        /// <value>The address line 1.</value>
        public string? AddressLine1 { get; set; }

        /// <summary>These are the string values that will return the standardized or corrected contents of the input
        /// address. These lines will include the entire address including the locality, administrative area, and postal
        /// code. If the DeliveryLines option is turned on, only the address up to the dependent locality will be
        /// returned.</summary>
        /// <value>The address line 2.</value>
        public string? AddressLine2 { get; set; }

        /// <summary>These are the string values that will return the standardized or corrected contents of the input
        /// address. These lines will include the entire address including the locality, administrative area, and postal
        /// code. If the DeliveryLines option is turned on, only the address up to the dependent locality will be
        /// returned.</summary>
        /// <value>The address line 3.</value>
        public string? AddressLine3 { get; set; }

        /// <summary>These are the string values that will return the standardized or corrected contents of the input
        /// address. These lines will include the entire address including the locality, administrative area, and postal
        /// code. If the DeliveryLines option is turned on, only the address up to the dependent locality will be
        /// returned.</summary>
        /// <value>The address line 4.</value>
        public string? AddressLine4 { get; set; }

        /// <summary>These are the string values that will return the standardized or corrected contents of the input
        /// address. These lines will include the entire address including the locality, administrative area, and postal
        /// code. If the DeliveryLines option is turned on, only the address up to the dependent locality will be
        /// returned.</summary>
        /// <value>The address line 5.</value>
        public string? AddressLine5 { get; set; }

        /// <summary>These are the string values that will return the standardized or corrected contents of the input
        /// address. These lines will include the entire address including the locality, administrative area, and postal
        /// code. If the DeliveryLines option is turned on, only the address up to the dependent locality will be
        /// returned.</summary>
        /// <value>The address line 6.</value>
        public string? AddressLine6 { get; set; }

        /// <summary>These are the string values that will return the standardized or corrected contents of the input
        /// address. These lines will include the entire address including the locality, administrative area, and postal
        /// code. If the DeliveryLines option is turned on, only the address up to the dependent locality will be
        /// returned.</summary>
        /// <value>The address line 7.</value>
        public string? AddressLine7 { get; set; }

        /// <summary>These are the string values that will return the standardized or corrected contents of the input
        /// address. These lines will include the entire address including the locality, administrative area, and postal
        /// code. If the DeliveryLines option is turned on, only the address up to the dependent locality will be
        /// returned.</summary>
        /// <value>The address line 8.</value>
        public string? AddressLine8 { get; set; }

        /// <summary>This is a string value that is the parsed SubPremises from the AddressLine elements. A subpremise
        /// are individual units with their own addresses inside a building. US Term: Suite or Apartment.</summary>
        /// <value>The sub premises.</value>
        public string? SubPremises { get; set; }

        /// <summary>This is a string value that is the standardized contents of the DoubleDependentLocality element. A
        /// double dependent locality is a logical area unit that is smaller than a dependent locality but bigger than a
        /// thoroughfare. This field is very rarely used. Great Britain is an example of a country that uses double
        /// dependent locality.</summary>
        /// <value>The double dependent locality.</value>
        public string? DoubleDependentLocality { get; set; }

        /// <summary>This is a string value that is the standardized contents of the DependentLocality element. A
        /// dependent locality is a logical area unit that is smaller than a locality but larger than a double dependent
        /// locality or thoroughfare. It can often be associated with a neighborhood or sector. Great Britain is an
        /// example of a country that uses double dependent locality. In the United States, this would correspond to
        /// Urbanization, which is used only in Puerto Rico.</summary>
        /// <value>The dependent locality.</value>
        public string? DependentLocality { get; set; }

        /// <summary>This is a string value that is the standardized contents of the Locality element. This is the most
        /// common geographic area and used by virtually all countries. This is usually the value that is written on a
        /// mailing label and referred to by terms like City, Town, Postal Town, etc.</summary>
        /// <value>The locality.</value>
        public string? Locality { get; set; }

        /// <summary>This is a string value that is the standardized contents of the SubAdministrativeArea element. This
        /// is a logical area that that is smaller than the administrative area but larger than a locality. While many
        /// countries can have a sub-administrative area value, it is very rarely used as part of an official address.</summary>
        /// <value>The sub administrative area.</value>
        public string? SubAdministrativeArea { get; set; }

        /// <summary>This is a string value that is the standardized contents of the AdministrativeArea element. This is
        /// a common geographic area unit for larger countries. Often referred to as State or Province. US Term: State.</summary>
        /// <value>The administrative area.</value>
        public string? AdministrativeArea { get; set; }

        /// <summary>This is a string value that is the standardized contents of the PostalCode element. Most countries
        /// have some form of a postal code system.</summary>
        /// <value>The postal code.</value>
        public string? PostalCode { get; set; }

        /// <summary>This is an appended string value that returns a one-character code for the type of address coded.
        /// This element works only for US and Canadian addresses.</summary>
        /// <value>The type of the address.</value>
        public string? AddressType { get; set; }

        /// <summary>This is a string value that is a unique key for the address. Only for US and Canadian addresses. The
        /// AddressKey can be used by other Melissa Data services, such as Geocoder or RBDI.</summary>
        /// <value>The address key.</value>
        public string? AddressKey { get; set; }

        /// <summary>This is a string value that is the standardized contents of the SubNationalArea element. A sub-
        /// national area is a logical area unit that is larger than an administrative area but smaller than the country
        /// itself. It is extremely rarely used.</summary>
        /// <value>The sub national area.</value>
        public string? SubNationalArea { get; set; }

        /// <summary>This is a string value that is the standardized contents of the CountryName element.</summary>
        /// <value>The name of the country.</value>
        public string? CountryName { get; set; }

        /// <summary>This is a string value that is the 2 letter ISO 3166 country code value.</summary>
        /// <value>The country ISO 3166 1 alpha 2.</value>
        public string? CountryISO3166_1_Alpha2 { get; set; }

        /// <summary>This is a string value that is the 3 letter ISO 3166 country code value.</summary>
        /// <value>The country ISO 3166 1 alpha 3.</value>
        public string? CountryISO3166_1_Alpha3 { get; set; }

        /// <summary>This is a string value that is the ISO 3166 country number value.</summary>
        /// <value>The country ISO 3166 1 numeric.</value>
        public string? CountryISO3166_1_Numeric { get; set; }

        /// <summary>This is the ISO3166-2 code for country subdivisions, usually tied to the administrative area for a
        /// country. The format is the 2 letter country code followed by a dash followed by 2 or 3 characters or two
        /// numbers. Examples are: US-CA, CN-16, or AU-VIC. Currently, this field is only populated for some countries.</summary>
        /// <value>The country subdivision code.</value>
        public string? CountrySubdivisionCode { get; set; }

        /// <summary>This is a string value that is the parsed Thoroughfare element from the output. This value is a part
        /// of the address lines and contains all the sub-elements of the thoroughfare like trailing type, thoroughfare
        /// name, pre direction, post direction, etc. US Term: Street.</summary>
        /// <value>The thoroughfare.</value>
        public string? Thoroughfare { get; set; }

        /// <summary>This is a string value that is the parsed ThoroughfarePreDirection element from the output. This
        /// value is a part of the Thoroughfare field. US Term: Pre Direction.</summary>
        /// <value>The thoroughfare pre direction.</value>
        public string? ThoroughfarePreDirection { get; set; }

        /// <summary>This is a string value that is the parsed ThoroughfareLeadingType element from the output. A leading
        /// type is a thoroughfare type that is placed before the thoroughfare. This value is a part of the Thoroughfare
        /// field. For example, the thoroughfare type of "Rue" in Canada and France is placed before the thoroughfare,
        /// making it a leading type. US Term: Not used in the US.</summary>
        /// <value>The type of the thoroughfare leading.</value>
        public string? ThoroughfareLeadingType { get; set; }

        /// <summary>This is a string value that is the parsed ThoroughfareName element from the output. This value is a
        /// part of the Thoroughfare field. US Term: Street name.</summary>
        /// <value>The name of the thoroughfare.</value>
        public string? ThoroughfareName { get; set; }

        /// <summary>This is a string value that is the parsed ThoroughfareTrailingType element from the output. A
        /// trailing type is a thoroughfare type that is placed after the thoroughfare. This value is a part of the
        /// Thoroughfare field. For example, the thoroughfare type of "Avenue" in the US is placed after the
        /// thoroughfare, making it a trailing type. US Term: Street Suffix.</summary>
        /// <value>The type of the thoroughfare trailing.</value>
        public string? ThoroughfareTrailingType { get; set; }

        /// <summary>This is a string value that is the parsed ThoroughfarePostDirection element from the output. This
        /// value is a part of the Thoroughfare field. US Term: Post Direction.</summary>
        /// <value>The thoroughfare post direction.</value>
        public string? ThoroughfarePostDirection { get; set; }

        /// <summary>This is a string value that is the parsed DependentThoroughfare element from the output. The
        /// dependent thoroughfare is a second thoroughfare that is required to narrow down the final address. This is
        /// rarely used.</summary>
        /// <value>The dependent thoroughfare.</value>
        public string? DependentThoroughfare { get; set; }

        /// <summary>This is a string value that is the parsed DependentThoroughfarePreDirection element from the output.
        /// This value is a part of the DependentThoroughfare field.</summary>
        /// <value>The dependent thoroughfare pre direction.</value>
        public string? DependentThoroughfarePreDirection { get; set; }

        /// <summary>This is a string value that is the parsed DependentThoroughfareLeadingType element from the output.
        /// This value is a part of the DependentThoroughfare field.</summary>
        /// <value>The type of the dependent thoroughfare leading.</value>
        public string? DependentThoroughfareLeadingType { get; set; }

        /// <summary>This is a string value that is the parsed DependentThoroughfareName element from the output. This
        /// value is a part of the DependentThoroughfare field.</summary>
        /// <value>The name of the dependent thoroughfare.</value>
        public string? DependentThoroughfareName { get; set; }

        /// <summary>This is a string value that is the parsed DependentThoroughfareTrailingType element from the output.
        /// This value is a part of the DependentThoroughfare field.</summary>
        /// <value>The type of the dependent thoroughfare trailing.</value>
        public string? DependentThoroughfareTrailingType { get; set; }

        /// <summary>This is a string value that is the parsed DependentThoroughfarePostDirection element from the
        /// output. This value is a part of the DependentThoroughfare field.</summary>
        /// <value>The dependent thoroughfare post direction.</value>
        public string? DependentThoroughfarePostDirection { get; set; }

        /// <summary>This is a string value that is the parsed Building element from the output.</summary>
        /// <value>The building.</value>
        public string? Building { get; set; }

        /// <summary>This is a string value that is the parsed PremisesType element from the output.</summary>
        /// <value>The type of the premises.</value>
        public string? PremisesType { get; set; }

        /// <summary>This is a string value that is the parsed PremisesNumber element from the output. US Term: House
        /// Number.</summary>
        /// <value>The premises number.</value>
        public string? PremisesNumber { get; set; }

        /// <summary>This is a string value that is the parsed SubPremisesType element from the output.</summary>
        /// <value>The type of the sub premises.</value>
        public string? SubPremisesType { get; set; }

        /// <summary>This is a string value that is the parsed SubPremisesNumber element from the output.</summary>
        /// <value>The sub premises number.</value>
        public string? SubPremisesNumber { get; set; }

        /// <summary>This is a string value that is the parsed PostBox element from the output.</summary>
        /// <value>The post box.</value>
        public string? PostBox { get; set; }

        /// <summary>This is a string value that is the parsed Latitude element from the output.</summary>
        /// <value>The latitude.</value>
        public string? Latitude { get; set; }

        /// <summary>This is a string value that is the parsed Longitude element from the output.</summary>
        /// <value>The longitude.</value>
        public string? Longitude { get; set; }
    }
}
