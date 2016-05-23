using System;
using System.Collections.Generic;
using System.Data.Entity.ModelConfiguration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SmartAdmin.Data.Model
{
    public class MenuMapper : EntityTypeConfiguration<MenuDto>
    {
        public MenuMapper()
        {
            // Primary Key
            this.HasKey(t => t.ID);

            // Propertys Required
            this.Property(_ => _.COD_MENU_PAI);
            this.Property(_ => _.NOME).IsRequired().HasMaxLength(100);
            this.Property(_ => _.CONTROLLER).IsRequired().HasMaxLength(100);
            this.Property(_ => _.ACTION).IsRequired().HasMaxLength(100);
            this.Property(_ => _.DESCRICAO).IsRequired().HasMaxLength(65535);
            this.Property(_ => _.DTH_CADASTRO);
            this.Property(_ => _.ICONE).IsRequired().HasMaxLength(100);
            this.Property(_ => _.STATUS).IsRequired().HasMaxLength(1);

            // Table & Column Mappings
            this.ToTable("MENU", "smartadmin");

            // Propertys Relationship Database Table Columns
            this.Property(_ => _.ID).HasColumnName("COD_MENU");
            this.Property(_ => _.COD_MENU_PAI).HasColumnName("COD_MENU_PAI");
            this.Property(_ => _.NOME).HasColumnName("NOME");
            this.Property(_ => _.CONTROLLER).HasColumnName("CONTROLLER");
            this.Property(_ => _.ACTION).HasColumnName("ACTION");
            this.Property(_ => _.DESCRICAO).HasColumnName("DESCRICAO");
            this.Property(_ => _.DTH_CADASTRO).HasColumnName("DTH_CADASTRO");
            this.Property(_ => _.ICONE).HasColumnName("ICONE");
            this.Property(_ => _.STATUS).HasColumnName("STATUS");
        }
    }
}

