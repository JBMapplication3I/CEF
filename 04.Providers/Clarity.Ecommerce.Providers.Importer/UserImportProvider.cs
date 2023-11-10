// <copyright file="UserImportProvider.cs" company="clarity-ventures.com">
// Copyright (c) 2019-2023 clarity-ventures.com. All rights reserved.
// </copyright>
// <summary>Implements the user import provider class.</summary>
// ReSharper disable StringLiteralTypo
namespace Clarity.Ecommerce.Workflow
{
    using System;
    using System.Collections;
    using System.Collections.Generic;
    using System.IO;
    using System.Linq;
    using System.Reflection;
    using System.Threading.Tasks;
    using DataModel;
    using Excel;
    using Interfaces.DataModel;
    using Interfaces.Models;
    using Interfaces.Models.Import;
    using Interfaces.Workflow;
    using JetBrains.Annotations;
    using JSConfigs;
    using Models;
    using Models.Import;
    using Service;
    using ServiceStack;
    using Utilities;

    public partial interface IUserImportProvider
    {
        /// <summary>Integrate users.</summary>
        /// <param name="spreadsheetModel">           The spreadsheet model.</param>
        /// <param name="associateAccountToAccountID">Identifier for the account to associate the user's individual
        ///                                           accounts to.</param>
        /// <param name="contextProfileName">         Name of the context profile.</param>
        /// <returns>A CEFActionResponse{List{int}}.</returns>
        Task<CEFActionResponse<List<int>?>> IntegrateUsersAsync(
            ISpreadsheetImportModel spreadsheetModel,
            int associateAccountToAccountID,
            string? contextProfileName);
    }

    public partial class UserImportProvider : IUserImportProvider
    {
        /// <summary>The excel data reader.</summary>
        private IExcelDataReader? excelDataReader;

        /// <summary>List of names of the user properties.</summary>
        private List<string>? userPropertyNames;

        /// <summary>List of names of the contact properties.</summary>
        private List<string>? contactPropertyNames;

        /// <summary>Gets a list of parsing errors.</summary>
        /// <value>A List of parsing errors.</value>
        protected List<string> ParsingErrorList { get; } = new();

        /// <summary>Gets the workflows.</summary>
        /// <value>The workflows.</value>
        private static IWorkflowsController Workflows { get; }
            = RegistryLoaderWrapper.GetInstance<IWorkflowsController>();

        /// <summary>Gets the logger.</summary>
        /// <value>The logger.</value>
        private static ILogger Logger { get; }
            = RegistryLoaderWrapper.GetInstance<ILogger>();

