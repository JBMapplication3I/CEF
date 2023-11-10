// <copyright file="BulkOrderHeadersModel.cs" company="clarity-ventures.com">
// Copyright (c) 2017-2023 clarity-ventures.com. All rights reserved.
// </copyright>
// <summary>Implements the bulk order headers model class</summary>
namespace Clarity.Ecommerce.Models
{
    using System.Collections.Generic;

    /// <summary>A data Model for the bulk order headers.</summary>
    public class BulkOrderHeadersModel
    {
        /// <summary>Gets or sets the file headers.</summary>
        /// <value>The file headers.</value>
        public IEnumerable<string>? FileHeaders { get; set; }

        /// <summary>Gets or sets the system headers.</summary>
        /// <value>The system headers.</value>
        public IEnumerable<string>? SystemHeaders { get; set; }
    }
}
