// <copyright file="MigrationActions.cs" company="clarity-ventures.com">
// Copyright (c) 2016-2023 clarity-ventures.com. All rights reserved.
// </copyright>
// <summary>Implements the Migration Actions class</summary>
// ReSharper disable InconsistentNaming, MissingXmlDoc, StringIndexOfIsCultureSpecific.1, StringIndexOfIsCultureSpecific.2
#pragma warning disable IDE0057 // Use range operator
#pragma warning disable SA1118 // Parameter should not span multiple lines
#pragma warning disable SA1202 // Element Ordering
#pragma warning disable SA1600 // Elements should be documented
namespace Clarity.Ecommerce.SeedDatabase.Testing
{
    using System;
    using System.Collections.Generic;
    using System.Data.Entity;
    using System.Data.Entity.Migrations.Infrastructure;
    using System.Data.SqlClient;
    using System.IO;
    using System.IO.Compression;
    using System.Linq;
    using System.Linq.Expressions;
    using System.Text;
    using System.Text.RegularExpressions;
    using System.Threading.Tasks;
    using System.Xml.Linq;
    using DataModel;
    using DataModel.Migrations;
    using Ecommerce.Testing;
    using Interfaces.Models;
    using LinqKit;
    using Utilities;
    using Xunit;
    using Xunit.Abstractions;

    [Trait("Category", "Seed Database.DB Migration Actions/Scripts")]
    public class MigrationActions : XUnitLogHelper
    {
        private static readonly Regex ImperialMeasurementRegexA = new(@"^(?<whole>\d+)-(?<divTop>\d+)\/(?<divBot>\d+)$");

        private static readonly Regex ImperialMeasurementRegexB = new(@"^(?<divTop>\d+)\/(?<divBot>\d+)$");

        public MigrationActions(ITestOutputHelper testOutputHelper)
            : base(testOutputHelper)
        {
        }

        ////[Fact(Skip = "Comment out the Skip when you need to Migrate data")]
        ////public void MigrateProductAttributesToJsonAttributes()
        ////{
        ////    using (var context = new ClarityEcommerceEntities())
        ////    {
        ////        const int skip = 0;
        ////        const int take = 100000;
        ////        var dict = context.Products
        ////            //.Include(x => x.Attributes)
        ////            //.Include(x => x.Attributes.Select(y => y.AttributeValue))
        ////            //.Include(x => x.Attributes.Select(y => y.AttributeValue.Attribute))
        ////            .Where(x => x.Active)
        ////            .OrderBy(x => x.ID)
        ////            .Skip(skip)
        ////            .Take(take)
        ////            .ToDictionary(k => k.ID, v => v.Attributes.Select(a => new
        ////            {
        ////                Active = a.Active && a.AttributeValue.Active && a.AttributeValue.Attribute.Active,
        ////                a.AttributeValue.AttributeID,
        ////                a.AttributeValue.Attribute.CustomKey,
        ////                a.AttributeValue.Attribute.Name,
        ////                a.AttributeValue.Value,
        ////                a.AttributeValue.UnitOfMeasure,
        ////                a.SortOrder
        ////            }));
        ////        //var counter = 0;
        ////        foreach (var kvp in dict)
        ////        {
        ////            ////TestOutputHelper.WriteLine($"Product ID: {kvp.Key}");
        ////            var serial = new SerializableAttributesDictionary();
        ////            //
        ////            foreach (var attribute in kvp.Value.Where(x => x.Active))
        ////            {
        ////                var value = new SerializableAttributeObject
        ////                {
        ////                    ID = attribute.AttributeID,
        ////                    Key = string.IsNullOrWhiteSpace(attribute.CustomKey) ? attribute.Name : attribute.CustomKey,
        ////                    Value = attribute.Value,
        ////                    UofM = attribute.UnitOfMeasure,
        ////                    SortOrder = attribute.SortOrder
        ////                };
        ////                serial[attribute.Name] = value;
        ////            }
        ////            //
        ////            context.Products.Single(x => x.ID == kvp.Key).JsonAttributes = serial.SerializeAttributesDictionary();
        ////            //if (counter % 250 == 0)
        ////            //{
        ////            //    context.SaveUnitOfWork(true);
        ////            //}
        ////            //counter++;
        ////            ////TestOutputHelper.WriteLine($"{counter}");
        ////        }
        ////        context.SaveUnitOfWork();
        ////    }
        ////}

