// <copyright file="ExportSheetData.cs" company="clarity-ventures.com">
// Copyright (c) 2018-2023 clarity-ventures.com. All rights reserved.
// </copyright>
// <summary>Implements the export sheet data class</summary>
#pragma warning disable SA1600 // Elements should be documented
namespace Clarity.Ecommerce.Providers.SalesQuoteImporter.Excel
{
    using System;
    using System.Collections;
    using System.Collections.Generic;
    using System.Data.Entity;
    using System.Linq;
    using Interfaces.DataModel;
    using Interfaces.Models;

    internal class ExportSheetData
    {
        internal Dictionary<string, Dictionary<uint, object>> Data { get; } = new Dictionary<string, Dictionary<uint, object>>();

        internal int ColumnSkip { get; set; }

        internal int RowSkip { get; set; }

        internal int HeaderRowCount { private get; set; }

        private object this[string letter, uint row]
        {
            set => this[new CellReference(letter, row)] = value;
        }

        private object this[CellReference cellReference]
        {
            set
            {
                if (cellReference == null)
                {
                    throw new ArgumentException();
                }
                if (!Data.ContainsKey(cellReference.Letter))
                {
                    Data[cellReference.Letter] = new Dictionary<uint, object>();
                }
                Data[cellReference.Letter][cellReference.Row] = value;
            }
        }

        internal void InitializeColumns(IReadOnlyList<MapFromIncomingProperty> mappings)
        {
            // Initialize the skipped columns to new dictionaries
            for (var c = 0; c < ColumnSkip; c++)
            {
                var col = ExcelSalesQuoteImporterProvider.GenColumnReference(c);
                Data[col] = new Dictionary<uint, object>();
                // Initialize the skipped rows to empty strings
                for (var r = 0u; r < RowSkip; r++)
                {
                    Data[col][r] = string.Empty;
                }
            }
            // Initialize Header rows
            for (var c = ColumnSkip; c < mappings.Count; c++)
            {
                var col = ExcelSalesQuoteImporterProvider.GenColumnReference(c);
                Data[col] = new Dictionary<uint, object>();
                for (var r = (uint)RowSkip; r < HeaderRowCount + RowSkip; r++)
                {
                    Data[col][r] = mappings[c].Header[r - RowSkip];
                }
            }
        }

