using SmartAdmin.Generator.Models;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SmartAdmin.Generator.Core
{
    public sealed class Domain
    {
        private StringBuilder TextClass;
        private EDataBase DatabaseType = EDataBase.MySql;

        public string BuildBase()
        {
            var FilePath = ConfigurationManager.AppSettings["OutputClassDomainModels"].ToString();
            var ProjectName = ConfigurationManager.AppSettings["ProjetName"].ToString();

            //--
            TextClass = new StringBuilder();

            TextClass.AppendLine("using System;");
            TextClass.AppendLine("using System.Text;");
            TextClass.AppendLine("using System.Collections;");
            TextClass.AppendLine("using System.Collections.Generic;");
            TextClass.AppendLine("using System.Linq;");
            TextClass.AppendLine("using System.ComponentModel.DataAnnotations.Schema;");
            TextClass.AppendLine("using System.Threading.Tasks;");
            TextClass.AppendLine("");
            TextClass.AppendLine("namespace " + ProjectName + ".Dto");
            TextClass.AppendLine("{");
            TextClass.AppendLine("    public class Base");
            TextClass.AppendLine("    {");
            TextClass.AppendLine("        protected Guid? IdObject { get; set; }");
            TextClass.AppendLine("");
            TextClass.AppendLine("        public int ID { get; set; }");
            TextClass.AppendLine("");
            TextClass.AppendLine("        public Base()");
            TextClass.AppendLine("        {");
            TextClass.AppendLine("            IdObject = Guid.NewGuid();");
            TextClass.AppendLine("        }");
            TextClass.AppendLine("");
            TextClass.AppendLine("        [NotMapped]");
            TextClass.AppendLine("        public Guid? IDLOGIC");
            TextClass.AppendLine("        {");
            TextClass.AppendLine("            get");
            TextClass.AppendLine("            {");
            TextClass.AppendLine("                return IdObject;");
            TextClass.AppendLine("            }");
            TextClass.AppendLine("            set");
            TextClass.AppendLine("            {");
            TextClass.AppendLine("                IdObject = value;");
            TextClass.AppendLine("            }");
            TextClass.AppendLine("        }");
            TextClass.AppendLine("    }");
            TextClass.AppendLine("}");
            //--

            var FileName = "Base.cs";
            var FullName = FilePath + @"\Models\" + FileName;
            var Directorio = new DirectoryInfo(FilePath);

            if (!Directorio.Exists)
                Directorio.Create();

            using (TextWriter Writer = File.CreateText(FullName))
            {
                Writer.WriteLine(TextClass.ToString());
            }

            TextClass.Clear();

            return ("> " + FileName);
        }

        public string BuildDomain(string TableName, ClassConfig DataModel)
        {
            var FilePath = ConfigurationManager.AppSettings["OutputClassDomain"].ToString();
            var TableSchema = Functions.GetTableSchema(TableName, DatabaseType);
            var ProjectName = ConfigurationManager.AppSettings["ProjetName"].ToString(); 
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
            TextClass.AppendLine("using " + ProjectName + ".Dto;");
            TextClass.AppendLine("");
            TextClass.AppendLine("namespace " + DataModel.NameSpaceDomain);
            TextClass.AppendLine("{");
            TextClass.AppendLine("    public partial class " + DataModel.ClassName);
            TextClass.AppendLine("    {");
            TextClass.AppendLine("        private bool _disposed = false;");
            TextClass.AppendLine("        private " + ProjectName + ".Data.UnitOfWork _unitOfWork = new " + ProjectName + ".Data.UnitOfWork();");
            TextClass.AppendLine("");
            TextClass.AppendLine("        /// <summary>");
            TextClass.AppendLine("        /// Salva um objeto<T>");
            TextClass.AppendLine("        /// </summary>");
            TextClass.AppendLine("        public void Save(" + DataModel.ClassName + Sufixo + " model)");
            TextClass.AppendLine("        {");
            TextClass.AppendLine("            _unitOfWork." + DataModel.ClassName + "Repository.Save(model);");
            TextClass.AppendLine("        }");
            TextClass.AppendLine("");
            TextClass.AppendLine("        /// <summary>");
            TextClass.AppendLine("        /// Salva e retorna o objeto<T> salvo");
            TextClass.AppendLine("        /// </summary>");
            TextClass.AppendLine("        public " + DataModel.ClassName + Sufixo + " SaveGetItem(" + DataModel.ClassName + Sufixo + " model)");
            TextClass.AppendLine("        {");
            TextClass.AppendLine("           var retorno = _unitOfWork." + DataModel.ClassName + "Repository.SaveGetItem(model);");
            TextClass.AppendLine("           return (retorno);");
            TextClass.AppendLine("        }");
            TextClass.AppendLine("");
            TextClass.AppendLine("        /// <summary>");
            TextClass.AppendLine("        /// Salva uma lista de objetos List<T>");
            TextClass.AppendLine("        /// </summary>");
            TextClass.AppendLine("        public void SaveAll(List<" + DataModel.ClassName + Sufixo + "> model)");
            TextClass.AppendLine("        {");
            TextClass.AppendLine("            _unitOfWork." + DataModel.ClassName + "Repository.SaveAll(model);");
            TextClass.AppendLine("        }");
            TextClass.AppendLine("");
            TextClass.AppendLine("        /// <summary>");
            TextClass.AppendLine("        /// Salva a edição de um objeto<T>");
            TextClass.AppendLine("        /// </summary>");
            TextClass.AppendLine("        public void Edit(" + DataModel.ClassName + Sufixo + " model)");
            TextClass.AppendLine("        {");
            TextClass.AppendLine("            _unitOfWork." + DataModel.ClassName + "Repository.Edit(model);");
            TextClass.AppendLine("        }");
            TextClass.AppendLine("");
            TextClass.AppendLine("        /// <summary>");
            TextClass.AppendLine("        /// Retorna um único objeto<T> buscado por expressão Lambda");
            TextClass.AppendLine("        /// </summary>");
            TextClass.AppendLine("        public " + DataModel.ClassName + Sufixo + " GetItem(Expression<Func<" + DataModel.ClassName + Sufixo + ", bool>> filter)");
            TextClass.AppendLine("        {");
            TextClass.AppendLine("            " + DataModel.ClassName + Sufixo + " model;");
            TextClass.AppendLine("            model = _unitOfWork." + DataModel.ClassName + "Repository.GetItem(filter);");
            TextClass.AppendLine("            return (model);");
            TextClass.AppendLine("        }");
            TextClass.AppendLine("");
            TextClass.AppendLine("        /// <summary>");
            TextClass.AppendLine("        /// Deleta um objeto");
            TextClass.AppendLine("        /// </summary>");
            TextClass.AppendLine("        public void Delete(Expression<Func<" + DataModel.ClassName + Sufixo + ", bool>> filter)");
            TextClass.AppendLine("        {");
            TextClass.AppendLine("            var model = _unitOfWork." + DataModel.ClassName + "Repository.GetItem(filter);");
            TextClass.AppendLine("            _unitOfWork." + DataModel.ClassName + "Repository.Delete(model);");
            TextClass.AppendLine("        }");
            TextClass.AppendLine("");        
            TextClass.AppendLine("        /// <summary>");
            TextClass.AppendLine("        /// Deleta uma lista de objetos");
            TextClass.AppendLine("        /// </summary>");
            TextClass.AppendLine("        public void DeleteAll(List<" + DataModel.ClassName + Sufixo + "> collection)");
            TextClass.AppendLine("        {");
            TextClass.AppendLine("            foreach (var item in collection) { _unitOfWork." + DataModel.ClassName + "Repository.Delete(item); }");
            TextClass.AppendLine("        }");  
            TextClass.AppendLine("");
            TextClass.AppendLine("        /// <summary>");
            TextClass.AppendLine("        /// Retorna uma lista List(T) de objetos buscados pela expressão Lambda");
            TextClass.AppendLine("        /// </summary>");
            TextClass.AppendLine("        public List<" + DataModel.ClassName + Sufixo + "> GetList(Expression<Func<" + DataModel.ClassName + Sufixo + ", bool>> filter)");
            TextClass.AppendLine("        {");
            TextClass.AppendLine("            List<" + DataModel.ClassName + Sufixo + "> collection;");
            TextClass.AppendLine("            collection = _unitOfWork." + DataModel.ClassName + "Repository.GetList(filter);");
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
                    TextClass.AppendLine("        public void ToInactive(int Id)");
                    TextClass.AppendLine("        {");
                    TextClass.AppendLine("            " + DataModel.ClassName + Sufixo + " model = _unitOfWork." + DataModel.ClassName + "Repository.GetItem(_ => _.ID == Id && _.STATUS == \"A\");");
                    TextClass.AppendLine("            model.STATUS = \"I\";");
                    TextClass.AppendLine("            _unitOfWork." + DataModel.ClassName + "Repository.Edit(model);");
                    TextClass.AppendLine("        }");
                    TextClass.AppendLine("");
                    TextClass.AppendLine("        /// <summary>");
                    TextClass.AppendLine("        /// Ativa um objeto para visualização");
                    TextClass.AppendLine("        /// </summary>");
                    TextClass.AppendLine("        public void ToActive(int Id)");
                    TextClass.AppendLine("        {");
                    TextClass.AppendLine("            " + DataModel.ClassName + Sufixo + " model = _unitOfWork." + DataModel.ClassName + "Repository.GetItem(_ => _.ID == Id && _.STATUS == \"I\");");
                    TextClass.AppendLine("            model.STATUS = \"A\";");
                    TextClass.AppendLine("            _unitOfWork." + DataModel.ClassName + "Repository.Edit(model);");
                    TextClass.AppendLine("        }");
                    TextClass.AppendLine("");
                }
            }

            TextClass.AppendLine("        /// <summary>");
            TextClass.AppendLine("        ///  Distroe o objeto e recursos não gerenciados liberando memória");
            TextClass.AppendLine("        /// </summary>");
            TextClass.AppendLine("        public void Dispose()");
            TextClass.AppendLine("        {");
            TextClass.AppendLine("            Clear(true);");
            TextClass.AppendLine("            GC.SuppressFinalize(this);");
            TextClass.AppendLine("        }");
            TextClass.AppendLine("");
            TextClass.AppendLine("        private void Clear(bool disposing)");
            TextClass.AppendLine("        {");
            TextClass.AppendLine("            if (!this._disposed)");
            TextClass.AppendLine("            {");
            TextClass.AppendLine("                if (disposing)");
            TextClass.AppendLine("                    _unitOfWork.Dispose();");
            TextClass.AppendLine("            }");
            TextClass.AppendLine("            _disposed = true;");
            TextClass.AppendLine("        }");
            TextClass.AppendLine("");
            TextClass.AppendLine("        ~" + DataModel.ClassName + "()");
            TextClass.AppendLine("        {");
            TextClass.AppendLine("            Clear(false);");
            TextClass.AppendLine("        }");
            TextClass.AppendLine("    }");
            TextClass.AppendLine("}");
            //--

            var FileName = DataModel.ClassName + ".cs";
            var FullName = FilePath + @"\Models\" + DataModel.ClassName + @"\" + FileName;
                                          
            if (!System.IO.File.Exists(FullName))
            {
                var Diretorio = new DirectoryInfo(FilePath + @"\Models\" + DataModel.ClassName);
                Diretorio.Create();

                using (TextWriter Writer = File.CreateText(FullName))
                {
                    Writer.WriteLine(TextClass.ToString());
                }

                TextClass.Clear();
            }

            return ("> " + FileName);

        }   

        public string BuildUnitOfWork(Dictionary<string, ClassConfig> GroupTables)
        {
            var FilePath = ConfigurationManager.AppSettings["OutputClassDomainUnitOfWork"].ToString();
            var ProjectName = ConfigurationManager.AppSettings["ProjetName"].ToString();

            //--
            TextClass = new StringBuilder();

            TextClass.AppendLine("using System;");
            TextClass.AppendLine("using System.Collections.Generic;");
            TextClass.AppendLine("using System.Linq;");
            TextClass.AppendLine("using System.Text;");
            TextClass.AppendLine("using System.Threading.Tasks;");
            TextClass.AppendLine("");
            TextClass.AppendLine("namespace " + ProjectName + ".Domain");
            TextClass.AppendLine("{");
            TextClass.AppendLine("    public class UnitOfWork : IDisposable");
            TextClass.AppendLine("    {");

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
                TextClass.AppendLine("                    this._" + Model.ClassName.ToLower() + "Domain = new " + Model.ClassName + "();");
                TextClass.AppendLine("                }");  
                TextClass.AppendLine("");
                TextClass.AppendLine("                return _" + Model.ClassName.ToLower() + "Domain;");
                TextClass.AppendLine("            }");
                TextClass.AppendLine("        }");
                TextClass.AppendLine("");  
            }

            TextClass.AppendLine("        private bool _disposed = false;");
            TextClass.AppendLine("");
            TextClass.AppendLine("        public void Dispose()");
            TextClass.AppendLine("        {");
            TextClass.AppendLine("            Clear(true);");
            TextClass.AppendLine("            GC.SuppressFinalize(this);");
            TextClass.AppendLine("        }");
            TextClass.AppendLine("");
            TextClass.AppendLine("        private void Clear(bool disposing)");
            TextClass.AppendLine("        {");
            TextClass.AppendLine("            if (!this._disposed)");
            TextClass.AppendLine("            {");
            TextClass.AppendLine("                if (disposing)");
            TextClass.AppendLine("                {");

            foreach (var item in GroupTables)
            {
                var Model = item.Value;
                TextClass.AppendLine("                    _" + Model.ClassName.ToLower() + "Domain.Dispose();");
            }

            TextClass.AppendLine("                }");
            TextClass.AppendLine("            }");
            TextClass.AppendLine("            _disposed = true;");
            TextClass.AppendLine("        }");
            TextClass.AppendLine("");
            TextClass.AppendLine("        ~UnitOfWork()");
            TextClass.AppendLine("        {");
            TextClass.AppendLine("            Clear(false);");
            TextClass.AppendLine("        }");
            TextClass.AppendLine("    }");
            TextClass.AppendLine("}");
            //--

            var FileName = "UnitOfWork.cs";
            var FullFile = FilePath + @"\" + FileName;

            DirectoryInfo Directory = new DirectoryInfo(FilePath);

            if (!Directory.Exists)
                Directory.Create();

            using (TextWriter Writer = File.CreateText(FullFile))
            {
                Writer.WriteLine(TextClass.ToString());
            }

            TextClass.Clear();

            return ("> " + FileName);
        }











        //private void BuildDomainExtension(ClassConfig DataModel, string FilePath, string Sufixo)
        //{
        //    var FileNameExt = DataModel.ClassName + ".ext.cs";
        //    var FilePatch = FilePath + DataModel.ClassName + "\\" + FileNameExt;
        //    var FileExist = File.Exists(FilePatch) ? true : false;

        //    if (!FileExist)
        //    {
        //        TextClass.AppendLine("using System;");
        //        TextClass.AppendLine("using System.Text;");
        //        TextClass.AppendLine("using System.Collections;");
        //        TextClass.AppendLine("using System.Collections.Generic;");
        //        TextClass.AppendLine("using System.Linq;");
        //        TextClass.AppendLine("using System.Threading.Tasks;");
        //        TextClass.AppendLine("using System.Linq.Expressions;");
        //        TextClass.AppendLine("using Common.Data;");
        //        TextClass.AppendLine("using Common.Dto;");
        //        TextClass.AppendLine("");
        //        TextClass.AppendLine("namespace " + DataModel.NameSpaceDomain);
        //        TextClass.AppendLine("{");
        //        TextClass.AppendLine("    public partial class " + DataModel.ClassName);
        //        TextClass.AppendLine("    {");
        //        TextClass.AppendLine("");
        //        TextClass.AppendLine("        public " + DataModel.ClassName + "()");
        //        TextClass.AppendLine("        {");
        //        TextClass.AppendLine("        }");
        //        TextClass.AppendLine("");
        //        TextClass.AppendLine("    }");
        //        TextClass.AppendLine("}");
        //        //--               

        //        using (TextWriter Writer = File.CreateText(FilePath + DataModel.ClassName + @"\" + FileNameExt))
        //        {
        //            Writer.WriteLine(TextClass.ToString());
        //        }

        //        TextClass.Clear();
        //    }
        //}                       
    }
}