        [Fact(Skip = "Comment out the Skip when you need to Migrate data")]
        // ReSharper disable once FunctionComplexityOverflow
        public void MigrateProductJsonAttributesToColumns()
        {
            using var context = new ClarityEcommerceEntities();
            const int Skip = 0;
            const int Take = 36000;
            var query = context.Products
                .Where(x => x.Active && x.JsonAttributes != null && x.JsonAttributes != "{}")
                .OrderBy(x => x.ID)
                .Skip(Skip)
                .Take(Take)
                .Select(x => new { x.ID, x.Name, x.JsonAttributes });
            var timestamp = DateTime.Now;
            var packageTypeID = context.PackageTypes.Single(x => x.CustomKey == "Package").ID;
            var masterPackTypeID = context.PackageTypes.Single(x => x.CustomKey == "Master Pack").ID;
            // var palletTypeID = context.PackageTypes.Single(x => x.CustomKey == "Pallet");
            foreach (var product in query)
            {
                var serial = product.JsonAttributes.DeserializeAttributesDictionary();
                var entity = context.Products.Include(x => x.Package).Include(x => x.MasterPack).Single(x => x.ID == product.ID);
                // Product Brand
                if (serial.ContainsKey("BrandName"))
                {
                    // ReSharper disable once PossibleNullReferenceException
                    entity.BrandName = Newtonsoft.Json.JsonConvert.DeserializeObject<Dictionary<string, object>>(serial["BrandName"].Value)!["Value"] as string;
                    // ReSharper disable once UnusedVariable
                    serial.TryRemove("BrandName", out var brandName);
                }
                #region Product Dimensions
                var hasDepth = serial.ContainsKey("Depth");
                var hasWidth = serial.ContainsKey("Width");
                var hasHeight = serial.ContainsKey("Height");
                var hasLength = serial.ContainsKey("Length");
                var hasWeight = serial.ContainsKey("Weight");
                if (hasDepth)
                {
                    // ReSharper disable once PossibleNullReferenceException
                    var valueDict = Newtonsoft.Json.JsonConvert.DeserializeObject<Dictionary<string, object>>(serial["Depth"].Value)!;
                    var raw = valueDict["Value"] as string;
                    var val = TryParseVal(raw!, out var doAssign);
                    if (doAssign)
                    {
                        entity.Depth = val;
                        entity.DepthUnitOfMeasure = valueDict["UofM"] as string;
                        serial.TryRemove("Depth", out var dummy);
                    }
                }
                else if (hasLength)
                {
                    // Length to Depth field if no Depth field
                    // ReSharper disable once PossibleNullReferenceException
                    var valueDict = Newtonsoft.Json.JsonConvert.DeserializeObject<Dictionary<string, object>>(serial["Length"].Value)!;
                    var raw = valueDict["Value"] as string;
                    var val = TryParseVal(raw!, out var doAssign);
                    if (doAssign)
                    {
                        entity.Depth = val;
                        entity.DepthUnitOfMeasure = valueDict["UofM"] as string;
                        serial.TryRemove("Length", out var dummy);
                        hasLength = false; // Don't try to use Length elsewhere since it's now used
                    }
                }
                if (hasWidth)
                {
                    // ReSharper disable once PossibleNullReferenceException
                    var valueDict = Newtonsoft.Json.JsonConvert.DeserializeObject<Dictionary<string, object>>(serial["Width"].Value)!;
                    var raw = valueDict["Value"] as string;
                    var val = TryParseVal(raw!, out var doAssign);
                    if (doAssign)
                    {
                        entity.Width = val;
                        entity.WidthUnitOfMeasure = valueDict["UofM"] as string;
                        serial.TryRemove("Width", out var dummy);
                    }
                }
                else if (!hasDepth && hasLength)
                {
                    // Length to Width field if have Depth and no Width field
                    // ReSharper disable once PossibleNullReferenceException
                    var valueDict = Newtonsoft.Json.JsonConvert.DeserializeObject<Dictionary<string, object>>(serial["Length"].Value)!;
                    var raw = valueDict["Value"] as string;
                    var val = TryParseVal(raw!, out var doAssign);
                    if (doAssign)
                    {
                        entity.Width = val;
                        entity.WidthUnitOfMeasure = valueDict["UofM"] as string;
                        serial.TryRemove("Length", out var dummy);
                        hasLength = false; // Don't try to use Length elsewhere since it's now used
                    }
                }
                if (hasHeight)
                {
                    // ReSharper disable once PossibleNullReferenceException
                    var valueDict = Newtonsoft.Json.JsonConvert.DeserializeObject<Dictionary<string, object>>(serial["Height"].Value)!;
                    var raw = valueDict["Value"] as string;
                    var val = TryParseVal(raw!, out var doAssign);
                    if (doAssign)
                    {
                        entity.Height = val;
                        entity.HeightUnitOfMeasure = valueDict["UofM"] as string;
                        serial.TryRemove("Height", out var dummy);
                    }
                }
                else if (!hasDepth && !hasWidth && hasLength)
                {
                    // Length to Width field if have Depth and Width and no Height field
                    // ReSharper disable once PossibleNullReferenceException
                    var valueDict = Newtonsoft.Json.JsonConvert.DeserializeObject<Dictionary<string, object>>(serial["Length"].Value)!;
                    var raw = valueDict["Value"] as string;
                    var val = TryParseVal(raw!, out var doAssign);
                    if (doAssign)
                    {
                        entity.Height = val;
                        entity.HeightUnitOfMeasure = valueDict["UofM"] as string;
                        serial.TryRemove("Height", out var dummy);
                        // hasLength = false; // Don't try to use Length elsewhere since it's now used
                    }
                }
                if (hasWeight)
                {
                    // ReSharper disable once PossibleNullReferenceException
                    var valueDict = Newtonsoft.Json.JsonConvert.DeserializeObject<Dictionary<string, object>>(serial["Weight"].Value)!;
                    var raw = valueDict["Value"] as string;
                    var val = TryParseVal(raw!, out var doAssign);
                    if (doAssign)
                    {
                        entity.Weight = val;
                        entity.WeightUnitOfMeasure = valueDict["UofM"] as string;
                        serial.TryRemove("Weight", out var dummy);
                    }
                }
                #endregion
                // Package Dimensions
                entity.Package = ApplyDimensions(timestamp, packageTypeID, "Item_Consumer_Package_Width", "Item_Consumer_Package_Depth", "Item_Consumer_Package_Height", "Item_Consumer_Package_Weight", ref serial, entity.PackageID, entity.Package!, product.Name!, "Package for ");
                // Master Pack Dimensions
                entity.MasterPack = ApplyDimensions(timestamp, masterPackTypeID, "Item_Master_Pack_Width", "Item_Master_Pack_Depth", "Item_Master_Pack_Height", "Item_Master_Pack_Weight", ref serial, entity.MasterPackID, entity.MasterPack!, product.Name!, "Master Pack for ");
                // Assign the modified dictionary back
                entity.JsonAttributes = serial.SerializeAttributesDictionary();
            }
            context.SaveUnitOfWork();
        }

