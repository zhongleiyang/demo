namespace Aliyun.OpenServices.OpenTableService.Model
{
    using Aliyun.OpenServices.OpenTableService;
    using Aliyun.OpenServices.OpenTableService.Utilities;
    using System;
    using System.Collections.Generic;
    using System.Runtime.CompilerServices;
    using System.Xml.Serialization;

    [XmlRoot(ElementName="TableMeta")]
    public class InternalTableMeta
    {
        public InternalTableMeta()
        {
            this.PrimaryKeys = new List<PrimaryKey>();
            this.Views = new List<InternalViewMeta>();
        }

        internal TableMeta ToTableMeta()
        {
            TableMeta tableMeta = new TableMeta(this.TableName) {
                PagingKeyLength = this.PagingKeyLen,
                TableGroupName = this.TableGroupName
            };
            foreach (PrimaryKey pk in this.PrimaryKeys)
            {
                tableMeta.PrimaryKeys.Add(pk.Name, PrimaryKeyTypeHelper.Parse(pk.PrimaryKeyType));
            }
            foreach (InternalViewMeta view in this.Views)
            {
                tableMeta.Views.Add(view.ToOpenTableViewMeta());
            }
            return tableMeta;
        }

        public int PagingKeyLen { get; set; }

        [XmlElement(ElementName="PrimaryKey")]
        public List<PrimaryKey> PrimaryKeys { get; set; }

        public string TableGroupName { get; set; }

        public string TableName { get; set; }

        [XmlElement(ElementName="View")]
        public List<InternalViewMeta> Views { get; set; }
    }
}

