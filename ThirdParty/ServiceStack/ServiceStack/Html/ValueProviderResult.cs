﻿using System;
using System.Collections;
using System.ComponentModel;
using System.Globalization;

namespace ServiceStack.Html
{
#if !NETSTANDARD1_6
    [Serializable]
#endif
    public class ValueProviderResult
    {
        private static readonly CultureInfo _staticCulture = CultureInfo.InvariantCulture;
        private CultureInfo _instanceCulture;

        // default constructor so that subclassed types can set the properties themselves
        protected ValueProviderResult() { }

        public ValueProviderResult(object rawValue, string attemptedValue, CultureInfo culture)
        {
            RawValue = rawValue;
            AttemptedValue = attemptedValue;
            Culture = culture;
        }

        public string AttemptedValue
        {
            get;
            protected set;
        }

        public CultureInfo Culture
        {
            get
            {
                _instanceCulture ??= _staticCulture;
                return _instanceCulture;
            }
            protected set => _instanceCulture = value;
        }

        public object RawValue
        {
            get;
            protected set;
        }

        private static object ConvertSimpleType(CultureInfo culture, object value, Type destinationType)
        {
            if (value == null || destinationType.InstanceOfType(value))
            {
                return value;
            }

            // if this is a user-input value but the user didn't type anything, return no value
            var valueAsString = value as string;
            if (valueAsString?.Trim().Length == 0)
            {
                return null;
            }

#if !NETSTANDARD1_6
            var converter = TypeDescriptor.GetConverter(destinationType);
            var canConvertFrom = converter.CanConvertFrom(value.GetType());
            if (!canConvertFrom)
            {
                converter = TypeDescriptor.GetConverter(value.GetType());
            }
            if (!(canConvertFrom || converter.CanConvertTo(destinationType)))
            {
                var message = string.Format(CultureInfo.CurrentCulture, MvcResources.ValueProviderResult_NoConverterExists,
                    value.GetType().FullName, destinationType.FullName);
                throw new InvalidOperationException(message);
            }

            try
            {
                var convertedValue = canConvertFrom ?
                     converter.ConvertFrom(null /* context */, culture, value) :
                     converter.ConvertTo(null /* context */, culture, value, destinationType);
                return convertedValue;
            }
            catch (Exception ex)
            {
                var message = string.Format(CultureInfo.CurrentCulture, MvcResources.ValueProviderResult_ConversionThrew,
                    value.GetType().FullName, destinationType.FullName);
                throw new InvalidOperationException(message, ex);
            }
#else
		    return value.ConvertTo(destinationType);			
#endif

        }

        public object ConvertTo(Type type)
        {
            return ConvertTo(type, null /* culture */);
        }

        public virtual object ConvertTo(Type type, CultureInfo culture)
        {
            if (type == null)
            {
                throw new ArgumentNullException(nameof(type));
            }

            var cultureToUse = culture ?? Culture;
            return UnwrapPossibleArrayType(cultureToUse, RawValue, type);
        }

        private static object UnwrapPossibleArrayType(CultureInfo culture, object value, Type destinationType)
        {
            if (value == null || destinationType.InstanceOfType(value))
            {
                return value;
            }

            // array conversion results in four cases, as below
            var valueAsArray = value as Array;
            if (destinationType.IsArray)
            {
                var destinationElementType = destinationType.GetElementType();
                if (valueAsArray != null)
                {
                    // case 1: both destination + source type are arrays, so convert each element
                    IList converted = Array.CreateInstance(destinationElementType, valueAsArray.Length);
                    for (var i = 0; i < valueAsArray.Length; i++)
                    {
                        converted[i] = ConvertSimpleType(culture, valueAsArray.GetValue(i), destinationElementType);
                    }
                    return converted;
                }
                else
                {
                    // case 2: destination type is array but source is single element, so wrap element in array + convert
                    var element = ConvertSimpleType(culture, value, destinationElementType);
                    IList converted = Array.CreateInstance(destinationElementType, 1);
                    converted[0] = element;
                    return converted;
                }
            }
            else if (valueAsArray != null)
            {
                // case 3: destination type is single element but source is array, so extract first element + convert
                if (valueAsArray.Length > 0)
                {
                    value = valueAsArray.GetValue(0);
                    return ConvertSimpleType(culture, value, destinationType);
                }
                else
                {
                    // case 3(a): source is empty array, so can't perform conversion
                    return null;
                }
            }
            // case 4: both destination + source type are single elements, so convert
            return ConvertSimpleType(culture, value, destinationType);
        }
    }
}
