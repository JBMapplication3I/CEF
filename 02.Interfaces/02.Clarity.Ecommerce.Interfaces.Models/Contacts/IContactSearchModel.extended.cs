// <copyright file="IContactSearchModel.extended.cs" company="clarity-ventures.com">
// Copyright (c) 2014-2023 clarity-ventures.com. All rights reserved.
// </copyright>
// <summary>Declares the IContactSearchModel interface</summary>
namespace Clarity.Ecommerce.Interfaces.Models
{
    /// <summary>Interface for contact search model.</summary>
    public interface IContactSearchModel
        : IHaveATypeBaseSearchModel,
            IAmFilterableByStoreSearchModel
    {
        /// <summary>Gets or sets the identifier of the address.</summary>
        /// <value>The identifier of the address.</value>
        int? AddressID { get; set; }

        /// <summary>Gets or sets the address identifier include null.</summary>
        /// <value>The address identifier include null.</value>
        bool? AddressIDIncludeNull { get; set; }

        /// <summary>Gets or sets the email 1.</summary>
        /// <value>The email 1.</value>
        string? Email1 { get; set; }

        /// <summary>Gets or sets the email 1 strict.</summary>
        /// <value>The email 1 strict.</value>
        bool? Email1Strict { get; set; }

        /// <summary>Gets or sets the email 1 include null.</summary>
        /// <value>The email 1 include null.</value>
        bool? Email1IncludeNull { get; set; }

        /// <summary>Gets or sets the fax 1.</summary>
        /// <value>The fax 1.</value>
        string? Fax1 { get; set; }

        /// <summary>Gets or sets the fax 1 strict.</summary>
        /// <value>The fax 1 strict.</value>
        bool? Fax1Strict { get; set; }

        /// <summary>Gets or sets the fax 1 include null.</summary>
        /// <value>The fax 1 include null.</value>
        bool? Fax1IncludeNull { get; set; }

        /// <summary>Gets or sets the person's first name.</summary>
        /// <value>The name of the first.</value>
        string? FirstName { get; set; }

        /// <summary>Gets or sets the first name strict.</summary>
        /// <value>The first name strict.</value>
        bool? FirstNameStrict { get; set; }

        /// <summary>Gets or sets the first name include null.</summary>
        /// <value>The first name include null.</value>
        bool? FirstNameIncludeNull { get; set; }

        /// <summary>Gets or sets the name of the full.</summary>
        /// <value>The name of the full.</value>
        string? FullName { get; set; }

        /// <summary>Gets or sets the full name strict.</summary>
        /// <value>The full name strict.</value>
        bool? FullNameStrict { get; set; }

        /// <summary>Gets or sets the full name include null.</summary>
        /// <value>The full name include null.</value>
        bool? FullNameIncludeNull { get; set; }

        /// <summary>Gets or sets the person's last name.</summary>
        /// <value>The name of the last.</value>
        string? LastName { get; set; }

        /// <summary>Gets or sets the last name strict.</summary>
        /// <value>The last name strict.</value>
        bool? LastNameStrict { get; set; }

        /// <summary>Gets or sets the last name include null.</summary>
        /// <value>The last name include null.</value>
        bool? LastNameIncludeNull { get; set; }

        /// <summary>Gets or sets the person's middle name.</summary>
        /// <value>The name of the middle.</value>
        string? MiddleName { get; set; }

        /// <summary>Gets or sets the middle name strict.</summary>
        /// <value>The middle name strict.</value>
        bool? MiddleNameStrict { get; set; }

        /// <summary>Gets or sets the middle name include null.</summary>
        /// <value>The middle name include null.</value>
        bool? MiddleNameIncludeNull { get; set; }

        /// <summary>Gets or sets the phone 1.</summary>
        /// <value>The phone 1.</value>
        string? Phone1 { get; set; }

        /// <summary>Gets or sets the phone 1 strict.</summary>
        /// <value>The phone 1 strict.</value>
        bool? Phone1Strict { get; set; }

        /// <summary>Gets or sets the phone 1 include null.</summary>
        /// <value>The phone 1 include null.</value>
        bool? Phone1IncludeNull { get; set; }

        /// <summary>Gets or sets the phone 2.</summary>
        /// <value>The phone 2.</value>
        string? Phone2 { get; set; }

        /// <summary>Gets or sets the phone 2 strict.</summary>
        /// <value>The phone 2 strict.</value>
        bool? Phone2Strict { get; set; }

        /// <summary>Gets or sets the phone 2 include null.</summary>
        /// <value>The phone 2 include null.</value>
        bool? Phone2IncludeNull { get; set; }

        /// <summary>Gets or sets the phone 3.</summary>
        /// <value>The phone 3.</value>
        string? Phone3 { get; set; }

        /// <summary>Gets or sets the phone 3 strict.</summary>
        /// <value>The phone 3 strict.</value>
        bool? Phone3Strict { get; set; }

        /// <summary>Gets or sets the phone 3 include null.</summary>
        /// <value>The phone 3 include null.</value>
        bool? Phone3IncludeNull { get; set; }

        /// <summary>Gets or sets the website 1.</summary>
        /// <value>The website 1.</value>
        string? Website1 { get; set; }

        /// <summary>Gets or sets the website 1 strict.</summary>
        /// <value>The website 1 strict.</value>
        bool? Website1Strict { get; set; }

        /// <summary>Gets or sets the website 1 include null.</summary>
        /// <value>The website 1 include null.</value>
        bool? Website1IncludeNull { get; set; }
    }
}