        /// <inheritdoc/>
        public async Task<CEFActionResponse<List<int>?>> IntegrateUsersAsync(
            ISpreadsheetImportModel spreadsheetModel,
            int associateAccountToAccountID,
            string? contextProfileName)
        {
            var loadRet = Load(spreadsheetModel);
            if (!loadRet)
            {
                return CEFAR.FailingCEFAR<List<int>?>("Error loading Spreadsheet");
            }
            var items = Parse()?.ToList();
            var errors = new List<string>();
            errors.AddRange(GetParsingError());
            if (items?.Any() != true)
            {
                return CEFAR.FailingCEFAR<List<int>?>(errors.ToArray());
            }
            var results = new List<int>();
            try
            {
                var typeID = await Workflows.UserTypes.ResolveToIDAsync(
                        byID: null,
                        byKey: "Customer",
                        byName: "Customer",
                        byDisplayName: "Customer",
                        model: null,
                        contextProfileName: contextProfileName)
                    .ConfigureAwait(false);
                // RESOLVE ITEMS FROM IMPORTER TO CEF MODELS
                var models = await ResolveAsync(items, contextProfileName).ConfigureAwait(false);
                if (models?.Any() != true)
                {
                    return CEFAR.FailingCEFAR<List<int>?>("ERROR! While resolving items, something didn't go right");
                }
                var timestamp = DateExtensions.GenDateTime;
                var count = 0;
                foreach (var model in models)
                {
                    if (!Contract.CheckAnyValidKey(model.CustomKey, model.Email))
                    {
                        continue;
                    }
                    var validKey = Contract.CheckValidKey(model.CustomKey);
                    var validEmail = Contract.CheckValidKey(model.Email);
                    try
                    {
                        var entityID = (validKey
                            ? await Workflows.Users.CheckExistsAsync(model.CustomKey!, contextProfileName).ConfigureAwait(false)
                            : null)
                            ?? (validEmail
                                ? await Workflows.Users.CheckExistsByEmailAsync(model.Email!, contextProfileName).ConfigureAwait(false)
                                : null);
                        model.Active = true;
                        if (!Contract.CheckValidID(model.TypeID))
                        {
                            model.TypeID = typeID;
                        }
                        if (!Contract.CheckValidID(model.AccountID))
                        {
                            // Create an account for the user and assign them to it
                            var accountID = validKey
                                ? await Workflows.Accounts.CheckExistsAsync(model.CustomKey!, contextProfileName).ConfigureAwait(false)
                                : null;
                            if (Contract.CheckValidID(accountID))
                            {
                                model.AccountID = accountID;
                            }
                            else
                            {
                                var newAccount = await Workflows.Accounts.CreateAsync(
                                        new AccountModel
                                        {
                                            Active = true,
                                            CreatedDate = timestamp,
                                            CustomKey = model.CustomKey,
                                            Name = model.Email,
                                            TypeKey = "Customer",
                                            StatusKey = "Normal",
                                        },
                                        contextProfileName)
                                    .ConfigureAwait(false);
                                model.AccountID = newAccount.Result;
                            }
                        }
                        model.Account ??= await Workflows.Accounts.GetAsync(
                                model.AccountID!.Value,
                                contextProfileName)
                            .ConfigureAwait(false);
                        if (Contract.CheckEmpty(model.Account!.AccountAssociations)
                            || model.Account.AccountAssociations!.All(x => x.MasterID != associateAccountToAccountID))
                        {
                            // Add the association
                            var collection = model.Account.AccountAssociations
                                ?? new List<IAccountAssociationModel>();
                            var upsertResult = await Workflows.AccountAssociations.UpsertAsync(
                                    new AccountAssociationModel
                                    {
                                        Active = true,
                                        CreatedDate = timestamp,
                                        CustomKey = null, // TODO: Best way to set this here?
                                        MasterID = associateAccountToAccountID,
                                        SlaveID = Contract.RequiresValidID(model.AccountID),
                                        SlaveKey = model.CustomKey,
                                        TypeKey = "Affiliate",
                                        SerializableAttributes = new()
                                        {
                                            ["AddedByEcomm"] = new()
                                            {
                                                Key = "AddedByEcomm",
                                                Value = "true",
                                            },
                                        },
                                    },
                                    contextProfileName)
                                .ConfigureAwait(false);
                            collection.Add(
                                (await Workflows.AccountAssociations.GetAsync(upsertResult.Result, contextProfileName).ConfigureAwait(false))!);
                            model.Account.AccountAssociations = collection;
                        }
                        if (Contract.CheckValidID(entityID))
                        {
                            model.ID = entityID!.Value;
                        }
                        else
                        {
                            // Create a temporary password
                            model.OverridePassword = Guid.NewGuid().ToString().Replace("-", string.Empty)[..12];
                        }
                        if (!Contract.CheckValidKey(model.Email) && Contract.CheckValidKey(model.CustomKey))
                        {
                            model.Email = model.CustomKey;
                        }
                        if (!Contract.CheckValidKey(model.UserName) && Contract.CheckValidKey(model.CustomKey))
                        {
                            model.UserName = model.CustomKey;
                        }
                        if (!Contract.CheckValidKey(model.UserName) && Contract.CheckValidKey(model.Email))
                        {
                            model.UserName = model.Email;
                        }
                        if (!Contract.CheckValidKey(model.CustomKey) && Contract.CheckValidKey(model.Email))
                        {
                            model.CustomKey = model.Email;
                        }
                        var upserted = await Workflows.Users.UpsertAsync(model, contextProfileName).ConfigureAwait(false);
                        if (upserted == null)
                        {
                            throw new($"Upsert returned null for line {count}");
                        }
                        if (!Contract.CheckValidID(upserted.Result))
                        {
                            throw new($"Upsert returned a model, but without a valid ID for line: {count}");
                        }
                        results.Add(upserted.Result);
                        if (true /*!upserted.SerializableAttributes.TryGetValue("Member Status", out var memberStatus)*/)
                        {
                            // No application of the role to modify
                            count++;
                            // continue;
                        }
                        /*
                        if (!bool.TryParse(memberStatus.Value, out var shouldHaveRole))
                        {
                            // The valid is invalid/Non-Parsable, TODO: Throw?
                            throw new(
                                $"Member Status column has a value that cannot be parsed to true or false on line {count}");
                        }
                        var role = new RoleForUserModel { Name = "Member", UserId = upserted.ID };
                        var response = shouldHaveRole
                            ? await Workflows.Authentication.AssignRoleToUserAsync(role, contextProfileName).ConfigureAwait(false)
                            : await Workflows.Authentication.RemoveRoleFromUserAsync(role, contextProfileName).ConfigureAwait(false);
                        if (response.ActionSucceeded)
                        {
                            // Success
                            continue;
                        }
                        throw new(
                            $"Failed to modify role to {(shouldHaveRole ? string.Empty : "not ")} have it per column value."
                            + $" {response.Messages.Aggregate((c, n) => $"{c}, {n}")}");
                        */
                    }
                    catch (Exception ex2)
                    {
                        var msg = $"Error with user \"{model.CustomKey ?? model.Email}\" on line {count} Error: {ex2.Message}";
                        errors.Add(msg);
                        await Logger.LogErrorAsync(
                                name: $"{nameof(UserImportProvider)}.Imports.{nameof(IntegrateUsersAsync)}",
                                message: msg,
                                ex: ex2,
                                contextProfileName: contextProfileName)
                            .ConfigureAwait(false);
                        throw;
                    }
                }
            }
            catch (Exception ex1)
            {
                var msg = $"Error while parsing spreadsheet: {ex1.Message}";
                errors.Add(msg);
                await Logger.LogErrorAsync(
                        name: $"{nameof(UserImportProvider)}.Imports.{nameof(IntegrateUsersAsync)}",
                        message: msg,
                        ex: ex1,
                        contextProfileName: contextProfileName)
                    .ConfigureAwait(false);
            }
            return errors.Count > 0
                ? results.WrapInFailingCEFAR(errors.ToArray())
                : results.WrapInPassingCEFAR();
        }

