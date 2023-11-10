// <copyright file="Visitor.cs" company="clarity-ventures.com">
// Copyright (c) 2016-2023 clarity-ventures.com. All rights reserved.
// </copyright>
// <summary>Implements the visitor class</summary>
#nullable enable
namespace Clarity.Ecommerce.Interfaces.DataModel
{
    using System.Collections.Generic;
    using Ecommerce.DataModel;

    public interface IVisitor : IAmALitelyTrackedEventBase
    {
        #region Associated Objects
        /// <summary>Gets or sets the visits.</summary>
        /// <value>The visits.</value>
        ICollection<Visit>? Visits { get; set; }
        #endregion
    }
}

namespace Clarity.Ecommerce.DataModel
{
    using System.Collections.Generic;
    using System.ComponentModel;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using Interfaces.DataModel;
    using Newtonsoft.Json;

    [SqlSchema("Tracking", "Visitor")]
    public class Visitor : NameableBase, IVisitor
    {
        private ICollection<Visit>? visits;

        public Visitor()
        {
            visits = new HashSet<Visit>();
        }

        #region IAmALitelyTrackedEvent Properties
        /// <inheritdoc/>
        [StringLength(20), StringIsUnicode(false), DefaultValue(null)]
        public string? IPAddress { get; set; }

        /// <inheritdoc/>
        [DefaultValue(null)]
        public int? Score { get; set; }

        /// <inheritdoc/>
        [InverseProperty(nameof(ID)), ForeignKey(nameof(Address)), DefaultValue(null)]
        public int? AddressID { get; set; }

        /// <inheritdoc/>
        [DefaultValue(null), JsonIgnore]
        public virtual Address? Address { get; set; }

        /// <inheritdoc/>
        [InverseProperty(nameof(ID)), ForeignKey(nameof(IPOrganization)), DefaultValue(null)]
        public int? IPOrganizationID { get; set; }

        /// <inheritdoc/>
        [DefaultValue(null), JsonIgnore]
        public virtual IPOrganization? IPOrganization { get; set; }

        /// <inheritdoc/>
        [InverseProperty(nameof(ID)), ForeignKey(nameof(User)), DefaultValue(null)]
        public int? UserID { get; set; }

        /// <inheritdoc/>
        [AllowMapInWithRelateWorkflowsButDontAutoGenerate, DefaultValue(null), JsonIgnore]
        public virtual User? User { get; set; }
        #endregion

        #region Associated Objects
        /// <inheritdoc/>
        [DefaultValue(null), JsonIgnore]
        public virtual ICollection<Visit>? Visits { get => visits; set => visits = value; }
        #endregion
    }
}
