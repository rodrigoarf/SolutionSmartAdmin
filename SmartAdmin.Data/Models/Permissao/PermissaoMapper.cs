using System;
using System.Text;
using System.Collections;
using System.Collections.Generic;
using System.Data.Entity.ModelConfiguration;
using SmartAdmin.Dto;

namespace SmartAdmin.Data.Mapper
{
    public class PermissaoMapper : EntityTypeConfiguration<PermissaoDto>
    {
        public PermissaoMapper()
        {
            // Primary Key
            this.HasKey(t => t.ID);

            // Propertys
            this.Property(_ => _.COD_USUARIO).IsRequired();
            this.Property(_ => _.COD_MENU).IsRequired();

            // Table & Column Mappings
            this.ToTable("SCP_PERM_USUARIO", "cliniccenter");

            this.Property(_ => _.COD_USUARIO).HasColumnName("COD_USUARIO");
            this.Property(_ => _.COD_MENU).HasColumnName("COD_MENU");
        }
    }
}