        /// <summary>Sets a type.</summary>
        /// <typeparam name="TModel">Type of the model.</typeparam>
        /// <param name="value">The value.</param>
        /// <param name="model">The model.</param>
        private static void SetType<TModel>(string value, ref TModel model)
        {
            if (string.IsNullOrEmpty(value))
            {
                return;
            }
            var prop = model!.GetType().GetProperty("Type", BindingFlags.Public | BindingFlags.Instance);
            // Create TypeModel object
            if (prop == null)
            {
                return;
            }
            if (prop.GetValue(model) != null)
            {
                return;
            }
            var typeInstance = Activator.CreateInstance(prop.PropertyType);
            var nameProperty = typeInstance!.GetType().GetProperty(nameof(INameableBaseModel.Name), BindingFlags.Public | BindingFlags.Instance);
            var customKeyProperty = typeInstance.GetType().GetProperty(nameof(IBaseModel.CustomKey), BindingFlags.Public | BindingFlags.Instance);
            var activeProperty = typeInstance.GetType().GetProperty(nameof(IBaseModel.Active), BindingFlags.Public | BindingFlags.Instance);
            var createdDateProperty = typeInstance.GetType().GetProperty(nameof(IBaseModel.CreatedDate), BindingFlags.Public | BindingFlags.Instance);
            customKeyProperty?.SetValue(typeInstance, value);
            activeProperty?.SetValue(typeInstance, true);
            createdDateProperty?.SetValue(typeInstance, DateExtensions.GenDateTime);
            nameProperty?.SetValue(typeInstance, value);
            prop.SetValue(model, typeInstance);
        }

