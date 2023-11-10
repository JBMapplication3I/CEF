// <copyright file="UserSupportRequest.cs" company="clarity-ventures.com">
// Copyright (c) 2021-2023 clarity-ventures.com. All rights reserved.
// </copyright>
// <summary>Implements the user support request class</summary>
namespace Clarity.Ecommerce.Interfaces.DataModel
{
    using Ecommerce.DataModel;

    public interface IUserSupportRequest : IBase, IAmExcludedFromT4Generation
    {
        /// <summary>Gets or sets the ID of the user who is requesting support.</summary>
        /// <value>The ID of the user who is requesting support.</value>
        int UserID { get; set; }

        /// <summary>Gets or sets the user who is requesting support.</summary>
        /// <value>The user who is requesting support.</value>
        User? User { get; set; }

        /// <summary>Gets or sets the auth key for the channel.</summary>
        /// <value>The auth key for the channel.</value>
        string? AuthKey { get; set; }

        /// <summary>Gets or sets the Pubnub channel name.</summary>
        /// <value>The Pubnub channel name.</value>
        string? ChannelName { get; set; }

        /// <summary>Gets or sets the current status of this support request.</summary>
        /// <value>The current status of this support request.</value>
        string? Status { get; set; }
    }
}

namespace Clarity.Ecommerce.DataModel
{
    using System.ComponentModel;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using Interfaces.DataModel;
    using Newtonsoft.Json;

    [SqlSchema("Messaging", "UserSupportRequest")]
    public class UserSupportRequest : Base, IUserSupportRequest
    {
        /// <inheritdoc/>
        [InverseProperty(nameof(ID)), ForeignKey(nameof(User))]
        public int UserID { get; set; }

        /// <inheritdoc/>
        [DefaultValue(null), JsonIgnore]
        public virtual User? User { get; set; }

        /// <inheritdoc/>
        [StringIsUnicode(false), StringLength(256), DefaultValue(null)]
        public string? AuthKey { get; set; }

        /// <inheritdoc/>
        [StringIsUnicode(false), StringLength(256), DefaultValue(null)]
        public string? ChannelName { get; set; }

        /// <inheritdoc/>
        [StringIsUnicode(false), StringLength(64), DefaultValue(null)]
        public string? Status { get; set; }
    }
}
