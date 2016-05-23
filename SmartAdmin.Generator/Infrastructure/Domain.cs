using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;     
using System.IO;
using SmartAdmin.Gerador.Enums;
using SmartAdmin.Gerador.Models;


namespace SmartAdmin.Gerador.Infrastructure
{
    public class Domain
    {
        private StringBuilder TextClass;
        private String FilePath = ConfigurationManager.AppSettings["CamadaDeDominio"].ToString();
        private String ProjectName = ConfigurationManager.AppSettings["NomeDoProjeto"].ToString();
        private EDataBase DatabaseType = SmartAdmin.Gerador.Program.DatabaseType;

        public string BuildModels(KeyValuePair<String, ModelConfig> TableSetting)
        {
            var TableName = TableSetting.Key;
            var DataModel = TableSetting.Value;
            var TableSchema = Utils.GetTableSchema(TableName, DatabaseType);
            var Sufixo = "Dto";

            //--  
            TextClass = new StringBuilder();       
            TextClass.AppendLine("using System;");
            TextClass.AppendLine("using System.Text;");
            TextClass.AppendLine("using System.Collections;");
            TextClass.AppendLine("using System.Collections.Generic;");
            TextClass.AppendLine("using System.Linq;");
            TextClass.AppendLine("using System.Threading.Tasks;");
            TextClass.AppendLine("using System.Linq.Expressions;");
            TextClass.AppendLine("using " + ProjectName + ".Data;");
            TextClass.AppendLine("using " + ProjectName + ".Data.Model;"); 
            TextClass.AppendLine("");
            TextClass.AppendLine("/// <summary>");
            TextClass.AppendLine("/// Esta classe abstrata não pode ser instanciada. O objetivo desta classe é fornecer uma definição de metodos");
            TextClass.AppendLine("/// base comuns para que várias outras classes derivadas desta possam compartilhar metodos por 'override'.");
            TextClass.AppendLine("/// </summary>");
            TextClass.AppendLine("namespace " + ProjectName + ".Domain.Model");
            TextClass.AppendLine("{");
            TextClass.AppendLine("    public abstract class " + DataModel.ClassName);
            TextClass.AppendLine("    {");
            TextClass.AppendLine("        public " + ProjectName + ".Data.UnitOfWork _unitOfWork = new " + ProjectName + ".Data.UnitOfWork();");
            TextClass.AppendLine("");
            TextClass.AppendLine("        /// <summary>");
            TextClass.AppendLine("        /// Salva um objeto<T>");
            TextClass.AppendLine("        /// </summary>");
            TextClass.AppendLine("        public virtual void Save(" + String.Concat(DataModel.ClassName, Sufixo) + " model)");
            TextClass.AppendLine("        {");
            TextClass.AppendLine("            _unitOfWork." + DataModel.ClassName + "Repository.Add(model);");
            TextClass.AppendLine("        }");
            TextClass.AppendLine("");
            TextClass.AppendLine("        /// <summary>");
            TextClass.AppendLine("        /// Salva e retorna o objeto<T> salvo");
            TextClass.AppendLine("        /// </summary>");
            TextClass.AppendLine("        public virtual " + String.Concat(DataModel.ClassName, Sufixo) + " SaveGetItem(" + String.Concat(DataModel.ClassName, Sufixo) + " model)");
            TextClass.AppendLine("        {");
            TextClass.AppendLine("           var retorno = _unitOfWork." + DataModel.ClassName + "Repository.AddGetItem(model);");
            TextClass.AppendLine("           return (retorno);");
            TextClass.AppendLine("        }");
            TextClass.AppendLine("");
            TextClass.AppendLine("        /// <summary>");
            TextClass.AppendLine("        /// Salva uma lista de objetos List<T>");
            TextClass.AppendLine("        /// </summary>");
            TextClass.AppendLine("        public virtual void SaveAll(List<" + String.Concat(DataModel.ClassName, Sufixo) + "> model)");
            TextClass.AppendLine("        {");
            TextClass.AppendLine("            _unitOfWork." + DataModel.ClassName + "Repository.AddAll(model);");
            TextClass.AppendLine("        }");
            TextClass.AppendLine("");
            TextClass.AppendLine("        /// <summary>");
            TextClass.AppendLine("        /// Salva a edição de um objeto<T>");
            TextClass.AppendLine("        /// </summary>");
            TextClass.AppendLine("        public virtual void Edit(" + String.Concat(DataModel.ClassName, Sufixo) + " model)");
            TextClass.AppendLine("        {");
            TextClass.AppendLine("            _unitOfWork." + DataModel.ClassName + "Repository.Edit(model);");
            TextClass.AppendLine("        }");
            TextClass.AppendLine("");
            TextClass.AppendLine("        /// <summary>");
            TextClass.AppendLine("        /// Retorna um único objeto<T> buscado por expressão Lambda");
            TextClass.AppendLine("        /// </summary>");
            TextClass.AppendLine("        public virtual " + String.Concat(DataModel.ClassName, Sufixo) + " GetItem(Expression<Func<" + String.Concat(DataModel.ClassName, Sufixo) + ", bool>> filter)");
            TextClass.AppendLine("        {");
            TextClass.AppendLine("            " + String.Concat(DataModel.ClassName, Sufixo) + " model;");
            TextClass.AppendLine("            model = _unitOfWork." + DataModel.ClassName + "Repository.GetItem(filter);");
            TextClass.AppendLine("            return (model);");
            TextClass.AppendLine("        }");
            TextClass.AppendLine("");
            TextClass.AppendLine("        /// <summary>");
            TextClass.AppendLine("        /// Deleta um objeto");
            TextClass.AppendLine("        /// </summary>");
            TextClass.AppendLine("        public virtual void Delete(Expression<Func<" + String.Concat(DataModel.ClassName, Sufixo) + ", bool>> filter)");
            TextClass.AppendLine("        {");
            TextClass.AppendLine("            var Collection = _unitOfWork." + DataModel.ClassName + "Repository.GetByFilter(filter);");
            TextClass.AppendLine("            if (Collection.ToList().Count > 0)");
            TextClass.AppendLine("            {");
            TextClass.AppendLine("                foreach (var item in Collection)");
            TextClass.AppendLine("                {");
            TextClass.AppendLine("                    _unitOfWork." + DataModel.ClassName + "Repository.Delete(item);");
            TextClass.AppendLine("                }");
            TextClass.AppendLine("            }");
            TextClass.AppendLine("        }");
            TextClass.AppendLine("");
            TextClass.AppendLine("        /// <summary>");
            TextClass.AppendLine("        /// Deleta uma lista de objetos");
            TextClass.AppendLine("        /// </summary>");
            TextClass.AppendLine("        public virtual void DeleteAll(List<" + String.Concat(DataModel.ClassName, Sufixo) + "> collection)");
            TextClass.AppendLine("        {");
            TextClass.AppendLine("            foreach (var item in collection) { _unitOfWork." + DataModel.ClassName + "Repository.Delete(item); }");
            TextClass.AppendLine("        }");
            TextClass.AppendLine("");
            TextClass.AppendLine("        /// <summary>");
            TextClass.AppendLine("        /// Retorna uma lista List(T) de objetos buscados pela expressão Lambda");
            TextClass.AppendLine("        /// </summary>");
            TextClass.AppendLine("        public virtual List<" + String.Concat(DataModel.ClassName, Sufixo) + "> GetList(Expression<Func<" + String.Concat(DataModel.ClassName, Sufixo) + ", bool>> filter)");
            TextClass.AppendLine("        {");
            TextClass.AppendLine("            List<" + String.Concat(DataModel.ClassName, Sufixo) + "> collection;");
            TextClass.AppendLine("            collection = _unitOfWork." + DataModel.ClassName + "Repository.GetByFilter(filter).ToList();");
            TextClass.AppendLine("            return (collection);");  
            TextClass.AppendLine("        }");
            TextClass.AppendLine("");  
            TextClass.AppendLine("        /// <summary>");
            TextClass.AppendLine("        /// Retorna uma lista IQueryable(T) de objetos buscados pela expressão Lambda");
            TextClass.AppendLine("        /// </summary>");
            TextClass.AppendLine("        public virtual IQueryable<" + String.Concat(DataModel.ClassName, Sufixo) + "> GetByFilter(Expression<Func<" + String.Concat(DataModel.ClassName, Sufixo) + ", bool>> filter)");
            TextClass.AppendLine("        {");
            TextClass.AppendLine("            var collection = _unitOfWork." + DataModel.ClassName + "Repository.GetByFilter(filter);");
            TextClass.AppendLine("            return (collection);");
            TextClass.AppendLine("        }");
            TextClass.AppendLine(""); 

            foreach (var ColumnMapper in TableSchema.CollectionColumn)
            {
                if (ColumnMapper.ColumnName == "STATUS")
                {
                    TextClass.AppendLine("        /// <summary>");
                    TextClass.AppendLine("        /// Inativa um objeto para visualização");
                    TextClass.AppendLine("        /// </summary>");
                    TextClass.AppendLine("        public virtual void ToInactive(int Id)");
                    TextClass.AppendLine("        {");
                    TextClass.AppendLine("            " + String.Concat(DataModel.ClassName, Sufixo) + " model = _unitOfWork." + DataModel.ClassName + "Repository.GetItem(_ => _.ID == Id && _.STATUS == \"A\");");
                    TextClass.AppendLine("            model.STATUS = \"I\";");
                    TextClass.AppendLine("            _unitOfWork." + DataModel.ClassName + "Repository.Edit(model);");
                    TextClass.AppendLine("        }");
                    TextClass.AppendLine("");
                    TextClass.AppendLine("        /// <summary>");
                    TextClass.AppendLine("        /// Ativa um objeto para visualização");
                    TextClass.AppendLine("        /// </summary>");
                    TextClass.AppendLine("        public virtual void ToActive(int Id)");
                    TextClass.AppendLine("        {");
                    TextClass.AppendLine("            " + String.Concat(DataModel.ClassName, Sufixo) + " model = _unitOfWork." + DataModel.ClassName + "Repository.GetItem(_ => _.ID == Id && _.STATUS == \"I\");");
                    TextClass.AppendLine("            model.STATUS = \"A\";");
                    TextClass.AppendLine("            _unitOfWork." + DataModel.ClassName + "Repository.Edit(model);");
                    TextClass.AppendLine("        }");
                    TextClass.AppendLine("");
                }
            }   

            TextClass.AppendLine("    }");
            TextClass.AppendLine("}");
            //--  

            return CreateFile(DataModel.ClassName);
        }

