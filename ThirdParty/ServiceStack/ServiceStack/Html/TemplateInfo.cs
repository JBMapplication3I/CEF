// Copyright (c) Microsoft Open Technologies, Inc. All rights reserved. See License.txt in the project root for license information.

using System;
using System.Collections.Generic;

namespace ServiceStack.Html
{
    public class TemplateInfo
    {
        private string _htmlFieldPrefix;
        private object _formattedModelValue;
        private HashSet<object> _visitedObjects;

        public object FormattedModelValue
        {
            get => _formattedModelValue ?? string.Empty;
            set => _formattedModelValue = value;
        }

        public string HtmlFieldPrefix
        {
            get => _htmlFieldPrefix ?? string.Empty;
            set => _htmlFieldPrefix = value;
        }

        public int TemplateDepth => VisitedObjects.Count;

        // DDB #224750 - Keep a collection of visited objects to prevent infinite recursion
        internal HashSet<object> VisitedObjects
        {
            get
            {
                _visitedObjects ??= new();
                return _visitedObjects;
            }
            set => _visitedObjects = value;
        }

        public string GetFullHtmlFieldId(string partialFieldName)
        {
            return HtmlHelper.GenerateIdFromName(GetFullHtmlFieldName(partialFieldName));
        }

        public string GetFullHtmlFieldName(string partialFieldName)
        {
            // This uses "combine and trim" because either or both of these values might be empty
            return (HtmlFieldPrefix + "." + (partialFieldName ?? string.Empty)).Trim('.');
        }

        public bool Visited(ModelMetadata metadata)
        {
            return VisitedObjects.Contains(metadata.Model ?? metadata.ModelType);
        }
    }
}
