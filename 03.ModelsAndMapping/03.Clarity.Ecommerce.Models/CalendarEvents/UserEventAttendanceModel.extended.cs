// <copyright file="UserEventAttendanceModel.extended.cs" company="clarity-ventures.com">
// Copyright (c) 2017-2023 clarity-ventures.com. All rights reserved.
// </copyright>
// <summary>Implements the user event attendance model class</summary>
namespace Clarity.Ecommerce.Models
{
    using System;
    using Interfaces.Models;

    /// <summary>A data Model for the user event attendance.</summary>
    /// <seealso cref="BaseModel"/>
    /// <seealso cref="IUserEventAttendanceModel"/>
    public partial class UserEventAttendanceModel
    {
        /// <inheritdoc/>
        public DateTime Date { get; set; }

        /// <inheritdoc/>
        public bool HasAttended { get; set; }

        /// <inheritdoc/>
        public decimal? UserEventTotal { get; set; }

        /// <inheritdoc/>
        public decimal? Balance { get; set; }

        /// <inheritdoc/>
        public string? EventPackageType { get; set; }

        #region Related Objects
        /// <inheritdoc/>
        public int? SalesOrderID { get; set; }

        /// <inheritdoc cref="IUserEventAttendanceModel.SalesOrder"/>
        public SalesOrderModel? SalesOrder { get; set; }

        /// <inheritdoc/>
        ISalesOrderModel? IUserEventAttendanceModel.SalesOrder { get => SalesOrder; set => SalesOrder = (SalesOrderModel?)value; }
        #endregion
    }
}
