using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SmartAdmin.Generator.Models;
using System.Configuration;
using System.IO;

namespace SmartAdmin.Generator.Core
{
    public class Data
    {
        private StringBuilder TextClass;
        private EDataBase DatabaseType = EDataBase.MySql;
        private static string MySqlDataBase = ConfigurationManager.AppSettings["MySqlDataBase"].ToString();

        public string BuildModel(KeyValuePair<string, ClassConfig> TableSetting)
        {
            var FilePath = ConfigurationManager.AppSettings["OutputClassDataModels"].ToString();
            var SufixoModels = "Dto";
            var TableName = TableSetting.Key;
            var DataModel = TableSetting.Value;
            var TableSchema = Functions.GetTableSchema(TableName, DatabaseType);

            //--
            TextClass = new StringBuilder();

            TextClass.AppendLine("using System;");
            TextClass.AppendLine("using System.Text;");
            TextClass.AppendLine("using System.Collections;");
            TextClass.AppendLine("using System.Collections.Generic;");
            TextClass.AppendLine("using System.Linq;");
            TextClass.AppendLine("using System.ComponentModel.DataAnnotations;");
            TextClass.AppendLine("using System.Threading.Tasks;");
            TextClass.AppendLine("");
            TextClass.AppendLine("namespace " + DataModel.NameSpaceDto);
            TextClass.AppendLine("{");
            TextClass.AppendLine("    public class " + DataModel.ClassName + SufixoModels + " : Base");
            TextClass.AppendLine("    {");

            var ColumnDataType = String.Empty;

            foreach (var ColumnMapper in TableSchema.CollectionColumn)
            {
                if (ColumnMapper.ColumnKey != "pk")
                {
                    if (ColumnMapper.DataType == "datetime")
                    {
                        if (ColumnMapper.IsNullable == "yes")
                        {
                            ColumnDataType = Functions.GetColumnType(ColumnMapper.DataType);
                            TextClass.AppendLine("        public Nullable<System.DateTime> " + ColumnMapper.ColumnName + " { get; set; }");
                        }
                        else
                        {
                            ColumnDataType = Functions.GetColumnType(ColumnMapper.DataType);
                            TextClass.AppendLine("        public " + ColumnDataType + " " + ColumnMapper.ColumnName + " { get; set; }");
                        }
                    }
                    else if (ColumnMapper.DataType == "int")
                    {
                        if (ColumnMapper.IsNullable == "yes")
                        {
                            ColumnDataType = Functions.GetColumnType(ColumnMapper.DataType);
                            TextClass.AppendLine("        public Nullable<System.Int32> " + ColumnMapper.ColumnName + " { get; set; }");
                        }
                        else
                        {
                            ColumnDataType = Functions.GetColumnType(ColumnMapper.DataType);
                            TextClass.AppendLine("        public " + ColumnDataType + " " + ColumnMapper.ColumnName + " { get; set; }");
                        }
                    }
                    else if (ColumnMapper.DataType == "decimal")
                    {
                        if (ColumnMapper.IsNullable == "yes")
                        {
                            ColumnDataType = Functions.GetColumnType(ColumnMapper.DataType);
                            TextClass.AppendLine("        public Nullable<System.Decimal> " + ColumnMapper.ColumnName + " { get; set; }");
                        }
                        else
                        {
                            ColumnDataType = Functions.GetColumnType(ColumnMapper.DataType);
                            TextClass.AppendLine("        public " + ColumnDataType + " " + ColumnMapper.ColumnName + " { get; set; }");
                        }
                    }
                    else if (ColumnMapper.DataType == "long")
                    {
                        if (ColumnMapper.IsNullable == "yes")
                        {                                   
                            ColumnDataType = Functions.GetColumnType(ColumnMapper.DataType);
                            TextClass.AppendLine("        public Nullable<System.long> " + ColumnMapper.ColumnName + " { get; set; }");
                        }
                        else
                        {
                            ColumnDataType = Functions.GetColumnType(ColumnMapper.DataType);
                            TextClass.AppendLine("        public " + ColumnDataType + " " + ColumnMapper.ColumnName + " { get; set; }");
                        }
                    }
                    else if (ColumnMapper.DataType == "longblob")
                    {
                        if (ColumnMapper.IsNullable == "yes")
                        {
                            ColumnDataType = Functions.GetColumnType(ColumnMapper.DataType);
                            TextClass.AppendLine("        public Nullable<System.Byte> " + ColumnMapper.ColumnName + " { get; set; }");
                        }
                        else
                        {
                            ColumnDataType = Functions.GetColumnType(ColumnMapper.DataType);
                            TextClass.AppendLine("        public " + ColumnDataType + " " + ColumnMapper.ColumnName + " { get; set; }");
                        }
                    }                     
                    else
                    {   
                        ColumnDataType = Functions.GetColumnType(ColumnMapper.DataType);
                        TextClass.AppendLine("        public " + ColumnDataType + " " + ColumnMapper.ColumnName + " { get; set; }");
                    }
                }
            }          
            //--  

            var Diretory = FilePath + @"\Models\" + DataModel.ClassName;
            var FileName = DataModel.ClassName + SufixoModels + ".cs";
            var FullFile = Diretory + @"\" + FileName;

            DirectoryInfo Directory = new DirectoryInfo(Diretory);

            if (!Directory.Exists)
                Directory.Create();

            using (TextWriter Writer = File.CreateText(FullFile))
            {
                Writer.WriteLine(TextClass.ToString());
            }

            TextClass.Clear();

            return ("> " + FileName);
        }

