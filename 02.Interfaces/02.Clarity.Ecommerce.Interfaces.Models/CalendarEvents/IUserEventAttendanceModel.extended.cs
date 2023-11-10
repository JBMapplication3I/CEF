// <copyright file="IUserEventAttendanceModel.extended.cs" company="clarity-ventures.com">
// Copyright (c) 2017-2023 clarity-ventures.com. All rights reserved.
// </copyright>
// <summary>Declares the IUserEventAttendanceModel interface</summary>
namespace Clarity.Ecommerce.Interfaces.Models
{
    using System;

    /// <summary>Interface for user event attendance model.</summary>
    public partial interface IUserEventAttendanceModel
    {
        /// <summary>Gets or sets the Date/Time of the date.</summary>
        /// <value>The date.</value>
        DateTime Date { get; set; }

        /// <summary>Gets or sets a value indicating whether this user has attended.</summary>
        /// <value>True if this user has attended, false if not.</value>
        bool HasAttended { get; set; }

        #region Convenience Properties
        /// <summary>Gets or sets the identifier of the sales order.</summary>
        /// <value>The identifier of the sales order.</value>
        int? SalesOrderID { get; set; }

        /// <summary>Gets or sets the sales order.</summary>
        /// <value>The sales order.</value>
        ISalesOrderModel? SalesOrder { get; set; }

        /// <summary>Gets or sets the balance.</summary>
        /// <value>The balance.</value>
        decimal? Balance { get; set; }

        /// <summary>Gets or sets the user event total.</summary>
        /// <value>The user event total.</value>
        decimal? UserEventTotal { get; set; }

        /// <summary>Gets or sets the type of the event package.</summary>
        /// <value>The type of the event package.</value>
        string? EventPackageType { get; set; }
        #endregion
    }
}
