// <copyright file="AccountController.cs" company="clarity-ventures.com">
// Copyright (c) 2015-2023 clarity-ventures.com. All rights reserved.
// </copyright>
// <summary>Implements the account controller class</summary>
// <remarks>This file content was copied from ClarityConnect</remarks>
namespace Clarity.Ecommerce.MicrosoftDynamicsGP.EconnectMethod.Accounts
{
    using System;
    using System.Collections.Generic;
    using System.Configuration;
    using System.Diagnostics;
    using System.Diagnostics.Contracts;
    using System.IO;
    using System.Linq;
    using System.Xml;
    using System.Xml.Serialization;
    using Interfaces.Models;
    using Interfaces.Workflow;
    using Microsoft.Dynamics.GP.eConnect;
    using Microsoft.Dynamics.GP.eConnect.Serialization;

    /// <summary>A controller for handling accounts.</summary>
    /// <seealso cref="T:Clarity.Ecommerce.Interfaces.Workflow.IAccountController"/>
    /// <seealso cref="T:Clarity.Connect.Provider.Interface.Accounts.IAccountController"/>
    public class AccountController : IAccountController
    {
        /// <summary>The connection string.</summary>
        private static string ConnectionString => ConfigurationManager.AppSettings["Dynamics.GP.eConnect.ConnectionString"];

        /// <summary>Gets a ProductModel.</summary>
        /// <param name="id">          The identifier to delete.</param>
        /// <param name="currentModel">The current model.</param>
        /// <returns>A ProductModel.</returns>
        public IAccountModel Get(string id, IAccountModel currentModel = null)
        {
            Debug.WriteLine("Beginning Get customer...");
            try
            {
                // Create an eConnectOut XML node object
                var myeConnectOut = new eConnectOut
                {
                    // Populate the eConnectOut XML node elements
                    ACTION = 1,
                    DOCTYPE = "Customer",
                    OUTPUTTYPE = 2,
                    INDEX1FROM = id,
                    INDEX1TO = id,
                    FORLIST = 1
                };
                // Call the eConnect_Requester method of the eConnectMethods object to retrieve specified XML data
                var resultDocument = MakeAnEconnectOutCall(myeConnectOut);
                if (string.IsNullOrWhiteSpace(resultDocument))
                {
                    throw new Exception("Customer record was not found.");
                }
                //// Display the result of the eConnect_Requester method call
                ////var printtest = resultDocument.Length > 1024 ? resultDocument.Substring(0, 1024) : resultDocument;
                ////Debug.WriteLine(printtest);
                // Deserialize the object
                Connect.Shared.eConnect eConnectResultObject;
                var serializer = new XmlSerializer(typeof(Connect.Shared.eConnect));
                using (var reader = new StringReader(resultDocument))
                {
                    eConnectResultObject = (Connect.Shared.eConnect)serializer.Deserialize(reader);
                }
                var customer = eConnectResultObject.Customer;
                Debug.WriteLine($"Downloaded Customer: {customer.CUSTNMBR}");
                var convertedCustomer = ConvertXmlToModel(customer, currentModel);
                Debug.WriteLine($"Updated GP customer {convertedCustomer.CustomKey} to Clarity eCommerce.");
                return convertedCustomer;
            }
            catch (Exception ex)
            {
                // Dislay any errors that occur to the console
                Debug.WriteLine("Error Processing Customer \r\n{0}", ex);
                throw;
            }
        }

        private static string MakeAnEconnectOutCall(eConnectOut myeConnectOut)
        {
            // Create an eConnect document type object
            var myEConnectType = new eConnectType();
            // Create a RQeConnectOutType schema object
            var myReqType = new RQeConnectOutType
            {
                // Add the eConnectOut XML node object to the RQeConnectOutType schema object
                eConnectOut = myeConnectOut
            };
            // Add the RQeConnectOutType schema object to the eConnect document object
            RQeConnectOutType[] myReqOutType = { myReqType };
            myEConnectType.RQeConnectOutType = myReqOutType;
            // Serialize the eConnect document object to a memory stream
            var myMemStream = new MemoryStream();
            var mySerializer = new XmlSerializer(myEConnectType.GetType());
            mySerializer.Serialize(myMemStream, myEConnectType);
            myMemStream.Position = 0;
            // Load the serialized eConnect document object into an XML document object
            var xmlreader = new XmlTextReader(myMemStream);
            var requestDocument = new XmlDocument();
            requestDocument.Load(xmlreader);
            // Create an eConnectMethods object
            var requester = new eConnectMethods();
            // Call the eConnect_Requester method of the eConnectMethods object to retrieve specified XML data
            var resultDocument = requester.eConnect_Requester(ConnectionString, EnumTypes.ConnectionStringType.SqlClient, requestDocument.OuterXml);
            // Remove the root tag so we can serialize
            resultDocument = resultDocument
                .Replace("<root>", "")
                .Replace("</root>", "");
            // Return the serializable-ready string
            return resultDocument;
        }

