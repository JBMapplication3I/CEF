using System;
using System.Collections.Generic;

namespace ServiceStack.Templates
{
    public static class TemplateConfig
    {
        public static HashSet<string> RemoveNewLineAfterFiltersNamed { get; set; } = new()
        {
            "assignTo",
            "assignError",
            "addTo",
            "addToStart",
            "appendTo",
            "prependTo",
            "do",
            "end",
            "throw",
            "ifthrow",
            "throwIf",
            "throwIf",
            "ifThrowArgumentException",
            "ifThrowArgumentNullException",
            "throwArgumentNullExceptionIf",
            "throwArgumentException",
            "throwArgumentNullException",
            "throwNotSupportedException",
            "throwNotImplementedException",
            "throwUnauthorizedAccessException",
            "throwFileNotFoundException",
            "throwOptimisticConcurrencyException",
            "throwNotSupportedException",
            "ifError",
            "ifErrorFmt",
            "skipExecutingFiltersOnError",
            "continueExecutingFiltersOnError",
            "publishToGateway",
        };
        
        public static HashSet<string> OnlyEvaluateFiltersWhenSkippingPageFilterExecution { get; set; } = new()
        {
            "ifError",
            "lastError",
            "htmlError",
            "htmlErrorMessage",
            "htmlErrorDebug",
        };
        
        /// <summary>
        /// Rethrow fatal exceptions thrown on incorrect API usage    
        /// </summary>
        public static HashSet<Type> FatalExceptions { get; set; } = new()
        {
            typeof(NotSupportedException),
            typeof(System.Reflection.TargetInvocationException),
            typeof(NotImplementedException),
        };
        
        public static HashSet<Type> CaptureAndEvaluateExceptionsToNull { get; set; } = new()
        {
            typeof(NullReferenceException),
            typeof(ArgumentNullException),
        };
    }
}