        /// <summary>Creates an attribute.</summary>
        /// <typeparam name="TModel">Type of the model.</typeparam>
        /// <param name="name"> The name.</param>
        /// <param name="value">The value.</param>
        /// <param name="model">The model.</param>
        private static void CreateAttribute<TModel>(string name, string value, ref TModel model)
        {
            if (string.IsNullOrEmpty(name) || string.IsNullOrEmpty(value))
            {
                return;
            }
            var prop = model!.GetType().GetProperty(nameof(IBaseModel.SerializableAttributes), BindingFlags.Public | BindingFlags.Instance);
            if (prop == null)
            {
                return;
            }
            // ReSharper disable once PossibleNullReferenceException
            if (prop.GetValue(model) == null)
            {
                var instance = Activator.CreateInstance(typeof(SerializableAttributesDictionary));
                prop.SetValue(model, instance);
            }
            if (prop.GetValue(model) is not SerializableAttributesDictionary serial)
            {
                return;
            }
            serial[name.Trim()] = new()
            {
                Key = name.Trim(),
                Value = value,
            };
            prop.SetValue(model, serial);
        }

        /// <summary>Sets associated object.</summary>
        /// <typeparam name="TModel">      Type of the model.</typeparam>
        /// <typeparam name="TAssocModel"> Type of the associated model.</typeparam>
        /// <typeparam name="TTargetModel">Type of the target model.</typeparam>
        /// <param name="value">         The value.</param>
        /// <param name="assocPropName"> Name of the associated property.</param>
        /// <param name="targetPropName">Name o the target property.</param>
        /// <param name="model">         The model.</param>
        private static void SetAssociatedObject<TModel, TAssocModel, TTargetModel>(
            string value,
            string assocPropName,
            string targetPropName,
            ref TModel model)
        {
            if (string.IsNullOrEmpty(value))
            {
                return;
            }
            var prop = model!.GetType().GetProperty(assocPropName, BindingFlags.Public | BindingFlags.Instance);
            if (prop?.CanWrite != true)
            {
                return;
            }
            var type = prop.PropertyType;
            if (!type.IsGenericType || type.GetGenericTypeDefinition() != typeof(List<>))
            {
                return;
            }
            var itemType = type.GetGenericArguments()[0];
            // If list is null -- create new list
            if (prop.GetValue(model) == null)
            {
                var listType = typeof(List<>);
                var constructedListType = listType.MakeGenericType(itemType);
                var instance = Activator.CreateInstance(constructedListType);
                prop.SetValue(model, instance);
            }
            // Get the list
            var list = prop.GetValue(model);
            // Cast the list to List<itemType>
            // ReSharper disable once PossibleNullReferenceException
            var newList = typeof(Enumerable)
                .GetMethod(nameof(Enumerable.Cast))!
                .MakeGenericMethod(itemType)
                .Invoke(null, new[] { list });
            if (newList == null)
            {
                return;
            }
            // Create obj
            foreach (var subValue in value.Split(','))
            {
                var assocObj = Activator.CreateInstance(typeof(TAssocModel))!;
                var targetModel = Activator.CreateInstance(typeof(TTargetModel))!;
                // SET TARGET ACTIVE AND NAME
                var activeProp = targetModel.GetType()
                    .GetProperty(nameof(IBase.Active), BindingFlags.Public | BindingFlags.Instance);
                var nameProp = targetModel.GetType()
                    .GetProperty(nameof(INameableBase.Name), BindingFlags.Public | BindingFlags.Instance);
                if (activeProp?.CanWrite != true)
                {
                    return;
                }
                if (nameProp?.CanWrite != true)
                {
                    return;
                }
                activeProp.SetValue(targetModel, true, null);
                nameProp.SetValue(targetModel, subValue, null);
                // SET ASSOC TARGET AND ACTIVE
                var activeAssocProp = assocObj.GetType()
                    .GetProperty(nameof(IBase.Active), BindingFlags.Public | BindingFlags.Instance);
                var assocTargetProp = assocObj.GetType()
                    .GetProperty(targetPropName, BindingFlags.Public | BindingFlags.Instance);
                if (activeAssocProp?.CanWrite != true)
                {
                    return;
                }
                if (assocTargetProp?.CanWrite != true)
                {
                    return;
                }
                activeAssocProp.SetValue(assocObj, true, null);
                assocTargetProp.SetValue(assocObj, targetModel, null);
                // ADD ASSOC TO LIST
                // ReSharper disable once PossibleNullReferenceException
                newList.GetType().GetMethod(nameof(IList.Add))!.Invoke(newList, new[] { assocObj });
            }
        }

