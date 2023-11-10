// <copyright file="StoreWorkflowTests.cs" company="clarity-ventures.com">
// Copyright (c) 2015-2023 clarity-ventures.com. All rights reserved.
// </copyright>
// <summary>Implements the store workflows tests class</summary>
// ReSharper disable InconsistentNaming, ObjectCreationAsStatement, ReturnValueOfPureMethodIsNotUsed
namespace Clarity.Ecommerce.Workflow.Testing
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using DataModel;
    using Interfaces.Models;
    using Models;
    using Xunit;

    public partial class Stores_Stores_StandardWorkflowTests
    {
        [Fact]
        public void Verify_Stores_FilterByZipCodeRadius_ReturnsProperResults()
        {
            Verify_Stores_FilterByZipCodeRadius_ReturnsProperResultsInner("78759", 01, "Miles", "");
            Verify_Stores_FilterByZipCodeRadius_ReturnsProperResultsInner("78759", 03, "Miles", "2");
            Verify_Stores_FilterByZipCodeRadius_ReturnsProperResultsInner("78759", 10, "Miles", "1,2,3");
            Verify_Stores_FilterByZipCodeRadius_ReturnsProperResultsInner("78701", 01, "Miles", "3");
        }

        private static void Verify_Stores_FilterByZipCodeRadius_ReturnsProperResultsInner(
            string zipCodeStr,
            int radius,
            string unitsStr,
            string storeIdsExpected)
        {
            // Arrange
            var zipCodes = new Dictionary<string, IZipCodeModel>
            {
                { "78701", LocatableZipCode(1, "78701", 30.2672292m, -97.7595272m) },
                { "78759", LocatableZipCode(2, "78759", 30.3990093m, -97.7935013m) },
            };
            var stores = new List<Store>
            {
                LocatableStore(1, "Austin Capitol", 30.2746652m, -97.7425392m),
                LocatableStore(2, "Clarity Ventures", 30.3903669m, -97.7509863m),
                LocatableStore(3, "78701 Centroid", 30.2672292m, -97.7595272m),
            };
            var zipCode = zipCodes[zipCodeStr];
            if (!Enum.TryParse(unitsStr, true, out Enums.LocatorUnits units))
            {
                throw new ArgumentException($"Invalid unit '{unitsStr}'", nameof(unitsStr));
            }
            var queryableStores = stores.AsQueryable();
            // Act
            var resultStores = queryableStores.FilterStoresByZipCodeRadius(
                zipCode?.Latitude,
                zipCode?.Longitude,
                radius,
                units);
            var resultStoreIds = string.Join(",", resultStores.Select(s => s.ID));
            // Assert
            Assert.Equal(storeIdsExpected, resultStoreIds);
        }

        private static ZipCodeModel LocatableZipCode(int id, string zipCode, decimal lat, decimal lng)
        {
            return new ZipCodeModel
            {
                ID = id,
                ZipCodeValue = zipCode,
                Latitude = lat,
                Longitude = lng,
            };
        }

        private static Store LocatableStore(int id, string name, decimal lat, decimal lng)
        {
            return new Store
            {
                ID = id,
                Name = name,
                Contact = new Contact
                {
                    Address = new Address
                    {
                        Latitude = lat,
                        Longitude = lng
                    }
                },
            };
        }
    }
}
