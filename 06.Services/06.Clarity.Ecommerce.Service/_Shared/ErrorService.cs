// <copyright file="ErrorService.cs" company="clarity-ventures.com">
// Copyright (c) 2019-2023 clarity-ventures.com. All rights reserved.
// </copyright>
// <summary>Implements the error service class</summary>
// ReSharper disable BadCommaSpaces, BadIndent, BadParensSpaces, InconsistentNaming, MissingLinebreak, MultipleSpaces
// ReSharper disable StyleCop.SA1001, StyleCop.SA1008, StyleCop.SA1025, StyleCop.SA1402, StyleCop.SA1516
// ReSharper disable NotResolvedInText
#pragma warning disable IDE0060 // Remove unused parameter
#pragma warning disable SA1001 // Commas should be spaced correctly
#pragma warning disable SA1008 // Opening parenthesis should be spaced correctly
#pragma warning disable SA1025 // Code should not contain multiple whitespace in a row
#pragma warning disable SA1134 // Attributes should not share line
#pragma warning disable SA1502 // Element should not be on a single line
#pragma warning disable SA1516 // Elements should be separated by blank line
#pragma warning disable 162
#pragma warning disable format
namespace Clarity.Ecommerce.Service
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel;
    using System.ComponentModel.Design;
    using System.Configuration;
    using System.Configuration.Provider;
    using System.Data;
    using System.Data.Entity.Core;
    using System.Data.SqlTypes;
    using System.IO;
    using System.IO.IsolatedStorage;
    using System.Net;
    using System.Net.Mail;
    using System.Net.NetworkInformation;
    using System.Net.Sockets;
    using System.Reflection;
    using System.Resources;
    using System.Runtime.InteropServices;
    using System.Runtime.Remoting;
    using System.Runtime.Serialization;
    using System.Security;
    using System.Security.AccessControl;
    using System.Security.Authentication;
    using System.Security.Cryptography;
    using System.Security.Policy;
    using System.Security.Principal;
    using System.Text;
    using System.Threading;
    using System.Web;
    using System.Web.Caching;
    using System.Web.Management;
    using System.Web.Security;
    using System.Web.UI;
    using System.Xml;
    using System.Xml.Schema;
    using System.Xml.XPath;
    using System.Xml.Xsl;
    using JetBrains.Annotations;
    using ServiceStack;
    using AuthenticationException = ServiceStack.AuthenticationException;
    using LicenseException = ServiceStack.LicenseException;

    [Route("/Errors/ThrowException"                                 , "POST", Summary = "Intentionally throw an Exception                                 ")] public class ThrowException                                  : IReturnVoid { }
    [Route("/Errors/ThrowAccessViolationException"                  , "POST", Summary = "Intentionally throw an AccessViolationException                  ")] public class ThrowAccessViolationException                   : IReturnVoid { }
    [Route("/Errors/ThrowAppDomainUnloadedException"                , "POST", Summary = "Intentionally throw an AppDomainUnloadedException                ")] public class ThrowAppDomainUnloadedException                 : IReturnVoid { }
    [Route("/Errors/ThrowApplicationException"                      , "POST", Summary = "Intentionally throw an ApplicationException                      ")] public class ThrowApplicationException                       : IReturnVoid { }
    [Route("/Errors/ThrowArgumentException"                         , "POST", Summary = "Intentionally throw an ArgumentException                         ")] public class ThrowArgumentException                          : IReturnVoid { }
    [Route("/Errors/ThrowArgumentNullException"                     , "POST", Summary = "Intentionally throw an ArgumentNullException                     ")] public class ThrowArgumentNullException                      : IReturnVoid { }
    [Route("/Errors/ThrowArgumentOutOfRangeException"               , "POST", Summary = "Intentionally throw an ArgumentOutOfRangeException               ")] public class ThrowArgumentOutOfRangeException                : IReturnVoid { }
    [Route("/Errors/ThrowArithmeticException"                       , "POST", Summary = "Intentionally throw an ArithmeticException                       ")] public class ThrowArithmeticException                        : IReturnVoid { }
    [Route("/Errors/ThrowArrayTypeMismatchException"                , "POST", Summary = "Intentionally throw an ArrayTypeMismatchException                ")] public class ThrowArrayTypeMismatchException                 : IReturnVoid { }
    [Route("/Errors/ThrowBadImageFormatException"                   , "POST", Summary = "Intentionally throw an BadImageFormatException                   ")] public class ThrowBadImageFormatException                    : IReturnVoid { }
    [Route("/Errors/ThrowCannotUnloadAppDomainException"            , "POST", Summary = "Intentionally throw an CannotUnloadAppDomainException            ")] public class ThrowCannotUnloadAppDomainException             : IReturnVoid { }
    [Route("/Errors/ThrowContextMarshalException"                   , "POST", Summary = "Intentionally throw an ContextMarshalException                   ")] public class ThrowContextMarshalException                    : IReturnVoid { }
    [Route("/Errors/ThrowDataMisalignedException"                   , "POST", Summary = "Intentionally throw an DataMisalignedException                   ")] public class ThrowDataMisalignedException                    : IReturnVoid { }
    [Route("/Errors/ThrowDivideByZeroException"                     , "POST", Summary = "Intentionally throw an DivideByZeroException                     ")] public class ThrowDivideByZeroException                      : IReturnVoid { }
    [Route("/Errors/ThrowDllNotFoundException"                      , "POST", Summary = "Intentionally throw an DllNotFoundException                      ")] public class ThrowDllNotFoundException                       : IReturnVoid { }
    [Route("/Errors/ThrowDuplicateWaitObjectException"              , "POST", Summary = "Intentionally throw an DuplicateWaitObjectException              ")] public class ThrowDuplicateWaitObjectException               : IReturnVoid { }
    [Route("/Errors/ThrowEntryPointNotFoundException"               , "POST", Summary = "Intentionally throw an EntryPointNotFoundException               ")] public class ThrowEntryPointNotFoundException                : IReturnVoid { }
    [Route("/Errors/ThrowFieldAccessException"                      , "POST", Summary = "Intentionally throw an FieldAccessException                      ")] public class ThrowFieldAccessException                       : IReturnVoid { }
    [Route("/Errors/ThrowFormatException"                           , "POST", Summary = "Intentionally throw an FormatException                           ")] public class ThrowFormatException                            : IReturnVoid { }
    [Route("/Errors/ThrowIndexOutOfRangeException"                  , "POST", Summary = "Intentionally throw an IndexOutOfRangeException                  ")] public class ThrowIndexOutOfRangeException                   : IReturnVoid { }
    [Route("/Errors/ThrowInsufficientMemoryException"               , "POST", Summary = "Intentionally throw an InsufficientMemoryException               ")] public class ThrowInsufficientMemoryException                : IReturnVoid { }
    [Route("/Errors/ThrowInvalidCastException"                      , "POST", Summary = "Intentionally throw an InvalidCastException                      ")] public class ThrowInvalidCastException                       : IReturnVoid { }
    [Route("/Errors/ThrowInvalidOperationException"                 , "POST", Summary = "Intentionally throw an InvalidOperationException                 ")] public class ThrowInvalidOperationException                  : IReturnVoid { }
    [Route("/Errors/ThrowInvalidProgramException"                   , "POST", Summary = "Intentionally throw an InvalidProgramException                   ")] public class ThrowInvalidProgramException                    : IReturnVoid { }
    [Route("/Errors/ThrowMemberAccessException"                     , "POST", Summary = "Intentionally throw an MemberAccessException                     ")] public class ThrowMemberAccessException                      : IReturnVoid { }
    [Route("/Errors/ThrowMethodAccessException"                     , "POST", Summary = "Intentionally throw an MethodAccessException                     ")] public class ThrowMethodAccessException                      : IReturnVoid { }
    [Route("/Errors/ThrowMissingFieldException"                     , "POST", Summary = "Intentionally throw an MissingFieldException                     ")] public class ThrowMissingFieldException                      : IReturnVoid { }
    [Route("/Errors/ThrowMissingMemberException"                    , "POST", Summary = "Intentionally throw an MissingMemberException                    ")] public class ThrowMissingMemberException                     : IReturnVoid { }
    [Route("/Errors/ThrowMissingMethodException"                    , "POST", Summary = "Intentionally throw an MissingMethodException                    ")] public class ThrowMissingMethodException                     : IReturnVoid { }
    [Route("/Errors/ThrowMulticastNotSupportedException"            , "POST", Summary = "Intentionally throw an MulticastNotSupportedException            ")] public class ThrowMulticastNotSupportedException             : IReturnVoid { }
    [Route("/Errors/ThrowNotFiniteNumberException"                  , "POST", Summary = "Intentionally throw an NotFiniteNumberException                  ")] public class ThrowNotFiniteNumberException                   : IReturnVoid { }
    [Route("/Errors/ThrowNotImplementedException"                   , "POST", Summary = "Intentionally throw an NotImplementedException                   ")] public class ThrowNotImplementedException                    : IReturnVoid { }
    [Route("/Errors/ThrowNotSupportedException"                     , "POST", Summary = "Intentionally throw an NotSupportedException                     ")] public class ThrowNotSupportedException                      : IReturnVoid { }
    [Route("/Errors/ThrowNullReferenceException"                    , "POST", Summary = "Intentionally throw an NullReferenceException                    ")] public class ThrowNullReferenceException                     : IReturnVoid { }
    [Route("/Errors/ThrowObjectDisposedException"                   , "POST", Summary = "Intentionally throw an ObjectDisposedException                   ")] public class ThrowObjectDisposedException                    : IReturnVoid { }
    [Route("/Errors/ThrowOperationCanceledException"                , "POST", Summary = "Intentionally throw an OperationCanceledException                ")] public class ThrowOperationCanceledException                 : IReturnVoid { }
    [Route("/Errors/ThrowOutOfMemoryException"                      , "POST", Summary = "Intentionally throw an OutOfMemoryException                      ")] public class ThrowOutOfMemoryException                       : IReturnVoid { }
    [Route("/Errors/ThrowOverflowException"                         , "POST", Summary = "Intentionally throw an OverflowException                         ")] public class ThrowOverflowException                          : IReturnVoid { }
    [Route("/Errors/ThrowPlatformNotSupportedException"             , "POST", Summary = "Intentionally throw an PlatformNotSupportedException             ")] public class ThrowPlatformNotSupportedException              : IReturnVoid { }
    [Route("/Errors/ThrowRankException"                             , "POST", Summary = "Intentionally throw an RankException                             ")] public class ThrowRankException                              : IReturnVoid { }
    [Route("/Errors/ThrowStackOverflowException"                    , "POST", Summary = "Intentionally throw an StackOverflowException                    ")] public class ThrowStackOverflowException                     : IReturnVoid { }
    [Route("/Errors/ThrowSystemException"                           , "POST", Summary = "Intentionally throw an SystemException                           ")] public class ThrowSystemException                            : IReturnVoid { }
    [Route("/Errors/ThrowTimeoutException"                          , "POST", Summary = "Intentionally throw an TimeoutException                          ")] public class ThrowTimeoutException                           : IReturnVoid { }
    [Route("/Errors/ThrowTypeInitializationException"               , "POST", Summary = "Intentionally throw an TypeInitializationException               ")] public class ThrowTypeInitializationException                : IReturnVoid { }
    [Route("/Errors/ThrowTypeLoadException"                         , "POST", Summary = "Intentionally throw an TypeLoadException                         ")] public class ThrowTypeLoadException                          : IReturnVoid { }
    [Route("/Errors/ThrowTypeUnloadedException"                     , "POST", Summary = "Intentionally throw an TypeUnloadedException                     ")] public class ThrowTypeUnloadedException                      : IReturnVoid { }
    [Route("/Errors/ThrowUnauthorizedAccessException"               , "POST", Summary = "Intentionally throw an UnauthorizedAccessException               ")] public class ThrowUnauthorizedAccessException                : IReturnVoid { }
    [Route("/Errors/ThrowKeyNotFoundException"                      , "POST", Summary = "Intentionally throw an KeyNotFoundException                      ")] public class ThrowKeyNotFoundException                       : IReturnVoid { }
    [Route("/Errors/ThrowDirectoryNotFoundException"                , "POST", Summary = "Intentionally throw an DirectoryNotFoundException                ")] public class ThrowDirectoryNotFoundException                 : IReturnVoid { }
    [Route("/Errors/ThrowDriveNotFoundException"                    , "POST", Summary = "Intentionally throw an DriveNotFoundException                    ")] public class ThrowDriveNotFoundException                     : IReturnVoid { }
    [Route("/Errors/ThrowEndOfStreamException"                      , "POST", Summary = "Intentionally throw an EndOfStreamException                      ")] public class ThrowEndOfStreamException                       : IReturnVoid { }
    [Route("/Errors/ThrowFileLoadException"                         , "POST", Summary = "Intentionally throw an FileLoadException                         ")] public class ThrowFileLoadException                          : IReturnVoid { }
    [Route("/Errors/ThrowFileNotFoundException"                     , "POST", Summary = "Intentionally throw an FileNotFoundException                     ")] public class ThrowFileNotFoundException                      : IReturnVoid { }
    [Route("/Errors/ThrowIOException"                               , "POST", Summary = "Intentionally throw an IOException                               ")] public class ThrowIOException                                : IReturnVoid { }
    [Route("/Errors/ThrowPathTooLongException"                      , "POST", Summary = "Intentionally throw an PathTooLongException                      ")] public class ThrowPathTooLongException                       : IReturnVoid { }
    [Route("/Errors/ThrowIsolatedStorageException"                  , "POST", Summary = "Intentionally throw an IsolatedStorageException                  ")] public class ThrowIsolatedStorageException                   : IReturnVoid { }
    [Route("/Errors/ThrowAmbiguousMatchException"                   , "POST", Summary = "Intentionally throw an AmbiguousMatchException                   ")] public class ThrowAmbiguousMatchException                    : IReturnVoid { }
    [Route("/Errors/ThrowCustomAttributeFormatException"            , "POST", Summary = "Intentionally throw an CustomAttributeFormatException            ")] public class ThrowCustomAttributeFormatException             : IReturnVoid { }
    [Route("/Errors/ThrowInvalidFilterCriteriaException"            , "POST", Summary = "Intentionally throw an InvalidFilterCriteriaException            ")] public class ThrowInvalidFilterCriteriaException             : IReturnVoid { }
    [Route("/Errors/ThrowMetadataException"                         , "POST", Summary = "Intentionally throw an MetadataException                         ")] public class ThrowMetadataException                          : IReturnVoid { }
    [Route("/Errors/ThrowReflectionTypeLoadException"               , "POST", Summary = "Intentionally throw an ReflectionTypeLoadException               ")] public class ThrowReflectionTypeLoadException                : IReturnVoid { }
    [Route("/Errors/ThrowTargetException"                           , "POST", Summary = "Intentionally throw an TargetException                           ")] public class ThrowTargetException                            : IReturnVoid { }
    [Route("/Errors/ThrowTargetInvocationException"                 , "POST", Summary = "Intentionally throw an TargetInvocationException                 ")] public class ThrowTargetInvocationException                  : IReturnVoid { }
    [Route("/Errors/ThrowTargetParameterCountException"             , "POST", Summary = "Intentionally throw an TargetParameterCountException             ")] public class ThrowTargetParameterCountException              : IReturnVoid { }
    [Route("/Errors/ThrowMissingManifestResourceException"          , "POST", Summary = "Intentionally throw an MissingManifestResourceException          ")] public class ThrowMissingManifestResourceException           : IReturnVoid { }
    [Route("/Errors/ThrowMissingSatelliteAssemblyException"         , "POST", Summary = "Intentionally throw an MissingSatelliteAssemblyException         ")] public class ThrowMissingSatelliteAssemblyException          : IReturnVoid { }
    [Route("/Errors/ThrowCOMException"                              , "POST", Summary = "Intentionally throw an COMException                              ")] public class ThrowCOMException                               : IReturnVoid { }
    [Route("/Errors/ThrowExternalException"                         , "POST", Summary = "Intentionally throw an ExternalException                         ")] public class ThrowExternalException                          : IReturnVoid { }
    [Route("/Errors/ThrowInvalidComObjectException"                 , "POST", Summary = "Intentionally throw an InvalidComObjectException                 ")] public class ThrowInvalidComObjectException                  : IReturnVoid { }
    [Route("/Errors/ThrowInvalidOleVariantTypeException"            , "POST", Summary = "Intentionally throw an InvalidOleVariantTypeException            ")] public class ThrowInvalidOleVariantTypeException             : IReturnVoid { }
    [Route("/Errors/ThrowMarshalDirectiveException"                 , "POST", Summary = "Intentionally throw an MarshalDirectiveException                 ")] public class ThrowMarshalDirectiveException                  : IReturnVoid { }
    [Route("/Errors/ThrowSafeArrayRankMismatchException"            , "POST", Summary = "Intentionally throw an SafeArrayRankMismatchException            ")] public class ThrowSafeArrayRankMismatchException             : IReturnVoid { }
    [Route("/Errors/ThrowSafeArrayTypeMismatchException"            , "POST", Summary = "Intentionally throw an SafeArrayTypeMismatchException            ")] public class ThrowSafeArrayTypeMismatchException             : IReturnVoid { }
    [Route("/Errors/ThrowSEHException"                              , "POST", Summary = "Intentionally throw an SEHException                              ")] public class ThrowSEHException                               : IReturnVoid { }
    [Route("/Errors/ThrowRemotingException"                         , "POST", Summary = "Intentionally throw an RemotingException                         ")] public class ThrowRemotingException                          : IReturnVoid { }
    [Route("/Errors/ThrowRemotingTimeoutException"                  , "POST", Summary = "Intentionally throw an RemotingTimeoutException                  ")] public class ThrowRemotingTimeoutException                   : IReturnVoid { }
    [Route("/Errors/ThrowServerException"                           , "POST", Summary = "Intentionally throw an ServerException                           ")] public class ThrowServerException                            : IReturnVoid { }
    [Route("/Errors/ThrowSerializationException"                    , "POST", Summary = "Intentionally throw an SerializationException                    ")] public class ThrowSerializationException                     : IReturnVoid { }
    [Route("/Errors/ThrowHostProtectionException"                   , "POST", Summary = "Intentionally throw an HostProtectionException                   ")] public class ThrowHostProtectionException                    : IReturnVoid { }
    [Route("/Errors/ThrowSecurityException"                         , "POST", Summary = "Intentionally throw an SecurityException                         ")] public class ThrowSecurityException                          : IReturnVoid { }
    [Route("/Errors/ThrowVerificationException"                     , "POST", Summary = "Intentionally throw an VerificationException                     ")] public class ThrowVerificationException                      : IReturnVoid { }
    [Route("/Errors/ThrowXmlSyntaxException"                        , "POST", Summary = "Intentionally throw an XmlSyntaxException                        ")] public class ThrowXmlSyntaxException                         : IReturnVoid { }
    [Route("/Errors/ThrowPrivilegeNotHeldException"                 , "POST", Summary = "Intentionally throw an PrivilegeNotHeldException                 ")] public class ThrowPrivilegeNotHeldException                  : IReturnVoid { }
    [Route("/Errors/ThrowCryptographicException"                    , "POST", Summary = "Intentionally throw an CryptographicException                    ")] public class ThrowCryptographicException                     : IReturnVoid { }
    [Route("/Errors/ThrowCryptographicUnexpectedOperationException" , "POST", Summary = "Intentionally throw an CryptographicUnexpectedOperationException ")] public class ThrowCryptographicUnexpectedOperationException  : IReturnVoid { }
    [Route("/Errors/ThrowPolicyException"                           , "POST", Summary = "Intentionally throw an PolicyException                           ")] public class ThrowPolicyException                            : IReturnVoid { }
    [Route("/Errors/ThrowIdentityNotMappedException"                , "POST", Summary = "Intentionally throw an IdentityNotMappedException                ")] public class ThrowIdentityNotMappedException                 : IReturnVoid { }
    [Route("/Errors/ThrowDecoderFallbackException"                  , "POST", Summary = "Intentionally throw an DecoderFallbackException                  ")] public class ThrowDecoderFallbackException                   : IReturnVoid { }
    [Route("/Errors/ThrowEncoderFallbackException"                  , "POST", Summary = "Intentionally throw an EncoderFallbackException                  ")] public class ThrowEncoderFallbackException                   : IReturnVoid { }
    [Route("/Errors/ThrowAbandonedMutexException"                   , "POST", Summary = "Intentionally throw an AbandonedMutexException                   ")] public class ThrowAbandonedMutexException                    : IReturnVoid { }
    [Route("/Errors/ThrowSynchronizationLockException"              , "POST", Summary = "Intentionally throw an SynchronizationLockException              ")] public class ThrowSynchronizationLockException               : IReturnVoid { }
    [Route("/Errors/ThrowThreadInterruptedException"                , "POST", Summary = "Intentionally throw an ThreadInterruptedException                ")] public class ThrowThreadInterruptedException                 : IReturnVoid { }
    [Route("/Errors/ThrowThreadStateException"                      , "POST", Summary = "Intentionally throw an ThreadStateException                      ")] public class ThrowThreadStateException                       : IReturnVoid { }
    [Route("/Errors/ThrowWaitHandleCannotBeOpenedException"         , "POST", Summary = "Intentionally throw an WaitHandleCannotBeOpenedException         ")] public class ThrowWaitHandleCannotBeOpenedException          : IReturnVoid { }
    [Route("/Errors/ThrowConfigurationErrorsException"              , "POST", Summary = "Intentionally throw an ConfigurationErrorsException              ")] public class ThrowConfigurationErrorsException               : IReturnVoid { }
    [Route("/Errors/ThrowProviderException"                         , "POST", Summary = "Intentionally throw an ProviderException                         ")] public class ThrowProviderException                          : IReturnVoid { }
    [Route("/Errors/ThrowConstraintException"                       , "POST", Summary = "Intentionally throw an ConstraintException                       ")] public class ThrowConstraintException                        : IReturnVoid { }
    [Route("/Errors/ThrowDataException"                             , "POST", Summary = "Intentionally throw an DataException                             ")] public class ThrowDataException                              : IReturnVoid { }
    [Route("/Errors/ThrowDBConcurrencyException"                    , "POST", Summary = "Intentionally throw an DBConcurrencyException                    ")] public class ThrowDBConcurrencyException                     : IReturnVoid { }
    [Route("/Errors/ThrowDeletedRowInaccessibleException"           , "POST", Summary = "Intentionally throw an DeletedRowInaccessibleException           ")] public class ThrowDeletedRowInaccessibleException            : IReturnVoid { }
    [Route("/Errors/ThrowDuplicateNameException"                    , "POST", Summary = "Intentionally throw an DuplicateNameException                    ")] public class ThrowDuplicateNameException                     : IReturnVoid { }
    [Route("/Errors/ThrowEvaluateException"                         , "POST", Summary = "Intentionally throw an EvaluateException                         ")] public class ThrowEvaluateException                          : IReturnVoid { }
    [Route("/Errors/ThrowInRowChangingEventException"               , "POST", Summary = "Intentionally throw an InRowChangingEventException               ")] public class ThrowInRowChangingEventException                : IReturnVoid { }
    [Route("/Errors/ThrowInvalidConstraintException"                , "POST", Summary = "Intentionally throw an InvalidConstraintException                ")] public class ThrowInvalidConstraintException                 : IReturnVoid { }
    [Route("/Errors/ThrowInvalidExpressionException"                , "POST", Summary = "Intentionally throw an InvalidExpressionException                ")] public class ThrowInvalidExpressionException                 : IReturnVoid { }
    [Route("/Errors/ThrowMissingPrimaryKeyException"                , "POST", Summary = "Intentionally throw an MissingPrimaryKeyException                ")] public class ThrowMissingPrimaryKeyException                 : IReturnVoid { }
    [Route("/Errors/ThrowNoNullAllowedException"                    , "POST", Summary = "Intentionally throw an NoNullAllowedException                    ")] public class ThrowNoNullAllowedException                     : IReturnVoid { }
    [Route("/Errors/ThrowReadOnlyException"                         , "POST", Summary = "Intentionally throw an ReadOnlyException                         ")] public class ThrowReadOnlyException                          : IReturnVoid { }
    [Route("/Errors/ThrowRowNotInTableException"                    , "POST", Summary = "Intentionally throw an RowNotInTableException                    ")] public class ThrowRowNotInTableException                     : IReturnVoid { }
    [Route("/Errors/ThrowStrongTypingException"                     , "POST", Summary = "Intentionally throw an StrongTypingException                     ")] public class ThrowStrongTypingException                      : IReturnVoid { }
    [Route("/Errors/ThrowSyntaxErrorException"                      , "POST", Summary = "Intentionally throw an SyntaxErrorException                      ")] public class ThrowSyntaxErrorException                       : IReturnVoid { }
    [Route("/Errors/ThrowTypedDataSetGeneratorException"            , "POST", Summary = "Intentionally throw an TypedDataSetGeneratorException            ")] public class ThrowTypedDataSetGeneratorException             : IReturnVoid { }
    [Route("/Errors/ThrowVersionNotFoundException"                  , "POST", Summary = "Intentionally throw an VersionNotFoundException                  ")] public class ThrowVersionNotFoundException                   : IReturnVoid { }
    [Route("/Errors/ThrowSqlAlreadyFilledException"                 , "POST", Summary = "Intentionally throw an SqlAlreadyFilledException                 ")] public class ThrowSqlAlreadyFilledException                  : IReturnVoid { }
    [Route("/Errors/ThrowSqlNotFilledException"                     , "POST", Summary = "Intentionally throw an SqlNotFilledException                     ")] public class ThrowSqlNotFilledException                      : IReturnVoid { }
    [Route("/Errors/ThrowSqlNullValueException"                     , "POST", Summary = "Intentionally throw an SqlNullValueException                     ")] public class ThrowSqlNullValueException                      : IReturnVoid { }
    [Route("/Errors/ThrowSqlTruncateException"                      , "POST", Summary = "Intentionally throw an SqlTruncateException                      ")] public class ThrowSqlTruncateException                       : IReturnVoid { }
    [Route("/Errors/ThrowSqlTypeException"                          , "POST", Summary = "Intentionally throw an SqlTypeException                          ")] public class ThrowSqlTypeException                           : IReturnVoid { }
    [Route("/Errors/ThrowHttpCompileException"                      , "POST", Summary = "Intentionally throw an HttpCompileException                      ")] public class ThrowHttpCompileException                       : IReturnVoid { }
    [Route("/Errors/ThrowHttpException"                             , "POST", Summary = "Intentionally throw an HttpException                             ")] public class ThrowHttpException                              : IReturnVoid { }
    [Route("/Errors/ThrowHttpParseException"                        , "POST", Summary = "Intentionally throw an HttpParseException                        ")] public class ThrowHttpParseException                         : IReturnVoid { }
    [Route("/Errors/ThrowHttpRequestValidationException"            , "POST", Summary = "Intentionally throw an HttpRequestValidationException            ")] public class ThrowHttpRequestValidationException             : IReturnVoid { }
    [Route("/Errors/ThrowHttpUnhandledException"                    , "POST", Summary = "Intentionally throw an HttpUnhandledException                    ")] public class ThrowHttpUnhandledException                     : IReturnVoid { }
    [Route("/Errors/ThrowDatabaseNotEnabledForNotificationException", "POST", Summary = "Intentionally throw an DatabaseNotEnabledForNotificationException")] public class ThrowDatabaseNotEnabledForNotificationException : IReturnVoid { }
    [Route("/Errors/ThrowTableNotEnabledForNotificationException"   , "POST", Summary = "Intentionally throw an TableNotEnabledForNotificationException   ")] public class ThrowTableNotEnabledForNotificationException    : IReturnVoid { }
    [Route("/Errors/ThrowSqlExecutionException"                     , "POST", Summary = "Intentionally throw an SqlExecutionException                     ")] public class ThrowSqlExecutionException                      : IReturnVoid { }
    [Route("/Errors/ThrowMembershipCreateUserException"             , "POST", Summary = "Intentionally throw an MembershipCreateUserException             ")] public class ThrowMembershipCreateUserException              : IReturnVoid { }
    [Route("/Errors/ThrowMembershipPasswordException"               , "POST", Summary = "Intentionally throw an MembershipPasswordException               ")] public class ThrowMembershipPasswordException                : IReturnVoid { }
    [Route("/Errors/ThrowViewStateException"                        , "POST", Summary = "Intentionally throw an ViewStateException                        ")] public class ThrowViewStateException                         : IReturnVoid { }
    [Route("/Errors/ThrowXmlException"                              , "POST", Summary = "Intentionally throw an XmlException                              ")] public class ThrowXmlException                               : IReturnVoid { }
    [Route("/Errors/ThrowXmlSchemaException"                        , "POST", Summary = "Intentionally throw an XmlSchemaException                        ")] public class ThrowXmlSchemaException                         : IReturnVoid { }
    [Route("/Errors/ThrowXmlSchemaInferenceException"               , "POST", Summary = "Intentionally throw an XmlSchemaInferenceException               ")] public class ThrowXmlSchemaInferenceException                : IReturnVoid { }
    [Route("/Errors/ThrowXmlSchemaValidationException"              , "POST", Summary = "Intentionally throw an XmlSchemaValidationException              ")] public class ThrowXmlSchemaValidationException               : IReturnVoid { }
    [Route("/Errors/ThrowXPathException"                            , "POST", Summary = "Intentionally throw an XPathException                            ")] public class ThrowXPathException                             : IReturnVoid { }
    [Route("/Errors/ThrowXsltCompileException"                      , "POST", Summary = "Intentionally throw an XsltCompileException                      ")] public class ThrowXsltCompileException                       : IReturnVoid { }
    [Route("/Errors/ThrowXsltException"                             , "POST", Summary = "Intentionally throw an XsltException                             ")] public class ThrowXsltException                              : IReturnVoid { }
    [Route("/Errors/ThrowUriFormatException"                        , "POST", Summary = "Intentionally throw an UriFormatException                        ")] public class ThrowUriFormatException                         : IReturnVoid { }
    [Route("/Errors/ThrowInvalidAsynchronousStateException"         , "POST", Summary = "Intentionally throw an InvalidAsynchronousStateException         ")] public class ThrowInvalidAsynchronousStateException          : IReturnVoid { }
    [Route("/Errors/ThrowInvalidEnumArgumentException"              , "POST", Summary = "Intentionally throw an InvalidEnumArgumentException              ")] public class ThrowInvalidEnumArgumentException               : IReturnVoid { }
    [Route("/Errors/ThrowLicenseException"                          , "POST", Summary = "Intentionally throw an LicenseException                          ")] public class ThrowLicenseException                           : IReturnVoid { }
    [Route("/Errors/ThrowWarningException"                          , "POST", Summary = "Intentionally throw an WarningException                          ")] public class ThrowWarningException                           : IReturnVoid { }
    [Route("/Errors/ThrowWin32Exception                            ", "POST", Summary = "Intentionally throw an Win32Exception                            ")] public class ThrowWin32Exception                             : IReturnVoid { }
    [Route("/Errors/ThrowCheckoutException"                         , "POST", Summary = "Intentionally throw an CheckoutException                         ")] public class ThrowCheckoutException                          : IReturnVoid { }
    [Route("/Errors/ThrowConfigurationException"                    , "POST", Summary = "Intentionally throw an ConfigurationException                    ")] public class ThrowConfigurationException                     : IReturnVoid { }
    [Route("/Errors/ThrowSettingsPropertyIsReadOnlyException"       , "POST", Summary = "Intentionally throw an SettingsPropertyIsReadOnlyException       ")] public class ThrowSettingsPropertyIsReadOnlyException        : IReturnVoid { }
    [Route("/Errors/ThrowSettingsPropertyNotFoundException"         , "POST", Summary = "Intentionally throw an SettingsPropertyNotFoundException         ")] public class ThrowSettingsPropertyNotFoundException          : IReturnVoid { }
    [Route("/Errors/ThrowSettingsPropertyWrongTypeException"        , "POST", Summary = "Intentionally throw an SettingsPropertyWrongTypeException        ")] public class ThrowSettingsPropertyWrongTypeException         : IReturnVoid { }
    [Route("/Errors/ThrowInternalBufferOverflowException"           , "POST", Summary = "Intentionally throw an InternalBufferOverflowException           ")] public class ThrowInternalBufferOverflowException            : IReturnVoid { }
    [Route("/Errors/ThrowInvalidDataException"                      , "POST", Summary = "Intentionally throw an InvalidDataException                      ")] public class ThrowInvalidDataException                       : IReturnVoid { }
    [Route("/Errors/ThrowCookieException"                           , "POST", Summary = "Intentionally throw an CookieException                           ")] public class ThrowCookieException                            : IReturnVoid { }
    [Route("/Errors/ThrowHttpListenerException"                     , "POST", Summary = "Intentionally throw an HttpListenerException                     ")] public class ThrowHttpListenerException                      : IReturnVoid { }
    [Route("/Errors/ThrowProtocolViolationException"                , "POST", Summary = "Intentionally throw an ProtocolViolationException                ")] public class ThrowProtocolViolationException                 : IReturnVoid { }
    [Route("/Errors/ThrowWebException"                              , "POST", Summary = "Intentionally throw an WebException                              ")] public class ThrowWebException                               : IReturnVoid { }
    [Route("/Errors/ThrowSmtpException"                             , "POST", Summary = "Intentionally throw an SmtpException                             ")] public class ThrowSmtpException                              : IReturnVoid { }
    [Route("/Errors/ThrowSmtpFailedRecipientException"              , "POST", Summary = "Intentionally throw an SmtpFailedRecipientException              ")] public class ThrowSmtpFailedRecipientException               : IReturnVoid { }
    [Route("/Errors/ThrowSmtpFailedRecipientsException"             , "POST", Summary = "Intentionally throw an SmtpFailedRecipientsException             ")] public class ThrowSmtpFailedRecipientsException              : IReturnVoid { }
    [Route("/Errors/ThrowNetworkInformationException"               , "POST", Summary = "Intentionally throw an NetworkInformationException               ")] public class ThrowNetworkInformationException                : IReturnVoid { }
    [Route("/Errors/ThrowPingException"                             , "POST", Summary = "Intentionally throw an PingException                             ")] public class ThrowPingException                              : IReturnVoid { }
    [Route("/Errors/ThrowSocketException"                           , "POST", Summary = "Intentionally throw an SocketException                           ")] public class ThrowSocketException                            : IReturnVoid { }
    [Route("/Errors/ThrowAuthenticationException"                   , "POST", Summary = "Intentionally throw an AuthenticationException                   ")] public class ThrowAuthenticationException                    : IReturnVoid { }
    [Route("/Errors/ThrowSemaphoreFullException"                    , "POST", Summary = "Intentionally throw an SemaphoreFullException                    ")] public class ThrowSemaphoreFullException                     : IReturnVoid { }
    [Route("/Errors/ThrowInvalidCredentialException"                , "POST", Summary = "Intentionally throw an InvalidCredentialException                ")] public class ThrowInvalidCredentialException                 : IReturnVoid { }
    [PublicAPI]
    public class ErrorService : ClarityEcommerceServiceBase
    {
        public void Post(ThrowException                                  request) { throw new("Test");             }
        public void Post(ThrowAccessViolationException                   request) { throw new AccessViolationException                  ("Test");             }
        public void Post(ThrowAppDomainUnloadedException                 request) { throw new AppDomainUnloadedException                ("Test");             }
        public void Post(ThrowApplicationException                       request) { throw new ApplicationException                      ("Test");             }
        public void Post(ThrowArgumentException                          request) { throw new ArgumentException                         ("Test");             }
        public void Post(ThrowArgumentNullException                      request) { throw new ArgumentNullException                     ("Test");             }
        public void Post(ThrowArgumentOutOfRangeException                request) { throw new ArgumentOutOfRangeException               ("Test");             }
        public void Post(ThrowArithmeticException                        request) { throw new ArithmeticException                       ("Test");             }
        public void Post(ThrowArrayTypeMismatchException                 request) { throw new ArrayTypeMismatchException                ("Test");             }
        public void Post(ThrowBadImageFormatException                    request) { throw new BadImageFormatException                   ("Test");             }
        public void Post(ThrowCannotUnloadAppDomainException             request) { throw new CannotUnloadAppDomainException            ("Test");             }
        public void Post(ThrowContextMarshalException                    request) { throw new ContextMarshalException                   ("Test");             }
        public void Post(ThrowDataMisalignedException                    request) { throw new DataMisalignedException                   ("Test");             }
        public void Post(ThrowDivideByZeroException                      request) { throw new DivideByZeroException                     ("Test");             }
        public void Post(ThrowDllNotFoundException                       request) { throw new DllNotFoundException                      ("Test");             }
        public void Post(ThrowDuplicateWaitObjectException               request) { throw new DuplicateWaitObjectException              ("Test");             }
        public void Post(ThrowEntryPointNotFoundException                request) { throw new EntryPointNotFoundException               ("Test");             }
        public void Post(ThrowFieldAccessException                       request) { throw new FieldAccessException                      ("Test");             }
        public void Post(ThrowFormatException                            request) { throw new FormatException                           ("Test");             }
        public void Post(ThrowIndexOutOfRangeException                   request) { throw new IndexOutOfRangeException                  ("Test");             }
        public void Post(ThrowInsufficientMemoryException                request) { throw new InsufficientMemoryException               ("Test");             }
        public void Post(ThrowInvalidCastException                       request) { throw new InvalidCastException                      ("Test");             }
        public void Post(ThrowInvalidOperationException                  request) { throw new InvalidOperationException                 ("Test");             }
        public void Post(ThrowInvalidProgramException                    request) { throw new InvalidProgramException                   ("Test");             }
        public void Post(ThrowMemberAccessException                      request) { throw new MemberAccessException                     ("Test");             }
        public void Post(ThrowMethodAccessException                      request) { throw new MethodAccessException                     ("Test");             }
        public void Post(ThrowMissingFieldException                      request) { throw new MissingFieldException                     ("Test");             }
        public void Post(ThrowMissingMemberException                     request) { throw new MissingMemberException                    ("Test");             }
        public void Post(ThrowMissingMethodException                     request) { throw new MissingMethodException                    ("Test");             }
        public void Post(ThrowMulticastNotSupportedException             request) { throw new MulticastNotSupportedException            ("Test");             }
        public void Post(ThrowNotFiniteNumberException                   request) { throw new NotFiniteNumberException                  ("Test");             }
        public void Post(ThrowNotImplementedException                    request) { throw new NotImplementedException                   ("Test");             }
        public void Post(ThrowNotSupportedException                      request) { throw new NotSupportedException                     ("Test");             }
        public void Post(ThrowNullReferenceException                     request) { throw new NullReferenceException                    ("Test");             }
        public void Post(ThrowObjectDisposedException                    request) { throw new ObjectDisposedException                   ("Test");             }
        public void Post(ThrowOperationCanceledException                 request) { throw new OperationCanceledException                ("Test");             }
        public void Post(ThrowOutOfMemoryException                       request) { throw new OutOfMemoryException                      ("Test");             }
        public void Post(ThrowOverflowException                          request) { throw new OverflowException                         ("Test");             }
        public void Post(ThrowPlatformNotSupportedException              request) { throw new PlatformNotSupportedException             ("Test");             }
        public void Post(ThrowRankException                              request) { throw new RankException                             ("Test");             }
        public void Post(ThrowStackOverflowException                     request) { throw new StackOverflowException                    ("Test");             }
        public void Post(ThrowSystemException                            request) { throw new SystemException                           ("Test");             }
        public void Post(ThrowTimeoutException                           request) { throw new TimeoutException                          ("Test");             }
        public void Post(ThrowTypeInitializationException                request) { throw new TypeInitializationException               ("Test", null);       }
        public void Post(ThrowTypeLoadException                          request) { throw new TypeLoadException                         ("Test");             }
        public void Post(ThrowTypeUnloadedException                      request) { throw new TypeUnloadedException                     ("Test");             }
        public void Post(ThrowUnauthorizedAccessException                request) { throw new UnauthorizedAccessException               ("Test");             }
        public void Post(ThrowKeyNotFoundException                       request) { throw new KeyNotFoundException                      ("Test");             }
        public void Post(ThrowDirectoryNotFoundException                 request) { throw new DirectoryNotFoundException                ("Test");             }
        public void Post(ThrowDriveNotFoundException                     request) { throw new DriveNotFoundException                    ("Test");             }
        public void Post(ThrowEndOfStreamException                       request) { throw new EndOfStreamException                      ("Test");             }
        public void Post(ThrowFileLoadException                          request) { throw new FileLoadException                         ("Test");             }
        public void Post(ThrowFileNotFoundException                      request) { throw new FileNotFoundException                     ("Test");             }
        public void Post(ThrowIOException                                request) { throw new IOException                               ("Test");             }
        public void Post(ThrowPathTooLongException                       request) { throw new PathTooLongException                      ("Test");             }
        public void Post(ThrowIsolatedStorageException                   request) { throw new IsolatedStorageException                  ("Test");             }
        public void Post(ThrowAmbiguousMatchException                    request) { throw new AmbiguousMatchException                   ("Test");             }
        public void Post(ThrowCustomAttributeFormatException             request) { throw new CustomAttributeFormatException            ("Test");             }
        public void Post(ThrowInvalidFilterCriteriaException             request) { throw new InvalidFilterCriteriaException            ("Test");             }
        public void Post(ThrowMetadataException                          request) { throw new MetadataException                         ("Test");             }
        public void Post(ThrowReflectionTypeLoadException                request) { throw new ReflectionTypeLoadException               (null, null, "Test"); }
        public void Post(ThrowTargetException                            request) { throw new TargetException                           ("Test");             }
        public void Post(ThrowTargetInvocationException                  request) { throw new TargetInvocationException                 ("Test", null);       }
        public void Post(ThrowTargetParameterCountException              request) { throw new TargetParameterCountException             ("Test");             }
        public void Post(ThrowMissingManifestResourceException           request) { throw new MissingManifestResourceException          ("Test");             }
        public void Post(ThrowMissingSatelliteAssemblyException          request) { throw new MissingSatelliteAssemblyException         ("Test");             }
        public void Post(ThrowCOMException                               request) { throw new COMException                              ("Test");             }
        public void Post(ThrowExternalException                          request) { throw new ExternalException                         ("Test");             }
        public void Post(ThrowInvalidComObjectException                  request) { throw new InvalidComObjectException                 ("Test");             }
        public void Post(ThrowInvalidOleVariantTypeException             request) { throw new InvalidOleVariantTypeException            ("Test");             }
        public void Post(ThrowMarshalDirectiveException                  request) { throw new MarshalDirectiveException                 ("Test");             }
        public void Post(ThrowSafeArrayRankMismatchException             request) { throw new SafeArrayRankMismatchException            ("Test");             }
        public void Post(ThrowSafeArrayTypeMismatchException             request) { throw new SafeArrayTypeMismatchException            ("Test");             }
        public void Post(ThrowSEHException                               request) { throw new SEHException                              ("Test");             }
        public void Post(ThrowRemotingException                          request) { throw new RemotingException                         ("Test");             }
        public void Post(ThrowRemotingTimeoutException                   request) { throw new RemotingTimeoutException                  ("Test");             }
        public void Post(ThrowServerException                            request) { throw new ServerException                           ("Test");             }
        public void Post(ThrowSerializationException                     request) { throw new SerializationException                    ("Test");             }
        public void Post(ThrowHostProtectionException                    request) { throw new HostProtectionException                   ("Test");             }
        public void Post(ThrowSecurityException                          request) { throw new SecurityException                         ("Test");             }
        public void Post(ThrowVerificationException                      request) { throw new VerificationException                     ("Test");             }
        public void Post(ThrowXmlSyntaxException                         request) { throw new XmlSyntaxException                        ("Test");             }
        public void Post(ThrowPrivilegeNotHeldException                  request) { throw new PrivilegeNotHeldException                 ("Test");             }
        public void Post(ThrowCryptographicException                     request) { throw new CryptographicException                    ("Test");             }
        public void Post(ThrowCryptographicUnexpectedOperationException  request) { throw new CryptographicUnexpectedOperationException ("Test");             }
        public void Post(ThrowPolicyException                            request) { throw new PolicyException                           ("Test");             }
        public void Post(ThrowIdentityNotMappedException                 request) { throw new IdentityNotMappedException                ("Test");             }
        public void Post(ThrowDecoderFallbackException                   request) { throw new DecoderFallbackException                  ("Test");             }
        public void Post(ThrowEncoderFallbackException                   request) { throw new EncoderFallbackException                  ("Test");             }
        public void Post(ThrowAbandonedMutexException                    request) { throw new AbandonedMutexException                   ("Test");             }
        public void Post(ThrowSynchronizationLockException               request) { throw new SynchronizationLockException              ("Test");             }
        public void Post(ThrowThreadInterruptedException                 request) { throw new ThreadInterruptedException                ("Test");             }
        public void Post(ThrowThreadStateException                       request) { throw new ThreadStateException                      ("Test");             }
        public void Post(ThrowWaitHandleCannotBeOpenedException          request) { throw new WaitHandleCannotBeOpenedException         ("Test");             }
        public void Post(ThrowConfigurationErrorsException               request) { throw new ConfigurationErrorsException              ("Test");             }
        public void Post(ThrowProviderException                          request) { throw new ProviderException                         ("Test");             }
        public void Post(ThrowConstraintException                        request) { throw new ConstraintException                       ("Test");             }
        public void Post(ThrowDataException                              request) { throw new DataException                             ("Test");             }
        public void Post(ThrowDBConcurrencyException                     request) { throw new DBConcurrencyException                    ("Test");             }
        public void Post(ThrowDeletedRowInaccessibleException            request) { throw new DeletedRowInaccessibleException           ("Test");             }
        public void Post(ThrowDuplicateNameException                     request) { throw new DuplicateNameException                    ("Test");             }
        public void Post(ThrowEvaluateException                          request) { throw new EvaluateException                         ("Test");             }
        public void Post(ThrowInRowChangingEventException                request) { throw new InRowChangingEventException               ("Test");             }
        public void Post(ThrowInvalidConstraintException                 request) { throw new InvalidConstraintException                ("Test");             }
        public void Post(ThrowInvalidExpressionException                 request) { throw new InvalidExpressionException                ("Test");             }
        public void Post(ThrowMissingPrimaryKeyException                 request) { throw new MissingPrimaryKeyException                ("Test");             }
        public void Post(ThrowNoNullAllowedException                     request) { throw new NoNullAllowedException                    ("Test");             }
        public void Post(ThrowReadOnlyException                          request) { throw new ReadOnlyException                         ("Test");             }
        public void Post(ThrowRowNotInTableException                     request) { throw new RowNotInTableException                    ("Test");             }
        public void Post(ThrowStrongTypingException                      request) { throw new StrongTypingException                     ("Test");             }
        public void Post(ThrowSyntaxErrorException                       request) { throw new SyntaxErrorException                      ("Test");             }
        public void Post(ThrowTypedDataSetGeneratorException             request) { throw new TypedDataSetGeneratorException            ("Test");             }
        public void Post(ThrowVersionNotFoundException                   request) { throw new VersionNotFoundException                  ("Test");             }
        public void Post(ThrowSqlAlreadyFilledException                  request) { throw new SqlAlreadyFilledException                 ("Test");             }
        public void Post(ThrowSqlNotFilledException                      request) { throw new SqlNotFilledException                     ("Test");             }
        public void Post(ThrowSqlNullValueException                      request) { throw new SqlNullValueException                     ("Test");             }
        public void Post(ThrowSqlTruncateException                       request) { throw new SqlTruncateException                      ("Test");             }
        public void Post(ThrowSqlTypeException                           request) { throw new SqlTypeException                          ("Test");             }
        public void Post(ThrowHttpCompileException                       request) { throw new HttpCompileException                      ("Test");             }
        public void Post(ThrowHttpException                              request) { throw new HttpException                             ("Test");             }
        public void Post(ThrowHttpParseException                         request) { throw new HttpParseException                        ("Test");             }
        public void Post(ThrowHttpRequestValidationException             request) { throw new HttpRequestValidationException            ("Test");             }
        public void Post(ThrowHttpUnhandledException                     request) { throw new HttpUnhandledException                    ("Test");             }
        public void Post(ThrowDatabaseNotEnabledForNotificationException request) { throw new DatabaseNotEnabledForNotificationException("Test");             }
        public void Post(ThrowTableNotEnabledForNotificationException    request) { throw new TableNotEnabledForNotificationException   ("Test");             }
        public void Post(ThrowSqlExecutionException                      request) { throw new SqlExecutionException                     ("Test");             }
        public void Post(ThrowMembershipCreateUserException              request) { throw new MembershipCreateUserException             ("Test");             }
        public void Post(ThrowMembershipPasswordException                request) { throw new MembershipPasswordException               ("Test");             }
        public void Post(ThrowViewStateException                         request) { throw new ViewStateException                        ();                   }
        public void Post(ThrowXmlException                               request) { throw new XmlException                              ("Test");             }
        public void Post(ThrowXmlSchemaException                         request) { throw new XmlSchemaException                        ("Test");             }
        public void Post(ThrowXmlSchemaInferenceException                request) { throw new XmlSchemaInferenceException               ("Test");             }
        public void Post(ThrowXmlSchemaValidationException               request) { throw new XmlSchemaValidationException              ("Test");             }
        public void Post(ThrowXPathException                             request) { throw new XPathException                            ("Test");             }
        public void Post(ThrowXsltCompileException                       request) { throw new XsltCompileException                      ("Test");             }
        public void Post(ThrowXsltException                              request) { throw new XsltException                             ("Test");             }
        public void Post(ThrowUriFormatException                         request) { throw new UriFormatException                        ("Test");             }
        public void Post(ThrowInvalidAsynchronousStateException          request) { throw new InvalidAsynchronousStateException         ("Test");             }
        public void Post(ThrowInvalidEnumArgumentException               request) { throw new InvalidEnumArgumentException              ("Test");             }
        public void Post(ThrowLicenseException                           request) { throw new LicenseException                          ("Test");             }
        public void Post(ThrowWarningException                           request) { throw new WarningException                          ("Test");             }
        public void Post(ThrowWin32Exception                             request) { throw new Win32Exception                            ("Test");             }
        public void Post(ThrowCheckoutException                          request) { throw new CheckoutException                         ("Test");             }
#pragma warning disable CS0618 // Type or member is obsolete
        public void Post(ThrowConfigurationException                     request) { throw new ConfigurationException                    ("Test");             }
#pragma warning restore CS0618 // Type or member is obsolete
        public void Post(ThrowSettingsPropertyIsReadOnlyException        request) { throw new SettingsPropertyIsReadOnlyException       ("Test");             }
        public void Post(ThrowSettingsPropertyNotFoundException          request) { throw new SettingsPropertyNotFoundException         ("Test");             }
        public void Post(ThrowSettingsPropertyWrongTypeException         request) { throw new SettingsPropertyWrongTypeException        ("Test");             }
        public void Post(ThrowInternalBufferOverflowException            request) { throw new InternalBufferOverflowException           ("Test");             }
        public void Post(ThrowInvalidDataException                       request) { throw new InvalidDataException                      ("Test");             }
        public void Post(ThrowCookieException                            request) { throw new CookieException                           ();                   }
        public void Post(ThrowHttpListenerException                      request) { throw new HttpListenerException                     (0, "Test");          }
        public void Post(ThrowProtocolViolationException                 request) { throw new ProtocolViolationException                ("Test");             }
        public void Post(ThrowWebException                               request) { throw new WebException                              ("Test");             }
        public void Post(ThrowSmtpException                              request) { throw new SmtpException                             ("Test");             }
        public void Post(ThrowSmtpFailedRecipientException               request) { throw new SmtpFailedRecipientException              ("Test");             }
        public void Post(ThrowSmtpFailedRecipientsException              request) { throw new SmtpFailedRecipientsException             ("Test");             }
        public void Post(ThrowNetworkInformationException                request) { throw new NetworkInformationException               (0);                  }
        public void Post(ThrowPingException                              request) { throw new PingException                             ("Test");             }
        public void Post(ThrowSocketException                            request) { throw new SocketException                           (0);                  }
        public void Post(ThrowAuthenticationException                    request) { throw new AuthenticationException                   ("Test");             }
        public void Post(ThrowSemaphoreFullException                     request) { throw new SemaphoreFullException                    ("Test");             }
        public void Post(ThrowInvalidCredentialException                 request) { throw new InvalidCredentialException                ("Test");             }
    }
}
