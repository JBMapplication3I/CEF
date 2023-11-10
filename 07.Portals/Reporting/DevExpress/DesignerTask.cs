// <copyright file="DesignerTask.cs" company="clarity-ventures.com">
// Copyright (c) 2016-2023 clarity-ventures.com. All rights reserved.
// </copyright>
// <summary>Implements the designer task class</summary>
namespace Clarity.Ecommerce.Service.Reporting
{
    using DevExpress.XtraReports.UI;

    /// <summary>Values that represent report editing modes.</summary>
    public enum ReportEditingMode
    {
        /// <summary>An enum constant representing the none option.</summary>
        None,

        /// <summary>An enum constant representing the new report option.</summary>
        NewReport,

        /// <summary>An enum constant representing the modify report option.</summary>
        ModifyReport
    }

    /// <summary>A designer task.</summary>
    public class DesignerTask
    {
        /// <summary>Gets or sets the mode.</summary>
        /// <value>The mode.</value>
        public ReportEditingMode Mode { get; set; }

        /// <summary>Gets or sets the identifier of the report.</summary>
        /// <value>The identifier of the report.</value>
        public string ReportID { get; set; }

        /// <summary>Gets or sets the report.</summary>
        /// <value>The report.</value>
        public XtraReport Report { get; set; }
    }
}
