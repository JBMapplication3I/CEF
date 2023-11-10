// <copyright file="NoteTypeModel.extended.cs" company="clarity-ventures.com">
// Copyright (c) 2016-2023 clarity-ventures.com. All rights reserved.
// </copyright>
// <summary>Implements the note type model class</summary>
namespace Clarity.Ecommerce.Models
{
    using ServiceStack;

    /// <summary>A data Model for the note type.</summary>
    /// <seealso cref="TypableBaseModel"/>
    /// <seealso cref="Interfaces.Models.INoteTypeModel"/>
    public partial class NoteTypeModel
    {
        /// <inheritdoc/>
        [ApiMember(Name = nameof(IsPublic), DataType = "bool", ParameterType = "body", IsRequired = false,
            Description = "Note Is Public")]
        public bool IsPublic { get; set; }

        /// <inheritdoc/>
        [ApiMember(Name = nameof(IsCustomer), DataType = "bool", ParameterType = "body", IsRequired = false,
            Description = "Note Is Customer")]
        public bool IsCustomer { get; set; }
    }
}
