// <copyright file="AmARelationshipTableBaseModel.cs" company="clarity-ventures.com">
// Copyright (c) 2022-2023 clarity-ventures.com. All rights reserved.
// </copyright>
// <summary>Implements the am a relationship table model class</summary>
namespace Clarity.Ecommerce.Models
{
    using System.ComponentModel;
    using System.Runtime.Serialization;
    using Interfaces.Models;
    using ServiceStack;

    /// <summary>A data transfer model for the am a relationship table.</summary>
    /// <seealso cref="BaseModel"/>
    /// <seealso cref="IAmARelationshipTableBaseModel"/>
    public abstract class AmARelationshipTableBaseModel
        : BaseModel,
            IAmARelationshipTableBaseModel
    {
        /// <inheritdoc/>
        [DefaultValue(0),
            DataMember(EmitDefaultValue = false),
            ApiMember(Name = nameof(MasterID), DataType = "int", ParameterType = "body", IsRequired = true)]
        public int MasterID { get; set; }

        /// <inheritdoc/>
        [DefaultValue(null),
            DataMember(EmitDefaultValue = false),
            ApiMember(Name = nameof(MasterKey), DataType = "string?", ParameterType = "body", IsRequired = false)]
        public string? MasterKey { get; set; }

        /// <inheritdoc/>
        [DefaultValue(0),
            DataMember(EmitDefaultValue = false),
            ApiMember(Name = nameof(SlaveID), DataType = "int", ParameterType = "body", IsRequired = true)]
        public int SlaveID { get; set; }

        /// <inheritdoc/>
        [DefaultValue(null),
            DataMember(EmitDefaultValue = false),
            ApiMember(Name = nameof(SlaveKey), DataType = "string?", ParameterType = "body", IsRequired = false)]
        public string? SlaveKey { get; set; }
    }

    /// <summary>A data transfer model for the am a relationship table base.</summary>
    /// <typeparam name="TSlaveModel">Type of the slave model.</typeparam>
    /// <seealso cref="AmARelationshipTableBaseModel"/>
    /// <seealso cref="IAmARelationshipTableBaseModel{TSlaveModel}"/>
    public abstract class AmARelationshipTableBaseModel<TSlaveModel>
        : AmARelationshipTableBaseModel,
            IAmARelationshipTableBaseModel<TSlaveModel>
    {
        /// <inheritdoc/>
        [DefaultValue(null),
            DataMember(EmitDefaultValue = false),
            ApiMember(Name = nameof(Slave), DataType = "TSlaveModel?", ParameterType = "body", IsRequired = false)]
        public TSlaveModel? Slave { get; set; }
    }

    /// <summary>A data transfer model for the am a relationship table.</summary>
    /// <seealso cref="NameableBaseModel"/>
    /// <seealso cref="IAmARelationshipTableBaseModel"/>
    public abstract class AmARelationshipTableNameableBaseModel
        : AmARelationshipTableBaseModel,
            IAmARelationshipTableNameableBaseModel
    {
        /// <inheritdoc/>
        [DefaultValue(null),
            DataMember(EmitDefaultValue = false),
            ApiMember(Name = nameof(Name), DataType = "string", ParameterType = "body", IsRequired = true,
                Description = "Name of the object, required, max length 128 characters")]
        public string? Name { get; set; }

        /// <inheritdoc/>
        [DefaultValue(null),
            DataMember(EmitDefaultValue = false),
            ApiMember(Name = nameof(Description), DataType = "string", ParameterType = "body", IsRequired = false,
                Description = "Description of the object, optional, no max length and ideal for storing long HTML content")]
        public string? Description { get; set; }

        #region ICloneable
        /// <inheritdoc/>
        public override string ToHashableString()
        {
            var builder = new System.Text.StringBuilder(base.ToHashableString());
            // Base
            builder.AppendLine("NameableBase");
            builder.Append("N: ").AppendLine(Name ?? string.Empty);
            builder.Append("D: ").AppendLine(Description ?? string.Empty);
            // Return
            return builder.ToString();
        }
        #endregion
    }

    /// <summary>A data transfer model for the am a relationship table base.</summary>
    /// <typeparam name="TSlaveModel">Type of the slave model.</typeparam>
    /// <seealso cref="IAmARelationshipTableNameableBaseModel{TSlaveModel}"/>
    /// <seealso cref="AmARelationshipTableNameableBaseModel"/>
    public abstract class AmARelationshipTableNameableBaseModel<TSlaveModel>
        : AmARelationshipTableNameableBaseModel,
            IAmARelationshipTableNameableBaseModel<TSlaveModel>
    {
        /// <inheritdoc/>
        [DefaultValue(null),
            DataMember(EmitDefaultValue = false),
            ApiMember(Name = nameof(Slave), DataType = "TSlaveModel?", ParameterType = "body", IsRequired = false)]
        public TSlaveModel? Slave { get; set; }
    }
}
