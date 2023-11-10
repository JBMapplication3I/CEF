// <copyright file="CEFComponentParameter.cs" company="clarity-ventures.com">
// Copyright (c) 2019-2023 clarity-ventures.com. All rights reserved.
// </copyright>
// <summary>Implements the cef component parameter class</summary>
namespace Clarity.Ecommerce.DNN.Extensions
{
    using System;
    using Newtonsoft.Json;

    public class CEFComponentParameter
    {
        /// <summary>Gets or sets the name.</summary>
        /// <value>The name.</value>
        public string Name { get; set; }

        /// <summary>Gets or sets the name of the friendly.</summary>
        /// <value>The name of the friendly.</value>
        public string FriendlyName { get; set; }

        /// <summary>Gets or sets the description.</summary>
        /// <value>The description.</value>
        public string Description { get; set; }

        /// <summary>Gets or sets the group.</summary>
        /// <value>The group.</value>
        public string Group { get; set; }

        /// <summary>Gets or sets the order.</summary>
        /// <value>The order.</value>
        public float Order { get; set; }

        /// <summary>Gets or sets the type.</summary>
        /// <value>The type.</value>
        public Type Type { get; set; }

        /// <summary>Gets or sets the value.</summary>
        /// <value>The value.</value>
        public object Value { get; set; }

        /// <summary>Gets the type of the input.</summary>
        /// <value>The type of the input.</value>
        [JsonIgnore]
        public string InputType => GetInputType();

        /// <summary>Gets the input value.</summary>
        /// <value>The input value.</value>
        [JsonIgnore]
        public string InputValue => GetInputValue();

        /// <summary>Gets the input attributes.</summary>
        /// <value>The input attributes.</value>
        [JsonIgnore]
        public string InputAttributes => GetInputAttributes();

        /// <summary>Gets input type.</summary>
        /// <returns>The input type.</returns>
        private string GetInputType()
        {
            switch (Type)
            {
                case { } boolType when boolType == typeof(bool):
                {
                    return "checkbox";
                }
                case { } intType when intType == typeof(int):
                {
                    return "number";
                }
                case { } longType when longType == typeof(long):
                {
                    return "number";
                }
                case { } doubleType when doubleType == typeof(double):
                {
                    return "number";
                }
                case { } decimalType when decimalType == typeof(decimal):
                {
                    return "number";
                }
                case { } dateType when dateType == typeof(DateTime):
                {
                    return "date";
                }
                default:
                {
                    return "text";
                }
            }
        }

        /// <summary>Gets input value.</summary>
        /// <returns>The input value.</returns>
        private string GetInputValue()
        {
            switch (Type)
            {
                case { } boolType when boolType == typeof(bool):
                {
                    return "true";
                }
                default:
                {
                    return Value.ToString();
                }
            }
        }

        /// <summary>Gets input attributes.</summary>
        /// <returns>The input attributes.</returns>
        private string GetInputAttributes()
        {
            switch (Type)
            {
                case { } boolType when boolType == typeof(bool):
                {
                    return (bool)Value ? " checked=\"checked\"" : string.Empty;
                }
                default:
                {
                    return Value.ToString();
                }
            }
        }
    }
}
