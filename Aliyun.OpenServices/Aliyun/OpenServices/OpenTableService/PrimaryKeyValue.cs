namespace Aliyun.OpenServices.OpenTableService
{
    using System;
    using System.Globalization;
    using System.Runtime.CompilerServices;
    using System.Runtime.InteropServices;

    [StructLayout(LayoutKind.Sequential)]
    public struct PrimaryKeyValue
    {
        internal string Value { get; private set; }
        internal bool IsInf { get; private set; }
        public PrimaryKeyType ValueType { get; private set; }
        internal PrimaryKeyValue(string value, PrimaryKeyType valueType) : this(value, valueType, false)
        {
        }

        internal PrimaryKeyValue(string value, PrimaryKeyType valueType, bool isInf)
        {
            this = new PrimaryKeyValue();
            if (value == null)
            {
                throw new ArgumentNullException("value");
            }
            this.Value = value;
            this.ValueType = valueType;
            this.IsInf = isInf;
        }

        public override bool Equals(object obj)
        {
            if ((obj == null) || (obj.GetType() != typeof(PrimaryKeyValue)))
            {
                return false;
            }
            PrimaryKeyValue val = (PrimaryKeyValue) obj;
            if ((this.ValueType != val.ValueType) || (this.IsInf != val.IsInf))
            {
                return false;
            }
            if (this.ValueType == PrimaryKeyType.String)
            {
                return (this.Value == val.Value);
            }
            return (string.Compare(this.Value, val.Value, CultureInfo.InvariantCulture, CompareOptions.IgnoreCase) == 0);
        }

        public override int GetHashCode()
        {
            return (this.Value.GetHashCode() ^ this.ValueType.GetHashCode());
        }

        public static bool operator ==(PrimaryKeyValue leftValue, PrimaryKeyValue rightValue)
        {
            return leftValue.Equals(rightValue);
        }

        public static bool operator !=(PrimaryKeyValue leftValue, PrimaryKeyValue rightValue)
        {
            return !leftValue.Equals(rightValue);
        }

        public static implicit operator PrimaryKeyValue(string value)
        {
            return new PrimaryKeyValue(value, PrimaryKeyType.String);
        }

        public static explicit operator string(PrimaryKeyValue value)
        {
            if (value.ValueType != PrimaryKeyType.String)
            {
                throw new InvalidCastException(string.Format(CultureInfo.CurrentUICulture, OtsExceptions.ValueCastInvalidTypeFormat, new object[] { "String" }));
            }
            return value.Value;
        }

        public static implicit operator PrimaryKeyValue(long value)
        {
            return new PrimaryKeyValue(value.ToString(CultureInfo.InvariantCulture), PrimaryKeyType.Integer);
        }

        public static explicit operator long(PrimaryKeyValue value)
        {
            long result;
            if (value.ValueType != PrimaryKeyType.Integer)
            {
                throw new InvalidCastException(string.Format(CultureInfo.CurrentUICulture, OtsExceptions.ValueCastInvalidTypeFormat, new object[] { "Int64" }));
            }
            if (!long.TryParse(value.Value, NumberStyles.Integer, CultureInfo.InvariantCulture, out result))
            {
                throw new InvalidCastException(string.Format(CultureInfo.CurrentUICulture, OtsExceptions.ValueCastFailedFormat, new object[] { "Int64" }));
            }
            return result;
        }

        public static implicit operator PrimaryKeyValue(bool value)
        {
            return new PrimaryKeyValue(value.ToString(CultureInfo.InvariantCulture), PrimaryKeyType.Boolean);
        }

        public static explicit operator bool(PrimaryKeyValue value)
        {
            bool result;
            if (value.ValueType != PrimaryKeyType.Boolean)
            {
                throw new InvalidCastException(string.Format(CultureInfo.CurrentUICulture, OtsExceptions.ValueCastInvalidTypeFormat, new object[] { "Boolean" }));
            }
            if (!bool.TryParse(value.Value, out result))
            {
                throw new InvalidCastException(string.Format(CultureInfo.CurrentUICulture, OtsExceptions.ValueCastFailedFormat, new object[] { "Boolean" }));
            }
            return result;
        }

        public override string ToString()
        {
            if (this.Value == null)
            {
                return string.Empty;
            }
            return this.Value;
        }

        internal int CompareTo(PrimaryKeyValue right)
        {
            PrimaryKeyValue left = this;
            if (left == PrimaryKeyRange.InfMax)
            {
                if (!(right == PrimaryKeyRange.InfMax))
                {
                    return 1;
                }
                return 0;
            }
            if (left == PrimaryKeyRange.InfMin)
            {
                if (!(right == PrimaryKeyRange.InfMin))
                {
                    return -1;
                }
                return 0;
            }
            if (right == PrimaryKeyRange.InfMin)
            {
                if (!(left == PrimaryKeyRange.InfMin))
                {
                    return 1;
                }
                return 0;
            }
            if (right == PrimaryKeyRange.InfMax)
            {
                if (!(left == PrimaryKeyRange.InfMax))
                {
                    return -1;
                }
                return 0;
            }
            if (left.ValueType == PrimaryKeyType.Integer)
            {
                long temp = (long) left;
                return temp.CompareTo((long) right);
            }
            if (left.ValueType != PrimaryKeyType.Boolean)
            {
                return StringComparer.Ordinal.Compare(left.Value, right.Value);
            }
            if (left.Value == right.Value)
            {
                return 0;
            }
            if ((bool) left)
            {
                return 1;
            }
            return -1;
        }
    }
}