        private static Package ApplyDimensions(
            DateTime timestamp,
            int typeID,
            string widthKey,
            string depthKey,
            string heightKey,
            string weightKey,
            ref SerializableAttributesDictionary serial,
            int? packageID,
            Package package,
            string productName,
            string packageNamePrefix)
        {
            var hasDepth = serial.ContainsKey(depthKey);
            var hasWidth = serial.ContainsKey(widthKey);
            var hasHeight = serial.ContainsKey(heightKey);
            var hasWeight = serial.ContainsKey(weightKey);
            var hasAnyValue = hasDepth || hasWidth || hasHeight || hasWeight;
            if (!hasAnyValue)
            {
                return package;
            }
            if (!packageID.HasValue)
            {
                package = new Package
                {
                    Active = true,
                    CreatedDate = timestamp,
                    TypeID = typeID,
                    IsCustom = true,
                };
            }
            package.Name = packageNamePrefix + productName;
            if (hasWeight)
            {
                // ReSharper disable once PossibleNullReferenceException
                var valueDict = Newtonsoft.Json.JsonConvert.DeserializeObject<Dictionary<string, object>>(serial[weightKey].Value)!;
                var raw = valueDict["Value"] as string;
                var val = TryParseVal(raw!, out var doAssign);
                if (doAssign)
                {
                    package.Weight = val;
                    package.WeightUnitOfMeasure = valueDict["UofM"] as string;
                    serial.TryRemove(weightKey, out _);
                }
            }
            if (hasWidth)
            {
                // ReSharper disable once PossibleNullReferenceException
                var valueDict = Newtonsoft.Json.JsonConvert.DeserializeObject<Dictionary<string, object>>(serial[widthKey].Value)!;
                var raw = valueDict["Value"] as string;
                var val = TryParseVal(raw!, out var doAssign);
                if (doAssign)
                {
                    package.Width = val;
                    package.WidthUnitOfMeasure = valueDict["UofM"] as string;
                    serial.TryRemove(widthKey, out _);
                }
            }
            if (hasDepth)
            {
                // ReSharper disable once PossibleNullReferenceException
                var valueDict = Newtonsoft.Json.JsonConvert.DeserializeObject<Dictionary<string, object>>(serial[depthKey].Value)!;
                var raw = valueDict["Value"] as string;
                var val = TryParseVal(raw!, out var doAssign);
                if (doAssign)
                {
                    package.Depth = val;
                    package.DepthUnitOfMeasure = valueDict["UofM"] as string;
                    serial.TryRemove(depthKey, out _);
                }
            }
            // ReSharper disable once InvertIf
            if (hasHeight)
            {
                // ReSharper disable once PossibleNullReferenceException
                var valueDict = Newtonsoft.Json.JsonConvert.DeserializeObject<Dictionary<string, object>>(serial[heightKey].Value)!;
                var raw = valueDict["Value"] as string;
                var val = TryParseVal(raw!, out var doAssign);
                // ReSharper disable once InvertIf
                if (doAssign)
                {
                    package.Height = val;
                    package.HeightUnitOfMeasure = valueDict["UofM"] as string;
                    // serial.Remove(heightKey);
                }
            }
            return package;
        }

        private static decimal TryParseVal(string raw, out bool doAssign)
        {
            Match m;
            doAssign = false;
            if (decimal.TryParse(raw, out var val))
            {
                doAssign = true;
            }
            else if ((m = ImperialMeasurementRegexA.Match(raw)).Success)
            {
                val = decimal.Parse(m.Groups["whole"].Value) + decimal.Parse(m.Groups["divTop"].Value) / decimal.Parse(m.Groups["divBot"].Value);
                doAssign = true;
            }
            else if ((m = ImperialMeasurementRegexB.Match(raw)).Success)
            {
                val = decimal.Parse(m.Groups["divTop"].Value) / decimal.Parse(m.Groups["divBot"].Value);
                doAssign = true;
            }
            return val;
        }

