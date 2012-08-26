namespace Aliyun.OpenServices.OpenTableService.Model
{
    using Aliyun.OpenServices.OpenTableService;
    using Aliyun.OpenServices.OpenTableService.Utilities;
    using System;
    using System.Collections.Generic;
    using System.Globalization;
    using System.Runtime.CompilerServices;
    using System.Text;
    using System.Xml.Serialization;

    [XmlRoot("Row")]
    public class InternalTableRow
    {
        public Row ToRow()
        {
            Row row = new Row();
            foreach (InternalTableColumn col in this.Columns)
            {
                string decodedValue = null;
                if (!string.IsNullOrEmpty(col.Value.Encoding))
                {
                    string temp;
                    if (((temp = col.Value.Encoding) != null) && (temp == "Base64"))
                    {
                        byte[] bytes = Convert.FromBase64String(col.Value.Value);
                        try
                        {
                            decodedValue = OtsUtility.DataEncoding.GetString(bytes);
                            goto Label_00BB;
                        }
                        catch (DecoderFallbackException)
                        {
                            throw ExceptionFactory.CreateInvalidResponseException(null, OtsExceptions.ColumnValueCannotBeDecoded, null);
                        }
                    }
                    throw ExceptionFactory.CreateInvalidResponseException(null, string.Format(CultureInfo.CurrentUICulture, OtsExceptions.UnsupportedEncodingFormat, new object[] { col.Value.Encoding }), null);
                }
                decodedValue = col.Value.Value;
            Label_00BB:
                row.Columns[col.Name] = new ColumnValue(decodedValue, ColumnTypeHelper.Parse(col.Value.ColumnType));
            }
            return row;
        }

        [XmlElement("Column")]
        public List<InternalTableColumn> Columns { get; set; }
    }
}

