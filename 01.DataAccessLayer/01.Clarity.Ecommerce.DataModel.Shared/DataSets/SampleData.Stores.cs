// <copyright file="SampleData.Stores.cs" company="clarity-ventures.com">
// Copyright (c) 2016-2023 clarity-ventures.com. All rights reserved.
// </copyright>
// <summary>Implements the sample data. stores class</summary>
// ReSharper disable PossibleInvalidOperationException
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
        private Address CreateStoreAddress(DateTime createdDate)
        {
            return new()
            {
                CustomKey = "STORE-0001-OFFICE",
                Street1 = "300 N Lamar",
                Street2 = "Apt. 1111",
                Street3 = null,
                City = "Austin",
                RegionID = context.Regions.Where(r => r.Name == "Texas").Select(r => r.ID).First(),
                CountryID = context.Countries.Where(c => c.Name == "United States of America").Select(c => c.ID).First(),
                PostalCode = "78703",
                Latitude = null,
                Longitude = null,
                CreatedDate = createdDate,
                Active = true,
            };
        }

        private Contact CreateStoreContact(DateTime createdDate)
        {
            return new()
            {
                CreatedDate = createdDate,
                Active = true,
                CustomKey = "Store #0001 Main Office",
                Address = CreateStoreAddress(createdDate),
                Email1 = "bobs.auto.shop@email.com",
                Phone1 = "1-555-343-4322",
                Fax1 = "1-555-533-1234",
                FullName = "Bob Jenkins",
                FirstName = "Bob",
                LastName = "Jenkins",
                TypeID = context.ContactTypes.First(x => x.Name == "Store").ID,
            };
        }

        private Store CreateStore(DateTime createdDate, string key, string name, int productIndex1, int productIndex2)
        {
            var account = context.Accounts.First(c => c.CustomKey == "ACCT-1121");
            var user = context.Users.First(c => c.CustomKey == "USER-0001");
            var type = context.StoreTypes.First(c => c.CustomKey == "General");
            var contact = CreateStoreContact(createdDate);
            var entity = new Store
            {
                Active = true,
                CreatedDate = createdDate,
                UpdatedDate = null,
                CustomKey = key,
                Name = name,
                Description = null,
                Contact = contact,
                Type = type,
                OperatingHoursTimeZoneId = "Central Daylight Time",
                OperatingHoursMondayStart = 8,
                OperatingHoursMondayEnd = 5,
                OperatingHoursTuesdayStart = 8,
                OperatingHoursTuesdayEnd = 5,
                OperatingHoursWednesdayStart = 8,
                OperatingHoursWednesdayEnd = 5,
                OperatingHoursThursdayStart = 8,
                OperatingHoursThursdayEnd = 5,
                OperatingHoursFridayStart = 8,
                OperatingHoursFridayEnd = 5,
                OperatingHoursSaturdayStart = 10,
                OperatingHoursSaturdayEnd = 3,
                OperatingHoursClosedStatement = "We are currently closed",
                Slogan = "Come to us for excellent service!",
                MissionStatement = "Our mission is your mission",
                Users = new[]
                {
                    new StoreUser
                    {
                        Active = true,
                        CreatedDate = createdDate,
                        UpdatedDate = null,
                        SlaveID = user.ID,
                    },
                },
                Accounts = new[]
                {
                    new StoreAccount
                    {
                        Active = true,
                        CreatedDate = createdDate,
                        UpdatedDate = null,
                        SlaveID = account.ID,
                    },
                },
                Products = new[]
                {
                    new StoreProduct
                    {
                        Active = true,
                        CreatedDate = createdDate,
                        UpdatedDate = null,
                        SlaveID = context.Products.Select(x => x.ID).ToList()[productIndex1],
                        IsVisibleIn = true,
                    },
                    new StoreProduct
                    {
                        Active = true,
                        CreatedDate = createdDate,
                        UpdatedDate = null,
                        SlaveID = context.Products.Select(x => x.ID).ToList()[productIndex2],
                        IsVisibleIn = false,
                    },
                },
            };
            return entity;
        }

        private void AddSampleStores(DateTime createdDate)
        {
            if (context?.Stores?.Any() == true)
            {
                return;
            }
            context!.Stores!.Add(CreateStore(createdDate: createdDate, key: "STORE-0001", name: "Bob's Auto Shop", productIndex1: 1, productIndex2: 2));
            context.Stores.Add(CreateStore(createdDate: createdDate, key: "STORE-0002", name: "Jane's Antique Store", productIndex1: 3, productIndex2: 4));
            context.SaveUnitOfWork(true);
        }
    }
}