        [Fact(Skip = "Building Process")]
        public void TestQueryingJsonAttributeData()
        {
            using var context = new ClarityEcommerceEntities();
            // Key/Value Pair we are looking for
            var key = "Color";
            var value = "WHITE";
            // var values = new [] { "WHITE" };
            var containsKey = $"\"{key}\"";
            var containsValueLead = "\"Value\":\"";
            var containsValueLeadLength = containsValueLead.Length;
            // var containsValue = $"{containsValueLead}{value}\",";
            // var attrRecord = $"\"{key}\":{{\"ID\":1,\"Key\":null,\"Value\":\"{value}\",\"UofM\":null,\"SortOrder\":null}}";
            var recordEnd = "}";
            var valueEnd = "\",\"UofM\"";
            // Build the query
            var query = context.Products
                .Where(x => x.JsonAttributes != null && x.JsonAttributes != "{}")
                .Where(x => x.JsonAttributes!.Contains(containsKey))
                // .Where(x => x.JsonAttributes.Contains(containsValue))
                .Select(x => new
                {
                    x.ID,
                    x.CustomKey,
                    x.Name,
                    x.JsonAttributes,
                    IndexOfContainsKey = x.JsonAttributes!.IndexOf(containsKey),
                    Substring1 = x.JsonAttributes
                        // Substring starting at record start
                        .Substring(x.JsonAttributes.IndexOf(containsKey)),
                    // This may be a Value end or the end of the record, we don't really
                    // care either way because we're at least passed where we want
                    IndexOfRecordEnd = x.JsonAttributes
                        // Substring starting at record start
                        .Substring(x.JsonAttributes.IndexOf(containsKey))
                        // Find the closing point (add 1 so we get the closing point itself)
                        .IndexOf(recordEnd) + 1,
                    Substring2 = x.JsonAttributes
                        // Substring starting at record start
                        .Substring(x.JsonAttributes.IndexOf(containsKey))
                        // Substring of Substring that started at record starting point and stopping at our ending point
                        .Substring(0, x.JsonAttributes
                            // Substring starting at record start
                            .Substring(x.JsonAttributes.IndexOf(containsKey))
                            // Find the closing point (add 1 so we get the closing point itself)
                            .IndexOf(recordEnd) + 1),
                    IndexOfContainsValue = x.JsonAttributes
                        // Substring starting at record start
                        .Substring(x.JsonAttributes.IndexOf(containsKey))
                        // Substring of Substring that started at record starting point and stopping at our ending point
                        .Substring(0, x.JsonAttributes
                            // Substring starting at record start
                            .Substring(x.JsonAttributes.IndexOf(containsKey))
                            // Find the closing point (add 1 so we get the closing point itself)
                            .IndexOf(recordEnd) + 1)
                        // Find the starting point of the Value (add the lead length so we start at the value itself)
                        .IndexOf(containsValueLead) + containsValueLeadLength,
                    Substring3 =
                        x.JsonAttributes
                            // Substring starting at record start
                            .Substring(x.JsonAttributes.IndexOf(containsKey))
                            // Substring of Substring that started at record starting point and stopping at our ending point
                            .Substring(0, x.JsonAttributes
                                // Substring starting at record start
                                .Substring(x.JsonAttributes.IndexOf(containsKey))
                                // Find the closing point (add 1 so we get the closing point itself)
                                .IndexOf(recordEnd) + 1)
                            .Substring(
                                x.JsonAttributes
                                    // Substring starting at record start
                                    .Substring(x.JsonAttributes.IndexOf(containsKey))
                                    // Substring of Substring that started at record starting point and stopping at our ending point
                                    .Substring(0, x.JsonAttributes
                                        // Substring starting at record start
                                        .Substring(x.JsonAttributes.IndexOf(containsKey))
                                        // Find the closing point (add 1 so we get the closing point itself)
                                        .IndexOf(recordEnd) + 1)
                                    // Find the starting point of the Value (add the lead length so we start at the value itself)
                                    .IndexOf(containsValueLead) + containsValueLeadLength),
                    IndexOfValueEnd =
                        x.JsonAttributes
                            // Substring starting at record start
                            .Substring(x.JsonAttributes.IndexOf(containsKey))
                            // Substring of Substring that started at record starting point and stopping at our ending point
                            .Substring(0, x.JsonAttributes
                                // Substring starting at record start
                                .Substring(x.JsonAttributes.IndexOf(containsKey))
                                // Find the closing point (add 1 so we get the closing point itself)
                                .IndexOf(recordEnd) + 1)
                            .Substring(
                                x.JsonAttributes
                                    // Substring starting at record start
                                    .Substring(x.JsonAttributes.IndexOf(containsKey))
                                    // Substring of Substring that started at record starting point and stopping at our ending point
                                    .Substring(0, x.JsonAttributes
                                        // Substring starting at record start
                                        .Substring(x.JsonAttributes.IndexOf(containsKey))
                                        // Find the closing point (add 1 so we get the closing point itself)
                                        .IndexOf(recordEnd) + 1)
                                    // Find the starting point of the Value (add the lead length so we start at the value itself)
                                    .IndexOf(containsValueLead) + containsValueLeadLength)
                            // Look for the end of the value, which should lead into a UofM (don't add 1 because we don't want the closing ")
                            .IndexOf(valueEnd),
                    Substring4 =
                        x.JsonAttributes
                            // Substring starting at record start
                            .Substring(x.JsonAttributes.IndexOf(containsKey))
                            // Substring of Substring that started at record starting point and stopping at our ending point
                            .Substring(0, x.JsonAttributes
                                // Substring starting at record start
                                .Substring(x.JsonAttributes.IndexOf(containsKey))
                                // Find the closing point (add 1 so we get the closing point itself)
                                .IndexOf(recordEnd) + 1)
                            .Substring(
                                x.JsonAttributes
                                    // Substring starting at record start
                                    .Substring(x.JsonAttributes.IndexOf(containsKey))
                                    // Substring of Substring that started at record starting point and stopping at our ending point
                                    .Substring(0, x.JsonAttributes
                                        // Substring starting at record start
                                        .Substring(x.JsonAttributes.IndexOf(containsKey))
                                        // Find the closing point (add 1 so we get the closing point itself)
                                        .IndexOf(recordEnd) + 1)
                                    // Find the starting point of the Value (add the lead length so we start at the value itself)
                                    .IndexOf(containsValueLead) + containsValueLeadLength)
                            .Substring(0, x.JsonAttributes
                                // Substring starting at record start
                                .Substring(x.JsonAttributes.IndexOf(containsKey))
                                // Substring of Substring that started at record starting point and stopping at our ending point
                                .Substring(0, x.JsonAttributes
                                    // Substring starting at record start
                                    .Substring(x.JsonAttributes.IndexOf(containsKey))
                                    // Find the closing point (add 1 so we get the closing point itself)
                                    .IndexOf(recordEnd) + 1)
                                .Substring(
                                    x.JsonAttributes
                                        // Substring starting at record start
                                        .Substring(x.JsonAttributes.IndexOf(containsKey))
                                        // Substring of Substring that started at record starting point and stopping at our ending point
                                        .Substring(0, x.JsonAttributes
                                            // Substring starting at record start
                                            .Substring(x.JsonAttributes.IndexOf(containsKey))
                                            // Find the closing point (add 1 so we get the closing point itself)
                                            .IndexOf(recordEnd) + 1)
                                        // Find the starting point of the Value (add the lead length so we start at the value itself)
                                        .IndexOf(containsValueLead) + containsValueLeadLength)
                                // Look for the end of the value, which should lead into a UofM (don't add 1 because we don't want the closing ")
                                .IndexOf(valueEnd)),
                    ContainsValue =
                        x.JsonAttributes
                            // Substring starting at record start
                            .Substring(x.JsonAttributes.IndexOf(containsKey))
                            // Substring of Substring that started at record starting point and stopping at our ending point
                            .Substring(0, x.JsonAttributes
                                // Substring starting at record start
                                .Substring(x.JsonAttributes.IndexOf(containsKey))
                                // Find the closing point (add 1 so we get the closing point itself)
                                .IndexOf(recordEnd) + 1)
                            .Substring(
                                x.JsonAttributes
                                    // Substring starting at record start
                                    .Substring(x.JsonAttributes.IndexOf(containsKey))
                                    // Substring of Substring that started at record starting point and stopping at our ending point
                                    .Substring(0, x.JsonAttributes
                                        // Substring starting at record start
                                        .Substring(x.JsonAttributes.IndexOf(containsKey))
                                        // Find the closing point (add 1 so we get the closing point itself)
                                        .IndexOf(recordEnd) + 1)
                                    // Find the starting point of the Value (add the lead length so we start at the value itself)
                                    .IndexOf(containsValueLead) + containsValueLeadLength)
                            .Substring(0, x.JsonAttributes
                                // Substring starting at record start
                                .Substring(x.JsonAttributes.IndexOf(containsKey))
                                // Substring of Substring that started at record starting point and stopping at our ending point
                                .Substring(0, x.JsonAttributes
                                    // Substring starting at record start
                                    .Substring(x.JsonAttributes.IndexOf(containsKey))
                                    // Find the closing point (add 1 so we get the closing point itself)
                                    .IndexOf(recordEnd) + 1)
                                .Substring(
                                    x.JsonAttributes
                                        // Substring starting at record start
                                        .Substring(x.JsonAttributes.IndexOf(containsKey))
                                        // Substring of Substring that started at record starting point and stopping at our ending point
                                        .Substring(0, x.JsonAttributes
                                            // Substring starting at record start
                                            .Substring(x.JsonAttributes.IndexOf(containsKey))
                                            // Find the closing point (add 1 so we get the closing point itself)
                                            .IndexOf(recordEnd) + 1)
                                        // Find the starting point of the Value (add the lead length so we start at the value itself)
                                        .IndexOf(containsValueLead) + containsValueLeadLength)
                                // Look for the end of the value, which should lead into a UofM (don't add 1 because we don't want the closing ")
                                .IndexOf(valueEnd))
                            .Contains(value),
                })
                .OrderBy(x => x.ID);
            // Print the result Count (to compare with SQL call)
            TestOutputHelper.WriteLine(query.Count().ToString());
            // Print the top 30 rows (to compare with SQL call)
            foreach (var p in query.Take(30).ToList())
            {
                TestOutputHelper.WriteLine($"\"{p.ID:0000000}\", \"{p.CustomKey}\", \"{p.Name}\", \"{p.JsonAttributes}\"");
                // TestOutputHelper.WriteLine($"\"{p.ID:0000000}\", {p.IndexOfContainsKey:0000}, {p.Substring1}");
                // TestOutputHelper.WriteLine($"\"{p.ID:0000000}\", {p.IndexOfContainsKey:0000}, {p.IndexOfRecordEnd:0000}, {p.Substring2}");
                // TestOutputHelper.WriteLine($"\"{p.ID:0000000}\", {p.IndexOfContainsKey:0000}, {p.IndexOfRecordEnd:0000}, {p.IndexOfContainsValue:000}, {p.Substring3}");
                // TestOutputHelper.WriteLine($"\"{p.ID:0000000}\", {p.IndexOfContainsKey:0000}, {p.IndexOfRecordEnd:0000}, {p.IndexOfContainsValue:000}, {p.IndexOfValueEnd:000}, {p.Substring4}");
                TestOutputHelper.WriteLine($"\"{p.ID:0000000}\", {p.IndexOfContainsKey:0000}, {p.IndexOfRecordEnd:0000}, {p.IndexOfContainsValue:000}, {p.IndexOfValueEnd:000}, {p.Substring4} Contains: {(p.ContainsValue ? "Yes" : "No")}");
                TestOutputHelper.WriteLine(string.Empty);
            }
        }

