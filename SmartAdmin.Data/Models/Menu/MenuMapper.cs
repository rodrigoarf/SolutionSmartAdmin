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
            this.Property(_ => _.COD_SUBMENU);
            this.Property(_ => _.NOME).IsRequired().HasMaxLength(100);
            this.Property(_ => _.CONTROLLER).IsRequired().HasMaxLength(50);
            this.Property(_ => _.ACTION).HasMaxLength(50);
            this.Property(_ => _.ICON).IsRequired().HasMaxLength(100);
            this.Property(_ => _.STATUS).IsRequired().HasMaxLength(1);

            // Table & Column Mappings
            this.ToTable("SCP_MENU", "cliniccenter");

            this.Property(_ => _.ID).HasColumnName("COD_MENU");
            this.Property(_ => _.COD_SUBMENU).HasColumnName("COD_SUBMENU");
            this.Property(_ => _.NOME).HasColumnName("NOME");
            this.Property(_ => _.CONTROLLER).HasColumnName("CONTROLLER");
            this.Property(_ => _.ACTION).HasColumnName("ACTION");
            this.Property(_ => _.ICON).HasColumnName("ICON");
            this.Property(_ => _.STATUS).HasColumnName("STATUS");
        }
    }
}

