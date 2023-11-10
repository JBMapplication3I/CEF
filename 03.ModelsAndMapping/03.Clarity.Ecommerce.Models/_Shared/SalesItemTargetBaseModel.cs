// <copyright file="SalesItemTargetBaseModel.cs" company="clarity-ventures.com">
// Copyright (c) 2018-2023 clarity-ventures.com. All rights reserved.
// </copyright>
// <summary>Implements the sales item target base model class</summary>
namespace Clarity.Ecommerce.Models
{
    using System.ComponentModel;
    using System.Globalization;
    using System.Runtime.Serialization;
    using Interfaces.Models;
    using Newtonsoft.Json;
    using ServiceStack;

    /// <summary>A data Model for the sales item target base.</summary>
    /// <seealso cref="BaseModel"/>
    /// <seealso cref="ISalesItemTargetBaseModel"/>
    public class SalesItemTargetBaseModel : BaseModel, ISalesItemTargetBaseModel
    {
        #region IHaveATypeBaseModel<ITypeModel>
        /// <inheritdoc/>
        [ApiMember(Name = nameof(TypeID), DataType = "int", ParameterType = "body", IsRequired = true,
            Description = "Identifier for the Type of this Account, required if no TypeModel present"), DefaultValue(0)]
        public int TypeID { get; set; }

        /// <inheritdoc cref="IHaveATypeBaseModel{ITypeModel}.Type"/>
        [ApiMember(Name = nameof(Type), DataType = "TypeModel", ParameterType = "body", IsRequired = false,
            Description = "Model for Type of this object, required if no TypeID present"), DefaultValue(null)]
        public TypeModel? Type { get; set; }

        /// <inheritdoc/>
        ITypeModel? IHaveATypeBaseModel<ITypeModel>.Type { get => Type; set => Type = (TypeModel?)value; }

        /// <inheritdoc/>
        [ApiMember(Name = nameof(TypeKey), DataType = "string", ParameterType = "body", IsRequired = false,
            Description = "Key for the Type of this object, read-only"), DefaultValue(null)]
        public string? TypeKey { get; set; }

        /// <inheritdoc/>
        [ApiMember(Name = nameof(TypeName), DataType = "string", ParameterType = "body", IsRequired = false,
            Description = "Name for the Type of this object, read-only"), DefaultValue(null)]
        public string? TypeName { get; set; }

        /// <inheritdoc/>
        [ApiMember(Name = nameof(TypeDisplayName), DataType = "string", ParameterType = "body", IsRequired = false,
            Description = "Display Name for the Type of this object, read-only"), DefaultValue(null)]
        public string? TypeDisplayName { get; set; }

        /// <inheritdoc/>
        [ApiMember(Name = nameof(TypeTranslationKey), DataType = "string", ParameterType = "body", IsRequired = false,
            Description = "Translation Key for the Type of this object, read-only"), DefaultValue(null)]
        public string? TypeTranslationKey { get; set; }

        /// <inheritdoc/>
        [ApiMember(Name = nameof(TypeSortOrder), DataType = "int?", ParameterType = "body", IsRequired = false,
            Description = "Sort Order for the Type of this object, read-only"), DefaultValue(null)]
        public int? TypeSortOrder { get; set; }
        #endregion

        /// <inheritdoc/>
        [ApiMember(Name = nameof(Quantity), DataType = "decimal", ParameterType = "body", IsRequired = false), DefaultValue(0)]
        public decimal Quantity { get; set; }

        /// <inheritdoc/>
        [ApiMember(Name = nameof(NothingToShip), DataType = "bool", ParameterType = "body", IsRequired = false), DefaultValue(false)]
        public bool NothingToShip { get; set; }

        /// <inheritdoc/>
        [ApiMember(Name = nameof(MasterID), DataType = "int", ParameterType = "body", IsRequired = false), DefaultValue(0)]
        public int MasterID { get; set; }

        /// <inheritdoc/>
        [JsonIgnore, ApiMember(ExcludeInSchema = true), IgnoreDataMember]
        public string? CustomSplitKey { get; set; }

