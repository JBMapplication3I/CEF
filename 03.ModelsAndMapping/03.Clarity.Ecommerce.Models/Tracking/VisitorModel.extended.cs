// <copyright file="VisitorModel.extended.cs" company="clarity-ventures.com">
// Copyright (c) 2017-2023 clarity-ventures.com. All rights reserved.
// </copyright>
// <summary>Implements the visitor model class</summary>
namespace Clarity.Ecommerce.Models
{
    using System.Collections.Generic;
    using System.Linq;
    using Interfaces.Models;
    using Newtonsoft.Json;

    public partial class VisitorModel
    {
        /// <inheritdoc cref="IVisitorModel.Visits"/>
        [JsonIgnore]
        public List<VisitModel>? Visits { get; set; }

        /// <inheritdoc/>
        List<IVisitModel>? IVisitorModel.Visits { get => Visits?.ToList<IVisitModel>(); set => Visits = value?.Cast<VisitModel>().ToList(); }
    }
}
