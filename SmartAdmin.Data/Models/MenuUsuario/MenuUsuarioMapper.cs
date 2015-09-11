using System;
using System.Text;
using System.Collections;
using System.Collections.Generic;
using System.Data.Entity.ModelConfiguration;
using SmartAdmin.Dto;

namespace SmartAdmin.Data.Mapper
{
    public class MenuUsuarioMapper : EntityTypeConfiguration<MenuUsuarioDto>
    {
        public MenuUsuarioMapper()
        {
            // Primary Key
            this.HasKey(t => t.ID);

            // Propertys
            this.Property(_ => _.COD_MENU).IsRequired();
            this.Property(_ => _.COD_USUARIO).IsRequired();

            // Table & Column Mappings
            this.ToTable("MENU_USUARIO", "agilecore1");

            this.Property(_ => _.ID).HasColumnName("COD_RELACAO");
            this.Property(_ => _.COD_MENU).HasColumnName("COD_MENU");
            this.Property(_ => _.COD_USUARIO).HasColumnName("COD_USUARIO");
        }
    }
}