        /// <inheritdoc/>
        [ApiMember(Name = nameof(DestinationContactID), DataType = "int", ParameterType = "body", IsRequired = false), DefaultValue(0)]
        public int DestinationContactID { get; set; }

        /// <inheritdoc/>
        [ApiMember(Name = nameof(DestinationContactKey), DataType = "string", ParameterType = "body", IsRequired = false), DefaultValue(null)]
        public string? DestinationContactKey { get; set; }

        /// <inheritdoc cref="ISalesItemTargetBaseModel.DestinationContact"/>
        [ApiMember(Name = nameof(DestinationContact), DataType = "ContactModel", ParameterType = "body", IsRequired = false), DefaultValue(null)]
        public ContactModel? DestinationContact { get; set; }

        /// <inheritdoc/>
        IContactModel? ISalesItemTargetBaseModel.DestinationContact { get => DestinationContact; set => DestinationContact = (ContactModel?)value; }

        /// <inheritdoc/>
        [ApiMember(Name = nameof(BrandProductID), DataType = "int?", ParameterType = "body", IsRequired = false), DefaultValue(null)]
        public int? BrandProductID { get; set; }

        /// <inheritdoc/>
        [ApiMember(Name = nameof(BrandProductKey), DataType = "string", ParameterType = "body", IsRequired = false), DefaultValue(null)]
        public string? BrandProductKey { get; set; }

        /// <inheritdoc cref="ISalesItemTargetBaseModel.BrandProduct"/>
        [ApiMember(Name = nameof(BrandProduct), DataType = "StoreProductModel", ParameterType = "body", IsRequired = false), DefaultValue(null)]
        public BrandProductModel? BrandProduct { get; set; }

        /// <inheritdoc/>
        IBrandProductModel? ISalesItemTargetBaseModel.BrandProduct { get => BrandProduct; set => BrandProduct = (BrandProductModel?)value; }

        /// <inheritdoc/>
        [ApiMember(Name = nameof(SelectedRateQuoteID), DataType = "int?", ParameterType = "body", IsRequired = false), DefaultValue(null)]
        public int? SelectedRateQuoteID { get; set; }

        /// <inheritdoc/>
        [ApiMember(Name = nameof(SelectedRateQuoteKey), DataType = "string", ParameterType = "body", IsRequired = false), DefaultValue(null)]
        public string? SelectedRateQuoteKey { get; set; }

        /// <inheritdoc/>
        [ApiMember(Name = nameof(SelectedRateQuoteName), DataType = "string", ParameterType = "body", IsRequired = false), DefaultValue(null)]
        public string? SelectedRateQuoteName { get; set; }

        /// <inheritdoc cref="ISalesItemTargetBaseModel.SelectedRateQuote"/>
        [ApiMember(Name = nameof(SelectedRateQuote), DataType = "RateQuoteModel", ParameterType = "body", IsRequired = false), DefaultValue(null)]
        public RateQuoteModel? SelectedRateQuote { get; set; }

        /// <inheritdoc/>
        IRateQuoteModel? ISalesItemTargetBaseModel.SelectedRateQuote { get => SelectedRateQuote; set => SelectedRateQuote = (RateQuoteModel?)value; }

        /// <inheritdoc/>
        public override string ToString()
        {
            return JsonConvert.SerializeObject(this, SerializableAttributesDictionaryExtensions.JsonSettings);
        }

        #region IClonable
        /// <inheritdoc/>
        public override string ToHashableString()
        {
            // Base
            var builder = new System.Text.StringBuilder(base.ToHashableString());
            // IHaveAType
            builder.Append("T: ").AppendLine(Type?.ToHashableString() ?? $"No Type={TypeID}");
            // SalesItemTargetBase
            builder.Append("Q: ").AppendLine(Quantity.ToString("n5", CultureInfo.InvariantCulture));
            builder.Append("N: ").AppendLine(NothingToShip.ToString());
            // Related Objects
            builder.Append("B: ").AppendLine(BrandProduct?.ToHashableString() ?? $"No BrandProduct={BrandProductID}");
            builder.Append("D: ").AppendLine(DestinationContact?.ToHashableString() ?? $"No DestinationContact={DestinationContactID}");
            // Return
            return builder.ToString();
        }
        #endregion
    }
}
