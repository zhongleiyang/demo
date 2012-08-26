namespace Aliyun.OpenServices.OpenTableService.Utilities
{
    using Aliyun.OpenServices.OpenTableService;
    using System;
    using System.Collections;
    using System.Collections.Generic;
    using System.Reflection;

    internal class EntityNameList : IList<string>, ICollection<string>, IEnumerable<string>, IEnumerable
    {
        private List<string> _innerList;

        public EntityNameList()
        {
            this._innerList = new List<string>();
        }

        public EntityNameList(IEnumerable<string> collection)
        {
            this._innerList = new List<string>(collection);
        }

        public void Add(string item)
        {
            OnAdding(item);
            this._innerList.Add(item);
        }

        public void Clear()
        {
            this._innerList.Clear();
        }

        public bool Contains(string item)
        {
            return this._innerList.Contains(item);
        }

        public void CopyTo(string[] array, int arrayIndex)
        {
            this._innerList.CopyTo(array, arrayIndex);
        }

        public IEnumerator<string> GetEnumerator()
        {
            return this._innerList.GetEnumerator();
        }

        public int IndexOf(string item)
        {
            return this._innerList.IndexOf(item);
        }

        public void Insert(int index, string item)
        {
            OnAdding(item);
            this._innerList.Insert(index, item);
        }

        private static void OnAdding(string item)
        {
            if (string.IsNullOrEmpty(item) || !OtsUtility.IsEntityNameValid(item))
            {
                throw new ArgumentException(OtsExceptions.NameFormatIsInvalid);
            }
        }

        public bool Remove(string item)
        {
            return this._innerList.Remove(item);
        }

        public void RemoveAt(int index)
        {
            this._innerList.RemoveAt(index);
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return this._innerList.GetEnumerator();
        }

        public int Count
        {
            get
            {
                return this._innerList.Count;
            }
        }

        public bool IsReadOnly
        {
            get
            {
                return false;
            }
        }

        public string this[int index]
        {
            get
            {
                return this._innerList[index];
            }
            set
            {
                OnAdding(value);
                this._innerList[index] = value;
            }
        }
    }
}