        public String BuildFilters(KeyValuePair<String, ModelConfig> TableSetting)
        {
            var TableName = TableSetting.Key;
            var DataModel = TableSetting.Value;
            var TableSchema = Utils.GetTableSchema(TableName, DatabaseType);
            var Sufixo = "Filter";
            var ColumnDataType = String.Empty;

            TextClass = new StringBuilder();
            TextClass.AppendLine("using System;");
            TextClass.AppendLine("using System.Linq;");
            TextClass.AppendLine("using System.Text;");
            TextClass.AppendLine("using System.Collections.Generic;");
            TextClass.AppendLine("using System.Threading.Tasks;");
            TextClass.AppendLine("");
            TextClass.AppendLine("namespace " + ProjectName + ".Domain.Filters");
            TextClass.AppendLine("{");
            TextClass.AppendLine("    public class " + String.Concat(DataModel.ClassName, Sufixo) + " : Base");
            TextClass.AppendLine("    {");

            foreach (var ColumnMapper in TableSchema.CollectionColumn)
            {
                if (ColumnMapper.ColumnKey != "pk")
                {
                    if (ColumnMapper.DataType == "datetime")
                    {
                        if (ColumnMapper.IsNullable == "yes")
                        {
                            ColumnDataType = Utils.GetColumnType(ColumnMapper.DataType);
                            TextClass.AppendLine("        public Nullable<System.DateTime> " + ColumnMapper.ColumnName + " { get; set; }");
                        }
                        else
                        {
                            ColumnDataType = Utils.GetColumnType(ColumnMapper.DataType);
                            TextClass.AppendLine("        public " + ColumnDataType + " " + ColumnMapper.ColumnName + " { get; set; }");
                        }
                    }
                    else if (ColumnMapper.DataType == "int")
                    {
                        if (ColumnMapper.IsNullable == "yes")
                        {
                            ColumnDataType = Utils.GetColumnType(ColumnMapper.DataType);
                            TextClass.AppendLine("        public Nullable<System.Int32> " + ColumnMapper.ColumnName + " { get; set; }");
                        }
                        else
                        {
                            ColumnDataType = Utils.GetColumnType(ColumnMapper.DataType);
                            TextClass.AppendLine("        public " + ColumnDataType + " " + ColumnMapper.ColumnName + " { get; set; }");
                        }
                    }
                    else if (ColumnMapper.DataType == "decimal")
                    {
                        if (ColumnMapper.IsNullable == "yes")
                        {
                            ColumnDataType = Utils.GetColumnType(ColumnMapper.DataType);
                            TextClass.AppendLine("        public Nullable<System.Decimal> " + ColumnMapper.ColumnName + " { get; set; }");
                        }
                        else
                        {
                            ColumnDataType = Utils.GetColumnType(ColumnMapper.DataType);
                            TextClass.AppendLine("        public " + ColumnDataType + " " + ColumnMapper.ColumnName + " { get; set; }");
                        }
                    }
                    else if (ColumnMapper.DataType == "long")
                    {
                        if (ColumnMapper.IsNullable == "yes")
                        {
                            ColumnDataType = Utils.GetColumnType(ColumnMapper.DataType);
                            TextClass.AppendLine("        public Nullable<System.long> " + ColumnMapper.ColumnName + " { get; set; }");
                        }
                        else
                        {
                            ColumnDataType = Utils.GetColumnType(ColumnMapper.DataType);
                            TextClass.AppendLine("        public " + ColumnDataType + " " + ColumnMapper.ColumnName + " { get; set; }");
                        }
                    }
                    else if (ColumnMapper.DataType == "longblob")
                    {
                        if (ColumnMapper.IsNullable == "yes")
                        {
                            ColumnDataType = Utils.GetColumnType(ColumnMapper.DataType);
                            TextClass.AppendLine("        public Nullable<System.Byte> " + ColumnMapper.ColumnName + " { get; set; }");
                        }
                        else
                        {
                            ColumnDataType = Utils.GetColumnType(ColumnMapper.DataType);
                            TextClass.AppendLine("        public " + ColumnDataType + " " + ColumnMapper.ColumnName + " { get; set; }");
                        }
                    }
                    else
                    {
                        ColumnDataType = Utils.GetColumnType(ColumnMapper.DataType);
                        TextClass.AppendLine("        public " + ColumnDataType + " " + ColumnMapper.ColumnName + " { get; set; }");
                    }
                }
            }

            TextClass.AppendLine("    }");
            TextClass.AppendLine("}");

            return CreateFile(String.Concat(DataModel.ClassName, Sufixo), "Filters");
        }

