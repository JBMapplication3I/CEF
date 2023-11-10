// <copyright file="RecordVersionSearchModel.extended.cs" company="clarity-ventures.com">
// Copyright (c) 2020-2023 clarity-ventures.com. All rights reserved.
// </copyright>
// <summary>Implements the record version search model class</summary>
namespace Clarity.Ecommerce.Models
{
    using System;
    using System.ComponentModel;
    using ServiceStack;

    public partial class RecordVersionSearchModel
    {
        /// <inheritdoc/>
        [DefaultValue(null),
         ApiMember(Name = nameof(MinEitherPublishDate), DataType = "DateTime?", ParameterType = "query", IsRequired = false,
             Description = "The minimum publish date (of either value) to filter to, optional/nullable.")]
        public DateTime? MinEitherPublishDate { get; set; }

        /// <inheritdoc/>
        [DefaultValue(null),
         ApiMember(Name = nameof(MaxEitherPublishDate), DataType = "DateTime?", ParameterType = "query", IsRequired = false,
             Description = "The minimum publish (of either value) date to filter to, optional/nullable.")]
        public DateTime? MaxEitherPublishDate { get; set; }
    }
}
