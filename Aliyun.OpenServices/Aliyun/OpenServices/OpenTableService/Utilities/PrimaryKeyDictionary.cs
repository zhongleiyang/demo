namespace Aliyun.OpenServices.OpenTableService.Utilities
{
    using Aliyun.OpenServices.OpenTableService;
    using System;
    using System.Collections.Generic;

    internal class PrimaryKeyDictionary : EntityDictionary<PrimaryKeyValue>
    {
        public PrimaryKeyDictionary()
        {
        }

        public PrimaryKeyDictionary(IDictionary<string, PrimaryKeyValue> dictionary) : base(dictionary)
        {
        }

        public PrimaryKeyDictionary(IEqualityComparer<string> comparer) : base(comparer)
        {
        }

        public PrimaryKeyDictionary(int capacity) : base(capacity)
        {
        }

        public PrimaryKeyDictionary(IDictionary<string, PrimaryKeyValue> dictionary, IEqualityComparer<string> comparer) : base(dictionary, comparer)
        {
        }

        public PrimaryKeyDictionary(int capacity, IEqualityComparer<string> comparer) : base(capacity, comparer)
        {
        }

        protected override void OnAdding(string key, PrimaryKeyValue value)
        {
            base.OnAdding(key, value);
            if (string.IsNullOrEmpty(value.Value))
            {
                throw new ArgumentException(OtsExceptions.PrimaryKeyValueIsNullOrEmpty);
            }
            if (value.IsInf)
            {
                throw new ArgumentException(OtsExceptions.PKInfNotAllowed);
            }
        }
    }
}