        [Fact(Skip = "Building Process")]
        public void TestQueryingJsonAttributeData_Works()
        {
            using var context = new ClarityEcommerceEntities();
            // Key/Value Pair we are looking for
            var key = "Color";
            var value = "WHITE";
            var containsKey = $"\"{key}\"";
            var containsValueLead = "\"Value\":\"";
            var containsValueLeadLength = containsValueLead.Length;
            // var attrRecord = $"\"{key}\":{{\"ID\":1,\"Key\":null,\"Value\":\"{value}\",\"UofM\":null,\"SortOrder\":null}}";
            var recordEnd = "}";
            var valueEnd = "\",\"UofM\"";
            // Build the query
            var query = context.Products
                .Where(x => x.JsonAttributes != null && x.JsonAttributes != "{}")
                .Where(x => x.JsonAttributes!.Contains(containsKey))
                .Where(x => x.JsonAttributes!
                        .Substring(x.JsonAttributes.IndexOf(containsKey))
                        .Substring(0, x.JsonAttributes.Substring(x.JsonAttributes.IndexOf(containsKey)).IndexOf(recordEnd) + 1)
                        .Substring(
                            x.JsonAttributes
                                .Substring(x.JsonAttributes.IndexOf(containsKey))
                                .Substring(0, x.JsonAttributes.Substring(x.JsonAttributes.IndexOf(containsKey)).IndexOf(recordEnd) + 1)
                                .IndexOf(containsValueLead) + containsValueLeadLength)
                        .Substring(0, x.JsonAttributes
                            .Substring(x.JsonAttributes.IndexOf(containsKey))
                            .Substring(0, x.JsonAttributes.Substring(x.JsonAttributes.IndexOf(containsKey)).IndexOf(recordEnd) + 1)
                            .Substring(
                                x.JsonAttributes
                                    .Substring(x.JsonAttributes.IndexOf(containsKey))
                                    .Substring(0, x.JsonAttributes.Substring(x.JsonAttributes.IndexOf(containsKey)).IndexOf(recordEnd) + 1)
                                    .IndexOf(containsValueLead) + containsValueLeadLength)
                            .IndexOf(valueEnd))
                        .Contains(value))
                .Select(x => new
                {
                    x.ID,
                    x.CustomKey,
                    x.Name,
                    x.JsonAttributes,
                })
                .OrderBy(x => x.ID);
            // Print the result Count (to compare with SQL call)
            TestOutputHelper.WriteLine(query.Count().ToString());
            // Print the top 30 rows (to compare with SQL call)
            foreach (var p in query.Take(30).ToList())
            {
                TestOutputHelper.WriteLine($"\"{p.ID:0000000}\"\t\"{p.CustomKey}\"\t\"{p.Name}\"\t\"{p.JsonAttributes}\"");
            }
        }

