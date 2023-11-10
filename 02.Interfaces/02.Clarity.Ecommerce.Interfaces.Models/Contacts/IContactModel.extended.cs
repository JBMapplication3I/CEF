// <copyright file="IContactModel.extended.cs" company="clarity-ventures.com">
// Copyright (c) 2016-2023 clarity-ventures.com. All rights reserved.
// </copyright>
// <summary>Declares the IContactModel interface</summary>
namespace Clarity.Ecommerce.Interfaces.Models
{
    using DataModel;

    /// <summary>Interface for contact model.</summary>
    /// <seealso cref="IBaseModel"/>
    public interface IContactModel
        : IHaveATypeBaseModel<ITypeModel>,
            IAmFilterableByStoreModel<IStoreModel>,
            IHaveImagesBaseModel<IContactImageModel, ITypeModel>,
            ICloneable
    {
        /// <summary>Gets or sets the person's first name.</summary>
        /// <value>The name of the first.</value>
        string? FirstName { get; set; }

        /// <summary>Gets or sets the person's middle name.</summary>
        /// <value>The name of the middle.</value>
        string? MiddleName { get; set; }

        /// <summary>Gets or sets the person's last name.</summary>
        /// <value>The name of the last.</value>
        string? LastName { get; set; }

        /// <summary>Gets or sets the name of the full.</summary>
        /// <value>The name of the full.</value>
        string? FullName { get; set; }

        /// <summary>Gets or sets the phone 1.</summary>
        /// <value>The phone 1.</value>
        string? Phone1 { get; set; }

        /// <summary>Gets or sets the phone 2.</summary>
        /// <value>The phone 2.</value>
        string? Phone2 { get; set; }

        /// <summary>Gets or sets the phone 3.</summary>
        /// <value>The phone 3.</value>
        string? Phone3 { get; set; }

        /// <summary>Gets or sets the fax 1.</summary>
        /// <value>The fax 1.</value>
        string? Fax1 { get; set; }

        /// <summary>Gets or sets the email 1.</summary>
        /// <value>The email 1.</value>
        string? Email1 { get; set; }

        /// <summary>Gets or sets the website 1.</summary>
        /// <value>The website 1.</value>
        string? Website1 { get; set; }

        /// <summary>Gets or sets a value indicating whether the same as billing.</summary>
        /// <value>True if same as billing, false if not.</value>
        bool SameAsBilling { get; set; }

        /// <summary>Gets or sets the title.</summary>
        /// <value>The title.</value>
        string? Title { get; set; }

        #region Related Objects
        /// <summary>Gets or sets the identifier of the address.</summary>
        /// <value>The identifier of the address.</value>
        int? AddressID { get; set; }

        /// <summary>Gets or sets the address key.</summary>
        /// <value>The address key.</value>
        string? AddressKey { get; set; }

        /// <summary>Gets or sets the address.</summary>
        /// <value>The address.</value>
        IAddressModel? Address { get; set; }
        #endregion
    }
}
