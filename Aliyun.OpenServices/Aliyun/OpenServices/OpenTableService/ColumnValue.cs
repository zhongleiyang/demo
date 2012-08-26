namespace Aliyun.OpenServices.OpenTableService
{
    using System;
    using System.Globalization;
    using System.Runtime.CompilerServices;
    using System.Runtime.InteropServices;

    [StructLayout(LayoutKind.Sequential)]
    public struct ColumnValue
    {
        internal string Value { get; private set; }
        public ColumnType ValueType { get; private set; }
        internal ColumnValue(string value, ColumnType valueType)
        {
            this = new ColumnValue();
            if (value == null)
            {
                throw new ArgumentNullException("value");
            }
            this.Value = value;
            this.ValueType = valueType;
        }

        public override bool Equals(object obj)
        {
            if ((obj == null) || (obj.GetType() != typeof(ColumnValue)))
            {
                return false;
            }
            ColumnValue val = (ColumnValue) obj;
            if (this.ValueType != val.ValueType)
            {
                return false;
            }
            if (this.ValueType == ColumnType.String)
            {
                return (this.Value == val.Value);
            }
            return (string.Compare(this.Value, val.Value, CultureInfo.InvariantCulture, CompareOptions.IgnoreCase) == 0);
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

        public static bool operator ==(ColumnValue leftValue, ColumnValue rightValue)
        {
            return leftValue.Equals(rightValue);
        }

        public static bool operator !=(ColumnValue value1, ColumnValue rightValue)
        {
            return !value1.Equals(rightValue);
        }

        public static implicit operator ColumnValue(string value)
        {
            return new ColumnValue(value, ColumnType.String);
        }

        public static explicit operator string(ColumnValue value)
        {
            if (value.ValueType != ColumnType.String)
            {
                throw new InvalidCastException(string.Format(CultureInfo.CurrentUICulture, OtsExceptions.ValueCastInvalidTypeFormat, new object[] { "String" }));
            }
            return value.Value;
        }

        public static implicit operator ColumnValue(bool value)
        {
            return new ColumnValue(value.ToString(CultureInfo.InvariantCulture), ColumnType.Boolean);
        }

        public static explicit operator bool(ColumnValue value)
        {
            bool result;
            if (value.ValueType != ColumnType.Boolean)
            {
                throw new InvalidCastException(string.Format(CultureInfo.CurrentUICulture, OtsExceptions.ValueCastInvalidTypeFormat, new object[] { "Boolean" }));
            }
            if (!bool.TryParse(value.Value, out result))
            {
                throw new InvalidCastException(string.Format(CultureInfo.CurrentUICulture, OtsExceptions.ValueCastFailedFormat, new object[] { "Boolean" }));
            }
            return result;
        }

        public static implicit operator ColumnValue(long value)
        {
            return new ColumnValue(value.ToString(CultureInfo.InvariantCulture), ColumnType.Integer);
        }

        public static explicit operator long(ColumnValue value)
        {
            long result;
            if (value.ValueType != ColumnType.Integer)
            {
                throw new InvalidCastException(string.Format(CultureInfo.CurrentUICulture, OtsExceptions.ValueCastInvalidTypeFormat, new object[] { "Integer" }));
            }
            if (!long.TryParse(value.Value, NumberStyles.Integer, CultureInfo.InvariantCulture, out result))
            {
                throw new InvalidCastException(string.Format(CultureInfo.CurrentUICulture, OtsExceptions.ValueCastFailedFormat, new object[] { "Int64" }));
            }
            return result;
        }

        public static implicit operator ColumnValue(double value)
        {
            if (double.IsNaN(value))
            {
                throw new InvalidCastException(OtsExceptions.CannotCastDoubleNaN);
            }
            return new ColumnValue(value.ToString("R", CultureInfo.InvariantCulture), ColumnType.Double);
        }

        public static explicit operator double(ColumnValue value)
        {
            double temp;
            if (value.ValueType != ColumnType.Double)
            {
                throw new InvalidCastException(string.Format(CultureInfo.CurrentUICulture, OtsExceptions.ValueCastInvalidTypeFormat, new object[] { "Double" }));
            }
            try
            {
                temp = Convert.ToDouble(value.Value, CultureInfo.InvariantCulture);
            }
            catch (OverflowException)
            {
                if (value.Value.StartsWith("-", StringComparison.OrdinalIgnoreCase))
                {
                    return double.MinValue;
                }
                temp = double.MaxValue;
            }
            catch (FormatException e)
            {
                throw new InvalidCastException(string.Format(CultureInfo.CurrentUICulture, OtsExceptions.ValueCastFailedFormat, new object[] { "Int64" }), e);
            }
            return temp;
        }
    }
}