        public string BuildMapper(KeyValuePair<string, ClassConfig> TableSetting)
        {
            var FilePath = ConfigurationManager.AppSettings["OutputClassDataMapper"].ToString();
            var SufixoMapper = "Mapper";
            var SufixoModels = "Dto";
            var TableName = TableSetting.Key;
            var DataModel = TableSetting.Value;
            var TableSchema = Functions.GetTableSchema(TableName, DatabaseType);

            //--
            TextClass = new StringBuilder();

            TextClass.AppendLine("using System;");
            TextClass.AppendLine("using System.Text;");
            TextClass.AppendLine("using System.Collections;");
            TextClass.AppendLine("using System.Collections.Generic;");
            TextClass.AppendLine("using System.Data.Entity.ModelConfiguration;");
            TextClass.AppendLine("using " + DataModel.NameSpaceDto + ";");
            TextClass.AppendLine("");
            TextClass.AppendLine("namespace " + DataModel.NameSpaceMapper);
            TextClass.AppendLine("{");
            TextClass.AppendLine("    public class " + DataModel.ClassName + SufixoMapper + " : EntityTypeConfiguration<" + DataModel.ClassName + SufixoModels + ">");
            TextClass.AppendLine("    {");
            TextClass.AppendLine("        public " + DataModel.ClassName + SufixoMapper + "()");
            TextClass.AppendLine("        {");
            TextClass.AppendLine("            // Primary Key");
            TextClass.AppendLine("            this.HasKey(t => t.ID);");
            TextClass.AppendLine("");
            TextClass.AppendLine("            // Propertys");

            foreach (var ColumnMapper in TableSchema.CollectionColumn)
            {
                if (ColumnMapper.ColumnKey != "pk")
                {
                    if (ColumnMapper.DataType == "longblob") //--> Tratamento somente para longblob
                    {
                        if (ColumnMapper.IsNullable == "no")
                        {
                            TextClass.AppendLine("            this.Property(_ => _." + ColumnMapper.ColumnName + ").IsRequired();");
                        }
                        else
                        {
                            TextClass.AppendLine("            this.Property(_ => _." + ColumnMapper.ColumnName + ");");
                        }
                    }
                    else if (ColumnMapper.DataType == "longtext") //--> Tratamento somente para longtext
                    {
                        if (ColumnMapper.IsNullable == "no")
                        {
                            TextClass.AppendLine("            this.Property(_ => _." + ColumnMapper.ColumnName + ").IsRequired();");
                        }
                        else
                        {
                            TextClass.AppendLine("            this.Property(_ => _." + ColumnMapper.ColumnName + ");");
                        }
                    }
                    else
                    {
                        if (ColumnMapper.IsNullable == "no")
                        {
                            if (ColumnMapper.MaxLenght != String.Empty)
                                TextClass.AppendLine("            this.Property(_ => _." + ColumnMapper.ColumnName + ").IsRequired().HasMaxLength(" + ColumnMapper.MaxLenght + ");");
                            else
                                TextClass.AppendLine("            this.Property(_ => _." + ColumnMapper.ColumnName + ").IsRequired();");
                        }
                        else if (ColumnMapper.IsNullable == "yes")
                        {
                            if (ColumnMapper.MaxLenght != String.Empty)
                                TextClass.AppendLine("            this.Property(_ => _." + ColumnMapper.ColumnName + ").HasMaxLength(" + ColumnMapper.MaxLenght + ");");
                            else
                                TextClass.AppendLine("            this.Property(_ => _." + ColumnMapper.ColumnName + ");");
                        }
                    }
                }
            }

            TextClass.AppendLine("");
            TextClass.AppendLine("            // Table & Column Mappings");
            TextClass.AppendLine(String.Format("            this.ToTable(\"" + TableName + "\", \"{0}\");", MySqlDataBase));
            TextClass.AppendLine("");

            foreach (var ColumnMapper in TableSchema.CollectionColumn)
            {
                if (ColumnMapper.ColumnKey == "pk")
                    TextClass.AppendLine("            this.Property(_ => _.ID).HasColumnName(\"" + ColumnMapper.ColumnName + "\");");
                else
                    TextClass.AppendLine("            this.Property(_ => _." + ColumnMapper.ColumnName + ").HasColumnName(\"" + ColumnMapper.ColumnName + "\");");
            }

            TextClass.AppendLine("        }");
            TextClass.AppendLine("    }");
            TextClass.AppendLine("}");
            //--

            var Diretory = FilePath + @"\Models\" + DataModel.ClassName;
            var FileName = DataModel.ClassName + SufixoMapper + ".cs";
            var FullFile = Diretory + @"\" + FileName;

            DirectoryInfo Directory = new DirectoryInfo(Diretory);

            if (!Directory.Exists)
                Directory.Create();

            using (TextWriter Writer = File.CreateText(FullFile))
            {
                Writer.WriteLine(TextClass.ToString());
            }

            TextClass.Clear();

            return ("> " + FileName);
        }

