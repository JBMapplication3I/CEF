﻿/* ****************************************************************************
 *
 * Copyright (c) Microsoft Corporation. All rights reserved.
 *
 * This software is subject to the Microsoft Public License (Ms-PL). 
 * A copy of the license can be found in the license.htm file included 
 * in this distribution.
 *
 * You must not remove this notice, or any other, from this software.
 *
 * ***************************************************************************/

// Copyright (c) Microsoft Open Technologies, Inc. All rights reserved. See License.txt in the project root for license information.

using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Threading;
using System.Web;

namespace ServiceStack.Html
{
    internal delegate bool TryGetValueDelegate(object dictionary, string key, out object value);

    internal static class TypeHelpers
    {
        private static readonly Dictionary<Type, TryGetValueDelegate> _tryGetValueDelegateCache = new();
        private static readonly ReaderWriterLockSlim _tryGetValueDelegateCacheLock = new();

        private static readonly MethodInfo _strongTryGetValueImplInfo = typeof(TypeHelpers).GetMethod("StrongTryGetValueImpl", BindingFlags.NonPublic | BindingFlags.Static);

        public static readonly Assembly MsCorLibAssembly = typeof(string).GetAssembly();
        //public static readonly Assembly MvcAssembly = typeof(Controller).Assembly;
#if !NETSTANDARD1_6
        public static readonly Assembly SystemWebAssembly = typeof(HttpContext).GetAssembly();
#endif

        // method is used primarily for lighting up new .NET Framework features even if MVC targets the previous version
        // thisParameter is the 'this' parameter if target method is instance method, should be null for static method
        public static TDelegate CreateDelegate<TDelegate>(Assembly assembly, string typeName, string methodName, object thisParameter) where TDelegate : class
        {
            // ensure target type exists
            var targetType = assembly.GetType(typeName, false /* throwOnError */);
            if (targetType == null)
            {
                return null;
            }

            return CreateDelegate<TDelegate>(targetType, methodName, thisParameter);
        }

        public static TDelegate CreateDelegate<TDelegate>(Type targetType, string methodName, object thisParameter) where TDelegate : class
        {
            // ensure target method exists
            var delegateParameters = typeof(TDelegate).GetMethod("Invoke").GetParameters();
            var argumentTypes = delegateParameters.Map(pInfo => pInfo.ParameterType).ToArray();
            var targetMethod = targetType.GetMethod(methodName, argumentTypes);
            if (targetMethod == null)
            {
                return null;
            }

            var d = targetMethod.CreateDelegate(typeof(TDelegate), thisParameter) as TDelegate;
            return d;
        }

        public static TryGetValueDelegate CreateTryGetValueDelegate(Type targetType)
        {
            TryGetValueDelegate result;

            _tryGetValueDelegateCacheLock.EnterReadLock();
            try
            {
                if (_tryGetValueDelegateCache.TryGetValue(targetType, out result))
                {
                    return result;
                }
            }
            finally
            {
                _tryGetValueDelegateCacheLock.ExitReadLock();
            }

            var dictionaryType = ExtractGenericInterface(targetType, typeof(IDictionary<,>));

            // just wrap a call to the underlying IDictionary<TKey, TValue>.TryGetValue() where string can be cast to TKey
            if (dictionaryType != null)
            {
                var typeArguments = dictionaryType.GetGenericArguments();
                var keyType = typeArguments[0];
                var returnType = typeArguments[1];

                if (keyType.IsAssignableFrom(typeof(string)))
                {
                    var strongImplInfo = _strongTryGetValueImplInfo.MakeGenericMethod(keyType, returnType);
                    result = (TryGetValueDelegate)strongImplInfo.CreateDelegate(typeof(TryGetValueDelegate));
                }
            }

            // wrap a call to the underlying IDictionary.Item()
            if (result == null && typeof(IDictionary).IsAssignableFrom(targetType))
            {
                result = TryGetValueFromNonGenericDictionary;
            }

            _tryGetValueDelegateCacheLock.EnterWriteLock();
            try
            {
                _tryGetValueDelegateCache[targetType] = result;
            }
            finally
            {
                _tryGetValueDelegateCacheLock.ExitWriteLock();
            }

            return result;
        }

        public static Type ExtractGenericInterface(Type queryType, Type interfaceType)
        {
            Func<Type, bool> matchesInterface = t => t.IsGenericType() && t.GetGenericTypeDefinition() == interfaceType;
            return matchesInterface(queryType) ? queryType : queryType.GetInterfaces().FirstOrDefault(matchesInterface);
        }

        public static object GetDefaultValue(Type type)
        {
            return TypeAllowsNullValue(type) ? null : Activator.CreateInstance(type);
        }

        public static bool IsCompatibleObject<T>(object value)
        {
            return value is T || (value == null && TypeAllowsNullValue(typeof(T)));
        }

        public static bool IsNullableValueType(Type type)
        {
            return Nullable.GetUnderlyingType(type) != null;
        }

        private static bool StrongTryGetValueImpl<TKey, TValue>(object dictionary, string key, out object value)
        {
            var strongDict = (IDictionary<TKey, TValue>)dictionary;

            var retVal = strongDict.TryGetValue((TKey)(object)key, out var strongValue);
            value = strongValue;
            return retVal;
        }

        private static bool TryGetValueFromNonGenericDictionary(object dictionary, string key, out object value)
        {
            var weakDict = (IDictionary)dictionary;

            var containsKey = weakDict.Contains(key);
            value = containsKey ? weakDict[key] : null;
            return containsKey;
        }

        public static bool TypeAllowsNullValue(Type type)
        {
            return !type.IsValueType() || IsNullableValueType(type);
        }
    }
}