        public String BuildModelsSpecialized(String ClassName)
        {
            var Sufixo = "Specialized";

            TextClass = new StringBuilder();
            TextClass.AppendLine("using System;");
            TextClass.AppendLine("using System.Text;");
            TextClass.AppendLine("using System.Collections;");
            TextClass.AppendLine("using System.Collections.Generic;");
            TextClass.AppendLine("using System.Linq;");
            TextClass.AppendLine("using System.Threading.Tasks;");
            TextClass.AppendLine("using System.Linq.Expressions;");
            TextClass.AppendLine("using " + ProjectName + ".Data;");
            TextClass.AppendLine("using " + ProjectName + ".Data.Model;");
            TextClass.AppendLine("");
            TextClass.AppendLine("namespace " + ProjectName + ".Domain.Model");
            TextClass.AppendLine("{");
            TextClass.AppendLine("    public partial class " + String.Concat(ClassName, Sufixo) + " : " + ClassName);
            TextClass.AppendLine("    {");
            TextClass.AppendLine("");
            TextClass.AppendLine("    }");
            TextClass.AppendLine("}");

            var FileName = String.Format(@"{0}.{1}", String.Concat(ClassName, Sufixo), "cs");
            var Diretory = String.Format(@"{0}\{1}\{2}", FilePath, "Models", ClassName);
            var FullFile = String.Format(@"{0}\{1}", Diretory, FileName);

            if (!File.Exists(FullFile))
            {
                using (System.IO.TextWriter Writer = File.CreateText(FullFile))
                {
                    Writer.WriteLine(TextClass.ToString());
                }
            }

            return FileName;
        }

