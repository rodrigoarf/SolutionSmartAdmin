using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SmartAdmin.Gerador.Enums;
using SmartAdmin.Gerador.Models;
using System.IO;

namespace SmartAdmin.Gerador.Infrastructure
{
    public class Presentation
    {
        private StringBuilder TextClass;
        private String FilePath = ConfigurationManager.AppSettings["CamadaDeFrontend"].ToString();
        private String ProjectName = ConfigurationManager.AppSettings["NomeDoProjeto"].ToString();
        private EDataBase DatabaseType = SmartAdmin.Gerador.Program.DatabaseType;
        private String Sufixo = "Controller";

        public String BuildBase()
        {
            TextClass = new StringBuilder();
            TextClass.AppendLine("using System;");
            TextClass.AppendLine("using System.Collections.Generic;");
            TextClass.AppendLine("using System.Linq;");
            TextClass.AppendLine("using System.Web;");
            TextClass.AppendLine("using System.Web.Mvc;");
            TextClass.AppendLine("");
            TextClass.AppendLine("namespace " + ProjectName + ".WebUI.Controllers");
            TextClass.AppendLine("{");
            TextClass.AppendLine("    public class BaseController : Controller");
            TextClass.AppendLine("    {");
            TextClass.AppendLine("        public int PageSize = 50;");
            TextClass.AppendLine("    }");
            TextClass.AppendLine("}");

            return CreateFile("Base", "Controllers");
        }

