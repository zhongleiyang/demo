namespace Aliyun.OpenServices.OpenTableService.Utilities
{
    using Aliyun.OpenServices.OpenTableService;
    using System;
    using System.Runtime.CompilerServices;

    internal static class TableValueExtension
    {
        internal static string ToParameterString(this ColumnValue value)
        {
            switch (value.ValueType)
            {
                case ColumnType.String:
                    return ("'" + value.ToString() + "'");

                case ColumnType.Boolean:
                    return value.Value.ToUpperInvariant();
            }
            return value.Value;
        }

        internal static string ToParameterString(this PartitionKeyValue value)
        {
            if (value.ValueType == PartitionKeyType.String)
            {
                return ("'" + value.ToString() + "'");
            }
            return value.Value;
        }

        internal static string ToParameterString(this PrimaryKeyValue value)
        {
            switch (value.ValueType)
            {
                case PrimaryKeyType.String:
                    if (!value.IsInf)
                    {
                        return ("'" + value.ToString() + "'");
                    }
                    return value.ToString();

                case PrimaryKeyType.Boolean:
                    return value.Value.ToUpperInvariant();
            }
            return value.Value;
        }
    }
}