        public String BuildBaseAnnotations()
        {
            TextClass = new StringBuilder();
            TextClass.AppendLine("using System;");
            TextClass.AppendLine("using System.Linq;");
            TextClass.AppendLine("using System.Text;");
            TextClass.AppendLine("using System.Collections.Generic;");
            TextClass.AppendLine("using System.Threading.Tasks;");
            TextClass.AppendLine("");
            TextClass.AppendLine("namespace " + ProjectName + ".Domain.Annotations");
            TextClass.AppendLine("{");
            TextClass.AppendLine("    [Serializable]");
            TextClass.AppendLine("    public class Base");
            TextClass.AppendLine("    {");
            TextClass.AppendLine("        public Int32 ID { get; set; }");
            TextClass.AppendLine("    }");
            TextClass.AppendLine("}");

            return CreateFile("Base", "Annotations");
        }

        public String BuildModelsBaseAnnotations(KeyValuePair<String, ModelConfig> TableSetting)
        {
            var TableName = TableSetting.Key;
            var DataModel = TableSetting.Value;
            var TableSchema = Utils.GetTableSchema(TableName, DatabaseType);

            TextClass = new StringBuilder();
            TextClass.AppendLine("using System;");
            TextClass.AppendLine("using System.Text;");
            TextClass.AppendLine("using System.Collections;");
            TextClass.AppendLine("using System.Collections.Generic;");
            TextClass.AppendLine("using System.Linq;");
            TextClass.AppendLine("using System.ComponentModel.DataAnnotations;");
            TextClass.AppendLine("using System.Threading.Tasks;");
            TextClass.AppendLine("");
            TextClass.AppendLine("namespace " + ProjectName + ".Domain.Annotations");
            TextClass.AppendLine("{");
            TextClass.AppendLine("    [Serializable]");
            TextClass.AppendLine("    public abstract class " + String.Concat(DataModel.ClassName) + " : Base");
            TextClass.AppendLine("    {");

            var ColumnDataType = String.Empty;

            foreach (var ColumnMapper in TableSchema.CollectionColumn)
            {
                if (ColumnMapper.ColumnKey != "pk")
                {
                    ColumnDataType = Utils.GetColumnType(ColumnMapper.DataType);

                    if (ColumnMapper.IsNullable == "no")
                    {
                        TextClass.AppendLine("        [Required(ErrorMessage = \"Campo " + ColumnMapper.ColumnName + " é obrigatório.\")]");

                        if (ColumnMapper.MaxLenght != String.Empty)
                        {
                            // tipos mysql e alguns do oracle
                            if ((ColumnMapper.DataType == "text") || (ColumnMapper.DataType == "longtext") || (ColumnMapper.DataType == "tinytext") || (ColumnMapper.DataType == "mediumtext"))
                            {
                                TextClass.AppendLine("        [StringLength(65535, ErrorMessage = \"A descrição do campo " + ColumnMapper.ColumnName + " deve ter no máximo 65535 caracteres.\")]");
                            }
                            else
                            {
                                TextClass.AppendLine("        [StringLength(" + ColumnMapper.MaxLenght + ", ErrorMessage = \"A descrição do campo " + ColumnMapper.ColumnName + " deve ter no máximo " + ColumnMapper.MaxLenght + " caracteres.\")]");
                            }
                        }

                        TextClass.AppendLine("        public virtual " + ColumnDataType + " " + ColumnMapper.ColumnName + " { get; set; }");
                        TextClass.AppendLine("");
                    }
                    else if (ColumnMapper.IsNullable == "yes")
                    {
                        if (ColumnMapper.DataType == "datetime") 
                        {
                            ColumnDataType = Utils.GetColumnType(ColumnMapper.DataType);
                            TextClass.AppendLine("        public virtual Nullable<System.DateTime> " + ColumnMapper.ColumnName + " { get; set; }");
                        }
                        else if (ColumnMapper.DataType == "int")
                        {
                            ColumnDataType = Utils.GetColumnType(ColumnMapper.DataType);
                            TextClass.AppendLine("        public Nullable<System.Int32> " + ColumnMapper.ColumnName + " { get; set; }");
                        }
                        else if (ColumnMapper.DataType == "decimal")
                        {
                            ColumnDataType = Utils.GetColumnType(ColumnMapper.DataType);
                            TextClass.AppendLine("        public Nullable<System.Decimal> " + ColumnMapper.ColumnName + " { get; set; }");
                        }
                        else if (ColumnMapper.DataType == "long")
                        {
                            ColumnDataType = Utils.GetColumnType(ColumnMapper.DataType);
                            TextClass.AppendLine("        public Nullable<System.long> " + ColumnMapper.ColumnName + " { get; set; }");
                        }
                        else if (ColumnMapper.DataType == "longblob")
                        {
                            ColumnDataType = Utils.GetColumnType(ColumnMapper.DataType);
                            TextClass.AppendLine("        public Nullable<System.Byte> " + ColumnMapper.ColumnName + " { get; set; }");
                        }
                        else
                        {
                            ColumnDataType = Utils.GetColumnType(ColumnMapper.DataType);
                            TextClass.AppendLine("        public " + ColumnDataType + " " + ColumnMapper.ColumnName + " { get; set; }");
                        } 

                        //TextClass.AppendLine("        public virtual " + ColumnDataType + " " + ColumnMapper.ColumnName + " { get; set; }");
                        TextClass.AppendLine("");
                    }
                }
            }    

            TextClass.AppendLine("    }");
            TextClass.AppendLine("}");

            return CreateFile(String.Concat(DataModel.ClassName), "Annotations");    
        }

