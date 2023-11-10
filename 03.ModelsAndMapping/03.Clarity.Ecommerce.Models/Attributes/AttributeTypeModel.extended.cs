// <copyright file="AttributeTypeModel.extended.cs" company="clarity-ventures.com">
// Copyright (c) 2016-2023 clarity-ventures.com. All rights reserved.
// </copyright>
// <summary>Implements the attribute type model class</summary>
namespace Clarity.Ecommerce.Models
{
    using System.Collections.Generic;
    using System.Linq;
    using Interfaces.Models;

    public partial class AttributeTypeModel
    {
        /// <inheritdoc cref="IAttributeTypeModel.GeneralAttributes"/>
        public List<GeneralAttributeModel>? GeneralAttributes { get; set; }

        /// <inheritdoc/>
        List<IGeneralAttributeModel>? IAttributeTypeModel.GeneralAttributes { get => GeneralAttributes?.ToList<IGeneralAttributeModel>(); set => GeneralAttributes = value?.Cast<GeneralAttributeModel>().ToList(); }
    }
}
