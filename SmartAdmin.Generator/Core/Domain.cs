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

        public string BuildDomain(string TableName, ClassConfig DataModel)
        {
            var FilePath = ConfigurationManager.AppSettings["OutputClassDomain"].ToString();
            var TableSchema = Functions.GetTableSchema(TableName, EDataBase.MySql);
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
            TextClass.AppendLine("        /// Salva um objeto");
            TextClass.AppendLine("        /// </summary>");
            TextClass.AppendLine("        public void Save(" + DataModel.ClassName + Sufixo + " model)");
            TextClass.AppendLine("        {");
            TextClass.AppendLine("            _unitOfWork." + DataModel.ClassName + "Repository.AddItem(model);");
            TextClass.AppendLine("        }");
            TextClass.AppendLine("");
            TextClass.AppendLine("        /// <summary>");
            TextClass.AppendLine("        /// Salva e retorna um objeto");
            TextClass.AppendLine("        /// </summary>");
            TextClass.AppendLine("        public " + DataModel.ClassName + Sufixo + " SaveGetItem(" + DataModel.ClassName + Sufixo + " model)");
            TextClass.AppendLine("        {");
            TextClass.AppendLine("           var retorno = _unitOfWork." + DataModel.ClassName + "Repository.AddGetItem(model);");
            TextClass.AppendLine("           return (retorno);");
            TextClass.AppendLine("        }");
            TextClass.AppendLine("");
            TextClass.AppendLine("        /// <summary>");
            TextClass.AppendLine("        /// Salva uma lista de objetos");
            TextClass.AppendLine("        /// </summary>");
            TextClass.AppendLine("        public void SaveAll(List<" + DataModel.ClassName + Sufixo + "> model)");
            TextClass.AppendLine("        {");
            TextClass.AppendLine("            _unitOfWork." + DataModel.ClassName + "Repository.AddAll(model);");
            TextClass.AppendLine("        }");
            TextClass.AppendLine("");
            TextClass.AppendLine("        /// <summary>");
            TextClass.AppendLine("        /// Salva a edição de um objeto");
            TextClass.AppendLine("        /// </summary>");
            TextClass.AppendLine("        public void Edit(" + DataModel.ClassName + Sufixo + " model)");
            TextClass.AppendLine("        {");
            TextClass.AppendLine("            _unitOfWork." + DataModel.ClassName + "Repository.Edit(model);");
            TextClass.AppendLine("        }");
            TextClass.AppendLine("");
            TextClass.AppendLine("        /// <summary>");
            TextClass.AppendLine("        /// Retorna um único objeto buscado por expressão Lambda");
            TextClass.AppendLine("        /// </summary>");
            TextClass.AppendLine("        public " + DataModel.ClassName + Sufixo + " GetItem(Expression<Func<" + DataModel.ClassName + Sufixo + ", bool>> filter)");
            TextClass.AppendLine("        {");
            TextClass.AppendLine("            " + DataModel.ClassName + Sufixo + " model;");
            TextClass.AppendLine("            model = _unitOfWork." + DataModel.ClassName + "Repository.GetItem(filter);");
            TextClass.AppendLine("            return (model);");
            TextClass.AppendLine("        }");
            TextClass.AppendLine("");
            TextClass.AppendLine("        /// <summary>");
            TextClass.AppendLine("        /// Retorna um objeto do tipo List(T) de objetos buscados pela expressão Lambda");
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
                    TextClass.AppendLine("        /// Anativa um objeto para visualização");
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
            TextClass.AppendLine("        ///  Distroe objeto e recursos não gerenciados liberando memória");
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


            //var Diretorio = new DirectoryInfo(FilePath + @"\Models\" + DataModel.ClassName);

            //if (!Diretorio.Exists)
            //    Diretorio.Create();

            //using (TextWriter Writer = File.CreateText(FullName))
            //{
            //    Writer.WriteLine(TextClass.ToString());
            //}

            //TextClass.Clear();

            //--
            //BuildDomainExtension(DataModel, FilePath, Sufixo);

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
