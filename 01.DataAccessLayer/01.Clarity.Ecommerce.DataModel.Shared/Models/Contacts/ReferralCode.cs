// <copyright file="ReferralCode.cs" company="clarity-ventures.com">
// Copyright (c) 2017-2023 clarity-ventures.com. All rights reserved.
// </copyright>
// <summary>Implements the referral code class</summary>
#nullable enable
namespace Clarity.Ecommerce.Interfaces.DataModel
{
    using Ecommerce.DataModel;

    public interface IReferralCode
        : IHaveATypeBase<ReferralCodeType>,
          IHaveAStatusBase<ReferralCodeStatus>,
          INameableBase
    {
        /// <summary>Gets or sets the code.</summary>
        /// <value>The code.</value>
        string? Code { get; set; }

        #region Related Objects
        /// <summary>Gets or sets the identifier of the user.</summary>
        /// <value>The identifier of the user.</value>
        int UserID { get; set; }

        /// <summary>Gets or sets the user.</summary>
        /// <value>The user.</value>
        User? User { get; set; }
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

    [SqlSchema("Contacts", "ReferralCode")]
    public class ReferralCode : NameableBase, IReferralCode
    {
        #region IHaveATypeBase<ReferralCodeType>
        /// <inheritdoc/>
        [InverseProperty(nameof(ID)), ForeignKey(nameof(Type)), DefaultValue(0)]
        public int TypeID { get; set; }

        /// <inheritdoc/>
        [DefaultValue(null), JsonIgnore]
        public virtual ReferralCodeType? Type { get; set; }
        #endregion

        #region IHaveAStatusBase<ReferralCodeStatus>
        /// <inheritdoc/>
        [InverseProperty(nameof(ID)), ForeignKey(nameof(Status)), DefaultValue(0)]
        public int StatusID { get; set; }

        /// <inheritdoc/>
        [DefaultValue(null), JsonIgnore]
        public virtual ReferralCodeStatus? Status { get; set; }
        #endregion

        /// <inheritdoc/>
        [Required, StringLength(128), StringIsUnicode(false), DefaultValue(null)]
        public string? Code { get; set; }

        #region Related Objects
        /// <inheritdoc/>
        [InverseProperty(nameof(ID)), ForeignKey(nameof(User)), DefaultValue(0)]
        public int UserID { get; set; }

        /// <inheritdoc/>
        [AllowMapInWithRelateWorkflowsButDontAutoGenerate, DefaultValue(null), JsonIgnore]
        public virtual User? User { get; set; }
        #endregion
    }
}