        public string BuildContext(Dictionary<string, ClassConfig> GroupTables)
        {
            var FilePath = ConfigurationManager.AppSettings["OutputClassDataContext"].ToString();
            var OutputClassDataContextName = ConfigurationManager.AppSettings["OutputClassDataContextName"].ToString();
            var ProjectName = ConfigurationManager.AppSettings["ProjetName"].ToString(); 
            var SufixoModels = "Dto";
            
            //--
            TextClass = new StringBuilder();

            TextClass.AppendLine("using System;");
            TextClass.AppendLine("using System.Text;");
            TextClass.AppendLine("using System.Collections.Generic;");
            TextClass.AppendLine("using System.ComponentModel.DataAnnotations.Schema;");
            TextClass.AppendLine("using System.Data.Entity;");
            TextClass.AppendLine("using System.Data.Entity.Infrastructure;");
            TextClass.AppendLine("using System.Data.Entity.ModelConfiguration.Conventions;");
            TextClass.AppendLine("using System.Linq;");
            TextClass.AppendLine("using System.Configuration;");
            TextClass.AppendLine("using System.Threading.Tasks;");
            TextClass.AppendLine("using " + ProjectName + ".Data.Mapper;");

            var FirstItem = GroupTables.First(); 
            TextClass.AppendLine("using " + FirstItem.Value.NameSpaceDto + ";");
            TextClass.AppendLine("");

            TextClass.AppendLine("namespace " + ProjectName + ".Data.Context");
            TextClass.AppendLine("{");
            TextClass.AppendLine("    public partial class " + OutputClassDataContextName + " : DbContext");
            TextClass.AppendLine("    {");
            TextClass.AppendLine("        static " + OutputClassDataContextName + "()");
            TextClass.AppendLine("        {");
            TextClass.AppendLine("            Database.SetInitializer<" + OutputClassDataContextName + ">(null);");
            TextClass.AppendLine("        }");
            TextClass.AppendLine("");
            TextClass.AppendLine("        public " + OutputClassDataContextName + "() : base(\"Name=" + OutputClassDataContextName + "\")");
            TextClass.AppendLine("        {");
            TextClass.AppendLine("             this.Configuration.AutoDetectChangesEnabled = false;");
            TextClass.AppendLine("             this.Configuration.LazyLoadingEnabled = false;");
            TextClass.AppendLine("             this.Configuration.ProxyCreationEnabled = false;");
            TextClass.AppendLine("             this.Configuration.ValidateOnSaveEnabled = false;");
            TextClass.AppendLine("        }");
            TextClass.AppendLine("");

            foreach (var item in GroupTables)
            {
                var Model = item.Value;  
                TextClass.AppendLine("        public DbSet<" + Model.ClassName + SufixoModels + "> " + Model.ClassName + " { get; set; }");
            }

            TextClass.AppendLine("");
            TextClass.AppendLine("        protected override void OnModelCreating(DbModelBuilder modelBuilder)");
            TextClass.AppendLine("        {");

            foreach (var item in GroupTables)
            {
                var Model = item.Value; 
                TextClass.AppendLine("             modelBuilder.Configurations.Add(new " + Model.ClassName + "Mapper());");
            }    

            TextClass.AppendLine("        }");
            TextClass.AppendLine("    }");
            TextClass.AppendLine("}");
            //--

            var FileName = OutputClassDataContextName + ".cs";
            var Diretory = FilePath + @"\Context";
            var FullFile = Diretory + @"\" + FileName;

            DirectoryInfo Directory = new DirectoryInfo(Diretory);

            if (!Directory.Exists)
                Directory.Create();

            using (TextWriter Writer = File.CreateText(FullFile))
            {
                Writer.WriteLine(TextClass.ToString());
            }

            TextClass.Clear();

            return ("> " + FileName);
        }

