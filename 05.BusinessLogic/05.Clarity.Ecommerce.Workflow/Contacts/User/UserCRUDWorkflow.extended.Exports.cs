// <copyright file="UserCRUDWorkflow.extended.Exports.cs" company="clarity-ventures.com">
// Copyright (c) 2015-2023 clarity-ventures.com. All rights reserved.
// </copyright>
// <summary>Implements the user workflow class</summary>
namespace Clarity.Ecommerce.Workflow
{
    using System.Collections.Generic;
    using System.Data;
    using System.Threading.Tasks;
    using Interfaces.Models;

    public partial class UserWorkflow
    {
        /// <inheritdoc/>
        public async Task<DataSet> ExportToExcelAsync(IUserSearchModel request, string? contextProfileName)
        {
            var users = (await SearchAsync(request, true, contextProfileName).ConfigureAwait(false)).results;
            return ExportToExcelDataSet(users);
        }

        /// <summary>Export to excel data set.</summary>
        /// <param name="users">The users.</param>
        /// <returns>A DataSet.</returns>
        private static DataSet ExportToExcelDataSet(IEnumerable<IUserModel> users)
        {
            var dataSet = new DataSet();
            var userTable = dataSet.Tables.Add("Users");
            // Base Properties
            userTable.Columns.Add("ID");
            userTable.Columns.Add("CustomKey");
            userTable.Columns.Add("Active");
            userTable.Columns.Add("CreatedDate");
            userTable.Columns.Add("UpdatedDate");
            // Other Properties
            userTable.Columns.Add("FirstName");
            userTable.Columns.Add("LastName");
            userTable.Columns.Add("Email");
            userTable.Columns.Add("Phone");

            userTable.Columns.Add("Street1");
            userTable.Columns.Add("Street2");
            userTable.Columns.Add("City");
            userTable.Columns.Add("Region");
            userTable.Columns.Add("Country");
            userTable.Columns.Add("PostalCode");

            userTable.Columns.Add("UserType");
            foreach (var user in users)
            {
                var row = userTable.NewRow();
                // Base Properties
                row["ID"] = user.ID;
                row["CustomKey"] = user.CustomKey;
                row["Active"] = user.Active;
                row["CreatedDate"] = user.CreatedDate;
                row["UpdatedDate"] = user.UpdatedDate;
                // Other Properties
                row["FirstName"] = user.ContactFirstName;
                row["LastName"] = user.ContactLastName;
                row["Email"] = user.Email;
                row["Phone"] = user.PhoneNumber;
                row["Street1"] = user.BillingAddress?.Street1;
                row["Street2"] = user.BillingAddress?.Street2;
                row["City"] = user.BillingAddress?.City;
                row["Region"] = user.BillingAddress?.RegionCode;
                row["Country"] = user.BillingAddress?.CountryName;
                row["PostalCode"] = user.BillingAddress?.PostalCode;
                row["UserType"] = user.TypeName;
                userTable.Rows.Add(row);
            }
            return dataSet;
        }
    }
}
