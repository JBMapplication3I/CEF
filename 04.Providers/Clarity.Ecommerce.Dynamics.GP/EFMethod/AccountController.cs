// <copyright file="AccountController.cs" company="clarity-ventures.com">
// Copyright (c) 2015-2023 clarity-ventures.com. All rights reserved.
// </copyright>
// <summary>Implements the account controller class</summary>
// <remarks>This file content was copied from ClarityConnect, and then modified from eConnect to EF</remarks>
namespace Clarity.Ecommerce.MicrosoftDynamicsGP.EFMethod.Accounts
{
    using System;
    using System.Collections.Generic;
    using System.Diagnostics;
    using System.Diagnostics.Contracts;
    using System.Linq;
    using Dynamics.GP.EFMethod.Model;
    using Interfaces.Models;
    using Interfaces.Workflow;

    /// <summary>A controller for handling accounts.</summary>
    /// <seealso cref="T:Clarity.Ecommerce.Interfaces.Workflow.IAccountController"/>
    /// <seealso cref="T:Clarity.Connect.Provider.Interface.Accounts.IAccountController"/>
    public class AccountController : IAccountController
    {
        private class GPCustomerRecord
        {
            public RM00101 Customer { get; set; }
            public List<RM00102> Addresses { get; set; }
        }

        /// <summary>Gets a ProductModel.</summary>
        /// <exception cref="ArgumentException">Thrown when one or more arguments have unsupported or
        ///                                     illegal values.</exception>
        /// <param name="key">         The key.</param>
        /// <param name="currentModel">The current model.</param>
        /// <returns>A ProductModel.</returns>
        public IAccountModel Get(string key, IAccountModel currentModel = null)
        {
            Debug.WriteLine("Beginning Get customer...");
            try
            {
                using (var gpContext = new DynGP10Entities())
                {
                    var rm00101 = gpContext.RM00101.FirstOrDefault(x => x.CUSTNMBR == key);
                    if (rm00101 == null) { throw new ArgumentException("Key must match an existing customer record in GP"); }
                    var addresses = gpContext.RM00102.Where(x => x.CUSTNMBR == key).ToList();
                    foreach (var address in addresses)
                    {
                        address.Internet_Address = gpContext.SY01200.FirstOrDefault(x => x.Master_ID == rm00101.CUSTNMBR && x.ADRSCODE == address.ADRSCODE);
                    }
                    var customer = new GPCustomerRecord
                    {
                        Customer = rm00101,
                        Addresses = addresses
                    };
                    return ConvertRecordToModel(customer, currentModel);
                }
            }
            catch (Exception ex)
            {
                // Dislay any errors that occur to the console
                Debug.WriteLine($"Error Processing Customer \r\n{ex}");
                throw;
            }
        }