        /// <summary>Sets value to property.</summary>
        /// <typeparam name="TModel">Type of the model.</typeparam>
        /// <param name="propertyName">Name of the property.</param>
        /// <param name="value">       The value.</param>
        /// <param name="model">       The model.</param>
        /// <param name="altName">     Name of the alternate.</param>
        private static void SetValueToProperty<TModel>(
            string propertyName,
            string value,
            ref TModel model,
            string? altName = null)
        {
            if (string.IsNullOrEmpty(value))
            {
                return;
            }
            var prop = Array.Find(
                model!.GetType().GetProperties(BindingFlags.Public | BindingFlags.Instance),
                x => x.Name.Equals(propertyName, StringComparison.CurrentCultureIgnoreCase));
            if (prop == null && Contract.CheckValidKey(altName))
            {
                // Try the alternate name
                prop = Array.Find(
                    model.GetType().GetProperties(BindingFlags.Public | BindingFlags.Instance),
                    x => x.Name.Equals(altName, StringComparison.CurrentCultureIgnoreCase));
            }
            if (prop?.CanWrite != true)
            {
                return;
            }
            if (prop.PropertyType == typeof(string))
            {
                prop.SetValue(model, value, null);
            }
            if (prop.PropertyType == typeof(decimal) || prop.PropertyType == typeof(decimal?))
            {
                var valueToUse = value;
                if (valueToUse == "-")
                {
                    valueToUse = "0";
                }
                if (decimal.TryParse(valueToUse, out var parsed))
                {
                    prop.SetValue(model, parsed, null);
                }
            }
            if (prop.PropertyType == typeof(int) || prop.PropertyType == typeof(int?))
            {
                if (int.TryParse(value, out var parsed))
                {
                    prop.SetValue(model, parsed, null);
                }
            }
            if (prop.PropertyType == typeof(bool) || prop.PropertyType == typeof(bool?))
            {
                var valueToUse = value;
                switch (valueToUse)
                {
                    case "N":
                    case "No":
                    case "NO":
                    case "0":
                    {
                        valueToUse = "false";
                        break;
                    }
                    case "Y":
                    case "Yes":
                    case "YES":
                    case "1":
                    {
                        valueToUse = "true";
                        break;
                    }
                }
                prop.SetValue(model, bool.Parse(valueToUse), null);
            }
        }

