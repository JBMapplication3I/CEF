// <copyright file="ExcelSalesQuoteImporterProviderTests.cs" company="clarity-ventures.com">
// Copyright (c) 2018-2023 clarity-ventures.com. All rights reserved.
// </copyright>
// <summary>Implements the excel sales quote importer provider tests class</summary>
// ReSharper disable StringLiteralTypo
// ReSharper disable CommentTypo
namespace Clarity.Ecommerce.Providers.SalesQuoteImporter.Excel.Testing
{
    using System;
    using System.Linq;
    using System.Threading.Tasks;
    using Xunit;

    [Trait("Category", "Providers.SalesQuoteImporter.Excel")]
    public class ExcelSalesQuoteImporterProviderTests
    {
        [Fact(Skip = "Validated")]
        public async Task Verify_Import_SalesQuote_From_Excel_WithAValidFileInTheProperFormat_Works()
        {
            // Arrange
            var provider = new ExcelSalesQuoteImporterProvider();
            ////var workflows = new WorkflowsController();
            // Act
            var result = await provider.ImportFileAsSalesQuoteAsync(null, "sample.xlsx").ConfigureAwait(false);
            // Assert
            Asserts.Success(result);
            // Check the core Entity
            Assert.True(result.Result.Active);
            Assert.Equal("VA-17-0005048", result.Result.CustomKey);
            Assert.True(result.Result.SerializableAttributes.ContainsKey("PMO_AllOrNone_OneOrMore"));
            Assert.Equal("One or more", result.Result.SerializableAttributes["PMO_AllOrNone_OneOrMore"].Value);
            Assert.True(result.Result.SerializableAttributes.ContainsKey("PMO_Contact"));
            Assert.Equal("McMillian, Timothy", result.Result.SerializableAttributes["PMO_Contact"].Value.Trim());
            // Check the Array
            Assert.NotEmpty(result.Result.SalesItems);
            Assert.Equal(345, result.Result.SalesItems.Count);
            /* The following is the contents of the first record being uploaded
            VOA_Tracker_ID							"VA-17-0005048"
            PMO_Row_Num								"10001"
            Parent_VOA_Tracker_ID					"VA-17-0005048"
            PMO_AllOrNone_OneOrMore					"One or more"
            PMO_Contact								"McMillian, Timothy"
            PMO_SupplyLine_Level1					"OPERATING ROOM"
            PMO_SupplyLine_Level2					"INSTRUMENTS SURGICAL"
            PMO_SupplyLine_Level3					"INSTRUMENTS SURGICAL: UROLOGY MISCELLANEOUS"
            PMO_SupplyLine_Level4					"SYSTEM PUMP SINGLE ACTION"
            PMO_UNSPSC								"42.29.49.12"
            PMO_MedPDB_Key							"2580074"
            PMO_NIF_Num								"137165"
            PMO_NIF_Long_Desc						"PUMPING SYSTEM,UROLOGIC,SINGLE ACTION,10 CC VACUUM SYRINGE,DUAL-CHECK VALVE,T-FITTING,CONTINUOUS FLOW,ADJUSTABLE LENGTH TUBING SYSTEM,ROLLER CLAMP,STERILE,DISPOSABLE"
            PMO_Product_Name						"SYSTEM PUMP SINGLE ACTION 10ML VACUUM SYRINGE DUAL CHECK VALVE CONTINUOUS FLOW ADJUSTABLE LENGTH TUBING"
            PMO_Manufacturer						"BOSTON SCIENTIFIC"
            PMO_Manu_Prod_Brand_Name				"SAPS"
            PMO_OEM_Part_Num						"M0067201011"
            PMO_URL_to_OEM							""
            PMO_Image_Filename						"NIF-137165.jpg"
            PMO_Compatibility						"n/a"
            PMO_BNOE								"BNOE"
            PMO_Unique_Char							"n/a"
            PMO_Salient_Char						"SYSTEM PUMP, URETEROSCOPY AND LASER LITHOTRIPSY, SINGLE ACTION, CONTINUOUS FLOW, DUAL CHECK ONE-WAY VALVE W/ AUTOMATIC REFILL, LATEX-FREE, ADJUSTABLE LENGTH TUBING, 10CC VACUUM SYRINGE"
            PMO_Hist_Ann_Qty_Num_Pkgs				"109"
            PMO_Num_Items_per_Pkg					"BX OF 5EA"
            PMO_Base_Year_Est						"$38,533.00"
            PMO_FSS_or_Non_FSS						"n/a"
            PMO_FSS_Link							""
            PMO_FSS_Contract_Holder					"n/a"
            PMO_FSS_Contract_Num					"n/a"
            PMO_FSS_Contract_End_Date				"n/a"
            VOA_Tracker_ID							"VA-17-0005048"
            Unicor_AbilityOne						"Not Unicor or Ability One"
            FSS_Category							"65 IIA"
            FSS_SIN									"A-87"
            Product_Supply_Code						"6515"
            Manu_NAICS_Code							"339112"
            FDA_CLASS								"I"
            Industry_Life_Cycle						"Maturity"
            Multiple_Sources_Avail					"Yes"
            Possible_Sources						"BOSTON SCIENTIFIC"
            Non_Manu_Rule_Waiver_Supported			"n/a"
            Rec_Acq_Strategy						"Set-Aside - SDVOSB"
            Manu_Pkging								"EACH"
            Units_per_pkg							"1"
            Pkg_Price								"74.47"
            Avg_Unit_Price							"74.47"
            Hist_ANNUAL_usage_per_pgk				"480"
            Hist_ANNUAL_usage_per_unit				"480"
            Base_Year_Estimate						"35745.6"
            IGCE_5_yr_total							"187890.6166"
            Lowest_Comm_Price						"42.71"
            Units_per_pkg_for_lowest_Comm_price		"1"
            Lowest_Fed_price						"52.38"
            Units_per_pkg_for_lowest_Federal_price	"1"
            Lowest_FSS_price						"0"
            Units_per_pkg_for_lowest_FSS_price		"1"
            BEST_Pot_Savings						"0"
            */
            {
                // Check the first item in the array
                var salesQuoteItem = result.Result.SalesItems.OrderBy(x => x.CustomKey).First();
                Assert.NotNull(salesQuoteItem);
                Assert.True(salesQuoteItem.Active);
                Assert.Equal("10001", salesQuoteItem.CustomKey);
                Assert.True(salesQuoteItem.SerializableAttributes.ContainsKey("PMO_Row_Num"));
                Assert.Equal("10001", salesQuoteItem.SerializableAttributes["PMO_Row_Num"].Value);
                Assert.True(salesQuoteItem.SerializableAttributes.ContainsKey("Parent_VOA_Tracker_ID"));
                Assert.Equal("VA-17-0005048", salesQuoteItem.SerializableAttributes["Parent_VOA_Tracker_ID"].Value);
                Assert.True(salesQuoteItem.SerializableAttributes.ContainsKey("PMO_NIF_Num"));
                Assert.Equal("137165", salesQuoteItem.SerializableAttributes["PMO_NIF_Num"].Value);
                Assert.True(salesQuoteItem.SerializableAttributes.ContainsKey("PMO_BNOE"));
                Assert.Equal("BNOE", salesQuoteItem.SerializableAttributes["PMO_BNOE"].Value);
                Assert.False(salesQuoteItem.SerializableAttributes.ContainsKey("PMO_FSS_or_Non_FSS"));
                Assert.False(salesQuoteItem.SerializableAttributes.ContainsKey("PMO_FSS_Link"));
                Assert.False(salesQuoteItem.SerializableAttributes.ContainsKey("PMO_FSS_Contract_Holder"));
                Assert.False(salesQuoteItem.SerializableAttributes.ContainsKey("PMO_FSS_Contract_Num"));
                Assert.False(salesQuoteItem.SerializableAttributes.ContainsKey("PMO_FSS_Contract_End_Date"));
                Assert.True(salesQuoteItem.SerializableAttributes.ContainsKey("Unicor_AbilityOne"));
                Assert.Equal("Not Unicor or Ability One", salesQuoteItem.SerializableAttributes["Unicor_AbilityOne"].Value);
                Assert.True(salesQuoteItem.SerializableAttributes.ContainsKey("FSS_Category"));
                Assert.Equal("65 IIA", salesQuoteItem.SerializableAttributes["FSS_Category"].Value);
                Assert.True(salesQuoteItem.SerializableAttributes.ContainsKey("FSS_SIN"));
                Assert.Equal("A-87", salesQuoteItem.SerializableAttributes["FSS_SIN"].Value);
                Assert.True(salesQuoteItem.SerializableAttributes.ContainsKey("Product_Supply_Code"));
                Assert.Equal("6515", salesQuoteItem.SerializableAttributes["Product_Supply_Code"].Value);
                Assert.True(salesQuoteItem.SerializableAttributes.ContainsKey("Manu_NAICS_Code"));
                Assert.Equal("339112", salesQuoteItem.SerializableAttributes["Manu_NAICS_Code"].Value);
                Assert.True(salesQuoteItem.SerializableAttributes.ContainsKey("FDA_CLASS"));
                Assert.Equal("I", salesQuoteItem.SerializableAttributes["FDA_CLASS"].Value);
                Assert.True(salesQuoteItem.SerializableAttributes.ContainsKey("Industry_Life_Cycle"));
                Assert.Equal("Maturity", salesQuoteItem.SerializableAttributes["Industry_Life_Cycle"].Value);
                Assert.True(salesQuoteItem.SerializableAttributes.ContainsKey("Multiple_Sources_Avail"));
                Assert.Equal("Yes", salesQuoteItem.SerializableAttributes["Multiple_Sources_Avail"].Value);
                Assert.True(salesQuoteItem.SerializableAttributes.ContainsKey("Possible_Sources"));
                Assert.Equal("BOSTON SCIENTIFIC", salesQuoteItem.SerializableAttributes["Possible_Sources"].Value);
                Assert.False(salesQuoteItem.SerializableAttributes.ContainsKey("Non_Manu_Rule_Waiver_Supported"));
                Assert.True(salesQuoteItem.SerializableAttributes.ContainsKey("Rec_Acq_Strategy"));
                Assert.Equal("Set-Aside - SDVOSB", salesQuoteItem.SerializableAttributes["Rec_Acq_Strategy"].Value);
                Assert.True(salesQuoteItem.SerializableAttributes.ContainsKey("Pkg_Price"));
                Assert.Equal("74.47", salesQuoteItem.SerializableAttributes["Pkg_Price"].Value);
                Assert.True(salesQuoteItem.SerializableAttributes.ContainsKey("Avg_Unit_Price"));
                Assert.Equal("74.47", salesQuoteItem.SerializableAttributes["Avg_Unit_Price"].Value);

                // Check the product on the first item in the array
                ////var product = salesQuoteItem.Product;
                ////Assert.NotNull(product);
                ////Assert.True(product.Active);
                ////Assert.True(product.SerializableAttributes.ContainsKey("PMO_UNSPSC"));
                ////Assert.Equal("42.29.49.12", product.SerializableAttributes["PMO_UNSPSC"].Value);
                ////Assert.True(product.SerializableAttributes.ContainsKey("PMO_MedPDB_Key"));
                ////Assert.Equal("2580074", product.SerializableAttributes["PMO_MedPDB_Key"].Value);
                ////const string PmoNifLongDesc = "PUMPING SYSTEM,UROLOGIC,SINGLE ACTION,10 CC VACUUM SYRINGE,DUAL-CHECK "
                ////    + "VALVE,T-FITTING,CONTINUOUS FLOW,ADJUSTABLE LENGTH TUBING SYSTEM,ROLLER CLAMP,STERILE,DISPOSABLE";
                ////Assert.Equal(PmoNifLongDesc, product.Description);
                ////Assert.Equal(PmoNifLongDesc, product.ShortDescription);
                ////Assert.True(product.SerializableAttributes.ContainsKey("PMO_NIF_Long_Desc"));
                ////Assert.Equal(PmoNifLongDesc, product.SerializableAttributes["PMO_NIF_Long_Desc"].Value);
                ////const string PmoProductName = "SYSTEM PUMP SINGLE ACTION 10ML VACUUM SYRINGE DUAL CHECK VALVE CONTINUOUS "
                ////    + "FLOW ADJUSTABLE LENGTH TUBING";
                ////Assert.Equal(PmoProductName, product.Name);
                ////Assert.True(product.SerializableAttributes.ContainsKey("PMO_Product_Name"));
                ////Assert.Equal(PmoProductName, product.SerializableAttributes["PMO_Product_Name"].Value);
                ////const string PmoManufacturerProdBrandName = "SAPS";
                ////Assert.Equal(PmoManufacturerProdBrandName, product.BrandName);
                ////Assert.True(product.SerializableAttributes.ContainsKey("PMO_Manu_Prod_Brand_Name"));
                ////Assert.Equal(PmoManufacturerProdBrandName, product.SerializableAttributes["PMO_Manu_Prod_Brand_Name"].Value);
                ////const string PmoOemPartNum = "M0067201011";
                ////Assert.Equal(PmoOemPartNum, product.ManufacturerPartNumber);
                ////Assert.True(product.SerializableAttributes.ContainsKey("PMO_OEM_Part_Num"));
                ////Assert.Equal(PmoOemPartNum, product.SerializableAttributes["PMO_OEM_Part_Num"].Value);
                ////Assert.False(product.SerializableAttributes.ContainsKey("PMO_URL_to_OEM"));
                ////// TODO: Assert Images were loaded
                ////Assert.True(product.SerializableAttributes.ContainsKey("PMO_Image_Filename"));
                ////Assert.Equal("NIF-137165.jpg", product.SerializableAttributes["PMO_Image_Filename"].Value);
                ////Assert.False(product.SerializableAttributes.ContainsKey("PMO_Compatibility"));
                ////Assert.False(product.SerializableAttributes.ContainsKey("PMO_Unique_Char"));
                ////Assert.True(product.SerializableAttributes.ContainsKey("PMO_Salient_Char"));
                ////Assert.Equal(
                ////    "SYSTEM PUMP, URETEROSCOPY AND LASER LITHOTRIPSY, SINGLE ACTION, CONTINUOUS FLOW, DUAL CHECK ONE-WAY "
                ////    + "VALVE W/ AUTOMATIC REFILL, LATEX-FREE, ADJUSTABLE LENGTH TUBING, 10CC VACUUM SYRINGE\n",
                ////    product.SerializableAttributes["PMO_Salient_Char"].Value);
                ////Assert.True(product.SerializableAttributes.ContainsKey("PMO_Hist_Ann_Qty_Num_Pkgs"));
                ////Assert.Equal("109", product.SerializableAttributes["PMO_Hist_Ann_Qty_Num_Pkgs"].Value);
                ////const string PmoNumItemsPerPkg = "BX OF 5EA";
                ////Assert.Equal(PmoNumItemsPerPkg, product.UnitOfMeasure);
                ////Assert.True(product.SerializableAttributes.ContainsKey("PMO_Num_Items_per_Pkg"));
                ////Assert.Equal(PmoNumItemsPerPkg, product.SerializableAttributes["PMO_Num_Items_per_Pkg"].Value);
                ////Assert.True(product.SerializableAttributes.ContainsKey("PMO_Base_Year_Est"));
                ////Assert.Equal("38533", product.SerializableAttributes["PMO_Base_Year_Est"].Value);
                ////const decimal UnitsPerPkg = 1;
                ////Assert.Equal(UnitsPerPkg, product.KitBaseQuantityPriceMultiplier);
                ////Assert.True(product.SerializableAttributes.ContainsKey("Units_per_pkg"));
                ////Assert.Equal(
                ////    UnitsPerPkg.ToString(CultureInfo.InvariantCulture),
                ////    product.SerializableAttributes["Units_per_pkg"].Value);
                ////Assert.True(product.SerializableAttributes.ContainsKey("Hist_ANNUAL_usage_per_pgk"));
                ////Assert.Equal("480", product.SerializableAttributes["Hist_ANNUAL_usage_per_pgk"].Value);
                ////Assert.True(product.SerializableAttributes.ContainsKey("Hist_ANNUAL_usage_per_unit"));
                ////Assert.Equal("480", product.SerializableAttributes["Hist_ANNUAL_usage_per_unit"].Value);
                ////Assert.True(product.SerializableAttributes.ContainsKey("Base_Year_Estimate"));
                ////Assert.Equal("35745.599999999999", product.SerializableAttributes["Base_Year_Estimate"].Value);
                ////Assert.True(product.SerializableAttributes.ContainsKey("IGCE_5_yr_total"));
                ////Assert.Equal("187890.61658812503", product.SerializableAttributes["IGCE_5_yr_total"].Value);
                ////Assert.True(product.SerializableAttributes.ContainsKey("Lowest_Comm_Price"));
                ////Assert.Equal("42.71", product.SerializableAttributes["Lowest_Comm_Price"].Value);
                ////Assert.True(product.SerializableAttributes.ContainsKey("Units_per_pkg_for_lowest_Comm_price"));
                ////Assert.Equal("1", product.SerializableAttributes["Units_per_pkg_for_lowest_Comm_price"].Value);
                ////Assert.True(product.SerializableAttributes.ContainsKey("Lowest_Fed_price"));
                ////Assert.Equal("52.38", product.SerializableAttributes["Lowest_Fed_price"].Value);
                ////Assert.True(product.SerializableAttributes.ContainsKey("Units_per_pkg_for_lowest_Federal_price"));
                ////Assert.Equal("1", product.SerializableAttributes["Units_per_pkg_for_lowest_Federal_price"].Value);
                ////Assert.True(product.SerializableAttributes.ContainsKey("Lowest_FSS_price"));
                ////Assert.Equal("0", product.SerializableAttributes["Lowest_FSS_price"].Value);
                ////Assert.True(product.SerializableAttributes.ContainsKey("Units_per_pkg_for_lowest_FSS_price"));
                ////Assert.Equal("1", product.SerializableAttributes["Units_per_pkg_for_lowest_FSS_price"].Value);
                ////Assert.True(product.SerializableAttributes.ContainsKey("BEST_Pot_Savings"));
                ////Assert.Equal("0", product.SerializableAttributes["BEST_Pot_Savings"].Value);
                ////
                ////// The ManufacturerProducts collection on the Product
                ////var results = workflows.ManufacturerProducts.Search(
                ////    new ManufacturerProductSearchModel { ProductID = product.ID },
                ////    false,
                ////    null)
                ////    .results;
                ////product.ManufacturerProducts = results;
                ////Assert.NotNull(product.ManufacturerProducts);
                ////Assert.NotEmpty(product.ManufacturerProducts);
                ////var manufacturerProduct = product.ManufacturerProducts.First();
                ////Assert.NotNull(manufacturerProduct);
                ////Assert.True(manufacturerProduct.Active);
                ////var manufacturer = workflows.Manufacturers.GetByName("BOSTON SCIENTIFIC", null);
                ////Assert.NotNull(manufacturer);
                ////Assert.True(manufacturer.Active);
                ////Assert.Equal("BOSTON SCIENTIFIC", manufacturer.Name);
                ////
                ////// The ProductCategoriesCollection on the Product
                ////var results2 = workflows.ProductCategories.Search(
                ////    new ProductCategorySearchModel { ProductID = product.ID },
                ////    false,
                ////    null)
                ////    .results;
                ////product.ProductCategories = results2;
                ////Assert.NotNull(product.ProductCategories);
                ////Assert.NotEmpty(product.ProductCategories);
                ////var productCategory = product.ProductCategories.First();
                ////Assert.NotNull(productCategory);
                ////Assert.True(productCategory.Active);
                ////var category = workflows.Categories.GetByName("SYSTEM PUMP SINGLE ACTION", null);
                ////Assert.NotNull(category);
                ////Assert.True(category.Active);
                ////Assert.Equal("SYSTEM PUMP SINGLE ACTION", category.Name);

                // Still Broken
                const decimal PkgPrice = 74.47m;
                Assert.Equal(PkgPrice, salesQuoteItem.UnitCorePrice);
                /* These fail
                Assert.Equal(PkgPrice, product.PriceBase);
                Assert.True(product.SerializableAttributes.ContainsKey("Pkg_Price"));
                Assert.Equal(
                    PkgPrice.ToString(CultureInfo.InvariantCulture),
                    product.SerializableAttributes["Pkg_Price"].Value);
                */
            }
            /* The following is the contents of the second record being uploaded
            VOA_Tracker_ID							"VA-17-0005048"
            PMO_Row_Num								"10002"
            Parent_VOA_Tracker_ID					"VA-17-0005048"
            PMO_AllOrNone_OneOrMore					"One or more"
            PMO_Contact								"McMillian, Timothy "
            PMO_SupplyLine_Level1					"OPERATING ROOM"
            PMO_SupplyLine_Level2					"INSTRUMENTS SURGICAL"
            PMO_SupplyLine_Level3					"INSTRUMENTS SURGICAL: UROLOGY MISCELLANEOUS"
            PMO_SupplyLine_Level4					"SYSTEM PUMP SINGLE ACTION"
            PMO_UNSPSC								"42.29.49.12"
            PMO_MedPDB_Key							"2580070"
            PMO_NIF_Num								"39236"
            PMO_NIF_Long_Desc						"PUMPING SYSTEM,UROLOGIC,SYSTEM PUMP,SINGLE ACTION,10 CC VACUUM SYRINGE,ONE-WAY VALVE,T-FITTING,STERILE,DISPOSABLE,75 CM CONNECTION"
            PMO_Product_Name						"SYSTEM PUMP SINGLE ACTION 10ML VACUUM SYRINGE ONE-WAY VALVE T FITTING 75CM CONNECTION TUBEX2"
            PMO_Manufacturer						"BOSTON SCIENTIFIC"
            PMO_Manu_Prod_Brand_Name				"SAPS"
            PMO_OEM_Part_Num						"M0067201001"
            PMO_URL_to_OEM							""
            PMO_Image_Filename						"NIF-39236.jpg"
            PMO_Compatibility						"n/a"
            PMO_BNOE								"BNOE"
            PMO_Unique_Char							"n/a"
            PMO_Salient_Char						"SYSTEM PUMP, URETEROSCOPY AND LASER LITHOTRIPSY, SINGLE ACTION, ONE-WAY VALVE W/ AUTOMATIC REFILL, LATEX-FREE, T FITTING 75CM CONNECTION TUBEX2, 10CC VACUUM SYRINGE\n"
            PMO_Hist_Ann_Qty_Num_Pkgs				"501"
            PMO_Num_Items_per_Pkg					"BX OF 5EA"
            PMO_Base_Year_Est						"$182,034.00"
            PMO_FSS_or_Non_FSS						"n/a"
            PMO_FSS_Link							""
            PMO_FSS_Contract_Holder					"n/a"
            PMO_FSS_Contract_Num					"n/a"
            PMO_FSS_Contract_End_Date				"n/a"
            VOA_Tracker_ID							"VA-17-0005048"
            Unicor_AbilityOne						"Not Unicor or Ability One"
            FSS_Category							"65 IIA"
            FSS_SIN									"A-87"
            Product_Supply_Code						"6515"
            Manu_NAICS_Code							"339112"
            FDA_CLASS								"I"
            Industry_Life_Cycle						"Maturity"
            Multiple_Sources_Avail					"Yes"
            Possible_Sources						"BOSTON SCIENTIFIC"
            Non_Manu_Rule_Waiver_Supported			"n/a"
            Rec_Acq_Strategy						"Set-Aside - SDVOSB"
            Manu_Pkging								"EACH"
            Units_per_pkg							"1"
            Pkg_Price								"75.14"
            Avg_Unit_Price							"75.14"
            Hist_ANNUAL_usage_per_pgk				"2460"
            Hist_ANNUAL_usage_per_unit				"2460"
            Base_Year_Estimate						"184844.4"
            IGCE_5_yr_total							"971602.8907"
            Lowest_Comm_Price						"32.14"
            Units_per_pkg_for_lowest_Comm_price		"1"
            Lowest_Fed_price						"50.44"
            Units_per_pkg_for_lowest_Federal_price	"1"
            Lowest_FSS_price						"0"
            Units_per_pkg_for_lowest_FSS_price		"1"
            BEST_Pot_Savings						"0"
            */
            {
                // Check the first item in the array
                var salesQuoteItem = result.Result.SalesItems.OrderBy(x => x.CustomKey).Skip(1).First();
                Assert.NotNull(salesQuoteItem);
                Assert.True(salesQuoteItem.Active);
                Assert.Equal("10002", salesQuoteItem.CustomKey);
                Assert.True(salesQuoteItem.SerializableAttributes.ContainsKey("PMO_Row_Num"));
                Assert.Equal("10002", salesQuoteItem.SerializableAttributes["PMO_Row_Num"].Value);
                Assert.True(salesQuoteItem.SerializableAttributes.ContainsKey("Parent_VOA_Tracker_ID"));
                Assert.Equal("VA-17-0005048", salesQuoteItem.SerializableAttributes["Parent_VOA_Tracker_ID"].Value);
                Assert.True(salesQuoteItem.SerializableAttributes.ContainsKey("PMO_NIF_Num"));
                Assert.Equal("39236", salesQuoteItem.SerializableAttributes["PMO_NIF_Num"].Value);
                Assert.True(salesQuoteItem.SerializableAttributes.ContainsKey("PMO_BNOE"));
                Assert.Equal("BNOE", salesQuoteItem.SerializableAttributes["PMO_BNOE"].Value);
                Assert.False(salesQuoteItem.SerializableAttributes.ContainsKey("PMO_FSS_or_Non_FSS"));
                Assert.False(salesQuoteItem.SerializableAttributes.ContainsKey("PMO_FSS_Link"));
                Assert.False(salesQuoteItem.SerializableAttributes.ContainsKey("PMO_FSS_Contract_Holder"));
                Assert.False(salesQuoteItem.SerializableAttributes.ContainsKey("PMO_FSS_Contract_Num"));
                Assert.False(salesQuoteItem.SerializableAttributes.ContainsKey("PMO_FSS_Contract_End_Date"));
                Assert.True(salesQuoteItem.SerializableAttributes.ContainsKey("Unicor_AbilityOne"));
                Assert.Equal("Not Unicor or Ability One", salesQuoteItem.SerializableAttributes["Unicor_AbilityOne"].Value);
                Assert.True(salesQuoteItem.SerializableAttributes.ContainsKey("FSS_Category"));
                Assert.Equal("65 IIA", salesQuoteItem.SerializableAttributes["FSS_Category"].Value);
                Assert.True(salesQuoteItem.SerializableAttributes.ContainsKey("FSS_SIN"));
                Assert.Equal("A-87", salesQuoteItem.SerializableAttributes["FSS_SIN"].Value);
                Assert.True(salesQuoteItem.SerializableAttributes.ContainsKey("Product_Supply_Code"));
                Assert.Equal("6515", salesQuoteItem.SerializableAttributes["Product_Supply_Code"].Value);
                Assert.True(salesQuoteItem.SerializableAttributes.ContainsKey("Manu_NAICS_Code"));
                Assert.Equal("339112", salesQuoteItem.SerializableAttributes["Manu_NAICS_Code"].Value);
                Assert.True(salesQuoteItem.SerializableAttributes.ContainsKey("FDA_CLASS"));
                Assert.Equal("I", salesQuoteItem.SerializableAttributes["FDA_CLASS"].Value);
                Assert.True(salesQuoteItem.SerializableAttributes.ContainsKey("Industry_Life_Cycle"));
                Assert.Equal("Maturity", salesQuoteItem.SerializableAttributes["Industry_Life_Cycle"].Value);
                Assert.True(salesQuoteItem.SerializableAttributes.ContainsKey("Multiple_Sources_Avail"));
                Assert.Equal("Yes", salesQuoteItem.SerializableAttributes["Multiple_Sources_Avail"].Value);
                Assert.True(salesQuoteItem.SerializableAttributes.ContainsKey("Possible_Sources"));
                Assert.Equal("BOSTON SCIENTIFIC", salesQuoteItem.SerializableAttributes["Possible_Sources"].Value);
                Assert.False(salesQuoteItem.SerializableAttributes.ContainsKey("Non_Manu_Rule_Waiver_Supported"));
                Assert.True(salesQuoteItem.SerializableAttributes.ContainsKey("Rec_Acq_Strategy"));
                Assert.Equal("Set-Aside - SDVOSB", salesQuoteItem.SerializableAttributes["Rec_Acq_Strategy"].Value);
                Assert.True(salesQuoteItem.SerializableAttributes.ContainsKey("Pkg_Price"));
                Assert.Equal("75.14", salesQuoteItem.SerializableAttributes["Pkg_Price"].Value);
                Assert.True(salesQuoteItem.SerializableAttributes.ContainsKey("Avg_Unit_Price"));
                Assert.Equal("75.14", salesQuoteItem.SerializableAttributes["Avg_Unit_Price"].Value);

                ////// Check the product on the first item in the array
                ////var product = salesQuoteItem.Product;
                ////Assert.NotNull(product);
                ////Assert.True(product.Active);
                ////Assert.True(product.SerializableAttributes.ContainsKey("PMO_UNSPSC"));
                ////Assert.Equal("42.29.49.12", product.SerializableAttributes["PMO_UNSPSC"].Value);
                ////Assert.True(product.SerializableAttributes.ContainsKey("PMO_MedPDB_Key"));
                ////Assert.Equal("2580070", product.SerializableAttributes["PMO_MedPDB_Key"].Value);
                ////const string PmoNifLongDesc = "PUMPING SYSTEM,UROLOGIC,SYSTEM PUMP,SINGLE ACTION,10 CC VACUUM SYRINGE,"
                ////    + "ONE-WAY VALVE,T-FITTING,STERILE,DISPOSABLE,75 CM CONNECTION";
                ////Assert.Equal(PmoNifLongDesc, product.Description);
                ////Assert.Equal(PmoNifLongDesc, product.ShortDescription);
                ////Assert.True(product.SerializableAttributes.ContainsKey("PMO_NIF_Long_Desc"));
                ////Assert.Equal(PmoNifLongDesc, product.SerializableAttributes["PMO_NIF_Long_Desc"].Value);
                ////const string PmoProductName = "SYSTEM PUMP SINGLE ACTION 10ML VACUUM SYRINGE ONE-WAY VALVE T FITTING "
                ////    + "75CM CONNECTION TUBEX2";
                ////Assert.Equal(PmoProductName, product.Name);
                ////Assert.True(product.SerializableAttributes.ContainsKey("PMO_Product_Name"));
                ////Assert.Equal(PmoProductName, product.SerializableAttributes["PMO_Product_Name"].Value);
                ////const string PmoManufacturerProdBrandName = "SAPS";
                ////Assert.Equal(PmoManufacturerProdBrandName, product.BrandName);
                ////Assert.True(product.SerializableAttributes.ContainsKey("PMO_Manu_Prod_Brand_Name"));
                ////Assert.Equal(PmoManufacturerProdBrandName, product.SerializableAttributes["PMO_Manu_Prod_Brand_Name"].Value);
                ////const string PmoOemPartNum = "M0067201001";
                ////Assert.Equal(PmoOemPartNum, product.ManufacturerPartNumber);
                ////Assert.True(product.SerializableAttributes.ContainsKey("PMO_OEM_Part_Num"));
                ////Assert.Equal(PmoOemPartNum, product.SerializableAttributes["PMO_OEM_Part_Num"].Value);
                ////Assert.False(product.SerializableAttributes.ContainsKey("PMO_URL_to_OEM"));
                ////// TODO: Assert Images were loaded
                ////Assert.True(product.SerializableAttributes.ContainsKey("PMO_Image_Filename"));
                ////Assert.Equal("NIF-39236.jpg", product.SerializableAttributes["PMO_Image_Filename"].Value);
                ////Assert.False(product.SerializableAttributes.ContainsKey("PMO_Compatibility"));
                ////Assert.False(product.SerializableAttributes.ContainsKey("PMO_Unique_Char"));
                ////Assert.True(product.SerializableAttributes.ContainsKey("PMO_Salient_Char"));
                ////Assert.Equal(
                ////    "SYSTEM PUMP, URETEROSCOPY AND LASER LITHOTRIPSY, SINGLE ACTION, ONE-WAY VALVE W/ AUTOMATIC REFILL,"
                ////    + " LATEX-FREE, T FITTING 75CM CONNECTION TUBEX2, 10CC VACUUM SYRINGE\n",
                ////    product.SerializableAttributes["PMO_Salient_Char"].Value);
                ////Assert.True(product.SerializableAttributes.ContainsKey("PMO_Hist_Ann_Qty_Num_Pkgs"));
                ////Assert.Equal("501", product.SerializableAttributes["PMO_Hist_Ann_Qty_Num_Pkgs"].Value);
                ////const string PmoNumItemsPerPkg = "BX OF 5EA";
                ////Assert.Equal(PmoNumItemsPerPkg, product.UnitOfMeasure);
                ////Assert.True(product.SerializableAttributes.ContainsKey("PMO_Num_Items_per_Pkg"));
                ////Assert.Equal(PmoNumItemsPerPkg, product.SerializableAttributes["PMO_Num_Items_per_Pkg"].Value);
                ////Assert.True(product.SerializableAttributes.ContainsKey("PMO_Base_Year_Est"));
                ////Assert.Equal("182034", product.SerializableAttributes["PMO_Base_Year_Est"].Value);
                ////const decimal UnitsPerPkg = 1;
                ////Assert.Equal(UnitsPerPkg, product.KitBaseQuantityPriceMultiplier);
                ////Assert.True(product.SerializableAttributes.ContainsKey("Units_per_pkg"));
                ////Assert.Equal(
                ////    UnitsPerPkg.ToString(CultureInfo.InvariantCulture),
                ////    product.SerializableAttributes["Units_per_pkg"].Value);
                ////Assert.True(product.SerializableAttributes.ContainsKey("Hist_ANNUAL_usage_per_pgk"));
                ////Assert.Equal("2460", product.SerializableAttributes["Hist_ANNUAL_usage_per_pgk"].Value);
                ////Assert.True(product.SerializableAttributes.ContainsKey("Hist_ANNUAL_usage_per_unit"));
                ////Assert.Equal("2460", product.SerializableAttributes["Hist_ANNUAL_usage_per_unit"].Value);
                ////Assert.True(product.SerializableAttributes.ContainsKey("Base_Year_Estimate"));
                ////Assert.Equal("184844.4", product.SerializableAttributes["Base_Year_Estimate"].Value);
                ////Assert.True(product.SerializableAttributes.ContainsKey("IGCE_5_yr_total"));
                ////Assert.Equal("971602.89067359373", product.SerializableAttributes["IGCE_5_yr_total"].Value);
                ////Assert.True(product.SerializableAttributes.ContainsKey("Lowest_Comm_Price"));
                ////Assert.Equal("32.14", product.SerializableAttributes["Lowest_Comm_Price"].Value);
                ////Assert.True(product.SerializableAttributes.ContainsKey("Units_per_pkg_for_lowest_Comm_price"));
                ////Assert.Equal("1", product.SerializableAttributes["Units_per_pkg_for_lowest_Comm_price"].Value);
                ////Assert.True(product.SerializableAttributes.ContainsKey("Lowest_Fed_price"));
                ////Assert.Equal("50.44", product.SerializableAttributes["Lowest_Fed_price"].Value);
                ////Assert.True(product.SerializableAttributes.ContainsKey("Units_per_pkg_for_lowest_Federal_price"));
                ////Assert.Equal("1", product.SerializableAttributes["Units_per_pkg_for_lowest_Federal_price"].Value);
                ////Assert.True(product.SerializableAttributes.ContainsKey("Lowest_FSS_price"));
                ////Assert.Equal("0", product.SerializableAttributes["Lowest_FSS_price"].Value);
                ////Assert.True(product.SerializableAttributes.ContainsKey("Units_per_pkg_for_lowest_FSS_price"));
                ////Assert.Equal("1", product.SerializableAttributes["Units_per_pkg_for_lowest_FSS_price"].Value);
                ////Assert.True(product.SerializableAttributes.ContainsKey("BEST_Pot_Savings"));
                ////Assert.Equal("0", product.SerializableAttributes["BEST_Pot_Savings"].Value);
                ////
                ////// The ManufacturerProducts collection on the Product
                ////var results = workflows.ManufacturerProducts.Search(
                ////    new ManufacturerProductSearchModel { ProductID = product.ID },
                ////    false,
                ////    null)
                ////    .results;
                ////product.ManufacturerProducts = results;
                ////Assert.NotNull(product.ManufacturerProducts);
                ////Assert.NotEmpty(product.ManufacturerProducts);
                ////var manufacturerProduct = product.ManufacturerProducts.First();
                ////Assert.NotNull(manufacturerProduct);
                ////Assert.True(manufacturerProduct.Active);
                ////var manufacturer = workflows.Manufacturers.GetByName("BOSTON SCIENTIFIC", null);
                ////Assert.NotNull(manufacturer);
                ////Assert.True(manufacturer.Active);
                ////Assert.Equal("BOSTON SCIENTIFIC", manufacturer.Name);
                ////
                ////// The ProductCategoriesCollection on the Product
                ////var results2 = workflows.ProductCategories.Search(
                ////    new ProductCategorySearchModel { ProductID = product.ID },
                ////    false,
                ////    null)
                ////    .results;
                ////product.ProductCategories = results2;
                ////Assert.NotNull(product.ProductCategories);
                ////Assert.NotEmpty(product.ProductCategories);
                ////var productCategory = product.ProductCategories.First();
                ////Assert.NotNull(productCategory);
                ////Assert.True(productCategory.Active);
                ////var category = workflows.Categories.GetByName("SYSTEM PUMP SINGLE ACTION", null);
                ////Assert.NotNull(category);
                ////Assert.True(category.Active);
                ////Assert.Equal("SYSTEM PUMP SINGLE ACTION", category.Name);
                ////
                ////// Still Broken
                ////const decimal PkgPrice = 75.14m;
                ////Assert.Equal(PkgPrice, salesQuoteItem.UnitCorePrice);
                /* These fail
                Assert.Equal(PkgPrice, product.PriceBase);
                Assert.True(product.SerializableAttributes.ContainsKey("Pkg_Price"));
                Assert.Equal(
                    PkgPrice.ToString(CultureInfo.InvariantCulture),
                    product.SerializableAttributes["Pkg_Price"].Value);
                */
            }
        }