        // ReSharper disable once FunctionComplexityOverflow
        internal void LoadDataFromContext(int id, MapFromIncomingProperty[] mappings, Func<IClarityEcommerceEntities> getContext)
        {
#if HOLD
            var propertyNames = mappings
                .Select(mapping => mapping.Assignments
                    .Select(assignment => assignment.Entity + "." + assignment.To.Single()
                        + (assignment.To.Single() == "JsonAttributes" ? mapping.Header.Aggregate(string.Empty, (c, n) => c + "." + n) : string.Empty)
                        + (mapping.HeaderOccurrence > 0 ? "." + mapping.HeaderOccurrence : string.Empty)
                        + (assignment.Instance > 0 ? "." + assignment.Instance : string.Empty)
                    ).First()
                ).ToList();
#endif
            IList results;
            using (var context = getContext())
            {
#if HOLD
                var query = context.SalesQuotes.AsNoTracking().FilterByID(id).AsExpandable();
                var tree = LinqRuntimeTypeBuilder.SelectDynamicA(propertyNames);
                var treeRoot = tree.First().Value;
                results = (IList)Activator.CreateInstance(typeof(List<>).MakeGenericType(treeRoot.LevelDynamicType));
                treeRoot.CallExpression = Expression.Call(
                    typeof(Queryable),
                    nameof(Queryable.Select),
                    new[] { treeRoot.LevelSourceType, treeRoot.LevelDynamicType },
                    Expression.Constant(query),
                    treeRoot.SelectExpression);
                var expanded = treeRoot.CallExpression.Expand();
                var queryableResult = (IQueryable)query.Provider.Execute(expanded);
                foreach (var result in queryableResult)
                {
                    Console.WriteLine(result);
                    results.Add(result);
                }
#else
                var query = context.SalesQuotes
                    .AsNoTracking()
                    .FilterByID(id)
                    .Select(x => new
                    {
                        x.CustomKey,
                        x.JsonAttributes,
                        SalesItems = x.SalesItems
                            .Where(y => y.Active)
                            .Select(y => new
                            {
                                y.ID,
                                Master = new
                                {
                                    x.CustomKey,
                                    x.JsonAttributes,
                                },
                                y.CustomKey,
                                y.UnitCorePrice,
                                y.JsonAttributes,
                                Product = new
                                {
                                    y.Product.Name,
                                    y.Product.Description,
                                    y.Product.UnitOfMeasure,
                                    y.Product.BrandName,
                                    y.Product.ManufacturerPartNumber,
                                    y.Product.KitBaseQuantityPriceMultiplier,
                                    y.Product.JsonAttributes,
                                    ProductCategories = y.Product.ProductCategories
                                        .Where(z => z.Active)
                                        .Select(z => z.Slave.Name),
                                    ManufacturerProduct = y.Product.Manufacturers
                                        .Where(z => z.Active)
                                        .Select(z => new
                                        {
                                            z.JsonAttributes,
                                            z.Master.Name,
                                        })
                                        .FirstOrDefault(),
                                },
                            }),
                    })
                    .SelectMany(x => x.SalesItems)
                    .Select(y => new
                    {
                        MasterCustomKey = y.Master.CustomKey,
                        MasterJsonAttributes = y.Master.JsonAttributes,
                        y.CustomKey,
                        y.UnitCorePrice,
                        y.JsonAttributes,
                        y.Product.Name,
                        y.Product.Description,
                        y.Product.UnitOfMeasure,
                        y.Product.BrandName,
                        y.Product.ManufacturerPartNumber,
                        y.Product.KitBaseQuantityPriceMultiplier,
                        ProductJsonAttributes = y.Product.JsonAttributes,
                        y.Product.ProductCategories,
                        ManufacturerProductJsonAttributes = y.Product.ManufacturerProduct.JsonAttributes,
                        ManufacturerName = y.Product.ManufacturerProduct.Name,
                    })
                    .ToList()
                    .Select(y => new
                    {
                        y.MasterCustomKey,
                        MasterSerializableAttributes = y.MasterJsonAttributes.DeserializeAttributesDictionary(),
                        y.CustomKey,
                        y.UnitCorePrice,
                        LineItemSerializableAttributes = y.JsonAttributes.DeserializeAttributesDictionary(),
                        y.Name,
                        y.Description,
                        y.UnitOfMeasure,
                        y.BrandName,
                        y.ManufacturerPartNumber,
                        y.KitBaseQuantityPriceMultiplier,
                        ProductSerializableAttributes = y.ProductJsonAttributes.DeserializeAttributesDictionary(),
                        ProductCategories = y.ProductCategories.ToArray(),
                        ManufacturerProductSerializableAttributes = y.ManufacturerProductJsonAttributes.DeserializeAttributesDictionary(),
                        y.ManufacturerName,
                    })
                    // ReSharper disable once CyclomaticComplexity
                    .Select(y => new Dictionary<string, string>
                    {
                        ////["PMO_AllOrNone_OneOrMore"] = y.MasterSerializableAttributes.ContainsKey("PMO_AllOrNone_OneOrMore") ? y.MasterSerializableAttributes["PMO_AllOrNone_OneOrMore"].Value : null,
                        ////["PMO_Contact"] = y.MasterSerializableAttributes.ContainsKey("PMO_Contact") ? y.MasterSerializableAttributes["PMO_Contact"].Value : null,
                        ////["PMO_SupplyLine_Level1"] = y.ProductCategories.Length > 0 ? y.ProductCategories[0] : null,
                        ////["PMO_SupplyLine_Level2"] = y.ProductCategories.Length > 1 ? y.ProductCategories[1] : null,
                        ////["PMO_SupplyLine_Level3"] = y.ProductCategories.Length > 2 ? y.ProductCategories[2] : null,
                        ////["PMO_SupplyLine_Level4"] = y.ProductCategories.Length > 3 ? y.ProductCategories[3] : null,
                        ////["PMO_UNSPSC"] = y.ProductSerializableAttributes.ContainsKey("PMO_UNSPSC") ? y.ProductSerializableAttributes["PMO_UNSPSC"].Value : null,
                        ////["PMO_MedPDB_Key"] = y.ProductSerializableAttributes.ContainsKey("PMO_MedPDB_Key") ? y.ProductSerializableAttributes["PMO_MedPDB_Key"].Value : null,
                        ////["PMO_URL_to_OEM"] = y.ProductSerializableAttributes.ContainsKey("PMO_URL_to_OEM") ? y.ProductSerializableAttributes["PMO_URL_to_OEM"].Value : null,
                        ////["PMO_Image_Filename"] = y.ProductSerializableAttributes.ContainsKey("PMO_Image_Filename") ? y.ProductSerializableAttributes["PMO_Image_Filename"].Value : null,
                        ////["PMO_Compatibility"] = y.ProductSerializableAttributes.ContainsKey("PMO_Compatibility") ? y.ProductSerializableAttributes["PMO_Compatibility"].Value : null,
                        ////["PMO_Unique_Char"] = y.ProductSerializableAttributes.ContainsKey("PMO_Unique_Char") ? y.ProductSerializableAttributes["PMO_Unique_Char"].Value : null,
                        ////["PMO_Salient_Char"] = y.ProductSerializableAttributes.ContainsKey("PMO_Salient_Char") ? y.ProductSerializableAttributes["PMO_Salient_Char"].Value : null,
                        ////["PMO_Hist_Ann_Qty_Num_Pkgs"] = y.ProductSerializableAttributes.ContainsKey("PMO_Hist_Ann_Qty_Num_Pkgs") ? y.ProductSerializableAttributes["PMO_Hist_Ann_Qty_Num_Pkgs"].Value : null,
                        ////["PMO_Num_Items_per_Pkg"] = y.UnitOfMeasure,
                        ////["PMO_Base_Year_Est"] = y.ProductSerializableAttributes.ContainsKey("PMO_Base_Year_Est") ? y.ProductSerializableAttributes["PMO_Base_Year_Est"].Value : null,
                        ////["Manu_Pkging"] = y.ManufacturerProductSerializableAttributes.ContainsKey("Manu_Pkging") ? y.ManufacturerProductSerializableAttributes["Manu_Pkging"].Value : null,
                        ////["Units_per_pkg"] = y.KitBaseQuantityPriceMultiplier?.ToString(),
                        ////["Pkg_Price"] = y.UnitCorePrice.ToString(CultureInfo.InvariantCulture),
                        ////["Hist_ANNUAL_usage_per_pgk"] = y.ProductSerializableAttributes.ContainsKey("Hist_ANNUAL_usage_per_pgk") ? y.ProductSerializableAttributes["Hist_ANNUAL_usage_per_pgk"].Value : null,
                        ////["Hist_ANNUAL_usage_per_unit"] = y.ProductSerializableAttributes.ContainsKey("Hist_ANNUAL_usage_per_unit") ? y.ProductSerializableAttributes["Hist_ANNUAL_usage_per_unit"].Value : null,
                        ////["Base_Year_Estimate"] = y.ProductSerializableAttributes.ContainsKey("Base_Year_Estimate") ? y.ProductSerializableAttributes["Base_Year_Estimate"].Value : null,
                        ////["IGCE_5_yr_total"] = y.ProductSerializableAttributes.ContainsKey("IGCE_5_yr_total") ? y.ProductSerializableAttributes["IGCE_5_yr_total"].Value : null,
                        ////["Lowest_Comm_Price"] = y.ProductSerializableAttributes.ContainsKey("Lowest_Comm_Price") ? y.ProductSerializableAttributes["Lowest_Comm_Price"].Value : null,
                        ////["Units_per_pkg_for_lowest_Comm_price"] = y.ProductSerializableAttributes.ContainsKey("Units_per_pkg_for_lowest_Comm_price") ? y.ProductSerializableAttributes["Units_per_pkg_for_lowest_Comm_price"].Value : null,
                        ////["Lowest_Fed_price"] = y.ProductSerializableAttributes.ContainsKey("Lowest_Fed_price") ? y.ProductSerializableAttributes["Lowest_Fed_price"].Value : null,
                        ////["Units_per_pkg_for_lowest_Federal_price"] = y.ProductSerializableAttributes.ContainsKey("Units_per_pkg_for_lowest_Federal_price") ? y.ProductSerializableAttributes["Units_per_pkg_for_lowest_Federal_price"].Value : null,
                        ////["Lowest_FSS_price"] = y.ProductSerializableAttributes.ContainsKey("Lowest_FSS_price") ? y.ProductSerializableAttributes["Lowest_FSS_price"].Value : null,
                        ////["Units_per_pkg_for_lowest_FSS_price"] = y.ProductSerializableAttributes.ContainsKey("Units_per_pkg_for_lowest_FSS_price") ? y.ProductSerializableAttributes["Units_per_pkg_for_lowest_FSS_price"].Value : null,
                        ////["BEST_Pot_Savings"] = y.ProductSerializableAttributes.ContainsKey("BEST_Pot_Savings") ? y.ProductSerializableAttributes["BEST_Pot_Savings"].Value : null,
                        ////["PMO_NIF_Long_Desc"] = y.Description,
                        ////["PMO_Product_Name"] = y.Name,
                        ////["PMO_Manufacturer"] = y.ManufacturerName,
                        ////["PMO_Manu_Prod_Brand_Name"] = y.BrandName,
                        ////["PMO_OEM_Part_Num"] = y.ManufacturerPartNumber,
                        ["VOA_Tracker_ID"] = y.MasterCustomKey,
                        ["PMO_Row_Num"] = y.CustomKey,
                        ["Parent_VOA_Tracker_ID"] = y.LineItemSerializableAttributes.ContainsKey("Parent_VOA_Tracker_ID") ? y.LineItemSerializableAttributes["Parent_VOA_Tracker_ID"].Value : null,
                        ["PMO_AllOrNone_OneOrMore"] = y.LineItemSerializableAttributes.ContainsKey("PMO_AllOrNone_OneOrMore") ? y.LineItemSerializableAttributes["PMO_AllOrNone_OneOrMore"].Value : null,
                        ["PMO_Contact"] = y.LineItemSerializableAttributes.ContainsKey("PMO_Contact") ? y.LineItemSerializableAttributes["PMO_Contact"].Value : null,
                        ["PMO_SupplyLine_Level1"] = y.LineItemSerializableAttributes.ContainsKey("PMO_SupplyLine_Level1") ? y.LineItemSerializableAttributes["PMO_SupplyLine_Level1"].Value : null,
                        ["PMO_SupplyLine_Level2"] = y.LineItemSerializableAttributes.ContainsKey("PMO_SupplyLine_Level2") ? y.LineItemSerializableAttributes["PMO_SupplyLine_Level2"].Value : null,
                        ["PMO_SupplyLine_Level3"] = y.LineItemSerializableAttributes.ContainsKey("PMO_SupplyLine_Level3") ? y.LineItemSerializableAttributes["PMO_SupplyLine_Level3"].Value : null,
                        ["PMO_SupplyLine_Level4"] = y.LineItemSerializableAttributes.ContainsKey("PMO_SupplyLine_Level4") ? y.LineItemSerializableAttributes["PMO_SupplyLine_Level4"].Value : null,
                        ["PMO_UNSPSC"] = y.LineItemSerializableAttributes.ContainsKey("PMO_UNSPSC") ? y.LineItemSerializableAttributes["PMO_UNSPSC"].Value : null,
                        ["PMO_MedPDB_Key"] = y.LineItemSerializableAttributes.ContainsKey("PMO_MedPDB_Key") ? y.LineItemSerializableAttributes["PMO_MedPDB_Key"].Value : null,
                        ["PMO_NIF_Num"] = y.LineItemSerializableAttributes.ContainsKey("PMO_NIF_Num") ? y.LineItemSerializableAttributes["PMO_NIF_Num"].Value : null,
                        ["PMO_NIF_Long_Desc"] = y.LineItemSerializableAttributes.ContainsKey("PMO_NIF_Long_Desc") ? y.LineItemSerializableAttributes["PMO_NIF_Long_Desc"].Value : null,
                        ["PMO_Product_Name"] = y.LineItemSerializableAttributes.ContainsKey("PMO_Product_Name") ? y.LineItemSerializableAttributes["PMO_Product_Name"].Value : null,
                        ["PMO_Manufacturer"] = y.LineItemSerializableAttributes.ContainsKey("PMO_Manufacturer") ? y.LineItemSerializableAttributes["PMO_Manufacturer"].Value : null,
                        ["PMO_Manu_Prod_Brand_Name"] = y.LineItemSerializableAttributes.ContainsKey("PMO_Manu_Prod_Brand_Name") ? y.LineItemSerializableAttributes["PMO_Manu_Prod_Brand_Name"].Value : null,
                        ["PMO_OEM_Part_Num"] = y.LineItemSerializableAttributes.ContainsKey("PMO_OEM_Part_Num") ? y.LineItemSerializableAttributes["PMO_OEM_Part_Num"].Value : null,
                        ["PMO_URL_to_OEM"] = y.LineItemSerializableAttributes.ContainsKey("PMO_URL_to_OEM") ? y.LineItemSerializableAttributes["PMO_URL_to_OEM"].Value : null,
                        ["PMO_Image_Filename"] = y.LineItemSerializableAttributes.ContainsKey("PMO_Image_Filename") ? y.LineItemSerializableAttributes["PMO_Image_Filename"].Value : null,
                        ["PMO_Compatibility"] = y.LineItemSerializableAttributes.ContainsKey("PMO_Compatibility") ? y.LineItemSerializableAttributes["PMO_Compatibility"].Value : null,
                        ["PMO_BNOE"] = y.LineItemSerializableAttributes.ContainsKey("PMO_BNOE") ? y.LineItemSerializableAttributes["PMO_BNOE"].Value : null,
                        ["PMO_Unique_Char"] = y.LineItemSerializableAttributes.ContainsKey("PMO_Unique_Char") ? y.LineItemSerializableAttributes["PMO_Unique_Char"].Value : null,
                        ["PMO_Salient_Char"] = y.LineItemSerializableAttributes.ContainsKey("PMO_Salient_Char") ? y.LineItemSerializableAttributes["PMO_Salient_Char"].Value : null,
                        ["PMO_Hist_Ann_Qty_Num_Pkgs"] = y.LineItemSerializableAttributes.ContainsKey("PMO_Hist_Ann_Qty_Num_Pkgs") ? y.LineItemSerializableAttributes["PMO_Hist_Ann_Qty_Num_Pkgs"].Value : null,
                        ["PMO_Num_Items_per_Pkg"] = y.LineItemSerializableAttributes.ContainsKey("PMO_Num_Items_per_Pkg") ? y.LineItemSerializableAttributes["PMO_Num_Items_per_Pkg"].Value : null,
                        ["PMO_Base_Year_Est"] = y.LineItemSerializableAttributes.ContainsKey("PMO_Base_Year_Est") ? y.LineItemSerializableAttributes["PMO_Base_Year_Est"].Value : null,
                        ["PMO_FSS_or_Non_FSS"] = y.LineItemSerializableAttributes.ContainsKey("PMO_FSS_or_Non_FSS") ? y.LineItemSerializableAttributes["PMO_FSS_or_Non_FSS"].Value : null,
                        ["PMO_FSS_Link"] = y.LineItemSerializableAttributes.ContainsKey("PMO_FSS_Link") ? y.LineItemSerializableAttributes["PMO_FSS_Link"].Value : null,
                        ["PMO_FSS_Contract_Holder"] = y.LineItemSerializableAttributes.ContainsKey("PMO_FSS_Contract_Holder") ? y.LineItemSerializableAttributes["PMO_FSS_Contract_Holder"].Value : null,
                        ["PMO_FSS_Contract_Num"] = y.LineItemSerializableAttributes.ContainsKey("PMO_FSS_Contract_Num") ? y.LineItemSerializableAttributes["PMO_FSS_Contract_Num"].Value : null,
                        ["PMO_FSS_Contract_End_Date"] = y.LineItemSerializableAttributes.ContainsKey("PMO_FSS_Contract_End_Date") ? y.LineItemSerializableAttributes["PMO_FSS_Contract_End_Date"].Value : null,
                        ["Unicor_AbilityOne"] = y.LineItemSerializableAttributes.ContainsKey("Unicor_AbilityOne") ? y.LineItemSerializableAttributes["Unicor_AbilityOne"].Value : null,
                        ["FSS_Category"] = y.LineItemSerializableAttributes.ContainsKey("FSS_Category") ? y.LineItemSerializableAttributes["FSS_Category"].Value : null,
                        ["FSS_SIN"] = y.LineItemSerializableAttributes.ContainsKey("FSS_SIN") ? y.LineItemSerializableAttributes["FSS_SIN"].Value : null,
                        ["Product_Supply_Code"] = y.LineItemSerializableAttributes.ContainsKey("Product_Supply_Code") ? y.LineItemSerializableAttributes["Product_Supply_Code"].Value : null,
                        ["Manu_NAICS_Code"] = y.LineItemSerializableAttributes.ContainsKey("Manu_NAICS_Code") ? y.LineItemSerializableAttributes["Manu_NAICS_Code"].Value : null,
                        ["FDA_CLASS"] = y.LineItemSerializableAttributes.ContainsKey("FDA_CLASS") ? y.LineItemSerializableAttributes["FDA_CLASS"].Value : null,
                        ["Industry_Life_Cycle"] = y.LineItemSerializableAttributes.ContainsKey("Industry_Life_Cycle") ? y.LineItemSerializableAttributes["Industry_Life_Cycle"].Value : null,
                        ["Multiple_Sources_Avail"] = y.LineItemSerializableAttributes.ContainsKey("Multiple_Sources_Avail") ? y.LineItemSerializableAttributes["Multiple_Sources_Avail"].Value : null,
                        ["Possible_Sources"] = y.LineItemSerializableAttributes.ContainsKey("Possible_Sources") ? y.LineItemSerializableAttributes["Possible_Sources"].Value : null,
                        ["Non_Manu_Rule_Waiver_Supported"] = y.LineItemSerializableAttributes.ContainsKey("Non_Manu_Rule_Waiver_Supported") ? y.LineItemSerializableAttributes["Non_Manu_Rule_Waiver_Supported"].Value : null,
                        ["Rec_Acq_Strategy"] = y.LineItemSerializableAttributes.ContainsKey("Rec_Acq_Strategy") ? y.LineItemSerializableAttributes["Rec_Acq_Strategy"].Value : null,
                        ["Manu_Pkging"] = y.LineItemSerializableAttributes.ContainsKey("Manu_Pkging") ? y.LineItemSerializableAttributes["Manu_Pkging"].Value : null,
                        ["Units_per_pkg"] = y.LineItemSerializableAttributes.ContainsKey("Units_per_pkg") ? y.LineItemSerializableAttributes["Units_per_pkg"].Value : null,
                        ["Pkg_Price"] = y.LineItemSerializableAttributes.ContainsKey("Pkg_Price") ? y.LineItemSerializableAttributes["Pkg_Price"].Value : null,
                        ["Avg_Unit_Price"] = y.LineItemSerializableAttributes.ContainsKey("Avg_Unit_Price") ? y.LineItemSerializableAttributes["Avg_Unit_Price"].Value : null,
                        ["Hist_ANNUAL_usage_per_pgk"] = y.LineItemSerializableAttributes.ContainsKey("Hist_ANNUAL_usage_per_pgk") ? y.LineItemSerializableAttributes["Hist_ANNUAL_usage_per_pgk"].Value : null,
                        ["Hist_ANNUAL_usage_per_unit"] = y.LineItemSerializableAttributes.ContainsKey("Hist_ANNUAL_usage_per_unit") ? y.LineItemSerializableAttributes["Hist_ANNUAL_usage_per_unit"].Value : null,
                        ["Base_Year_Estimate"] = y.LineItemSerializableAttributes.ContainsKey("Base_Year_Estimate") ? y.LineItemSerializableAttributes["Base_Year_Estimate"].Value : null,
                        ["IGCE_5_yr_total"] = y.LineItemSerializableAttributes.ContainsKey("IGCE_5_yr_total") ? y.LineItemSerializableAttributes["IGCE_5_yr_total"].Value : null,
                        ["Lowest_Comm_Price"] = y.LineItemSerializableAttributes.ContainsKey("Lowest_Comm_Price") ? y.LineItemSerializableAttributes["Lowest_Comm_Price"].Value : null,
                        ["Units_per_pkg_for_lowest_Comm_price"] = y.LineItemSerializableAttributes.ContainsKey("Units_per_pkg_for_lowest_Comm_price") ? y.LineItemSerializableAttributes["Units_per_pkg_for_lowest_Comm_price"].Value : null,
                        ["Lowest_Fed_price"] = y.LineItemSerializableAttributes.ContainsKey("Lowest_Fed_price") ? y.LineItemSerializableAttributes["Lowest_Fed_price"].Value : null,
                        ["Units_per_pkg_for_lowest_Federal_price"] = y.LineItemSerializableAttributes.ContainsKey("Units_per_pkg_for_lowest_Federal_price") ? y.LineItemSerializableAttributes["Units_per_pkg_for_lowest_Federal_price"].Value : null,
                        ["Lowest_FSS_price"] = y.LineItemSerializableAttributes.ContainsKey("Lowest_FSS_price") ? y.LineItemSerializableAttributes["Lowest_FSS_price"].Value : null,
                        ["Units_per_pkg_for_lowest_FSS_price"] = y.LineItemSerializableAttributes.ContainsKey("Units_per_pkg_for_lowest_FSS_price") ? y.LineItemSerializableAttributes["Units_per_pkg_for_lowest_FSS_price"].Value : null,
                        ["BEST_Pot_Savings"] = y.LineItemSerializableAttributes.ContainsKey("BEST_Pot_Savings") ? y.LineItemSerializableAttributes["BEST_Pot_Savings"].Value : null,
                    });
                results = query.ToList();
#endif
            }
            var resultsArray = new object[results.Count];
            results.CopyTo(resultsArray, 0);
            var maps = new List<(string col, MapFromIncomingProperty mapping)>();
            var colIndex = 0;
            foreach (var mapping in mappings)
            {
                maps.Add((ExcelSalesQuoteImporterProvider.GenColumnReference(colIndex), mapping));
                colIndex++;
            }
            for (var r = (uint)(RowSkip + HeaderRowCount); r < resultsArray.Length + RowSkip + HeaderRowCount; r++)
            {
                var index = (int)r - RowSkip - HeaderRowCount;
                var result = (Dictionary<string, string>)resultsArray[index];
                for (var c = ColumnSkip; c < mappings.Length + ColumnSkip; c++)
                {
                    var (col, mapping) = maps[c - ColumnSkip];
                    var value = result[mapping.Header[0]];
                    this[col, r] = value;
                }
            }
        }

