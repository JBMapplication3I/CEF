// <copyright file="CEFErrorCodeLibrary.cs" company="clarity-ventures.com">
// Copyright (c) 2019-2023 clarity-ventures.com. All rights reserved.
// </copyright>
// <summary>Implements the CEF error code library class</summary>
// ReSharper disable BadCommaSpaces, InconsistentNaming, MultipleSpaces, UnusedMember.Global
// ReSharper disable StyleCop.SA1001, StyleCop.SA1025, StyleCop.SA1310, StyleCop.SA1602
#pragma warning disable format
namespace Clarity.Ecommerce
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel;
    using System.ComponentModel.Design;
#if !NET5_0_OR_GREATER
    using System.Configuration;
#endif
    using System.Data;
    using System.Data.Entity.Core;
    using System.Data.Entity.Migrations;
    using System.Data.Entity.Migrations.Infrastructure;
    using System.Data.SqlClient;
    using System.Data.SqlTypes;
    using System.IO;
    using System.IO.IsolatedStorage;
    using System.Linq;
    using System.Net;
    using System.Net.Mail;
    using System.Net.NetworkInformation;
    using System.Net.Sockets;
    using System.Reflection;
    using System.Resources;
    using System.Runtime.InteropServices;
#if !NET5_0_OR_GREATER
    using System.Runtime.Remoting;
#endif
    using System.Runtime.Serialization;
    using System.Security;
    using System.Security.AccessControl;
    using System.Security.Authentication;
    using System.Security.Cryptography;
    using System.Security.Policy;
    using System.Security.Principal;
    using System.Text;
    using System.Threading;
#if !NET5_0_OR_GREATER
    using System.Web;
    using System.Web.Caching;
    using System.Web.Management;
    using System.Web.Security;
    using System.Web.UI;
