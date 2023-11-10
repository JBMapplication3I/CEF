// <copyright file="Conversation.cs" company="clarity-ventures.com">
// Copyright (c) 2016-2023 clarity-ventures.com. All rights reserved.
// </copyright>
// <summary>Implements the conversation class</summary>
#nullable enable
namespace Clarity.Ecommerce.Interfaces.DataModel
{
    using System.Collections.Generic;
    using Ecommerce.DataModel;

    public interface IConversation
        : IAmFilterableByNullableStore,
          IAmFilterableByNullableBrand,
          IAmFilterableByUser<ConversationUser>
    {
        #region Conversation Properties
        /// <summary>Gets or sets the has ended.</summary>
        /// <value>The has ended.</value>
        bool? HasEnded { get; set; }

        /// <summary>Gets or sets the copy user when ended.</summary>
        /// <value>The copy user when ended.</value>
        bool? CopyUserWhenEnded { get; set; }
        #endregion

        #region Associated Objects
        /// <summary>Gets or sets the messages.</summary>
        /// <value>The messages.</value>
        ICollection<Message>? Messages { get; set; }
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

    [SqlSchema("Messaging", "Conversation")]
    public class Conversation : Base, IConversation
    {
        private ICollection<Message>? messages;
        private ICollection<ConversationUser>? users;

        public Conversation()
        {
            // IAmFilterableByUser Properties
            users = new HashSet<ConversationUser>();
            // Conversation Properties
            messages = new HashSet<Message>();
        }

        #region IAmFilterableByNullableStore Properties
        /// <inheritdoc/>
        [InverseProperty(nameof(ID)), ForeignKey(nameof(Store)), DefaultValue(null)]
        public int? StoreID { get; set; }

        /// <inheritdoc/>
        [AllowMapInWithRelateWorkflowsButDontAutoGenerate, DefaultValue(null), JsonIgnore]
        public virtual Store? Store { get; set; }
        #endregion

        #region IAmFilterableByNullableBrand Properties
        /// <inheritdoc/>
        [InverseProperty(nameof(ID)), ForeignKey(nameof(Brand)), DefaultValue(null)]
        public int? BrandID { get; set; }

        /// <inheritdoc/>
        [AllowMapInWithRelateWorkflowsButDontAutoGenerate, DefaultValue(null), JsonIgnore]
        public virtual Brand? Brand { get; set; }
        #endregion

        #region IAmFilterableByUser Properties
        /// <inheritdoc/>
        [DefaultValue(null), JsonIgnore]
        public virtual ICollection<ConversationUser>? Users { get => users; set => users = value; }
        #endregion

        #region Conversation Properties
        /// <inheritdoc/>
        [DefaultValue(null)]
        public bool? HasEnded { get; set; }

        /// <inheritdoc/>
        [DefaultValue(null)]
        public bool? CopyUserWhenEnded { get; set; }
        #endregion

        #region Associated Objects
        /// <inheritdoc/>
        [AllowMapInWithAssociateWorkflowsButDontAutoGenerate, DefaultValue(null), JsonIgnore]
        public virtual ICollection<Message>? Messages { get => messages; set => messages = value; }
        #endregion
    }
}
