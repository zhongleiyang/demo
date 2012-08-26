namespace Aliyun.OpenServices.OpenTableService.Utilities
{
    using Aliyun.OpenServices.OpenTableService;
    using System;
    using System.Collections.Generic;
    using System.Diagnostics;
    using System.Text;
    using System.Text.RegularExpressions;

    internal static class OtsUtility
    {
        public static readonly Encoding DataEncoding = Encoding.UTF8;

        [DebuggerNonUserCode, Conditional("DEBUG")]
        public static void AssertColumnNames(IEnumerable<string> names)
        {
            using (IEnumerator<string> temp = names.GetEnumerator())
            {
                while (temp.MoveNext())
                {
                    string current = temp.Current;
                }
            }
        }

        public static string GetFullQueryTableName(RowQueryCriteria criteria)
        {
            string entityName = criteria.TableName;
            if (!string.IsNullOrEmpty(criteria.ViewName))
            {
                entityName = entityName + "." + criteria.ViewName;
            }
            return entityName;
        }

        public static bool IsEntityNameValid(string name)
        {
            string pattern = @"^[a-zA-Z_][\w]{0,99}$";
            Regex regex = new Regex(pattern);
            return regex.Match(name).Success;
        }
    }
}

