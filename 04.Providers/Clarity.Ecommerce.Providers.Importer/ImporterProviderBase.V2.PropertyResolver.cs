// <copyright file="ImporterProviderBase.V2.PropertyResolver.cs" company="clarity-ventures.com">
// Copyright (c) 2017-2023 clarity-ventures.com. All rights reserved.
// </copyright>
// <summary>Implements the importer provider base class</summary>
// ReSharper disable BadSwitchBracesIndent, StringLiteralTypo
// #define UAG
// ReSharper disable MultipleStatementsOnOneLine
namespace Clarity.Ecommerce.Providers.Importer
{
    using System.Linq;
    using System.Threading.Tasks;
    using DataModel;
    using Interfaces.Models;
    using Interfaces.Models.Import;
    using Inventory;
    using Models;
    using Utilities;

    public abstract partial class ImporterProviderBase
    {
        /// <summary>Resolve item.</summary>
        /// <param name="item">              The item.</param>
        /// <param name="model">             The model.</param>
        /// <param name="contextProfileName">Name of the context profile.</param>
        /// <returns>A Task.</returns>
        private async Task<(bool ensurePricingGetsSent, IRawProductPricesModel? rawPricingModel, bool ensureInventoryGetsSent, IUpdateInventoryForProduct? rawInventoryModel)>
            // ReSharper disable once CognitiveComplexity, CyclomaticComplexity
            ResolveItemAsync(IImportItem item, IProductModel model, string? contextProfileName)
        {
            var ensurePricingGetsSent = false;
            var ensureInventoryGetsSent = false;
            var ensurePackageGetsAKey = false;
            var ensureMasterPackGetsAKey = false;
            var ensurePalletGetsAKey = false;
            var ensureStatus = true;
            var ensureType = true;
            IRawProductPricesModel? rawPricingModel = null;
            IUpdateInventoryForProduct? rawInventoryModel = null;
            foreach (var field in item.Fields!)
            {
                var value = field.Value?.Trim();
                if (!Contract.CheckAllValidKeys(field.Name, value))
                {
                    continue;
                }
                var fieldName = field.Name!.Trim().ToLower();
                while (fieldName.Contains(" "))
                {
                    fieldName = fieldName.Replace(" ", string.Empty);
                }
                while (fieldName.Contains("-"))
                {
                    fieldName = fieldName.Replace("-", string.Empty);
                }
                while (fieldName.Contains("_"))
                {
                    fieldName = fieldName.Replace("_", string.Empty);
                }
                while (fieldName.Contains("."))
                {
                    fieldName = fieldName.Replace(".", string.Empty);
                }
                while (fieldName.Contains("*"))
                {
                    fieldName = fieldName.Replace("*", string.Empty);
                }
                while (fieldName.Contains("("))
                {
                    fieldName = fieldName.Replace("(", string.Empty);
                }
                while (fieldName.Contains(")"))
                {
                    fieldName = fieldName.Replace(")", string.Empty);
                }
                while (fieldName.Contains("/"))
                {
                    fieldName = fieldName.Replace("/", string.Empty);
                }
                while (fieldName.Contains("\\"))
                {
                    fieldName = fieldName.Replace("\\", string.Empty);
                }
                while (fieldName.Contains("\""))
                {
                    fieldName = fieldName.Replace("\"", string.Empty);
                }
                while (fieldName.Contains("%"))
                {
                    fieldName = fieldName.Replace("%", "percent");
                }
                while (fieldName.Contains("#"))
                {
                    fieldName = fieldName.Replace("#", "number");
                }
                while (fieldName.Contains("qty"))
                {
                    fieldName = fieldName.Replace("qty", "quantity");
                }
                while (fieldName.Contains("uom"))
                {
                    fieldName = fieldName.Replace("uom", "unitofmeasure");
                }
                while (fieldName.Contains("unitofmeasureunitofmeasure"))
                {
                    fieldName = fieldName.Replace("unitofmeasureunitofmeasure", "unitofmeasure");
                }
                while (fieldName.Contains("pkg"))
                {
                    fieldName = fieldName.Replace("pkg", "package");
                }
                while (fieldName.Contains("nbr"))
                {
                    fieldName = fieldName.Replace("nbr", "number");
                }
                while (fieldName.Contains("lenght"))
                {
                    fieldName = fieldName.Replace("lenght", "length");
                }
                var originalFieldName = fieldName;
                string? assignToOtherEntity = null;
                switch (fieldName)
                {
                    case "category":
                    case "categories":
#if UAG
                    case "subcategory":
                    case "vendorcategory":
                    case "vendorsubcategory":
#endif
                    {
                        if (propertyNames.Contains("ProductCategories"))
                        {
                            SetCategoryToEntity(value, model);
                        }
                        continue;
                    }
#if UAG
                    case "uagcategory":
                    { fieldName = "UAG Category"; break; } // Creates an Attribute
                    case "uagsubcategory":
                    { fieldName = "UAG Sub Category"; break; } // Creates an Attribute
#endif
                    case "status":
                    case "productstatus":
                    {
                        if (propertyNames.Contains("Status"))
                        {
                            SetStatus(value, model);
                            ensureStatus = false;
                        }
                        continue;
                    }
                    case "producttype":
                    case "type":
                    {
                        if (propertyNames.Contains("Type"))
                        {
                            SetType(value, model);
                            ensureType = false;
                        }
                        continue;
                    }
                    case "associations":
                    case "relatedproducts":
                    {
                        if (propertyNames.Contains("ProductAssociations"))
                        {
                            SetAssociations(value, model);
                        }
                        continue;
                    }
                    case "variant":
                    case "variants":
                    case "variantsofmaster":
                    case "variantofmaster":
                    {
                        if (propertyNames.Contains("ProductAssociations"))
                        {
                            SetAssociations(value, model, "Variant of Master");
                        }
                        continue;
                    }
                    case "kitcomponent":
                    case "kitcomponents":
                    {
                        if (propertyNames.Contains("ProductAssociations"))
                        {
                            SetAssociations(value, model, "Kit Component");
                        }
                        continue;
                    }
                    case "image":
                    case "imagenew":
                    case "images":
                    case "imagesnew":
#if UAG
                    case "primaryimage":
                    case "secondaryimage":
#endif
                    {
                        if (propertyNames.Contains(nameof(IProductModel.Images)))
                        {
                            SetImage(value, model);
                        }
                        continue;
                    }
                    case "documents":
                    case "storedfiles":
                    {
                        if (propertyNames.Contains(nameof(IProductModel.StoredFiles)))
                        {
                            SetFile(value, model);
                        }
                        continue;
                    }
                    case "manufacturers":
                    {
                        if (propertyNames.Contains(nameof(IProductModel.Manufacturers)))
                        {
                            SetAssociatedObject<IProductModel, ManufacturerProductModel, ManufacturerModel>(
                                value,
                                nameof(Product.Manufacturers),
                                nameof(ManufacturerProduct.Master),
                                model,
                                x => x.MasterName == value || x.Master != null && x.Master.Name == value);
                        }
                        continue;
                    }
                    case "vendorname":
                    case "vendors":
                    {
                        if (propertyNames.Contains(nameof(IProductModel.Vendors)))
                        {
                            SetAssociatedObject<IProductModel, VendorProductModel, VendorModel>(
                                value,
                                nameof(Product.Vendors),
                                nameof(VendorProduct.Master),
                                model,
                                x => x.MasterName == value || x.Master != null && x.Master.Name == value);
                        }
                        continue;
                    }
                    case "stores":
                    {
                        if (propertyNames.Contains("Stores"))
                        {
                            SetAssociatedObject<IProductModel, StoreProductModel, StoreModel>(
                                value,
                                nameof(Product.Stores),
                                nameof(StoreProduct.Master),
                                model,
                                x => x.MasterName == value || x.Master != null && x.Master.Name == value);
                        }
                        continue;
                    }
                    case "brands":
                    {
                        if (propertyNames.Contains("Brands"))
                        {
                            SetAssociatedObject<IProductModel, BrandProductModel, BrandModel>(
                                value,
                                nameof(Product.Brands),
                                nameof(BrandProduct.Master),
                                model,
                                x => x.MasterName == value || x.Master != null && x.Master.Name == value);
                        }
                        continue;
                    }
#if UAG
                    // ReSharper disable BadControlBracesLineBreaks, MissingLinebreak, MultipleStatementsOnOneLine
                    case "sort":
                    { fieldName = nameof(IProduct.SortOrder); break; }
                    case "catalog":
                    case "catalognumber":
                    case "harrproductid":
                    case "item":
                    case "itemcode":
                    case "itemno":
                    case "itemnumber":
                    case "manufitemnumber":
                    case "mfgnumber":
                    case "model":
                    case "modelnumber":
                    case "ordercode":
                    case "part":
                    case "partnumber":
                    case "partnumbers":
                    case "parts":
                    case "productsku":
                    case "sku":
                    case "stockcode":
                    case "stylenumber":
                    case "customkey":
                    { fieldName = nameof(IProduct.CustomKey); break; }
                    case "archived":
                    case "cancelled":
                    case "disabled":
                    { fieldName = nameof(IProduct.IsDiscontinued); break; }
                    case "availablefrom":
                    { fieldName = nameof(IProduct.AvailableStartDate); break; }
                    case "availableuntil":
                    { fieldName = nameof(IProduct.AvailableEndDate); break; }
                    case "description":
                    case "itemname":
                    case "itemdescription":
                    case "itemshortdescription":
                    case "marketingdescription":
                    case "modelanddescription":
                    case "name":
                    case "productdescription":
                    case "productname":
                    case "shortdescription":
                    case "stockcodedescription":
                    { fieldName = nameof(IProduct.Name); break; }
                    case "seourl":
                    { fieldName = nameof(IProduct.SeoUrl); break; }
                    case "active":
                    { fieldName = nameof(IProduct.Active); break; }
                    case "isvisible":
                    { fieldName = nameof(IProduct.IsVisible); break; }
                    case "isdiscontinued":
                    { fieldName = nameof(IProduct.IsDiscontinued); break; }
                    case "itemlongdescription":
                    case "longdescription":
                    case "longdesc":
                    { fieldName = nameof(IProduct.Description); break; }
                    case "cefshortdescription":
                    { fieldName = nameof(IProduct.ShortDescription); break; }
                    case "itemspercase":
                    case "mastercartonquantity":
                    case "piecespercase":
                    { fieldName = nameof(IProduct.QuantityPerMasterPack); break; }
                    case "piecesperpallet":
                    case "palletquantity":
                    { fieldName = nameof(IProduct.QuantityPerPallet); break; }
                    case "orderminimum":
                    { fieldName = "Order Minimum"; break; }
                    case "restockingfee":
                    { fieldName = "Restocking Fee"; break; }
                    case "brandid":
                    { fieldName = nameof(IProduct.BrandName); break; }
                    case "cartonquantity":
                    case "packaging":
                    case "shipquantity":
                    case "size1":
                    case "unitofmeasure":
                    case "unitsize":
                    { fieldName = nameof(IProduct.UnitOfMeasure); break; }
                    case "manufactureritemcode":
                    case "manufacturerpartnumber":
                    case "mfgpartnumber":
                    { fieldName = nameof(IProduct.ManufacturerPartNumber); break; }
                    case "manufactureritemcode1":
                    { fieldName = "Manufacturer Item Code 1"; break; } // Creates an Attribute
                    case "height":
                    { fieldName = "Height"; break; }
                    case "dimpackageheightin":
                    case "packageheight":
                    case "productdepth":
                    case "shippingheight":
                    case "shippingh":
                    { fieldName = "package.height"; break; } // Will assign to package property
                    case "packageheightunitofmeasure":
                    case "packageheightuofm":
                    { fieldName = "package.heightunitofmeasure"; break; } // Will assign to package property
                    case "depth":
                    case "length":
                    { fieldName = "Depth"; break; }
                    case "dimpackagelengthin":
                    case "packagedepth":
                    case "productlength":
                    case "shippinglength":
                    case "shippingl":
                    { fieldName = "package.depth"; break; } // Will assign to package property
                    case "packagedepthunitofmeasure":
                    case "packagedepthuofm":
                    { fieldName = "package.depthunitofmeasure"; break; } // Will assign to package property
                    case "width":
                    { fieldName = "Width"; break; }
                    case "dimpackagewidthin":
                    case "packagewidth":
                    case "productwidth":
                    case "shippingwidth":
                    case "shippingw":
                    { fieldName = "package.width"; break; } // Will assign to package property
                    case "packagewidthunitofmeasure":
                    case "packagewidthuofm":
                    { fieldName = "package.widthunitofmeasure"; break; } // Will assign to package property
                    case "weight":
                    { fieldName = "Weight"; break; }
                    case "averageweight":
                    case "individualweight":
                    case "lbs":
                    case "packageweight":
                    case "packagedweightlbs":
                    case "shipweight":
                    case "shipweightum":
                    case "shippingweight":
                    case "shippingwtlbs":
                    case "weightlbs":
                    case "wtealbs":
                    { fieldName = "package.weight"; break; } // Will assign to package property
                    case "packageweightunitofmeasure":
                    case "packageweightuofm":
                    { fieldName = "package.weightunitofmeasure"; break; } // Will assign to package property
                    case "packagedimensionalweight":
                    { fieldName = "package.dimensionalweight"; break; } // Will assign to package property
                    case "packagedimensionalweightunitofmeasure":
                    case "packagedimensionalweightuofm":
                    { fieldName = "package.dimensionalweightunitofmeasure"; break; } // Will assign to package property
                    case "masterpackheight":
                    { fieldName = "masterpack.height"; break; } // Will assign to master pack property
                    case "masterpackheightunitofmeasure":
                    case "masterpackheightuofm":
                    { fieldName = "masterpack.heightunitofmeasure"; break; } // Will assign to master pack property
                    case "masterpackwidth":
                    { fieldName = "masterpack.width"; break; } // Will assign to master pack property
                    case "masterpackwidthunitofmeasure":
                    case "masterpackwidthuofm":
                    { fieldName = "masterpack.widthunitofmeasure"; break; } // Will assign to master pack property
                    case "masterpackdepth":
                    { fieldName = "masterpack.depth"; break; } // Will assign to master pack property
                    case "masterpackdepthunitofmeasure":
                    case "masterpackdepthuofm":
                    { fieldName = "masterpack.depthunitofmeasure"; break; } // Will assign to master pack property
                    case "masterpackweight":
                    { fieldName = "masterpack.weight"; break; } // Will assign to master pack property
                    case "masterpackweightunitofmeasure":
                    case "masterpackweightuofm":
                    { fieldName = "masterpack.weightunitofmeasure"; break; } // Will assign to master pack property
                    case "masterpackimensionalweight":
                    { fieldName = "masterpack.dimensionalweight"; break; } // Will assign to master pack property
                    case "masterpackdimensionalweightunitofmeasure":
                    case "masterpackdimensionalweightuofm":
                    { fieldName = "masterpack.dimensionalweightunitofmeasure"; break; } // Will assign to master pack property
                    case "palletheight":
                    { fieldName = "pallet.height"; break; } // Will assign to pallet property
                    case "palletheightunitofmeasure":
                    case "palletheightuofm":
                    { fieldName = "pallet.heightunitofmeasure"; break; } // Will assign to pallet property
                    case "palletwidth":
                    { fieldName = "pallet.width"; break; } // Will assign to pallet property
                    case "palletwidthunitofmeasure":
                    case "palletwidthuofm":
                    { fieldName = "pallet.widthunitofmeasure"; break; } // Will assign to pallet property
                    case "palletdepth":
                    { fieldName = "pallet.depth"; break; } // Will assign to pallet property
                    case "palletdepthunitofmeasure":
                    case "palletdepthuofm":
                    { fieldName = "pallet.depthunitofmeasure"; break; } // Will assign to pallet property
                    case "palletweight":
                    { fieldName = "pallet.weight"; break; } // Will assign to pallet property
                    case "palletweightunitofmeasure":
                    case "palletweightuofm":
                    { fieldName = "pallet.weightunitofmeasure"; break; } // Will assign to pallet property
                    case "palletimensionalweight":
                    { fieldName = "pallet.dimensionalweight"; break; } // Will assign to pallet property
                    case "palletdimensionalweightunitofmeasure":
                    case "palletdimensionalweightuofm":
                    { fieldName = "pallet.dimensionalweightunitofmeasure"; break; } // Will assign to pallet property
                    case "season":
                    case "season1":
                    case "vendorid":
                    case "vendoridnumber":
                    { fieldName = "Vendor ID"; break; } // Creates an Attribute
                    case "vendorcontact":
                    { fieldName = "Vendor Contact"; break; } // Creates an Attribute
                    case "id":
                    { fieldName = "Imported ID"; break; } // Creates an Attribute
                    case "color":
                    case "vendorcolor":
                    { fieldName = "Color"; break; } // Creates an Attribute
                    case "vendor":
                    case "vendorcompanyname":
                    case "vendordisplayname":
                    { fieldName = "vendor_display_name"; break; } // Creates an Attribute
                    case "banners":
                    { fieldName = "Banners"; break; } // Creates an Attribute
                    case "colorcode":
                    { fieldName = "Color Code"; break; } // Creates an Attribute
                    case "seasons":
                    { fieldName = "Seasons"; break; } // Creates an Attribute
                    case "division":
                    { fieldName = "Division"; break; } // Creates an Attribute
                    case "department":
                    { fieldName = "Department"; break; } // Creates an Attribute
                    case "casequantity":
                    case "packed":
                    case "packquantity":
                    case "packagequantity":
                    case "quantity":
                    case "quantityinunitofmeasure":
                    case "unitsperpack1":
                    { fieldName = "QuantityInUoM"; break; } // Creates an Attribute
                    case "palletspertruckload":
                    { fieldName = "Pallets Per Truckload"; break; } // Creates an Attribute
                    case "associatedparts":
                    { fieldName = "Associated Parts"; break; } // Creates an Attribute
                    case "associatedwholegoods":
                    { fieldName = "Associated Whole Goods"; break; } // Creates an Attribute
                    case "brokencasefee":
                    case "brokencsfee":
                    { fieldName = "Broken Case Fee"; break; } // Creates an Attribute
                    case "freightterms":
                    { fieldName = "Freight Terms"; break; } // Creates an Attribute
                    case "rebatedetails":
                    { fieldName = "Rebate Details"; break; } // Creates an Attribute
                    case "shippingrestrictions":
                    case "shippinglocationrestrictions":
                    { fieldName = "Shipping Location Restrictions"; break; } // Creates an Attribute
                    case "vendorrating":
                    { fieldName = "Vendor Rating"; break; } // Creates an Attribute
                    case "rebate":
                    case "rebates":
                    case "vendorrebates":
                    { fieldName = "Vendor Rebates"; break; } // Creates an Attribute
                    case "vendorarp":
                    { fieldName = "Vendor ARP"; break; } // Creates an Attribute
                    case "vendordocuments":
                    { fieldName = "Vendor Documents"; break; } // Creates an Attribute
                    case "vendortype":
                    { fieldName = "Vendor Type"; break; } // Creates an Attribute
                    case "warranty":
                    { fieldName = "Warranty"; break; } // Creates an Attribute
                    case "warrantydetails":
                    { fieldName = "Warranty Details"; break; } // Creates an Attribute
                    case "website":
                    { fieldName = "Website"; break; } // Creates an Attribute
                    case "returnpolicy":
                    { fieldName = "Return Policy"; break; } // Creates an Attribute
                    case "map":
                    { fieldName = "Map"; break; } // Creates an Attribute
                    case "multiplierappliedto":
                    case "multiplierappliedtomsrpmaplistprice":
                    { fieldName = "Multiplier Applied To"; break; } // Creates an Attribute
                    case "multiplier":
                    case "multiplierpercent":
                    case "aquatechmultiplier":
                    { fieldName = "Multiplier Percentage"; break; } // Creates an Attribute
                    case "technicaldetails":
                    { fieldName = "Technical Details"; break; } // Creates an Attribute
                    case "shipsfrom":
                    { fieldName = "Ships From"; break; } // Creates an Attribute
                    case "averageleadtime":
                    case "leadtime":
                    case "leadtimebusinessdays":
                    { fieldName = "Average Lead Time Business Days"; break; } // Creates an Attribute
                    case "searchterms":
                    { fieldName = "Search Terms"; break; } // Creates an Attribute
                    case "selltodepletion":
                    { fieldName = "Sell To Depletion"; break; } // Creates an Attribute
                    case "olditemnumber":
                    case "replaces":
                    { fieldName = "Replaces Old Item Number"; break; } // Creates an Attribute
                    case "topsellers":
                    { fieldName = "Top Sellers"; break; } // Creates an Attribute
                    case "customergrouprule":
                    { fieldName = "Customer Group Rule"; break; } // Creates an Attribute
                    case "sizegroups":
                    { fieldName = "Size Groups"; break; } // Creates an Attribute
                    case "gpitemnumber1":
                    { fieldName = "GP Item Number 1"; break; } // Creates an Attribute
                    case "gpitemdescription1":
                    { fieldName = "GP Item Description 1"; break; } // Creates an Attribute
                    case "gtin":
                    case "upc":
                    case "upc1":
                    case "upccode":
                    { fieldName = "UPC/GTIN"; break; } // Creates an Attribute
                    case "suggestedlist":
                    { fieldName = nameof(IProduct.PriceMsrp); break; }
                    case "mapmsrp":
                    case "retailprice":
                    case "retailprice1usd":
                    case "retailusd":
                    { fieldName = nameof(IProduct.PriceReduction); break; }
                    case "loyaltypricing":
                    case "vpleligible":
                    { fieldName = "Vendor Loyalty Pricing"; break; } // Creates an Attribute
                    case "listprice":
                    { fieldName = "List Price"; break; } // Creates an Attribute
                    case "bulkpricebreaks":
                    { fieldName = "Bulk Pricing Breaks"; break; } // Creates an Attribute
                    case "pricelevel":
                    { fieldName = "Price Level"; break; } // Creates an Attribute
                    case "costeach":
                    case "individualprice":
                    case "priceperpiece":
                    case "qplhighestpriceperpiece":
                    case "uagnetpriceperpiece":
                    case "wholesaleprice":
                    case "wholesaleusd":
                    { fieldName = "Wholesale Price (per piece)"; break; } // Creates an Attribute
                    case "casecost":
                    case "cost":
                    case "custprice":
                    case "ddprice":
                    case "net":
                    case "netcost":
                    case "netprice":
                    case "newprice":
                    case "price":
                    case "price1usd":
                    case "priceeach":
                    case "priceperunitofmeasure":
                    case "qplhighestpriceperunitofmeasure":
                    case "qplprice":
                    case "specialusd":
                    case "stdnetprice":
                    case "uagnetpriceperunitofmeasure":
                    case "uagnetpriceperunitofmeasuresku":
                    case "uagcost":
                    case "uagnet":
                    case "uagnetcost":
                    case "uagnetprice":
                    case "usdprice":
                    case "unitcost":
                    case "unitprice":
                    { fieldName = nameof(IProduct.PriceBase); break; }
                    case "bulklevel1quantity1":
                    case "bulklevel1quantity":
                    { fieldName = "Volume Pricing Minimum 1"; assignToOtherEntity = nameof(IProductPricePoint); break; } // Creates an Attribute
                    case "bulklevel1price":
                    case "bulklevel1price1":
                    { fieldName = "Volume Pricing Price 1"; assignToOtherEntity = nameof(IProductPricePoint); break; } // Creates an Attribute
                    case "bulklevel2quantity":
                    case "bulklevel2quantity1":
                    { fieldName = "Volume Pricing Minimum 2"; assignToOtherEntity = nameof(IProductPricePoint); break; } // Creates an Attribute
                    case "bulklevel2price":
                    case "bulklevel2price1":
                    { fieldName = "Volume Pricing Price 2"; assignToOtherEntity = nameof(IProductPricePoint); break; } // Creates an Attribute
                    case "bulklevel3quantity":
                    case "bulklevel3quantity1":

                    { fieldName = "Volume Pricing Minimum 3"; assignToOtherEntity = nameof(IProductPricePoint); break; } // Creates an Attribute
                    case "bulklevel3price":
                    case "bulklevel3price1":
                    { fieldName = "Volume Pricing Price 3"; assignToOtherEntity = nameof(IProductPricePoint); break; } // Creates an Attribute
                    case "bulklevel4quantity":
                    case "bulklevel4quantity1":
                    { fieldName = "Volume Pricing Minimum 4"; assignToOtherEntity = nameof(IProductPricePoint); break; } // Creates an Attribute
                    case "bulklevel4price":
                    case "bulklevel4price1":
                    { fieldName = "Volume Pricing Price 4"; assignToOtherEntity = nameof(IProductPricePoint); break; } // Creates an Attribute
                    case "bulklevel5quantity":
                    case "bulklevel5quantity1":
                    { fieldName = "Volume Pricing Minimum 5"; assignToOtherEntity = nameof(IProductPricePoint); break; } // Creates an Attribute
                    case "bulklevel5price":
                    case "bulklevel5price1":
                    { fieldName = "Volume Pricing Price 5"; assignToOtherEntity = nameof(IProductPricePoint); break; } // Creates an Attribute
                    case "bulklevel6quantity":
                    case "bulklevel6quantity1":
                    { fieldName = "Volume Pricing Minimum 6"; assignToOtherEntity = nameof(IProductPricePoint); break; } // Creates an Attribute
                    case "bulklevel6price":
                    case "bulklevel6price1":
                    { fieldName = "Volume Pricing Price 6"; assignToOtherEntity = nameof(IProductPricePoint); break; } // Creates an Attribute
                    case "bulklevel7quantity":
                    case "bulklevel7quantity1":
                    { fieldName = "Volume Pricing Minimum 7"; assignToOtherEntity = nameof(IProductPricePoint); break; } // Creates an Attribute
                    case "bulklevel7price":
                    case "bulklevel7price1":
                    { fieldName = "Volume Pricing Price 7"; assignToOtherEntity = nameof(IProductPricePoint); break; } // Creates an Attribute
                    case "bulklevel8quantity":
                    case "bulklevel8quantity1":
                    { fieldName = "Volume Pricing Minimum 8"; assignToOtherEntity = nameof(IProductPricePoint); break; } // Creates an Attribute
                    case "bulklevel8price":
                    case "bulklevel8price1":
                    { fieldName = "Volume Pricing Price 8"; assignToOtherEntity = nameof(IProductPricePoint); break; } // Creates an Attribute
                    case "bulklevel9quantity":
                    case "bulklevel9quantity1":
                    { fieldName = "Volume Pricing Minimum 9"; assignToOtherEntity = nameof(IProductPricePoint); break; } // Creates an Attribute
                    case "bulklevel9price":
                    case "bulklevel9price1":
                    { fieldName = "Volume Pricing Price 9"; assignToOtherEntity = nameof(IProductPricePoint); break; } // Creates an Attribute
                    case "bulklevel10quantity":
                    case "bulklevel10quantity1":
                    { fieldName = "Volume Pricing Minimum 10"; assignToOtherEntity = nameof(IProductPricePoint); break; } // Creates an Attribute
                    case "bulklevel10price":
                    case "bulklevel10price1":
                    { fieldName = "Volume Pricing Price 10"; assignToOtherEntity = nameof(IProductPricePoint); break; } // Creates an Attribute
                    // ReSharper restore BadControlBracesLineBreaks, MissingLinebreak, MultipleStatementsOnOneLine
#else
                    // Unit OOB Dimensions
                    case "height":
                    {
                        fieldName = "Height";
                        break;
                    }
                    case "depth":
                    case "length":
                    {
                        fieldName = "Depth";
                        break;
                    }
                    case "width":
                    {
                        fieldName = "Width";
                        break;
                    }
                    case "weight":
                    {
                        fieldName = "Weight";
                        break;
                    }
                    // Package Dimensions
                    case "packageheight":
                    {
                        fieldName = "package.height";
                        break;
                    } // Will assign to package property
                    case "packageheightunitofmeasure":
                    case "packageheightuofm":
                    {
                        fieldName = "package.heightunitofmeasure";
                        break;
                    } // Will assign to package property
                    case "packagedepth":
                    case "packagelength":
                    {
                        fieldName = "package.depth";
                        break;
                    } // Will assign to package property
                    case "packagedepthunitofmeasure":
                    case "packagelengthunitofmeasure":
                    case "packagedepthuofm":
                    case "packagelengthuofm":
                    {
                        fieldName = "package.depthunitofmeasure";
                        break;
                    } // Will assign to package property
                    case "packagewidth":
                    {
                        fieldName = "package.width";
                        break;
                    } // Will assign to package property
                    case "packagewidthunitofmeasure":
                    case "packagewidthuofm":
                    {
                        fieldName = "package.widthunitofmeasure";
                        break;
                    } // Will assign to package property
                    case "packageweight":
                    {
                        fieldName = "package.weight";
                        break;
                    } // Will assign to package property
                    case "packageweightunitofmeasure":
                    case "packageweightuofm":
                    {
                        fieldName = "package.weightunitofmeasure";
                        break;
                    } // Will assign to package property
                    case "packagedimensionalweight":
                    {
                        fieldName = "package.dimensionalweight";
                        break;
                    } // Will assign to package property
                    case "packagedimensionalweightunitofmeasure":
                    case "packagedimensionalweightuofm":
                    {
                        fieldName = "package.dimensionalweightunitofmeasure";
                        break;
                    } // Will assign to package property
                    // Master Pack Dimensions
                    case "masterpackheight":
                    {
                        fieldName = "masterpack.height";
                        break;
                    } // Will assign to master pack property
                    case "masterpackheightunitofmeasure":
                    case "masterpackheightuofm":
                    {
                        fieldName = "masterpack.heightunitofmeasure";
                        break;
                    } // Will assign to master pack property
                    case "masterpackdepth":
                    case "masterpacklength":
                    {
                        fieldName = "masterpack.depth";
                        break;
                    } // Will assign to master pack property
                    case "masterpackdepthunitofmeasure":
                    case "masterpacklengthunitofmeasure":
                    case "masterpackdepthuofm":
                    case "masterpacklengthuofm":
                    {
                        fieldName = "masterpack.depthunitofmeasure";
                        break;
                    } // Will assign to master pack property
                    case "masterpackwidth":
                    {
                        fieldName = "masterpack.width";
                        break;
                    } // Will assign to master pack property
                    case "masterpackwidthunitofmeasure":
                    case "masterpackwidthuofm":
                    {
                        fieldName = "masterpack.widthunitofmeasure";
                        break;
                    } // Will assign to master pack property
                    case "masterpackweight":
                    {
                        fieldName = "masterpack.weight";
                        break;
                    } // Will assign to master pack property
                    case "masterpackweightunitofmeasure":
                    case "masterpackweightuofm":
                    {
                        fieldName = "masterpack.weightunitofmeasure";
                        break;
                    } // Will assign to master pack property
                    case "masterpackimensionalweight":
                    {
                        fieldName = "masterpack.dimensionalweight";
                        break;
                    } // Will assign to master pack property
                    case "masterpackdimensionalweightunitofmeasure":
                    case "masterpackdimensionalweightuofm":
                    {
                        fieldName = "masterpack.dimensionalweightunitofmeasure";
                        break;
                    } // Will assign to master pack property
                    // Pallet Dimensions
                    case "palletheight":
                    {
                        fieldName = "pallet.height";
                        break;
                    } // Will assign to pallet property
                    case "palletheightunitofmeasure":
                    case "palletheightuofm":
                    {
                        fieldName = "pallet.heightunitofmeasure";
                        break;
                    } // Will assign to pallet property
                    case "palletdepth":
                    case "palletlength":
                    {
                        fieldName = "pallet.depth";
                        break;
                    } // Will assign to pallet property
                    case "palletdepthunitofmeasure":
                    case "palletlengthunitofmeasure":
                    case "palletdepthuofm":
                    case "palletlengthuofm":
                    {
                        fieldName = "pallet.depthunitofmeasure";
                        break;
                    } // Will assign to pallet property
                    case "palletwidth":
                    {
                        fieldName = "pallet.width";
                        break;
                    } // Will assign to pallet property
                    case "palletwidthunitofmeasure":
                    case "palletwidthuofm":
                    {
                        fieldName = "pallet.widthunitofmeasure";
                        break;
                    } // Will assign to pallet property
                    case "palletweight":
                    {
                        fieldName = "pallet.weight";
                        break;
                    } // Will assign to pallet property
                    case "palletweightunitofmeasure":
                    case "palletweightuofm":
                    {
                        fieldName = "pallet.weightunitofmeasure";
                        break;
                    } // Will assign to pallet property
                    case "palletimensionalweight":
                    {
                        fieldName = "pallet.dimensionalweight";
                        break;
                    } // Will assign to pallet property
                    case "palletdimensionalweightunitofmeasure":
                    case "palletdimensionalweightuofm":
                    {
                        fieldName = "pallet.dimensionalweightunitofmeasure";
                        break;
                    } // Will assign to pallet property
                    case "listprice":
                    case "msrpprice":
                    case "pricelist":
                    case "pricemsrp":
                    {
                        fieldName = "rawpricing.pricemsrp";
                        break;
                    }
                    case "reductionprice":
                    case "pricereduction":
                    {
                        fieldName = "rawpricing.pricereduction";
                        break;
                    }
                    case "baseprice":
                    case "pricebase":
                    {
                        fieldName = "rawpricing.pricebase";
                        break;
                    }
                    case "saleprice":
                    case "pricesale":
                    {
                        fieldName = "rawpricing.pricesale";
                        break;
                    }
                    case "stockquantity":
                    case "quantity":
                    {
                        fieldName = "rawinventory.quantity";
                        break;
                    }
                    case "stockquantityallocated":
                    case "quantityallocated":
                    {
                        fieldName = "rawinventory.quantityallocated";
                        break;
                    }
                    case "stockquantitypresold":
                    case "quantitypresold":
                    {
                        fieldName = "rawinventory.quantitypresold";
                        break;
                    }
#endif
                }
                if (Contract.CheckValidKey(assignToOtherEntity))
                {
                    // TODO
                    continue;
                }
                if (fieldName.StartsWith("rawpricing.") && fieldName != "rawpricing.")
                {
                    var innerFieldName = fieldName["rawpricing".Length..];
                    var name = GetRawPricingInnerName(innerFieldName);
                    if (Contract.CheckValidKey(name))
                    {
                        ensurePricingGetsSent = true;
                        rawPricingModel ??= RegistryLoaderWrapper.GetInstance<IRawProductPricesModel>(contextProfileName);
                        SetRawPricingProperty(name!, value, rawPricingModel);
                        continue;
                    }
                }
                if (fieldName.StartsWith("rawinventory.") && fieldName != "rawinventory.")
                {
                    var innerFieldName = fieldName["rawinventory".Length..];
                    var name = GetRawInventoryInnerName(innerFieldName);
                    if (Contract.CheckValidKey(name))
                    {
                        ensureInventoryGetsSent = true;
                        rawInventoryModel ??= new UpdateInventoryForProduct();
                        SetRawInventoryProperty(name!, value, rawInventoryModel);
                        continue;
                    }
                }
                if (fieldName.StartsWith("package.") && fieldName != "package.")
                {
                    var innerFieldName = fieldName["package".Length..];
                    var name = GetPackageInnerName(innerFieldName);
                    if (Contract.CheckValidKey(name))
                    {
                        if (model.Package?.IsCustom == false)
                        {
                            // Don't override the properties on a non-custom package, blank it so it will make a new entry
                            model.PackageID = null;
                            model.PackageKey = null;
                            model.PackageName = null;
                            model.Package = null;
                        }
                        ensurePackageGetsAKey = true;
                        await EnsurePackageAsync(model, contextProfileName).ConfigureAwait(false);
                        SetPackageProperty(name!, value, model);
                        continue;
                    }
                }
                else if (fieldName.StartsWith("masterpack.") && fieldName != "masterpack.")
                {
                    var innerFieldName = fieldName["masterpack".Length..];
                    var name = GetPackageInnerName(innerFieldName);
                    if (Contract.CheckValidKey(name))
                    {
                        if (model.MasterPack?.IsCustom == false)
                        {
                            // Don't override the properties on a non-custom master pack, blank it so it will make a new entry
                            model.MasterPackID = null;
                            model.MasterPackKey = null;
                            model.MasterPackName = null;
                            model.MasterPack = null;
                        }
                        ensureMasterPackGetsAKey = true;
                        await EnsureMasterPackAsync(model, contextProfileName).ConfigureAwait(false);
                        SetMasterPackProperty(name!, value, model);
                        continue;
                    }
                }
                else if (fieldName.StartsWith("pallet.") && fieldName != "pallet.")
                {
                    var innerFieldName = fieldName["pallet".Length..];
                    var name = GetPackageInnerName(innerFieldName);
                    if (Contract.CheckValidKey(name))
                    {
                        if (model.Pallet?.IsCustom == false)
                        {
                            // Don't override the properties on a non-custom pallet, blank it so it will make a new entry
                            model.PalletID = null;
                            model.PalletKey = null;
                            model.PalletName = null;
                            model.Pallet = null;
                        }
                        ensurePalletGetsAKey = true;
                        await EnsurePalletAsync(model, contextProfileName).ConfigureAwait(false);
                        SetPalletProperty(name!, value, model);
                        continue;
                    }
                }
                else if (propertyNames.Contains(fieldName)
                    || propertyNames.Select(x => x.ToLower()).Contains(fieldName.ToLower()))
                {
                    // Check if the model has a property with the same name as FieldName
                    SetValueToProperty(field.Name, value, model, fieldName);
                    continue;
                }
                // Create attribute
                // Check if we modified the field name before using field.Name
                CreateAttribute(fieldName == originalFieldName ? field.Name : fieldName, value, model);
            }
            if (ensureType)
            {
                if (Contract.CheckInvalidID(GeneralTypeID))
                {
                    GeneralTypeID = await Workflows.ProductTypes.ResolveWithAutoGenerateToIDAsync(
                            byID: null,
                            byKey: "General",
                            byName: "General",
                            byDisplayName: "General",
                            model: null,
                            contextProfileName: contextProfileName)
                        .ConfigureAwait(false);
                }
                model.TypeID = GeneralTypeID;
            }
            if (ensureStatus)
            {
                if (Contract.CheckInvalidID(NormalStatusID))
                {
                    NormalStatusID = await Workflows.ProductStatuses.ResolveWithAutoGenerateToIDAsync(
                            byID: null,
                            byKey: "Normal",
                            byName: "Normal",
                            byDisplayName: "Normal",
                            model: null,
                            contextProfileName: contextProfileName)
                        .ConfigureAwait(false);
                }
                model.StatusID = NormalStatusID;
            }
            if (ensurePackageGetsAKey)
            {
                var value = "Custom Package for " + model.Name;
                if (value.Length > 128)
                {
                    value = value[..128];
                }
                model.Package!.CustomKey = model.Package.Name = value;
            }
            if (ensureMasterPackGetsAKey)
            {
                var value = "Custom Master Pack for " + model.Name;
                if (value.Length > 128)
                {
                    value = value[..128];
                }
                model.MasterPack!.CustomKey = model.MasterPack.Name = value;
            }
            if (ensurePalletGetsAKey)
            {
                var value = "Custom Pallet for " + model.Name;
                if (value.Length > 128)
                {
                    value = value[..128];
                }
                model.Pallet!.CustomKey = model.Pallet.Name = value;
            }
            return (ensurePricingGetsSent, rawPricingModel, ensureInventoryGetsSent, rawInventoryModel);
        }
    }
}
