// <copyright file="Base.cs" company="clarity-ventures.com">
// Copyright (c) 2020-2023 clarity-ventures.com. All rights reserved.
// </copyright>
// <summary>Implements the base class</summary>
#nullable enable
namespace Clarity.Ecommerce.DataModel
{
    using System;
    using System.ComponentModel;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using Interfaces.DataModel;
    using Interfaces.Models;
    using Newtonsoft.Json;

    /// <summary>A data model base.</summary>
    /// <seealso cref="IBase"/>
    public abstract class Base : IBase
    {
        /// <inheritdoc/>
        [Key, Index, DatabaseGenerated(DatabaseGeneratedOption.Identity), DefaultValue(0)]
        public virtual int ID { get; set; }

        /// <inheritdoc/>
        [StringLength(128), StringIsUnicode(false), Index, DefaultValue(null)]
        public virtual string? CustomKey { get; set; }

        /// <inheritdoc/>
        [/*Column(TypeName = "datetime2"), DateTimePrecision(7),*/ Index]
        public virtual DateTime CreatedDate { get; set; }

        /// <inheritdoc/>
        [/*Column(TypeName = "datetime2"), DateTimePrecision(7),*/ Index, DefaultValue(null), DontMapOutWithListing]
        public virtual DateTime? UpdatedDate { get; set; }

        /// <inheritdoc/>
        [Index, DefaultValue(true)]
        public virtual bool Active { get; set; } = true;

        /// <inheritdoc/>
        [Index, DefaultValue(null)]
        public virtual long? Hash { get; set; } = null;

        /// <inheritdoc/>
        [DefaultValue("{}")]
        public virtual string? JsonAttributes { get; set; } = "{}";

        /// <inheritdoc/>
        /// <remarks>This value is read-only as it just deserializes the <see cref="JsonAttributes"/> property.</remarks>
        [NotMapped, ReadOnly(true), JsonIgnore]
        public virtual SerializableAttributesDictionary SerializableAttributes => JsonAttributes!.DeserializeAttributesDictionary();

        /// <inheritdoc/>
        public override string ToString()
        {
            return JsonConvert.SerializeObject(this, SerializableAttributesDictionaryExtensions.JsonSettings);
        }

        #region ICloneable
        /// <inheritdoc/>
        public virtual object Clone()
        {
            var clone = MemberwiseClone();
            return clone;
        }

        /// <summary>Converts this Base to a hash-able string.</summary>
        /// <returns>This Base as a string.</returns>
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
    }
}
