// <copyright file="SampleData.Franchises.cs" company="clarity-ventures.com">
// Copyright (c) 2020-2023 clarity-ventures.com. All rights reserved.
// </copyright>
// <summary>Implements the sample data. franchises class</summary>
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
        private Franchise CreateFranchise(DateTime createdDate, string key, string name, int productIndex1, int productIndex2)
        {
            var account = context.Accounts.First(c => c.CustomKey == "ACCT-1121");
            var user = context.Users.First(c => c.CustomKey == "USER-0001");
            var type = context.FranchiseTypes.First(c => c.CustomKey == "General");
            var entity = new Franchise
            {
                Active = true,
                CreatedDate = createdDate,
                UpdatedDate = null,
                CustomKey = key,
                Name = name,
                Description = null,
                Type = type,
                Users = new[]
                {
                    new FranchiseUser
                    {
                        Active = true,
                        CreatedDate = createdDate,
                        UpdatedDate = null,
                        SlaveID = user.ID,
                    },
                },
                Accounts = new[]
                {
                    new FranchiseAccount
                    {
                        Active = true,
                        CreatedDate = createdDate,
                        UpdatedDate = null,
                        SlaveID = account.ID,
                    },
                },
                Products = new[]
                {
                    new FranchiseProduct
                    {
                        Active = true,
                        CreatedDate = createdDate,
                        UpdatedDate = null,
                        SlaveID = context.Products.Select(x => x.ID).ToList()[productIndex1],
                        IsVisibleIn = true,
                    },
                    new FranchiseProduct
                    {
                        Active = true,
                        CreatedDate = createdDate,
                        UpdatedDate = null,
                        SlaveID = context.Products.Select(x => x.ID).ToList()[productIndex2],
                        IsVisibleIn = false,
                    },
                },
            };
            entity.Categories = context.Categories
                .Where(x => x.Active)
                .ToList()
                .Select(x => new FranchiseCategory
                {
                    Active = true,
                    CreatedDate = createdDate,
                    UpdatedDate = null,
                    SlaveID = x.ID,
                })
                .ToList();
            return entity;
        }

        private void AddSampleFranchises(DateTime createdDate)
        {
            if (context?.Franchises?.Any() != true)
            {
                return;
            }
            context.Franchises.Add(CreateFranchise(createdDate: createdDate, key: "FRANCHISE-0001", name: "Bob's Auto Shop", productIndex1: 1, productIndex2: 2));
            context.Franchises.Add(CreateFranchise(createdDate: createdDate, key: "FRANCHISE-0002", name: "Jane's Antique Franchise", productIndex1: 3, productIndex2: 4));
            context.SaveUnitOfWork(true);
        }
    }
}
