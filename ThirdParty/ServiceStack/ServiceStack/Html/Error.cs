using System;
using System.Globalization;

namespace ServiceStack.Html
{
    internal static class Error
    {
        public static InvalidOperationException ViewDataDictionary_WrongTModelType(Type valueType, Type modelType)
        {
            var message = string.Format(CultureInfo.CurrentCulture, MvcResources.ViewDataDictionary_WrongTModelType,
                valueType, modelType);
            return new(message);
        }

        public static InvalidOperationException ViewDataDictionary_ModelCannotBeNull(Type modelType)
        {
            var message = string.Format(CultureInfo.CurrentCulture, MvcResources.ViewDataDictionary_ModelCannotBeNull,
                modelType);
            return new(message);
        }

        public static ArgumentException ParameterCannotBeNullOrEmpty(string parameterName)
        {
            return new(MvcResources.Common_NullOrEmpty, parameterName);
        }
    }
}
