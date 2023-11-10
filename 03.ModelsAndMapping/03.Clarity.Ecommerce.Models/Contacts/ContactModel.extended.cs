// <copyright file="ContactModel.extended.cs" company="clarity-ventures.com">
// Copyright (c) 2016-2023 clarity-ventures.com. All rights reserved.
// </copyright>
// <summary>Implements the contact model class</summary>
// ReSharper disable MissingXmlDoc
namespace Clarity.Ecommerce.Models
{
    using System.Collections.Generic;
    using System.Linq;
    using Interfaces.Models;
    using Newtonsoft.Json;
    using ServiceStack;

    /// <summary>A data Model for the contact.</summary>
    /// <seealso cref="BaseModel"/>
    /// <seealso cref="IContactModel"/>
    public class ContactModel : BaseModel, IContactModel
    {
        #region IHaveATypeBaseModel<ITypeModel>
        /// <inheritdoc/>
        [ApiMember(Name = nameof(TypeID), DataType = "int?", ParameterType = "body", IsRequired = true,
            Description = "Identifier for the Type of this object, optional")]
        public int TypeID { get; set; }

        /// <inheritdoc cref="IHaveATypeBaseModel{ITypeModel}.Type"/>
        [ApiMember(Name = nameof(Type), DataType = "TypeModel", ParameterType = "body", IsRequired = true,
            Description = "Model for Type of this object, optional")]
        public TypeModel? Type { get; set; }

        /// <inheritdoc/>
        ITypeModel? IHaveATypeBaseModel<ITypeModel>.Type { get => Type; set => Type = (TypeModel?)value; }

        /// <inheritdoc/>
        [ApiMember(Name = nameof(TypeKey), DataType = "string", ParameterType = "body", IsRequired = true,
            Description = "Key for the Type of this object, read-only")]
        public string? TypeKey { get; set; }

        /// <inheritdoc/>
        [ApiMember(Name = nameof(TypeName), DataType = "string", ParameterType = "body", IsRequired = true,
            Description = "Name for the Type of this object, read-only")]
        public string? TypeName { get; set; }

        /// <inheritdoc/>
        [ApiMember(Name = nameof(TypeDisplayName), DataType = "string", ParameterType = "body", IsRequired = true,
            Description = "Display Name for the Type of this object, read-only")]
        public string? TypeDisplayName { get; set; }

        /// <inheritdoc/>
        [ApiMember(Name = nameof(TypeTranslationKey), DataType = "string", ParameterType = "body", IsRequired = true,
            Description = "Translation Key for the Type of this object, read-only")]
        public string? TypeTranslationKey { get; set; }

        /// <inheritdoc/>
        [ApiMember(Name = nameof(TypeSortOrder), DataType = "int?", ParameterType = "body", IsRequired = true,
            Description = "Sort Order for the Type of this object, read-only")]
        public int? TypeSortOrder { get; set; }
        #endregion

        #region IAmFilterableByStoreModel
        /// <inheritdoc cref="IAmFilterableByStoreModel{IStoreModel}.Stores"/>
        [JsonIgnore]
        public List<StoreModel>? Stores { get; set; }

        /// <inheritdoc/>
        List<IStoreModel>? IAmFilterableByStoreModel<IStoreModel>.Stores { get => Stores?.ToList<IStoreModel>(); set => Stores = value?.Cast<StoreModel>().ToList(); }
        #endregion

        #region IHaveImagesBaseModel
        /// <inheritdoc cref="IHaveImagesBaseModel{IContactImageModel, ITypeModel}.Images"/>
        public List<ContactImageModel>? Images { get; set; }

        /// <inheritdoc/>
        List<IContactImageModel>? IHaveImagesBaseModel<IContactImageModel, ITypeModel>.Images { get => Images?.ToList<IContactImageModel>(); set => Images = value?.Cast<ContactImageModel>().ToList(); }

        /// <inheritdoc/>
        [ApiMember(Name = nameof(PrimaryImageFileName), DataType = "string", ParameterType = "body", IsRequired = false,
            Description = "The primary image from the list of images, or the first image if no primary is set (read-only)")]
        public string? PrimaryImageFileName { get; set; }
        #endregion

        #region Contact Properties
        /// <inheritdoc/>
        public string? FirstName { get; set; }

        /// <inheritdoc/>
        public string? MiddleName { get; set; }

        /// <inheritdoc/>
        public string? LastName { get; set; }

        /// <inheritdoc/>
        public string? FullName { get; set; }

        /// <inheritdoc/>
        public string? Phone1 { get; set; }

        /// <inheritdoc/>
        public string? Phone2 { get; set; }

        /// <inheritdoc/>
        public string? Phone3 { get; set; }

        /// <inheritdoc/>
        public string? Fax1 { get; set; }

        /// <inheritdoc/>
        public string? Email1 { get; set; }

        /// <inheritdoc/>
        public string? Website1 { get; set; }

        /// <inheritdoc/>
        public bool SameAsBilling { get; set; }

        /// <inheritdoc/>
        public string? Title { get; set; }
        #endregion

        #region Related Objects
        /// <inheritdoc/>
        public int? AddressID { get; set; }

        /// <inheritdoc/>
        public string? AddressKey { get; set; }

        /// <inheritdoc cref="IContactModel.Address"/>
        public AddressModel? Address { get; set; }

        /// <inheritdoc/>
        IAddressModel? IContactModel.Address { get => Address; set => Address = (AddressModel?)value; }
        #endregion

        #region IClonable
        /// <inheritdoc/>
        public override string ToHashableString()
        {
            // Base
            var builder = new System.Text.StringBuilder(base.ToHashableString());
            // Contact
            builder.Append("CK: ").AppendLine(CustomKey ?? string.Empty); // Contact Needs the key in case multiple address have same data
            builder.Append("FN: ").AppendLine(FirstName ?? string.Empty);
            builder.Append("MN: ").AppendLine(MiddleName ?? string.Empty);
            builder.Append("LN: ").AppendLine(LastName ?? string.Empty);
            builder.Append("Fl: ").AppendLine(FullName ?? string.Empty);
            builder.Append("P1: ").AppendLine(Phone1 ?? string.Empty);
            builder.Append("P2: ").AppendLine(Phone2 ?? string.Empty);
            builder.Append("P3: ").AppendLine(Phone3 ?? string.Empty);
            builder.Append("Fx: ").AppendLine(Fax1 ?? string.Empty);
            builder.Append("E1: ").AppendLine(Email1 ?? string.Empty);
            builder.Append("W1: ").AppendLine(Website1 ?? string.Empty);
            // Related Objects
            builder.Append("T: ").AppendLine(Type?.ToHashableString() ?? $"No Type={TypeID}");
            builder.Append("Ad: ").AppendLine(Address?.ToHashableString() ?? $"No Ad={AddressID}");
            // Return
            return builder.ToString();
        }
        #endregion
    }
}
