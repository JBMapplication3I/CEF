// <copyright file="Contractor.cs" company="clarity-ventures.com">
// Copyright (c) 2021-2023 clarity-ventures.com. All rights reserved.
// </copyright>
// <summary>Implements the contractor class</summary>
#nullable enable
namespace Clarity.Ecommerce.Interfaces.DataModel
{
    using System.Collections.Generic;
    using Ecommerce.DataModel;

    public interface IContractor
        : IBase
    {
        #region Related Objects
        /// <summary>Gets or sets the ID of this contractor's account.</summary>
        /// <value>The ID of this contractor's account.</value>
        int? AccountID { get; set; }

        /// <summary>Gets or sets this contractor's account.</summary>
        /// <value>This contractor's account.</value>
        Account? Account { get; set; }

        /// <summary>Gets or sets the ID of this contractor's user.</summary>
        /// <value>The ID of this contractor's user.</value>
        int? UserID { get; set; }

        /// <summary>Gets or sets this contractor's user.</summary>
        /// <value>This contractor's user.</value>
        User? User { get; set; }

        /// <summary>Gets or sets the ID of this contractor's store.</summary>
        /// <value>The ID of this contractor's store.</value>
        int? StoreID { get; set; }

        /// <summary>Gets or sets this contactor's store.</summary>
        /// <value>This contractor's store.</value>
        Store? Store { get; set; }
        #endregion

        #region Associated Objects
        /// <summary>Gets or sets the service areas for this contractor.</summary>
        /// <value>The service areas for this contractor.</value>
        ICollection<ServiceArea>? ServiceAreas { get; set; }
        #endregion
    }
}

namespace Clarity.Ecommerce.DataModel
{
    using System.Collections.Generic;
    using System.ComponentModel;
    using System.ComponentModel.DataAnnotations.Schema;
    using Interfaces.DataModel;
    using Newtonsoft.Json;

    [SqlSchema("Accounts", "Contractor")]
    public class Contractor : Base, IContractor
    {
        private ICollection<ServiceArea>? serviceAreas;

        public Contractor()
        {
            serviceAreas = new HashSet<ServiceArea>();
        }

        #region Related Objects
        /// <inheritdoc/>
        [InverseProperty(nameof(ID)), ForeignKey(nameof(Account)), DefaultValue(null)]
        public int? AccountID { get; set; }

        /// <inheritdoc/>
        [JsonIgnore, DefaultValue(null)]
        public virtual Account? Account { get; set; }

        /// <inheritdoc/>
        [InverseProperty(nameof(ID)), ForeignKey(nameof(User)), DefaultValue(null)]
        public int? UserID { get; set; }

        /// <inheritdoc/>
        [JsonIgnore, DefaultValue(null)]
        public virtual User? User { get; set; }

        /// <inheritdoc/>
        [InverseProperty(nameof(ID)), ForeignKey(nameof(Store)), DefaultValue(null)]
        public int? StoreID { get; set; }

        /// <inheritdoc/>
        [JsonIgnore, DefaultValue(null)]
        public virtual Store? Store { get; set; }
        #endregion

        #region Associated Objects
        /// <inheritdoc/>
        [JsonIgnore]
        public virtual ICollection<ServiceArea>? ServiceAreas { get => serviceAreas; set => serviceAreas = value; }
        #endregion
    }
}
