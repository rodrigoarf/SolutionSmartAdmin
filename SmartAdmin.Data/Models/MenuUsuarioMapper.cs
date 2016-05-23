using System;
using System.Collections.Generic;
using System.Data.Entity.ModelConfiguration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SmartAdmin.Data.Model
{
    public class MenuUsuarioMapper : EntityTypeConfiguration<MenuUsuarioDto>
    {
        public MenuUsuarioMapper()
        {
            // Primary Key
            this.HasKey(t => t.ID);

            // Propertys Required
            this.Property(_ => _.COD_MENU).IsRequired();
            this.Property(_ => _.COD_USUARIO).IsRequired();

            // Table & Column Mappings
            this.ToTable("MENU_USUARIO", "smartadmin");

            // Propertys Relationship Database Table Columns
            this.Property(_ => _.ID).HasColumnName("COD_RELACAO");
            this.Property(_ => _.COD_MENU).HasColumnName("COD_MENU");
            this.Property(_ => _.COD_USUARIO).HasColumnName("COD_USUARIO");
        }
    }
}