        public string BuildRepository()
        {
            var FilePath = ConfigurationManager.AppSettings["OutputClassDataRepository"].ToString();
            var OutputClassDataContextName = ConfigurationManager.AppSettings["OutputClassDataContextName"].ToString();
            var ProjectName = ConfigurationManager.AppSettings["ProjetName"].ToString(); 

            //--
            TextClass = new StringBuilder();

            TextClass.AppendLine("using System;");
            TextClass.AppendLine("using System.Text;");
            TextClass.AppendLine("using System.Collections;");
            TextClass.AppendLine("using System.Collections.Generic;");
            TextClass.AppendLine("using System.Data;");
            TextClass.AppendLine("using System.Data.Entity;");
            TextClass.AppendLine("using System.Data.Entity.Infrastructure;");
            TextClass.AppendLine("using System.Linq;");
            TextClass.AppendLine("using System.Linq.Expressions;");
            TextClass.AppendLine("using System.Threading.Tasks;");
            TextClass.AppendLine("using " + ProjectName + ".Data.Context;");
            TextClass.AppendLine("");
            TextClass.AppendLine("namespace " + ProjectName + ".Data.Generic");
            TextClass.AppendLine("{");
            TextClass.AppendLine("    public class Repository<TEntity> : IDisposable where TEntity : class");
            TextClass.AppendLine("    {");
            TextClass.AppendLine("        protected " + OutputClassDataContextName + " _context;");
            TextClass.AppendLine("        protected DbSet<TEntity> _dbSet;");
            TextClass.AppendLine("");
            TextClass.AppendLine("        public Repository(" + OutputClassDataContextName + " context)");
            TextClass.AppendLine("        {");
            TextClass.AppendLine("            this._context = context;");
            TextClass.AppendLine("            this._dbSet = context.Set<TEntity>();");
            TextClass.AppendLine("        }");
            TextClass.AppendLine("");
            TextClass.AppendLine("        /// <summary>");
            TextClass.AppendLine("        /// Método genérico que salva uma entidade"); 
            TextClass.AppendLine("        /// </summary>");
            TextClass.AppendLine("        public void Save(TEntity entity)");
            TextClass.AppendLine("        {");
            TextClass.AppendLine("            try");
            TextClass.AppendLine("            {");
            TextClass.AppendLine("                _dbSet.Add(entity);");
            TextClass.AppendLine("                _context.SaveChanges();");
            TextClass.AppendLine("            }");
            TextClass.AppendLine("            catch (System.Data.Entity.Validation.DbEntityValidationException ex)");
            TextClass.AppendLine("            {");
            TextClass.AppendLine("                StringBuilder sb = new StringBuilder();");
            TextClass.AppendLine("");
            TextClass.AppendLine("                foreach (var failure in ex.EntityValidationErrors)");
            TextClass.AppendLine("                {");
            TextClass.AppendLine("                    sb.AppendFormat(\"{0} Falha da validação \", failure.Entry.Entity.GetType());");
            TextClass.AppendLine("");
            TextClass.AppendLine("                    foreach (var error in failure.ValidationErrors)");
            TextClass.AppendLine("                    {");
            TextClass.AppendLine("                        sb.AppendFormat(\"- {0} : {1}\", error.PropertyName, error.ErrorMessage);");
            TextClass.AppendLine("                        sb.AppendLine();");
            TextClass.AppendLine("                    }");
            TextClass.AppendLine("                }");
            TextClass.AppendLine("");
            TextClass.AppendLine("                throw new System.Data.Entity.Validation.DbEntityValidationException(\"Erros da validação da Entidade: \" + sb.ToString(), ex);");
            TextClass.AppendLine("            }");
            TextClass.AppendLine("        }");
            TextClass.AppendLine("");
            TextClass.AppendLine("        /// <summary>");
            TextClass.AppendLine("        /// Método genérico que atualiza uma entidade");
            TextClass.AppendLine("        /// </summary>");
            TextClass.AppendLine("        public void Edit(TEntity entity)");
            TextClass.AppendLine("        {");
            TextClass.AppendLine("            var entry = _context.Entry<TEntity>(entity);");
            TextClass.AppendLine("            var pkey = _dbSet.Create().GetType().GetProperty(\"ID\").GetValue(entity);");
            TextClass.AppendLine("");
            TextClass.AppendLine("            if (entry.State ==EntityState.Detached)");
            TextClass.AppendLine("            {");
            TextClass.AppendLine("                var set = _context.Set<TEntity>();");
            TextClass.AppendLine("                TEntity attachedEntity = set.Find(pkey);");
            TextClass.AppendLine("");
            TextClass.AppendLine("                if (attachedEntity != null)");
            TextClass.AppendLine("                {");
            TextClass.AppendLine("                    var attachedEntry = _context.Entry(attachedEntity);");
            TextClass.AppendLine("                    attachedEntry.CurrentValues.SetValues(entity);");
            TextClass.AppendLine("                }");
            TextClass.AppendLine("                else");
            TextClass.AppendLine("                {");
            TextClass.AppendLine("                    entry.State = EntityState.Modified;");
            TextClass.AppendLine("                }");
            TextClass.AppendLine("            }");
            TextClass.AppendLine("");
            TextClass.AppendLine("            try");
            TextClass.AppendLine("            {");
            TextClass.AppendLine("                _context.SaveChanges();");
            TextClass.AppendLine("            }");
            TextClass.AppendLine("            catch (System.Data.Entity.Validation.DbEntityValidationException ex)");
            TextClass.AppendLine("            {");
            TextClass.AppendLine("                StringBuilder sb = new StringBuilder();");
            TextClass.AppendLine("");
            TextClass.AppendLine("                foreach (var failure in ex.EntityValidationErrors)");
            TextClass.AppendLine("                {");
            TextClass.AppendLine("                    sb.AppendFormat(\"{0} Falha da validação \", failure.Entry.Entity.GetType());");
            TextClass.AppendLine("                    foreach (var error in failure.ValidationErrors)");
            TextClass.AppendLine("                    {");
            TextClass.AppendLine("                        sb.AppendFormat(\"- {0} : {1}\", error.PropertyName, error.ErrorMessage);");
            TextClass.AppendLine("                        sb.AppendLine();");
            TextClass.AppendLine("                    }");
            TextClass.AppendLine("                }");
            TextClass.AppendLine("");
            TextClass.AppendLine("                throw new System.Data.Entity.Validation.DbEntityValidationException(\"Erros da validação da Entidade: \" + sb.ToString(), ex);");
            TextClass.AppendLine("            }");
            TextClass.AppendLine("        }");
            TextClass.AppendLine("");
            TextClass.AppendLine("        /// <summary>");
            TextClass.AppendLine("        /// Método genérico que deleta uma entidade, isto é exclui definitivamente da tabela");
            TextClass.AppendLine("        /// </summary>");
            TextClass.AppendLine("        public void Delete(TEntity entity)");
            TextClass.AppendLine("        {");
            TextClass.AppendLine("            try");
            TextClass.AppendLine("            {");
            TextClass.AppendLine("                _dbSet.Remove(entity);");
            TextClass.AppendLine("                _context.SaveChanges();");
            TextClass.AppendLine("            }");
            TextClass.AppendLine("            catch (System.Data.Entity.Validation.DbEntityValidationException ex)");
            TextClass.AppendLine("            {");
            TextClass.AppendLine("                StringBuilder sb = new StringBuilder();");
            TextClass.AppendLine("");
            TextClass.AppendLine("                foreach (var failure in ex.EntityValidationErrors)");
            TextClass.AppendLine("                {");
            TextClass.AppendLine("                    sb.AppendFormat(\"{0} Falha da validação \", failure.Entry.Entity.GetType());");
            TextClass.AppendLine("                    foreach (var error in failure.ValidationErrors)");
            TextClass.AppendLine("                    {");
            TextClass.AppendLine("                        sb.AppendFormat(\"- {0} : {1}\", error.PropertyName, error.ErrorMessage);");
            TextClass.AppendLine("                        sb.AppendLine();");
            TextClass.AppendLine("                    }");
            TextClass.AppendLine("                }");
            TextClass.AppendLine("");
            TextClass.AppendLine("                throw new System.Data.Entity.Validation.DbEntityValidationException(\"Erros da validação da Entidade: \" + sb.ToString(), ex);");
            TextClass.AppendLine("            }");
            TextClass.AppendLine("        }");
            TextClass.AppendLine("");
            TextClass.AppendLine("        /// <summary>");
            TextClass.AppendLine("        /// Método genérico que salva uma entidade e retorna a mesma salva");
            TextClass.AppendLine("        /// </summary>");
            TextClass.AppendLine("        public TEntity SaveGetItem(TEntity entity)");
            TextClass.AppendLine("        {");
            TextClass.AppendLine("            try");
            TextClass.AppendLine("            {");
            TextClass.AppendLine("                _dbSet.Add(entity);");
            TextClass.AppendLine("                _context.SaveChanges();");
            TextClass.AppendLine("");
            TextClass.AppendLine("                return (entity);");
            TextClass.AppendLine("            }");
            TextClass.AppendLine("            catch (System.Data.Entity.Validation.DbEntityValidationException ex)");
            TextClass.AppendLine("            {");
            TextClass.AppendLine("                StringBuilder sb = new StringBuilder();");
            TextClass.AppendLine("");
            TextClass.AppendLine("                foreach (var failure in ex.EntityValidationErrors)");
            TextClass.AppendLine("                {");
            TextClass.AppendLine("                    sb.AppendFormat(\"{0} Falha da validação \", failure.Entry.Entity.GetType());");
            TextClass.AppendLine("");
            TextClass.AppendLine("                    foreach (var error in failure.ValidationErrors)");
            TextClass.AppendLine("                    {");
            TextClass.AppendLine("                        sb.AppendFormat(\"- {0} : {1}\", error.PropertyName, error.ErrorMessage);");
            TextClass.AppendLine("                        sb.AppendLine();");
            TextClass.AppendLine("                    }");
            TextClass.AppendLine("                }");
            TextClass.AppendLine("");
            TextClass.AppendLine("                throw new System.Data.Entity.Validation.DbEntityValidationException(\"Erros da validação da Entidade: \" + sb.ToString(), ex);");
            TextClass.AppendLine("            }");
            TextClass.AppendLine("        }");
            TextClass.AppendLine("");
            TextClass.AppendLine("        /// <summary>");
            TextClass.AppendLine("        /// Método genérico que salva uma lista de entidades");
            TextClass.AppendLine("        /// </summary>");
            TextClass.AppendLine("        public void SaveAll(List<TEntity> entity)");
            TextClass.AppendLine("        {");
            TextClass.AppendLine("            try");
            TextClass.AppendLine("            {");
            TextClass.AppendLine("                foreach (var item in entity)");
            TextClass.AppendLine("                {");
            TextClass.AppendLine("                    _dbSet.Add(item);");
            TextClass.AppendLine("                }");
            TextClass.AppendLine("                _context.SaveChanges();");
            TextClass.AppendLine("            }");
            TextClass.AppendLine("            catch (System.Data.Entity.Validation.DbEntityValidationException ex)");
            TextClass.AppendLine("            {");
            TextClass.AppendLine("                StringBuilder sb = new StringBuilder();");
            TextClass.AppendLine("");
            TextClass.AppendLine("                foreach (var failure in ex.EntityValidationErrors)");
            TextClass.AppendLine("                {");
            TextClass.AppendLine("                    sb.AppendFormat(\"{0} Falha da validação \", failure.Entry.Entity.GetType());");
            TextClass.AppendLine("                    foreach (var error in failure.ValidationErrors)");
            TextClass.AppendLine("                    {");
            TextClass.AppendLine("                        sb.AppendFormat(\"- {0} : {1}\", error.PropertyName, error.ErrorMessage);");
            TextClass.AppendLine("                        sb.AppendLine();");
            TextClass.AppendLine("                    }");
            TextClass.AppendLine("                }");
            TextClass.AppendLine("");
            TextClass.AppendLine("                throw new System.Data.Entity.Validation.DbEntityValidationException(\"Erros da validação da Entidade: \" + sb.ToString(), ex);");
            TextClass.AppendLine("            }");
            TextClass.AppendLine("        }");
            TextClass.AppendLine("");
            TextClass.AppendLine("        /// <summary>");
            TextClass.AppendLine("        /// Método genérico que deleta uma lsita de entidades, isto é exclui definitivamente da tabela");
            TextClass.AppendLine("        /// </summary>");
            TextClass.AppendLine("        public void DeleteAll(Expression<Func<TEntity, bool>> filter = null)");
            TextClass.AppendLine("        {");
            TextClass.AppendLine("            IQueryable<TEntity> query = _dbSet;");
            TextClass.AppendLine("            List<TEntity> listDelete = query.Where(filter).ToList();");
            TextClass.AppendLine("");
            TextClass.AppendLine("            foreach (var item in listDelete)");
            TextClass.AppendLine("                _dbSet.Remove(item);");
            TextClass.AppendLine("");
            TextClass.AppendLine("            _context.SaveChanges();");
            TextClass.AppendLine("        }");
            TextClass.AppendLine("");
            TextClass.AppendLine("        /// <summary>");
            TextClass.AppendLine("        /// Método genérico que retorna uma entidade, mediante uma expressão lambda");
            TextClass.AppendLine("        /// </summary>");
            TextClass.AppendLine("        public TEntity GetItem(Expression<Func<TEntity, bool>> filter = null, Func<IQueryable<TEntity>, IOrderedQueryable<TEntity>> orderBy = null)");
            TextClass.AppendLine("        {");
            TextClass.AppendLine("            try");
            TextClass.AppendLine("            {");
            TextClass.AppendLine("                IQueryable<TEntity> query = _dbSet;");
            TextClass.AppendLine("");
            TextClass.AppendLine("                if (filter != null)");
            TextClass.AppendLine("                    query = query.Where(filter);");
            TextClass.AppendLine("");
            TextClass.AppendLine("                return query.ToList().FirstOrDefault();");
            TextClass.AppendLine("            }");
            TextClass.AppendLine("            catch (System.Data.Entity.Validation.DbEntityValidationException ex)");
            TextClass.AppendLine("            {");
            TextClass.AppendLine("                StringBuilder sb = new StringBuilder();");
            TextClass.AppendLine("");
            TextClass.AppendLine("                foreach (var failure in ex.EntityValidationErrors)");
            TextClass.AppendLine("                {");
            TextClass.AppendLine("                    sb.AppendFormat(\"{0} Falha da validação \", failure.Entry.Entity.GetType());");
            TextClass.AppendLine("                    foreach (var error in failure.ValidationErrors)");
            TextClass.AppendLine("                    {");
            TextClass.AppendLine("                        sb.AppendFormat(\"- {0} : {1}\", error.PropertyName, error.ErrorMessage);");
            TextClass.AppendLine("                        sb.AppendLine();");
            TextClass.AppendLine("                    }");
            TextClass.AppendLine("                }");
            TextClass.AppendLine("");
            TextClass.AppendLine("                throw new System.Data.Entity.Validation.DbEntityValidationException(\"Erros da validação da Entidade: \" + sb.ToString(), ex);");
            TextClass.AppendLine("            }");
            TextClass.AppendLine("        }");
            TextClass.AppendLine("");
            TextClass.AppendLine("        /// <summary>");
            TextClass.AppendLine("        /// Método genérico que retorna uma lista de entidades, mediante uma expressão lambda");
            TextClass.AppendLine("        /// </summary>");
            TextClass.AppendLine("        public virtual List<TEntity> GetList(Expression<Func<TEntity, bool>> filter = null, Func<IQueryable<TEntity>, IOrderedQueryable<TEntity>> orderBy = null)");
            TextClass.AppendLine("        {");
            TextClass.AppendLine("            try");
            TextClass.AppendLine("            {");
            TextClass.AppendLine("                IQueryable<TEntity> query = _dbSet;");
            TextClass.AppendLine("");
            TextClass.AppendLine("                if (filter != null)");
            TextClass.AppendLine("                    query = query.Where(filter);");
            TextClass.AppendLine("");
            TextClass.AppendLine("                if (orderBy != null)");
            TextClass.AppendLine("                    return orderBy(query).ToList();");
            TextClass.AppendLine("                else");
            TextClass.AppendLine("                    return query.ToList();");
            TextClass.AppendLine("            }");
            TextClass.AppendLine("            catch (System.Data.Entity.Validation.DbEntityValidationException ex)");
            TextClass.AppendLine("            {");
            TextClass.AppendLine("                StringBuilder sb = new StringBuilder();");
            TextClass.AppendLine("");
            TextClass.AppendLine("                foreach (var failure in ex.EntityValidationErrors)");
            TextClass.AppendLine("                {");
            TextClass.AppendLine("                    sb.AppendFormat(\"{0} Falha da validação \", failure.Entry.Entity.GetType());");
            TextClass.AppendLine("                    foreach (var error in failure.ValidationErrors)");
            TextClass.AppendLine("                    {");
            TextClass.AppendLine("                        sb.AppendFormat(\"- {0} : {1}\", error.PropertyName, error.ErrorMessage);");
            TextClass.AppendLine("                        sb.AppendLine();");
            TextClass.AppendLine("                    }");
            TextClass.AppendLine("                }");
            TextClass.AppendLine("");
            TextClass.AppendLine("                throw new System.Data.Entity.Validation.DbEntityValidationException(\"Erros da validação da Entidade: \" + sb.ToString(), ex);");
            TextClass.AppendLine("");
            TextClass.AppendLine("            }");
            TextClass.AppendLine("        }");
            TextClass.AppendLine("");
            TextClass.AppendLine("        /// <summary>");
            TextClass.AppendLine("        /// Método genérico que exclui o objeto da memória Garbage Collector");
            TextClass.AppendLine("        /// </summary>");
            TextClass.AppendLine("        public void Dispose()");
            TextClass.AppendLine("        {");
            TextClass.AppendLine("            _dbSet = null;");
            TextClass.AppendLine("            _context.Dispose();");
            TextClass.AppendLine("            GC.SuppressFinalize(this);");
            TextClass.AppendLine("        }");
            TextClass.AppendLine("    }");
            TextClass.AppendLine("}");
            //--

            var FileName = "Repository.cs";
            var FullFile = FilePath + @"\Repository\" + FileName;
             
            DirectoryInfo Directory = new DirectoryInfo(FilePath + @"\Repository");

            if (!Directory.Exists)
                Directory.Create();

            using (TextWriter Writer = File.CreateText(FullFile))
            {
                Writer.WriteLine(TextClass.ToString());
            }

            TextClass.Clear();

            return ("> " + FileName);
        }

