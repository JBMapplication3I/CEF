// <copyright file="IAmAContactRelationshipTableModel.cs" company="clarity-ventures.com">
// Copyright (c) 2017-2023 clarity-ventures.com. All rights reserved.
// </copyright>
// <summary>Declares the IAmAContactRelationshipTableModel interface</summary>
namespace Clarity.Ecommerce.Interfaces.Models
{
    /// <summary>Interface for am a contact relationship table model.</summary>
    public interface IAmAContactRelationshipTableModel
        : IAmARelationshipTableBaseModel<IContactModel>,
            IHaveAContactBaseModel
    {
    }
}
