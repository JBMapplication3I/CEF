// <copyright file="AttrModel.cs" company="clarity-ventures.com">
// Copyright (c) 2017-2023 clarity-ventures.com. All rights reserved.
// </copyright>
// <summary>Implements the attribute model class</summary>
namespace Clarity.Ecommerce.Providers.Searching.ElasticSearch
{
    /// <summary>A data Model for the attribute.</summary>
    public class AttrModel
    {
        /// <summary>Gets or sets the identifier.</summary>
        /// <value>The identifier.</value>
        public int ID { get; set; }

        /// <summary>Gets or sets the custom key.</summary>
        /// <value>The custom key.</value>
        public string? CustomKey { get; set; }

        /// <summary>Gets or sets a value indicating whether the active.</summary>
        /// <value>True if active, false if not.</value>
        public bool Active { get; set; }

        /// <summary>Gets or sets the name.</summary>
        /// <value>The name.</value>
        public string? Name { get; set; }

        /// <summary>Gets or sets the name of the display.</summary>
        /// <value>The name of the display.</value>
        public string? DisplayName { get; set; }

        /// <summary>Gets or sets the sort order.</summary>
        /// <value>The sort order.</value>
        public int? SortOrder { get; set; }

        /// <summary>Gets or sets a value indicating whether this AttrModel is filter.</summary>
        /// <value>True if this AttrModel is filter, false if not.</value>
        public bool IsFilter { get; set; }

        /// <summary>Gets or sets the group.</summary>
        /// <value>The group.</value>
        public string? Group { get; set; }

        /// <summary>Gets or sets the identifier of the type.</summary>
        /// <value>The identifier of the type.</value>
        public int? TypeID { get; set; }
    }
}
