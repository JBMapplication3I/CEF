// <copyright file="UserEventAttendance.cs" company="clarity-ventures.com">
// Copyright (c) 2017-2023 clarity-ventures.com. All rights reserved.
// </copyright>
// <summary>Implements the user event attendance class</summary>
#nullable enable
namespace Clarity.Ecommerce.Interfaces.DataModel
{
    using System;
    using Ecommerce.DataModel;

    public interface IUserEventAttendance
        : IHaveATypeBase<UserEventAttendanceType>,
            IAmAUserRelationshipTableWhereUserIsTheSlave<CalendarEvent>
    {
        /// <summary>Gets or sets a value indicating whether this IUserEventAttendance has attended.</summary>
        /// <value>True if this IUserEventAttendance has attended, false if not.</value>
        bool HasAttended { get; set; }

        /// <summary>Gets or sets the Date/Time of the date.</summary>
        /// <value>The date.</value>
        DateTime Date { get; set; }
    }
}

namespace Clarity.Ecommerce.DataModel
{
    using System;
    using System.ComponentModel;
    using System.ComponentModel.DataAnnotations.Schema;
    using Interfaces.DataModel;
    using Newtonsoft.Json;

    [SqlSchema("CalendarEvents", "UserEventAttendance")]
    public class UserEventAttendance : Base, IUserEventAttendance
    {
        #region IHaveATypeBase<UserEventAttendanceType>
        /// <inheritdoc/>
        [InverseProperty(nameof(ID)), ForeignKey(nameof(Type)), DefaultValue(0)]
        public int TypeID { get; set; }

        /// <inheritdoc/>
        [DefaultValue(null), JsonIgnore]
        public virtual UserEventAttendanceType? Type { get; set; }
        #endregion

        #region IAmARelationshipTable<CalendarEvent, User>
        /// <inheritdoc/>
        [InverseProperty(nameof(ID)), ForeignKey(nameof(Master)), DefaultValue(0)]
        public int MasterID { get; set; }

        /// <inheritdoc/>
        [AllowMapInWithRelateWorkflowsButDontAutoGenerate, DefaultValue(null), JsonIgnore]
        public virtual CalendarEvent? Master { get; set; }

        /// <inheritdoc/>
        [InverseProperty(nameof(ID)), ForeignKey(nameof(Slave)), DefaultValue(0)]
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
        int IAmAUserRelationshipTableWhereUserIsTheSlave<CalendarEvent>.UserID { get => SlaveID; set => SlaveID = value; }

        /// <inheritdoc/>
        [NotMapped, JsonIgnore, Obsolete("Cannot use in queries, use Slave instead.", true)]
        User? IAmAUserRelationshipTableWhereUserIsTheSlave<CalendarEvent>.User { get => Slave; set => Slave = value; }
        #endregion

        #region UserEventAttendance Properties
        /// <inheritdoc/>
        [DefaultValue(false)]
        public bool HasAttended { get; set; }

        /// <inheritdoc/>
        [/*Column(TypeName = "datetime2"), DateTimePrecision(7),*/ Index]
        public DateTime Date { get; set; }
        #endregion
    }
}
