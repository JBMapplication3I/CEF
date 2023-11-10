// <copyright file="IHaveAContactBaseModel.cs" company="clarity-ventures.com">
// Copyright (c) 2016-2023 clarity-ventures.com. All rights reserved.
// </copyright>
// <summary>Declares the IHaveAContactBaseModel interface</summary>
namespace Clarity.Ecommerce.Interfaces.Models
{
    /// <summary>Interface for have a contact base model.</summary>
    /// <seealso cref="IBaseModel"/>
    public interface IHaveAContactBaseModel : IBaseModel
    {
        /// <summary>Gets or sets the identifier of the contact.</summary>
        /// <value>The identifier of the contact.</value>
        int ContactID { get; set; }

        /// <summary>Gets or sets the contact.</summary>
        /// <value>The contact.</value>
        IContactModel? Contact { get; set; }

        /// <summary>Gets or sets the contact key.</summary>
        /// <value>The contact key.</value>
        string? ContactKey { get; set; }

        /// <summary>Gets or sets the contact phone.</summary>
        /// <value>The contact phone.</value>
        string? ContactPhone { get; set; }

        /// <summary>Gets or sets the contact fax.</summary>
        /// <value>The contact fax.</value>
        string? ContactFax { get; set; }

        /// <summary>Gets or sets the contact email.</summary>
        /// <value>The contact email.</value>
        string? ContactEmail { get; set; }

        /// <summary>Gets or sets the name of the contact first.</summary>
        /// <value>The name of the contact first.</value>
        string? ContactFirstName { get; set; }

        /// <summary>Gets or sets the name of the contact last.</summary>
        /// <value>The name of the contact last.</value>
        string? ContactLastName { get; set; }
    }

    /// <summary>Interface for have a nullable contact base model.</summary>
    /// <seealso cref="IBaseModel"/>
    public interface IHaveANullableContactBaseModel : IBaseModel
    {
        /// <summary>Gets or sets the identifier of the contact.</summary>
        /// <value>The identifier of the contact.</value>
        int? ContactID { get; set; }

        /// <summary>Gets or sets the contact.</summary>
        /// <value>The contact.</value>
        IContactModel? Contact { get; set; }

        /// <summary>Gets or sets the contact key.</summary>
        /// <value>The contact key.</value>
        string? ContactKey { get; set; }

        /// <summary>Gets or sets the contact phone.</summary>
        /// <value>The contact phone.</value>
        string? ContactPhone { get; set; }

        /// <summary>Gets or sets the contact fax.</summary>
        /// <value>The contact fax.</value>
        string? ContactFax { get; set; }

        /// <summary>Gets or sets the contact email.</summary>
        /// <value>The contact email.</value>
        string? ContactEmail { get; set; }

        /// <summary>Gets or sets the name of the contact first.</summary>
        /// <value>The name of the contact first.</value>
        string? ContactFirstName { get; set; }

        /// <summary>Gets or sets the name of the contact last.</summary>
        /// <value>The name of the contact last.</value>
        string? ContactLastName { get; set; }
    }

    /// <summary>Interface for have a contact with same identifier base model.</summary>
    /// <seealso cref="IBaseModel"/>
    public interface IHaveAContactWithSameIDBaseModel : IBaseModel
    {
        /// <summary>Gets or sets the contact.</summary>
        /// <value>The contact.</value>
        IContactModel? Contact { get; set; }
    }

    /// <summary>Interface for have a contact base search model.</summary>
    public interface IHaveAContactBaseSearchModel
    {
        /// <summary>Gets or sets the identifier of the contact.</summary>
        /// <value>The identifier of the contact.</value>
        int? ContactID { get; set; }

        /// <summary>Gets or sets the contact identifier include null.</summary>
        /// <value>The contact identifier include null.</value>
        bool? ContactIDIncludeNull { get; set; }

        /// <summary>Gets or sets the contact key.</summary>
        /// <value>The contact key.</value>
        string? ContactKey { get; set; }

        /// <summary>Gets or sets the contact key strict.</summary>
        /// <value>The contact key strict.</value>
        bool? ContactKeyStrict { get; set; }

        /// <summary>Gets or sets the contact key include null.</summary>
        /// <value>The contact key include null.</value>
        bool? ContactKeyIncludeNull { get; set; }

        /// <summary>Gets or sets the first name of the contact.</summary>
        /// <value>The first name of the contact.</value>
        string? ContactFirstName { get; set; }

        /// <summary>Gets or sets the contact first name strict.</summary>
        /// <value>The contact first name strict.</value>
        bool? ContactFirstNameStrict { get; set; }

        /// <summary>Gets or sets the contact first name include null.</summary>
        /// <value>The contact first name include null.</value>
        bool? ContactFirstNameIncludeNull { get; set; }

        /// <summary>Gets or sets the last name of the contact.</summary>
        /// <value>The last name of the contact.</value>
        string? ContactLastName { get; set; }

        /// <summary>Gets or sets the contact last name strict.</summary>
        /// <value>The contact last name strict.</value>
        bool? ContactLastNameStrict { get; set; }

        /// <summary>Gets or sets the contact last name include null.</summary>
        /// <value>The contact last name include null.</value>
        bool? ContactLastNameIncludeNull { get; set; }

        /// <summary>Gets or sets the contact phone.</summary>
        /// <value>The contact phone.</value>
        string? ContactPhone { get; set; }

        /// <summary>Gets or sets the contact phone strict.</summary>
        /// <value>The contact phone strict.</value>
        bool? ContactPhoneStrict { get; set; }

        /// <summary>Gets or sets the contact phone include null.</summary>
        /// <value>The contact phone include null.</value>
        bool? ContactPhoneIncludeNull { get; set; }

        /// <summary>Gets or sets the contact fax.</summary>
        /// <value>The contact fax.</value>
        string? ContactFax { get; set; }

        /// <summary>Gets or sets the contact fax strict.</summary>
        /// <value>The contact fax strict.</value>
        bool? ContactFaxStrict { get; set; }

        /// <summary>Gets or sets the contact fax include null.</summary>
        /// <value>The contact fax include null.</value>
        bool? ContactFaxIncludeNull { get; set; }

        /// <summary>Gets or sets the contact email.</summary>
        /// <value>The contact email.</value>
        string? ContactEmail { get; set; }

        /// <summary>Gets or sets the contact email strict.</summary>
        /// <value>The contact email strict.</value>
        bool? ContactEmailStrict { get; set; }

        /// <summary>Gets or sets the contact email include null.</summary>
        /// <value>The contact email include null.</value>
        bool? ContactEmailIncludeNull { get; set; }
    }
}
