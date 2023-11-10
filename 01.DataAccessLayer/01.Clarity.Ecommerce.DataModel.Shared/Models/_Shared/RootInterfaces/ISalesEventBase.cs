// <copyright file="ISalesEventBase.cs" company="clarity-ventures.com">
// Copyright (c) 2021-2023 clarity-ventures.com. All rights reserved.
// </copyright>
// <summary>Declares the ISalesEventBase interface</summary>
#nullable enable
namespace Clarity.Ecommerce.Interfaces.DataModel
{
    /// <summary>Interface for am a contact relationship table base.</summary>
    /// <typeparam name="TMaster">        Type of the master entity.</typeparam>
    /// <typeparam name="TSalesEventType">Type of the sales event type.</typeparam>
    public interface ISalesEventBase<out TMaster, TSalesEventType>
        : INameableBase, IHaveATypeBase<TSalesEventType>
        where TMaster : IBase // , IHaveSalesEventsBase<TMaster, TContact>
        where TSalesEventType : ITypableBase
    {
        /// <summary>Gets or sets the identifier of the master.</summary>
        /// <value>The identifier of the master.</value>
        int MasterID { get; set; }

        /// <summary>Gets the master.</summary>
        /// <value>The master.</value>
        TMaster? Master { get; }

        #region SalesEvent Properties
        /// <summary>Gets or sets the identifier of the old state.</summary>
        /// <value>The identifier of the old state.</value>
        int? OldStateID { get; set; }

        /// <summary>Gets or sets the identifier of the new state.</summary>
        /// <value>The identifier of the new state.</value>
        int? NewStateID { get; set; }

        /// <summary>Gets or sets the identifier of the old status.</summary>
        /// <value>The identifier of the old status.</value>
        int? OldStatusID { get; set; }

        /// <summary>Gets or sets the identifier of the new status.</summary>
        /// <value>The identifier of the new status.</value>
        int? NewStatusID { get; set; }

        /// <summary>Gets or sets the identifier of the old type.</summary>
        /// <value>The identifier of the old type.</value>
        int? OldTypeID { get; set; }

        /// <summary>Gets or sets the identifier of the new type.</summary>
        /// <value>The identifier of the new type.</value>
        int? NewTypeID { get; set; }

        /// <summary>Gets or sets the old hash.</summary>
        /// <value>The old hash.</value>
        long? OldHash { get; set; }

        /// <summary>Gets or sets the new hash.</summary>
        /// <value>The new hash.</value>
        long? NewHash { get; set; }

        /// <summary>Gets or sets the old record serialized.</summary>
        /// <value>The old record serialized.</value>
        string? OldRecordSerialized { get; set; }

        /// <summary>Gets or sets the new record serialized.</summary>
        /// <value>The new record serialized.</value>
        string? NewRecordSerialized { get; set; }
        #endregion
    }
}
