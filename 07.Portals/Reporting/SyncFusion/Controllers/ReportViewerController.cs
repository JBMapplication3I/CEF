// <copyright file="ReportViewerController.cs" company="clarity-ventures.com">
// Copyright (c) 2020-2023 clarity-ventures.com. All rights reserved.
// </copyright>
// <summary>Implements the report viewer controller class</summary>
namespace Clarity.Ecommerce.Reporting.Syncfusion.Controllers
{
    using System.Collections.Generic;
    using System.IO;
    using BoldReports.Web.ReportViewer;
    using Microsoft.AspNetCore.Hosting;
    using Microsoft.AspNetCore.Mvc;
    using Microsoft.Extensions.Caching.Memory;

    public class ReportViewerController : Controller, IReportController
    {
        private readonly IMemoryCache cache;
        private readonly IWebHostEnvironment hostingEnvironment;

        public ReportViewerController(IMemoryCache memoryCache, IWebHostEnvironment hostingEnvironment)
        {
            cache = memoryCache;
            this.hostingEnvironment = hostingEnvironment;
        }

        public object PostReportAction([FromBody] Dictionary<string, object> jsonArray)
        {
            return ReportHelper.ProcessReport(jsonArray, this, cache);
        }

        public void OnInitReportOptions(ReportViewerOptions reportOption)
        {
            var basePath = hostingEnvironment.WebRootPath;
            var reportStream = new FileStream(
                basePath + @$"\resources\report\{reportOption.ReportModel.ReportPath}",
                FileMode.Open,
                FileAccess.Read);
            reportOption.ReportModel.Stream = reportStream;
        }

        public void OnReportLoaded(ReportViewerOptions reportOption)
        {
        }

        [ActionName("GetResource")]
        [AcceptVerbs("GET")]
        public object GetResource(ReportResource resource)
        {
            return ReportHelper.GetResource(resource, this, cache);
        }

        public object PostFormReportAction()
        {
            return ReportHelper.ProcessReport(null, this, cache);
        }
    }
}
