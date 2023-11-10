// <copyright file="IPOrganization.cs" company="clarity-ventures.com">
// Copyright (c) 2016-2023 clarity-ventures.com. All rights reserved.
// </copyright>
// <summary>Implements the IP organization class</summary>
#nullable enable
namespace Clarity.Ecommerce.Interfaces.DataModel
{
    using Ecommerce.DataModel;

    public interface IIPOrganization : INameableBase, IHaveAStatusBase<IPOrganizationStatus>
    {
        /// <summary>Gets or sets the IP address.</summary>
        /// <value>The IP address.</value>
        string? IPAddress { get; set; }

        /// <summary>Gets or sets the score.</summary>
        /// <value>The score.</value>
        int? Score { get; set; }

        /// <summary>Gets or sets the visitor key.</summary>
        /// <value>The visitor key.</value>
        string? VisitorKey { get; set; }

        #region Related Objects
        /// <summary>Gets or sets the identifier of the primary user.</summary>
        /// <value>The identifier of the primary user.</value>
        int? PrimaryUserID { get; set; }

        /// <summary>Gets or sets the primary user.</summary>
        /// <value>The primary user.</value>
        User? PrimaryUser { get; set; }

        /// <summary>Gets or sets the identifier of the address.</summary>
        /// <value>The identifier of the address.</value>
        int? AddressID { get; set; }

        /// <summary>Gets or sets the address.</summary>
        /// <value>The address.</value>
        Address? Address { get; set; }
        #endregion
    }
}

namespace Clarity.Ecommerce.DataModel
{
    using System.ComponentModel;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using Interfaces.DataModel;
    using Newtonsoft.Json;

    [SqlSchema("Tracking", "IPOrganization")]
    public class IPOrganization : NameableBase, IIPOrganization
    {
        #region IHaveAStatusBase
        /// <inheritdoc/>
        [InverseProperty(nameof(ID)), ForeignKey(nameof(Status)), DefaultValue(0)]
        public int StatusID { get; set; }

        /// <inheritdoc/>
        [ForceMapOutWithLite, DefaultValue(null), JsonIgnore]
        public virtual IPOrganizationStatus? Status { get; set; }
        #endregion

        /// <inheritdoc/>
        [StringLength(20), StringIsUnicode(false), DefaultValue(null)]
        public string? IPAddress { get; set; }

        /// <inheritdoc/>
        [DefaultValue(null)]
        public int? Score { get; set; }

        /// <inheritdoc/>
        [StringLength(50), DefaultValue(null)]
        public string? VisitorKey { get; set; }

        #region Related Objects
        /// <inheritdoc/>
        [InverseProperty(nameof(ID)), ForeignKey(nameof(Address))]
        public int? AddressID { get; set; }

        /// <inheritdoc/>
        [DefaultValue(null), JsonIgnore]
        public virtual Address? Address { get; set; }

        /// <inheritdoc/>
        [InverseProperty(nameof(ID)), ForeignKey(nameof(PrimaryUser)), DefaultValue(null)]
        public int? PrimaryUserID { get; set; }

        /// <inheritdoc/>
        [AllowMapInWithRelateWorkflowsButDontAutoGenerate, DefaultValue(null), JsonIgnore]
        public virtual User? PrimaryUser { get; set; }
        #endregion
    }
}
