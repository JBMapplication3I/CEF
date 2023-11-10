// <copyright file="SampleData.Vendors.cs" company="clarity-ventures.com">
// Copyright (c) 2016-2023 clarity-ventures.com. All rights reserved.
// </copyright>
// <summary>Implements the sample data vendors class</summary>
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
        private void AddSampleVendors(DateTime createdDate)
        {
            if (context?.Vendors == null)
            {
                return;
            }
            var index = 1;
            if (!context.Vendors.Any(x => x.CustomKey == "VENDOR-" + index))
            {
                context.Vendors.Add(new()
                {
                    Active = true,
                    CreatedDate = createdDate,
                    CustomKey = "VENDOR-" + index,
                    Name = "Vendor " + index,
                    AllowDropShip = true,
                    TypeID = 1,
                    Contact = new()
                    {
                        Active = true,
                        CreatedDate = createdDate,
                        CustomKey = "VENDOR-" + index + "-CONTACT-1",
                        Phone1 = "555-555-1234",
                        Email1 = "rep@vendor" + index + ".com",
                        TypeID = 9,
                        Address = new()
                        {
                            Active = true,
                            CreatedDate = createdDate,
                            Street1 = "11" + index + " Fake Vendor Ln",
                            City = "Fake City",
                            RegionID = context.Regions.First(x => x.Name == "Texas").ID,
                            PostalCode = "78759",
                            CountryID = context.Countries.First(x => x.Name == "United States of America").ID,
                            CustomKey = "VENDOR-" + index + "-ADDRESS-1",
                        },
                    },
                });
                context.SaveUnitOfWork();
            }
            index++;
            // ReSharper disable once InvertIf
            if (!context.Vendors.Any(x => x.CustomKey == "VENDOR-" + index))
            {
                context.Vendors.Add(new()
                {
                    Active = true,
                    CreatedDate = createdDate,
                    CustomKey = "VENDOR-" + index,
                    Name = "Vendor " + index,
                    AllowDropShip = false,
                    TypeID = 1,
                    Contact = new()
                    {
                        Active = true,
                        CreatedDate = createdDate,
                        CustomKey = "VENDOR-" + index + "-CONTACT-1",
                        Phone1 = "555-555-1234",
                        Email1 = "rep@vendor" + index + ".com",
                        TypeID = 9,
                        Address = new()
                        {
                            Active = true,
                            CreatedDate = createdDate,
                            Street1 = "11" + index + " Fake Vendor Ln",
                            City = "Fake City",
                            RegionID = context.Regions.First(x => x.Name == "Texas").ID,
                            PostalCode = "78759",
                            CountryID = context.Countries.First(x => x.Name == "United States of America").ID,
                            CustomKey = "VENDOR-" + index + "-ADDRESS-1",
                        },
                    },
                });
                context.SaveUnitOfWork();
            }
        }
    }
}
