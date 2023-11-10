// <copyright file="ClarityReportStorageWebExtension.cs" company="clarity-ventures.com">
// Copyright (c) 2016-2023 clarity-ventures.com. All rights reserved.
// </copyright>
// <summary>Implements the clarity report storage web extension class</summary>
namespace Clarity.Ecommerce.Service.Reporting
{
    using System;
    using System.Collections.Generic;
    using System.IO;
    using System.Linq;
    using System.Reflection;
    using DataModel;
    using DevExpress.XtraReports.UI;
    using DevExpress.XtraReports.Web.Extensions;
    using Interfaces.DataModel;
    using Utilities;

    /// <summary>A clarity report storage web extension.</summary>
    /// <seealso cref="ReportStorageWebExtension"/>
    public class ClarityReportStorageWebExtension : ReportStorageWebExtension
    {
        private ClarityEcommerceEntities entityContext;

        private ClarityEcommerceEntities EntityContext => entityContext ??= new ClarityEcommerceEntities();

        public override Dictionary<string, string> GetUrls()
        {
            // Get URLs and display names for all reports available in storage
            var reportTypes = GetReportTypesFromCurrentAssembly();
            var dictionary1 = reportTypes
                .ToDictionary(
                    x => x.AssemblyQualifiedName,
                    x => "Default: " + ((XtraReport)Activator.CreateInstance(x)).DisplayName);
            var dictionary2 = EntityContext.ReportTypes
                .Where(x => x.Active)
                .FilterReportTypesByHasTemplate()
                .ToDictionary(x => x.ID.ToString(), x => x.DisplayName);
            foreach (var kvp in dictionary2) { dictionary1[kvp.Key] = kvp.Value; }
            return dictionary1;
        }

        public override bool CanSetData(string url)
        {
            // Check if the URL is available in the report storage
            return ConvertUrlToEntity(url) != null;
        }

        public override byte[] GetData(string url)
        {
            // Get the report layout data from storage
            var type = Type.GetType(url);
            if (type == null)
            {
                return ConvertUrlToEntity(url)?.Template;
            }
            using var ms = new MemoryStream();
            ((XtraReport)Activator.CreateInstance(type)).SaveLayoutToXml(ms);
            return ms.GetBuffer();
        }

        public override bool IsValidUrl(string url)
        {
            // Check if the specified URL is valid for the current report storage
            // In this example, a URL should be a string containing a numeric value that is used as a data row primary key
            return !string.IsNullOrWhiteSpace(url);
        }

        /*public override void SetData(XtraReport report, Stream stream)
        {
            base.SetData(report, stream);
        }*/

        public override void SetData(XtraReport report, string url)
        {
            // Write a report to the storage under the specified URL
            var entity = ConvertUrlToEntity(url);
            if (entity == null) { return; }
            using (var ms = new MemoryStream())
            {
                report.SaveLayoutToXml(ms);
                entity.Template = ms.GetBuffer();
            }
            EntityContext.SaveUnitOfWork();
        }

        public override string SetNewData(XtraReport report, string defaultUrl)
        {
            // Save a report to the storage under a new URL
            // The defaultUrl parameter contains the report display name specified by a user
            var entity = new ReportType
            {
                // Base Properties
                CustomKey = defaultUrl,
                Active = true,
                CreatedDate = DateExtensions.GenDateTime,
                UpdatedDate = null,
                // NameableBase Properties
                Name = defaultUrl,
                Description = null,
                // TypableBase Properties
                DisplayName = defaultUrl,
                SortOrder = null
            };
            using (var ms = new MemoryStream())
            {
                report.SaveLayoutToXml(ms);
                entity.Template = ms.GetBuffer();
            }
            EntityContext.ReportTypes.Add(entity);
            EntityContext.SaveUnitOfWork(true);
            return entity.ID.ToString();
        }

        private static IEnumerable<Type> GetReportTypesFromCurrentAssembly()
        {
            return Assembly.GetExecutingAssembly()
                .GetTypes()
                .Where(x => x.BaseType == typeof(XtraReport) && x != typeof(ReportTemplate));
        }

        private ReportType ConvertUrlToEntity(string url)
        {
            var isID = int.TryParse(url, out var id);
            var entity = EntityContext.ReportTypes
                .Where(x => x.Active)
                .FilterReportTypesByHasTemplate()
                .FirstOrDefault(x => isID && x.ID == id || !isID && x.DisplayName == url);
            return entity;
        }
    }

    public static class ReportTypeSearchExtensions
    {
        public static IQueryable<TEntity> FilterReportTypesByHasTemplate<TEntity>(this IQueryable<TEntity> query)
            where TEntity : class, IReportType
        {
            Contract.RequiresNotNull(query);
            return query.Where(x => x.Template != null);
        }
    }
}