        public String BuildModelsAnnotations(KeyValuePair<String, ModelConfig> TableSetting)
        {
            var TableName = TableSetting.Key;
            var DataModel = TableSetting.Value;
            var TableSchema = Utils.GetTableSchema(TableName, DatabaseType);
            var Sufixo = "Annotations";

            TextClass = new StringBuilder();
            TextClass.AppendLine("using System;");
            TextClass.AppendLine("using System.Text;");
            TextClass.AppendLine("using System.Collections;");
            TextClass.AppendLine("using System.Collections.Generic;");
            TextClass.AppendLine("using System.Linq;");
            TextClass.AppendLine("using System.ComponentModel.DataAnnotations;");
            TextClass.AppendLine("using System.Threading.Tasks;");
            TextClass.AppendLine("");
            TextClass.AppendLine("namespace " + ProjectName + ".Domain.Annotations");
            TextClass.AppendLine("{");
            TextClass.AppendLine("    [Serializable]");
            TextClass.AppendLine("    public class " + String.Concat(DataModel.ClassName, Sufixo) + " : Base");
            TextClass.AppendLine("    {"); 
            TextClass.AppendLine("    }");
            TextClass.AppendLine("}");

            var FileName = String.Format(@"{0}.{1}", String.Concat(DataModel.ClassName, Sufixo), "cs");
            var Diretory = String.Format(@"{0}\{1}", FilePath, "Annotations");
            var FullFile = String.Format(@"{0}\{1}", Diretory, FileName);

            if (!File.Exists(FullFile))
            {
                using (System.IO.TextWriter Writer = File.CreateText(FullFile))
                {
                    Writer.WriteLine(TextClass.ToString());
                }
            }

            return FileName; 
        }

