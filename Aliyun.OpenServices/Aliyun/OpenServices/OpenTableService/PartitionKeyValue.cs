namespace Aliyun.OpenServices.OpenTableService
{
    using System;
    using System.Globalization;
    using System.Runtime.CompilerServices;
    using System.Runtime.InteropServices;

    [StructLayout(LayoutKind.Sequential)]
    public struct PartitionKeyValue
    {
        internal string Value { get; private set; }
        public PartitionKeyType ValueType { get; private set; }
        internal PartitionKeyValue(string value, PartitionKeyType valueType)
        {
            this = new PartitionKeyValue();
            if (value == null)
            {
                throw new ArgumentNullException("value");
            }
            this.Value = value;
            this.ValueType = valueType;
        }

        public override bool Equals(object obj)
        {
            if ((obj == null) || (obj.GetType() != typeof(PartitionKeyValue)))
            {
                return false;
            }
            PartitionKeyValue val = (PartitionKeyValue) obj;
            return ((this.ValueType == val.ValueType) && (this.Value == val.Value));
        }

        public override int GetHashCode()
        {
            return (this.Value.GetHashCode() ^ this.ValueType.GetHashCode());
        }

        public override string ToString()
        {
            if (this.Value == null)
            {
                return string.Empty;
            }
            return this.Value;
        }

        public static bool operator ==(PartitionKeyValue leftValue, PartitionKeyValue rightValue)
        {
            return leftValue.Equals(rightValue);
        }

        public static bool operator !=(PartitionKeyValue leftValue, PartitionKeyValue rightValue)
        {
            return !leftValue.Equals(rightValue);
        }

        public static implicit operator PartitionKeyValue(string value)
        {
            return new PartitionKeyValue(value, PartitionKeyType.String);
        }

        public static explicit operator string(PartitionKeyValue value)
        {
            if (value.ValueType != PartitionKeyType.String)
            {
                throw new InvalidCastException(string.Format(CultureInfo.CurrentUICulture, OtsExceptions.ValueCastInvalidTypeFormat, new object[] { "String" }));
            }
            return value.Value;
        }

        public static implicit operator PartitionKeyValue(long value)
        {
            return new PartitionKeyValue(value.ToString(CultureInfo.InvariantCulture), PartitionKeyType.Integer);
        }

        public static explicit operator long(PartitionKeyValue value)
        {
            long result;
            if (value.ValueType != PartitionKeyType.Integer)
            {
                throw new InvalidCastException(string.Format(CultureInfo.CurrentUICulture, OtsExceptions.ValueCastInvalidTypeFormat, new object[] { "Int64" }));
            }
            if (!long.TryParse(value.Value, NumberStyles.Integer, CultureInfo.InvariantCulture, out result))
            {
                throw new InvalidCastException(string.Format(CultureInfo.CurrentUICulture, OtsExceptions.ValueCastFailedFormat, new object[] { "Int64" }));
            }
            return result;
        }
    }
}

