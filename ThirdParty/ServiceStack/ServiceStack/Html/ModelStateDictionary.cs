using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

namespace ServiceStack.Html
{
    public class ModelStateDictionary : IDictionary<string, ModelState>
    {
        private readonly Dictionary<string, ModelState> innerDictionary = new(StringComparer.OrdinalIgnoreCase);

        public ModelStateDictionary() { }

        public ModelStateDictionary(ModelStateDictionary dictionary)
        {
            if (dictionary == null)
            {
                throw new ArgumentNullException(nameof(dictionary));
            }

            foreach (var entry in dictionary)
            {
                innerDictionary.Add(entry.Key, entry.Value);
            }
        }

        public int Count => innerDictionary.Count;

        public bool IsReadOnly => ((IDictionary<string, ModelState>)innerDictionary).IsReadOnly;

        public bool IsValid
        {
            get
            {
                return Values.All(modelState => modelState.Errors.Count == 0);
            }
        }

        public ICollection<string> Keys => innerDictionary.Keys;

        public ModelState this[string key]
        {
            get
            {
                innerDictionary.TryGetValue(key, out var value);
                return value;
            }
            set => innerDictionary[key] = value;
        }

        public ICollection<ModelState> Values => innerDictionary.Values;

        public void Add(KeyValuePair<string, ModelState> item)
        {
            ((IDictionary<string, ModelState>)innerDictionary).Add(item);
        }

        public void Add(string key, ModelState value)
        {
            innerDictionary.Add(key, value);
        }

        public void AddModelError(string key, Exception exception)
        {
            GetModelStateForKey(key).Errors.Add(exception);
        }

        public void AddModelError(string key, string errorMessage)
        {
            GetModelStateForKey(key).Errors.Add(errorMessage);
        }

        public void Clear()
        {
            innerDictionary.Clear();
        }

        public bool Contains(KeyValuePair<string, ModelState> item)
        {
            return ((IDictionary<string, ModelState>)innerDictionary).Contains(item);
        }

        public bool ContainsKey(string key)
        {
            return innerDictionary.ContainsKey(key);
        }

        public void CopyTo(KeyValuePair<string, ModelState>[] array, int arrayIndex)
        {
            ((IDictionary<string, ModelState>)innerDictionary).CopyTo(array, arrayIndex);
        }

        public IEnumerator<KeyValuePair<string, ModelState>> GetEnumerator()
        {
            return innerDictionary.GetEnumerator();
        }

        private ModelState GetModelStateForKey(string key)
        {
            if (key == null)
            {
                throw new ArgumentNullException(nameof(key));
            }

            if (!TryGetValue(key, out var modelState))
            {
                modelState = new();
                this[key] = modelState;
            }

            return modelState;
        }

        public bool IsValidField(string key)
        {
            if (key == null)
            {
                throw new ArgumentNullException(nameof(key));
            }

            // if the key is not found in the dictionary, we just say that it's valid (since there are no errors)
            return DictionaryHelpers.FindKeysWithPrefix(this, key).All(entry => entry.Value.Errors.Count == 0);
        }

        public void Merge(ModelStateDictionary dictionary)
        {
            if (dictionary == null)
            {
                return;
            }

            foreach (var entry in dictionary)
            {
                this[entry.Key] = entry.Value;
            }
        }

        public bool Remove(KeyValuePair<string, ModelState> item)
        {
            return ((IDictionary<string, ModelState>)innerDictionary).Remove(item);
        }

        public bool Remove(string key)
        {
            return innerDictionary.Remove(key);
        }

        public void SetModelValue(string key, ValueProviderResult value)
        {
            GetModelStateForKey(key).Value = value;
        }

        public bool TryGetValue(string key, out ModelState value)
        {
            return innerDictionary.TryGetValue(key, out value);
        }

        #region IEnumerable Members
        IEnumerator IEnumerable.GetEnumerator()
        {
            return ((IEnumerable)innerDictionary).GetEnumerator();
        }
        #endregion
    }

    internal static class DictionaryHelpers
    {
        public static IEnumerable<KeyValuePair<string, TValue>> FindKeysWithPrefix<TValue>(IDictionary<string, TValue> dictionary, string prefix)
        {
            if (dictionary.TryGetValue(prefix, out var exactMatchValue))
            {
                yield return new(prefix, exactMatchValue);
            }

            foreach (var entry in dictionary)
            {
                var key = entry.Key;

                if (key.Length <= prefix.Length)
                {
                    continue;
                }

                if (!key.StartsWith(prefix, StringComparison.OrdinalIgnoreCase))
                {
                    continue;
                }

                var charAfterPrefix = key[prefix.Length];
                switch (charAfterPrefix)
                {
                    case '[':
                    case '.':
                        yield return entry;
                        break;
                }
            }
        }

        public static bool DoesAnyKeyHavePrefix<TValue>(IDictionary<string, TValue> dictionary, string prefix)
        {
            return FindKeysWithPrefix(dictionary, prefix).Any();
        }

        public static TValue GetOrDefault<TKey, TValue>(this IDictionary<TKey, TValue> dict, TKey key, TValue @default)
        {
            if (dict.TryGetValue(key, out var value))
            {
                return value;
            }
            return @default;
        }
    }
}