        public string BuildUnitOfWork(Dictionary<string, ClassConfig> GroupTables)
        {
            var FilePath = ConfigurationManager.AppSettings["OutputClassDataUnitOfWork"].ToString();
            var OutputClassDataContextName = ConfigurationManager.AppSettings["OutputClassDataContextName"].ToString();
            var ProjectName = ConfigurationManager.AppSettings["ProjetName"].ToString(); 
            var SufixoModels = "Dto";

            //--
            TextClass = new StringBuilder();

            TextClass.AppendLine("using System;");
            TextClass.AppendLine("using System.Collections.Generic;");
            TextClass.AppendLine("using System.Linq;");
            TextClass.AppendLine("using System.Text;");
            TextClass.AppendLine("using System.Threading.Tasks;");
            TextClass.AppendLine("using " + ProjectName + ".Dto;");
            TextClass.AppendLine("using " + ProjectName + ".Data.Generic;");
            TextClass.AppendLine("using " + ProjectName + ".Data.Context;");
            TextClass.AppendLine("");
            TextClass.AppendLine("namespace " + ProjectName + ".Data");
            TextClass.AppendLine("{");
            TextClass.AppendLine("    public class UnitOfWork : IDisposable");
            TextClass.AppendLine("    {");
            TextClass.AppendLine("        private " + OutputClassDataContextName + " _context = new " + OutputClassDataContextName + "();");
            TextClass.AppendLine("        private bool _disposed = false;");
            TextClass.AppendLine("");

            foreach (var item in GroupTables)
            {
                var Model = item.Value;

                TextClass.AppendLine("        private Repository<" + Model.ClassName + SufixoModels + "> _" + Model.ClassName.ToLower() + "Repository;");
            }

            TextClass.AppendLine("");

            foreach (var item in GroupTables)
            {
                var Model = item.Value;

                TextClass.AppendLine("        public Repository<" + Model.ClassName + SufixoModels + "> " + Model.ClassName + "Repository");
                TextClass.AppendLine("        {");
                TextClass.AppendLine("            get");
                TextClass.AppendLine("            {");
                TextClass.AppendLine("                if (this._" + Model.ClassName.ToLower() + "Repository == null)");
                TextClass.AppendLine("                {");
                TextClass.AppendLine("                    this._" + Model.ClassName.ToLower() + "Repository = new Repository<" + Model.ClassName + SufixoModels + ">(_context);");
                TextClass.AppendLine("                }");
                TextClass.AppendLine("");
                TextClass.AppendLine("                return _" + Model.ClassName.ToLower() + "Repository;");
                TextClass.AppendLine("            }");
                TextClass.AppendLine("        }");
                TextClass.AppendLine("");
            }

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
            TextClass.AppendLine("                    _context.Dispose();");

            foreach (var item in GroupTables)
            {
                var Model = item.Value;
                TextClass.AppendLine("                    _" + Model.ClassName.ToLower() + "Repository = null;");
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

        public string BuildBase()
        {
            var FilePath = ConfigurationManager.AppSettings["OutputClassDataModels"].ToString();
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









        public string BuildModelDataAnnotations(KeyValuePair<string, ClassConfig> TableSetting)
        {
            var FilePath = ConfigurationManager.AppSettings["OutputClassDataModels"].ToString();
            var SufixoModels = "Dto";
            var TableName = TableSetting.Key;
            var DataModel = TableSetting.Value;
            var TableSchema = Functions.GetTableSchema(TableName, DatabaseType);

            //--
            TextClass = new StringBuilder();

            TextClass.AppendLine("using System;");
            TextClass.AppendLine("using System.Text;");
            TextClass.AppendLine("using System.Collections;");
            TextClass.AppendLine("using System.Collections.Generic;");
            TextClass.AppendLine("using System.Linq;");
            TextClass.AppendLine("using System.ComponentModel.DataAnnotations;");
            TextClass.AppendLine("using System.Threading.Tasks;");
            TextClass.AppendLine("");
            TextClass.AppendLine("namespace " + DataModel.NameSpaceDto);
            TextClass.AppendLine("{");
            TextClass.AppendLine("");
            TextClass.AppendLine("    [MetadataType(typeof(" + DataModel.ClassName + SufixoModels + "Validator))]");
            TextClass.AppendLine("    public class " + DataModel.ClassName + SufixoModels + " : Base");
            TextClass.AppendLine("    {");

            var ColumnDataType = String.Empty;

            foreach (var ColumnMapper in TableSchema.CollectionColumn)
            {
                if (ColumnMapper.ColumnKey != "pk")
                {
                    if (ColumnMapper.DataType == "datetime")
                    {
                        if (ColumnMapper.IsNullable == "yes")
                        {
                            ColumnDataType = Functions.GetColumnType(ColumnMapper.DataType);
                            TextClass.AppendLine("        public Nullable<System.DateTime> " + ColumnMapper.ColumnName + " { get; set; }");
                        }
                        else
                        {
                            ColumnDataType = Functions.GetColumnType(ColumnMapper.DataType);
                            TextClass.AppendLine("        public " + ColumnDataType + " " + ColumnMapper.ColumnName + " { get; set; }");
                        }
                    }
                    else if (ColumnMapper.DataType == "int")
                    {
                        if (ColumnMapper.IsNullable == "yes")
                        {
                            ColumnDataType = Functions.GetColumnType(ColumnMapper.DataType);
                            TextClass.AppendLine("        public Nullable<System.Int32> " + ColumnMapper.ColumnName + " { get; set; }");
                        }
                        else
                        {
                            ColumnDataType = Functions.GetColumnType(ColumnMapper.DataType);
                            TextClass.AppendLine("        public " + ColumnDataType + " " + ColumnMapper.ColumnName + " { get; set; }");
                        }
                    }
                    else if (ColumnMapper.DataType == "decimal")
                    {
                        if (ColumnMapper.IsNullable == "yes")
                        {
                            ColumnDataType = Functions.GetColumnType(ColumnMapper.DataType);
                            TextClass.AppendLine("        public Nullable<System.Decimal> " + ColumnMapper.ColumnName + " { get; set; }");
                        }
                        else
                        {
                            ColumnDataType = Functions.GetColumnType(ColumnMapper.DataType);
                            TextClass.AppendLine("        public " + ColumnDataType + " " + ColumnMapper.ColumnName + " { get; set; }");
                        }
                    }
                    else if (ColumnMapper.DataType == "long")
                    {
                        if (ColumnMapper.IsNullable == "yes")
                        {
                            ColumnDataType = Functions.GetColumnType(ColumnMapper.DataType);
                            TextClass.AppendLine("        public Nullable<System.long> " + ColumnMapper.ColumnName + " { get; set; }");
                        }
                        else
                        {
                            ColumnDataType = Functions.GetColumnType(ColumnMapper.DataType);
                            TextClass.AppendLine("        public " + ColumnDataType + " " + ColumnMapper.ColumnName + " { get; set; }");
                        }
                    }
                    else
                    {
                        ColumnDataType = Functions.GetColumnType(ColumnMapper.DataType);
                        TextClass.AppendLine("        public " + ColumnDataType + " " + ColumnMapper.ColumnName + " { get; set; }");
                    }
                }                   
            }

            TextClass.AppendLine("    }");
            TextClass.AppendLine("}");
            //--  

            var Diretory = FilePath + @"\Models\" + DataModel.ClassName;
            var FileName = DataModel.ClassName + SufixoModels + ".cs";
            var FullFile = Diretory + @"\" + FileName;

            DirectoryInfo Directory = new DirectoryInfo(Diretory);

            if (!Directory.Exists)
                Directory.Create();

            using (TextWriter Writer = File.CreateText(FullFile))
            {
                Writer.WriteLine(TextClass.ToString());
            }

            TextClass.Clear();

            return ("> " + FileName);
        }

        public string BuildDataAnnotations(KeyValuePair<string, ClassConfig> TableSetting)
        {
            var FilePath = ConfigurationManager.AppSettings["OutputClassDataModels"].ToString();
            var SufixoModels = "DtoValidator";
            var TableName = TableSetting.Key;
            var DataModel = TableSetting.Value;
            var TableSchema = Functions.GetTableSchema(TableName, DatabaseType);

            //--
            TextClass = new StringBuilder();

            TextClass.AppendLine("using System;");
            TextClass.AppendLine("using System.Text;");
            TextClass.AppendLine("using System.Collections;");
            TextClass.AppendLine("using System.Collections.Generic;");
            TextClass.AppendLine("using System.Linq;");
            TextClass.AppendLine("using System.ComponentModel.DataAnnotations;");
            TextClass.AppendLine("using System.Threading.Tasks;");
            TextClass.AppendLine("");
            TextClass.AppendLine("namespace " + DataModel.NameSpaceDto);
            TextClass.AppendLine("{");
            TextClass.AppendLine("    public class " + DataModel.ClassName + SufixoModels + " : Base");
            TextClass.AppendLine("    {");

            var ColumnDataType = String.Empty;

            foreach (var ColumnMapper in TableSchema.CollectionColumn)
            {
                if (ColumnMapper.ColumnKey != "pk")
                {
                    ColumnDataType = Functions.GetColumnType(ColumnMapper.DataType);

                    if (ColumnMapper.IsNullable == "no")
                    {
                        TextClass.AppendLine("        [Required(ErrorMessage = \"Este campo é obrigatório.\")]");

                        if (ColumnMapper.MaxLenght != String.Empty)
                        {
                            TextClass.AppendLine("        [StringLength(" + ColumnMapper.MaxLenght + ", ErrorMessage = \"A descrição deve ter no máximo " + ColumnMapper.MaxLenght + " caracteres.\")]");
                        }                        
                        
                        TextClass.AppendLine("        public " + ColumnDataType + " " + ColumnMapper.ColumnName + " { get; set; }");
                        TextClass.AppendLine("");
                    }  
                    // Campos não requerido nao precisao ir no Metadata class
                    //else if (ColumnMapper.IsNullable == "yes")  
                    //{
                    //    TextClass.AppendLine("        [Exclude]");
                    //    TextClass.AppendLine("        public " + ColumnDataType + " " + ColumnMapper.ColumnName + " { get; set; }");
                    //}                     
                }
            }

            TextClass.AppendLine("    }");
            TextClass.AppendLine("}");
            //--  

            var Diretory = FilePath + @"\Models\" + DataModel.ClassName;
            var FileName = DataModel.ClassName + SufixoModels + ".cs";
            var FullFile = Diretory + @"\" + FileName;

            DirectoryInfo Directory = new DirectoryInfo(Diretory);

            if (!Directory.Exists)
                Directory.Create();

            using (TextWriter Writer = File.CreateText(FullFile))
            {
                Writer.WriteLine(TextClass.ToString());
            }

            TextClass.Clear();

            return ("> " + FileName);
        }
    }
}