        [Fact(Skip = "Building Process")]
        public void TestQueryingJsonAttributeData_Alt()
        {
            using var context = new ClarityEcommerceEntities();
            // Key/Value Pair we are looking for
            // var key = "Color";
            // var value = "WHITE";
            var allowedValuesDictionary = new Dictionary<string, string[]>
            {
                ["Color"] = new[] { "WHITE" },
            };
            // var values = new [] { "WHITE" };
            // var containsKey = "\"Color\"";
            // var containsValueLead = "\"Value\":\"";
            // var containsValueLeadLength = containsValueLead.Length;
            // var containsValue = $"{containsValueLead}{value}\",";
            // var attrRecord = $"\"{key}\":{{\"ID\":1,\"Key\":null,\"Value\":\"{value}\",\"UofM\":null,\"SortOrder\":null}}";
            // var recordEnd = "}";
            // var valueEnd = "\",\"UofM\"";
            // Build the query
            var query = context.Products
                // .Where(x => x.JsonAttributes != null && x.JsonAttributes != "{}")
                // .Where(x => x.JsonAttributes.Contains(containsKey))
                .AsExpandable().Where(BuildJsonAttributePredicate(allowedValuesDictionary))
                // .Where(x => x.JsonAttributes.Contains(containsValue))
                .Select(x => new
                {
                    x.ID,
                    x.CustomKey,
                    x.Name,
                    x.JsonAttributes,
                    /*IndexOfContainsKey = x.JsonAttributes.IndexOf(containsKey),
                        Substring1 = x.JsonAttributes
                            // Substring starting at record start
                            .Substring(x.JsonAttributes.IndexOf(containsKey)),
                        // This may be a Value end or the end of the record, we don't really
                        // care either way because we're at least passed where we want
                        IndexOfRecordEnd = x.JsonAttributes
                            // Substring starting at record start
                            .Substring(x.JsonAttributes.IndexOf(containsKey))
                            // Find the closing point (add 1 so we get the closing point itself)
                            .IndexOf(recordEnd) + 1,
                        Substring2 = x.JsonAttributes
                            // Substring starting at record start
                            .Substring(x.JsonAttributes.IndexOf(containsKey))
                            // Substring of Substring that started at record starting point and stopping at our ending point
                            .Substring(0, x.JsonAttributes
                                // Substring starting at record start
                                .Substring(x.JsonAttributes.IndexOf(containsKey))
                                // Find the closing point (add 1 so we get the closing point itself)
                                .IndexOf(recordEnd) + 1),
                        IndexOfContainsValue = x.JsonAttributes
                            // Substring starting at record start
                            .Substring(x.JsonAttributes.IndexOf(containsKey))
                            // Substring of Substring that started at record starting point and stopping at our ending point
                            .Substring(0, x.JsonAttributes
                                // Substring starting at record start
                                .Substring(x.JsonAttributes.IndexOf(containsKey))
                                // Find the closing point (add 1 so we get the closing point itself)
                                .IndexOf(recordEnd) + 1)
                            // Find the starting point of the Value (add the lead length so we start at the value itself)
                            .IndexOf(containsValueLead) + containsValueLeadLength,
                        Substring3 =
                            x.JsonAttributes
                                // Substring starting at record start
                                .Substring(x.JsonAttributes.IndexOf(containsKey))
                                // Substring of Substring that started at record starting point and stopping at our ending point
                                .Substring(0, x.JsonAttributes
                                    // Substring starting at record start
                                    .Substring(x.JsonAttributes.IndexOf(containsKey))
                                    // Find the closing point (add 1 so we get the closing point itself)
                                    .IndexOf(recordEnd) + 1)
                                .Substring(
                                    x.JsonAttributes
                                        // Substring starting at record start
                                        .Substring(x.JsonAttributes.IndexOf(containsKey))
                                        // Substring of Substring that started at record starting point and stopping at our ending point
                                        .Substring(0, x.JsonAttributes
                                            // Substring starting at record start
                                            .Substring(x.JsonAttributes.IndexOf(containsKey))
                                            // Find the closing point (add 1 so we get the closing point itself)
                                            .IndexOf(recordEnd) + 1)
                                        // Find the starting point of the Value (add the lead length so we start at the value itself)
                                        .IndexOf(containsValueLead) + containsValueLeadLength),
                        IndexOfValueEnd =
                            x.JsonAttributes
                                // Substring starting at record start
                                .Substring(x.JsonAttributes.IndexOf(containsKey))
                                // Substring of Substring that started at record starting point and stopping at our ending point
                                .Substring(0, x.JsonAttributes
                                    // Substring starting at record start
                                    .Substring(x.JsonAttributes.IndexOf(containsKey))
                                    // Find the closing point (add 1 so we get the closing point itself)
                                    .IndexOf(recordEnd) + 1)
                                .Substring(
                                    x.JsonAttributes
                                        // Substring starting at record start
                                        .Substring(x.JsonAttributes.IndexOf(containsKey))
                                        // Substring of Substring that started at record starting point and stopping at our ending point
                                        .Substring(0, x.JsonAttributes
                                            // Substring starting at record start
                                            .Substring(x.JsonAttributes.IndexOf(containsKey))
                                            // Find the closing point (add 1 so we get the closing point itself)
                                            .IndexOf(recordEnd) + 1)
                                        // Find the starting point of the Value (add the lead length so we start at the value itself)
                                        .IndexOf(containsValueLead) + containsValueLeadLength)
                                // Look for the end of the value, which should lead into a UofM (don't add 1 because we don't want the closing ")
                                .IndexOf(valueEnd),
                        Substring4 =
                            x.JsonAttributes
                                // Substring starting at record start
                                .Substring(x.JsonAttributes.IndexOf(containsKey))
                                // Substring of Substring that started at record starting point and stopping at our ending point
                                .Substring(0, x.JsonAttributes
                                    // Substring starting at record start
                                    .Substring(x.JsonAttributes.IndexOf(containsKey))
                                    // Find the closing point (add 1 so we get the closing point itself)
                                    .IndexOf(recordEnd) + 1)
                                .Substring(
                                    x.JsonAttributes
                                        // Substring starting at record start
                                        .Substring(x.JsonAttributes.IndexOf(containsKey))
                                        // Substring of Substring that started at record starting point and stopping at our ending point
                                        .Substring(0, x.JsonAttributes
                                            // Substring starting at record start
                                            .Substring(x.JsonAttributes.IndexOf(containsKey))
                                            // Find the closing point (add 1 so we get the closing point itself)
                                            .IndexOf(recordEnd) + 1)
                                        // Find the starting point of the Value (add the lead length so we start at the value itself)
                                        .IndexOf(containsValueLead) + containsValueLeadLength)
                                .Substring(0, x.JsonAttributes
                                    // Substring starting at record start
                                    .Substring(x.JsonAttributes.IndexOf(containsKey))
                                    // Substring of Substring that started at record starting point and stopping at our ending point
                                    .Substring(0, x.JsonAttributes
                                        // Substring starting at record start
                                        .Substring(x.JsonAttributes.IndexOf(containsKey))
                                        // Find the closing point (add 1 so we get the closing point itself)
                                        .IndexOf(recordEnd) + 1)
                                    .Substring(
                                        x.JsonAttributes
                                            // Substring starting at record start
                                            .Substring(x.JsonAttributes.IndexOf(containsKey))
                                            // Substring of Substring that started at record starting point and stopping at our ending point
                                            .Substring(0, x.JsonAttributes
                                                // Substring starting at record start
                                                .Substring(x.JsonAttributes.IndexOf(containsKey))
                                                // Find the closing point (add 1 so we get the closing point itself)
                                                .IndexOf(recordEnd) + 1)
                                            // Find the starting point of the Value (add the lead length so we start at the value itself)
                                            .IndexOf(containsValueLead) + containsValueLeadLength)
                                    // Look for the end of the value, which should lead into a UofM (don't add 1 because we don't want the closing ")
                                    .IndexOf(valueEnd)
                                ),*/
                })
                .OrderBy(x => x.ID);
            // Print the result Count (to compare with SQL call)
            TestOutputHelper.WriteLine(query.Count().ToString());
            // Print the top 30 rows (to compare with SQL call)
            foreach (var p in query.Take(30).ToList())
            {
                TestOutputHelper.WriteLine($"\"{p.ID:0000000}\", \"{p.CustomKey}\", \"{p.Name}\", \"{p.JsonAttributes}\"");
                // TestOutputHelper.WriteLine($"\"{p.ID:0000000}\", {p.IndexOfContainsKey:0000}, {p.Substring1}");
                // TestOutputHelper.WriteLine($"\"{p.ID:0000000}\", {p.IndexOfContainsKey:0000}, {p.IndexOfRecordEnd:0000}, {p.Substring2}");
                // TestOutputHelper.WriteLine($"\"{p.ID:0000000}\", {p.IndexOfContainsKey:0000}, {p.IndexOfRecordEnd:0000}, {p.IndexOfContainsValue:000}, {p.Substring3}");
                // TestOutputHelper.WriteLine($"\"{p.ID:0000000}\", {p.IndexOfContainsKey:0000}, {p.IndexOfRecordEnd:0000}, {p.IndexOfContainsValue:000}, {p.IndexOfValueEnd:000}, {p.Substring4}");
                // TestOutputHelper.WriteLine($"\"{p.ID:0000000}\", {p.IndexOfContainsKey:0000}, {p.IndexOfRecordEnd:0000}, {p.IndexOfContainsValue:000}, {p.IndexOfValueEnd:000}, {p.Substring4}");
                TestOutputHelper.WriteLine(string.Empty);
            }
        }

