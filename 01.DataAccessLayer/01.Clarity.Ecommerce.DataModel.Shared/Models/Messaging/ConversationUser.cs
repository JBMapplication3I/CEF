// <copyright file="ConversationUser.cs" company="clarity-ventures.com">
// Copyright (c) 2016-2023 clarity-ventures.com. All rights reserved.
// </copyright>
// <summary>Implements the conversation user class</summary>
#nullable enable
namespace Clarity.Ecommerce.Interfaces.DataModel
{
    using System;
    using Ecommerce.DataModel;

    public interface IConversationUser
        : IAmAUserRelationshipTableWhereUserIsTheSlave<Conversation>
    {
        #region ConversationUser Properties
        /// <summary>Gets or sets the Date/Time of the last heartbeat.</summary>
        /// <value>The last heartbeat.</value>
        DateTime? LastHeartbeat { get; set; }

        /// <summary>Gets or sets the is typing.</summary>
        /// <value>The is typing.</value>
        bool? IsTyping { get; set; }
        #endregion
    }
}

namespace Clarity.Ecommerce.DataModel
{
    using System;
    using System.ComponentModel;
    using System.ComponentModel.DataAnnotations.Schema;
    using Interfaces.DataModel;
    using Newtonsoft.Json;

    [SqlSchema("Messaging", "ConversationUser")]
    public class ConversationUser : Base, IConversationUser
    {
        #region IAmARelationshipTable<Conversation, User>
        /// <inheritdoc/>
        [InverseProperty(nameof(ID)), ForeignKey(nameof(Master)), DefaultValue(null)]
        public int MasterID { get; set; }

        /// <inheritdoc/>
        [DefaultValue(null), JsonIgnore]
        public virtual Conversation? Master { get; set; }

        /// <inheritdoc/>
        [InverseProperty(nameof(ID)), ForeignKey(nameof(Slave)), DefaultValue(null)]
        public int SlaveID { get; set; }

        /// <inheritdoc/>
        [ForceMapOutWithLite, DefaultValue(null), JsonIgnore]
        public virtual User? Slave { get; set; }

        /// <inheritdoc/>
        [NotMapped, JsonIgnore, Obsolete("Cannot use in queries, use SlaveID instead.", true)]
        int IAmFilterableByUser.UserID { get => SlaveID; set => SlaveID = value; }

        /// <inheritdoc/>
        [NotMapped, JsonIgnore, Obsolete("Cannot use in queries, use Slave instead.", true)]
        User? IAmFilterableByUser.User { get => Slave; set => Slave = value; }

        /// <inheritdoc/>
        [NotMapped, JsonIgnore, Obsolete("Cannot use in queries, use SlaveID instead.", true)]
        int IAmAUserRelationshipTableWhereUserIsTheSlave<Conversation>.UserID { get => SlaveID; set => SlaveID = value; }

        /// <inheritdoc/>
        [NotMapped, JsonIgnore, Obsolete("Cannot use in queries, use Slave instead.", true)]
        User? IAmAUserRelationshipTableWhereUserIsTheSlave<Conversation>.User { get => Slave; set => Slave = value; }
        #endregion

        #region ConversationUser Properties
        /// <inheritdoc/>
        [/*Column(TypeName = "datetime2"), DateTimePrecision(7),*/ DefaultValue(null)]
        public DateTime? LastHeartbeat { get; set; }

        /// <inheritdoc/>
        [DefaultValue(null)]
        public bool? IsTyping { get; set; }
        #endregion
    }
}