        /// <summary>Resolves the record.</summary>
        /// <param name="items">             The items.</param>
        /// <param name="contextProfileName">Name of the context profile.</param>
        /// <returns>A List{IUserModel}.</returns>
        private async Task<List<IUserModel>> ResolveAsync(IEnumerable<IImportItem> items, string? contextProfileName)
        {
            var models = new List<IUserModel>();
            userPropertyNames = typeof(UserModel).GetProperties().Select(p => p.Name).OrderBy(x => x).ToList();
            contactPropertyNames = typeof(ContactModel).GetProperties().Select(p => p.Name).OrderBy(x => x).ToList();
            var haveAlternateKeyName = Contract.CheckValidKey(CEFConfigDictionary.ImportUsersAlternateCustomKeyColumnName);
            var index = 0;
            foreach (var item in items)
            {
                IUserModel? model = null;
                var customKey = item.Fields!
                    .Find(x => string.Equals(x.Name, "customkey", StringComparison.OrdinalIgnoreCase)
                            || haveAlternateKeyName
                                && string.Equals(
                                    x.Name,
                                    CEFConfigDictionary.ImportUsersAlternateCustomKeyColumnName,
                                    StringComparison.OrdinalIgnoreCase))
                    ?.Value;
                if (Contract.CheckValidKey(customKey))
                {
                    var existing = await Workflows.Users.GetAsync(customKey!, contextProfileName).ConfigureAwait(false);
                    if (existing != null)
                    {
                        model = existing;
                    }
                }
                if (model == null)
                {
                    var email = item.Fields
                        .Find(x => string.Equals(x.Name, "email", StringComparison.OrdinalIgnoreCase)
                                || string.Equals(x.Name, "e-mail", StringComparison.OrdinalIgnoreCase))
                        ?.Value;
                    if (Contract.CheckValidKey(email))
                    {
                        var existing = await Workflows.Users.GetByEmailAsync(email!, contextProfileName).ConfigureAwait(false);
                        if (existing != null)
                        {
                            model = existing;
                        }
                    }
                }
                model ??= RegistryLoaderWrapper.GetInstance<IUserModel>(contextProfileName);
                ResolveItem(item, ref model, ++index);
                models.Add(model);
            }
            return models;
        }