        public String BuildUnitOfWork(Dictionary<String, ModelConfig> GroupTables)
        {
            TextClass = new StringBuilder();
            TextClass.AppendLine("using System;");
            TextClass.AppendLine("using System.Collections.Generic;");
            TextClass.AppendLine("using System.Linq;");
            TextClass.AppendLine("using System.Text;");
            TextClass.AppendLine("using System.Threading.Tasks;");
            TextClass.AppendLine("using " + ProjectName + ".Domain.Model;");
            TextClass.AppendLine("");
            TextClass.AppendLine("namespace " + ProjectName + ".Domain");
            TextClass.AppendLine("{");
            TextClass.AppendLine("    public class UnitOfWork : IDisposable");
            TextClass.AppendLine("    {");
            TextClass.AppendLine("        private bool _Disposed = false;");
            TextClass.AppendLine("");

            foreach (var item in GroupTables)
            {
                var Model = item.Value;  
                TextClass.AppendLine("        private " + Model.ClassName + " _" + Model.ClassName.ToLower() + "Domain;");
            }

            TextClass.AppendLine("");

            foreach (var item in GroupTables)
            {
                var Model = item.Value;

                TextClass.AppendLine("        public " + Model.ClassName + " " + Model.ClassName + "Domain");
                TextClass.AppendLine("        {");
                TextClass.AppendLine("            get");
                TextClass.AppendLine("            {");
                TextClass.AppendLine("                if (this._" + Model.ClassName.ToLower() + "Domain == null)");
                TextClass.AppendLine("                {");
                TextClass.AppendLine("                    this._" + Model.ClassName.ToLower() + "Domain = new " + Model.ClassName + "Specialized();");
                TextClass.AppendLine("                }");
                TextClass.AppendLine("                return _" + Model.ClassName.ToLower() + "Domain;");
                TextClass.AppendLine("            }");
                TextClass.AppendLine("        }");
            }

            TextClass.AppendLine("");
            TextClass.AppendLine("        public void Dispose()");
            TextClass.AppendLine("        {");
            TextClass.AppendLine("            Clear(true);");
            TextClass.AppendLine("            GC.SuppressFinalize(this);");
            TextClass.AppendLine("        }");
            TextClass.AppendLine("");
            TextClass.AppendLine("        private void Clear(bool disposing)");
            TextClass.AppendLine("        {");
            TextClass.AppendLine("            if (!this._Disposed)");
            TextClass.AppendLine("            {");
            TextClass.AppendLine("                if (disposing)");
            TextClass.AppendLine("                {");

            foreach (var item in GroupTables)
            {
                var Model = item.Value;
                TextClass.AppendLine("                    _" + Model.ClassName.ToLower() + "Domain = null;");
            }

            TextClass.AppendLine("                }");
            TextClass.AppendLine("            }");
            TextClass.AppendLine("            _Disposed = true;");
            TextClass.AppendLine("        }");
            TextClass.AppendLine("");
            TextClass.AppendLine("        ~UnitOfWork()");
            TextClass.AppendLine("        {");
            TextClass.AppendLine("            Clear(false);");
            TextClass.AppendLine("        }");
            TextClass.AppendLine("    }");
            TextClass.AppendLine("}");

            var FileName = String.Format(@"{0}.{1}", "UnitOfWork", "cs");
            var Diretory = String.Format(@"{0}\{1}", FilePath, FileName);
            var DirInfo = new DirectoryInfo(FilePath);

            if (!DirInfo.Exists) { DirInfo.Create(); }
            using (TextWriter Writer = File.CreateText(Diretory)) { Writer.WriteLine(TextClass.ToString()); }
            return FileName;
        }