        private static IAccountModel ConvertRecordToModel(GPCustomerRecord record, IAccountModel currentModel)
        {
            Contract.Requires<ArgumentNullException>(record != null);
            //
            if (currentModel == null)
            {
                currentModel = RegistryLoader.ContainerInstance.GetInstance<IAccountModel>();
            }
            if (currentModel.Attributes == null) { currentModel.Attributes = new List<IAttributeModel>(); }
            // Core
            currentModel.TypeID = 1; // "Customer"
            currentModel.CustomKey = record.Customer.CUSTNMBR.Trim();
            currentModel.CreatedDate = record.Customer.CREATDDT;
            currentModel.UpdatedDate = record.Customer.MODIFDT;
            currentModel.IsTaxable = false;
            currentModel.Active = true;
            // Account Basics
            currentModel.Name = record.Customer.CUSTNAME.Trim();
            currentModel.Phone = record.Customer.PHONE1;
            currentModel.Fax = record.Customer.FAX;
            currentModel.Email = record.Customer.COMMENT1;
            // Account's set Price Level
            if (!string.IsNullOrWhiteSpace(record.Customer.PRCLEVEL))
            {
                if (currentModel.AccountPricePoints == null)
                {
                    currentModel.AccountPricePoints = new List<IAccountPricePointModel>();
                }
                if (currentModel.AccountPricePoints.Count > 1)
                {
                    // Remove all but the first, there should only ever be one
                    var first = currentModel.AccountPricePoints.First();
                    currentModel.AccountPricePoints.Clear();
                    currentModel.AccountPricePoints.Add(first);
                }
                if (!currentModel.AccountPricePoints.Any())
                {
                    var accountPricePointModel = RegistryLoader.ContainerInstance.GetInstance<IAccountPricePointModel>();
                    accountPricePointModel.AccountID = currentModel.ID ?? 0;
                    accountPricePointModel.Active = true;
                    accountPricePointModel.CreatedDate = record.Customer.MODIFDT;
                    accountPricePointModel.PricePointKey = record.Customer.PRCLEVEL.Trim();
                    accountPricePointModel.AccountKey = currentModel.CustomKey;
                    currentModel.AccountPricePoints.Add(accountPricePointModel);
                }
                else
                {
                    currentModel.AccountPricePoints[0].Active = true;
                    currentModel.AccountPricePoints[0].UpdatedDate = record.Customer.MODIFDT;
                    currentModel.AccountPricePoints[0].PricePointKey = record.Customer.PRCLEVEL.Trim();
                }
            }
            // Account Terms: Don't replace current Terms, but can add if not there or take away if not supposed to be there anymore
            if (!string.IsNullOrWhiteSpace(record.Customer.PYMTRMID) && currentModel.AccountTerm?.CustomKey == record.Customer.PYMTRMID.Trim())
            {
                // Add New Terms
                var accountTermModel = RegistryLoader.ContainerInstance.GetInstance<IAccountTermModel>();
                accountTermModel.CustomKey = record.Customer.PYMTRMID.Trim();
                accountTermModel.AccountKey = record.Customer.CUSTNMBR.Trim();
                accountTermModel.Active = true;
                accountTermModel.CreatedDate = currentModel.CreatedDate;
                accountTermModel.UpdatedDate = currentModel.UpdatedDate;
                currentModel.AccountTerm = accountTermModel;
            }
            else if (string.IsNullOrWhiteSpace(record.Customer.PYMTRMID) && currentModel.AccountTerm != null)
            {
                // Remove Existing Terms
                currentModel.AccountTerm = null;
                currentModel.AccountTermID = null;
            }
            // Process Addresses
            {
                // Process a single address from information provided in Customer XML and not its subclasses
                currentModel.AccountAddresses = new List<IAccountAddressModel>();
                // The following code is how it should work to set an address using the multiple addresses listed in customer XML
                foreach (var address in record.Addresses)
                {
                    var newAccountAddress2 = RegistryLoader.ContainerInstance.GetInstance<IAccountAddressModel>();
                    newAccountAddress2.AccountID = currentModel.ID ?? 0;
                    newAccountAddress2.CustomKey = $"{address.CUSTNMBR}|{address.ADRSCODE}";
                    newAccountAddress2.TransmittedToERP = true;
                    newAccountAddress2.IsPrimary = record.Customer.PRSTADCD == address.ADRSCODE;
                    newAccountAddress2.IsBilling = record.Customer.PRBTADCD == address.ADRSCODE;
                    newAccountAddress2.Active = true;
                    newAccountAddress2.CreatedDate = currentModel.CreatedDate;
                    newAccountAddress2.UpdatedDate = currentModel.UpdatedDate;
                    var newAddress2 = RegistryLoader.ContainerInstance.GetInstance<IAddressModel>();
                    newAddress2.CustomKey = address.ADRSCODE;
                    newAddress2.Name = address.CNTCPRSN;
                    newAddress2.Street1 = address.ADDRESS1;
                    newAddress2.Street2 = address.ADDRESS2;
                    newAddress2.Street3 = address.ADDRESS3;
                    newAddress2.City = address.CITY;
                    newAddress2.RegionName = address.STATE;
                    newAddress2.CountryName = address.COUNTRY;
                    newAddress2.PostalCode = address.ZIP;
                    newAddress2.Phone = address.PHONE1;
                    newAddress2.Phone2 = address.PHONE2;
                    newAddress2.Phone3 = address.PHONE3;
                    newAddress2.Fax = address.FAX;
                    newAddress2.Email = address.Internet_Address.INET1?.Replace(" ", "").Trim() ?? "";
                    newAddress2.IsPrimary = record.Customer.PRSTADCD == address.ADRSCODE;
                    newAddress2.IsBilling = record.Customer.PRBTADCD == address.ADRSCODE;
                    newAddress2.Active = true;
                    newAddress2.CreatedDate = currentModel.CreatedDate;
                    newAddress2.UpdatedDate = currentModel.UpdatedDate;
                    newAccountAddress2.Address = newAddress2;
                    currentModel.AccountAddresses.Add(newAccountAddress2);
                }
            }
            // Return the ProductModel
            return currentModel;
        }
    }
}
