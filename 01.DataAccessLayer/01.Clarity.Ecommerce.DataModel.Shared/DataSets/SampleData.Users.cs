// <copyright file="SampleData.Users.cs" company="clarity-ventures.com">
// Copyright (c) 2016-2023 clarity-ventures.com. All rights reserved.
// </copyright>
// <summary>Implements the sample data. users class</summary>
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
        private void AddSampleUsers(DateTime createdDate)
        {
            if (!context?.Users?.Any(x => x.CustomKey == "USER-0001") == true)
            {
                context!.Users!.Add(new()
                {
                    CreatedDate = createdDate,
                    Active = true,
                    CustomKey = "USER-0001",
                    UserName = "rhalversen",
                    AccountID = context.Accounts.Where(x => x.CustomKey == "ACCT-1121").Select(x => x.ID).FirstOrDefault(),
                    StatusID = context.UserStatuses.Where(x => x.Name == "Registered").Select(x => x.ID).FirstOrDefault(),
                    TypeID = context.UserTypes.Where(x => x.Name == "Customer").Select(x => x.ID).FirstOrDefault(),
                    Contact = new()
                    {
                        CreatedDate = createdDate,
                        Active = true,
                        Phone1 = "1-888-987-3755",
                        TypeID = 1,
                        Address = new()
                        {
                            CreatedDate = createdDate,
                            Active = true,
                            Company = "HQ",
                            Street1 = "9442 N. Capital Of Texas Hwy",
                            Street2 = "Suite 925",
                            City = "Austin",
                            RegionID = context.Regions.Where(r => r.Active && r.Name == "Texas").Select(r => r.ID).FirstOrDefault(),
                            CountryID = context.Countries.Where(c => c.Active && c.Name == "United States of America").Select(c => c.ID).FirstOrDefault(),
                            PostalCode = "78759",
                        },
                    },
                });
                context.SaveUnitOfWork();
            }
        }
    }
}
