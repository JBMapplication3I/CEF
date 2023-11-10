// <copyright file="SampleData.Products.cs" company="clarity-ventures.com">
// Copyright (c) 2016-2023 clarity-ventures.com. All rights reserved.
// </copyright>
// <summary>Implements the sample data. products class</summary>
// ReSharper disable CyclomaticComplexity, CognitiveComplexity, FunctionComplexityOverflow, InvertIf, StringLiteralTypo
#nullable enable
#pragma warning disable format, SA1025,IDE0079
#if ORACLE
namespace Clarity.Ecommerce.DataModel.Oracle.DataSets
#else
namespace Clarity.Ecommerce.DataModel.DataSets
#endif
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using Interfaces.Models;
    using Utilities;

    public partial class SampleData
    {
        private void AddSampleProducts(DateTime createdDate)
        {
            if (context?.Products?.Any(x => x.CustomKey == "432957") == false)
            {
                context.Products.Add(new()
                {
                    // Last Updated from Sales Imported data 2019-04-12
                    Active                              = true,
                    AllowBackOrder                      = true,
                    BrandName                           = "Acer®",
                    CustomKey                           = "432957",
                    Depth                               = 10.8m,
                    DepthUnitOfMeasure                  = "in",
                    Description                         = @"<p><img alt="""" src=""http://cvilnk.com/ecomm-demosite-images/acer-models.jpg"" width=""100%"" /></p><p style=""margin:50px 0 50px 0;"">Acer - Aspire 17.3"" Laptop - 16GB Memory - i7 - 1TB - NVIDIA GeForce 750. This Acer Aspire V3-772G-9850 laptop offers plenty of room to store your media. NVIDIA GeForce GT 750M graphics with 4GB of dedicated video memory ensure content is presented in stunning clarity on the 17.3"" LED-backlit display. Intel i7 3.2 Ghz quad core CPU, 8GB of DDR3 RAM, 6-cell Lithium battery, and a 1 TB hard drive make it perfect for both school and gaming.</p><img alt="""" src=""http://cvilnk.com/ecomm-demosite-images/acer-banner.jpg"" width=""100%"" /><p></p>",
                    Height                              = 1.3m,
                    HeightUnitOfMeasure                 = "in",
                    IsDiscontinued                      = false,
                    IsEligibleForReturn                 = false,
                    IsFreeShipping                      = false,
                    IsTaxable                           = true,
                    IsUnlimitedStock                    = true,
                    IsVisible                           = true,
                    KitBaseQuantityPriceMultiplier      = 0,
                    ManufacturerPartNumber              = "100",
                    Name                                = "Acer® - Aspire 17.3\" Gaming Laptop",
                    NothingToShip                       = false,
                    PriceBase                           = 1299.99m,
                    PriceSale                           = 917.99m,
                    SeoDescription                      = "Great deal on an this Acer Aspire V3-772G-9850 laptop with NVIDIA GeForce GT 750M graphics with 4GB of dedicated video memory and 17.3\" LED-backlit display.",
                    SeoKeywords                         = "Acer, Aspire, Laptop",
                    SeoPageTitle                        = "Acer Aspire Laptop Model V3-772G-9850 16GB 1TB | Clarity",
                    SeoUrl                              = "Acer-Aspire-Laptop-V3-772G-9850-16GB-1TB",
                    ShippingLeadTimeIsCalendarDays      = false,
                    ShortDescription                    = "Acer Aspire 17.3\" Laptop - 16GB Memory - i7 - 1TB - NVIDIA GeForce 750. This Acer Aspire V3-772G-9850 laptop offers plenty of room to store your media. NVIDIA GeForce GT 750M graphics with 4GB of dedicated video memory, on the 17.3\" LED-backlit display.",
                    UnitOfMeasure                       = "EACH",
                    Weight                              = 7.1m,
                    WeightUnitOfMeasure                 = "lbs",
                    Width                               = 16.3m,
                    WidthUnitOfMeasure                  = "in",
                    TypeID = context.ProductTypes.Where(pt => pt.Active && pt.Name == "General").Select(pt => pt.ID).First(),
                    StatusID = context.ProductStatuses.Where(pt => pt.Active && pt.Name == "Normal").Select(pt => pt.ID).First(),
                    AvailableStartDate = null,
                    AvailableEndDate = null,
                    CreatedDate = createdDate,
                    PackageID = context.Packages.Where(p => p.Name == "Default").Select(p => p.ID).FirstOrDefault(),
                    Categories = new List<ProductCategory>
                    {
                        new()
                        {
                            SlaveID = context.Categories.First(c => c.CustomKey == "CAT-1").ID,
                            CreatedDate = createdDate,
                            Active = true,
                        },
                    },
                    ProductInventoryLocationSections = new[]
                    {
                        new ProductInventoryLocationSection
                        {
                            Active = true,
                            CreatedDate = createdDate,
                            SlaveID = context.InventoryLocationSections.First(x => x.CustomKey == "WAREHOUSE-1-Section-2").ID,
                            Quantity = 5,
                        },
                    },
                    JsonAttributes = new SerializableAttributesDictionary
                    {
                        ["Material"] = new() { Key = "Material", Value = "Composite" },
                        ["Notes"] = new() { Key = "Notes", Value = "<iframe allow=\"accelerometer; autoplay; encrypted-media; gyroscope; picture-in-picture\" allowfullscreen=\"\" frameborder=\"0\" height=\"315\" src=\"https://www.youtube.com/embed/3f8ovcPu6Qk\" width=\"560\"></iframe>" },
                        ["Bulleted Specs"] = new() { Key = "Bulleted Specs", Value = "<ul style=\"margin-left:50px;\">\n<li>Warranty Terms - Parts &amp; Labor 1 year </li>\n<li>Height: 1.4\"H x 16.3\"W x 10.8\"D</li>\n<li>Weight: 7.1 pounds </li>\n<li>Processor Brand : Intel&reg; </li>\n<li>Processor : Intel&reg; 4th Generation Core&trade; i7 CPU </li>\n<li>Processor Speed : 2.2GHz (with Turbo Boost up to 3.2GHz) </li>\n<li>Battery Type: 6-cell lithium-ion </li>\n<li>Display Type: HD widescreen LED Technology </li>\n<li>Screen Size (Measured Diagonally): 17.3\" </li>\n<li>Cache Memory: 6MB </li>\n<li>System Memory (RAM): 16GB, DDR3L, Expandable To 32GB </li>\n<li>Hard Drive Size &amp; Type: 1 TB SATA (5400 rpm) </li>\n<li>Optical Drive: Double-layer DVD&plusmn;RW/CD-RW </li>\n<li>Digital Media Reader or Slots: Yes, digital media card reader </li>\n<li>Graphics: NVIDIA GeForce GT 750 , 4GB DDR3 (dedicated)</li>\n<li>Built-in Webcam: Yes </li>\n<li>Networking: Built-in 10/100/1000 Gigabit Ethernet LAN (RJ-45 connector </li>\n<li>Wireless Networking: Wireless-A+B+G+N </li>\n<li>Bluetooth-Enabled: Yes </li>\n<li>Audio Speakers: Internal Optimized Dolby enhancements </li>\n<li>USB 2.0 Ports: 2 USB 3.0; 2 USB 2.0 </li>\n<li>Pointing Device: Multigesture touchpad </li>\n<li>HDMI Output: Yes </li>\n<li>Blu-ray Player: No </li>\n<li>Operating System Platform: Windows </li>\n<li>Operating System: Windows 8.1 </li>\n<li>ENERGY STAR Certified: Yes </li>\n<li>Gaming Series: Yes </li>\n<li>UPC: 887899327704</li>\n</ul>" },
                        ["Color"] = new() { Key = "Color", Value = "Black" },
                        ["Is Digital Download"] = new() { Key = "Is Digital Download", Value = "N" },
                        ["Model #"] = new() { Key = "Model #", Value = "V3-772G-9850" },
                        ["Details"] = new() { Key = "Details", Value = "• Acer Aspire 17.3\" Laptop\n• 16GB Memory \n• 1TB Hard Drive\n• 6-cell lithium-ion battery\n• AC power cord, AC power adapter\n• Owner's manual" },
                    }.SerializeAttributesDictionary(),
                });
                context.SaveUnitOfWork();
            }
            if (context?.Products?.Any(x => x.CustomKey == "ADIDCS6AC") == false)
            {
                context.Products.Add(new()
                {
                    // Last Updated from Sales Imported data 2019-04-12
                    CustomKey                                   = "ADIDCS6AC",
                    Active                                      = true,
                    Name                                        = "Adobe® Acrobat XI Standard",
                    Description                                 = "Adobe Acrobat XI Standard Windows (Download). All-new Adobe Acrobat Pro DC with Adobe Document Cloud services is the complete PDF workflow solution for today’s mobile, connected world. Acrobat desktop software is now combined with the new Acrobat DC mobile app and Document Cloud services helps you meet end-user demand for mobile solutions, let your organizaton build smarter document workflows, and help ensure document security across devices. From Adobe, the leader in secure digital documents for over 20 years.",
                    SeoKeywords                                 = "Adobe, CS6, Acrobat",
                    SeoUrl                                      = "adobe-CS6-acrobat-XI-professional",
                    SeoPageTitle                                = "Adobe CS6 Acrobat XI Professional | Clarity",
                    SeoDescription                              = "All-new Adobe Acrobat Pro DC with Adobe Document Cloud services is the complete PDF workflow solution for today’s mobile, connected world.",
                    WeightUnitOfMeasure                         = "lbs",
                    WidthUnitOfMeasure                          = "in",
                    DepthUnitOfMeasure                          = "in",
                    HeightUnitOfMeasure                         = "in",
                    ManufacturerPartNumber                      = "78347687",
                    ShortDescription                            = "Adobe Acrobat XI Standard Windows (Download). All-new Adobe Acrobat Pro DC with Adobe Document Cloud services is the complete PDF workflow solution for today’s mobile, connected world. From Adobe, the leader in secure digital documents for over 20 years.",
                    BrandName                                   = "Adobe®",
                    PriceBase                                   = 279m,
                    PriceSale                                   = 279m,
                    IsVisible                                   = true,
                    IsTaxable                                   = true,
                    IsUnlimitedStock                            = true,
                    AllowBackOrder                              = true,
                    UnitOfMeasure                               = "file",
                    KitBaseQuantityPriceMultiplier              = 1,
                    NothingToShip                               = true,
                    JsonAttributes = new SerializableAttributesDictionary
                    {
                        ["Bulleted Specs"] = new() { Key = "Bulleted Specs", Value = "Adobe Acrobat: Model 65257401 \n\nFeatures \nCombine files Keep materials together. Combine and arrange documents, spreadsheets, emails and more in one PDF file. \nScan to PDF Turn your paper documents into searchable or editable PDFs. Copy and paste the text inside to reuse elsewhere. \nStandardize routine PDF tasks Create PDFs the same way every time. Use actions to guide you through a series of steps. \nProtect your PDFs Share with confidence. Keep others from copying and editing the information contained in your PDFs. \nCreate fillable forms Turn existing paper, Word or PDF forms into digital forms that are easy to fill or sign. \nAccess tools from anywhere Access PDF tools and your most recent files in the office, on your home computer or on your mobile device. \n\nSpecifications \nName Acrobat Pro XI \nVersion Professional \nOperating Systems Supported Windows \nPackaging Retail \n\nSystem Requirements \n1.5 GHz or faster processor \nMicrosoft Windows Server 2008 R2 (64 bit), 2012 (64 bit), or 2012 R2 (64 bit); Windows 7 (32 bit and 64 bit); or Windows 8 (32 bit and 64 bit) \n1 GB of RAM \n4.5 GB of available hard-disk space \n1024 x 768 screen resolution \nInternet Explorer 8, 9, 10 (Windows 8 minimum), or 11; Firefox (ESR) \nVideo hardware acceleration (optional)" },
                        ["Is Digital Download"] = new() { Key = "Is Digital Download", Value = "Y" },
                        ["Download URL"] = new() { Key = "Download URL", Value = "http://cvilnk.com/Collateral/eCommerce_Features-Overview_2014.pdf" },
                        ["Model #"] = new() { Key = "Model #", Value = "XI" },
                        ["Details"] = new() { Key = "Details", Value = "• Turn scanned paper documents into instantly editable PDFs\n• Edit PDFs faster with full-page paragraph reflow \n• Smart bullet updates\n• Export PDFs to Word, Excel, or PowerPoint" },
                    }.SerializeAttributesDictionary(),
                    TypeID = context.ProductTypes.Where(pt => pt.Active && pt.Name == "General").Select(pt => pt.ID).First(),
                    StatusID = context.ProductStatuses.Where(pt => pt.Active && pt.Name == "Normal").Select(pt => pt.ID).First(),
                    IsFreeShipping = true,
                    Weight = 0,
                    CreatedDate = createdDate,
                    PackageID = context.Packages.Where(p => p.Name == "Default").Select(p => p.ID).FirstOrDefault(),
                    Categories = new List<ProductCategory>
                    {
                        new()
                        {
                            SlaveID = context.Categories.Where(c => c.CustomKey == "CAT-1").Select(c => c.ID).First(),
                            CreatedDate = createdDate,
                            Active = true,
                        },
                    },
                    ProductInventoryLocationSections = new[]
                    {
                        new ProductInventoryLocationSection
                        {
                            Active = true,
                            CreatedDate = createdDate,
                            SlaveID = context.InventoryLocationSections.First(x => x.CustomKey == "WAREHOUSE-1-Section-2").ID,
                            Quantity = 5,
                        },
                    },
                });
                context.SaveUnitOfWork();
            }
            if (context?.Products?.Any(x => x.CustomKey == "ADIDCS6W-ST") == false)
            {
                context.Products.Add(new()
                {
                    TypeID = context.ProductTypes.Where(pt => pt.Active && pt.Name == "General").Select(pt => pt.ID).First(),
                    StatusID = context.ProductStatuses.Where(pt => pt.Active && pt.Name == "Normal").Select(pt => pt.ID).First(),
                    CustomKey = "ADIDCS6W-ST",
                    Name = "Fury",
                    ShortDescription = "April, 1945. As the Allies make their final push in the European Theater, a battle-hardened army sergeant named Wardaddy (Brad Pitt) commands a Sherman tank and her five-man crew on a deadly mission behind enemy lines.",
                    Description = "April, 1945. As the Allies make their final push in the European Theater, a battle-hardened army sergeant named Wardaddy (Brad Pitt) commands a Sherman tank and her five-man crew on a deadly mission behind enemy lines.",
                    SeoUrl = "Fury",
                    PriceBase = 12.99m,
                    PriceSale = 12.99m,
                    IsVisible = true,
                    IsDiscontinued = false,
                    IsTaxable = true,
                    IsFreeShipping = true,
                    Weight = 0,
                    AvailableStartDate = null,
                    AvailableEndDate = null,
                    CreatedDate = createdDate,
                    UpdatedDate = null,
                    Active = true,
                    SeoKeywords = "Fury,Keywords",
                    SeoDescription = "Fury,Description",
                    SeoPageTitle = "Fury",
                    PackageID = context.Packages.Where(p => p.Name == "Default").Select(p => p.ID).FirstOrDefault(),
                    Categories = new List<ProductCategory>
                    {
                        new()
                        {
                            SlaveID = context.Categories.Where(c => c.CustomKey == "CAT-1").Select(c => c.ID).First(),
                            CreatedDate = createdDate,
                            Active = true,
                        },
                    },
                    ProductInventoryLocationSections = new[]
                    {
                        new ProductInventoryLocationSection
                        {
                            Active = true,
                            CreatedDate = createdDate,
                            SlaveID = context.InventoryLocationSections.First(x => x.CustomKey == "WAREHOUSE-1-Section-2").ID,
                            Quantity = 5,
                        },
                    },
                });
                context.SaveUnitOfWork();
            }
            if (context?.Products?.Any(x => x.CustomKey == "ADIDCS6W") == false)
            {
                context.Products.Add(new()
                {
                    TypeID = context.ProductTypes.Where(pt => pt.Active && pt.Name == "General").Select(pt => pt.ID).First(),
                    StatusID = context.ProductStatuses.Where(pt => pt.Active && pt.Name == "Normal").Select(pt => pt.ID).First(),
                    CustomKey = "ADIDCS6W",
                    Name = "Dracula Untold",
                    ShortDescription = "Luke Evans (Fast & Furious 6, The Hobbit series) stars in the epic action adventure, Dracula Untold, the origin story of the man who became Dracula.",
                    Description = "Luke Evans (Fast & Furious 6, The Hobbit series) stars in the epic action adventure, Dracula Untold, the origin story of the man who became Dracula.",
                    SeoUrl = "Dracula-Untold",
                    PriceBase = 6.99m,
                    PriceSale = 6.99m,
                    IsVisible = true,
                    IsDiscontinued = false,
                    IsTaxable = true,
                    IsFreeShipping = true,
                    Weight = 0,
                    AvailableStartDate = null,
                    AvailableEndDate = null,
                    CreatedDate = createdDate,
                    UpdatedDate = null,
                    Active = true,
                    SeoKeywords = "Dracula,Keywords",
                    SeoDescription = "Dracula,Description",
                    SeoPageTitle = "Dracula",
                    PackageID = context.Packages.Where(p => p.Name == "Default").Select(p => p.ID).FirstOrDefault(),
                    Categories = new List<ProductCategory>
                    {
                        new()
                        {
                            SlaveID = context.Categories.Where(c => c.CustomKey == "CAT-1").Select(c => c.ID).First(),
                            CreatedDate = createdDate,
                            Active = true,
                        },
                    },
                    ProductInventoryLocationSections = new[]
                    {
                        new ProductInventoryLocationSection
                        {
                            Active = true,
                            CreatedDate = createdDate,
                            SlaveID = context.InventoryLocationSections.First(x => x.CustomKey == "WAREHOUSE-1-Section-2").ID,
                            Quantity = 5,
                        },
                    },
                });
                context.SaveUnitOfWork();
            }
            if (context?.Products?.Any(x => x.CustomKey == "6542456") == false)
            {
                context.Products.Add(new()
                {
                    TypeID = context.ProductTypes.Where(pt => pt.Active && pt.Name == "General").Select(pt => pt.ID).First(),
                    StatusID = context.ProductStatuses.Where(pt => pt.Active && pt.Name == "Normal").Select(pt => pt.ID).First(),
                    CustomKey = "6542456",
                    Name = "The Legend Of Hercules",
                    ShortDescription = "When the king banishes him, Hercules gathers an army to rise against him. Slowly becoming aware of his true origins as the son of Zeus, Hercules harnesses his demi-god powers in the fight against tyranny in this action-filled epic. Starring Kellan Lu",
                    Description = "When the king banishes him, Hercules gathers an army to rise against him. Slowly becoming aware of his true origins as the son of Zeus, Hercules harnesses his demi-god powers in the fight against tyranny in this action-filled epic. Starring Kellan Lutz and directed by Renny Harlin.",
                    SeoUrl = "The-Legend-Of-Hercules",
                    PriceBase = 6.99m,
                    PriceSale = 6.99m,
                    IsVisible = true,
                    IsDiscontinued = false,
                    IsTaxable = true,
                    IsFreeShipping = true,
                    Weight = 0,
                    AvailableStartDate = null,
                    AvailableEndDate = null,
                    CreatedDate = createdDate,
                    UpdatedDate = null,
                    Active = true,
                    SeoKeywords = "The Legend Of Hercules,Keywords",
                    SeoDescription = "The Legend Of Hercules,Description",
                    SeoPageTitle = "The Legend Of Hercules",
                    PackageID = context.Packages.Where(p => p.Name == "Default").Select(p => p.ID).FirstOrDefault(),
                    Categories = new List<ProductCategory>
                    {
                        new()
                        {
                            SlaveID = context.Categories.Where(c => c.CustomKey == "CAT-1").Select(c => c.ID).First(),
                            CreatedDate = createdDate,
                            Active = true,
                        },
                    },
                    ProductInventoryLocationSections = new[]
                    {
                        new ProductInventoryLocationSection
                        {
                            Active = true,
                            CreatedDate = createdDate,
                            SlaveID = context.InventoryLocationSections.First(x => x.CustomKey == "WAREHOUSE-1-Section-2").ID,
                            Quantity = 5,
                        },
                    },
                });
                context.SaveUnitOfWork();
            }
            if (context?.Products?.Any(x => x.CustomKey == "GR35466") == false)
            {
                context.Products.Add(new()
                {
                    // Last Updated from Sales Imported data 2019-04-15
                    IsFreeShipping                  = false,
                    IsDiscontinued                  = false,
                    IsEligibleForReturn             = false,
                    NothingToShip                   = false,
                    ShippingLeadTimeIsCalendarDays  = false,
                    Active                          = true,
                    IsVisible                       = true,
                    IsTaxable                       = true,
                    IsUnlimitedStock                = true,
                    AllowBackOrder                  = true,
                    KitBaseQuantityPriceMultiplier  = 1m,
                    Weight                          = 1.8m,
                    Height                          = 6m,
                    Depth                           = 7m,
                    Width                           = 8m,
                    TotalPurchasedQuantity          = 20m,
                    PriceBase                       = 94.95m,
                    PriceSale                       = 94.95m,
                    TotalPurchasedAmount            = 1899m,
                    SeoPageTitle                    = "Allen-Edmonds Leather Shoe Care Kit | Clarity",
                    BrandName                       = "Allen-Edmonds®",
                    Name                            = "Allen-Edmonds® Leather Shoe Care Kit",
                    SeoUrl                          = "allen-edmonds-leather-shoe-care-kit",
                    CustomKey                       = "GR35466",
                    ManufacturerPartNumber          = "GR35466",
                    WidthUnitOfMeasure              = "in",
                    DepthUnitOfMeasure              = "in",
                    HeightUnitOfMeasure             = "in",
                    UnitOfMeasure                   = "kit",
                    WeightUnitOfMeasure             = "lbs",
                    Description                     = "Make sure your shoes never lose their shine with this Allen-Edmonds™ shoe care kit. With a couple of buffs here and some elbow grease there, you'll have your favorite pair of shoes back to their original luster in no time!",
                    ShortDescription                = "Make sure your shoes never lose their shine with this Allen-Edmonds™ shoe care kit. With a couple of buffs here and some elbow grease there, you'll have your favorite pair of shoes back to their original luster in no time!",
                    SeoDescription                  = "Make sure your shoes never lose their shine with this Allen-Edmonds™ shoe care kit. With a couple of buffs you'll have your favorite pair of shoes back to their original luster!",
                    SeoKeywords                     = "shoe shine kit, shoe care kit",
                    JsonAttributes = new SerializableAttributesDictionary
                    {
                        ["Is Digital Download"] = new() { Key = "Is Digital Download", Value = "N" },
                        ["Model #"] = new() { Key = "Model #", Value = "GR35466" },
                        ["Bulleted Specs"] = new()
                        {
                            Key = "Bulleted Specs",
                            Value = "<ul>\r\n<li>Premium leather case</li>\r\n"
                                + "<li>2 Wood-backed Horsehair Shoe Shine Brushes</li>\r\n"
                                + "<li>2 Horsehair Polish Daubers</li>\r\n"
                                + "<li>2 Cotton Flannel Polishing Cloths</li>\r\n"
                                + "<li>Allen Edmonds Conditioner/Cleaner</li>\r\n"
                                + "<li>Carnauba Wax Shoe Polish (black and brown)</li>\r\n"
                                + "<li>Made in the USA</li>\r\n</ul>",
                        },
                        ["Details"] = new()
                        {
                            Key = "Details",
                            Value = "<ul>\r\n<li>Premium leather case</li>\r\n"
                                + "<li>2 Wood-backed Horsehair Shoe Shine Brushes</li>\r\n"
                                + "<li>2 Horsehair Polish Daubers</li>\r\n"
                                + "<li>2 Cotton Flannel Polishing Cloths</li>\r\n"
                                + "<li>Allen Edmonds Conditioner/Cleaner</li>\r\n"
                                + "<li>Carnauba Wax Shoe Polish (black and brown)</li>\r\n"
                                + "<li>Made in the USA</li>\r\n</ul>",
                        },
                    }.SerializeAttributesDictionary(),
                    TypeID = context.ProductTypes.Where(pt => pt.Active && pt.Name == "General").Select(pt => pt.ID).First(),
                    StatusID = context.ProductStatuses.Where(pt => pt.Active && pt.Name == "Normal").Select(pt => pt.ID).First(),
                    CreatedDate = createdDate,
                    PackageID = context.Packages.Where(p => p.Name == "Default").Select(p => p.ID).FirstOrDefault(),
                    Categories = new List<ProductCategory>
                    {
                        new()
                        {
                            SlaveID = context.Categories.Where(c => c.CustomKey == "CAT-2").Select(c => c.ID).First(),
                            CreatedDate = createdDate,
                            Active = true,
                        },
                    },
                    ProductInventoryLocationSections = new[]
                    {
                        new ProductInventoryLocationSection
                        {
                            Active = true,
                            CreatedDate = createdDate,
                            SlaveID = context.InventoryLocationSections.First(x => x.CustomKey == "WAREHOUSE-1-Section-2").ID,
                            Quantity = 5,
                        },
                    },
                });
                context.SaveUnitOfWork();
            }
            if (context?.Products?.Any(x => x.CustomKey == "DSW3456976") == false)
            {
                context.Products.Add(new()
                {
                    // Last Updated from Sales Imported data 2019-04-15
                    IsFreeShipping                   = false,
                    IsDiscontinued                   = false,
                    IsEligibleForReturn              = false,
                    NothingToShip                    = false,
                    ShippingLeadTimeIsCalendarDays   = false,
                    Active                           = true,
                    IsVisible                        = true,
                    IsTaxable                        = true,
                    IsUnlimitedStock                 = true,
                    AllowBackOrder                   = true,
                    KitBaseQuantityPriceMultiplier   = 1m,
                    Weight                           = 1.125m,
                    Height                           = 2.5m,
                    Width                            = 15m,
                    TotalPurchasedQuantity           = 18m,
                    PriceBase                        = 24.99m,
                    PriceSale                        = 24.99m,
                    TotalPurchasedAmount             = 449.82m,
                    SeoDescription                   = "A great alternative to traditional shoe trees. A flexible steel spring means one size fits all. Perfect for all shoe styles and ideal for pumps and loafers.",
                    CustomKey                        = "DSW3456976",
                    ManufacturerPartNumber           = "DSW3456976",
                    WidthUnitOfMeasure               = "in",
                    DepthUnitOfMeasure               = "in",
                    HeightUnitOfMeasure              = "in",
                    WeightUnitOfMeasure              = "lbs",
                    UnitOfMeasure                    = "pair",
                    SeoKeywords                      = "shoe tree",
                    SeoPageTitle                     = "Woodlore Men's Shoemate Shoe Trees in Natural Cedar Finish | Clarity",
                    ShortDescription                 = "Woodlore Men's Shoemate Shoe Trees in Natural Cedar Finish. A great alternative to traditional shoe trees. A flexible steel spring means one size fits all.",
                    Description                      = "Woodlore Men's Shoemate Shoe Trees in Natural Cedar Finish. A great alternative to traditional shoe trees. A flexible steel spring means one size fits all. Perfect for all shoe styles and ideal for pumps and loafers.",
                    BrandName                        = "Woodlore®",
                    Name                             = "Woodlore® Men's Shoe Trees",
                    SeoUrl                           = "woodlore-mens-cedar-shoe-tree-set",
                    JsonAttributes = new SerializableAttributesDictionary
                    {
                        ["Is Digital Download"] = new() { Key = "Is Digital Download", Value = "N" },
                        ["Model #"] = new() { Key = "Model #", Value = "DSW3456976" },
                        ["Size"] = new() { Key = "Size", Value = "7,8,9,10,11,12,13" },
                        ["Material"] = new() { Key = "Material", Value = "Cedar" },
                        ["Color"] = new() { Key = "Color", Value = "Natural" },
                        ["Bulleted Specs"] = new()
                        {
                            Key = "Bulleted Specs",
                            Value = "<ul>\r\n<li>Weights & Dimensions</li>\r\n<li>Overall: 2.5\" W x 15\" D</li>\r\n<li>Overall Product Weight: 1.125lbs</li>\r\n<li>Features</li>\r\n<li>Finish: Natural</li>\r\n<li>Product Type: Shoe tree</li>\r\n<li>Material: Wood</li>\r\n<li>Material Details: Cedar</li>\r\n<li>Hand Crafted: No</li>\r\n<li>Pairs of Shoes Capacity: 1</li>\r\n<li>Stackable: No</li>\r\n<li>Folding: No</li>\r\n<li>Revolving: No</li>\r\n<li>Shoe Cabinet Included: No</li>\r\n<li>Eco-Friendly: No</li>\r\n<li>Country of Manufacture: United States</li>\r\n</ul>",
                        },
                        ["Details"] = new()
                        {
                            Key = "Details",
                            Value = "<ul>\r\n<li>Aromatic cedar</li>\r\n<li>Repel insects and keeps closets and drawers smelling naturally fresh</li>\r\n<li>Shoe trees prolong the life of shoes by absorbing moisture from them</li>\r\n<li>Maintain the shape of the shoe</li>\r\n<li>Extend the life of a pair of shoes by as much as 30%</li>\r\n<li>Light sanding of the cedar rejuvenates the natural aromatic scent</li>\r\n</ul>",
                        },
                    }.SerializeAttributesDictionary(),
                    TypeID = context.ProductTypes.Where(pt => pt.Active && pt.Name == "General").Select(pt => pt.ID).First(),
                    StatusID = context.ProductStatuses.Where(pt => pt.Active && pt.Name == "Normal").Select(pt => pt.ID).First(),
                    CreatedDate = createdDate,
                    PackageID = context.Packages.Where(p => p.Name == "Default").Select(p => p.ID).FirstOrDefault(),
                    Categories = new List<ProductCategory>
                    {
                        new()
                        {
                            SlaveID = context.Categories.Where(c => c.CustomKey == "CAT-2").Select(c => c.ID).First(),
                            CreatedDate = createdDate,
                            Active = true,
                        },
                    },
                    ProductInventoryLocationSections = new[]
                    {
                        new ProductInventoryLocationSection
                        {
                            Active = true,
                            CreatedDate = createdDate,
                            SlaveID = context.InventoryLocationSections.First(x => x.CustomKey == "WAREHOUSE-1-Section-2").ID,
                            Quantity = 5,
                        },
                    },
                });
                context.SaveUnitOfWork();
            }
            if (context?.Products?.Any(x => x.CustomKey == "6009686") == false)
            {
                context.Products.Add(new()
                {
                    // Last Updated from Sales Imported data 2019-04-17
                    IsFreeShipping                 = false,
                    IsDiscontinued                 = false,
                    IsEligibleForReturn            = false,
                    NothingToShip                  = false,
                    ShippingLeadTimeIsCalendarDays = false,
                    Active                         = true,
                    IsVisible                      = true,
                    IsTaxable                      = true,
                    IsUnlimitedStock               = true,
                    AllowBackOrder                 = true,
                    KitBaseQuantityPriceMultiplier = 1m,
                    Width                          = 2.79m,
                    Depth                          = 0.3m,
                    Height                         = 5.65m,
                    Weight                         = 6.4m,
                    PriceBase                      = 1099.99m,
                    PriceSale                      = 1099.99m,
                    CustomKey                      = "6009686",
                    ManufacturerPartNumber         = "6009686",
                    WeightUnitOfMeasure            = "oz",
                    Name                           = "Apple iPhone X 256GB",
                    SeoPageTitle                   = "Apple iPhone X, 256GB in Space Grey | Clarity",
                    BrandName                      = "Apple®",
                    SeoUrl                         = "apple-iphone-x-256gb-space-grey",
                    UnitOfMeasure                  = "EACH",
                    WidthUnitOfMeasure             = "in",
                    DepthUnitOfMeasure             = "in",
                    HeightUnitOfMeasure            = "in",
                    ShortDescription               = "iPhone X features an all-screen design with a 5.8-inch Super Retina HD display with HDR and True Tone.¹ Designed with the most durable glass ever in a smartphone and a surgical grade stainless steel band. Charges wirelessly.² Resists water and dust.³",
                    Description                    = "iPhone X features an all-screen design with a 5.8-inch Super Retina HD display with HDR and True Tone.¹ Designed with the most durable glass ever in a smartphone and a surgical grade stainless steel band. Charges wirelessly.² Resists water and dust.³ 12MP dual cameras with dual optical image stabilization for great low-light photos. TrueDepth camera with Portrait selfies and new Portrait Lighting.4 Face ID lets you unlock and use Apple Pay with just a glance. Powered by A11 Bionic, the most powerful and smartest chip ever in a smartphone. Supports augmented reality experiences in games and apps. With iPhone X, the next era of iPhone has begun.",
                    SeoDescription                 = "This is the best phone to ever come out of Apple. The face recognition and camera's are the bomb!",
                    JsonAttributes = new SerializableAttributesDictionary
                    {
                        ["Is Digital Download"] = new() { Key = "Is Digital Download", Value = "N" },
                        ["Model #"] = new() { Key = "Model #", Value = "MQA62LL/A" },
                        ["Material"] = new() { Key = "Material", Value = "Composite" },
                        ["Color"] = new() { Key = "Color", Value = "Silver, Space Grey" },
                        ["Notes"] = new()
                        {
                            Key = "Color",
                            Value = "*NOTE:\r\nThis phone can also be leased through your Electronics store, such as Best Buy or directly through your carrier like AT&T, Verizon or Sprint.",
                        },
                        ["Bulleted Specs"] = new()
                        {
                            Key = "Bulleted Specs",
                            Value = "Features\r\nUnlocked No\r\nText/Instant Messaging Yes\r\nEmail Capable Yes\r\nKeyboard Type Touch Screen\r\nMedia Card Slot None\r\nMobile Hotspot Capability Yes\r\nBuilt-In GPS Yes\r\nSensors Accelerometer, Ambient light sensor, Barometer, Gyro sensor, Proximity sensor\r\nMaximum Depth Of Water Resistance 0 feet\r\nVoice Activated Yes\r\nMobile Payment Service Supported Apple Pay\r\nWireless Charging Yes\r\nSocial Media and Messaging Services Messages, Facetime, MMS\r\nDust Resistant Yes\r\nStylus Dock None\r\nHeadphone Jack No\r\n\r\nDisplay\r\nTouch Screen Yes\r\n\r\nImaging\r\nIntegrated Camera Yes - Front and Back\r\nRecording Resolution 3840 x 2160 (4K)\nFrame Rate 60 frames per second\r\n\r\nPower\r\nBattery Type Lithium-ion\r\n\r\nCompatibility\r\nBluetooth EnabledYes\r\nNFC TechnologyYes\r\nCharging Interface(s) Lightning\r\nWireless Charging Standard Qi\r\nModel Family Apple iPhone X\r\nExpandable Memory up to 0 gigabytes\r\nSIM Card Size Nano SIM\r\nSIM Card Slots Single SIM\r\n\r\nGeneral\r\nNo-Contract No\r\nData Plan Required Yes\r\nDevice Manufacturer Apple\r\nAdditional Hardware Included EarPods with Lightning Connector, Lightning to 3.5 mm Headphone Jack Adapter, Lightning to USB Cable, USB Power Adapter\r\nPhone Style Smartphone\r\nColor Silver\r\nColor Category Silver\r\nModel Number MQA62LL/A",
                        },
                        ["Details"] = new()
                        {
                            Key = "Details",
                            Value = "Apple iPhone X 256GB\r\nEarPods with Lightning Connector\r\nLightning to 3.5 mm Headphone Jack Adapter\r\nLightning to USB Cable\r\nUSB Power Adapter\r\nDocumentation",
                        },
                    }.SerializeAttributesDictionary(),
                    TypeID = context.ProductTypes.Where(pt => pt.Active && pt.Name == "General").Select(pt => pt.ID).First(),
                    StatusID = context.ProductStatuses.Where(pt => pt.Active && pt.Name == "Normal").Select(pt => pt.ID).First(),
                    CreatedDate = createdDate,
                    PackageID = context.Packages.Where(p => p.Name == "Default").Select(p => p.ID).FirstOrDefault(),
                    Categories = new List<ProductCategory>
                    {
                        new()
                        {
                            SlaveID = context.Categories.Where(c => c.CustomKey == "CAT-2").Select(c => c.ID).First(),
                            CreatedDate = createdDate,
                            Active = true,
                        },
                    },
                    ProductInventoryLocationSections = new[]
                    {
                        new ProductInventoryLocationSection
                        {
                            Active = true,
                            CreatedDate = createdDate,
                            SlaveID = context.InventoryLocationSections.First(x => x.CustomKey == "WAREHOUSE-1-Section-2").ID,
                            Quantity = 5,
                        },
                    },
                });
                context.SaveUnitOfWork();
            }
            if (context?.Products?.Any(x => x.CustomKey == "AC4.1.5") == false)
            {
                context.Products.Add(new()
                {
                    TypeID = context.ProductTypes.Where(pt => pt.Active && pt.Name == "General").Select(pt => pt.ID).First(),
                    StatusID = context.ProductStatuses.Where(pt => pt.Active && pt.Name == "Normal").Select(pt => pt.ID).First(),
                    CustomKey = "AC4.1.5",
                    Name = "Team of Rivals: The Political Genius of Abraham Lincoln",
                    ShortDescription = "Acclaimed historian Doris Kearns Goodwin illuminates Lincoln's political genius in this highly original work, as the one-term congressman and prairie lawyer rises from obscurity to prevail over three gifted rivals of national reputation to become pre",
                    Description = "Acclaimed historian Doris Kearns Goodwin illuminates Lincoln's political genius in this highly original work, as the one-term congressman and prairie lawyer rises from obscurity to prevail over three gifted rivals of national reputation to become president.",
                    SeoUrl = "Team-of-Rivals-The-Political-Genius-of-Abraham-Lincoln",
                    PriceBase = 14.28m,
                    PriceSale = 14.28m,
                    IsVisible = true,
                    IsDiscontinued = false,
                    IsTaxable = true,
                    IsFreeShipping = true,
                    Weight = 0,
                    AvailableStartDate = null,
                    AvailableEndDate = null,
                    CreatedDate = createdDate,
                    UpdatedDate = null,
                    Active = true,
                    SeoKeywords = "Team of Rivals: The Political Genius of Abraham Lincoln,Keywords",
                    SeoDescription = "Team of Rivals: The Political Genius of Abraham Lincoln,Description",
                    SeoPageTitle = "Team of Rivals: The Political Genius of Abraham Lincoln",
                    PackageID = context.Packages.Where(p => p.Name == "Default").Select(p => p.ID).FirstOrDefault(),
                    Categories = new List<ProductCategory>
                    {
                        new()
                        {
                            SlaveID = context.Categories.Where(c => c.CustomKey == "CAT-2").Select(c => c.ID).First(),
                            CreatedDate = createdDate,
                            Active = true,
                        },
                    },
                    ProductInventoryLocationSections = new[]
                    {
                        new ProductInventoryLocationSection
                        {
                            Active = true,
                            CreatedDate = createdDate,
                            SlaveID = context.InventoryLocationSections.First(x => x.CustomKey == "WAREHOUSE-1-Section-2").ID,
                            Quantity = 5,
                        },
                    },
                });
                context.SaveUnitOfWork();
            }
            if (context?.Products?.Any(x => x.CustomKey == "AFC10.1.2") == false)
            {
                context.Products.Add(new()
                {
                    TypeID = context.ProductTypes.Where(pt => pt.Active && pt.Name == "General").Select(pt => pt.ID).First(),
                    StatusID = context.ProductStatuses.Where(pt => pt.Active && pt.Name == "Normal").Select(pt => pt.ID).First(),
                    CustomKey = "AFC10.1.2",
                    Name = "The Amazing Adventures of Kavalier & Clay",
                    ShortDescription = "The beloved, award-winning The Amazing Adventures of Kavalier & Clay, a Michael Chabon masterwork, is the American epic of two boy geniuses named Joe Kavalier and Sammy Clay. Now with special bonus material by Michael Chabon.",
                    Description = "The beloved, award-winning The Amazing Adventures of Kavalier & Clay, a Michael Chabon masterwork, is the American epic of two boy geniuses named Joe Kavalier and Sammy Clay. Now with special bonus material by Michael Chabon.",
                    SeoUrl = "The-Amazing-Adventures-of-Kavalier-And-Clay",
                    PriceBase = 9.72m,
                    PriceSale = 9.72m,
                    IsVisible = true,
                    IsDiscontinued = false,
                    IsTaxable = true,
                    IsFreeShipping = true,
                    Weight = 0,
                    AvailableStartDate = null,
                    AvailableEndDate = null,
                    CreatedDate = createdDate,
                    UpdatedDate = null,
                    Active = true,
                    SeoKeywords = "The Amazing Adventures of Kavalier & Clay,Keywords",
                    SeoDescription = "The Amazing Adventures of Kavalier & Clay,Description",
                    SeoPageTitle = "The Amazing Adventures of Kavalier & Clay",
                    PackageID = context.Packages.Where(p => p.Name == "Default").Select(p => p.ID).FirstOrDefault(),
                    Categories = new List<ProductCategory>
                    {
                        new()
                        {
                            SlaveID = context.Categories.Where(c => c.CustomKey == "CAT-2").Select(c => c.ID).First(),
                            CreatedDate = createdDate,
                            Active = true,
                        },
                    },
                    ProductInventoryLocationSections = new[]
                    {
                        new ProductInventoryLocationSection
                        {
                            Active = true,
                            CreatedDate = createdDate,
                            SlaveID = context.InventoryLocationSections.First(x => x.CustomKey == "WAREHOUSE-1-Section-2").ID,
                            Quantity = 5,
                        },
                    },
                });
                context.SaveUnitOfWork();
            }
            if (context?.Products?.Any(x => x.CustomKey == "AM5.0.7") == false)
            {
                context.Products.Add(new()
                {
                    TypeID = context.ProductTypes.Where(pt => pt.Active && pt.Name == "General").Select(pt => pt.ID).First(),
                    StatusID = context.ProductStatuses.Where(pt => pt.Active && pt.Name == "Normal").Select(pt => pt.ID).First(),
                    CustomKey = "AM5.0.7",
                    Name = "Funko POP! Marvel: Dancing Groot Bobble Action Figure",
                    ShortDescription = "Dancing Groot Pop! Check out the other Guardians of the Galaxy figures from Funko!",
                    Description = "Dancing Groot Pop! Check out the other Guardians of the Galaxy figures from Funko!",
                    SeoUrl = "Funko-POP-Marvel-Dancing-Groot-Bobble-Action-Figure",
                    PriceBase = 7.79m,
                    PriceSale = 7.79m,
                    IsVisible = true,
                    IsDiscontinued = false,
                    IsTaxable = true,
                    IsFreeShipping = true,
                    Weight = 0,
                    AvailableStartDate = null,
                    AvailableEndDate = null,
                    CreatedDate = createdDate,
                    UpdatedDate = null,
                    Active = true,
                    SeoKeywords = "Funko POP! Marvel: Dancing Groot Bobble Action Figure,Keywords",
                    SeoDescription = "Funko POP! Marvel: Dancing Groot Bobble Action Figure,Description",
                    SeoPageTitle = "Funko POP! Marvel: Dancing Groot Bobble Action Figure",
                    PackageID = context.Packages.Where(p => p.Name == "Default").Select(p => p.ID).FirstOrDefault(),
                    Categories = new List<ProductCategory>
                    {
                        new()
                        {
                            SlaveID = context.Categories.Where(c => c.CustomKey == "CAT-3").Select(c => c.ID).First(),
                            CreatedDate = createdDate,
                            Active = true,
                        },
                    },
                    ProductInventoryLocationSections = new[]
                    {
                        new ProductInventoryLocationSection
                        {
                            Active = true,
                            CreatedDate = createdDate,
                            SlaveID = context.InventoryLocationSections.First(x => x.CustomKey == "WAREHOUSE-1-Section-2").ID,
                            Quantity = 5,
                        },
                    },
                });
                context.SaveUnitOfWork();
            }
            if (context?.Products?.Any(x => x.CustomKey == "00493-00000000") == false)
            {
                context.Products.Add(new()
                {
                    TypeID = context.ProductTypes.Where(pt => pt.Active && pt.Name == "General").Select(pt => pt.ID).First(),
                    StatusID = context.ProductStatuses.Where(pt => pt.Active && pt.Name == "Normal").Select(pt => pt.ID).First(),
                    CustomKey = "00493-00000000",
                    Name = "Vinyl Mini Dinosaurs",
                    ShortDescription = "Hard plastic / vinyl figures. Up to 2 1/2 inch. Assorted colors and styles. There are additional pictures above to show the different types. Brand new in sealed polybag.",
                    Description = "Hard plastic / vinyl figures. Up to 2 1/2 inch. Assorted colors and styles. There are additional pictures above to show the different types. Brand new in sealed polybag.",
                    SeoUrl = "Vinyl-Mini-Dinosaurs",
                    PriceBase = 9.04m,
                    PriceSale = 9.04m,
                    IsVisible = true,
                    IsDiscontinued = false,
                    IsTaxable = true,
                    IsFreeShipping = true,
                    Weight = 0,
                    AvailableStartDate = null,
                    AvailableEndDate = null,
                    CreatedDate = createdDate,
                    UpdatedDate = null,
                    Active = true,
                    SeoKeywords = "Vinyl Mini Dinosaurs,Keywords",
                    SeoDescription = "Vinyl Mini Dinosaurs,Description",
                    SeoPageTitle = "Vinyl Mini Dinosaurs",
                    PackageID = context.Packages.Where(p => p.Name == "Default").Select(p => p.ID).FirstOrDefault(),
                    Categories = new List<ProductCategory>
                    {
                        new()
                        {
                            SlaveID = context.Categories.Where(c => c.CustomKey == "CAT-3").Select(c => c.ID).First(),
                            CreatedDate = createdDate,
                            Active = true,
                        },
                    },
                    ProductInventoryLocationSections = new[]
                    {
                        new ProductInventoryLocationSection
                        {
                            Active = true,
                            CreatedDate = createdDate,
                            SlaveID = context.InventoryLocationSections.First(x => x.CustomKey == "WAREHOUSE-1-Section-2").ID,
                            Quantity = 5,
                        },
                    },
                });
                context.SaveUnitOfWork();
            }
            if (context?.Products?.Any(x => x.CustomKey == "AC1165482") == false)
            {
                context.Products.Add(new()
                {
                    TypeID = context.ProductTypes.Where(pt => pt.Active && pt.Name == "General").Select(pt => pt.ID).First(),
                    StatusID = context.ProductStatuses.Where(pt => pt.Active && pt.Name == "Normal").Select(pt => pt.ID).First(),
                    CustomKey = "AC1165482",
                    Name = "Despicable Me 2 The Minions Role Figure Display Toy PVC 4Pcs Set",
                    ShortDescription = "Made of safe material. Mini size and looks very realistic. Good decoration to your table, computer table. Nice gift for Movie fans",
                    Description = "Made of safe material. Mini size and looks very realistic. Good decoration to your table, computer table. Nice gift for Movie fans",
                    SeoUrl = "Despicable-Me-2-The-Minions-Role-Figures",
                    PriceBase = 3.88m,
                    PriceSale = 3.88m,
                    IsVisible = true,
                    IsDiscontinued = false,
                    IsTaxable = true,
                    IsFreeShipping = true,
                    Weight = 0,
                    AvailableStartDate = null,
                    AvailableEndDate = null,
                    CreatedDate = createdDate,
                    UpdatedDate = null,
                    Active = true,
                    SeoKeywords = "Despicable Me 2 The Minions Role Figure Display Toy PVC 4Pcs Set,Keywords",
                    SeoDescription = "Despicable Me 2 The Minions Role Figure Display Toy PVC 4Pcs Set,Description",
                    SeoPageTitle = "Despicable Me 2 The Minions Role Figure Display Toy PVC 4Pcs Set",
                    PackageID = context.Packages.Where(p => p.Name == "Default").Select(p => p.ID).FirstOrDefault(),
                    Categories = new List<ProductCategory>
                    {
                        new()
                        {
                            SlaveID = context.Categories.Where(c => c.CustomKey == "CAT-3").Select(c => c.ID).First(),
                            CreatedDate = createdDate,
                            Active = true,
                        },
                    },
                    ProductInventoryLocationSections = new[]
                    {
                        new ProductInventoryLocationSection
                        {
                            Active = true,
                            CreatedDate = createdDate,
                            SlaveID = context.InventoryLocationSections.First(x => x.CustomKey == "WAREHOUSE-1-Section-2").ID,
                            Quantity = 5,
                        },
                    },
                });
                context.SaveUnitOfWork();
            }
            if (context?.Products?.Any(x => x.CustomKey == "811943") == false)
            {
                context.Products.Add(new()
                {
                    // Last Updated from Sales Imported data 2019-04-15
                    IsDiscontinued                  = false,
                    IsEligibleForReturn             = false,
                    IsFreeShipping                  = false,
                    NothingToShip                   = false,
                    ShippingLeadTimeIsCalendarDays  = false,
                    Active                          = true,
                    AllowBackOrder                  = true,
                    IsTaxable                       = true,
                    IsUnlimitedStock                = true,
                    IsVisible                       = true,
                    KitBaseQuantityPriceMultiplier  = 1m,
                    PriceBase                       = 4.99m,
                    PriceSale                       = 4.99m,
                    TotalPurchasedQuantity          = 22m,
                    TotalPurchasedAmount            = 109.78m,
                    CustomKey                       = "811943",
                    ManufacturerPartNumber          = "811943",
                    SeoPageTitle                    = "BIC Mechanical Pencils | Clarity",
                    BrandName                       = "BIC®",
                    Name                            = "BIC® Mechanical Pencils, 0.7 mm, Black Barrel, Pack Of 12",
                    SeoUrl                          = "BIC-mechanical-pencils-pack-12",
                    DepthUnitOfMeasure              = "in",
                    HeightUnitOfMeasure             = "in",
                    WidthUnitOfMeasure              = "in",
                    WeightUnitOfMeasure             = "lbs",
                    SeoKeywords                     = "mechanical pencils",
                    SeoDescription                  = "One of the world's top-selling mechanical pencils. The smooth writing lead does not smudge and erases cleanly, and the lead advances quickly and easily.",
                    ShortDescription                = "One of the world's top-selling mechanical pencils. The smooth writing lead does not smudge and erases cleanly, and the lead advances quickly and easily.",
                    Description                     = "One of the world's top-selling mechanical pencils. The smooth writing lead does not smudge and erases cleanly, and the lead advances quickly and easily. One mechanical pencil equals 2-1/2 wood case pencils, giving you more for your money. Mechanism: Mechanical; Pencil Type: Mechanical; Lead Color(s): Black; Barrel Color(s): Assorted.",
                    UnitOfMeasure                   = "pack",
                    JsonAttributes = new SerializableAttributesDictionary
                    {
                        ["Color"] = new() { Key = "Color", Value = "Multi-colored" },
                        ["Is Digital Download"] = new() { Key = "Is Digital Download", Value = "N" },
                        ["Model #"] = new() { Key = "Model #", Value = "811943" },
                        ["Material"] = new() { Key = "Material", Value = "Composite" },
                        ["Bulleted Specs"] = new()
                        {
                            Key = "Bulleted Specs",
                            Value = "<ul>\r\n<li>Global Product Type Pencils-Mechanical</li>\r\n<li>Mechanism Mechanical</li>\r\n<li>Pencil Type Mechanical</li>\r\n<li>Lead Color(s) Black</li>\r\n<li>Barrel Color(s) Assorted</li>\r\n<li>Barrel Material Plastic</li>\r\n<li>Refillable No</li>\r\n<li>Lead Degree (Hardness) HB</li>\r\n<li>Retractable Yes</li>\r\n<li>Eraser Yes</li>\r\n<li>Assorted Barrel Color 5 Blue; 5 Green; 5 Orange; 4 Purple; 5 Red</li>\r\n<li>Lead Diameter 0.7 mm</li>\r\n<li>Pocket Clip Yes</li>\r\n<li>Suggested Use General Use</li>\r\n<li>Lead Hardness (Number) #2</li>\r\n<li>Pre-Consumer Recycled Content Percent 0%</li>\r\n<li>Post-Consumer Recycled Content Percent 0%</li>\r\n<li>Total Recycled Content Percent 0%</li></ul>",
                        },
                        ["Details"] = new()
                        {
                            Key = "Details",
                            Value = "<ul>\r\n<li>Global Product Type Pencils-Mechanical</li>\r\n<li>Mechanism Mechanical</li>\r\n<li>Pencil Type Mechanical</li>\r\n<li>Lead Color(s) Black</li>\r\n<li>Barrel Color(s) Assorted</li>\r\n<li>Barrel Material Plastic</li>\r\n<li>Refillable No</li>\r\n<li>Lead Degree (Hardness) HB</li>\r\n<li>Retractable Yes</li>\r\n<li>Eraser Yes</li>\r\n<li>Assorted Barrel Color 5 Blue; 5 Green; 5 Orange; 4 Purple; 5 Red</li>\r\n<li>Lead Diameter 0.7 mm</li>\r\n<li>Pocket Clip Yes</li>\r\n<li>Suggested Use General Use</li>\r\n<li>Lead Hardness (Number) #2</li>\r\n<li>Pre-Consumer Recycled Content Percent 0%</li>\r\n<li>Post-Consumer Recycled Content Percent 0%</li>\r\n<li>Total Recycled Content Percent 0%</li></ul>",
                        },
                    }.SerializeAttributesDictionary(),
                    TypeID = context.ProductTypes.Where(pt => pt.Active && pt.Name == "General").Select(pt => pt.ID).First(),
                    StatusID = context.ProductStatuses.Where(pt => pt.Active && pt.Name == "Normal").Select(pt => pt.ID).First(),
                    Weight = 0,
                    CreatedDate = createdDate,
                    PackageID = context.Packages.Where(p => p.Name == "Default").Select(p => p.ID).FirstOrDefault(),
                    Categories = new List<ProductCategory>
                    {
                        new()
                        {
                            SlaveID = context.Categories.Where(c => c.CustomKey == "CAT-3").Select(c => c.ID).First(),
                            CreatedDate = createdDate,
                            Active = true,
                        },
                    },
                    ProductInventoryLocationSections = new[]
                    {
                        new ProductInventoryLocationSection
                        {
                            Active = true,
                            CreatedDate = createdDate,
                            SlaveID = context.InventoryLocationSections.First(x => x.CustomKey == "WAREHOUSE-1-Section-2").ID,
                            Quantity = 5,
                        },
                    },
                });
                context.SaveUnitOfWork();
            }
            if (context?.Products?.Any(x => x.CustomKey == "655266") == false)
            {
                context.Products.Add(new()
                {
                    TypeID = context.ProductTypes.Where(pt => pt.Active && pt.Name == "General").Select(pt => pt.ID).First(),
                    StatusID = context.ProductStatuses.Where(pt => pt.Active && pt.Name == "Normal").Select(pt => pt.ID).First(),
                    CustomKey = "655266",
                    Name = "Jurassic World Chomping Tyrannosaurus Rex Figure",
                    ShortDescription = "Tyrannosaurus Rex figure looks like the massive dino",
                    Description = "Tyrannosaurus Rex figure looks like the massive dino",
                    SeoUrl = "Jurassic-World-Chomping-Tyrannosaurus-Rex-Figure",
                    PriceBase = 9.72m,
                    PriceSale = 9.72m,
                    IsVisible = true,
                    IsDiscontinued = false,
                    IsTaxable = true,
                    IsFreeShipping = true,
                    Weight = 0,
                    AvailableStartDate = null,
                    AvailableEndDate = null,
                    CreatedDate = createdDate,
                    UpdatedDate = null,
                    Active = true,
                    SeoKeywords = "Jurassic World Chomping Tyrannosaurus Rex Figure,Keywords",
                    SeoDescription = "Jurassic World Chomping Tyrannosaurus Rex Figure,Description",
                    SeoPageTitle = "Jurassic World Chomping Tyrannosaurus Rex Figure",
                    PackageID = context.Packages.Where(p => p.Name == "Default").Select(p => p.ID).FirstOrDefault(),
                    Categories = new List<ProductCategory>
                    {
                        new()
                        {
                            SlaveID = context.Categories.Where(c => c.CustomKey == "CAT-3").Select(c => c.ID).First(),
                            CreatedDate = createdDate,
                            Active = true,
                        },
                    },
                    ProductInventoryLocationSections = new[]
                    {
                        new ProductInventoryLocationSection
                        {
                            Active = true,
                            CreatedDate = createdDate,
                            SlaveID = context.InventoryLocationSections.First(x => x.CustomKey == "WAREHOUSE-1-Section-2").ID,
                            Quantity = 5,
                        },
                    },
                });
                context.SaveUnitOfWork();
            }
            if (context?.Products?.Any(x => x.CustomKey == "B140386") == false)
            {
                context.Products.Add(new()
                {
                    TypeID = context.ProductTypes.Where(pt => pt.Active && pt.Name == "General").Select(pt => pt.ID).First(),
                    StatusID = context.ProductStatuses.Where(pt => pt.Active && pt.Name == "Normal").Select(pt => pt.ID).First(),
                    CustomKey = "B140386",
                    Name = "DEWALT DCD771C2 20V MAX Lithium-Ion Compact Drill/Driver Kit",
                    ShortDescription = "High performance motor delivers 300 unit watts out (UWO) of power ability completing a wide range of applications",
                    Description = "High performance motor delivers 300 unit watts out (UWO) of power ability completing a wide range of applications",
                    SeoUrl = "DEWALT-DCD771C2-20V-MAX-Lithium-Ion-Compact-Drill-Driver-Kit",
                    PriceBase = 9.72m,
                    PriceSale = 9.72m,
                    IsVisible = true,
                    IsDiscontinued = false,
                    IsTaxable = true,
                    IsFreeShipping = true,
                    Weight = 0,
                    AvailableStartDate = null,
                    AvailableEndDate = null,
                    CreatedDate = createdDate,
                    UpdatedDate = null,
                    Active = true,
                    SeoKeywords = "DEWALT DCD771C2 20V MAX Lithium-Ion Compact Drill/Driver Kit,Keywords",
                    SeoDescription = "DEWALT DCD771C2 20V MAX Lithium-Ion Compact Drill/Driver Kit,Description",
                    SeoPageTitle = "DEWALT DCD771C2 20V MAX Lithium-Ion Compact Drill/Driver Kit",
                    PackageID = context.Packages.Where(p => p.Name == "Default").Select(p => p.ID).FirstOrDefault(),
                    Categories = new List<ProductCategory>
                    {
                        new()
                        {
                            SlaveID = context.Categories.Where(c => c.CustomKey == "CAT-4").Select(c => c.ID).First(),
                            CreatedDate = createdDate,
                            Active = true,
                        },
                    },
                    ProductInventoryLocationSections = new[]
                    {
                        new ProductInventoryLocationSection
                        {
                            Active = true,
                            CreatedDate = createdDate,
                            SlaveID = context.InventoryLocationSections.First(x => x.CustomKey == "WAREHOUSE-1-Section-2").ID,
                            Quantity = 5,
                        },
                    },
                });
                context.SaveUnitOfWork();
            }
            if (context?.Products?.Any(x => x.CustomKey == "M6872") == false)
            {
                context.Products.Add(new()
                {
                    TypeID = context.ProductTypes.Where(pt => pt.Active && pt.Name == "General").Select(pt => pt.ID).First(),
                    StatusID = context.ProductStatuses.Where(pt => pt.Active && pt.Name == "Normal").Select(pt => pt.ID).First(),
                    CustomKey = "M6872",
                    Name = "CH Hanson 03040 Magnetic Stud Finder",
                    ShortDescription = "Small and compact, easily fits in pocket",
                    Description = "Small and compact, easily fits in pocket",
                    SeoUrl = "CH-Hanson-03040-Magnetic-Stud-Finder",
                    PriceBase = 8.99m,
                    PriceSale = 8.99m,
                    IsVisible = true,
                    IsDiscontinued = false,
                    IsTaxable = true,
                    IsFreeShipping = true,
                    Weight = 0,
                    AvailableStartDate = null,
                    AvailableEndDate = null,
                    CreatedDate = createdDate,
                    UpdatedDate = null,
                    Active = true,
                    SeoKeywords = "CH Hanson 03040 Magnetic Stud Finder,Keywords",
                    SeoDescription = "CH Hanson 03040 Magnetic Stud Finder,Description",
                    SeoPageTitle = "CH Hanson 03040 Magnetic Stud Finder",
                    PackageID = context.Packages.Where(p => p.Name == "Default").Select(p => p.ID).FirstOrDefault(),
                    Categories = new List<ProductCategory>
                    {
                        new()
                        {
                            SlaveID = context.Categories.Where(c => c.CustomKey == "CAT-4").Select(c => c.ID).First(),
                            CreatedDate = createdDate,
                            Active = true,
                        },
                    },
                    ProductInventoryLocationSections = new[]
                    {
                        new ProductInventoryLocationSection
                        {
                            Active = true,
                            CreatedDate = createdDate,
                            SlaveID = context.InventoryLocationSections.First(x => x.CustomKey == "WAREHOUSE-1-Section-2").ID,
                            Quantity = 5,
                        },
                    },
                });
                context.SaveUnitOfWork();
            }
            if (context?.Products?.Any(x => x.CustomKey == "M3659") == false)
            {
                context.Products.Add(new()
                {
                    TypeID = context.ProductTypes.Where(pt => pt.Active && pt.Name == "General").Select(pt => pt.ID).First(),
                    StatusID = context.ProductStatuses.Where(pt => pt.Active && pt.Name == "Normal").Select(pt => pt.ID).First(),
                    CustomKey = "M3659",
                    Name = "Dremel 7300-N/8 MiniMite 4.8-Volt Cordless Two-Speed Rotary Tool",
                    ShortDescription = "Cordless small rotary tool; compatible with Dremel bit accessories; 1/8-inch collet",
                    Description = "Cordless small rotary tool; compatible with Dremel bit accessories; 1/8-inch collet",
                    SeoUrl = "Dremel-7300-N-8-MiniMite-4-8-Volt-Cordless-Two-Speed-Rotary-Tool",
                    PriceBase = 9.72m,
                    PriceSale = 9.72m,
                    IsVisible = true,
                    IsDiscontinued = false,
                    IsTaxable = true,
                    IsFreeShipping = true,
                    Weight = 0,
                    AvailableStartDate = null,
                    AvailableEndDate = null,
                    CreatedDate = createdDate,
                    UpdatedDate = null,
                    Active = true,
                    SeoKeywords = "Dremel 7300-N/8 MiniMite 4.8-Volt Cordless Two-Speed Rotary Tool,Keywords",
                    SeoDescription = "Dremel 7300-N/8 MiniMite 4.8-Volt Cordless Two-Speed Rotary Tool,Description",
                    SeoPageTitle = "Dremel 7300-N/8 MiniMite 4.8-Volt Cordless Two-Speed Rotary Tool",
                    PackageID = context.Packages.Where(p => p.Name == "Default").Select(p => p.ID).FirstOrDefault(),
                    Categories = new List<ProductCategory>
                    {
                        new()
                        {
                            SlaveID = context.Categories.Where(c => c.CustomKey == "CAT-4").Select(c => c.ID).First(),
                            CreatedDate = createdDate,
                            Active = true,
                        },
                    },
                    ProductInventoryLocationSections = new[]
                    {
                        new ProductInventoryLocationSection
                        {
                            Active = true,
                            CreatedDate = createdDate,
                            SlaveID = context.InventoryLocationSections.First(x => x.CustomKey == "WAREHOUSE-1-Section-2").ID,
                            Quantity = 5,
                        },
                    },
                });
                context.SaveUnitOfWork();
            }
            if (context?.Products?.Any(x => x.CustomKey == "M7801") == false)
            {
                context.Products.Add(new()
                {
                    TypeID = context.ProductTypes.Where(pt => pt.Active && pt.Name == "General").Select(pt => pt.ID).First(),
                    StatusID = context.ProductStatuses.Where(pt => pt.Active && pt.Name == "Normal").Select(pt => pt.ID).First(),
                    CustomKey = "M7801",
                    Name = "Stack-On DS-60 60 Drawer Storage Cabinet",
                    ShortDescription = "Medium-sized see-through drawers with tab stops to prevent spills and easy-pull ribbed handles",
                    Description = "Medium-sized see-through drawers with tab stops to prevent spills and easy-pull ribbed handles",
                    SeoUrl = "Stack-On-DS-60-60-Drawer-Storage-Cabinet",
                    PriceBase = 24.99m,
                    PriceSale = 24.99m,
                    IsVisible = true,
                    IsDiscontinued = false,
                    IsTaxable = true,
                    IsFreeShipping = true,
                    Weight = 0,
                    AvailableStartDate = null,
                    AvailableEndDate = null,
                    CreatedDate = createdDate,
                    UpdatedDate = null,
                    Active = true,
                    SeoKeywords = "Stack-On DS-60 60 Drawer Storage Cabinet,Keywords",
                    SeoDescription = "Stack-On DS-60 60 Drawer Storage Cabinet,Description",
                    SeoPageTitle = "Stack-On DS-60 60 Drawer Storage Cabinet",
                    PackageID = context.Packages.Where(p => p.Name == "Default").Select(p => p.ID).FirstOrDefault(),
                    Categories = new List<ProductCategory>
                    {
                        new()
                        {
                            SlaveID = context.Categories.Where(c => c.CustomKey == "CAT-4").Select(c => c.ID).First(),
                            CreatedDate = createdDate,
                            Active = true,
                        },
                    },
                    ProductInventoryLocationSections = new[]
                    {
                        new ProductInventoryLocationSection
                        {
                            Active = true,
                            CreatedDate = createdDate,
                            SlaveID = context.InventoryLocationSections.First(x => x.CustomKey == "WAREHOUSE-1-Section-2").ID,
                            Quantity = 5,
                        },
                    },
                });
                context.SaveUnitOfWork();
            }
            if (context?.Products?.Any(x => x.CustomKey == "calv-4db") == false)
            {
                context.Products.Add(new()
                {
                    TypeID = context.ProductTypes.Where(pt => pt.Active && pt.Name == "General").Select(pt => pt.ID).First(),
                    StatusID = context.ProductStatuses.Where(pt => pt.Active && pt.Name == "Normal").Select(pt => pt.ID).First(),
                    CustomKey = "calv-4db",
                    Name = "Homak GS00727021 Steel 2-Door Wall Cabinet with 2-Shelves",
                    ShortDescription = null,
                    Description = null,
                    SeoUrl = "Homak-GS00727021-Steel-2-Door-Wall-Cabinet-with-2-Shelves",
                    PriceBase = 99.00m,
                    PriceSale = 99.00m,
                    IsVisible = true,
                    IsDiscontinued = false,
                    IsTaxable = true,
                    IsFreeShipping = true,
                    Weight = 0,
                    AvailableStartDate = null,
                    AvailableEndDate = null,
                    CreatedDate = createdDate,
                    UpdatedDate = null,
                    Active = true,
                    SeoKeywords = "Homak GS00727021 Steel 2-Door Wall Cabinet with 2-Shelves,Keywords",
                    SeoDescription = "Homak GS00727021 Steel 2-Door Wall Cabinet with 2-Shelves,Description",
                    SeoPageTitle = "Homak GS00727021 Steel 2-Door Wall Cabinet with 2-Shelves",
                    PackageID = context.Packages.Where(p => p.Name == "Default").Select(p => p.ID).FirstOrDefault(),
                    Categories = new List<ProductCategory>
                    {
                        new()
                        {
                            SlaveID = context.Categories.Where(c => c.CustomKey == "CAT-4").Select(c => c.ID).First(),
                            CreatedDate = createdDate,
                            Active = true,
                        },
                    },
                    ProductInventoryLocationSections = new[]
                    {
                        new ProductInventoryLocationSection
                        {
                            Active = true,
                            CreatedDate = createdDate,
                            SlaveID = context.InventoryLocationSections.First(x => x.CustomKey == "WAREHOUSE-1-Section-2").ID,
                            Quantity = 5,
                        },
                    },
                });
                context.SaveUnitOfWork();
            }
            if (context?.Products?.Any(x => x.CustomKey == "EOS-T5") == false)
            {
                context.Products.Add(new()
                {
                    TypeID = context.ProductTypes.Where(pt => pt.Active && pt.Name == "General").Select(pt => pt.ID).First(),
                    StatusID = context.ProductStatuses.Where(pt => pt.Active && pt.Name == "Normal").Select(pt => pt.ID).First(),
                    CustomKey = "EOS-T5",
                    Name = "Callaway Men's Strata Complete Golf Club Set with Bag (12-Piece)",
                    ShortDescription = "Stand Bag: The lightweight, durable stand bag comes with five convenient pockets, an additional cooler pocket, a rain hood and a backpack strap system",
                    Description = "Stand Bag: The lightweight, durable stand bag comes with five convenient pockets, an additional cooler pocket, a rain hood and a backpack strap system",
                    SeoUrl = "Callaway-Strata-Golf-Club-Set-With-Bag",
                    PriceBase = 9.72m,
                    PriceSale = 9.72m,
                    IsVisible = true,
                    IsDiscontinued = false,
                    IsTaxable = true,
                    IsFreeShipping = true,
                    Weight = 0,
                    AvailableStartDate = null,
                    AvailableEndDate = null,
                    CreatedDate = createdDate,
                    UpdatedDate = null,
                    Active = true,
                    SeoKeywords = "Callaway Men's Strata Complete Golf Club Set with Bag (12-Piece),Keywords",
                    SeoDescription = "Callaway Men's Strata Complete Golf Club Set with Bag (12-Piece),Description",
                    SeoPageTitle = "Callaway Men's Strata Complete Golf Club Set with Bag",
                    PackageID = context.Packages.Where(p => p.Name == "Default").Select(p => p.ID).FirstOrDefault(),
                    Categories = new List<ProductCategory>
                    {
                        new()
                        {
                            SlaveID = context.Categories.Where(c => c.CustomKey == "CAT-5").Select(c => c.ID).First(),
                            CreatedDate = createdDate,
                            Active = true,
                        },
                    },
                    ProductInventoryLocationSections = new[]
                    {
                        new ProductInventoryLocationSection
                        {
                            Active = true,
                            CreatedDate = createdDate,
                            SlaveID = context.InventoryLocationSections.First(x => x.CustomKey == "WAREHOUSE-1-Section-2").ID,
                            Quantity = 5,
                        },
                    },
                });
                context.SaveUnitOfWork();
            }
            if (context?.Products?.Any(x => x.CustomKey == "5350171") == false)
            {
                context.Products.Add(new()
                {
                    TypeID = context.ProductTypes.Where(pt => pt.Active && pt.Name == "General").Select(pt => pt.ID).First(),
                    StatusID = context.ProductStatuses.Where(pt => pt.Active && pt.Name == "Normal").Select(pt => pt.ID).First(),
                    CustomKey = "5350171",
                    Name = "Trigger Point Performance Nano Foot Roller Massager",
                    ShortDescription = "Ultra compact foam roller specifically designed to relive foot aches and pains",
                    Description = "Ultra compact foam roller specifically designed to relive foot aches and pains",
                    SeoUrl = "Trigger-Point-Nano-Foot-Roller-Massager",
                    PriceBase = 9.72m,
                    PriceSale = 9.72m,
                    IsVisible = true,
                    IsDiscontinued = false,
                    IsTaxable = true,
                    IsFreeShipping = true,
                    Weight = 0,
                    AvailableStartDate = null,
                    AvailableEndDate = null,
                    CreatedDate = createdDate,
                    UpdatedDate = null,
                    Active = true,
                    SeoKeywords = "Trigger Point Performance Nano Foot Roller Massager,Keywords",
                    SeoDescription = "Trigger Point Performance Nano Foot Roller Massager,Description",
                    SeoPageTitle = "Trigger Point Performance Nano Foot Roller Massager",
                    PackageID = context.Packages.Where(p => p.Name == "Default").Select(p => p.ID).FirstOrDefault(),
                    Categories = new List<ProductCategory>
                    {
                        new()
                        {
                            SlaveID = context.Categories.Where(c => c.CustomKey == "CAT-5").Select(c => c.ID).First(),
                            CreatedDate = createdDate,
                            Active = true,
                        },
                    },
                    ProductInventoryLocationSections = new[]
                    {
                        new ProductInventoryLocationSection
                        {
                            Active = true,
                            CreatedDate = createdDate,
                            SlaveID = context.InventoryLocationSections.First(x => x.CustomKey == "WAREHOUSE-1-Section-2").ID,
                            Quantity = 5,
                        },
                    },
                });
                context.SaveUnitOfWork();
            }
            if (context?.Products?.Any(x => x.CustomKey == "516995") == false)
            {
                context.Products.Add(new()
                {
                    TypeID = context.ProductTypes.Where(pt => pt.Active && pt.Name == "General").Select(pt => pt.ID).First(),
                    StatusID = context.ProductStatuses.Where(pt => pt.Active && pt.Name == "Normal").Select(pt => pt.ID).First(),
                    CustomKey = "516995",
                    Name = "Apex Deluxe 3-Tier Dumbbell Rack",
                    ShortDescription = "Dumbbell rack accommodate multiple size dumbbells",
                    Description = "Dumbbell rack accommodate multiple size dumbbells",
                    SeoUrl = "Apex-Deluxe-3-Tier-Dumbbell-Rack",
                    PriceBase = 9.72m,
                    PriceSale = 9.72m,
                    IsVisible = true,
                    IsDiscontinued = false,
                    IsTaxable = true,
                    IsFreeShipping = true,
                    Weight = 0,
                    AvailableStartDate = null,
                    AvailableEndDate = null,
                    CreatedDate = createdDate,
                    UpdatedDate = null,
                    Active = true,
                    SeoKeywords = "Apex Deluxe 3-Tier Dumbbell Rack,Keywords",
                    SeoDescription = "Apex Deluxe 3-Tier Dumbbell RackS,Description",
                    SeoPageTitle = "Apex Deluxe 3-Tier Dumbbell Rack",
                    PackageID = context.Packages.Where(p => p.Name == "Default").Select(p => p.ID).FirstOrDefault(),
                    Categories = new List<ProductCategory>
                    {
                        new()
                        {
                            SlaveID = context.Categories.Where(c => c.CustomKey == "CAT-5").Select(c => c.ID).First(),
                            CreatedDate = createdDate,
                            Active = true,
                        },
                    },
                    ProductInventoryLocationSections = new[]
                    {
                        new ProductInventoryLocationSection
                        {
                            Active = true,
                            CreatedDate = createdDate,
                            SlaveID = context.InventoryLocationSections.First(x => x.CustomKey == "WAREHOUSE-1-Section-2").ID,
                            Quantity = 5,
                        },
                    },
                });
                context.SaveUnitOfWork();
            }
            if (context?.Products?.Any(x => x.CustomKey == "516996") == false)
            {
                context.Products.Add(new()
                {
                    TypeID = context.ProductTypes.Where(pt => pt.Active && pt.Name == "General").Select(pt => pt.ID).First(),
                    StatusID = context.ProductStatuses.Where(pt => pt.Active && pt.Name == "Normal").Select(pt => pt.ID).First(),
                    CustomKey = "516996",
                    Name = "Gaiam Print Premium Yoga Mats (5mm)",
                    ShortDescription = "Lightweight, durable and extra-thick for additional cushioning",
                    Description = "Lightweight, durable and extra-thick for additional cushioning",
                    SeoUrl = "Gaiam-Print-Premium-Yoga-Mats",
                    PriceBase = 9.72m,
                    PriceSale = 9.72m,
                    IsVisible = true,
                    IsDiscontinued = false,
                    IsTaxable = true,
                    IsFreeShipping = true,
                    Weight = 0,
                    AvailableStartDate = null,
                    AvailableEndDate = null,
                    CreatedDate = createdDate,
                    UpdatedDate = null,
                    Active = true,
                    SeoKeywords = "Gaiam Print Premium Yoga Mats,Keywords",
                    SeoDescription = "Gaiam Print Premium Yoga Mats,Description",
                    SeoPageTitle = "Gaiam Print Premium Yoga Mats",
                    Categories = new List<ProductCategory>
                    {
                        new()
                        {
                            SlaveID = context.Categories.Where(c => c.CustomKey == "CAT-5").Select(c => c.ID).First(),
                            CreatedDate = createdDate,
                            Active = true,
                        },
                    },
                    ProductInventoryLocationSections = new[]
                    {
                        new ProductInventoryLocationSection
                        {
                            Active = true,
                            CreatedDate = createdDate,
                            SlaveID = context.InventoryLocationSections.First(x => x.CustomKey == "WAREHOUSE-1-Section-2").ID,
                            Quantity = 5,
                        },
                    },
                });
                context.SaveUnitOfWork();
            }
            if (context?.Products?.Any(x => x.CustomKey == "403100000000") == false)
            {
                context.Products.Add(new()
                {
                    TypeID = context.ProductTypes.Where(pt => pt.Active && pt.Name == "General").Select(pt => pt.ID).First(),
                    StatusID = context.ProductStatuses.Where(pt => pt.Active && pt.Name == "Normal").Select(pt => pt.ID).First(),
                    CustomKey = "403100000000",
                    Name = "Timex Personal Trainer Heart Rate Monitor",
                    ShortDescription = null,
                    Description = null,
                    SeoUrl = "Timex-Personal-Trainer-Heart-Rate-Monitor",
                    PriceBase = 9.72m,
                    PriceSale = 9.72m,
                    IsVisible = true,
                    IsDiscontinued = false,
                    IsTaxable = true,
                    IsFreeShipping = true,
                    Weight = 0,
                    AvailableStartDate = null,
                    AvailableEndDate = null,
                    CreatedDate = createdDate,
                    UpdatedDate = null,
                    Active = true,
                    SeoKeywords = "Timex Personal Trainer Heart Rate Monitor,Keywords",
                    SeoDescription = "Timex Personal Trainer Heart Rate Monitor,Description",
                    SeoPageTitle = "Timex Personal Trainer Heart Rate Monitor",
                    PackageID = context.Packages.Where(p => p.Name == "Default").Select(p => p.ID).FirstOrDefault(),
                    Categories = new List<ProductCategory>
                    {
                        new()
                        {
                            SlaveID = context.Categories.Where(c => c.CustomKey == "CAT-5").Select(c => c.ID).First(),
                            CreatedDate = createdDate,
                            Active = true,
                        },
                    },
                    ProductInventoryLocationSections = new[]
                    {
                        new ProductInventoryLocationSection
                        {
                            Active = true,
                            CreatedDate = createdDate,
                            SlaveID = context.InventoryLocationSections.First(x => x.CustomKey == "WAREHOUSE-1-Section-2").ID,
                            Quantity = 5,
                        },
                    },
                });
                context.SaveUnitOfWork();
            }
            if (context?.Products?.Any(x => x.CustomKey == "878249") == false)
            {
                context.Products.Add(new()
                {
                    TypeID = context.ProductTypes.Where(pt => pt.Active && pt.Name == "General").Select(pt => pt.ID).First(),
                    StatusID = context.ProductStatuses.Where(pt => pt.Active && pt.Name == "Normal").Select(pt => pt.ID).First(),
                    CustomKey = "878249",
                    Name = "Upstar P32EE7 32-Inch 720p 60Hz LED TV",
                    ShortDescription = "Dimensions (W x H x D): TV without stand: 29.1\" x 17.8\" x 2.7\" , TV with stand: 29.10\" x 19.40\" x 7.5\"",
                    Description = "Dimensions (W x H x D): TV without stand: 29.1\" x 17.8\" x 2.7\" , TV with stand: 29.10\" x 19.40\" x 7.5\"",
                    SeoUrl = "Upstar-P32EE7-32-Inch-720p-60Hz-LED-TV",
                    PriceBase = 179.99m,
                    PriceSale = 179.99m,
                    IsVisible = true,
                    IsDiscontinued = false,
                    IsTaxable = true,
                    IsFreeShipping = true,
                    Weight = 0,
                    AvailableStartDate = null,
                    AvailableEndDate = null,
                    CreatedDate = createdDate,
                    UpdatedDate = null,
                    Active = true,
                    SeoKeywords = ",Keywords",
                    SeoDescription = ",Description",
                    SeoPageTitle = null,
                    PackageID = context.Packages.Where(p => p.Name == "Default").Select(p => p.ID).FirstOrDefault(),
                    Categories = new List<ProductCategory>
                    {
                        new()
                        {
                            SlaveID = context.Categories.Where(c => c.CustomKey == "CAT-6").Select(c => c.ID).First(),
                            CreatedDate = createdDate,
                            Active = true,
                        },
                    },
                    ProductInventoryLocationSections = new[]
                    {
                        new ProductInventoryLocationSection
                        {
                            Active = true,
                            CreatedDate = createdDate,
                            SlaveID = context.InventoryLocationSections.First(x => x.CustomKey == "WAREHOUSE-1-Section-2").ID,
                            Quantity = 5,
                        },
                    },
                });
                context.SaveUnitOfWork();
            }
            if (context?.Products?.Any(x => x.CustomKey == "CBENM09") == false)
            {
                context.Products.Add(new()
                {
                    TypeID = context.ProductTypes.Where(pt => pt.Active && pt.Name == "General").Select(pt => pt.ID).First(),
                    StatusID = context.ProductStatuses.Where(pt => pt.Active && pt.Name == "Normal").Select(pt => pt.ID).First(),
                    CustomKey = "CBENM09",
                    Name = "Canon EOS Rebel T5 Digital SLR Camera with EF-S 18-55mm IS II + EF 75-300mm f/4-5.6 III Bundle",
                    ShortDescription = "Features include continuous shooting up to 3fps, Scene Intelligent Auto mode, creative filers, built-in flash and feature guide",
                    Description = "Features include continuous shooting up to 3fps, Scene Intelligent Auto mode, creative filers, built-in flash and feature guide",
                    SeoUrl = "Canon-EOS-Rebel-T5-Digital-SLR-Camera",
                    PriceBase = 449.00m,
                    PriceSale = 449.00m,
                    IsVisible = true,
                    IsDiscontinued = false,
                    IsTaxable = true,
                    IsFreeShipping = true,
                    Weight = 0,
                    AvailableStartDate = null,
                    AvailableEndDate = null,
                    CreatedDate = createdDate,
                    UpdatedDate = null,
                    Active = true,
                    SeoKeywords = "Canon EOS Rebel T5 Digital SLR Camera with EF-S 18-55mm IS II + EF 75-300mm f/4-5.6 III Bundle,Keywords",
                    SeoDescription = "Canon EOS Rebel T5 Digital SLR Camera with EF-S 18-55mm IS II + EF 75-300mm f/4-5.6 III Bundle,Description",
                    SeoPageTitle = "Canon EOS Rebel T5 Digital SLR Camera",
                    PackageID = context.Packages.Where(p => p.Name == "Default").Select(p => p.ID).FirstOrDefault(),
                    Categories = new List<ProductCategory>
                    {
                        new()
                        {
                            SlaveID = context.Categories.Where(c => c.CustomKey == "CAT-6").Select(c => c.ID).First(),
                            CreatedDate = createdDate,
                            Active = true,
                        },
                    },
                    ProductInventoryLocationSections = new[]
                    {
                        new ProductInventoryLocationSection
                        {
                            Active = true,
                            CreatedDate = createdDate,
                            SlaveID = context.InventoryLocationSections.First(x => x.CustomKey == "WAREHOUSE-1-Section-2").ID,
                            Quantity = 5,
                        },
                    },
                });
                context.SaveUnitOfWork();
            }
            if (context?.Products?.Any(x => x.CustomKey == "537011") == false)
            {
                context.Products.Add(new()
                {
                    TypeID = context.ProductTypes.Where(pt => pt.Active && pt.Name == "General").Select(pt => pt.ID).First(),
                    StatusID = context.ProductStatuses.Where(pt => pt.Active && pt.Name == "Normal").Select(pt => pt.ID).First(),
                    CustomKey = "537011",
                    Name = "Humminbird 409610-1 Helix 5 Fish finder with GPS",
                    ShortDescription = "GPS Chart plotting with UniMap",
                    Description = "GPS Chart plotting with UniMap",
                    SeoUrl = "Humminbird-409610-1-Helix-5-Fish-Finder-With-GPS",
                    PriceBase = 296.70m,
                    PriceSale = 296.70m,
                    IsVisible = true,
                    IsDiscontinued = false,
                    IsTaxable = true,
                    IsFreeShipping = true,
                    Weight = 0,
                    AvailableStartDate = null,
                    AvailableEndDate = null,
                    CreatedDate = createdDate,
                    UpdatedDate = null,
                    Active = true,
                    SeoKeywords = "Humminbird 409610-1 Helix 5 Fish finder with GPS,Keywords",
                    SeoDescription = "Humminbird 409610-1 Helix 5 Fish finder with GPS,Description",
                    SeoPageTitle = "Humminbird 409610-1 Helix 5 Fish finder with GPS",
                    PackageID = context.Packages.Where(p => p.Name == "Default").Select(p => p.ID).FirstOrDefault(),
                    Categories = new List<ProductCategory>
                    {
                        new()
                        {
                            SlaveID = context.Categories.Where(c => c.CustomKey == "CAT-6").Select(c => c.ID).First(),
                            CreatedDate = createdDate,
                            Active = true,
                        },
                    },
                    ProductInventoryLocationSections = new[]
                    {
                        new ProductInventoryLocationSection
                        {
                            Active = true,
                            CreatedDate = createdDate,
                            SlaveID = context.InventoryLocationSections.First(x => x.CustomKey == "WAREHOUSE-1-Section-2").ID,
                            Quantity = 5,
                        },
                    },
                });
                context.SaveUnitOfWork();
            }
            if (context?.Products?.Any(x => x.CustomKey == "1056120") == false)
            {
                context.Products.Add(new()
                {
                    TypeID = context.ProductTypes.Where(pt => pt.Active && pt.Name == "General").Select(pt => pt.ID).First(),
                    StatusID = context.ProductStatuses.Where(pt => pt.Active && pt.Name == "Normal").Select(pt => pt.ID).First(),
                    CustomKey = "1056120",
                    Name = "Garmin Dash Cam 20 Standalone Driving Recorder",
                    ShortDescription = "Take still images, even remove from vehicle, to capture collision damage",
                    Description = "Take still images, even remove from vehicle, to capture collision damage",
                    SeoUrl = "Garmin-Dash-Cam-20-Standalone-Driving-Recorder",
                    PriceBase = 179.99m,
                    PriceSale = 179.99m,
                    IsVisible = true,
                    IsDiscontinued = false,
                    IsTaxable = true,
                    IsFreeShipping = true,
                    Weight = 0,
                    AvailableStartDate = null,
                    AvailableEndDate = null,
                    CreatedDate = createdDate,
                    UpdatedDate = null,
                    Active = true,
                    SeoKeywords = "Garmin Dash Cam 20 Standalone Driving Recorder,Keywords",
                    SeoDescription = "Garmin Dash Cam 20 Standalone Driving Recorder,Description",
                    SeoPageTitle = "Garmin Dash Cam 20 Standalone Driving Recorder",
                    PackageID = context.Packages.Where(p => p.Name == "Default").Select(p => p.ID).FirstOrDefault(),
                    Categories = new List<ProductCategory>
                    {
                        new()
                        {
                            SlaveID = context.Categories.Where(c => c.CustomKey == "CAT-6").Select(c => c.ID).First(),
                            CreatedDate = createdDate,
                            Active = true,
                        },
                    },
                    ProductInventoryLocationSections = new[]
                    {
                        new ProductInventoryLocationSection
                        {
                            Active = true,
                            CreatedDate = createdDate,
                            SlaveID = context.InventoryLocationSections.First(x => x.CustomKey == "WAREHOUSE-1-Section-2").ID,
                            Quantity = 5,
                        },
                    },
                });
                context.SaveUnitOfWork();
            }
            if (context?.Products?.Any(x => x.CustomKey == "308450") == false)
            {
                context.Products.Add(new()
                {
                    TypeID = context.ProductTypes.Where(pt => pt.Active && pt.Name == "General").Select(pt => pt.ID).First(),
                    StatusID = context.ProductStatuses.Where(pt => pt.Active && pt.Name == "Normal").Select(pt => pt.ID).First(),
                    CustomKey = "308450",
                    Name = "Bluedio Q5 Sports Bluetooth stereo headphones/wireless Bluetooth4.1 headphones/headset Earphones for outdoor Sports Gift package",
                    ShortDescription = "Revolutionarily, Q5 has no heavy control box. No annoying box swings back and forth, even tries to pull out the earphones during workout. You may ask: but can I control music & calls? Don't worry, there are buttons on the right earphone for you to co",
                    Description = "Revolutionarily, Q5 has no heavy control box. No annoying box swings back and forth, even tries to pull out the earphones during workout. You may ask: but can I control music & calls? Don't worry, there are buttons on the right earphone for you to control.",
                    SeoUrl = "Bluedio-Q5-Sports-Bluetooth-Stereo-Headphones",
                    PriceBase = 9.72m,
                    PriceSale = 9.72m,
                    IsVisible = true,
                    IsDiscontinued = false,
                    IsTaxable = true,
                    IsFreeShipping = true,
                    Weight = 0,
                    WeightUnitOfMeasure = "lbs",
                    AvailableStartDate = null,
                    AvailableEndDate = null,
                    CreatedDate = createdDate,
                    UpdatedDate = null,
                    Active = true,
                    SeoKeywords = "Bluedio Q5 Sports Bluetooth stereo headphones/wireless Bluetooth4.1 headphones/headset Earphones for outdoor Sports Gift package,Keywords",
                    SeoDescription = "Bluedio Q5 Sports Bluetooth stereo headphones/wireless Bluetooth4.1 headphones/headset Earphones for outdoor Sports Gift package,Description",
                    SeoPageTitle = "Bluedio Q5 Sports Bluetooth stereo headphones",
                    PackageID = context.Packages.Where(p => p.Name == "Default").Select(p => p.ID).FirstOrDefault(),
                    Categories = new List<ProductCategory>
                    {
                        new()
                        {
                            SlaveID = context.Categories.Where(c => c.CustomKey == "CAT-6").Select(c => c.ID).First(),
                            CreatedDate = createdDate,
                            Active = true,
                        },
                    },
                    ProductInventoryLocationSections = new[]
                    {
                        new ProductInventoryLocationSection
                        {
                            Active = true,
                            CreatedDate = createdDate,
                            SlaveID = context.InventoryLocationSections.First(x => x.CustomKey == "WAREHOUSE-1-Section-2").ID,
                            Quantity = 5,
                        },
                    },
                });
                context.SaveUnitOfWork();
            }
            if (context?.Products?.Any(x => x.CustomKey == "344352") == false)
            {
                context.Products.Add(new()
                {
                    TypeID = context.ProductTypes.Where(pt => pt.Active && pt.Name == "General").Select(pt => pt.ID).First(),
                    StatusID = context.ProductStatuses.Where(pt => pt.Active && pt.Name == "Normal").Select(pt => pt.ID).First(),
                    CustomKey = "344352",
                    Name = "Dockers Men's Perfect Short D3 Classic Fit Flat Front",
                    ShortDescription = "Short in flat-front silhouette with side-slant pockets and back button-through welt pockets",
                    Description = "Short in flat-front silhouette with side-slant pockets and back button-through welt pockets",
                    SeoUrl = "Dockers-Short-D3-Classic-Fit-Flat-Front",
                    PriceBase = 24.99m,
                    PriceSale = 24.99m,
                    IsVisible = true,
                    IsDiscontinued = false,
                    IsTaxable = true,
                    IsFreeShipping = true,
                    Weight = 0,
                    AvailableStartDate = null,
                    AvailableEndDate = null,
                    CreatedDate = createdDate,
                    UpdatedDate = null,
                    Active = true,
                    SeoKeywords = "Dockers Men's Perfect Short D3 Classic Fit Flat Front,Keywords",
                    SeoDescription = "Dockers Men's Perfect Short D3 Classic Fit Flat Front,Description",
                    SeoPageTitle = "Dockers Men's Perfect Short D3 Classic Fit Flat Front",
                    PackageID = context.Packages.Where(p => p.Name == "Default").Select(p => p.ID).FirstOrDefault(),
                    Categories = new List<ProductCategory>
                    {
                        new()
                        {
                            SlaveID = context.Categories.Where(c => c.CustomKey == "CAT-7").Select(c => c.ID).First(),
                            CreatedDate = createdDate,
                            Active = true,
                        },
                    },
                    ProductInventoryLocationSections = new[]
                    {
                        new ProductInventoryLocationSection
                        {
                            Active = true,
                            CreatedDate = createdDate,
                            SlaveID = context.InventoryLocationSections.First(x => x.CustomKey == "WAREHOUSE-1-Section-2").ID,
                            Quantity = 5,
                        },
                    },
                });
                context.SaveUnitOfWork();
            }
            if (context?.Products?.Any(x => x.CustomKey == "343731") == false)
            {
                context.Products.Add(new()
                {
                    TypeID = context.ProductTypes.Where(pt => pt.Active && pt.Name == "General").Select(pt => pt.ID).First(),
                    StatusID = context.ProductStatuses.Where(pt => pt.Active && pt.Name == "Normal").Select(pt => pt.ID).First(),
                    CustomKey = "343731",
                    Name = "Kenneth Cole New York Women's KC4852 Transparency Stainless Steel Watch",
                    ShortDescription = "Rose gold-tone watch in stainless steel with transparent outer dial, crystal-set bezel, and inner sunray dial",
                    Description = "Rose gold-tone watch in stainless steel with transparent outer dial, crystal-set bezel, and inner sunray dial",
                    SeoUrl = "Kenneth-Cole-KC4852-Transparency-Stainless-Steel-Watch",
                    PriceBase = 83.22m,
                    PriceSale = 83.22m,
                    IsVisible = true,
                    IsDiscontinued = false,
                    IsTaxable = true,
                    IsFreeShipping = true,
                    Weight = 0,
                    AvailableStartDate = null,
                    AvailableEndDate = null,
                    CreatedDate = createdDate,
                    UpdatedDate = null,
                    Active = true,
                    SeoKeywords = "Kenneth Cole New York Women's KC4852 Transparency Stainless Steel Watch,Keywords",
                    SeoDescription = "Kenneth Cole New York Women's KC4852 Transparency Stainless Steel Watch,Description",
                    SeoPageTitle = "Kenneth Cole New York Women's KC4852 Transparency Stainless Steel Watch",
                    PackageID = context.Packages.Where(p => p.Name == "Default").Select(p => p.ID).FirstOrDefault(),
                    Categories = new List<ProductCategory>
                    {
                        new()
                        {
                            SlaveID = context.Categories.Where(c => c.CustomKey == "CAT-7").Select(c => c.ID).First(),
                            CreatedDate = createdDate,
                            Active = true,
                        },
                    },
                    ProductInventoryLocationSections = new[]
                    {
                        new ProductInventoryLocationSection
                        {
                            Active = true,
                            CreatedDate = createdDate,
                            SlaveID = context.InventoryLocationSections.First(x => x.CustomKey == "WAREHOUSE-1-Section-2").ID,
                            Quantity = 5,
                        },
                    },
                });
                context.SaveUnitOfWork();
            }
            if (context?.Products?.Any(x => x.CustomKey == "11473224") == false)
            {
                context.Products.Add(new()
                {
                    TypeID = context.ProductTypes.Where(pt => pt.Active && pt.Name == "General").Select(pt => pt.ID).First(),
                    StatusID = context.ProductStatuses.Where(pt => pt.Active && pt.Name == "Normal").Select(pt => pt.ID).First(),
                    CustomKey = "11473224",
                    Name = "Diesel Men's Dragon 94 Draags94 Fashion Sneaker",
                    ShortDescription = "Textile Imported Synthetic sole Diesel Black/Black Fashion-sneakers Made in Spain",
                    Description = "Textile Imported Synthetic sole Diesel Black/Black Fashion-sneakers Made in Spain",
                    SeoUrl = "Diesel-Dragon-94-Fashion-Sneaker",
                    PriceBase = 140.00m,
                    PriceSale = 140.00m,
                    IsVisible = true,
                    IsDiscontinued = false,
                    IsTaxable = true,
                    IsFreeShipping = true,
                    Weight = 0,
                    AvailableStartDate = null,
                    AvailableEndDate = null,
                    CreatedDate = createdDate,
                    UpdatedDate = null,
                    Active = true,
                    SeoKeywords = "Diesel Men's Dragon 94 Draags94 Fashion Sneaker,Keywords",
                    SeoDescription = "Diesel Men's Dragon 94 Draags94 Fashion Sneaker,Description",
                    SeoPageTitle = "Diesel Men's Dragon 94 Draags94 Fashion Sneaker",
                    PackageID = context.Packages.Where(p => p.Name == "Default").Select(p => p.ID).FirstOrDefault(),
                    Categories = new List<ProductCategory>
                    {
                        new()
                        {
                            SlaveID = context.Categories.Where(c => c.CustomKey == "CAT-7").Select(c => c.ID).First(),
                            CreatedDate = createdDate,
                            Active = true,
                        },
                    },
                    ProductInventoryLocationSections = new[]
                    {
                        new ProductInventoryLocationSection
                        {
                            Active = true,
                            CreatedDate = createdDate,
                            SlaveID = context.InventoryLocationSections.First(x => x.CustomKey == "WAREHOUSE-1-Section-2").ID,
                            Quantity = 5,
                        },
                    },
                });
                context.SaveUnitOfWork();
            }
            if (context?.Products?.Any(x => x.CustomKey == "4327678") == false)
            {
                context.Products.Add(new()
                {
                    TypeID = context.ProductTypes.Where(pt => pt.Active && pt.Name == "General").Select(pt => pt.ID).First(),
                    StatusID = context.ProductStatuses.Where(pt => pt.Active && pt.Name == "Normal").Select(pt => pt.ID).First(),
                    CustomKey = "4327678",
                    Name = "Calvin Klein 4 DB Logo Satchel Top Handle Bag",
                    ShortDescription = "100% Polyvinyl Chloride Imported polyester lining zipper closure Made in China",
                    Description = "100% Polyvinyl Chloride Imported polyester lining zipper closure Made in China",
                    SeoUrl = "Calvin-Klein-4-DB-Logo-Satchel-Top-Handle-Bag",
                    PriceBase = 178.00m,
                    PriceSale = 178.00m,
                    IsVisible = true,
                    IsDiscontinued = false,
                    IsTaxable = true,
                    IsFreeShipping = true,
                    Weight = 0,
                    AvailableStartDate = null,
                    AvailableEndDate = null,
                    CreatedDate = createdDate,
                    UpdatedDate = null,
                    Active = true,
                    SeoKeywords = "Calvin Klein 4 DB Logo Satchel Top Handle Bag,Keywords",
                    SeoDescription = "Calvin Klein 4 DB Logo Satchel Top Handle Bag,Description",
                    SeoPageTitle = "Calvin Klein 4 DB Logo Satchel Top Handle Bag",
                    PackageID = context.Packages.Where(p => p.Name == "Default").Select(p => p.ID).FirstOrDefault(),
                    Categories = new List<ProductCategory>
                    {
                        new()
                        {
                            SlaveID = context.Categories.Where(c => c.CustomKey == "CAT-7").Select(c => c.ID).First(),
                            CreatedDate = createdDate,
                            Active = true,
                        },
                    },
                    ProductInventoryLocationSections = new[]
                    {
                        new ProductInventoryLocationSection
                        {
                            Active = true,
                            CreatedDate = createdDate,
                            SlaveID = context.InventoryLocationSections.First(x => x.CustomKey == "WAREHOUSE-1-Section-2").ID,
                            Quantity = 5,
                        },
                    },
                });
                context.SaveUnitOfWork();
            }
            if (context?.Products?.Any(x => x.CustomKey == "8876345") == false)
            {
                context.Products.Add(new()
                {
                    TypeID = context.ProductTypes.Where(pt => pt.Active && pt.Name == "General").Select(pt => pt.ID).First(),
                    StatusID = context.ProductStatuses.Where(pt => pt.Active && pt.Name == "Normal").Select(pt => pt.ID).First(),
                    CustomKey = "8876345",
                    Name = "Sperry Top-Sider Men's A/O 3 Eye Fleck Canvas Boat Shoe",
                    ShortDescription = "These fun shoes by Sperry feature genuine hand sewn tru moc construction, garment washed canvas upper and a shock absorbing EVA heel cup",
                    Description = "These fun shoes by Sperry feature genuine hand sewn tru moc construction, garment washed canvas upper and a shock absorbing EVA heel cup",
                    SeoUrl = "Sperry-AO-3-Eye-Fleck-Canvas-Boat-Shoe",
                    PriceBase = 89.95m,
                    PriceSale = 89.95m,
                    IsVisible = true,
                    IsDiscontinued = false,
                    IsTaxable = true,
                    IsFreeShipping = true,
                    Weight = 0,
                    AvailableStartDate = null,
                    AvailableEndDate = null,
                    CreatedDate = createdDate,
                    UpdatedDate = null,
                    Active = true,
                    SeoKeywords = "Sperry Top-Sider Men's A/O 3 Eye Fleck Canvas Boat Shoe,Keywords",
                    SeoDescription = "Sperry Top-Sider Men's A/O 3 Eye Fleck Canvas Boat Shoe,Description",
                    SeoPageTitle = "Sperry Top-Sider Men's A/O 3 Eye Fleck Canvas Boat Shoe",
                    PackageID = context.Packages.Where(p => p.Name == "Default").Select(p => p.ID).FirstOrDefault(),
                    Categories = new List<ProductCategory>
                    {
                        new()
                        {
                            SlaveID = context.Categories.Where(c => c.CustomKey == "CAT-7").Select(c => c.ID).First(),
                            CreatedDate = createdDate,
                            Active = true,
                        },
                    },
                    ProductInventoryLocationSections = new[]
                    {
                        new ProductInventoryLocationSection
                        {
                            Active = true,
                            CreatedDate = createdDate,
                            SlaveID = context.InventoryLocationSections.First(x => x.CustomKey == "WAREHOUSE-1-Section-2").ID,
                            Quantity = 5,
                        },
                    },
                });
                context.SaveUnitOfWork();
            }
            if (context?.Products?.Any(x => x.CustomKey == "GPL-321CB") == false)
            {
                context.Products.Add(new()
                {
                    TypeID = context.ProductTypes.Where(pt => pt.Active && pt.Name == "General").Select(pt => pt.ID).First(),
                    StatusID = context.ProductStatuses.Where(pt => pt.Active && pt.Name == "Normal").Select(pt => pt.ID).First(),
                    CustomKey = "GPL-321CB",
                    Name = "Microfiber Cleaning Cloth 24 Pack",
                    ShortDescription = "Ultra soft, non-abrasive microfiber cloths will not scratch paints, coats or other surfaces",
                    Description = "Ultra soft, non-abrasive microfiber cloths will not scratch paints, coats or other surfaces",
                    SeoUrl = "Microfiber-Cleaning-Cloth-24-Pack",
                    PriceBase = 14.99m,
                    PriceSale = 14.99m,
                    IsVisible = true,
                    IsDiscontinued = false,
                    IsTaxable = true,
                    IsFreeShipping = true,
                    Weight = 0,
                    AvailableStartDate = null,
                    AvailableEndDate = null,
                    CreatedDate = createdDate,
                    UpdatedDate = null,
                    Active = true,
                    SeoKeywords = "Microfiber Cleaning Cloth 24 Pack,Keywords",
                    SeoDescription = "Microfiber Cleaning Cloth 24 Pack,Description",
                    SeoPageTitle = "Microfiber Cleaning Cloth 24 Pack",
                    PackageID = context.Packages.Where(p => p.Name == "Default").Select(p => p.ID).FirstOrDefault(),
                    Categories = new List<ProductCategory>
                    {
                        new()
                        {
                            SlaveID = context.Categories.Where(c => c.CustomKey == "CAT-8").Select(c => c.ID).First(),
                            CreatedDate = createdDate,
                            Active = true,
                        },
                    },
                    ProductInventoryLocationSections = new[]
                    {
                        new ProductInventoryLocationSection
                        {
                            Active = true,
                            CreatedDate = createdDate,
                            SlaveID = context.InventoryLocationSections.First(x => x.CustomKey == "WAREHOUSE-1-Section-2").ID,
                            Quantity = 5,
                        },
                    },
                });
                context.SaveUnitOfWork();
            }
            if (context?.Products?.Any(x => x.CustomKey == "GPL-204B") == false)
            {
                context.Products.Add(new()
                {
                    TypeID = context.ProductTypes.Where(pt => pt.Active && pt.Name == "General").Select(pt => pt.ID).First(),
                    StatusID = context.ProductStatuses.Where(pt => pt.Active && pt.Name == "Normal").Select(pt => pt.ID).First(),
                    CustomKey = "GPL-204B",
                    Name = "Keeper 07203-1 Waterproof Roof Top Cargo Bag",
                    ShortDescription = "Waterproof design protects cargo against road grit, sun, wind and rain",
                    Description = "Waterproof design protects cargo against road grit, sun, wind and rain",
                    SeoUrl = "Keeper-07203-1-Waterproof-Roof-Top-Cargo-Bag",
                    PriceBase = 51.99m,
                    PriceSale = 51.99m,
                    IsVisible = true,
                    IsDiscontinued = false,
                    IsTaxable = true,
                    IsFreeShipping = true,
                    Weight = 0,
                    AvailableStartDate = null,
                    AvailableEndDate = null,
                    CreatedDate = createdDate,
                    UpdatedDate = null,
                    Active = true,
                    SeoKeywords = "Keeper 07203-1 Waterproof Roof Top Cargo Bag,Keywords",
                    SeoDescription = "Keeper 07203-1 Waterproof Roof Top Cargo Bag,Description",
                    SeoPageTitle = "Keeper 07203-1 Waterproof Roof Top Cargo Bag",
                    PackageID = context.Packages.Where(p => p.Name == "Default").Select(p => p.ID).FirstOrDefault(),
                    Categories = new List<ProductCategory>
                    {
                        new()
                        {
                            SlaveID = context.Categories.Where(c => c.CustomKey == "CAT-8").Select(c => c.ID).First(),
                            CreatedDate = createdDate,
                            Active = true,
                        },
                    },
                    ProductInventoryLocationSections = new[]
                    {
                        new ProductInventoryLocationSection
                        {
                            Active = true,
                            CreatedDate = createdDate,
                            SlaveID = context.InventoryLocationSections.First(x => x.CustomKey == "WAREHOUSE-1-Section-2").ID,
                            Quantity = 5,
                        },
                    },
                });
                context.SaveUnitOfWork();
            }
            if (context?.Products?.Any(x => x.CustomKey == "166734") == false)
            {
                context.Products.Add(new()
                {
                    TypeID = context.ProductTypes.Where(pt => pt.Active && pt.Name == "General").Select(pt => pt.ID).First(),
                    StatusID = context.ProductStatuses.Where(pt => pt.Active && pt.Name == "Normal").Select(pt => pt.ID).First(),
                    CustomKey = "166734",
                    Name = "Prestone AS257 Bug Wash Windshield Washer Fluid - 1 Gallon",
                    ShortDescription = "Formulated with ingredients to remove bug residue, road grime, bird droppings and tree sap from windshields",
                    Description = "Formulated with ingredients to remove bug residue, road grime, bird droppings and tree sap from windshields",
                    SeoUrl = "Prestone-AS257-Bug-Wash-Windshield-Washer-Fluid-1-Gallon",
                    PriceBase = 26.27m,
                    PriceSale = 26.27m,
                    IsVisible = true,
                    IsDiscontinued = false,
                    IsTaxable = true,
                    IsFreeShipping = true,
                    Weight = 0,
                    AvailableStartDate = null,
                    AvailableEndDate = null,
                    CreatedDate = createdDate,
                    UpdatedDate = null,
                    Active = true,
                    SeoKeywords = "Prestone AS257 Bug Wash Windshield Washer Fluid - 1 Gallon,Keywords",
                    SeoDescription = "Prestone AS257 Bug Wash Windshield Washer Fluid - 1 Gallon,Description",
                    SeoPageTitle = "Prestone AS257 Bug Wash Windshield Washer Fluid - 1 Gallon",
                    PackageID = context.Packages.Where(p => p.Name == "Default").Select(p => p.ID).FirstOrDefault(),
                    Categories = new List<ProductCategory>
                    {
                        new()
                        {
                            SlaveID = context.Categories.Where(c => c.CustomKey == "CAT-8").Select(c => c.ID).First(),
                            CreatedDate = createdDate,
                            Active = true,
                        },
                    },
                    ProductInventoryLocationSections = new[]
                    {
                        new ProductInventoryLocationSection
                        {
                            Active = true,
                            CreatedDate = createdDate,
                            SlaveID = context.InventoryLocationSections.First(x => x.CustomKey == "WAREHOUSE-1-Section-2").ID,
                            Quantity = 5,
                        },
                    },
                });
                context.SaveUnitOfWork();
            }
            if (context?.Products?.Any(x => x.CustomKey == "GPL-3431Y") == false)
            {
                context.Products.Add(new()
                {
                    TypeID = context.ProductTypes.Where(pt => pt.Active && pt.Name == "General").Select(pt => pt.ID).First(),
                    StatusID = context.ProductStatuses.Where(pt => pt.Active && pt.Name == "Normal").Select(pt => pt.ID).First(),
                    CustomKey = "GPL-3431Y",
                    Name = "Philips H3 CrystalVision Ultra Headlight Bulb",
                    ShortDescription = "Halogen Headlamps for the Look of HID Lighting",
                    Description = "Halogen Headlamps for the Look of HID Lighting",
                    SeoUrl = "Philips-H3-CrystalVision-Ultra-Headlight-Bulb",
                    PriceBase = 19.09m,
                    PriceSale = 19.09m,
                    IsVisible = true,
                    IsDiscontinued = false,
                    IsTaxable = true,
                    IsFreeShipping = true,
                    Weight = 0,
                    AvailableStartDate = null,
                    AvailableEndDate = null,
                    CreatedDate = createdDate,
                    UpdatedDate = null,
                    Active = true,
                    SeoKeywords = "Philips H3 CrystalVision Ultra Headlight Bulb,Keywords",
                    SeoDescription = "Philips H3 CrystalVision Ultra Headlight Bulb,Description",
                    SeoPageTitle = "Philips H3 CrystalVision Ultra Headlight Bulb",
                    PackageID = context.Packages.Where(p => p.Name == "Default").Select(p => p.ID).FirstOrDefault(),
                    Categories = new List<ProductCategory>
                    {
                        new()
                        {
                            SlaveID = context.Categories.Where(c => c.CustomKey == "CAT-8").Select(c => c.ID).First(),
                            CreatedDate = createdDate,
                            Active = true,
                        },
                    },
                    ProductInventoryLocationSections = new[]
                    {
                        new ProductInventoryLocationSection
                        {
                            Active = true,
                            CreatedDate = createdDate,
                            SlaveID = context.InventoryLocationSections.First(x => x.CustomKey == "WAREHOUSE-1-Section-2").ID,
                            Quantity = 5,
                        },
                    },
                });
                context.SaveUnitOfWork();
            }
            if (context?.Products?.Any(x => x.CustomKey == "GPL-887BK") == false)
            {
                context.Products.Add(new()
                {
                    TypeID = context.ProductTypes.Where(pt => pt.Active && pt.Name == "General").Select(pt => pt.ID).First(),
                    StatusID = context.ProductStatuses.Where(pt => pt.Active && pt.Name == "Normal").Select(pt => pt.ID).First(),
                    CustomKey = "GPL-887BK",
                    Name = "Trimax UMAX50 Premium Universal 'Solid Hardened Steel' Trailer Lock",
                    ShortDescription = "Type A key which is a spring loaded, 7 pin, high security key. Resists attempted drill outs. Rugged and durable, the key will not bend or break.",
                    Description = "Type A key which is a spring loaded, 7 pin, high security key. Resists attempted drill outs. Rugged and durable, the key will not bend or break.",
                    SeoUrl = "Trimax-UMAX50-Premium-Universal-Trailer-Lock",
                    PriceBase = 30.15m,
                    PriceSale = 30.15m,
                    IsVisible = true,
                    IsDiscontinued = false,
                    IsTaxable = true,
                    IsFreeShipping = true,
                    Weight = 0,
                    AvailableStartDate = null,
                    AvailableEndDate = null,
                    CreatedDate = createdDate,
                    UpdatedDate = null,
                    Active = true,
                    SeoKeywords = "Trimax UMAX50 Premium Universal 'Solid Hardened Steel' Trailer Lock,Keywords",
                    SeoDescription = "Trimax UMAX50 Premium Universal 'Solid Hardened Steel' Trailer Lock,Description",
                    SeoPageTitle = "Trimax UMAX50 Premium Universal 'Solid Hardened Steel' Trailer Lock ",
                    PackageID = context.Packages.Where(p => p.Name == "Default").Select(p => p.ID).FirstOrDefault(),
                    Categories = new List<ProductCategory>
                    {
                        new()
                        {
                            SlaveID = context.Categories.Where(c => c.CustomKey == "CAT-8").Select(c => c.ID).First(),
                            CreatedDate = createdDate,
                            Active = true,
                        },
                    },
                    ProductInventoryLocationSections = new[]
                    {
                        new ProductInventoryLocationSection
                        {
                            Active = true,
                            CreatedDate = createdDate,
                            SlaveID = context.InventoryLocationSections.First(x => x.CustomKey == "WAREHOUSE-1-Section-2").ID,
                            Quantity = 5,
                        },
                    },
                });
                context.SaveUnitOfWork();
            }
        }
    }
}
