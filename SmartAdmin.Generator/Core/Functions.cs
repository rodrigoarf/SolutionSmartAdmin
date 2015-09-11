using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SmartAdmin.Generator.Models;
using CodeGenerator.Connections;
using System.Configuration;
using System.Data;

namespace SmartAdmin.Generator.Core
{
    public class Functions
    {
        private static string MySqlDataBase = ConfigurationManager.AppSettings["MySqlDataBase"].ToString();

        public static TableMapper GetTableSchema(string TableName, EDataBase DataBaseType)
        {
            try
            {
                var Retorno = new TableMapper();
                var Query = String.Empty;

                switch (DataBaseType)
                {
                    case EDataBase.MySql:
                        Retorno = LoadMySql(TableName);
                        break;
                    case EDataBase.SqlServer:
                        Retorno = LoadSQLServer(TableName);
                        break;
                    case EDataBase.Oracle:
                        Retorno = null;
                        break;
                }

                return (Retorno);
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
         
        private static TableMapper LoadSQLServer(String TableName)
        {
            var TableMapper = new TableMapper();

            var Query = "SELECT " +
                                "UPPER(A.COLUMN_NAME) COLUMN_NAME, " +
                                "LOWER(A.IS_NULLABLE) IS_NULLABLE, " +
                                "LOWER(A.DATA_TYPE) DATA_TYPE, " +
                                "LOWER(A.CHARACTER_MAXIMUM_LENGTH) MAXIMUM_LENGTH, " +
                                "B.COLUMN_KEY " +
                        "FROM " +
                                "INFORMATION_SCHEMA.COLUMNS A LEFT JOIN " +
                                "(SELECT UPPER(KCU.TABLE_NAME) AS TABLE_NAME, " +
                                "      UPPER(KCU.COLUMN_NAME) AS COLUMN_NAME, " +
                                "      CASE WHEN LOWER(TC.CONSTRAINT_TYPE) = 'PRIMARY KEY' THEN 'pk' " +
                                "           WHEN LOWER(TC.CONSTRAINT_TYPE) = 'FOREIGN KEY' THEN 'fk' " +
                                "           WHEN LOWER(TC.CONSTRAINT_TYPE) = 'UNIQUE' THEN NULL " +
                                "           WHEN LOWER(TC.CONSTRAINT_TYPE) = NULL THEN NULL " +
                                "      END AS COLUMN_KEY " +
                                " FROM INFORMATION_SCHEMA.KEY_COLUMN_USAGE KCU " +
                                "      LEFT JOIN INFORMATION_SCHEMA.TABLE_CONSTRAINTS TC " +
                                "      ON KCU.TABLE_NAME = TC.TABLE_NAME " +
                                "      AND KCU.TABLE_SCHEMA = TC.TABLE_SCHEMA " +
                                "      AND KCU.TABLE_CATALOG = TC.TABLE_CATALOG " +
                                "      AND KCU.CONSTRAINT_CATALOG = TC.CONSTRAINT_CATALOG " +
                                "      AND KCU.CONSTRAINT_NAME = TC.CONSTRAINT_NAME " +
                                "      LEFT JOIN INFORMATION_SCHEMA.REFERENTIAL_CONSTRAINTS RC " +
                                "      ON RC.CONSTRAINT_SCHEMA = TC.CONSTRAINT_SCHEMA " +
                                "      AND RC.CONSTRAINT_CATALOG = TC.CONSTRAINT_CATALOG " +
                                "      AND RC.CONSTRAINT_NAME = TC.CONSTRAINT_NAME " +
                                "      LEFT JOIN INFORMATION_SCHEMA.CONSTRAINT_COLUMN_USAGE CCU " +
                                "      ON RC.UNIQUE_CONSTRAINT_SCHEMA = CCU.CONSTRAINT_SCHEMA " +
                                "      AND RC.UNIQUE_CONSTRAINT_CATALOG = CCU.CONSTRAINT_CATALOG " +
                                "      AND RC.UNIQUE_CONSTRAINT_NAME = CCU.CONSTRAINT_NAME) B " +
                                "      ON A.TABLE_NAME = B.TABLE_NAME AND A.COLUMN_NAME = B.COLUMN_NAME " +
                        "WHERE " +
                                "A.TABLE_NAME = '" + TableName + "'";

            using (var Conexao = new ConnectSqlServer(Query))
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
           
        private static TableMapper LoadMySql(String TableName)
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
                                "A.TABLE_SCHEMA = '" + MySqlDataBase + "' AND A.TABLE_NAME = '" + TableName + "' " +
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

                        //--> Nova Implementação
                        //var Relation = new List<TableRelationship>();
                        //Relation.Add(GetRelationship(TableName, Column.ColumnName, ETypeFind.All));
                        //TableMapper.CollectionRelationship = Relation;
                    }

                    TableMapper.CollectionColumn = CollectionColumnMapper;
                }
            }
            return (TableMapper);
        }