        public string BuildController(String ClassName)
        {
            TextClass = new StringBuilder();
            TextClass.AppendLine("using System;");
            TextClass.AppendLine("using System.Collections.Generic;");
            TextClass.AppendLine("using System.Linq;");
            TextClass.AppendLine("using System.Web;");
            TextClass.AppendLine("using System.Web.Mvc;");
            TextClass.AppendLine("using PagedList;");
            TextClass.AppendLine("using " + ProjectName + ".Data.Model;");
            TextClass.AppendLine("using " + ProjectName + ".Domain.Model;");
            TextClass.AppendLine("using " + ProjectName + ".Domain.Filters;");
            TextClass.AppendLine("using " + ProjectName + ".Domain.Annotations;");
            TextClass.AppendLine("using " + ProjectName + ".WebUI.Controllers;");
            TextClass.AppendLine("using AutoMapper;");
            TextClass.AppendLine("");
            TextClass.AppendLine("namespace " + ProjectName + ".WebUI.Controllers");
            TextClass.AppendLine("{");
            TextClass.AppendLine("    public class " + ClassName + "Controller : BaseController");
            TextClass.AppendLine("    {");
            TextClass.AppendLine("        private " + ClassName + "Specialized " + ClassName + "Domain = new " + ClassName + "Specialized();");
            TextClass.AppendLine("");
            TextClass.AppendLine("        public " + ClassName + "Controller()");
            TextClass.AppendLine("        {");
            TextClass.AppendLine("            ViewBag.PageTitle = \"Titulo aqui!\";");
            TextClass.AppendLine("            ViewBag.PageDescr = \"Descricao aqui!\";");
            TextClass.AppendLine("        }");
            TextClass.AppendLine("");
            TextClass.AppendLine("        public ActionResult Index(int? Page)");
            TextClass.AppendLine("        {");
            TextClass.AppendLine("            var Collection = new List<" + ClassName + "Dto>();");
            TextClass.AppendLine("            var CurrentPage = ((Page == null) ? 1 : Convert.ToInt32(Page));");
            TextClass.AppendLine("");
            TextClass.AppendLine("            if (TempData[\"Listagem\"] != null) { Collection = TempData[\"Listagem\"] as List<" + ClassName + "Dto>; }");
            TextClass.AppendLine("            else { Collection = " + ClassName + "Domain.GetList(_ => _.ID > 0).Take(10000).ToList(); }");
            TextClass.AppendLine("");
            TextClass.AppendLine("            ViewBag.CurrentPage = CurrentPage;");
            TextClass.AppendLine("            ViewBag.TotalPage = Math.Ceiling((double)Collection.Count / PageSize);");
            TextClass.AppendLine("");
            TextClass.AppendLine("            var CollectionAnnotations = Mapper.Map<IEnumerable<" + ClassName + "Dto>, IEnumerable<" + ClassName + "Annotations>>(Collection);");
            TextClass.AppendLine("            return View(CollectionAnnotations.ToPagedList(CurrentPage, PageSize));");
            TextClass.AppendLine("        }");
            TextClass.AppendLine("");
            TextClass.AppendLine("        public ActionResult Create()");
            TextClass.AppendLine("        {");
            TextClass.AppendLine("            return View();");
            TextClass.AppendLine("        }");
            TextClass.AppendLine("");
            TextClass.AppendLine("        public ActionResult Edit(Int32 Id)");
            TextClass.AppendLine("        {");
            TextClass.AppendLine("            var ModelView = (Id > 0) ? AutoMapper.Mapper.Map<" + ClassName + "Annotations>(" + ClassName + "Domain.GetItem(_ => _.ID == Id)) : new " + ClassName + "Annotations();");
            TextClass.AppendLine("            return View(ModelView);");
            TextClass.AppendLine("        }");
            TextClass.AppendLine("");
            TextClass.AppendLine("        public ActionResult Save(" + ClassName + "Annotations ModelView)");
            TextClass.AppendLine("        {");
            TextClass.AppendLine("            if (!ModelState.IsValid)");
            TextClass.AppendLine("            {");
            TextClass.AppendLine("                return RedirectToAction(\"Edit\", new { Id = ModelView.ID });");
            TextClass.AppendLine("            }");
            TextClass.AppendLine("            else");
            TextClass.AppendLine("            {");
            TextClass.AppendLine("                var Model = Mapper.Map<" + ClassName + "Annotations, " + ClassName + "Dto>(ModelView);");
            TextClass.AppendLine("");
            TextClass.AppendLine("                if (Model.ID == 0)");
            TextClass.AppendLine("                { " + ClassName + "Domain.Save(Model); }");
            TextClass.AppendLine("                else");
            TextClass.AppendLine("                { " + ClassName + "Domain.Edit(Model); }");
            TextClass.AppendLine("");
            TextClass.AppendLine("                return RedirectToAction(\"Index\");");
            TextClass.AppendLine("            }");
            TextClass.AppendLine("        }");
            TextClass.AppendLine("");
            TextClass.AppendLine("        [HttpPost]");
            TextClass.AppendLine("        public ActionResult Load(" + ClassName + "Filter Model)");
            TextClass.AppendLine("        {");
            TextClass.AppendLine("            var Collection = " + ClassName + "Domain.GetByFilter(_ => _.ID > 0).Take(10000);");
            TextClass.AppendLine("");
            TextClass.AppendLine("            if (Model.ID > 0) { Collection = Collection.Where(_ => _.ID == Model.ID); }");
            TextClass.AppendLine("");
            TextClass.AppendLine("            TempData[\"Listagem\"] = Collection.ToList();");
            TextClass.AppendLine("");
            TextClass.AppendLine("            return RedirectToAction(\"Index\");");
            TextClass.AppendLine("        }");
            TextClass.AppendLine("    }");
            TextClass.AppendLine("}");

            var FileName = String.Format(@"{0}.{1}", String.Concat(ClassName, Sufixo), "cs");
            var Diretory = String.Format(@"{0}\{1}\{2}", FilePath, "Controllers", ClassName);
            var FullFile = String.Format(@"{0}\{1}", Diretory, FileName);
            var DirInfo = new DirectoryInfo(Diretory);

            if (!DirInfo.Exists) { DirInfo.Create(); }

            if (!File.Exists(FullFile))
            {
                using (System.IO.TextWriter Writer = File.CreateText(FullFile))
                {
                    Writer.WriteLine(TextClass.ToString());
                }
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
            using (TextWriter Writer = File.CreateText(FullFile)) { Writer.WriteLine(TextClass.ToString()); }
            return FileName;
        }

    }
}
