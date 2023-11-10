// Decompiled with Telerik decompiler
// Type: Microsoft.AspNet.Identity.EntityFramework.IdentityDbContext`1
// Assembly: Microsoft.AspNet.Identity.EntityFramework, Version=2.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35
// MVID: 67CFF174-6DA7-49B9-BAF6-D5DB9B7CBCAA
// Assembly location: C:\Users\jotha\.nuget\packages\microsoft.aspnet.identity.entityframework\2.2.3\lib\net45\Microsoft.AspNet.Identity.EntityFramework.dll

namespace Microsoft.AspNet.Identity.EntityFramework
{
    using System;
    using System.Collections.Generic;
    using System.Data.Common;
    using System.Data.Entity;
    using System.Data.Entity.Infrastructure;
    using System.Data.SqlClient;
    using System.Linq;

    /// <summary>DbContext which uses a custom user entity with a string primary key.</summary>
    /// <typeparam name="TUser">.</typeparam>
    /// <seealso cref="IdentityDbContext{TUser,IdentityRole,String,IdentityUserLogin,IdentityUserRole,IdentityUserClaim}"/>
    public class IdentityDbContext<TUser>
        : IdentityDbContext<TUser, IdentityRole, string, IdentityUserLogin, IdentityUserRole, IdentityUserClaim>
        where TUser : IdentityUser
    {
        /// <summary>Default constructor which uses the DefaultConnection.</summary>
        public IdentityDbContext() : this("DefaultConnection") { }

        /// <summary>Constructor which takes the connection string to use.</summary>
        /// <param name="nameOrConnectionString">.</param>
        public IdentityDbContext(string nameOrConnectionString) : this(nameOrConnectionString, true) { }

        /// <summary>Constructor which takes the connection string to use.</summary>
        /// <param name="nameOrConnectionString">.</param>
        /// <param name="throwIfV1Schema">       Will throw an exception if the schema matches that of Identity 1.0.0.</param>
        public IdentityDbContext(string nameOrConnectionString, bool throwIfV1Schema) : base(nameOrConnectionString)
        {
            if (throwIfV1Schema && IsIdentityV1Schema(this))
            {
                throw new InvalidOperationException(IdentityResources.IdentityV1SchemaError);
            }
        }

        /// <summary>Constructs a new context instance using the existing connection to connect to a database, and
        /// initializes it from the given model.  The connection will not be disposed when the context is disposed if
        /// contextOwnsConnection is false.</summary>
        /// <param name="existingConnection">   An existing connection to use for the new context.</param>
        /// <param name="model">                The model that will back this context.</param>
        /// <param name="contextOwnsConnection">Constructs a new context instance using the existing connection to
        ///                                     connect to a database, and initializes it from the given model.  The
        ///                                     connection will not be disposed when the context is disposed if
        ///                                     contextOwnsConnection is false.</param>
        public IdentityDbContext(DbConnection existingConnection, DbCompiledModel model, bool contextOwnsConnection)
            : base(existingConnection, model, contextOwnsConnection)
        {
        }

        /// <summary>Constructs a new context instance using conventions to create the name of the database to which a
        /// connection will be made, and initializes it from the given model.  The by-convention name is the full name
        /// (namespace + class name) of the derived context class.  See the class remarks for how this is used to create
        /// a connection.</summary>
        /// <param name="model">The model that will back this context.</param>
        public IdentityDbContext(DbCompiledModel model)
            : base(model)
        {
        }

        /// <summary>Constructs a new context instance using the existing connection to connect to a database.  The
        /// connection will not be disposed when the context is disposed if contextOwnsConnection is false.</summary>
        /// <param name="existingConnection">   An existing connection to use for the new context.</param>
        /// <param name="contextOwnsConnection">If set to true the connection is disposed when the context is disposed,
        ///                                     otherwise the caller must dispose the connection.</param>
        public IdentityDbContext(DbConnection existingConnection, bool contextOwnsConnection)
            : base(existingConnection, contextOwnsConnection)
        {
        }

        /// <summary>Constructs a new context instance using the given string as the name or connection string for the
        /// database to which a connection will be made, and initializes it from the given model.  See the class remarks
        /// for how this is used to create a connection.</summary>
        /// <param name="nameOrConnectionString">Either the database name or a connection string.</param>
        /// <param name="model">                 The model that will back this context.</param>
        public IdentityDbContext(string nameOrConnectionString, DbCompiledModel model)
            : base(nameOrConnectionString, model)
        {
        }

        /// <summary>Query if 'db' is identity v 1 schema.</summary>
        /// <param name="db">The database.</param>
        /// <returns>True if identity v 1 schema, false if not.</returns>
        internal static bool IsIdentityV1Schema(DbContext db)
        {
            if (db.Database.Connection is not SqlConnection connection || !db.Database.Exists())
            {
                return false;
            }
            using (var conn = new SqlConnection(connection.ConnectionString))
            {
                conn.Open();
                int num;
                if (VerifyColumns(
                    conn,
                    "AspNetUsers",
                    "Id",
                    "UserName",
                    "PasswordHash",
                    "SecurityStamp",
                    "Discriminator"))
                {
                    if (VerifyColumns(conn, "AspNetRoles", "Id", "Name")
                        && VerifyColumns(conn, "AspNetUserRoles", "UserId", "RoleId")
                        && VerifyColumns(conn, "AspNetUserClaims", "Id", "ClaimType", "ClaimValue", "User_Id"))
                    {
                        num = VerifyColumns(conn, "AspNetUserLogins", "UserId", "ProviderKey", "LoginProvider")
                            ? 1
                            : 0;
                        goto label0;
                    }
                }
                num = 0;
            label0:
                return num != 0;
            }
        }

        /// <summary>Verify columns.</summary>
        /// <param name="conn">   The connection.</param>
        /// <param name="table">  The table.</param>
        /// <param name="columns">A variable-length parameters list containing columns.</param>
        /// <returns>True if it succeeds, false if it fails.</returns>
        internal static bool VerifyColumns(SqlConnection conn, string table, params string[] columns)
        {
            var strs = new List<string>();
            using (var sqlCommand = new SqlCommand(
                "SELECT COLUMN_NAME FROM INFORMATION_SCHEMA.COLUMNS where TABLE_NAME=@Table",
                conn))
            {
                sqlCommand.Parameters.Add(new SqlParameter("Table", table));
                using var sqlDataReader = sqlCommand.ExecuteReader();
                while (sqlDataReader.Read())
                {
                    strs.Add(sqlDataReader.GetString(0));
                }
            }
            var strs1 = strs;
            return columns.All(strs1.Contains);
        }
    }
}
