// <copyright file="IAmARelationshipTableBaseSearchModel.cs" company="clarity-ventures.com">
// Copyright (c) 2019-2023 clarity-ventures.com. All rights reserved.
// </copyright>
// <summary>Declares the IAmARelationshipTableBaseSearchModel interface</summary>
namespace Clarity.Ecommerce.Interfaces.Models
{
    using System.Collections.Generic;

    /// <summary>Interface for am a relationship table search model.</summary>
    public interface IAmARelationshipTableBaseSearchModel
        : IBaseSearchModel
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
        : IAmARelationshipTableBaseSearchModel,
            INameableBaseSearchModel
    {
    }
}
