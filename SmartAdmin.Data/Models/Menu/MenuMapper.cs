using System;
using System.Text;
using System.Collections;
using System.Collections.Generic;
using System.Data.Entity.ModelConfiguration;
using SmartAdmin.Dto;

namespace SmartAdmin.Data.Mapper
{
    public class MenuMapper : EntityTypeConfiguration<MenuDto>
    {
        public MenuMapper()
        {
            // Primary Key
            this.HasKey(t => t.ID);

            // Propertys
            this.Property(_ => _.COD_MENU_PAI);
            this.Property(_ => _.NOME).IsRequired().HasMaxLength(100);
            this.Property(_ => _.CONTROLLER).HasMaxLength(100);
            this.Property(_ => _.ACTION).HasMaxLength(100);
            this.Property(_ => _.DESCRICAO).HasMaxLength(65535);
            this.Property(_ => _.DTH_CADASTRO);
            this.Property(_ => _.ICONE).HasMaxLength(100);
            this.Property(_ => _.STATUS).IsRequired().HasMaxLength(1);

            // Table & Column Mappings
            this.ToTable("MENU", "agilecore1");

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

