// <copyright file="SampleData.Manufacturers.cs" company="clarity-ventures.com">
// Copyright (c) 2016-2023 clarity-ventures.com. All rights reserved.
// </copyright>
// <summary>Implements the sample data manufacturers class</summary>
#nullable enable
#if ORACLE
namespace Clarity.Ecommerce.DataModel.Oracle.DataSets
#else
namespace Clarity.Ecommerce.DataModel.DataSets
#endif
{
    using System;
    using System.Linq;
    using Utilities;

    public partial class SampleData
    {
        private void AddSampleManufacturers(DateTime createdDate)
        {
            if (context?.Manufacturers == null)
            {
                return;
            }
            var index = 1;
            if (!context.Manufacturers.Any(x => x.CustomKey == "MANUFACTURER-" + index))
            {
                context.Manufacturers.Add(new()
                {
                    Active = true,
                    CreatedDate = createdDate,
                    CustomKey = "MANUFACTURER-" + index,
                    Name = "Manufacturer " + index,
                    TypeID = 1,
                    Contact = new()
                    {
                        Active = true,
                        CreatedDate = createdDate,
                        Phone1 = "555-555-1234",
                        Email1 = "rep@manufacturer" + index + ".com",
                        TypeID = 8,
                        Address = new()
                        {
                            Active = true,
                            CreatedDate = createdDate,
                            CustomKey = "MANUFACTURER-" + index + "-ADDRESS-1",
                            Street1 = "11" + index + " Fake Manufacturer Ln",
                            City = "Fake City",
                            RegionID = context.Regions.First(x => x.Name == "Texas").ID,
                            PostalCode = "78759",
                            CountryID = context.Countries.First(x => x.Name == "United States of America").ID,
                        },
                    },
                });
                context.SaveUnitOfWork();
            }
            index++;
            if (!context.Manufacturers.Any(x => x.CustomKey == "MANUFACTURER-" + index))
            {
                context.Manufacturers.Add(new()
                {
                    Active = true,
                    CreatedDate = createdDate,
                    CustomKey = "MANUFACTURER-" + index,
                    Name = "Manufacturer " + index,
                    TypeID = 1,
                    Contact = new()
                    {
                        Active = true,
                        CreatedDate = createdDate,
                        Phone1 = "555-555-1234",
                        Email1 = "rep@manufacturer" + index + ".com",
                        TypeID = 8,
                        Address = new()
                        {
                            Active = true,
                            CreatedDate = createdDate,
                            CustomKey = "MANUFACTURER-" + index + "-ADDRESS-1",
                            Street1 = "11" + index + " Fake Manufacturer Ln",
                            City = "Fake City",
                            RegionID = context.Regions.First(x => x.Name == "Texas").ID,
                            PostalCode = "78759",
                            CountryID = context.Countries.First(x => x.Name == "United States of America").ID,
                        },
                    },
                });
                context.SaveUnitOfWork();
            }
        }
    }
}