#endif
    using System.Xml;
    using System.Xml.Schema;
    using System.Xml.XPath;
    using System.Xml.Xsl;

    public static class CEFErrorCodeLibrary
    {
        public enum CEFErrorCodes
        {
            #region Standard System.* Errors like InvalidOperationException, FileNotFoundException, ArgumentNullException
            InvalidCredentialException                 = 10000,
            SemaphoreFullException                     = 10001,
            AuthenticationException                    = 10002,
            SocketException                            = 10003,
            PingException                              = 10004,
            NetworkInformationException                = 10005,
            SmtpFailedRecipientsException              = 10006,
            SmtpFailedRecipientException               = 10007,
            SmtpException                              = 10008,
            WebException                               = 10009,
            ProtocolViolationException                 = 10010,
            HttpListenerException                      = 10011,
            CookieException                            = 10012,
            InvalidDataException                       = 10013,
            InternalBufferOverflowException            = 10014,
            SettingsPropertyWrongTypeException         = 10015,
            SettingsPropertyNotFoundException          = 10016,
            SettingsPropertyIsReadOnlyException        = 10017,
            ConfigurationErrorsException               = 10018,
            ConfigurationException                     = 10019,
            CheckoutException                          = 10020,
            Win32Exception                             = 10021,
            WarningException                           = 10022,
            LicenseException                           = 10023,
            InvalidEnumArgumentException               = 10024,
            InvalidAsynchronousStateException          = 10025,
            UriFormatException                         = 10026,
            XsltCompileException                       = 10027,
            XsltException                              = 10028,
            XPathException                             = 10029,
            XmlSchemaValidationException               = 10030,
            XmlSchemaInferenceException                = 10031,
            XmlSchemaException                         = 10032,
            XmlException                               = 10033,
            ViewStateException                         = 10034,
            MembershipPasswordException                = 10035,
            MembershipCreateUserException              = 10036,
            SqlExecutionException                      = 10037,
            TableNotEnabledForNotificationException    = 10038,
            DatabaseNotEnabledForNotificationException = 10039,
            HttpUnhandledException                     = 10040,
            HttpRequestValidationException             = 10041,
            HttpParseException                         = 10042,
            HttpCompileException                       = 10043,
            HttpException                              = 10044,
            SqlTruncateException                       = 10045,
            SqlNullValueException                      = 10046,
            SqlNotFilledException                      = 10047,
            SqlAlreadyFilledException                  = 10048,
            SqlTypeException                           = 10049,
            VersionNotFoundException                   = 10050,
            TypedDataSetGeneratorException             = 10051,
            SyntaxErrorException                       = 10052,
            StrongTypingException                      = 10053,
            RowNotInTableException                     = 10054,
            ReadOnlyException                          = 10055,
            NoNullAllowedException                     = 10056,
            MissingPrimaryKeyException                 = 10057,
            EvaluateException                          = 10058,
            InvalidExpressionException                 = 10059,
            InvalidConstraintException                 = 10060,
            InRowChangingEventException                = 10061,
            DuplicateNameException                     = 10062,
            DeletedRowInaccessibleException            = 10063,
            DBConcurrencyException                     = 10064,
            ConstraintException                        = 10065,
            MetadataException                          = 10066,
            DataException                              = 10067,
            ProviderException                          = 10068,
            WaitHandleCannotBeOpenedException          = 10069,
            ThreadStateException                       = 10070,
            ThreadInterruptedException                 = 10071,
            SynchronizationLockException               = 10072,
            AbandonedMutexException                    = 10073,
            EncoderFallbackException                   = 10074,
            DecoderFallbackException                   = 10075,
            IdentityNotMappedException                 = 10076,
            PolicyException                            = 10077,
            CryptographicUnexpectedOperationException  = 10078,
            CryptographicException                     = 10079,
            PrivilegeNotHeldException                  = 10080,
            XmlSyntaxException                         = 10081,
            VerificationException                      = 10082,
            SecurityException                          = 10083,
            HostProtectionException                    = 10084,
            SerializationException                     = 10085,
            ServerException                            = 10086,
            RemotingTimeoutException                   = 10087,
            RemotingException                          = 10088,
            SEHException                               = 10089,
            SafeArrayTypeMismatchException             = 10090,
            SafeArrayRankMismatchException             = 10091,
            MarshalDirectiveException                  = 10092,
            InvalidOleVariantTypeException             = 10093,
            InvalidComObjectException                  = 10094,
            COMException                               = 10095,
            ExternalException                          = 10096,
            MissingSatelliteAssemblyException          = 10097,
            MissingManifestResourceException           = 10098,
            TargetParameterCountException              = 10099,
            TargetInvocationException                  = 10100,
            TargetException                            = 10101,
            ReflectionTypeLoadException                = 10102,
            InvalidFilterCriteriaException             = 10103,
            CustomAttributeFormatException             = 10104,
            AmbiguousMatchException                    = 10105,
            IsolatedStorageException                   = 10106,
            PathTooLongException                       = 10107,
            FileNotFoundException                      = 10108,
            FileLoadException                          = 10109,
            EndOfStreamException                       = 10110,
            DriveNotFoundException                     = 10111,
            DirectoryNotFoundException                 = 10112,
            IOException                                = 10113,
            KeyNotFoundException                       = 10114,
            UnauthorizedAccessException                = 10115,
            TypeUnloadedException                      = 10116,
            DllNotFoundException                       = 10117,
            EntryPointNotFoundException                = 10118,
            TypeLoadException                          = 10119,
            TypeInitializationException                = 10120,
            TimeoutException                           = 10121,
            StackOverflowException                     = 10122,
            RankException                              = 10123,
            PlatformNotSupportedException              = 10124,
            OverflowException                          = 10125,
            InsufficientMemoryException                = 10126,
            OutOfMemoryException                       = 10127,
            OperationCanceledException                 = 10128,
            ObjectDisposedException                    = 10129,
            NullReferenceException                     = 10130,
            NotSupportedException                      = 10131,
            NotImplementedException                    = 10132,
            NotFiniteNumberException                   = 10133,
            MulticastNotSupportedException             = 10134,
            MissingMethodException                     = 10135,
            MissingFieldException                      = 10136,
            MissingMemberException                     = 10137,
            MethodAccessException                      = 10138,
            FieldAccessException                       = 10139,
            MemberAccessException                      = 10140,
            InvalidProgramException                    = 10141,
            InvalidOperationException                  = 10142,
            InvalidCastException                       = 10143,
            IndexOutOfRangeException                   = 10144,
            FormatException                            = 10145,
            DuplicateWaitObjectException               = 10146,
            DivideByZeroException                      = 10147,
            DataMisalignedException                    = 10148,
            ContextMarshalException                    = 10149,
            CannotUnloadAppDomainException             = 10150,
            BadImageFormatException                    = 10151,
            ArrayTypeMismatchException                 = 10152,
            ArithmeticException                        = 10153,
            ArgumentOutOfRangeException                = 10154,
            ArgumentNullException                      = 10155,
            ArgumentException                          = 10156,
            ApplicationException                       = 10157,
            AppDomainUnloadedException                 = 10158,
            AccessViolationException                   = 10159,
            SystemException                            = 10160,
            Exception                                  = 10161,
            #endregion
            #region SQL/Database Errors
            EFMigration                                = 20000,
            #endregion
            #region StructureMap/Registry/MEF Errors
            MissingProvider_AddressValidation          = 31001,
            MissingProvider_Chatting                   = 31002,
            MissingProvider_Checkouts                  = 31003,
            MissingProvider_CurrencyConversions        = 31004,
            MissingProvider_Files                      = 31005,
            MissingProvider_Importer                   = 31006,
            MissingProvider_Memberships                = 31007,
            MissingProvider_Packaging                  = 31008,
            MissingProvider_Payments                   = 31009,
            MissingProvider_Personalization            = 31010,
            MissingProvider_Pricing                    = 31011,
            MissingProvider_SalesQuoteImporter         = 31012,
            MissingProvider_Searching                  = 31013,
            MissingProvider_Shipping                   = 31014,
            MissingProvider_Surveys                    = 31015,
            MissingProvider_Taxes                      = 31016,
            #endregion
        }

        /// <summary>Convert raw exception to error code.</summary>
        /// <param name="ex">   The exception.</param>
        /// <param name="logID">(Optional) Identifier for the log.</param>
        /// <returns>The raw converted exception to error code.</returns>
        public static object ConvertRawExceptionToErrorCode(this Exception ex, Guid? logID = null)
        {
            return ConvertRawExceptionToErrorCode(ex, logID?.ToString());
        }

        /// <summary>Convert raw exception to error code.</summary>
        /// <param name="ex">   The exception.</param>
        /// <param name="logID">(Optional) Identifier for the log.</param>
        /// <returns>The raw converted exception to error code.</returns>
        // ReSharper disable once CyclomaticComplexity
        public static object ConvertRawExceptionToErrorCode(this Exception ex, string? logID = null)
        {
            // TODO: Recursively loop through inner exceptions for more/better info
            if (ex.Message.Contains("The model backing the 'ClarityEcommerceEntities'")
                || ex.Message.Contains("Unable to update database to match the current model"))
            {
                return new ErrorCodeWithMessage(
                    (int)CEFErrorCodes.EFMigration,
                    logID: logID,
                    message: GetLastMigration());
            }
            switch (ex)
            {
#if !NET5_0_OR_GREATER
                case ViewStateException                         ex10034: { return new ErrorCodeWithMessage((int)CEFErrorCodes.ViewStateException                         , logID, ex10034.Message); }
                case MembershipPasswordException                ex10035: { return new ErrorCodeWithMessage((int)CEFErrorCodes.MembershipPasswordException                , logID, ex10035.Message); }
                case MembershipCreateUserException              ex10036: { return new ErrorCodeWithMessage((int)CEFErrorCodes.MembershipCreateUserException              , logID, ex10036.Message); }
                case SqlExecutionException                      ex10037: { return new ErrorCodeWithMessage((int)CEFErrorCodes.SqlExecutionException                      , logID, ex10037.Message); }
                case TableNotEnabledForNotificationException    ex10038: { return new ErrorCodeWithMessage((int)CEFErrorCodes.TableNotEnabledForNotificationException    , logID, ex10038.Message); }
                case DatabaseNotEnabledForNotificationException ex10039: { return new ErrorCodeWithMessage((int)CEFErrorCodes.DatabaseNotEnabledForNotificationException , logID, ex10039.Message); }
                case HttpUnhandledException                     ex10040: { return new ErrorCodeWithMessage((int)CEFErrorCodes.HttpUnhandledException                     , logID, ex10040.Message); }
                case HttpRequestValidationException             ex10041: { return new ErrorCodeWithMessage((int)CEFErrorCodes.HttpRequestValidationException             , logID, ex10041.Message); }
                case HttpParseException                         ex10042: { return new ErrorCodeWithMessage((int)CEFErrorCodes.HttpParseException                         , logID, ex10042.Message); }
                case HttpCompileException                       ex10043: { return new ErrorCodeWithMessage((int)CEFErrorCodes.HttpCompileException                       , logID, ex10043.Message); }
                case HttpException                              ex10044: { return new ErrorCodeWithMessage((int)CEFErrorCodes.HttpException                              , logID, ex10044.Message); }
                case TypedDataSetGeneratorException             ex10051: { return new ErrorCodeWithMessage((int)CEFErrorCodes.TypedDataSetGeneratorException             , logID, ex10051.Message); }
                case HostProtectionException                    ex10084: { return new ErrorCodeWithMessage((int)CEFErrorCodes.HostProtectionException                    , logID, ex10084.Message); }
                case ServerException                            ex10086: { return new ErrorCodeWithMessage((int)CEFErrorCodes.ServerException                            , logID, ex10086.Message); }
                case RemotingTimeoutException                   ex10087: { return new ErrorCodeWithMessage((int)CEFErrorCodes.RemotingTimeoutException                   , logID, ex10087.Message); }
                case RemotingException                          ex10088: { return new ErrorCodeWithMessage((int)CEFErrorCodes.RemotingException                          , logID, ex10088.Message); }
                case SettingsPropertyWrongTypeException         ex10015: { return new ErrorCodeWithMessage((int)CEFErrorCodes.SettingsPropertyWrongTypeException         , logID, ex10015.Message); }
                case SettingsPropertyNotFoundException          ex10016: { return new ErrorCodeWithMessage((int)CEFErrorCodes.SettingsPropertyNotFoundException          , logID, ex10016.Message); }
                case SettingsPropertyIsReadOnlyException        ex10017: { return new ErrorCodeWithMessage((int)CEFErrorCodes.SettingsPropertyIsReadOnlyException        , logID, ex10017.Message); }
                case ConfigurationErrorsException               ex10018: { return new ErrorCodeWithMessage((int)CEFErrorCodes.ConfigurationErrorsException               , logID, ex10018.Message); }
                case ConfigurationException                     ex10019: { return new ErrorCodeWithMessage((int)CEFErrorCodes.ConfigurationException                     , logID, ex10019.Message); }
#endif
                case InvalidCredentialException                 ex10000: { return new ErrorCodeWithMessage((int)CEFErrorCodes.InvalidCredentialException                 , logID, ex10000.Message); }
                case SemaphoreFullException                     ex10001: { return new ErrorCodeWithMessage((int)CEFErrorCodes.SemaphoreFullException                     , logID, ex10001.Message); }
                case AuthenticationException                    ex10002: { return new ErrorCodeWithMessage((int)CEFErrorCodes.AuthenticationException                    , logID, ex10002.Message); }
                case SocketException                            ex10003: { return new ErrorCodeWithMessage((int)CEFErrorCodes.SocketException                            , logID, ex10003.Message); }
                case PingException                              ex10004: { return new ErrorCodeWithMessage((int)CEFErrorCodes.PingException                              , logID, ex10004.Message); }
                case NetworkInformationException                ex10005: { return new ErrorCodeWithMessage((int)CEFErrorCodes.NetworkInformationException                , logID, ex10005.Message); }
                case SmtpFailedRecipientsException              ex10006: { return new ErrorCodeWithMessage((int)CEFErrorCodes.SmtpFailedRecipientsException              , logID, ex10006.Message); }
                case SmtpFailedRecipientException               ex10007: { return new ErrorCodeWithMessage((int)CEFErrorCodes.SmtpFailedRecipientException               , logID, ex10007.Message); }
                case SmtpException                              ex10008: { return new ErrorCodeWithMessage((int)CEFErrorCodes.SmtpException                              , logID, ex10008.Message); }
                case WebException                               ex10009: { return new ErrorCodeWithMessage((int)CEFErrorCodes.WebException                               , logID, ex10009.Message); }
                case ProtocolViolationException                 ex10010: { return new ErrorCodeWithMessage((int)CEFErrorCodes.ProtocolViolationException                 , logID, ex10010.Message); }
                case HttpListenerException                      ex10011: { return new ErrorCodeWithMessage((int)CEFErrorCodes.HttpListenerException                      , logID, ex10011.Message); }
                case CookieException                            ex10012: { return new ErrorCodeWithMessage((int)CEFErrorCodes.CookieException                            , logID, ex10012.Message); }
                case InvalidDataException                       ex10013: { return new ErrorCodeWithMessage((int)CEFErrorCodes.InvalidDataException                       , logID, ex10013.Message); }
                case InternalBufferOverflowException            ex10014: { return new ErrorCodeWithMessage((int)CEFErrorCodes.InternalBufferOverflowException            , logID, ex10014.Message); }
                case CheckoutException                          ex10020: { return new ErrorCodeWithMessage((int)CEFErrorCodes.CheckoutException                          , logID, ex10020.Message); }
                case Win32Exception                             ex10021: { return new ErrorCodeWithMessage((int)CEFErrorCodes.Win32Exception                             , logID, ex10021.Message); }
                case WarningException                           ex10022: { return new ErrorCodeWithMessage((int)CEFErrorCodes.WarningException                           , logID, ex10022.Message); }
                case LicenseException                           ex10023: { return new ErrorCodeWithMessage((int)CEFErrorCodes.LicenseException                           , logID, ex10023.Message); }
                case InvalidEnumArgumentException               ex10024: { return new ErrorCodeWithMessage((int)CEFErrorCodes.InvalidEnumArgumentException               , logID, ex10024.Message); }
                case InvalidAsynchronousStateException          ex10025: { return new ErrorCodeWithMessage((int)CEFErrorCodes.InvalidAsynchronousStateException          , logID, ex10025.Message); }
                case UriFormatException                         ex10026: { return new ErrorCodeWithMessage((int)CEFErrorCodes.UriFormatException                         , logID, ex10026.Message); }
                case XsltCompileException                       ex10027: { return new ErrorCodeWithMessage((int)CEFErrorCodes.XsltCompileException                       , logID, ex10027.Message); }
                case XsltException                              ex10028: { return new ErrorCodeWithMessage((int)CEFErrorCodes.XsltException                              , logID, ex10028.Message); }
                case XPathException                             ex10029: { return new ErrorCodeWithMessage((int)CEFErrorCodes.XPathException                             , logID, ex10029.Message); }
                case XmlSchemaValidationException               ex10030: { return new ErrorCodeWithMessage((int)CEFErrorCodes.XmlSchemaValidationException               , logID, ex10030.Message); }
                case XmlSchemaInferenceException                ex10031: { return new ErrorCodeWithMessage((int)CEFErrorCodes.XmlSchemaInferenceException                , logID, ex10031.Message); }
                case XmlSchemaException                         ex10032: { return new ErrorCodeWithMessage((int)CEFErrorCodes.XmlSchemaException                         , logID, ex10032.Message); }
                case XmlException                               ex10033: { return new ErrorCodeWithMessage((int)CEFErrorCodes.XmlException                               , logID, ex10033.Message); }
                case SqlTruncateException                       ex10045: { return new ErrorCodeWithMessage((int)CEFErrorCodes.SqlTruncateException                       , logID, ex10045.Message); }
                case SqlNullValueException                      ex10046: { return new ErrorCodeWithMessage((int)CEFErrorCodes.SqlNullValueException                      , logID, ex10046.Message); }
                case SqlNotFilledException                      ex10047: { return new ErrorCodeWithMessage((int)CEFErrorCodes.SqlNotFilledException                      , logID, ex10047.Message); }
                case SqlAlreadyFilledException                  ex10048: { return new ErrorCodeWithMessage((int)CEFErrorCodes.SqlAlreadyFilledException                  , logID, ex10048.Message); }
                case SqlTypeException                           ex10049: { return new ErrorCodeWithMessage((int)CEFErrorCodes.SqlTypeException                           , logID, ex10049.Message); }
                case VersionNotFoundException                   ex10050: { return new ErrorCodeWithMessage((int)CEFErrorCodes.VersionNotFoundException                   , logID, ex10050.Message); }
                case SyntaxErrorException                       ex10052: { return new ErrorCodeWithMessage((int)CEFErrorCodes.SyntaxErrorException                       , logID, ex10052.Message); }
                case StrongTypingException                      ex10053: { return new ErrorCodeWithMessage((int)CEFErrorCodes.StrongTypingException                      , logID, ex10053.Message); }
                case RowNotInTableException                     ex10054: { return new ErrorCodeWithMessage((int)CEFErrorCodes.RowNotInTableException                     , logID, ex10054.Message); }
                case ReadOnlyException                          ex10055: { return new ErrorCodeWithMessage((int)CEFErrorCodes.ReadOnlyException                          , logID, ex10055.Message); }
                case NoNullAllowedException                     ex10056: { return new ErrorCodeWithMessage((int)CEFErrorCodes.NoNullAllowedException                     , logID, ex10056.Message); }
                case MissingPrimaryKeyException                 ex10057: { return new ErrorCodeWithMessage((int)CEFErrorCodes.MissingPrimaryKeyException                 , logID, ex10057.Message); }
                case EvaluateException                          ex10058: { return new ErrorCodeWithMessage((int)CEFErrorCodes.EvaluateException                          , logID, ex10058.Message); }
                case InvalidExpressionException                 ex10059: { return new ErrorCodeWithMessage((int)CEFErrorCodes.InvalidExpressionException                 , logID, ex10059.Message); }
                case InvalidConstraintException                 ex10060: { return new ErrorCodeWithMessage((int)CEFErrorCodes.InvalidConstraintException                 , logID, ex10060.Message); }
                case InRowChangingEventException                ex10061: { return new ErrorCodeWithMessage((int)CEFErrorCodes.InRowChangingEventException                , logID, ex10061.Message); }
                case DuplicateNameException                     ex10062: { return new ErrorCodeWithMessage((int)CEFErrorCodes.DuplicateNameException                     , logID, ex10062.Message); }
                case DeletedRowInaccessibleException            ex10063: { return new ErrorCodeWithMessage((int)CEFErrorCodes.DeletedRowInaccessibleException            , logID, ex10063.Message); }
                case DBConcurrencyException                     ex10064: { return new ErrorCodeWithMessage((int)CEFErrorCodes.DBConcurrencyException                     , logID, ex10064.Message); }
                case ConstraintException                        ex10065: { return new ErrorCodeWithMessage((int)CEFErrorCodes.ConstraintException                        , logID, ex10065.Message); }
                case MetadataException                          ex10066: { return new ErrorCodeWithMessage((int)CEFErrorCodes.MetadataException                          , logID, ex10066.Message); }
                case DataException                              ex10067: { return new ErrorCodeWithMessage((int)CEFErrorCodes.DataException                              , logID, ex10067.Message); }
                // case ProviderException                          ex10068: { return new ErrorCodeWithMessage((int)CEFErrorCodes.ProviderException                          , logID, ex10068.Message); }
                case WaitHandleCannotBeOpenedException          ex10069: { return new ErrorCodeWithMessage((int)CEFErrorCodes.WaitHandleCannotBeOpenedException          , logID, ex10069.Message); }
                case ThreadStateException                       ex10070: { return new ErrorCodeWithMessage((int)CEFErrorCodes.ThreadStateException                       , logID, ex10070.Message); }
                case ThreadInterruptedException                 ex10071: { return new ErrorCodeWithMessage((int)CEFErrorCodes.ThreadInterruptedException                 , logID, ex10071.Message); }
                case SynchronizationLockException               ex10072: { return new ErrorCodeWithMessage((int)CEFErrorCodes.SynchronizationLockException               , logID, ex10072.Message); }
                case AbandonedMutexException                    ex10073: { return new ErrorCodeWithMessage((int)CEFErrorCodes.AbandonedMutexException                    , logID, ex10073.Message); }
                case EncoderFallbackException                   ex10074: { return new ErrorCodeWithMessage((int)CEFErrorCodes.EncoderFallbackException                   , logID, ex10074.Message); }
                case DecoderFallbackException                   ex10075: { return new ErrorCodeWithMessage((int)CEFErrorCodes.DecoderFallbackException                   , logID, ex10075.Message); }
                case IdentityNotMappedException                 ex10076: { return new ErrorCodeWithMessage((int)CEFErrorCodes.IdentityNotMappedException                 , logID, ex10076.Message); }
                case PolicyException                            ex10077: { return new ErrorCodeWithMessage((int)CEFErrorCodes.PolicyException                            , logID, ex10077.Message); }
                case CryptographicUnexpectedOperationException  ex10078: { return new ErrorCodeWithMessage((int)CEFErrorCodes.CryptographicUnexpectedOperationException  , logID, ex10078.Message); }
                case CryptographicException                     ex10079: { return new ErrorCodeWithMessage((int)CEFErrorCodes.CryptographicException                     , logID, ex10079.Message); }
                case PrivilegeNotHeldException                  ex10080: { return new ErrorCodeWithMessage((int)CEFErrorCodes.PrivilegeNotHeldException                  , logID, ex10080.Message); }
                case XmlSyntaxException                         ex10081: { return new ErrorCodeWithMessage((int)CEFErrorCodes.XmlSyntaxException                         , logID, ex10081.Message); }
                case VerificationException                      ex10082: { return new ErrorCodeWithMessage((int)CEFErrorCodes.VerificationException                      , logID, ex10082.Message); }
                case SecurityException                          ex10083: { return new ErrorCodeWithMessage((int)CEFErrorCodes.SecurityException                          , logID, ex10083.Message); }
                case SerializationException                     ex10085: { return new ErrorCodeWithMessage((int)CEFErrorCodes.SerializationException                     , logID, ex10085.Message); }
                case SEHException                               ex10089: { return new ErrorCodeWithMessage((int)CEFErrorCodes.SEHException                               , logID, ex10089.Message); }
                case SafeArrayTypeMismatchException             ex10090: { return new ErrorCodeWithMessage((int)CEFErrorCodes.SafeArrayTypeMismatchException             , logID, ex10090.Message); }
                case SafeArrayRankMismatchException             ex10091: { return new ErrorCodeWithMessage((int)CEFErrorCodes.SafeArrayRankMismatchException             , logID, ex10091.Message); }
                case MarshalDirectiveException                  ex10092: { return new ErrorCodeWithMessage((int)CEFErrorCodes.MarshalDirectiveException                  , logID, ex10092.Message); }
                case InvalidOleVariantTypeException             ex10093: { return new ErrorCodeWithMessage((int)CEFErrorCodes.InvalidOleVariantTypeException             , logID, ex10093.Message); }
                case InvalidComObjectException                  ex10094: { return new ErrorCodeWithMessage((int)CEFErrorCodes.InvalidComObjectException                  , logID, ex10094.Message); }
                case COMException                               ex10095: { return new ErrorCodeWithMessage((int)CEFErrorCodes.COMException                               , logID, ex10095.Message); }
                case ExternalException                          ex10096: { return new ErrorCodeWithMessage((int)CEFErrorCodes.ExternalException                          , logID, ex10096.Message); }
                case MissingSatelliteAssemblyException          ex10097: { return new ErrorCodeWithMessage((int)CEFErrorCodes.MissingSatelliteAssemblyException          , logID, ex10097.Message); }
                case MissingManifestResourceException           ex10098: { return new ErrorCodeWithMessage((int)CEFErrorCodes.MissingManifestResourceException           , logID, ex10098.Message); }
                case TargetParameterCountException              ex10099: { return new ErrorCodeWithMessage((int)CEFErrorCodes.TargetParameterCountException              , logID, ex10099.Message); }
                case TargetInvocationException                  ex10100: { return new ErrorCodeWithMessage((int)CEFErrorCodes.TargetInvocationException                  , logID, ex10100.Message); }
                case TargetException                            ex10101: { return new ErrorCodeWithMessage((int)CEFErrorCodes.TargetException                            , logID, ex10101.Message); }
                case ReflectionTypeLoadException                ex10102: { return new ErrorCodeWithMessage((int)CEFErrorCodes.ReflectionTypeLoadException                , logID, ex10102.Message); }
                case InvalidFilterCriteriaException             ex10103: { return new ErrorCodeWithMessage((int)CEFErrorCodes.InvalidFilterCriteriaException             , logID, ex10103.Message); }
                case CustomAttributeFormatException             ex10104: { return new ErrorCodeWithMessage((int)CEFErrorCodes.CustomAttributeFormatException             , logID, ex10104.Message); }
                case AmbiguousMatchException                    ex10105: { return new ErrorCodeWithMessage((int)CEFErrorCodes.AmbiguousMatchException                    , logID, ex10105.Message); }
                case IsolatedStorageException                   ex10106: { return new ErrorCodeWithMessage((int)CEFErrorCodes.IsolatedStorageException                   , logID, ex10106.Message); }
                case PathTooLongException                       ex10107: { return new ErrorCodeWithMessage((int)CEFErrorCodes.PathTooLongException                       , logID, ex10107.Message); }
                case FileNotFoundException                      ex10108: { return new ErrorCodeWithMessage((int)CEFErrorCodes.FileNotFoundException                      , logID, ex10108.Message); }
                case FileLoadException                          ex10109: { return new ErrorCodeWithMessage((int)CEFErrorCodes.FileLoadException                          , logID, ex10109.Message); }
                case EndOfStreamException                       ex10110: { return new ErrorCodeWithMessage((int)CEFErrorCodes.EndOfStreamException                       , logID, ex10110.Message); }
                case DriveNotFoundException                     ex10111: { return new ErrorCodeWithMessage((int)CEFErrorCodes.DriveNotFoundException                     , logID, ex10111.Message); }
                case DirectoryNotFoundException                 ex10112: { return new ErrorCodeWithMessage((int)CEFErrorCodes.DirectoryNotFoundException                 , logID, ex10112.Message); }
                case IOException                                ex10113: { return new ErrorCodeWithMessage((int)CEFErrorCodes.IOException                                , logID, ex10113.Message); }
                case KeyNotFoundException                       ex10114: { return new ErrorCodeWithMessage((int)CEFErrorCodes.KeyNotFoundException                       , logID, ex10114.Message); }
                case UnauthorizedAccessException                ex10115: { return new ErrorCodeWithMessage((int)CEFErrorCodes.UnauthorizedAccessException                , logID, ex10115.Message); }
                case TypeUnloadedException                      ex10116: { return new ErrorCodeWithMessage((int)CEFErrorCodes.TypeUnloadedException                      , logID, ex10116.Message); }
                case DllNotFoundException                       ex10117: { return new ErrorCodeWithMessage((int)CEFErrorCodes.DllNotFoundException                       , logID, ex10117.Message); }
                case EntryPointNotFoundException                ex10118: { return new ErrorCodeWithMessage((int)CEFErrorCodes.EntryPointNotFoundException                , logID, ex10118.Message); }
                case TypeLoadException                          ex10119: { return new ErrorCodeWithMessage((int)CEFErrorCodes.TypeLoadException                          , logID, ex10119.Message); }
                case TypeInitializationException                ex10120: { return new ErrorCodeWithMessage((int)CEFErrorCodes.TypeInitializationException                , logID, ex10120.Message); }
                case TimeoutException                           ex10121: { return new ErrorCodeWithMessage((int)CEFErrorCodes.TimeoutException                           , logID, ex10121.Message); }
                case StackOverflowException                     ex10122: { return new ErrorCodeWithMessage((int)CEFErrorCodes.StackOverflowException                     , logID, ex10122.Message); }
                case RankException                              ex10123: { return new ErrorCodeWithMessage((int)CEFErrorCodes.RankException                              , logID, ex10123.Message); }
                case PlatformNotSupportedException              ex10124: { return new ErrorCodeWithMessage((int)CEFErrorCodes.PlatformNotSupportedException              , logID, ex10124.Message); }
                case OverflowException                          ex10125: { return new ErrorCodeWithMessage((int)CEFErrorCodes.OverflowException                          , logID, ex10125.Message); }
                case InsufficientMemoryException                ex10126: { return new ErrorCodeWithMessage((int)CEFErrorCodes.InsufficientMemoryException                , logID, ex10126.Message); }
                case OutOfMemoryException                       ex10127: { return new ErrorCodeWithMessage((int)CEFErrorCodes.OutOfMemoryException                       , logID, ex10127.Message); }
                case OperationCanceledException                 ex10128: { return new ErrorCodeWithMessage((int)CEFErrorCodes.OperationCanceledException                 , logID, ex10128.Message); }
                case ObjectDisposedException                    ex10129: { return new ErrorCodeWithMessage((int)CEFErrorCodes.ObjectDisposedException                    , logID, ex10129.Message); }
                case NullReferenceException                     ex10130: { return new ErrorCodeWithMessage((int)CEFErrorCodes.NullReferenceException                     , logID, ex10130.Message); }
                case NotSupportedException                      ex10131: { return new ErrorCodeWithMessage((int)CEFErrorCodes.NotSupportedException                      , logID, ex10131.Message); }
                case NotImplementedException                    ex10132: { return new ErrorCodeWithMessage((int)CEFErrorCodes.NotImplementedException                    , logID, ex10132.Message); }
                case NotFiniteNumberException                   ex10133: { return new ErrorCodeWithMessage((int)CEFErrorCodes.NotFiniteNumberException                   , logID, ex10133.Message); }
                case MulticastNotSupportedException             ex10134: { return new ErrorCodeWithMessage((int)CEFErrorCodes.MulticastNotSupportedException             , logID, ex10134.Message); }
                case MissingMethodException                     ex10135: { return new ErrorCodeWithMessage((int)CEFErrorCodes.MissingMethodException                     , logID, ex10135.Message); }
                case MissingFieldException                      ex10136: { return new ErrorCodeWithMessage((int)CEFErrorCodes.MissingFieldException                      , logID, ex10136.Message); }
                case MissingMemberException                     ex10137: { return new ErrorCodeWithMessage((int)CEFErrorCodes.MissingMemberException                     , logID, ex10137.Message); }
                case MethodAccessException                      ex10138: { return new ErrorCodeWithMessage((int)CEFErrorCodes.MethodAccessException                      , logID, ex10138.Message); }
                case FieldAccessException                       ex10139: { return new ErrorCodeWithMessage((int)CEFErrorCodes.FieldAccessException                       , logID, ex10139.Message); }
                case MemberAccessException                      ex10140: { return new ErrorCodeWithMessage((int)CEFErrorCodes.MemberAccessException                      , logID, ex10140.Message); }
                case InvalidProgramException                    ex10141: { return new ErrorCodeWithMessage((int)CEFErrorCodes.InvalidProgramException                    , logID, ex10141.Message); }
                case InvalidOperationException                  ex10142: { return new ErrorCodeWithMessage((int)CEFErrorCodes.InvalidOperationException                  , logID, ex10142.Message); }
                case InvalidCastException                       ex10143: { return new ErrorCodeWithMessage((int)CEFErrorCodes.InvalidCastException                       , logID, ex10143.Message); }
                case IndexOutOfRangeException                   ex10144: { return new ErrorCodeWithMessage((int)CEFErrorCodes.IndexOutOfRangeException                   , logID, ex10144.Message); }
                case FormatException                            ex10145: { return new ErrorCodeWithMessage((int)CEFErrorCodes.FormatException                            , logID, ex10145.Message); }
                case DuplicateWaitObjectException               ex10146: { return new ErrorCodeWithMessage((int)CEFErrorCodes.DuplicateWaitObjectException               , logID, ex10146.Message); }
                case DivideByZeroException                      ex10147: { return new ErrorCodeWithMessage((int)CEFErrorCodes.DivideByZeroException                      , logID, ex10147.Message); }
                case DataMisalignedException                    ex10148: { return new ErrorCodeWithMessage((int)CEFErrorCodes.DataMisalignedException                    , logID, ex10148.Message); }
                case ContextMarshalException                    ex10149: { return new ErrorCodeWithMessage((int)CEFErrorCodes.ContextMarshalException                    , logID, ex10149.Message); }
                case CannotUnloadAppDomainException             ex10150: { return new ErrorCodeWithMessage((int)CEFErrorCodes.CannotUnloadAppDomainException             , logID, ex10150.Message); }
                case BadImageFormatException                    ex10151: { return new ErrorCodeWithMessage((int)CEFErrorCodes.BadImageFormatException                    , logID, ex10151.Message); }
                case ArrayTypeMismatchException                 ex10152: { return new ErrorCodeWithMessage((int)CEFErrorCodes.ArrayTypeMismatchException                 , logID, ex10152.Message); }
                case ArithmeticException                        ex10153: { return new ErrorCodeWithMessage((int)CEFErrorCodes.ArithmeticException                        , logID, ex10153.Message); }
                case ArgumentOutOfRangeException                ex10154: { return new ErrorCodeWithMessage((int)CEFErrorCodes.ArgumentOutOfRangeException                , logID, ex10154.Message); }
                case ArgumentNullException                      ex10155: { return new ErrorCodeWithMessage((int)CEFErrorCodes.ArgumentNullException                      , logID, ex10155.Message); }
                case ArgumentException                          ex10156: { return new ErrorCodeWithMessage((int)CEFErrorCodes.ArgumentException                          , logID, ex10156.Message); }
                case ApplicationException                       ex10157: { return new ErrorCodeWithMessage((int)CEFErrorCodes.ApplicationException                       , logID, ex10157.Message); }
                case AppDomainUnloadedException                 ex10158: { return new ErrorCodeWithMessage((int)CEFErrorCodes.AppDomainUnloadedException                 , logID, ex10158.Message); }
                case AccessViolationException                   ex10159: { return new ErrorCodeWithMessage((int)CEFErrorCodes.AccessViolationException                   , logID, ex10159.Message); }
                case SystemException                            ex10160: { return new ErrorCodeWithMessage((int)CEFErrorCodes.SystemException                            , logID, ex10160.Message); }
                case                                        { } ex10161: { return new ErrorCodeWithMessage((int)CEFErrorCodes.Exception                                  , logID, ex10161.Message); }
            }
            return ex;
        }

        private static string GetLastMigration()
        {
            var dbMigs = new List<Mig>();
            const string SQL = "SELECT * FROM [dbo].[__MigrationHistory]";
#if NET5_0_OR_GREATER
            using (var con = new SqlConnection(/*ConfigurationManager.ConnectionStrings["ClarityEcommerceEntities"].ConnectionString*/))
#else
            using (var con = new SqlConnection(ConfigurationManager.ConnectionStrings["ClarityEcommerceEntities"].ConnectionString))
#endif
            {
                con.Open();
                using var cmd = new SqlCommand(SQL, con);
                using var dr = cmd.ExecuteReader();
                while (dr.Read())
                {
                    var mig = new Mig
                    {
                        MigrationId = dr!["MigrationId"]!.ToString()!,
                        // ContextKey = dr["ContextKey"].ToString(),
                        // Model = dr["Model"].ToString(),
                        // ProductVersion = dr["ProductVersion"].ToString()
                    };
                    dbMigs.Add(mig);
                }
            }
            ////var assembly = Assembly.GetAssembly(typeof(ClarityEcommerceEntities));
            var assembly = Assembly.LoadFrom("bin\\Clarity.Ecommerce.DataModel.dll");
            var codeMigs = assembly.GetExportedTypes()
                .Where(x => x.BaseType == typeof(DbMigration) && x.IsSealed)
                .Select(x => ((IMigrationMetadata)Activator.CreateInstance(x)!).Id)
                .OrderBy(x => x);
            var lastDbMig = dbMigs.Last().MigrationId;
            var lastCodeMig = codeMigs.Last();
            if (lastDbMig == lastCodeMig)
            {
                return $"A Migration Error has occurred, the last migration is: {lastDbMig} in both the code and the "
                    + "database. Since they are the same, there is likely code modifying the database without a "
                    + "migration added";
            }
            return $"A Migration Error has occurred, the last migration in the DB is: {lastDbMig}, the last one in the"
                + $" code is: {lastCodeMig}";
        }

        private class Mig
        {
            public string MigrationId { get; set; } = null!;
        }
    }
}
