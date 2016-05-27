using System;
using System.Collections.Generic;
using System.Data.Entity.ModelConfiguration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SmartAdmin.Data.Model
{
    public class NoticiaMapper : EntityTypeConfiguration<NoticiaDto>
    {
        public NoticiaMapper()
        {
            // Primary Key
            this.HasKey(t => t.ID);

            // Propertys Required
            this.Property(_ => _.COD_CATEGORIA).IsRequired();
            this.Property(_ => _.COD_PUBLICADOR).IsRequired();
            this.Property(_ => _.TITULO).IsRequired().HasMaxLength(100);
            this.Property(_ => _.TEXTO).IsRequired().HasMaxLength(65535);
            this.Property(_ => _.DTH_CADASTRO);
            this.Property(_ => _.STATUS).IsRequired().HasMaxLength(1);

            // Table & Column Mappings
            this.ToTable("NOTICIA", "smartadmin");

            // Propertys Relationship Database Table Columns
            this.Property(_ => _.ID).HasColumnName("COD_NOTICIA");
            this.Property(_ => _.COD_CATEGORIA).HasColumnName("COD_CATEGORIA");
            this.Property(_ => _.COD_PUBLICADOR).HasColumnName("COD_PUBLICADOR");
            this.Property(_ => _.TITULO).HasColumnName("TITULO");
            this.Property(_ => _.TEXTO).HasColumnName("TEXTO");
            this.Property(_ => _.DTH_CADASTRO).HasColumnName("DTH_CADASTRO");
            this.Property(_ => _.STATUS).HasColumnName("STATUS");
        }
    }
}

