using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SmartAdmin.Generator.Models
{
    public sealed class TableMapper
    {
        public TableMapper() { }
        public string TableName { get; set; }
        public List<ColumnMapper> CollectionColumn { get; set; }
        //public List<TableRelationship> CollectionRelationship { get; set; }
        
    }

    public sealed class ColumnMapper
    {
        public string ColumnName { get; set; }
        public string IsNullable { get; set; }
        public string DataType { get; set; }
        public string MaxLenght { get; set; }
        public string ColumnKey { get; set; }
    }

    public sealed class TableRelationship
    {
        public string KeyType { get; set; }
        public string TablePK { get; set; }
        public string ColumnPK { get; set; }
        public string TableFK { get; set; }
        public string ColumnFK { get; set; }
    }
}
