// <copyright file="AmARelationshipTableModel.cs" company="clarity-ventures.com">
// Copyright (c) 2022-2023 clarity-ventures.com. All rights reserved.
// </copyright>
// <summary>Implements the am a relationship table model class</summary>
namespace Clarity.Ecommerce.MVC.Api.Models
{
    using System.Collections.Generic;
    using System.ComponentModel;
    using System.Runtime.Serialization;
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

    /// <summary>A data transfer model for the am a relationship table search base.</summary>
    /// <seealso cref="BaseSearchModel"/>
    /// <seealso cref="IAmARelationshipTableBaseSearchModel"/>
    public abstract class AmARelationshipTableBaseSearchModel
        : BaseSearchModel,
            IAmARelationshipTableBaseSearchModel
    {
        /// <inheritdoc/>
        [ApiMember(Name = nameof(MasterID), DataType = "int?", ParameterType = "query", IsRequired = false,
            Description = "The Identifier of the Master Record [Optional]")]
        public int? MasterID { get; set; }

        /// <inheritdoc/>
        [ApiMember(Name = nameof(MasterIDs), DataType = "List<int>", ParameterType = "query", IsRequired = false,
            Description = "The Identifiers of the Master Records [Optional]")]
        public List<int>? MasterIDs { get; set; }

        /// <inheritdoc/>
        [ApiMember(Name = nameof(MasterKey), DataType = "string", ParameterType = "query", IsRequired = false,
            Description = "The Custom Key of the Master Record [Optional]")]
        public string? MasterKey { get; set; }

        /// <inheritdoc/>
        [ApiMember(Name = nameof(SlaveID), DataType = "int?", ParameterType = "query", IsRequired = false,
            Description = "The Identifier of the Slave Record [Optional]")]
        public int? SlaveID { get; set; }

        /// <inheritdoc/>
        [ApiMember(Name = nameof(SlaveIDs), DataType = "List<int>", ParameterType = "query", IsRequired = false,
            Description = "The Identifiers of the Slave Records [Optional]")]
        public List<int>? SlaveIDs { get; set; }

        /// <inheritdoc/>
        [ApiMember(Name = nameof(SlaveKey), DataType = "string", ParameterType = "query", IsRequired = false,
            Description = "The Custom Key of the Slave Record [Optional]")]
        public string? SlaveKey { get; set; }
    }

    /// <summary>A data Model for the am a relationship table nameable base search.</summary>
    /// <seealso cref="AmARelationshipTableBaseSearchModel"/>
    /// <seealso cref="IAmARelationshipTableNameableBaseSearchModel"/>
    public abstract class AmARelationshipTableNameableBaseSearchModel
        : AmARelationshipTableBaseSearchModel,
            IAmARelationshipTableNameableBaseSearchModel
    {
        /// <inheritdoc/>
        [ApiMember(Name = nameof(Name), DataType = "string", ParameterType = "query", IsRequired = false,
            Description = "Will return objects with same or similar Names (Case-Insensitive)")]
        public string? Name { get; set; }

        /// <inheritdoc/>
        [ApiMember(Name = nameof(NameStrict), DataType = "bool?", ParameterType = "query", IsRequired = false,
            Description = "When true, causes the Name parameter to require exact matches")]
        public bool? NameStrict { get; set; }

        /// <inheritdoc/>
        [ApiMember(Name = nameof(Description), DataType = "string", ParameterType = "query", IsRequired = false,
            Description = "Will return objects with same or similar Descriptions (Case-Insensitive)")]
        public string? Description { get; set; }

        /// <inheritdoc/>
        [ApiMember(Name = nameof(CustomKeyOrName), DataType = "string", ParameterType = "query", IsRequired = false,
            Description = "Will return objects with same or similar Names or Custom Keys (Case-Insensitive)")]
        public string? CustomKeyOrName { get; set; }

        /// <inheritdoc/>
        [ApiMember(Name = nameof(CustomKeyOrNameOrDescription), DataType = "string", ParameterType = "query", IsRequired = false,
            Description = "Will return objects with same or similar Names, Custom Keys or Descriptions (Case-Insensitive)")]
        public string? CustomKeyOrNameOrDescription { get; set; }

        /// <inheritdoc/>
        [ApiMember(Name = nameof(IDOrCustomKeyOrName), DataType = "string", ParameterType = "query", IsRequired = false,
            Description = "Will return objects with same or similar Names, IDs or Custom Keys (Case-Insensitive)")]
        public string? IDOrCustomKeyOrName { get; set; }

        /// <inheritdoc/>
        [ApiMember(Name = nameof(IDOrCustomKeyOrNameOrDescription), DataType = "string", ParameterType = "query", IsRequired = false,
            Description = "Will return objects with same or similar Names, IDs, Custom Keys or Descriptions (Case-Insensitive)")]
        public string? IDOrCustomKeyOrNameOrDescription { get; set; }
    }

