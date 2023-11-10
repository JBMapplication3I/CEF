// <copyright file="SampleData.cs" company="clarity-ventures.com">
// Copyright (c) 2016-2023 clarity-ventures.com. All rights reserved.
// </copyright>
// <summary>Implements the sample data class</summary>
// ReSharper disable PossibleInvalidOperationException
#nullable enable
#if ORACLE
namespace Clarity.Ecommerce.DataModel.Oracle.DataSets
#else
namespace Clarity.Ecommerce.DataModel.DataSets
#endif
{
    using System;

    public partial class SampleData
    {
#if ORACLE
        private readonly OracleClarityEcommerceEntities context;
#else
        private readonly ClarityEcommerceEntities context;
#endif

#if ORACLE
        public SampleData(OracleClarityEcommerceEntities context)
#else
        public SampleData(ClarityEcommerceEntities context)
#endif
        {
            this.context = context;
        }

        public void Populate()
        {
            var createdDate = new DateTime(2020, 1, 1);
            AddSampleAttributes(createdDate);
            AddSampleAccounts(createdDate);
            AddSampleUsers(createdDate);
            AddSampleCategories(createdDate);
            AddSampleInventoryLocations(createdDate);
            AddSampleManufacturers(createdDate);
            AddSampleVendors(createdDate);
            AddSampleProducts(createdDate);
            AddSampleStores(createdDate);
            AddSampleFranchises(createdDate);
            AddSampleBrands(createdDate);
            AddSampleSalesOrders(createdDate);
            AddSampleSalesQuotes(createdDate);
            AddSampleSalesInvoices(createdDate);
            AddSamplePriceRules(createdDate);
        }
    }
}
