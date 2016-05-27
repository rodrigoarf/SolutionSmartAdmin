using System;
using System.Collections.Generic;
using System.Data.Entity.ModelConfiguration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SmartAdmin.Data.Model
{
    public class NoticiaPublicadorMapper : EntityTypeConfiguration<NoticiaPublicadorDto>
    {
        public NoticiaPublicadorMapper()
        {
            // Primary Key
            this.HasKey(t => t.ID);

            // Propertys Required
            this.Property(_ => _.NOME).IsRequired().HasMaxLength(100);
            this.Property(_ => _.EMAIL).IsRequired().HasMaxLength(100);
            this.Property(_ => _.STATUS).IsRequired().HasMaxLength(1);

            // Table & Column Mappings
            this.ToTable("NOTICIA_PUBLICADOR", "smartadmin");

            // Propertys Relationship Database Table Columns
            this.Property(_ => _.ID).HasColumnName("COD_PUBLICADOR");
            this.Property(_ => _.NOME).HasColumnName("NOME");
            this.Property(_ => _.EMAIL).HasColumnName("EMAIL");
            this.Property(_ => _.STATUS).HasColumnName("STATUS");
        }
    }
}

