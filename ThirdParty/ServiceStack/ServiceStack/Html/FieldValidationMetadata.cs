// Copyright (c) Microsoft Open Technologies, Inc. All rights reserved. See License.txt in the project root for license information.

using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;

namespace ServiceStack.Html
{
    public class FieldValidationMetadata
    {
        private readonly Collection<ModelClientValidationRule> _validationRules = new();
        private string _fieldName;

        public string FieldName
        {
            get => _fieldName ?? string.Empty;
            set => _fieldName = value;
        }

        public bool ReplaceValidationMessageContents { get; set; }

        public string ValidationMessageId { get; set; }

        public ICollection<ModelClientValidationRule> ValidationRules => _validationRules;
    }
}