        [Fact(Skip = "Validated")]
        public async Task Verify_Import_SalesQuote_From_Excel_With_AFileThatDoesntExist_Returns_AFailingCEFActionResponse()
        {
            // Arrange/Act
            var result = await new ExcelSalesQuoteImporterProvider().ImportFileAsSalesQuoteAsync(null, "doesnt-exist.xlsx").ConfigureAwait(false);
            // Assert
            Assert.False(result.ActionSucceeded);
            Assert.Equal("ERROR! The file 'doesnt-exist.xlsx' does not exist at the expected location", result.Messages[0]);
        }

        [Fact(Skip = "Validated")]
        public async Task Verify_Export_SalesQuote_To_Excel_Works()
        {
            // Arrange
            var provider = new ExcelSalesQuoteImporterProvider();
            // Act
            var result = await provider.ExportSalesQuoteAsFileAsync(null, 17, "xlsx").ConfigureAwait(false);
            // Assert
            Asserts.Success(result);
            /*var header = response.GetHeader("content-disposition");
            Assert.NotNull(header);
            Assert.StartsWith("attachment; filename=SalesQuoteExport-No17-2018-", header);
            Assert.EndsWith(".xlsx", header);*/
        }

        [Fact(Skip = "Validated")]
        public async Task Verify_Export_SalesQuote_To_Excel_WithAnInvalidFileFormat_Returns_AFailingCEFActionResponse()
        {
            // Arrange/Act/Assert
            foreach (var format in new[] { null, "", "\r", "\n", "\r\n", "xls", "pdf" })
            {
                var result = await new ExcelSalesQuoteImporterProvider().ExportSalesQuoteAsFileAsync(null, 1, format).ConfigureAwait(false);
                Assert.False(result.ActionSucceeded);
                Assert.Equal($"ERROR! The excel export cannot export file type {format}", result.Messages[0]);
            }
        }

