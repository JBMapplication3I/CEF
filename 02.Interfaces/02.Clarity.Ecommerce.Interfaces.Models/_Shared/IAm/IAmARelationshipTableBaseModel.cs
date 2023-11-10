// <copyright file="IAmARelationshipTableBaseModel.cs" company="clarity-ventures.com">
// Copyright (c) 2016-2023 clarity-ventures.com. All rights reserved.
// </copyright>
// <summary>Declares the IAmARelationshipTableBaseModel interface</summary>
namespace Clarity.Ecommerce.Interfaces.Models
{
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
}
