// <copyright file="SampleData.Accounts.cs" company="clarity-ventures.com">
// Copyright (c) 2016-2023 clarity-ventures.com. All rights reserved.
// </copyright>
// <summary>Implements the sample data. accounts class</summary>
// ReSharper disable CognitiveComplexity, FunctionComplexityOverflow, PossibleInvalidOperationException, StringLiteralTypo
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
        private void AddSampleAccount(DateTime createdDate, string key, string name)
        {
            if (context.Accounts.Any(x => x.CustomKey == key))
            {
                return;
            }
            if (key == "ACCT-1121")
            {
                context.Accounts.Add(new()
                {
                    TypeID = context.AccountTypes.Where(at => at.Active && at.Name == "Customer").Select(at => at.ID).FirstOrDefault(),
                    StatusID = 1,
                    CustomKey = key,
                    Name = name,
                    Description = $"This is an account for {name}.",
                    CreatedDate = createdDate,
                    Active = true,
                    AccountContacts = new[]
                    {
                        new AccountContact
                        {
                            CustomKey = key + "|BILL TO",
                            IsPrimary = false,
                            IsBilling = true,
                            CreatedDate = createdDate,
                            Active = true,
                            Slave = new()
                            {
                                Active = true,
                                CreatedDate = createdDate,
                                CustomKey = key + "|BILL TO",
                                FirstName = "Ron",
                                LastName = "Halversen",
                                Phone1 = "1-888-987-3565",
                                Fax1 = "1-512-596-4532",
                                Email1 = "ron.halversen@claritymis.com",
                                TypeID = context.ContactTypes.Where(r => r.Active && r.Name == "Account Address").Select(r => r.ID).FirstOrDefault(),
                                Address = new()
                                {
                                    Company = "HQ",
                                    CustomKey = "BILL TO",
                                    Street1 = "9442 N. Capital Of Texas Hwy",
                                    Street2 = "Suite 925",
                                    City = "Austin",
                                    RegionID = context.Regions.Where(r => r.Active && r.Name == "Texas").Select(r => r.ID).FirstOrDefault(),
                                    CountryID = context.Countries.Where(c => c.Active && c.Name == "United States of America").Select(c => c.ID).FirstOrDefault(),
                                    PostalCode = "78759",
                                    CreatedDate = createdDate,
                                    Active = true,
                                },
                            },
                        },
                        new AccountContact
                        {
                            CustomKey = key + "|SHIP TO",
                            IsPrimary = true,
                            IsBilling = false,
                            CreatedDate = createdDate,
                            Active = true,
                            Slave = new()
                            {
                                Active = true,
                                CreatedDate = createdDate,
                                CustomKey = key + "|BILL TO",
                                FirstName = "Ron",
                                LastName = "Halversen",
                                Phone1 = "1-888-987-3565",
                                Fax1 = "1-512-596-4532",
                                Email1 = "ron.halversen@claritymis.com",
                                TypeID = context.ContactTypes.Where(r => r.Active && r.Name == "Account Address").Select(r => r.ID).FirstOrDefault(),
                                Address = new()
                                {
                                    Company = "HQ",
                                    CustomKey = "SHIP TO",
                                    Street1 = "9442 N. Capital Of Texas Hwy",
                                    Street2 = "Suite 925",
                                    City = "Austin",
                                    RegionID = context.Regions.Where(r => r.Active && r.Name == "Texas").Select(r => r.ID).FirstOrDefault(),
                                    CountryID = context.Countries.Where(c => c.Active && c.Name == "United States of America").Select(c => c.ID).FirstOrDefault(),
                                    PostalCode = "78759",
                                    CreatedDate = createdDate,
                                    Active = true,
                                },
                            },
                        },
                    },
                });
                context.SaveUnitOfWork();
            }
            if (key == "ACCT-1122")
            {
                context.Accounts.Add(new()
                {
                    TypeID = context.AccountTypes.Where(at => at.Active && at.Name == "Customer").Select(at => at.ID).FirstOrDefault(),
                    StatusID = 1,
                    CustomKey = key,
                    Name = name,
                    Description = $"This is an account for {name}.",
                    CreatedDate = createdDate,
                    Active = true,
                    AccountContacts = new[]
                    {
                        new AccountContact
                        {
                            CustomKey = key + "|BILL TO",
                            IsPrimary = false,
                            IsBilling = true,
                            CreatedDate = createdDate,
                            Active = true,
                            Slave = new()
                            {
                                Active = true,
                                CreatedDate = createdDate,
                                CustomKey = key + "|BILL TO",
                                FirstName = "Jeff",
                                LastName = "Hendrickson",
                                Phone1 = "1-800-456-7822",
                                Fax1 = "1-210-255-4582",
                                Email1 = "jeff@boedekeremail.com",
                                TypeID = context.ContactTypes.Where(r => r.Active && r.Name == "Account Address").Select(r => r.ID).FirstOrDefault(),
                                Address = new()
                                {
                                    Company = "HQ",
                                    CustomKey = "BILL TO",
                                    Street1 = "904 6th St",
                                    City = "Shiner",
                                    RegionID = context.Regions.Where(r => r.Active && r.Name == "Texas").Select(r => r.ID).FirstOrDefault(),
                                    CountryID = context.Countries.Where(c => c.Active && c.Name == "United States of America").Select(c => c.ID).FirstOrDefault(),
                                    PostalCode = "77984",
                                    CreatedDate = createdDate,
                                    Active = true,
                                },
                            },
                        },
                        new AccountContact
                        {
                            CustomKey = key + "|SHIP TO",
                            IsPrimary = true,
                            IsBilling = false,
                            CreatedDate = createdDate,
                            Active = true,
                            Slave = new()
                            {
                                Active = true,
                                CreatedDate = createdDate,
                                CustomKey = key + "|BILL TO",
                                FirstName = "Jeff",
                                LastName = "Hendrickson",
                                Phone1 = "1-800-456-7822",
                                Fax1 = "1-210-255-4582",
                                Email1 = "jeff@boedekeremail.com",
                                TypeID = context.ContactTypes.Where(r => r.Active && r.Name == "Account Address").Select(r => r.ID).FirstOrDefault(),
                                Address = new()
                                {
                                    Company = "HQ",
                                    CustomKey = "SHIP TO",
                                    Street1 = "904 6th St",
                                    City = "Shiner",
                                    RegionID = context.Regions.Where(r => r.Active && r.Name == "Texas").Select(r => r.ID).FirstOrDefault(),
                                    CountryID = context.Countries.Where(c => c.Active && c.Name == "United States of America").Select(c => c.ID).FirstOrDefault(),
                                    PostalCode = "77984",
                                    CreatedDate = createdDate,
                                    Active = true,
                                },
                            },
                        },
                    },
                });
                context.SaveUnitOfWork();
            }
            if (key == "ACCT-1123")
            {
                context.Accounts.Add(new()
                {
                    TypeID = context.AccountTypes.Where(at => at.Active && at.Name == "Customer").Select(at => at.ID).FirstOrDefault(),
                    StatusID = 1,
                    CustomKey = key,
                    Name = name,
                    Description = $"This is an account for {name}.",
                    CreatedDate = createdDate,
                    Active = true,
                    AccountContacts = new[]
                    {
                        new AccountContact
                        {
                            CustomKey = key + "|BILL TO",
                            IsPrimary = false,
                            IsBilling = true,
                            CreatedDate = createdDate,
                            Active = true,
                            Slave = new()
                            {
                                Active = true,
                                CreatedDate = createdDate,
                                CustomKey = key + "|BILL TO",
                                FirstName = "Kevin",
                                LastName = "Kramer",
                                Phone1 = "1-713-569-7852",
                                Email1 = "Kevin.Kramer@caravan.com",
                                TypeID = context.ContactTypes.Where(r => r.Active && r.Name == "Account Address").Select(r => r.ID).FirstOrDefault(),
                                Address = new()
                                {
                                    Company = "HQ",
                                    CustomKey = "BILL TO",
                                    Street1 = "134 Saint Marys Dr",
                                    City = "Hutto",
                                    RegionID = context.Regions.Where(r => r.Active && r.Name == "Texas").Select(r => r.ID).FirstOrDefault(),
                                    CountryID = context.Countries.Where(c => c.Active && c.Name == "United States of America").Select(c => c.ID).FirstOrDefault(),
                                    PostalCode = "78634",
                                    CreatedDate = createdDate,
                                    Active = true,
                                },
                            },
                        },
                        new AccountContact
                        {
                            CustomKey = key + "|SHIP TO",
                            IsPrimary = true,
                            IsBilling = false,
                            CreatedDate = createdDate,
                            Active = true,
                            Slave = new()
                            {
                                Active = true,
                                CreatedDate = createdDate,
                                CustomKey = key + "|BILL TO",
                                FirstName = "Kevin",
                                LastName = "Kramer",
                                Phone1 = "1-713-569-7852",
                                Email1 = "Kevin.Kramer@caravan.com",
                                TypeID = context.ContactTypes.Where(r => r.Active && r.Name == "Account Address").Select(r => r.ID).FirstOrDefault(),
                                Address = new()
                                {
                                    Company = "HQ",
                                    CustomKey = "SHIP TO",
                                    Street1 = "134 Saint Marys Dr",
                                    City = "Hutto",
                                    RegionID = context.Regions.Where(r => r.Active && r.Name == "Texas").Select(r => r.ID).FirstOrDefault(),
                                    CountryID = context.Countries.Where(c => c.Active && c.Name == "United States of America").Select(c => c.ID).FirstOrDefault(),
                                    PostalCode = "78634",
                                    CreatedDate = createdDate,
                                    Active = true,
                                },
                            },
                        },
                    },
                });
                context.SaveUnitOfWork();
            }
            if (key == "ACCT-1124")
            {
                context.Accounts.Add(new()
                {
                    TypeID = context.AccountTypes.Where(at => at.Active && at.Name == "Customer").Select(at => at.ID).FirstOrDefault(),
                    StatusID = 1,
                    CustomKey = key,
                    Name = name,
                    Description = $"This is an account for {name}.",
                    CreatedDate = createdDate,
                    Active = true,
                    AccountContacts = new[]
                    {
                        new AccountContact
                        {
                            CustomKey = key + "|BILL TO",
                            IsPrimary = false,
                            IsBilling = true,
                            CreatedDate = createdDate,
                            Active = true,
                            Slave = new()
                            {
                                Active = true,
                                CreatedDate = createdDate,
                                CustomKey = key + "|BILL TO",
                                FirstName = "Jason",
                                LastName = "MacIver",
                                Phone1 = "1-910-987-3565",
                                Email1 = "jason.maciver@gmail.com",
                                TypeID = context.ContactTypes.Where(r => r.Active && r.Name == "Account Address").Select(r => r.ID).FirstOrDefault(),
                                Address = new()
                                {
                                    Company = "HQ",
                                    CustomKey = "BILL TO",
                                    Street1 = "9621 Belfast Rd",
                                    City = "La Porte",
                                    RegionID = context.Regions.Where(r => r.Active && r.Name == "Texas").Select(r => r.ID).FirstOrDefault(),
                                    CountryID = context.Countries.Where(c => c.Active && c.Name == "United States of America").Select(c => c.ID).FirstOrDefault(),
                                    PostalCode = "77571",
                                    CreatedDate = createdDate,
                                    Active = true,
                                },
                            },
                        },
                        new AccountContact
                        {
                            CustomKey = key + "|SHIP TO",
                            IsPrimary = true,
                            IsBilling = false,
                            CreatedDate = createdDate,
                            Active = true,
                            Slave = new()
                            {
                                Active = true,
                                CreatedDate = createdDate,
                                CustomKey = key + "|BILL TO",
                                FirstName = "Jason",
                                LastName = "MacIver",
                                Phone1 = "1-910-987-3565",
                                Email1 = "jason.maciver@gmail.com",
                                TypeID = context.ContactTypes.Where(r => r.Active && r.Name == "Account Address").Select(r => r.ID).FirstOrDefault(),
                                Address = new()
                                {
                                    Company = "HQ",
                                    CustomKey = "SHIP TO",
                                    Street1 = "9621 Belfast Rd",
                                    City = "La Porte",
                                    RegionID = context.Regions.Where(r => r.Active && r.Name == "Texas").Select(r => r.ID).FirstOrDefault(),
                                    CountryID = context.Countries.Where(c => c.Active && c.Name == "United States of America").Select(c => c.ID).FirstOrDefault(),
                                    PostalCode = "77571",
                                    CreatedDate = createdDate,
                                    Active = true,
                                },
                            },
                        },
                    },
                });
                context.SaveUnitOfWork();
            }
            if (key == "ACCT-1125")
            {
                context.Accounts.Add(new()
                {
                    TypeID = context.AccountTypes.Where(at => at.Active && at.Name == "Customer").Select(at => at.ID).FirstOrDefault(),
                    StatusID = 1,
                    CustomKey = key,
                    Name = name,
                    Description = $"This is an account for {name}.",
                    CreatedDate = createdDate,
                    Active = true,
                    AccountContacts = new[]
                    {
                        new AccountContact
                        {
                            CustomKey = key + "|BILL TO",
                            IsPrimary = false,
                            IsBilling = true,
                            CreatedDate = createdDate,
                            Active = true,
                            Slave = new()
                            {
                                Active = true,
                                CreatedDate = createdDate,
                                CustomKey = key + "|BILL TO",
                                FirstName = "Jim",
                                LastName = "King",
                                Phone1 = "1-732-569-4520",
                                Email1 = "ron.halversen@claritymis.com",
                                TypeID = context.ContactTypes.Where(r => r.Active && r.Name == "Account Address").Select(r => r.ID).FirstOrDefault(),
                                Address = new()
                                {
                                    Company = "HQ",
                                    CustomKey = "BILL TO",
                                    Street1 = "8810 Spanish Ridge Ave",
                                    City = "Las Vegas",
                                    RegionID = context.Regions.Where(r => r.Active && r.Name == "Nevada").Select(r => r.ID).FirstOrDefault(),
                                    CountryID = context.Countries.Where(c => c.Active && c.Name == "United States of America").Select(c => c.ID).FirstOrDefault(),
                                    PostalCode = "89148",
                                    CreatedDate = createdDate,
                                    Active = true,
                                },
                            },
                        },
                        new AccountContact
                        {
                            CustomKey = key + "|SHIP TO",
                            IsPrimary = true,
                            IsBilling = false,
                            CreatedDate = createdDate,
                            Active = true,
                            Slave = new()
                            {
                                Active = true,
                                CreatedDate = createdDate,
                                CustomKey = key + "|BILL TO",
                                FirstName = "Jim",
                                LastName = "King",
                                Phone1 = "1-732-569-4520",
                                Email1 = "ron.halversen@claritymis.com",
                                TypeID = context.ContactTypes.Where(r => r.Active && r.Name == "Account Address").Select(r => r.ID).FirstOrDefault(),
                                Address = new()
                                {
                                    Company = "HQ",
                                    CustomKey = "SHIP TO",
                                    Street1 = "8810 Spanish Ridge Ave",
                                    City = "Las Vegas",
                                    RegionID = context.Regions.Where(r => r.Active && r.Name == "Nevada").Select(r => r.ID).FirstOrDefault(),
                                    CountryID = context.Countries.Where(c => c.Active && c.Name == "United States of America").Select(c => c.ID).FirstOrDefault(),
                                    PostalCode = "89148",
                                    CreatedDate = createdDate,
                                    Active = true,
                                },
                            },
                        },
                    },
                });
                context.SaveUnitOfWork();
            }
            if (key == "ACCT-1126")
            {
                context.Accounts.Add(new()
                {
                    TypeID = context.AccountTypes.Where(at => at.Active && at.Name == "Customer").Select(at => at.ID).FirstOrDefault(),
                    StatusID = 1,
                    CustomKey = key,
                    Name = name,
                    Description = $"This is an account for {name}.",
                    CreatedDate = createdDate,
                    Active = true,
                    AccountContacts = new[]
                    {
                        new AccountContact
                        {
                            CustomKey = key + "|BILL TO",
                            IsPrimary = false,
                            IsBilling = true,
                            CreatedDate = createdDate,
                            Active = true,
                            Slave = new()
                            {
                                Active = true,
                                CreatedDate = createdDate,
                                CustomKey = key + "|BILL TO",
                                FirstName = "James",
                                LastName = "Gray",
                                Phone1 = "1-800-999-3355",
                                Email1 = "james.gray@dell.com",
                                TypeID = context.ContactTypes.Where(r => r.Active && r.Name == "Account Address").Select(r => r.ID).FirstOrDefault(),
                                Address = new()
                                {
                                    Company = "HQ",
                                    CustomKey = "BILL TO",
                                    Street1 = "2401 Greenlawn Blvd",
                                    Street2 = "Bldg 7",
                                    City = "Round Rock",
                                    RegionID = context.Regions.Where(r => r.Active && r.Name == "Texas").Select(r => r.ID).FirstOrDefault(),
                                    CountryID = context.Countries.Where(c => c.Active && c.Name == "United States of America").Select(c => c.ID).FirstOrDefault(),
                                    PostalCode = "78664",
                                    CreatedDate = createdDate,
                                    Active = true,
                                },
                            },
                        },
                        new AccountContact
                        {
                            CustomKey = key + "|SHIP TO",
                            IsPrimary = true,
                            IsBilling = false,
                            CreatedDate = createdDate,
                            Active = true,
                            Slave = new()
                            {
                                Active = true,
                                CreatedDate = createdDate,
                                CustomKey = key + "|BILL TO",
                                FirstName = "James",
                                LastName = "Gray",
                                Phone1 = "1-800-999-3355",
                                Email1 = "james.gray@dell.com",
                                TypeID = context.ContactTypes.Where(r => r.Active && r.Name == "Account Address").Select(r => r.ID).FirstOrDefault(),
                                Address = new()
                                {
                                    Company = "HQ",
                                    CustomKey = "SHIP TO",
                                    Street1 = "2401 Greenlawn Blvd",
                                    Street2 = "Bldg 7",
                                    City = "Round Rock",
                                    RegionID = context.Regions.Where(r => r.Active && r.Name == "Texas").Select(r => r.ID).FirstOrDefault(),
                                    CountryID = context.Countries.Where(c => c.Active && c.Name == "United States of America").Select(c => c.ID).FirstOrDefault(),
                                    PostalCode = "78664",
                                    CreatedDate = createdDate,
                                    Active = true,
                                },
                            },
                        },
                    },
                });
                context.SaveUnitOfWork();
            }
            if (key == "ACCT-1127")
            {
                context.Accounts.Add(new()
                {
                    TypeID = context.AccountTypes.Where(at => at.Active && at.Name == "Customer").Select(at => at.ID).FirstOrDefault(),
                    StatusID = 1,
                    CustomKey = key,
                    Name = name,
                    Description = $"This is an account for {name}.",
                    CreatedDate = createdDate,
                    Active = true,
                    AccountContacts = new[]
                    {
                        new AccountContact
                        {
                            CustomKey = key + "|BILL TO",
                            IsPrimary = false,
                            IsBilling = true,
                            CreatedDate = createdDate,
                            Active = true,
                            Slave = new()
                            {
                                Active = true,
                                CreatedDate = createdDate,
                                CustomKey = key + "|BILL TO",
                                FirstName = "Emily",
                                LastName = "Dodds",
                                Phone1 = "1-877-852-9630",
                                Email1 = "emily.dodds@intel.com",
                                TypeID = context.ContactTypes.Where(r => r.Active && r.Name == "Account Address").Select(r => r.ID).FirstOrDefault(),
                                Address = new()
                                {
                                    Company = "HQ",
                                    CustomKey = "BILL TO",
                                    Street1 = "1300 S. MoPac Expy",
                                    City = "Austin",
                                    RegionID = context.Regions.Where(r => r.Active && r.Name == "Texas").Select(r => r.ID).FirstOrDefault(),
                                    CountryID = context.Countries.Where(c => c.Active && c.Name == "United States of America").Select(c => c.ID).FirstOrDefault(),
                                    PostalCode = "78746",
                                    CreatedDate = createdDate,
                                    Active = true,
                                },
                            },
                        },
                        new AccountContact
                        {
                            CustomKey = key + "|SHIP TO",
                            IsPrimary = true,
                            IsBilling = false,
                            CreatedDate = createdDate,
                            Active = true,
                            Slave = new()
                            {
                                Active = true,
                                CreatedDate = createdDate,
                                CustomKey = key + "|BILL TO",
                                FirstName = "Emily",
                                LastName = "Dodds",
                                Phone1 = "1-877-852-9630",
                                Email1 = "emily.dodds@intel.com",
                                TypeID = context.ContactTypes.Where(r => r.Active && r.Name == "Account Address").Select(r => r.ID).FirstOrDefault(),
                                Address = new()
                                {
                                    Company = "HQ",
                                    CustomKey = "SHIP TO",
                                    Street1 = "1300 S. MoPac Expy",
                                    City = "Austin",
                                    RegionID = context.Regions.Where(r => r.Active && r.Name == "Texas").Select(r => r.ID).FirstOrDefault(),
                                    CountryID = context.Countries.Where(c => c.Active && c.Name == "United States of America").Select(c => c.ID).FirstOrDefault(),
                                    PostalCode = "78746",
                                    CreatedDate = createdDate,
                                    Active = true,
                                },
                            },
                        },
                    },
                });
                context.SaveUnitOfWork();
            }
            if (key == "ACCT-1128")
            {
                context.Accounts.Add(new()
                {
                    TypeID = context.AccountTypes.Where(at => at.Active && at.Name == "Customer").Select(at => at.ID).FirstOrDefault(),
                    StatusID = 1,
                    CustomKey = key,
                    Name = name,
                    Description = $"This is an account for {name}.",
                    CreatedDate = createdDate,
                    Active = true,
                    AccountContacts = new[]
                    {
                        new AccountContact
                        {
                            CustomKey = key + "|BILL TO",
                            IsPrimary = false,
                            IsBilling = true,
                            CreatedDate = createdDate,
                            Active = true,
                            Slave = new()
                            {
                                Active = true,
                                CreatedDate = createdDate,
                                CustomKey = key + "|BILL TO",
                                FirstName = "Ron",
                                LastName = "Halversen",
                                Phone1 = "1-888-987-3565",
                                Fax1 = "1-512-596-4532",
                                Email1 = "ron.halversen@claritymis.com",
                                TypeID = context.ContactTypes.Where(r => r.Active && r.Name == "Account Address").Select(r => r.ID).FirstOrDefault(),
                                Address = new()
                                {
                                    Company = "HQ",
                                    CustomKey = "BILL TO",
                                    Street1 = "9442 N. Capital Of Texas Hwy",
                                    Street2 = "Suite 925",
                                    City = "Austin",
                                    RegionID = context.Regions.Where(r => r.Active && r.Name == "Texas").Select(r => r.ID).FirstOrDefault(),
                                    CountryID = context.Countries.Where(c => c.Active && c.Name == "United States of America").Select(c => c.ID).FirstOrDefault(),
                                    PostalCode = "78759",
                                    CreatedDate = createdDate,
                                    Active = true,
                                },
                            },
                        },
                        new AccountContact
                        {
                            CustomKey = key + "|SHIP TO",
                            IsPrimary = true,
                            IsBilling = false,
                            CreatedDate = createdDate,
                            Active = true,
                            Slave = new()
                            {
                                Active = true,
                                CreatedDate = createdDate,
                                CustomKey = key + "|BILL TO",
                                FirstName = "Ron",
                                LastName = "Halversen",
                                Phone1 = "1-888-987-3565",
                                Fax1 = "1-512-596-4532",
                                Email1 = "ron.halversen@claritymis.com",
                                TypeID = context.ContactTypes.Where(r => r.Active && r.Name == "Account Address").Select(r => r.ID).FirstOrDefault(),
                                Address = new()
                                {
                                    Company = "HQ",
                                    CustomKey = "SHIP TO",
                                    Street1 = "9442 N. Capital Of Texas Hwy",
                                    Street2 = "Suite 925",
                                    City = "Austin",
                                    RegionID = context.Regions.Where(r => r.Active && r.Name == "Texas").Select(r => r.ID).FirstOrDefault(),
                                    CountryID = context.Countries.Where(c => c.Active && c.Name == "United States of America").Select(c => c.ID).FirstOrDefault(),
                                    PostalCode = "78759",
                                    CreatedDate = createdDate,
                                    Active = true,
                                },
                            },
                        },
                    },
                });
                context.SaveUnitOfWork();
            }
        }

        private void AddSampleAccounts(DateTime createdDate)
        {
            if (context?.Accounts == null)
            {
                return;
            }
            AddSampleAccount(createdDate, "ACCT-1121", "Clarity Ventures Inc");
            var defaultClarityAdmin = context.Users.FirstOrDefault(x => x.UserName == "clarity");
            if (defaultClarityAdmin?.AccountID.HasValue == false)
            {
                defaultClarityAdmin.AccountID = context.Accounts.First(x => x.CustomKey == "ACCT-1121").ID;
            }
            AddSampleAccount(createdDate, "ACCT-1122", "Boedeker Plastics");
            AddSampleAccount(createdDate, "ACCT-1123", "Caravan Health");
            AddSampleAccount(createdDate, "ACCT-1124", "Catholic Travel Centre");
            AddSampleAccount(createdDate, "ACCT-1125", "United Aqua Group");
            AddSampleAccount(createdDate, "ACCT-1126", "Dell");
            AddSampleAccount(createdDate, "ACCT-1127", "Intel");
            AddSampleAccount(createdDate, "ACCT-1128", "Brick Packaging");
        }
    }
}