        private static IAccountModel ConvertXmlToModel(Connect.Classes.Customer customer, IAccountModel currentModel)
        {
            Contract.Requires<ArgumentNullException>(customer != null);
            //
            if (currentModel == null)
            {
                currentModel = RegistryLoader.ContainerInstance.GetInstance<IAccountModel>();
            }
            if (currentModel.Attributes == null) { currentModel.Attributes = new List<IAttributeModel>(); }
            // Core
            currentModel.TypeID = 1; // "Customer"
            currentModel.CustomKey = customer.CUSTNMBR.Trim();
            currentModel.CreatedDate = customer.CREATDDT;
            currentModel.UpdatedDate = customer.MODIFDT;
            currentModel.IsTaxable = false;
            currentModel.Active = true;
            // Account Basics
            currentModel.Name = customer.CUSTNAME.Trim();
            currentModel.Phone = customer.PHONE1;
            currentModel.Fax = customer.FAX;
            currentModel.Email = customer.COMMENT1;
            // Account's set Price Level
            if (!string.IsNullOrWhiteSpace(customer.PRCLEVEL))
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
                    var accountPricePoint = RegistryLoader.ContainerInstance.GetInstance<IAccountPricePointModel>();
                    accountPricePoint.AccountID = currentModel.ID ?? 0;
                    accountPricePoint.Active = true;
                    accountPricePoint.CreatedDate = customer.MODIFDT;
                    accountPricePoint.PricePointKey = customer.PRCLEVEL.Trim();
                    accountPricePoint.AccountKey = currentModel.CustomKey;
                    currentModel.AccountPricePoints.Add(accountPricePoint);
                }
                else
                {
                    currentModel.AccountPricePoints[0].Active = true;
                    currentModel.AccountPricePoints[0].UpdatedDate = customer.MODIFDT;
                    currentModel.AccountPricePoints[0].PricePointKey = customer.PRCLEVEL.Trim();
                }
            }
            // Account Terms: Don't replace current Terms, but can add if not there or take away if not supposed to be there anymore
            if (!string.IsNullOrWhiteSpace(customer.PYMTRMID) && currentModel.AccountTerm?.CustomKey == customer.PYMTRMID.Trim())
            {
                // Add New Terms
                var accountTerm = RegistryLoader.ContainerInstance.GetInstance<IAccountTermModel>();
                accountTerm.CustomKey = customer.PYMTRMID.Trim();
                accountTerm.AccountKey = customer.CUSTNMBR.Trim();
                accountTerm.Active = true;
                accountTerm.CreatedDate = currentModel.CreatedDate;
                accountTerm.UpdatedDate = currentModel.UpdatedDate;
                currentModel.AccountTerm = accountTerm;
            }
            else if (string.IsNullOrWhiteSpace(customer.PYMTRMID) && currentModel.AccountTerm != null)
            {
                // Remove Existing Terms
                currentModel.AccountTerm = null;
                currentModel.AccountTermID = null;
            }
            // Process Addresses
            {
                // Process a single address from information provided in Customer XML and not its subclasses
                currentModel.AccountAddresses = new List<IAccountAddressModel>();
                // The following code is how is should work to set an address using the multiple addresses listed in customer XML
                foreach (var address in customer.Addresses)
                {
                    var newAccountAddress2 = RegistryLoader.ContainerInstance.GetInstance<IAccountAddressModel>();
                    var newAddress = RegistryLoader.ContainerInstance.GetInstance<IAddressModel>();
                    newAccountAddress2.AccountID = currentModel.ID ?? 0;
                    newAccountAddress2.CustomKey = $"{address.CUSTNMBR}|{address.ADRSCODE}";
                    newAccountAddress2.TransmittedToERP = true;
                    newAccountAddress2.IsPrimary = customer.PRSTADCD == address.ADRSCODE;
                    newAccountAddress2.IsBilling = customer.PRBTADCD == address.ADRSCODE;
                    newAccountAddress2.Active = true;
                    newAccountAddress2.CreatedDate = currentModel.CreatedDate;
                    newAccountAddress2.UpdatedDate = currentModel.UpdatedDate;
                    newAddress.CustomKey = address.ADRSCODE;
                    newAddress.Name = address.CNTCPRSN;
                    newAddress.Street1 = address.ADDRESS1;
                    newAddress.Street2 = address.ADDRESS2;
                    newAddress.Street3 = address.ADDRESS3;
                    newAddress.City = address.CITY;
                    newAddress.RegionName = address.STATE;
                    newAddress.CountryName = address.COUNTRY;
                    newAddress.PostalCode = address.ZIP;
                    newAddress.Phone = address.PHONE1;
                    newAddress.Phone2 = address.PHONE2;
                    newAddress.Phone3 = address.PHONE3;
                    newAddress.Fax = address.FAX;
                    newAddress.Email = address.Internet_Address.INET1?.Replace(" ", "").Trim() ?? "";
                    newAddress.IsPrimary = customer.PRSTADCD == address.ADRSCODE;
                    newAddress.IsBilling = customer.PRBTADCD == address.ADRSCODE;
                    newAddress.Active = true;
                    newAddress.CreatedDate = currentModel.CreatedDate;
                    newAddress.UpdatedDate = currentModel.UpdatedDate;
                    newAccountAddress2.Address = newAddress;
                    currentModel.AccountAddresses.Add(newAccountAddress2);
                }
            }
            // Return the ProductModel
            return currentModel;
        }
    }
}
