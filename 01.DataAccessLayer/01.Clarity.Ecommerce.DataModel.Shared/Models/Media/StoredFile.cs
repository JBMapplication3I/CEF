// <copyright file="StoredFile.cs" company="clarity-ventures.com">
// Copyright (c) 2017-2023 clarity-ventures.com. All rights reserved.
// </copyright>
// <summary>Implements the stored file class</summary>
#nullable enable
namespace Clarity.Ecommerce.Interfaces.DataModel
{
    using System.Collections.Generic;
    using Ecommerce.DataModel;

    public interface IStoredFile : INameableBase
    {
        #region Displaying it info (and metadata)
        /// <summary>Gets or sets the sort order.</summary>
        /// <value>The sort order.</value>
        int? SortOrder { get; set; }

        /// <summary>Gets or sets the name of the display.</summary>
        /// <value>The name of the display.</value>
        string? DisplayName { get; set; }

        /// <summary>Gets or sets the seo title.</summary>
        /// <value>The seo title.</value>
        string? SeoTitle { get; set; }

        /// <summary>Gets or sets the author.</summary>
        /// <value>The author.</value>
        string? Author { get; set; }

        /// <summary>Gets or sets the copyright.</summary>
        /// <value>The copyright.</value>
        string? Copyright { get; set; }
        #endregion

        #region The file as uploaded
        /// <summary>Gets or sets the file format.</summary>
        /// <value>The file format.</value>
        string? FileFormat { get; set; }

        /// <summary>Gets or sets the filename of the file.</summary>
        /// <value>The name of the file.</value>
        string? FileName { get; set; }

        /// <summary>Gets or sets a value indicating whether this IStoredFile is stored in database.</summary>
        /// <value>True if this IStoredFile is stored in database, false if not.</value>
        bool IsStoredInDB { get; set; }

        /// <summary>Gets or sets the bytes.</summary>
        /// <value>The bytes.</value>
        byte[]? Bytes { get; set; }
        #endregion

        #region Associated Objects
        /// <summary>Gets or sets the account files.</summary>
        /// <value>The account files.</value>
        ICollection<AccountFile> AccountFiles { get; set; }

        /// <summary>Gets or sets the category files.</summary>
        /// <value>The category files.</value>
        ICollection<CategoryFile> CategoryFiles { get; set; }
        #endregion
    }
}

namespace Clarity.Ecommerce.DataModel
{
    using System.Collections.Generic;
    using System.ComponentModel;
    using System.ComponentModel.DataAnnotations;
    using Interfaces.DataModel;
    using Newtonsoft.Json;

    [SqlSchema("Media", "StoredFile")]
    public class StoredFile : NameableBase, IStoredFile
    {
        private ICollection<AccountFile> accountFiles;
        private ICollection<CategoryFile> categoryFiles;

        public StoredFile()
        {
            accountFiles = new HashSet<AccountFile>();
            categoryFiles = new HashSet<CategoryFile>();
        }

        #region Displaying it info (and metadata)
        /// <inheritdoc/>
        [DefaultValue(null)]
        public int? SortOrder { get; set; }

        /// <inheritdoc/>
        [StringLength(128), DefaultValue(null)]
        public string? DisplayName { get; set; }

        /// <inheritdoc/>
        [StringLength(75), DefaultValue(null)]
        public string? SeoTitle { get; set; }

        /// <inheritdoc/>
        [StringLength(512), DefaultValue(null)]
        public string? Author { get; set; }

        /// <inheritdoc/>
        [StringLength(512), DefaultValue(null)]
        public string? Copyright { get; set; }
        #endregion

        #region The file as uploaded
        /// <inheritdoc/>
        [StringLength(512), DefaultValue(null)]
        public string? FileFormat { get; set; }

        /// <inheritdoc/>
        [StringLength(512), DefaultValue(null)]
        public string? FileName { get; set; }

        /// <inheritdoc/>
        [DefaultValue(false)]
        public bool IsStoredInDB { get; set; }

        /// <inheritdoc/>
        [DontMapOutWithLite, DefaultValue(null)]
        public byte[]? Bytes { get; set; }
        #endregion

        #region Associated Objects
        /// <inheritdoc/>
        [DontMapInEver, DontMapOutEver, DefaultValue(null), JsonIgnore]
        public virtual ICollection<AccountFile> AccountFiles { get => accountFiles; set => accountFiles = value; }

        /// <inheritdoc/>
        [DontMapInEver, DontMapOutEver, DefaultValue(null), JsonIgnore]
        public virtual ICollection<CategoryFile> CategoryFiles { get => categoryFiles; set => categoryFiles = value; }
        #endregion
    }
}
