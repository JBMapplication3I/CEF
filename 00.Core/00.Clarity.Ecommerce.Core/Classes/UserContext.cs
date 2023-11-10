// <copyright file="UserContext.cs" company="clarity-ventures.com">
// Copyright (c) 2023 clarity-ventures.com. All rights reserved.
// </copyright>
// <summary>Implements the UserContext class.</summary>
namespace Clarity.Ecommerce.Models
{
    using Utilities;

    /// <summary>Provides details about the physical and logical user we're performing requests for.</summary>
    public class UserContext
    {
        /// <summary>Initializes a new instance of the <see cref="UserContext"/> class. Create a context to identify the
        /// user(s) we're doing things for.</summary>
        /// <remarks>Will validate the physical/logical/account IDs via <see cref="Contract.RequiresValidID(int?,string?)"/>.</remarks>
        /// <param name="physicalUserID"> Gets the user physically doing things (e.g.- CSR).</param>
        /// <param name="logicalUserID">  Gets the user the actions are being performed on the behalf of. May match
        ///                               <see cref="PhysicalUserID" />.</param>
        /// <param name="logicalUserName">Gets username of the <see cref="LogicalUserID" />.</param>
        /// <param name="accountID">      Gets the account ID the actions are being performed on behalf of.</param>
        /// <param name="accountKey">     Gets the CustomKey of <see cref="AccountID" />.</param>
        public UserContext(int physicalUserID, int logicalUserID, string logicalUserName, int accountID, string accountKey)
        {
            PhysicalUserID = Contract.RequiresValidID(physicalUserID);
            LogicalUserID = Contract.RequiresValidID(logicalUserID);
            LogicalUserName = logicalUserName;
            AccountID = Contract.RequiresValidID(accountID);
            AccountKey = accountKey;
        }

        /// <summary>Gets the account ID the actions are being performed on behalf of.</summary>
        /// <value>The account ID the actions are being performed on behalf of.</value>
        public int AccountID { get; }

        /// <summary>Gets the CustomKey of <see cref="AccountID" />.</summary>
        /// <value>The CustomKey of <see cref="AccountID" />.</value>
        public string AccountKey { get; }

        /// <summary>Gets the user the actions are being performed on the behalf of. May match <see cref="PhysicalUserID" />.</summary>
        /// <remarks>This is who wallets would come from, invoices would be under, etc.</remarks>
        /// <value>The user the actions are being performed on the behalf of.</value>
        public int LogicalUserID { get; }

        /// <summary>Gets username of the <see cref="LogicalUserID" />.</summary>
        /// <value>The Username of the <see cref="LogicalUserID" />.</value>
        public string LogicalUserName { get; }

        /// <summary>Gets the user physically doing things (e.g.- CSR).</summary>
        /// <value>The user physically doing things (e.g.- CSR).</value>
        public int PhysicalUserID { get; }

        /// <summary>Gets a value indicating whether we're emulating a user (i.e is a CSR doing this?).</summary>
        /// <value>True if we are emulating a user.</value>
        public bool UserIsEmulated => LogicalUserID != PhysicalUserID;
    }
}
