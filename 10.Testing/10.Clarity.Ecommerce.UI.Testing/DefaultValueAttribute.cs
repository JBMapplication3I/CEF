namespace Clarity.Ecommerce.UI.Testing
{
    using System;

    internal sealed class DefaultValueAttribute : Attribute
    {
        internal string Value;

        internal DefaultValueAttribute(string value)
        {
            Value = value;
        }
    }
}