        internal void LoadDataFromContextShort(int id, MapFromIncomingProperty[] mappings, Func<IClarityEcommerceEntities> getContext)
        {
#if HOLD
            var propertyNames = mappings
                .Select(mapping => mapping.Assignments
                    .Select(assignment => assignment.Entity + "." + assignment.To.Single()
                        + (assignment.To.Single() == "JsonAttributes" ? mapping.Header.Aggregate(string.Empty, (c, n) => c + "." + n) : string.Empty)
                        + (mapping.HeaderOccurrence > 0 ? "." + mapping.HeaderOccurrence : string.Empty)
                        + (assignment.Instance > 0 ? "." + assignment.Instance : string.Empty)
                    ).First()
                ).ToList();
#endif
            IList results;
            using (var context = getContext())
            {
#if HOLD
                var query = context.SalesQuotes.AsNoTracking().FilterByID(id).AsExpandable();
                var tree = LinqRuntimeTypeBuilder.SelectDynamicA(propertyNames);
                var treeRoot = tree.First().Value;
                results = (IList)Activator.CreateInstance(typeof(List<>).MakeGenericType(treeRoot.LevelDynamicType));
                treeRoot.CallExpression = Expression.Call(
                    typeof(Queryable),
                    nameof(Queryable.Select),
                    new[] { treeRoot.LevelSourceType, treeRoot.LevelDynamicType },
                    Expression.Constant(query),
                    treeRoot.SelectExpression);
                var expanded = treeRoot.CallExpression.Expand();
                var queryableResult = (IQueryable)query.Provider.Execute(expanded);
                foreach (var result in queryableResult)
                {
                    Console.WriteLine(result);
                    results.Add(result);
                }
#else
                var query = context.SalesQuotes
                    .AsNoTracking()
                    .FilterByID(id)
                    .Select(x => new
                    {
                        x.CustomKey,
                        x.JsonAttributes,
                        SalesItems = x.SalesItems
                            .Where(y => y.Active)
                            .Select(y => new
                            {
                                y.ID,
                                Master = new
                                {
                                    x.CustomKey,
                                    x.JsonAttributes,
                                },
                                y.CustomKey,
                                y.UnitCorePrice,
                                y.JsonAttributes,
                                Product = new
                                {
                                    y.Product.Name,
                                    y.Product.Description,
                                    y.Product.UnitOfMeasure,
                                    y.Product.BrandName,
                                    y.Product.ManufacturerPartNumber,
                                    y.Product.KitBaseQuantityPriceMultiplier,
                                    y.Product.JsonAttributes,
                                    ProductCategories = y.Product.ProductCategories
                                        .Where(z => z.Active)
                                        .Select(z => z.Slave.Name),
                                    ManufacturerProduct = y.Product.Manufacturers
                                        .Where(z => z.Active)
                                        .Select(z => new
                                        {
                                            z.JsonAttributes,
                                            z.Master.Name,
                                        })
                                        .FirstOrDefault(),
                                },
                            }),
                    })
                    .SelectMany(x => x.SalesItems)
                    .Select(y => new
                    {
                        MasterCustomKey = y.Master.CustomKey,
                        y.JsonAttributes,
                        y.CustomKey,
                        y.Product.Name,
                        ProductJsonAttributes = y.Product.JsonAttributes,
                        ManufacturerName = y.Product.ManufacturerProduct.Name,
                    })
                    .ToList()
                    .Select(y => new
                    {
                        y.MasterCustomKey,
                        y.CustomKey,
                        y.Name,
                        LineItemSerializableAttributes = y.JsonAttributes.DeserializeAttributesDictionary(),
                        ProductSerializableAttributes = y.ProductJsonAttributes.DeserializeAttributesDictionary(),
                        y.ManufacturerName,
                    })
                    .Select(y => new Dictionary<string, string>
                    {
                        ["VOA_Tracker_ID"] = y.MasterCustomKey,
                        ["PMO_Row_Num"] = y.CustomKey,
                        ////["PMO_Product_Name"] = y.Name,
                        ////["PMO_Manufacturer"] = y.ManufacturerName,
                        ////["PMO_Salient_Char"] = y.ProductSerializableAttributes.ContainsKey("PMO_Salient_Char") ? y.ProductSerializableAttributes["PMO_Salient_Char"].Value : null,
                        ////["PMO_Base_Year_Est"] = y.ProductSerializableAttributes.ContainsKey("PMO_Base_Year_Est") ? y.ProductSerializableAttributes["PMO_Base_Year_Est"].Value : null,
                        ["PMO_Product_Name"] = y.LineItemSerializableAttributes.ContainsKey("PMO_Product_Name") ? y.LineItemSerializableAttributes["PMO_Product_Name"].Value : null,
                        ["PMO_Manufacturer"] = y.LineItemSerializableAttributes.ContainsKey("PMO_Manufacturer") ? y.LineItemSerializableAttributes["PMO_Manufacturer"].Value : null,
                        ["PMO_Salient_Char"] = y.LineItemSerializableAttributes.ContainsKey("PMO_Salient_Char") ? y.LineItemSerializableAttributes["PMO_Salient_Char"].Value : null,
                        ["PMO_Base_Year_Est"] = y.LineItemSerializableAttributes.ContainsKey("PMO_Base_Year_Est") ? y.LineItemSerializableAttributes["PMO_Base_Year_Est"].Value : null,
                    });
                results = query.ToList();
#endif
            }
            var resultsArray = new object[results.Count];
            results.CopyTo(resultsArray, 0);
            var maps = new List<(string col, MapFromIncomingProperty mapping)>();
            var colIndex = 0;
            foreach (var mapping in mappings)
            {
                maps.Add((ExcelSalesQuoteImporterProvider.GenColumnReference(colIndex), mapping));
                colIndex++;
            }
            for (var r = (uint)(RowSkip + HeaderRowCount); r < resultsArray.Length + RowSkip + HeaderRowCount; r++)
            {
                var index = (int)r - RowSkip - HeaderRowCount;
                var result = (Dictionary<string, string>)resultsArray[index];
                for (var c = ColumnSkip; c < mappings.Length + ColumnSkip; c++)
                {
                    var (col, mapping) = maps[c - ColumnSkip];
                    var value = result[mapping.Header[0]];
                    this[col, r] = value;
                }
            }
        }
    }
}