        private static Expression<Func<Product, bool>> BuildJsonAttributePredicate(Dictionary<string, string[]> jsonAttributes)
        {
            // ReSharper disable StringIndexOfIsCultureSpecific.1, StringIndexOfIsCultureSpecific.2
            var result = PredicateBuilder.New<Product>(true);
            if (jsonAttributes == null)
            {
                return result;
            }
            var iterateList = jsonAttributes.Where(kvp => kvp.Value.Any(x => !string.IsNullOrWhiteSpace(x))).ToList();
            if (!iterateList.Any())
            {
                return result;
            }
            result = result.And(x => x.JsonAttributes != null && x.JsonAttributes != "{}");
            foreach (var kvp in iterateList)
            {
                // Key/Value Pair we are looking for
                var containsKey = $"\"{kvp.Key}\"";
                var allowedValues = kvp.Value.Where(x => !string.IsNullOrWhiteSpace(x));
                result = result
                    .And(x => allowedValues
                        .Select(y => new
                        {
                            AllowedValue = y,
                            IndexOfRecordStart = x.JsonAttributes!.IndexOf(containsKey),
                        })
                        .Select(y => new
                        {
                            y.AllowedValue,
                            DictStartingAtRecord = x.JsonAttributes!.Substring(y.IndexOfRecordStart),
                        })
                        .Select(y => new
                        {
                            y.AllowedValue,
                            y.DictStartingAtRecord,
                            IndexOfRecordEnd = (y.DictStartingAtRecord.IndexOf("}") > 0 ? y.DictStartingAtRecord.IndexOf("}") : 500) + 1,
                        })
                        .Select(y => new
                        {
                            y.AllowedValue,
                            Record = y.DictStartingAtRecord.Substring(0, y.IndexOfRecordEnd),
                        })
                        .Select(y => new
                        {
                            y.AllowedValue,
                            y.Record,
                            IndexOfValueStart = (y.Record.IndexOf("\"Value\":\"") > 0 ? y.Record.IndexOf("\"Value\":\"") : 500) + "\"Value\":\"".Length,
                        })
                        .Select(y => new
                        {
                            y.AllowedValue,
                            RecordStartingAtValue = y.Record.Substring(y.IndexOfValueStart, x.JsonAttributes!.Length),
                        })
                        .Select(y => new
                        {
                            y.AllowedValue,
                            y.RecordStartingAtValue,
                            IndexOfValueEnd = y.RecordStartingAtValue.IndexOf("\",\"UofM\"") > 0 ? y.RecordStartingAtValue.IndexOf("\",\"UofM\"") : 500,
                        })
                        .Select(y => new
                        {
                            y.AllowedValue,
                            Value = y.RecordStartingAtValue.Substring(0, y.IndexOfValueEnd),
                        })
                        .Any(y => y.Value.Contains(y.AllowedValue)));
            }
            return result;
            // ReSharper restore StringIndexOfIsCultureSpecific.1, StringIndexOfIsCultureSpecific.2
        }

        [Fact(Skip = "Run to fix data")]
        public void CreateSeoUrlsForCategoriesWhichDontHaveThem()
        {
            using var context = new ClarityEcommerceEntities();
            for (var i = 0; i < context.Categories.Count(x => x.SeoUrl == null || x.SeoUrl == string.Empty);)
            {
                var category = context.Categories.First(x => x.SeoUrl == null || x.SeoUrl == string.Empty);
                category.SeoUrl = category.Name!.ToSeoUrl();
                context.SaveUnitOfWork();
            }
        }

        [Fact(Skip = "Run to fix data")]
        public void SetSeoUrlsToSlugifiedNamesForProducts()
        {
            using var context = new ClarityEcommerceEntities();
            foreach (var product in context.Products.Where(x => x.SeoUrl == null || x.SeoUrl == string.Empty || x.SeoUrl.Contains("®")))
            {
                product.SeoUrl = product.Name!.ToSeoUrl();
            }
            context.SaveUnitOfWork();
        }