        public static TableRelationship GetRelationship(string TableName, string ColumnName, ETypeFind TypeFind)
        {
            var Query = string.Format(@"SELECT * FROM
	                                        (SELECT CASE A.CONSTRAINT_NAME WHEN 'PRIMARY' THEN 'PK' ELSE 'FK' END AS KEY_TYPE,
		                                           UPPER(A.TABLE_NAME) AS TABLE_FK, 
		                                           UPPER(A.COLUMN_NAME) AS COLUMN_FK,
		                                           UPPER(A.REFERENCED_TABLE_NAME) AS TABLE_PK,
		                                           UPPER(A.REFERENCED_COLUMN_NAME) AS COLUMN_PK 
	                                          FROM 
		                                           INFORMATION_SCHEMA.KEY_COLUMN_USAGE A, 
		                                           INFORMATION_SCHEMA.TABLE_CONSTRAINTS B 
	                                         WHERE 
		                                           A.TABLE_SCHEMA = '{0}' AND
		                                           UPPER(A.TABLE_NAME) = UPPER(B.TABLE_NAME) AND 
		                                           UPPER(A.TABLE_SCHEMA) = UPPER(B.TABLE_SCHEMA) AND
		                                           UPPER(A.CONSTRAINT_NAME) = UPPER(B.CONSTRAINT_NAME)) AA 
                                                   WHERE 1=1 ", MySqlDataBase);

            switch (TypeFind)
            {
                case ETypeFind.PK:
                    Query += string.Format(@" AND AA.TABLE_PK = '{0}' AND AA.COLUMN_PK = '{1}' AND AA.KEY_TYPE = 'PK'", TableName, ColumnName);
                    break;
                case ETypeFind.FK:
                    Query += string.Format(@" AND AA.TABLE_FK = '{0}' AND AA.COLUMN_FK = '{1}' AND AA.KEY_TYPE = 'FK'", TableName, ColumnName);
                    break;
            }

            var Relation = new TableRelationship();
            using (var Conexao = new ConnectMySql(Query))
            {
                if (Conexao.Open())
                {
                    while (Conexao.LerRegistro())
                    {        
                        Relation.KeyType = Conexao.Ler("KEY_TYPE").ToString();
                        Relation.TableFK = Conexao.Ler("TABLE_FK").ToString();
                        Relation.ColumnFK = Conexao.Ler("COLUMN_FK").ToString();
                        Relation.TablePK = Conexao.Ler("TABLE_PK").ToString();
                        Relation.ColumnPK = Conexao.Ler("COLUMN_PK").ToString();
                    }
                }
            }  
            return (Relation);
        } 
    }

    public enum EDataBase
    {
        MySql,
        SqlServer,
        Oracle
    }

    public enum ETypeFind
    {
        PK,
        FK,
        All
    }  

    public enum ERelationship
    {
        OneToOne,
        OneToMany,
        ManyToMany
    }   

}
