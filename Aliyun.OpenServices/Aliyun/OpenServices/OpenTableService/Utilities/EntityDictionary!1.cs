namespace Aliyun.OpenServices.OpenTableService.Utilities
{
    using Aliyun.OpenServices.OpenTableService;
    using System;
    using System.Collections;
    using System.Collections.Generic;
    using System.Linq;
    using System.Reflection;
    using System.Runtime.InteropServices;

    internal class EntityDictionary<TValue> : IDictionary<string, TValue>, ICollection<KeyValuePair<string, TValue>>, IEnumerable<KeyValuePair<string, TValue>>, IEnumerable
    {
        private Dictionary<string, TValue> _innerDictionary;

        public EntityDictionary()
        {
            this._innerDictionary = new Dictionary<string, TValue>();
        }

        public EntityDictionary(IDictionary<string, TValue> dictionary)
        {
            this._innerDictionary = new Dictionary<string, TValue>(dictionary);
        }

        public EntityDictionary(IEqualityComparer<string> comparer)
        {
            this._innerDictionary = new Dictionary<string, TValue>(comparer);
        }

        public EntityDictionary(int capacity)
        {
            this._innerDictionary = new Dictionary<string, TValue>(capacity);
        }

        public EntityDictionary(IDictionary<string, TValue> dictionary, IEqualityComparer<string> comparer)
        {
            this._innerDictionary = new Dictionary<string, TValue>(dictionary, comparer);
        }

        public EntityDictionary(int capacity, IEqualityComparer<string> comparer)
        {
            this._innerDictionary = new Dictionary<string, TValue>(capacity, comparer);
        }

        public void Add(KeyValuePair<string, TValue> item)
        {
            if (item.Key == null)
            {
                throw new ArgumentNullException("item");
            }
            this.OnAdding(item.Key, item.Value);
            this._innerDictionary.Add(item.Key, item.Value);
        }

        public void Add(string key, TValue value)
        {
            if (key == null)
            {
                throw new ArgumentNullException("key");
            }
            this.OnAdding(key, value);
            this._innerDictionary.Add(key, value);
        }

        public void Clear()
        {
            this._innerDictionary.Clear();
        }

        public bool Contains(KeyValuePair<string, TValue> item)
        {
            return this._innerDictionary.Contains<KeyValuePair<string, TValue>>(item);
        }

        public bool ContainsKey(string key)
        {
            return this._innerDictionary.ContainsKey(key);
        }

        public void CopyTo(KeyValuePair<string, TValue>[] array, int arrayIndex)
        {
           // this._innerDictionary.CopyTo(array, arrayIndex);
        }

        public IEnumerator<KeyValuePair<string, TValue>> GetEnumerator()
        {
            return this._innerDictionary.GetEnumerator();
        }

        protected virtual void OnAdding(string key, TValue value)
        {
            if (string.IsNullOrEmpty(key) || !OtsUtility.IsEntityNameValid(key))
            {
                throw new ArgumentException(OtsExceptions.NameFormatIsInvalid, "key");
            }
        }

        public bool Remove(KeyValuePair<string, TValue> item)
        {
            return this._innerDictionary.Remove(item.Key);
        }

        public bool Remove(string key)
        {
            return this._innerDictionary.Remove(key);
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return this._innerDictionary.GetEnumerator();
        }

        public bool TryGetValue(string key, out TValue value)
        {
            return this._innerDictionary.TryGetValue(key, out value);
        }

        public int Count
        {
            get
            {
                return this._innerDictionary.Count;
            }
        }

        public bool IsReadOnly
        {
            get
            {
                return ((IDictionary) this._innerDictionary).IsReadOnly;
            }
        }

        public TValue this[string key]
        {
            get
            {
                return this._innerDictionary[key];
            }
            set
            {
                this.OnAdding(key, value);
                this._innerDictionary[key] = value;
            }
        }

        public ICollection<string> Keys
        {
            get
            {
                return this._innerDictionary.Keys;
            }
        }

        public ICollection<TValue> Values
        {
            get
            {
                return (ICollection<TValue>) this._innerDictionary.Values;
            }
        }
    }
}

