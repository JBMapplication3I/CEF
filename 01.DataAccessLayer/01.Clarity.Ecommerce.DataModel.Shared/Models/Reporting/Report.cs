// <copyright file="Report.cs" company="clarity-ventures.com">
// Copyright (c) 2016-2023 clarity-ventures.com. All rights reserved.
// </copyright>
// <summary>Implements the report class</summary>
#nullable enable
namespace Clarity.Ecommerce.Interfaces.DataModel
{
    using Ecommerce.DataModel;

    public interface IReport : IHaveATypeBase<ReportType>, INameableBase
    {
        #region Report Properties
        /// <summary>Gets or sets information describing the results.</summary>
        /// <value>Information describing the results.</value>
        string? ResultsData { get; set; }
        #endregion

        #region Related Objects
        /// <summary>Gets or sets the identifier of the run by user.</summary>
        /// <value>The identifier of the run by user.</value>
        int RunByUserID { get; set; }

        /// <summary>Gets or sets the run by user.</summary>
        /// <value>The run by user.</value>
        User? RunByUser { get; set; }
        #endregion
    }
}

// ReSharper disable once CheckNamespace
namespace Clarity.Ecommerce.DataModel
{
    using System.ComponentModel;
    using System.ComponentModel.DataAnnotations.Schema;
    using Interfaces.DataModel;

    [SqlSchema("Reporting", "Reports")]
    public class Report : NameableBase, IReport
    {
        #region IHaveAType Properties
        /// <inheritdoc/>
        [InverseProperty(nameof(ID)), ForeignKey(nameof(Type)), DefaultValue(0)]
        public int TypeID { get; set; }

        /// <inheritdoc/>
        [ForceMapOutWithLite, DefaultValue(null)]
        public virtual ReportType? Type { get; set; }
        #endregion

        #region Report Properties
        [DontMapOutWithListing, DefaultValue(null)]
        public string? ResultsData { get; set; }
        #endregion

        #region Related Objects
        /// <inheritdoc/>
        [InverseProperty(nameof(ID)), ForeignKey(nameof(RunByUser)), DefaultValue(0)]
        public int RunByUserID { get; set; }

        /// <inheritdoc/>
        [ForceMapOutWithLite, DefaultValue(null)]
        public virtual User? RunByUser { get; set; }
        #endregion
    }
}
