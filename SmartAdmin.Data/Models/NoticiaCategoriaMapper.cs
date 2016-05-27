using System;
using System.Collections.Generic;
using System.Data.Entity.ModelConfiguration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SmartAdmin.Data.Model
{
    public class NoticiaCategoriaMapper : EntityTypeConfiguration<NoticiaCategoriaDto>
    {
        public NoticiaCategoriaMapper()
        {
            // Primary Key
            this.HasKey(t => t.ID);

            // Propertys Required
            this.Property(_ => _.TITULO).IsRequired().HasMaxLength(100);
            this.Property(_ => _.STATUS).IsRequired().HasMaxLength(1);

            // Table & Column Mappings
            this.ToTable("NOTICIA_CATEGORIA", "smartadmin");

            // Propertys Relationship Database Table Columns
            this.Property(_ => _.ID).HasColumnName("COD_CATEGORIA");
            this.Property(_ => _.TITULO).HasColumnName("TITULO");
            this.Property(_ => _.STATUS).HasColumnName("STATUS");
        }
    }
}

