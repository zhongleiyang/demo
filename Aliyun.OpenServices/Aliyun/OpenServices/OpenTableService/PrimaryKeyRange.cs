namespace Aliyun.OpenServices.OpenTableService
{
    using Aliyun.OpenServices.OpenTableService.Utilities;
    using System;
    using System.Runtime.CompilerServices;

    public class PrimaryKeyRange
    {
        private static readonly PrimaryKeyValue _infMax = new PrimaryKeyValue("INF_MAX", PrimaryKeyType.String, true);
        private static readonly PrimaryKeyValue _infMin = new PrimaryKeyValue("INF_MIN", PrimaryKeyType.String, true);

        public PrimaryKeyRange(string primaryKeyName, PrimaryKeyValue rangeBegin, PrimaryKeyValue rangeEnd) : this(primaryKeyName, rangeBegin, rangeEnd, !rangeBegin.IsInf ? rangeBegin.ValueType : (!rangeEnd.IsInf ? rangeEnd.ValueType : PrimaryKeyType.String))
        {
        }

        public PrimaryKeyRange(string primaryKeyName, PrimaryKeyValue rangeBegin, PrimaryKeyValue rangeEnd, PrimaryKeyType primaryKeyType)
        {
            if (string.IsNullOrEmpty(primaryKeyName) || !OtsUtility.IsEntityNameValid(primaryKeyName))
            {
                throw new ArgumentException(OtsExceptions.NameFormatIsInvalid, "primaryKeyName");
            }
            if ((!rangeBegin.IsInf && (rangeBegin.ValueType != primaryKeyType)) || (!rangeEnd.IsInf && (rangeEnd.ValueType != primaryKeyType)))
            {
                throw new ArgumentException(OtsExceptions.RangeTypeNotMatch);
            }
            this.PrimaryKeyName = primaryKeyName;
            this.RangeBegin = rangeBegin;
            this.RangeEnd = rangeEnd;
            this.RangeType = primaryKeyType;
        }

        public static PrimaryKeyValue InfMax
        {
            get
            {
                return _infMax;
            }
        }

        public static PrimaryKeyValue InfMin
        {
            get
            {
                return _infMin;
            }
        }

        public string PrimaryKeyName { get; private set; }

        public PrimaryKeyValue RangeBegin { get; private set; }

        public PrimaryKeyValue RangeEnd { get; private set; }

        public PrimaryKeyType RangeType { get; private set; }
    }
}

