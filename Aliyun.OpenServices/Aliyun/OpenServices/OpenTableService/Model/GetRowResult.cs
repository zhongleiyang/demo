namespace Aliyun.OpenServices.OpenTableService.Model
{
    using Aliyun.OpenServices.OpenTableService;
    using System;
    using System.Collections.Generic;
    using System.Runtime.CompilerServices;
    using System.Xml.Serialization;
    using System.Linq;

    public class GetRowResult : OpenTableServiceResult
    {
        public IEnumerable<Row> GetMultipleRows()
        {
            if (this.Rows == null)
            {
                return new List<Row>();
            }
            return (from r in this.Rows select r.ToRow());
        }

        public Row GetSingleRow()
        {
            if (((this.Rows != null) && (this.Rows.Count != 0)) && ((this.Rows.Count != 1) || (this.Rows[0].Columns.Count != 0)))
            {
                return this.Rows[0].ToRow();
            }
            return null;
        }

        [XmlArrayItem("Row"), XmlArray("Table")]
        public List<InternalTableRow> Rows { get; set; }
    }
}

