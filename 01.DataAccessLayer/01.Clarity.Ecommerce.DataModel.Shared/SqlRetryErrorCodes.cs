// <copyright file="SqlRetryErrorCodes.cs" company="clarity-ventures.com">
// Copyright (c) 2016-2023 clarity-ventures.com. All rights reserved.
// </copyright>
// <summary>Implements the SQL Retry Error Codes class</summary>
// <remarks>This class was discovered at:
// http://www.entityframework.info/Home/MetadataValidation
// </remarks>
// ReSharper disable NotAccessedField.Global, UnusedMember.Global
namespace Clarity.Ecommerce.DataModel
{
    public static class SqlRetryErrorCodes
    {
        public const int TimeoutExpired = -2;
        public const int Deadlock = 1205;
        public const int CouldNotOpenConnection = 53;
        public const int TransportFail = 121;
    }
}