        // [Theory(Skip = "Run only to T/S database issues")]
        // [InlineData("201612021738169_InitialCreate_4pt7")]
        // [InlineData("201612070517527_SalesQuotesAndSampleRequestsSchema")]
        // [InlineData("201612070728240_FilesForAllSalesCollections")]
        // [InlineData("201612081922468_DropCheckouts")]
        // [InlineData("201612202054553_DropAttributeFilterValuesAndAccountAddresses")]
        // [InlineData("201612230147293_DropFavoritesCustomersAndIndividuals")]
        // [InlineData("201701061432005_SplitShipping")]
        // [InlineData("201701062139203_GlobalizationAndCurrency")]
        // [InlineData("201701102149344_FixTriggerName")]
        // [InlineData("201701111447011_SavedSellers")]
        // [InlineData("201702021418384_OrdersWithMultipleContacts")]
        // [InlineData("201702111901020_ImagesForGlobalizationAndGeographyAssignments")]
        // [InlineData("201702121904460_ProductSalesAnalytics")]
        // [InlineData("201703231920001_Groups")]
        // [InlineData("201703291331233_PILInSalesItems")]
        // [InlineData("201704032222033_InventoryHash")]
        // [InlineData("201704121839113_UserAccountsOptionallyRequireApprovalForLogin")]
        // [InlineData("201704140207336_CategoryHashes")]
        // [InlineData("201704172328247_StoreProductHash")]
        // [InlineData("201704211808567_CalendarEvents")]
        // ReSharper disable once UnusedMember.Local
#pragma warning disable IDE0051 // Remove unused private members
        private void DecompressDatabaseMigration(string migrationName)
#pragma warning restore IDE0051 // Remove unused private members
        {
            var connectionString = @"data source=SQLB\SQL2016;initial catalog=CEF_DEMO;persist security info=True;user id=SQLLogin;password=p4ssw0rd;MultipleActiveResultSets=true;";
            // ConfigurationManager.ConnectionStrings["ClarityEcommerceEntities"];// connection string to DB with migrations
            var sqlToExecute = $"SELECT [Model] FROM [dbo].[__MigrationHistory] WHERE [MigrationId] LIKE '%{migrationName}'";
            using var connection = new SqlConnection(connectionString);
            connection.Open();
#pragma warning disable CA2100, SCS0026 // MsSQL Data Provider: SQL injection possible in {1} argument passed to '{0}'
            var command = new SqlCommand(sqlToExecute, connection);
#pragma warning restore CA2100, SCS0026 // MsSQL Data Provider: SQL injection possible in {1} argument passed to '{0}'
            var reader = command.ExecuteReader();
            if (!reader.HasRows)
            {
                throw new Exception("Now Rows to display. Probably migration name is incorrect");
            }
            while (reader.Read())
            {
                var model = (byte[])reader["model"];
                var decompressed = Decompress(model);
                TestOutputHelper.WriteLine(Convert.ToBase64String(Encoding.UTF8.GetBytes(decompressed.ToString())));
                // TestOutputHelper.WriteLine(decompressed.ToString());
            }
        }

        /// <summary>Using decomposer from EF itself:
        /// http://entityframework.codeplex.com/SourceControl/latest#src/EntityFramework/Migrations/Edm/ModelCompressor.cs .</summary>
        /// <param name="bytes">The bytes.</param>
        /// <returns>An XDocument.</returns>
        protected virtual XDocument Decompress(byte[] bytes)
        {
            using var memoryStream = new MemoryStream(bytes);
            using var gzipStream = new GZipStream(memoryStream, CompressionMode.Decompress);
            return XDocument.Load(gzipStream);
        }

        [Fact(Skip = "Run only to fix a migration")]
        public void DecompressMigrationEncoding()
        {
            var migrationClass = (IMigrationMetadata)new CalendarEvents();
            var target = migrationClass.Target;
            TestOutputHelper.WriteLine(Convert.FromBase64String(target).ToString());
            // var decompressed = Decompress(Convert.FromBase64String(target));
            // TestOutputHelper.WriteLine(decompressed.ToString());
        }

        [Fact(Skip = "Run only to cleanup duplicated records")]
        public void CleanUpDuplicatedShipCarrierMethods()
        {
            using var context = new ClarityEcommerceEntities();
            // Retrieve active duplicated Name (for same Ship Carrier ID)
            var duplicatedShipCarrierMethods = context.ShipCarrierMethods.Where(x => x.Active)
                .GroupBy(x => new { x.ShipCarrierID, x.Name })
                .Where(grp => grp.Count() > 1)
                .Select(x => new { x.Key.Name, x.Key.ShipCarrierID, Count = x.Count() });
            foreach (var duplicatedShipCarrierMethod in duplicatedShipCarrierMethods)
            {
                // Retrieve active Ship Carrier Methods, not used, with duplicated name
                var duplicatesList = context.ShipCarrierMethods.Where(
                    x => x.Name == duplicatedShipCarrierMethod.Name
                        && x.ShipCarrierID == duplicatedShipCarrierMethod.ShipCarrierID
                        && x.Active
                        // ReSharper disable AccessToDisposedClosure
                        && !context.Shipments.Any(s => s.ShipCarrierMethodID == x.ID));
                // ReSharper restore AccessToDisposedClosure
                // If at least one record is used, remove all the others
                if (duplicatesList.Count() >= duplicatedShipCarrierMethod.Count)
                {
                    // No record used, remove all records except one
                    var firstRecordID = duplicatesList.First().ID;
                    duplicatesList = duplicatesList.Where(x => x.ID != firstRecordID);
                }
                duplicatesList.ToList().ForEach(x => context.ShipCarrierMethods.Remove(x));
            }
            context.SaveUnitOfWork();
        }

        [Fact(Skip = "Only run as needed")]
        public async Task ReSaveJsonAttributesOnProductsAsync()
        {
            using var context = new ClarityEcommerceEntities();
            await context.Products
                .Select(x => x.ID)
                .ForEachAsync(8, async id =>
                {
                    using var context2 = new ClarityEcommerceEntities();
                    var product = await context2.Products.SingleAsync(x => x.ID == id).ConfigureAwait(false);
                    var dict = product.JsonAttributes.DeserializeAttributesDictionary();
                    var changed = false;
                    foreach (var key in dict.Where(x => !Contract.CheckValidKey(x.Value.Value)).Select(x => x.Key).ToList())
                    {
                        dict.TryRemove(key, out _);
                        changed = true;
                    }
                    if (!changed)
                    {
                        return;
                    }
                    product.JsonAttributes = dict.SerializeAttributesDictionary();
                    await context2.SaveChangesAsync().ConfigureAwait(false);
                })
                .ConfigureAwait(false);
        }
    }
}
