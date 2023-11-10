// <copyright file="SampleData.Inventory.cs" company="clarity-ventures.com">
// Copyright (c) 2016-2023 clarity-ventures.com. All rights reserved.
// </copyright>
// <summary>Implements the sample data. inventory class</summary>
// ReSharper disable PossibleInvalidOperationException
#nullable enable
#if ORACLE
namespace Clarity.Ecommerce.DataModel.Oracle.DataSets
#else
namespace Clarity.Ecommerce.DataModel.DataSets
#endif
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using Utilities;

    public partial class SampleData
    {
        private void AddSampleInventoryLocations(DateTime createdDate)
        {
            if (context?.InventoryLocations == null)
            {
                return;
            }
            var index = 1;
            if (!context.InventoryLocations.Any(x => x.CustomKey == "WAREHOUSE-" + index))
            {
                context.InventoryLocations.Add(new()
                {
                    Active = true,
                    CreatedDate = createdDate,
                    CustomKey = "WAREHOUSE-" + index,
                    Name = "Warehouse " + index,
                    Contact = new()
                    {
                        Active = true,
                        CreatedDate = createdDate,
                        CustomKey = "WAREHOUSE-" + index + "-CONTACT-1",
                        Phone1 = "555-555-1234",
                        Email1 = "rep@warehouse" + index + ".com",
                        TypeID = 7,
                        Address = new()
                        {
                            Active = true,
                            CreatedDate = createdDate,
                            Street1 = "11" + index + " Fake Warehouse Ln",
                            City = "Fake City",
                            RegionID = context.Regions.First(x => x.Name == "Texas").ID,
                            PostalCode = "78759",
                            CountryID = context.Countries.First(x => x.Name == "United States of America").ID,
                            CustomKey = "WAREHOUSE-" + index + "-ADDRESS-1",
                        },
                    },
                    Sections = new HashSet<InventoryLocationSection>
                    {
                        new()
                        {
                            Active = true,
                            CreatedDate = createdDate,
                            CustomKey = "WAREHOUSE-" + index + "-Section-1",
                            Name = "Section 1",
                        },
                        new()
                        {
                            Active = true,
                            CreatedDate = createdDate,
                            CustomKey = "WAREHOUSE-" + index + "-Section-2",
                            Name = "Section 2",
                        },
                    },
                });
                context.SaveUnitOfWork();
            }
            index++;
            // ReSharper disable once InvertIf
            if (!context.InventoryLocations.Any(x => x.CustomKey == "WAREHOUSE-" + index))
            {
                context.InventoryLocations.Add(new()
                {
                    Active = true,
                    CreatedDate = createdDate,
                    CustomKey = "WAREHOUSE-" + index,
                    Name = "Warehouse " + index,
                    Contact = new()
                    {
                        Active = true,
                        CreatedDate = createdDate,
                        CustomKey = "WAREHOUSE-" + index + "-CONTACT-1",
                        Phone1 = "555-555-1234",
                        Email1 = "rep@warehouse" + index + ".com",
                        TypeID = 7,
                        Address = new()
                        {
                            Active = true,
                            CreatedDate = createdDate,
                            Street1 = "11" + index + " Fake Warehouse Ln",
                            City = "Fake City",
                            RegionID = context.Regions.First(x => x.Name == "Texas").ID,
                            PostalCode = "78759",
                            CountryID = context.Countries.First(x => x.Name == "United States of America").ID,
                            CustomKey = "WAREHOUSE-" + index + "-ADDRESS-1",
                        },
                    },
                    Sections = new HashSet<InventoryLocationSection>
                    {
                        new()
                        {
                            Active = true,
                            CreatedDate = createdDate,
                            CustomKey = "WAREHOUSE-" + index + "-Shelf-1",
                            Name = "Shelf 1",
                        },
                        new()
                        {
                            Active = true,
                            CreatedDate = createdDate,
                            CustomKey = "WAREHOUSE-" + index + "-Shelf-2",
                            Name = "Shelf 2",
                        },
                    },
                });
                context.SaveUnitOfWork();
            }
        }
    }
}
