using System;
using System.Collections;
using System.Collections.Generic;

namespace Eco.Echolon.ApiClient.Model.CommonModels
{
    public class KeyValueDictionary : IDictionary
    {
        public readonly IDictionary Dictionary;

        public KeyValueDictionary(IDictionary dictionary)
        {
            Dictionary = dictionary;
        }

        public void Add(object key, object value)
        {
            Dictionary.Add(key, value);
        }

        public void Clear()
        {
            Dictionary.Clear();
        }

        public bool Contains(object key)
        {
            return Dictionary.Contains(key);
        }

        public IDictionaryEnumerator GetEnumerator()
        {
            return Dictionary.GetEnumerator();
        }

        public void Remove(object key)
        {
            Dictionary.Remove(key);
        }

        public bool IsFixedSize { get => Dictionary.IsFixedSize; }
        public bool IsReadOnly { get => Dictionary.IsReadOnly; }

        public object this[object key]
        {
            get => Dictionary[key];
            set => Dictionary[key] = value;
        }

        public ICollection Keys { get => Dictionary.Keys; }
        public ICollection Values { get => Dictionary.Values; }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }

        public void CopyTo(Array array, int index)
        {
            Dictionary.CopyTo(array, index);
        }

        public int Count { get => Dictionary.Count; }
        public bool IsSynchronized { get => Dictionary.IsSynchronized; }
        public object SyncRoot { get => Dictionary.SyncRoot; }
    }
}