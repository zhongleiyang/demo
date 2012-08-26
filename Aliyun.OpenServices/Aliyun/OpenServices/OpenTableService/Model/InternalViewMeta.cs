namespace Aliyun.OpenServices.OpenTableService.Model
{
    using Aliyun.OpenServices.OpenTableService;
    using Aliyun.OpenServices.OpenTableService.Utilities;
    using System;
    using System.Collections.Generic;
    using System.Runtime.CompilerServices;
    using System.Xml.Serialization;

    [XmlRoot(ElementName="View")]
    public class InternalViewMeta
    {
        public InternalViewMeta()
        {
            this.PrimaryKeys = new List<PrimaryKey>();
            this.Columns = new List<ViewColumn>();
        }

        internal ViewMeta ToOpenTableViewMeta()
        {
            ViewMeta viewMeta = new ViewMeta(this.Name) {
                PagingKeyLength = this.PagingKeyLen
            };
            foreach (PrimaryKey pk in this.PrimaryKeys)
            {
                viewMeta.PrimaryKeys.Add(pk.Name, PrimaryKeyTypeHelper.Parse(pk.PrimaryKeyType));
            }
            foreach (ViewColumn col in this.Columns)
            {
                viewMeta.AttributeColumns.Add(col.Name, ColumnTypeHelper.Parse(col.ColumnType));
            }
            return viewMeta;
        }

        [XmlElement(ElementName="Column")]
        public List<ViewColumn> Columns { get; set; }

        public string Name { get; set; }

        public int PagingKeyLen { get; set; }

        [XmlElement(ElementName="PrimaryKey")]
        public List<PrimaryKey> PrimaryKeys { get; set; }
    }
}