    /// <summary>Interface for am a relationship table base model.</summary>
    /// <seealso cref="IBaseModel"/>
    public interface IAmARelationshipTableBaseModel
        : IBaseModel
    {
        /// <summary>Gets or sets the identifier of the master.</summary>
        /// <value>The identifier of the master.</value>
        int MasterID { get; set; }

        /// <summary>Gets or sets the master key.</summary>
        /// <value>The master key.</value>
        string? MasterKey { get; set; }

        /// <summary>Gets or sets the identifier of the slave.</summary>
        /// <value>The identifier of the slave.</value>
        int SlaveID { get; set; }

        /// <summary>Gets or sets the slave key.</summary>
        /// <value>The slave key.</value>
        string? SlaveKey { get; set; }
    }

    /// <summary>Interface for am a relationship table base model.</summary>
    /// <typeparam name="TSlaveModel">Type of the slave model.</typeparam>
    /// <seealso cref="IAmARelationshipTableBaseModel"/>
    public interface IAmARelationshipTableBaseModel<TSlaveModel>
        : IAmARelationshipTableBaseModel
    {
        /// <summary>Gets or sets the slave.</summary>
        /// <value>The slave.</value>
        TSlaveModel? Slave { get; set; }
    }

    /// <summary>Interface for am a relationship table nameable base model.</summary>
    public interface IAmARelationshipTableNameableBaseModel
        : IAmARelationshipTableBaseModel, INameableBaseModel
    {
    }

    /// <summary>Interface for am a relationship table nameable base model.</summary>
    /// <typeparam name="TSlaveModel">Type of the slave model.</typeparam>
    /// <seealso cref="IAmARelationshipTableBaseModel"/>
    public interface IAmARelationshipTableNameableBaseModel<TSlaveModel>
        : IAmARelationshipTableBaseModel<TSlaveModel>,
            IAmARelationshipTableNameableBaseModel
    {
    }

    /// <summary>Interface for am a relationship table search model.</summary>
    public interface IAmARelationshipTableBaseSearchModel //: IBaseSearchModel
    {
        /// <summary>Gets or sets the identifier of the master.</summary>
        /// <value>The identifier of the master.</value>
        int? MasterID { get; set; }

        /// <summary>Gets or sets the master key.</summary>
        /// <value>The master key.</value>
        string? MasterKey { get; set; }

        /// <summary>Gets or sets the master IDs.</summary>
        /// <value>The master IDs.</value>
        List<int>? MasterIDs { get; set; }

        /// <summary>Gets or sets the identifier of the slave.</summary>
        /// <value>The identifier of the slave.</value>
        int? SlaveID { get; set; }

        /// <summary>Gets or sets the slave key.</summary>
        /// <value>The slave key.</value>
        string? SlaveKey { get; set; }

        /// <summary>Gets or sets the slave IDs.</summary>
        /// <value>The slave IDs.</value>
        List<int>? SlaveIDs { get; set; }
    }

    /// <summary>Interface for am a relationship table nameable base search model.</summary>
    public interface IAmARelationshipTableNameableBaseSearchModel
        : IAmARelationshipTableBaseSearchModel// , INameableBaseSearchModel
    {
    }
}
