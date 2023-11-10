// <copyright file="Custom.ascx.cs" company="clarity-ventures.com">
// Copyright (c) 2019-2023 clarity-ventures.com. All rights reserved.
// </copyright>
// <summary>Implements the custom.ascx class</summary>
// ReSharper disable InconsistentNaming
namespace Clarity.Ecommerce.DNN.Extensions.CEFComponents
{
    using System;
    using System.Text;

    public partial class Custom : CEFComponentUserControl
    {
        protected string HtmlTagValue { get; private set; }

        protected string CEFDirectiveValue { get; private set; }

        protected string AttributeNameValue_0 { get; private set; }

        protected string AttributeValue_0 { get; private set; }

        protected string AttributeNameValue_1 { get; private set; }

        protected string AttributeValue_1 { get; private set; }

        protected string AttributeNameValue_2 { get; private set; }

        protected string AttributeValue_2 { get; private set; }

        protected string AttributeNameValue_3 { get; private set; }

        protected string AttributeValue_3 { get; private set; }

        protected string AttributeNameValue_4 { get; private set; }

        protected string AttributeValue_4 { get; private set; }

        protected string AttributeNameValue_5 { get; private set; }

        protected string AttributeValue_5 { get; private set; }

        protected string AttributeNameValue_6 { get; private set; }

        protected string AttributeValue_6 { get; private set; }

        protected string AttributeNameValue_7 { get; private set; }

        protected string AttributeValue_7 { get; private set; }

        protected string AttributeNameValue_8 { get; private set; }

        protected string AttributeValue_8 { get; private set; }

        protected string AttributeNameValue_9 { get; private set; }

        protected string AttributeValue_9 { get; private set; }

        protected void Page_Load(object sender, EventArgs e)
        {
            HtmlTagValue = Convert.ToString(Component.GetParameter("HtmlTag")?.Value ?? string.Empty);
            CEFDirectiveValue = Convert.ToString(Component.GetParameter("CefDirective")?.Value ?? string.Empty);
            AttributeNameValue_0 = Convert.ToString(Component.GetParameter("AttributeName_0")?.Value ?? string.Empty);
            AttributeValue_0 = Convert.ToString(Component.GetParameter("Attribute_0")?.Value ?? string.Empty);
            AttributeNameValue_1 = Convert.ToString(Component.GetParameter("AttributeName_1")?.Value ?? string.Empty);
            AttributeValue_1 = Convert.ToString(Component.GetParameter("Attribute_1")?.Value ?? string.Empty);
            AttributeNameValue_2 = Convert.ToString(Component.GetParameter("AttributeName_2")?.Value ?? string.Empty);
            AttributeValue_2 = Convert.ToString(Component.GetParameter("Attribute_2")?.Value ?? string.Empty);
            AttributeNameValue_3 = Convert.ToString(Component.GetParameter("AttributeName_3")?.Value ?? string.Empty);
            AttributeValue_3 = Convert.ToString(Component.GetParameter("Attribute_3")?.Value ?? string.Empty);
            AttributeNameValue_4 = Convert.ToString(Component.GetParameter("AttributeName_4")?.Value ?? string.Empty);
            AttributeValue_4 = Convert.ToString(Component.GetParameter("Attribute_4")?.Value ?? string.Empty);
            AttributeNameValue_5 = Convert.ToString(Component.GetParameter("AttributeName_5")?.Value ?? string.Empty);
            AttributeValue_5 = Convert.ToString(Component.GetParameter("Attribute_5")?.Value ?? string.Empty);
            AttributeNameValue_6 = Convert.ToString(Component.GetParameter("AttributeName_6")?.Value ?? string.Empty);
            AttributeValue_6 = Convert.ToString(Component.GetParameter("Attribute_6")?.Value ?? string.Empty);
            AttributeNameValue_7 = Convert.ToString(Component.GetParameter("AttributeName_7")?.Value ?? string.Empty);
            AttributeValue_7 = Convert.ToString(Component.GetParameter("Attribute_7")?.Value ?? string.Empty);
            AttributeNameValue_8 = Convert.ToString(Component.GetParameter("AttributeName_8")?.Value ?? string.Empty);
            AttributeValue_8 = Convert.ToString(Component.GetParameter("Attribute_8")?.Value ?? string.Empty);
            AttributeNameValue_9 = Convert.ToString(Component.GetParameter("AttributeName_9")?.Value ?? string.Empty);
            AttributeValue_9 = Convert.ToString(Component.GetParameter("Attribute_9")?.Value ?? string.Empty);
        }

        protected string RenderComponent()
        {
            var directive = CEFDirectiveValue?.Trim().ToLower();
            var tag = !string.IsNullOrWhiteSpace(HtmlTagValue)
                ? HtmlTagValue.Trim().ToLower()
                : directive;
            var tagDirective = tag == directive
                ? string.Empty
                : $" {directive}";
            var attributes = new StringBuilder();
            AppendAttribute(AttributeNameValue_0, AttributeValue_0, attributes);
            AppendAttribute(AttributeNameValue_1, AttributeValue_1, attributes);
            AppendAttribute(AttributeNameValue_2, AttributeValue_2, attributes);
            AppendAttribute(AttributeNameValue_3, AttributeValue_3, attributes);
            AppendAttribute(AttributeNameValue_4, AttributeValue_4, attributes);
            AppendAttribute(AttributeNameValue_5, AttributeValue_5, attributes);
            AppendAttribute(AttributeNameValue_6, AttributeValue_6, attributes);
            AppendAttribute(AttributeNameValue_7, AttributeValue_7, attributes);
            AppendAttribute(AttributeNameValue_8, AttributeValue_8, attributes);
            AppendAttribute(AttributeNameValue_9, AttributeValue_9, attributes);
            return $"<{tag}{tagDirective}{attributes}></{tag}>";
       }

        private static void AppendAttribute(string name, string value, StringBuilder attributes)
        {
            if (!string.IsNullOrWhiteSpace(name) && !string.IsNullOrWhiteSpace(value))
            {
                attributes.Append($" {name.Trim().ToLower()}=\"{value}\"");
            }
        }
    }
}
