// <copyright file="SampleData.PriceRules.cs" company="clarity-ventures.com">
// Copyright (c) 2016-2023 clarity-ventures.com. All rights reserved.
// </copyright>
// <summary>Implements the sample data. price rules class</summary>
namespace Clarity.Ecommerce.DataModel.DataSets
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using Utilities;

    public partial class SampleData
    {
        private void CreatePriceRule(DateTime createdDate, string key)
        {
            var brandId = context.Brands.First(x => x.CustomKey == "BRAND-0001").ID;
            var storeId = context.Stores.First(x => x.CustomKey == "STORE-0001").ID;
            var vendorId = context.Vendors.First(x => x.CustomKey == "VENDOR-1").ID;
            var accountId = context.Accounts.First(x => x.CustomKey == "ACCT-1121").ID;
            var productId = context.Products.First(x => x.CustomKey == "432957").ID;
            var manufacturerId = context.Manufacturers.First(x => x.CustomKey == "MANUFACTURER-1").ID;
            var countryId = context.Countries.First(x => x.CustomKey == "United States of America").ID;
            var userRoleId = context.RoleUsers.First(x => x.RoleId == 1).RoleId;
            var categoryId = context.Categories.First(x => x.CustomKey == "CAT-1").ID;
            var accountTypeId = context.AccountTypes.First(x => x.CustomKey == "VENDOR").ID;
            var productTypeId = context.ProductTypes.First(x => x.CustomKey == "GENERAL").ID;
            context.PriceRules.Add(new PriceRule
            {
                CustomKey = key,
                CreatedDate = createdDate,
                Active = true,
                Name = "New Customer Price Rule",
                Description = "10% Off For New Customers",
                Brands = new List<PriceRuleBrand>()
                {
                    new PriceRuleBrand
                    {
                        CustomKey = key,
                        CreatedDate = createdDate,
                        Active = true,
                        SlaveID = brandId,
                    },
                },
                Stores = new List<PriceRuleStore>()
                {
                    new PriceRuleStore
                    {
                        CustomKey = key,
                        CreatedDate = createdDate,
                        Active = true,
                        SlaveID = storeId,
                    },
                },
                Vendors = new List<PriceRuleVendor>()
                {
                    new PriceRuleVendor
                    {
                        CustomKey = key,
                        CreatedDate = createdDate,
                        Active = true,
                        SlaveID = vendorId,
                    },
                },
                Accounts = new List<PriceRuleAccount>()
                {
                    new PriceRuleAccount
                    {
                        CustomKey = key,
                        CreatedDate = createdDate,
                        Active = true,
                        SlaveID = accountId,
                    },
                },
                Products = new List<PriceRuleProduct>()
                {
                    new PriceRuleProduct
                    {
                        CustomKey = key,
                        CreatedDate = createdDate,
                        Active = true,
                        SlaveID = productId,
                        OverridePrice = false,
                        OverrideBasePrice = null,
                        OverrideSalePrice = null,
                    },
                },
                Manufacturers = new List<PriceRuleManufacturer>()
                {
                    new PriceRuleManufacturer
                    {
                        CustomKey = key,
                        CreatedDate = createdDate,
                        Active = true,
                        SlaveID = manufacturerId,
                    },
                },
                PriceRuleCountries = new List<PriceRuleCountry>()
                {
                    new PriceRuleCountry
                    {
                        CustomKey = key,
                        CreatedDate = createdDate,
                        Active = true,
                        SlaveID = countryId,
                    },
                },
                PriceRuleUserRoles = new List<PriceRuleUserRole>()
                {
                    new PriceRuleUserRole
                    {
                        CustomKey = key,
                        CreatedDate = createdDate,
                        Active = true,
                        RoleName = null,
                        PriceRuleID = userRoleId,
                    },
                },
                PriceRuleCategories = new List<PriceRuleCategory>()
                {
                    new PriceRuleCategory
                    {
                        CustomKey = key,
                        CreatedDate = createdDate,
                        Active = true,
                        SlaveID = categoryId,
                    },
                },
                PriceRuleAccountTypes = new List<PriceRuleAccountType>()
                {
                    new PriceRuleAccountType
                    {
                        CustomKey = key,
                        CreatedDate = createdDate,
                        Active = true,
                        SlaveID = accountTypeId,
                    },
                },
                PriceRuleProductTypes = new List<PriceRuleProductType>()
                {
                    new PriceRuleProductType
                    {
                        CustomKey = key,
                        CreatedDate = createdDate,
                        Active = true,
                        SlaveID = productTypeId,
                    },
                },
                StartDate = DateTime.Today,
                EndDate = null,
                UnitOfMeasure = null,
                PriceAdjustment = .01m,
                MinQuantity = 1,
                MaxQuantity = 4,
                IsPercentage = true,
                IsMarkup = false,
                UsePriceBase = false,
                IsExclusive = false,
                IsOnlyForAnonymousUsers = false,
                Priority = null,
                CurrencyID = null,
                Currency = null,
            });
        }

        private void AddSamplePriceRules(DateTime createdDate)
        {
            if (context?.PriceRules?.Any() == true)
            {
                return;
            }
            CreatePriceRule(DateTime.Today, "RULE-0001");
            context!.SaveUnitOfWork();
        }
    }
}