        [Fact(Skip = "Validated")]
        public async Task Verify_Export_SalesQuote_To_Excel_WithAnInvalidID_Returns_AFailingCEFActionResponse()
        {
            // Arrange/Act/Assert
            foreach (var id in new[] { 0, -1, -5, -100, int.MinValue, int.MaxValue })
            {
                var result = await new ExcelSalesQuoteImporterProvider().ExportSalesQuoteAsFileAsync(null, id, "xlsx").ConfigureAwait(false);
                Assert.False(result.ActionSucceeded);
                Assert.Equal($"ERROR! The excel export cannot use {id} to locate a record for export", result.Messages[0]);
            }
        }

        [Fact(Skip = "Validated")]
        public async Task Verify_Export_SalesQuote_To_Excel_WithoutCallingInitialize_Returns_AFailingCEFActionResponse()
        {
            // Arrange/Act
            var result = await new ExcelSalesQuoteImporterProvider().ExportSalesQuoteAsFileAsync(null, 1, "xlsx").ConfigureAwait(false);
            // Assert
            Assert.False(result.ActionSucceeded);
            Assert.Equal("ERROR! Context or DbSet was null, run Initialize before calling this function.", result.Messages[0]);
        }

        [Fact(Skip = "Validated")]
        public async Task Verify_Export_SalesQuote_To_Excel_WithAnIDNotInTheDbSet_Throws_AnArgumentException()
        {
            // Arrange
            var provider = new ExcelSalesQuoteImporterProvider();
            foreach (var id in new[] { 9999, 99999, 999999, 9999999, 99999999, 999999999 })
            {
                // Act/Assert
                await Assert.ThrowsAsync<ArgumentException>(() => provider.ExportSalesQuoteAsFileAsync(null, id, "xlsx")).ConfigureAwait(false);
            }
        }
    }
}
