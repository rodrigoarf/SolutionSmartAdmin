using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SmartAdmin.Generator.Models;
using CodeGenerator.Connections;

namespace SmartAdmin.Generator.Core
{
    public class Functions
    {
        public static TableMapper GetTableSchema(string TableName, EDataBase DataBaseType)
        {
            try
            {
                var TableMapper = new TableMapper();

                var Query = "SELECT " +
                                     "UPPER(COLUMN_NAME) COLUMN_NAME," +
                                     "LOWER(IS_NULLABLE) IS_NULLABLE," +
                                     "LOWER(DATA_TYPE) DATA_TYPE," +
                                     "LOWER(CHARACTER_MAXIMUM_LENGTH) MAXIMUM_LENGTH, " +
                                     "CASE" +
                                     "   WHEN LOWER(COLUMN_KEY) = 'pri' THEN 'pk' " +
                                     "   WHEN LOWER(COLUMN_KEY) = 'mul' THEN 'fk' " +
                                     "   WHEN LOWER(COLUMN_KEY) = NULL THEN NULL " +
                                     "END AS COLUMN_KEY " +
                            "FROM " +
                                    "INFORMATION_SCHEMA.COLUMNS A " +
                            "WHERE " +
                                    "A.TABLE_SCHEMA = 'cliniccenter' AND A.TABLE_NAME = '" + TableName + "' " +
                            "ORDER BY " +
                                    "A.ORDINAL_POSITION ASC";

                using (var Conexao = new ConnectMySql(Query))
                {
                    if (Conexao.Open())
                    {
                        TableMapper.TableName = TableName;
                        var CollectionColumnMapper = new List<ColumnMapper>();

                        while (Conexao.LerRegistro())
                        {
                            var Column = new ColumnMapper();

                            Column.ColumnName = Conexao.Ler("COLUMN_NAME").ToString();
                            Column.IsNullable = Conexao.Ler("IS_NULLABLE").ToString();
                            Column.DataType = Conexao.Ler("DATA_TYPE").ToString();
                            Column.MaxLenght = Conexao.Ler("MAXIMUM_LENGTH").ToString();
                            Column.ColumnKey = Conexao.Ler("COLUMN_KEY").ToString();

                            CollectionColumnMapper.Add(Column);
                        }

                        TableMapper.CollectionColumn = CollectionColumnMapper;
                    }
                }

                return (TableMapper);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public static String GetColumnType(string ColumnDataType)
        {
            var DataType = String.Empty;

            if ((ColumnDataType == "boolean") || (ColumnDataType == "bit"))
                DataType = "System.Boolean";

            if ((ColumnDataType == "tinyint") || (ColumnDataType == "unsigned"))
                DataType = "System.Byte";

            if ((ColumnDataType == "binary") || (ColumnDataType == "varbinary") || (ColumnDataType == "blob") || (ColumnDataType == "longblob"))
                DataType = "System.Byte[]";

            if ((ColumnDataType == "datetime"))
                DataType = "System.DateTime";

            if ((ColumnDataType == "decimal"))
                DataType = "System.Decimal";

            if ((ColumnDataType == "double"))
                DataType = "System.Double";

            if ((ColumnDataType == "char"))
                DataType = "System.Guid";

            if ((ColumnDataType == "smallint"))
                DataType = "System.Int16";

            if ((ColumnDataType == "int"))
                DataType = "System.Int32";

            if ((ColumnDataType == "bigint"))
                DataType = "System.Int64";

            if ((ColumnDataType == "tinyint"))
                DataType = "System.SByte";

            if ((ColumnDataType == "float"))
                DataType = "System.Single";

            if ((ColumnDataType == "char") || (ColumnDataType == "varchar") || (ColumnDataType == "text") || (ColumnDataType == "longtext"))
                DataType = "System.String";

            if ((ColumnDataType == "time"))
                DataType = "System.TimeSpan";

            return (DataType);
        }
    }

    public enum EDataBase
    {
        MySql,
        SqlServer,
        Oracle
    }
}