        // ReSharper disable once CyclomaticComplexity
        private void ResolveItem(IImportItem item, ref IUserModel model, int index)
        {
            var emailFound = false;
            var firstNameFound = false;
            var lastNameFound = false;
            foreach (var field in item.Fields!)
            {
                var value = field.Value!.Trim();
                if (string.IsNullOrEmpty(field.Name))
                {
                    continue;
                }
                var fieldName = field.Name!.Trim().ToLower();
                while (fieldName.Contains(" "))
                {
                    fieldName = fieldName.Replace(" ", string.Empty);
                }
                while (fieldName.Contains("-"))
                {
                    fieldName = fieldName.Replace("-", string.Empty);
                }
                while (fieldName.Contains("_"))
                {
                    fieldName = fieldName.Replace("_", string.Empty);
                }
                while (fieldName.Contains("."))
                {
                    fieldName = fieldName.Replace(".", string.Empty);
                }
                while (fieldName.Contains("*"))
                {
                    fieldName = fieldName.Replace("*", string.Empty);
                }
                while (fieldName.Contains("("))
                {
                    fieldName = fieldName.Replace("(", string.Empty);
                }
                while (fieldName.Contains(")"))
                {
                    fieldName = fieldName.Replace(")", string.Empty);
                }
                while (fieldName.Contains("/"))
                {
                    fieldName = fieldName.Replace("/", string.Empty);
                }
                while (fieldName.Contains("\\"))
                {
                    fieldName = fieldName.Replace("\\", string.Empty);
                }
                while (fieldName.Contains("\""))
                {
                    fieldName = fieldName.Replace("\"", string.Empty);
                }
                while (fieldName.Contains("%"))
                {
                    fieldName = fieldName.Replace("%", "percent");
                }
                while (fieldName.Contains("#"))
                {
                    fieldName = fieldName.Replace("#", "number");
                }
                while (fieldName.Contains("nbr"))
                {
                    fieldName = fieldName.Replace("nbr", "number");
                }
                while (fieldName.Contains("e-mail"))
                {
                    fieldName = fieldName.Replace("e-mail", "email");
                }
                string? assignToOtherEntity = null;
                if (Contract.CheckValidKey(CEFConfigDictionary.ImportUsersAlternateCustomKeyColumnName)
                    && (fieldName == CEFConfigDictionary.ImportUsersAlternateCustomKeyColumnName!.ToLower()
                        || field.Name == CEFConfigDictionary.ImportUsersAlternateCustomKeyColumnName))
                {
                    fieldName = nameof(IUser.CustomKey);
                }
                switch (fieldName)
                {
                    case "type":
                    case "usertype":
                    {
                        if (userPropertyNames!.Contains(nameof(IUser.Type)))
                        {
                            SetType(value, ref model);
                        }
                        continue;
                    }
                    case "stores":
                    {
                        if (userPropertyNames!.Contains(nameof(IUser.Stores)))
                        {
                            SetAssociatedObject<IUserModel, StoreUserModel, StoreModel>(
                                value, nameof(User.Stores), nameof(StoreUser.Master), ref model);
                        }
                        continue;
                    }
                    case "givenname":
                    {
                        firstNameFound = true;
                        fieldName = nameof(IUser.Contact.FirstName);
                        assignToOtherEntity = nameof(IUser.Contact);
                        break;
                    }
                    case "familyname":
                    {
                        lastNameFound = true;
                        fieldName = nameof(IUser.Contact.LastName);
                        assignToOtherEntity = nameof(IUser.Contact);
                        break;
                    }
                    case "memberstatus":
                    {
                        // Creates an Attribute
                        fieldName = "Member Status";
                        break;
                    }
                    case "localaccountnumber":
                    {
                        // Creates an Attribute
                        fieldName = "Local Account Number";
                        break;
                    }
                    case "accountcreationdate":
                    {
                        // Creates an Attribute
                        fieldName = "Account Creation Date";
                        break;
                    }
                    case "email":
                    {
                        emailFound = true;
                        break;
                    }
                }
                if (Contract.CheckValidKey(assignToOtherEntity))
                {
                    switch (assignToOtherEntity)
                    {
                        case nameof(IUser.Contact):
                        {
                            model.Contact ??= new ContactModel
                            {
                                Active = true,
                                CreatedDate = model.CreatedDate,
                            };
                            var contact = model.Contact;
                            // Check if the model has a property with the same name as FieldName
                            if (contactPropertyNames!.Contains(fieldName)
                                || contactPropertyNames.Select(x => x.ToLower()).Contains(fieldName.ToLower()))
                            {
                                SetValueToProperty(field.Name, value, ref contact, fieldName);
                                continue;
                            }
                            // Create attribute
                            CreateAttribute(field.Name, value, ref contact);
                            break;
                        }
                    }
                    continue;
                }
                // Check if the model has a property with the same name as FieldName
                if (userPropertyNames!.Contains(fieldName)
                    || userPropertyNames.Select(x => x.ToLower()).Contains(fieldName.ToLower()))
                {
                    SetValueToProperty(field.Name, value, ref model, fieldName);
                    continue;
                }
                // Create attribute
                CreateAttribute(field.Name, value, ref model);
            }
            if (!emailFound)
            {
                throw new ArgumentException($"ERROR! Email is required. Row {index + 1}");
            }
            if (!firstNameFound)
            {
                throw new ArgumentException($"ERROR! First/Given is required. Row {index + 1}");
            }
            if (!lastNameFound)
            {
                throw new ArgumentException($"ERROR! Last/Family Name is required. Row {index + 1}");
            }
        }

        private IEnumerable<string> GetParsingError()
        {
            return ParsingErrorList;
        }

        private bool Load(ISpreadsheetImportModel spsModel)
        {
            switch (spsModel.ImportType)
            {
                case Enums.ImportType.XLSX:
                {
                    // 2. Reading from a OpenXml Excel file (2007+ format; *.xlsx)
                    excelDataReader = ExcelReaderFactory.CreateOpenXmlReader(spsModel.SpreadsheetStream);
                    break;
                }
            }
            return true;
        }

