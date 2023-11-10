#if !NETSTANDARD1_6

// Copyright (c) Microsoft Open Technologies, Inc. All rights reserved. See License.txt in the project root for license information.

using System;
using System.Collections;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Web.UI;

namespace ServiceStack.Html
{
    public class MultiSelectList : IEnumerable<SelectListItem>
    {
        public MultiSelectList(IEnumerable items)
            : this(items, null /* selectedValues */)
        {
        }

        public MultiSelectList(IEnumerable items, IEnumerable selectedValues)
            : this(items, null /* dataValuefield */, null /* dataTextField */, selectedValues)
        {
        }

        public MultiSelectList(IEnumerable items, string dataValueField, string dataTextField)
            : this(items, dataValueField, dataTextField, null /* selectedValues */)
        {
        }

        public MultiSelectList(IEnumerable items, string dataValueField, string dataTextField, IEnumerable selectedValues)
        {
            Items = items ?? throw new ArgumentNullException(nameof(items));
            DataValueField = dataValueField;
            DataTextField = dataTextField;
            SelectedValues = selectedValues;
        }

        public string DataTextField { get; private set; }

        public string DataValueField { get; private set; }

        public IEnumerable Items { get; private set; }

        public IEnumerable SelectedValues { get; private set; }

        public virtual IEnumerator<SelectListItem> GetEnumerator()
        {
            return GetListItems().GetEnumerator();
        }

        internal IList<SelectListItem> GetListItems()
        {
            return !string.IsNullOrEmpty(DataValueField)
                       ? GetListItemsWithValueField()
                       : GetListItemsWithoutValueField();
        }

        private IList<SelectListItem> GetListItemsWithValueField()
        {
            var selectedValues = new HashSet<string>(StringComparer.OrdinalIgnoreCase);
            if (SelectedValues != null)
            {
                selectedValues.UnionWith(from object value in SelectedValues
                                         select Convert.ToString(value, CultureInfo.CurrentCulture));
            }

            var listItems = from object item in Items
                            let value = Eval(item, DataValueField)
                            select new SelectListItem
                            {
                                Value = value,
                                Text = Eval(item, DataTextField),
                                Selected = selectedValues.Contains(value)
                            };
            return listItems.ToList();
        }

        private IList<SelectListItem> GetListItemsWithoutValueField()
        {
            var selectedValues = new HashSet<object>();
            if (SelectedValues != null)
            {
                selectedValues.UnionWith(SelectedValues.Cast<object>());
            }

            var listItems = from object item in Items
                            select new SelectListItem
                            {
                                Text = Eval(item, DataTextField),
                                Selected = selectedValues.Contains(item)
                            };
            return listItems.ToList();
        }

        private static string Eval(object container, string expression)
        {
            var value = container;
            if (!string.IsNullOrEmpty(expression))
            {
                value = DataBinder.Eval(container, expression);
            }
            return Convert.ToString(value, CultureInfo.CurrentCulture);
        }

        #region IEnumerable Members

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }

        #endregion
    }
}

#endif
