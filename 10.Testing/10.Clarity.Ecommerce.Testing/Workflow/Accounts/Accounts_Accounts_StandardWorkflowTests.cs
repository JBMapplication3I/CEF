// <copyright file="Accounts_Accounts_StandardWorkflowTests.cs" company="clarity-ventures.com">
// Copyright (c) 2018-2023 clarity-ventures.com. All rights reserved.
// </copyright>
// <summary>Implements the accounts standard workflow tests class</summary>
namespace Clarity.Ecommerce.Workflow.Testing
{
    using System.Collections.Generic;
    using Models;

    public partial class Accounts_Accounts_StandardWorkflowTests
    {
        protected override AccountModel Create_WithValidData_ModelHook()
        {
            return new AccountModel
            {
                Active = true,
                CustomKey = "TEST",
                Name = "TEST",
                //
                Type = new TypeModel { Name = "Customer" },
                Status = new StatusModel { Name = "Normal" },
                //
                AccountContacts = new List<AccountContactModel>
                {
                    new AccountContactModel
                    {
                        Active = true,
                        IsBilling = true,
                        IsPrimary = false,
                        TransmittedToERP = false,
                        Name = "Address 1",
                        Slave = new ContactModel
                        {
                            Active = true,
                            Phone1 = "555-555-5555",
                            Phone2 = "555-555-5556",
                            Phone3 = "555-555-5557",
                            Fax1 = "",
                            TypeID = 1,
                            Address = new AddressModel
                            {
                                Active = true,
                                Company = "Bob Jones c/o My Company",
                                Street1 = "111 Fake Ln",
                                Street2 = "",
                                Street3 = "",
                                City = "Fake City",
                                RegionKey = "TX",
                                PostalCode = "78759",
                                Region = new RegionModel { Code = "TX" },
                                Country = new CountryModel { Code = "USA" }
                            }
                        }
                    },
                },
                AccountPricePoints = new List<AccountPricePointModel> { new AccountPricePointModel { MasterKey = "TEST", Active = true, SlaveKey = "LTL", } },
                Notes = new List<NoteModel> { new NoteModel { Active = true, Note1 = "This is my note for this account", Type = new NoteTypeModel { Active = true, Name = "Account Notes" } } },
            };
        }

        protected override AccountModel Update_WithValidData_ModelHook()
        {
            return new AccountModel
            {
                ID = 1,
                Active = true,
                CustomKey = "MIBSF49682",
                Name = "Black Star Farms",
                Description = "A company",
                //
                TypeID = 2,
                StatusID = 1,
                Type = new TypeModel { Name = "Customer" },
                //
                AccountPricePoints = new List<AccountPricePointModel> { new AccountPricePointModel { MasterKey = "TEST", Active = true, SlaveKey = "LTL", } },
                Notes = new List<NoteModel> { new NoteModel { Active = true, Note1 = "This is my note for this account", Type = new NoteTypeModel { Active = true, Name = "Account Notes" } } },
            };
        }
    }
}