        private IEnumerable<IImportItem> Parse()
        {
            // TEST
            var items = new List<IImportItem>();
            excelDataReader!.IsFirstRowAsColumnNames = true;
            excelDataReader.AsDataSet();
            var currentRow = 0;
            // 5. Data Reader methods
            var columnNames = new string[excelDataReader.FieldCount];
            var haveAltCustomKeyName = Contract.CheckValidKey(CEFConfigDictionary.ImportUsersAlternateCustomKeyColumnName);
            while (excelDataReader.Read())
            {
                var item = new ImportItem
                {
                    Fields = new(),
                };
                if (currentRow == 0)
                {
                    for (var i = 0; i != excelDataReader.FieldCount; i++)
                    {
                        columnNames[i] = excelDataReader.GetString(i);
                    }
                    currentRow++;
                    continue;
                }
                var doBreak = false;
                for (var i = 0; i != excelDataReader.FieldCount; i++)
                {
                    var columnName = columnNames[i];
                    var value = excelDataReader.GetString(i);
                    if (string.IsNullOrEmpty(value))
                    {
                        continue;
                    }
                    switch (columnName)
                    {
                        default:
                        {
                            if (value == "MyTestUser"
                                && (string.Equals(columnName, "username", StringComparison.OrdinalIgnoreCase)
                                    || haveAltCustomKeyName
                                        && string.Equals(
                                            columnName,
                                            CEFConfigDictionary.ImportUsersAlternateCustomKeyColumnName,
                                            StringComparison.OrdinalIgnoreCase)))
                            {
                                doBreak = true;
                                break;
                            }
                            item.Fields.Add(new ImportField
                            {
                                Name = columnName,
                                Value = value,
                            });
                            continue;
                        }
                    }
                    break;
                }
                if (!doBreak && !item.Fields.All(x => string.IsNullOrWhiteSpace(x.Value)))
                {
                    items.Add(item);
                }
                currentRow++;
            }
            excelDataReader.Close();
            return items;
        }
    }

    [PublicAPI, UsedInStorefront,
        Authenticate, RequiredRole("CEF Local Administrator"),
        Route("/Contacts/Users/ImportFromExcelToCurrentAccount", "POST",
        Summary = "Use to import a list of users to the current account. Returns the IDs of all users upserted")]
    public class ImportUsersFromExcelToCurrentAccount : IReturn<CEFActionResponse<List<int>>>
    {
        /// <summary>Gets or sets the filename of the file.</summary>
        /// <value>The name of the file.</value>
        [ApiMember(Name = nameof(FileName), DataType = "string", ParameterType = "body", IsRequired = true)]
        public string FileName { get; set; } = null!;

        /// <summary>Gets or sets the identifier of the account.</summary>
        /// <value>The identifier of the account.</value>
        [ApiMember(Name = nameof(AccountID), DataType = "int?", ParameterType = "body", IsRequired = false)]
        public int? AccountID { get; set; }
    }

    public class LocalAdministratorService : ClarityEcommerceServiceBase
    {
#pragma warning disable CA1822,IDE1006
        public async Task<object?> Post(ImportUsersFromExcelToCurrentAccount request)
#pragma warning restore CA1822,IDE1006
        {
            Contract.RequiresValidKey(request.FileName, "ERROR! FileName is required");
            var provider = RegistryLoaderWrapper.GetFilesProvider(contextProfileName: null);
            var rootPath = await provider!.GetFileSaveRootPathFromFileEntityTypeAsync(
                    Enums.FileEntityType.ImportUser)
                .ConfigureAwait(false);
            var filePath = Path.Combine(rootPath, request.FileName);
            var stream = File.OpenRead(filePath);
            var spreadsheetModel = new SpreadsheetImportModel
            {
                SpreadsheetStream = stream,
            };
            switch (request.FileName.GetExtension().ToLower())
            {
                case ".xlsx":
                {
                    spreadsheetModel.ImportType = Enums.ImportType.XLSX;
                    break;
                }
                case ".xls":
                {
                    return CEFAR.FailingCEFAR<List<int>>("ERROR! Cannot process XLS files, re-save the file as an XLSX");
                }
                case ".csv":
                {
                    return CEFAR.FailingCEFAR<List<int>>("ERROR! Cannot process CSV files, re-save the file as an XLSX");
                }
                default:
                {
                    return CEFAR.FailingCEFAR<List<int>>("ERROR! Extension not supported");
                }
            }
            return await new UserImportProvider().IntegrateUsersAsync(
                    spreadsheetModel,
                    await LocalAdminAccountIDOrThrow401Async(request.AccountID ?? CurrentAccountIDOrThrow401).ConfigureAwait(false),
                    contextProfileName: null)
                .ConfigureAwait(false);
        }
    }
}
