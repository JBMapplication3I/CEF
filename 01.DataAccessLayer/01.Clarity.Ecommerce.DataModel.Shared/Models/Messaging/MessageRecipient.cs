// <copyright file="MessageRecipient.cs" company="clarity-ventures.com">
// Copyright (c) 2016-2023 clarity-ventures.com. All rights reserved.
// </copyright>
// <summary>Implements the message recipient class</summary>
#nullable enable
namespace Clarity.Ecommerce.Interfaces.DataModel
{
    using System;
    using System.Collections.Generic;
    using Ecommerce.DataModel;

    public interface IMessageRecipient
        : IAmAUserRelationshipTableWhereUserIsTheSlave<Message>
    {
        #region MessageRecipient Properties
        /// <summary>Gets or sets a value indicating whether this IMessageRecipient is read.</summary>
        /// <value>True if this IMessageRecipient is read, false if not.</value>
        bool IsRead { get; set; }

        /// <summary>Gets or sets the Date/Time of the read at.</summary>
        /// <value>The read at.</value>
        DateTime? ReadAt { get; set; }

        /// <summary>Gets or sets a value indicating whether this IMessageRecipient is archived.</summary>
        /// <value>True if this IMessageRecipient is archived, false if not.</value>
        bool IsArchived { get; set; }

        /// <summary>Gets or sets the Date/Time of the archived at.</summary>
        /// <value>The archived at.</value>
        DateTime? ArchivedAt { get; set; }

        /// <summary>Gets or sets a value indicating whether this IMessageRecipient has sent an email.</summary>
        /// <value>True if this IMessageRecipient has sent an email, false if not.</value>
        bool HasSentAnEmail { get; set; }

        /// <summary>Gets or sets the Date/Time of the email sent at.</summary>
        /// <value>The email sent at.</value>
        DateTime? EmailSentAt { get; set; }
        #endregion

        #region Related Objects
        /// <summary>Gets or sets the identifier of the group.</summary>
        /// <value>The identifier of the group.</value>
        int? GroupID { get; set; }

        /// <summary>Gets or sets the group.</summary>
        /// <value>The group.</value>
        Group? Group { get; set; }
        #endregion

        #region Associated Objects
        /// <summary>Gets or sets the email queues.</summary>
        /// <value>The email queues.</value>
        ICollection<EmailQueue>? EmailQueues { get; set; }
        #endregion
    }
}

namespace Clarity.Ecommerce.DataModel
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel;
    using System.ComponentModel.DataAnnotations.Schema;
    using Interfaces.DataModel;
    using Newtonsoft.Json;

    [SqlSchema("Messaging", "MessageRecipient")]
    public class MessageRecipient : Base, IMessageRecipient
    {
        private ICollection<EmailQueue>? emailQueues;

        public MessageRecipient()
        {
            emailQueues = new HashSet<EmailQueue>();
        }

        #region IAmARelationshipTable<Message, User>
        /// <inheritdoc/>
        [InverseProperty(nameof(ID)), ForeignKey(nameof(Master)), DefaultValue(null)]
        public int MasterID { get; set; }

        /// <inheritdoc/>
        [DontMapInWithRelateWorkflows, DontMapOutEver, DefaultValue(null), JsonIgnore]
        public virtual Message? Master { get; set; }

        /// <inheritdoc/>
        [InverseProperty(nameof(ID)), ForeignKey(nameof(Slave))]
        public int SlaveID { get; set; }

        /// <inheritdoc/>
        [AllowMapInWithRelateWorkflowsButDontAutoGenerate, DefaultValue(null), JsonIgnore]
        public virtual User? Slave { get; set; }

        /// <inheritdoc/>
        [NotMapped, JsonIgnore, Obsolete("Cannot use in queries, use SlaveID instead.", true)]
        int IAmFilterableByUser.UserID { get => SlaveID; set => SlaveID = value; }

        /// <inheritdoc/>
        [NotMapped, JsonIgnore, Obsolete("Cannot use in queries, use Slave instead.", true)]
        User? IAmFilterableByUser.User { get => Slave; set => Slave = value; }

        /// <inheritdoc/>
        [NotMapped, JsonIgnore, Obsolete("Cannot use in queries, use SlaveID instead.", true)]
        int IAmAUserRelationshipTableWhereUserIsTheSlave<Message>.UserID { get => SlaveID; set => SlaveID = value; }

        /// <inheritdoc/>
        [NotMapped, JsonIgnore, Obsolete("Cannot use in queries, use Slave instead.", true)]
        User? IAmAUserRelationshipTableWhereUserIsTheSlave<Message>.User { get => Slave; set => Slave = value; }
        #endregion

        #region MessageRecipient Properties
        /// <inheritdoc/>
        [DefaultValue(false)]
        public bool IsRead { get; set; }

        /// <inheritdoc/>
        [/*Column(TypeName = "datetime2"), DateTimePrecision(7),*/ DefaultValue(null)]
        public DateTime? ReadAt { get; set; }

        /// <inheritdoc/>
        [DefaultValue(false)]
        public bool IsArchived { get; set; }

        /// <inheritdoc/>
        [/*Column(TypeName = "datetime2"), DateTimePrecision(7),*/ DefaultValue(null)]
        public DateTime? ArchivedAt { get; set; }

        /// <inheritdoc/>
        [DefaultValue(false)]
        public bool HasSentAnEmail { get; set; }

        /// <inheritdoc/>
        [/*Column(TypeName = "datetime2"), DateTimePrecision(7),*/ DefaultValue(null)]
        public DateTime? EmailSentAt { get; set; }
        #endregion

        #region Related Objects
        /// <inheritdoc/>
        [InverseProperty(nameof(ID)), ForeignKey(nameof(Group)), DefaultValue(null)]
        public int? GroupID { get; set; }

        /// <inheritdoc/>
        [AllowMapInWithRelateWorkflowsButDontAutoGenerate, DefaultValue(null), JsonIgnore]
        public virtual Group? Group { get; set; }
        #endregion

        #region Associated Objects
        /// <inheritdoc/>
        [DontMapInEver, DontMapOutEver, DefaultValue(null), JsonIgnore]
        public virtual ICollection<EmailQueue>? EmailQueues { get => emailQueues; set => emailQueues = value; }
        #endregion
    }
}
