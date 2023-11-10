// <copyright file="BaseModel.cs" company="clarity-ventures.com">
// Copyright (c) 2016-2023 clarity-ventures.com. All rights reserved.
// </copyright>
// <summary>Implements the base model class</summary>
namespace Clarity.Ecommerce.Models
{
    using System;
    using System.ComponentModel;
    using System.Runtime.Serialization;
    using Interfaces.Models;
    using Newtonsoft.Json;
    using ServiceStack;

    /// <summary>A data Model for the base.</summary>
    /// <seealso cref="IBaseModel"/>
    public abstract class BaseModel : IBaseModel, Interfaces.DataModel.ICloneable
    {
        /// <inheritdoc/>
        [DefaultValue(0),
            DataMember(EmitDefaultValue = false),
            ApiMember(Name = nameof(ID), DataType = "int", ParameterType = "body", IsRequired = false,
                Description = "Identifier of the object (required when submitting Updates, must be blank or 0 for Creates")]
        public int ID { get; set; }

        /// <inheritdoc/>
        [DefaultValue(null),
            DataMember(EmitDefaultValue = false),
            ApiMember(Name = nameof(CustomKey), DataType = "string", ParameterType = "body", IsRequired = false,
            Description = "Custom Key of the object, optional when not using an ERP integration, max 128 characters")]
        public string? CustomKey { get; set; }

        /// <inheritdoc/>
        [DefaultValue(false),
            DataMember(EmitDefaultValue = false),
            ApiMember(Name = nameof(Active), DataType = "bool", ParameterType = "body", IsRequired = false,
                Description = "Whether the object is active. Inactive objects are essentially 'Soft-Deleted'. Ensure you set this to true to keep the object alive!")]
        public bool Active { get; set; } = true;

        /// <inheritdoc/>
        [DefaultValue(null),
            DataMember(EmitDefaultValue = false),
            ApiMember(Name = nameof(CreatedDate), DataType = "string", ParameterType = "body", IsRequired = false,
                Description = "The original date this object was created. Not required for Creates, will be automatically set")]
        public DateTime CreatedDate { get; set; } = DateExtensions.GenDateTime;

        /// <inheritdoc/>
        [DefaultValue(null),
            DataMember(EmitDefaultValue = false),
            ApiMember(Name = nameof(UpdatedDate), DataType = "string", ParameterType = "body", IsRequired = false,
                Description = "The last time the object was modified. Not Required for Updates, will be automatically set")]
        public DateTime? UpdatedDate { get; set; }

        /// <inheritdoc/>
        [DefaultValue(null),
            DataMember(EmitDefaultValue = false),
            ApiMember(Name = nameof(Hash), DataType = "long?", ParameterType = "body", IsRequired = false,
                Description = "The Hash of the object data points coming from Clarity Connect")]
        public long? Hash { get; set; }

        /// <inheritdoc/>
        [DefaultValue(null),
            DataMember(EmitDefaultValue = false),
            ApiMember(Name = nameof(SerializableAttributes), DataType = "SerializableAttributes", ParameterType = "body", IsRequired = true,
                Description = "A dictionary of extending Attributes for the this object")]
        public SerializableAttributesDictionary SerializableAttributes { get; set; } = null!;

        /// <summary>Should serialize.</summary>
        /// <param name="name">The name.</param>
        /// <returns>A bool?.</returns>
        public bool? ShouldSerialize(string name) => this.IgnoreEmptyData(name);

        #region IClonable
        /// <inheritdoc/>
        public virtual object Clone()
        {
            return MemberwiseClone();
        }

        /// <inheritdoc/>
        public virtual string ToHashableString()
        {
            var builder = new System.Text.StringBuilder();
            // Base
            builder.AppendLine("Base");
            builder.Append("A: ").Append(Active).AppendLine();
            builder.Append("J: ").AppendLine(SerializableAttributes.SerializeAttributesDictionary());
            // Return
            return builder.ToString();
        }
        #endregion

        /// <inheritdoc/>
        public override bool Equals(object? obj)
        {
            return Equals(obj as BaseModel);
        }

        /// <inheritdoc/>
        public override int GetHashCode()
        {
#if NET5_0_OR_GREATER
            return HashCode.Combine(ID, CustomKey, Active, SerializableAttributes);
#else
            unchecked
            {
                // ReSharper disable NonReadonlyMemberInGetHashCode
                var hashCode = ID.GetHashCode();
                hashCode = (hashCode * 397) ^ (CustomKey != null ? CustomKey.GetHashCode() : 0);
                hashCode = (hashCode * 397) ^ Active.GetHashCode();
                hashCode = (hashCode * 397) ^ (SerializableAttributes != null ? SerializableAttributes.GetHashCode() : 0);
                // Ignore CreatedDate, UpdatedDate and Hash values
                return hashCode;
                // ReSharper restore NonReadonlyMemberInGetHashCode
            }
#endif
        }

        /// <inheritdoc/>
        public override string ToString()
        {
            return JsonConvert.SerializeObject(
                this,
                SerializableAttributesDictionaryExtensions.JsonSettings);
        }

        /// <summary>Tests if this BaseModel is considered equal to another.</summary>
        /// <param name="other">The base model to compare to this BaseModel.</param>
        /// <returns>True if the objects are considered equal, false if they are not.</returns>
        protected bool Equals(BaseModel? other)
        {
            if (other == null)
            {
                return false;
            }
            return ID == other.ID
                && string.Equals(CustomKey, other.CustomKey)
                && Active == other.Active
                && Equals(SerializableAttributes, other.SerializableAttributes);
        }
    }
}