        public String BuildBaseFilter()
        {
            TextClass = new StringBuilder();
            TextClass.AppendLine("using System;");
            TextClass.AppendLine("using System.Linq;");
            TextClass.AppendLine("using System.Text;");
            TextClass.AppendLine("using System.Collections.Generic;");
            TextClass.AppendLine("using System.Threading.Tasks;");
            TextClass.AppendLine("");
            TextClass.AppendLine("namespace " + ProjectName + ".Domain.Filters");
            TextClass.AppendLine("{");
            TextClass.AppendLine("    public class Base");
            TextClass.AppendLine("    {");
            TextClass.AppendLine("        public Int32 ID { get; set; }");
            TextClass.AppendLine("    }");
            TextClass.AppendLine("}");

            return CreateFile("Base", "Filters");
        }      

        private String CreateFile(String ClassName)
        {
            var FileName = String.Format(@"{0}.{1}", ClassName, "cs");
            var Diretory = String.Format(@"{0}\{1}\{2}", FilePath, "Models", ClassName);
            var FullFile = String.Format(@"{0}\{1}", Diretory, FileName);
            var DirInfo = new System.IO.DirectoryInfo(Diretory);

            if (!DirInfo.Exists) { DirInfo.Create(); }

            using (System.IO.TextWriter Writer = System.IO.File.CreateText(FullFile))
            {
                Writer.WriteLine(TextClass.ToString());
            }

            return FileName;
        }

        private String CreateFile(String ClassName, String Folder)
        {
            var FileName = String.Format(@"{0}.{1}", ClassName, "cs");
            var Diretory = String.Format(@"{0}\{1}", FilePath, Folder);
            var FullFile = String.Format(@"{0}\{1}", Diretory, FileName);
            var DirInfo = new DirectoryInfo(Diretory);

            if (!DirInfo.Exists) { DirInfo.Create(); }
            using (TextWriter Writer = File.CreateText(FullFile)) 
            { 
                Writer.WriteLine(TextClass.ToString());
            }

            return FileName;
        }

    }
}
