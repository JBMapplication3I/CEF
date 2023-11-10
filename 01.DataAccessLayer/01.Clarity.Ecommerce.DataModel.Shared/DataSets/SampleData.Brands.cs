// <copyright file="SampleData.Brands.cs" company="clarity-ventures.com">
// Copyright (c) 2020-2023 clarity-ventures.com. All rights reserved.
// </copyright>
// <summary>Implements the sample data. brands class</summary>
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
        private Brand CreateBrand(DateTime createdDate, string key, string name, int productIndex1, int productIndex2)
        {
            var account = context.Accounts.First(c => c.CustomKey == "ACCT-1121");
            var user = context.Users.First(c => c.CustomKey == "USER-0001");
            var entity = new Brand
            {
                Active = true,
                CreatedDate = createdDate,
                UpdatedDate = null,
                CustomKey = key,
                Name = name,
                Description = null,
                Users = new[]
                {
                    new BrandUser
                    {
                        Active = true,
                        CreatedDate = createdDate,
                        UpdatedDate = null,
                        SlaveID = user.ID,
                    },
                },
                Accounts = new[]
                {
                    new BrandAccount
                    {
                        Active = true,
                        CreatedDate = createdDate,
                        UpdatedDate = null,
                        SlaveID = account.ID,
                    },
                },
                Products = new[]
                {
                    new BrandProduct
                    {
                        Active = true,
                        CreatedDate = createdDate,
                        UpdatedDate = null,
                        SlaveID = context.Products.Select(x => x.ID).ToList()[productIndex1],
                        IsVisibleIn = true,
                    },
                    new BrandProduct
                    {
                        Active = true,
                        CreatedDate = createdDate,
                        UpdatedDate = null,
                        SlaveID = context.Products.Select(x => x.ID).ToList()[productIndex2],
                        IsVisibleIn = false,
                    },
                },
                Categories = context.Categories
                    .Where(x => x.Active)
                    .ToList()
                    .Select(x => new BrandCategory
                    {
                        Active = true,
                        CreatedDate = createdDate,
                        UpdatedDate = null,
                        SlaveID = x.ID,
                    })
                    .ToList(),
            };
            return entity;
        }

        private void AddSampleBrands(DateTime createdDate)
        {
            if (context?.Brands?.Any() == true)
            {
                return;
            }
            context!.Brands!.Add(CreateBrand(createdDate: createdDate, key: "BRAND-0001", name: "Bob's Auto Shop", productIndex1: 1, productIndex2: 2));
            context.Brands.Add(CreateBrand(createdDate: createdDate, key: "BRAND-0002", name: "Jane's Antique Brand", productIndex1: 3, productIndex2: 4));
            context.SaveUnitOfWork(true);
        }
    }
}
