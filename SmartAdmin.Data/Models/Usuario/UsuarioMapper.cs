using System;
using System.Text;
using System.Collections;
using System.Collections.Generic;
using System.Data.Entity.ModelConfiguration;
using SmartAdmin.Dto;

namespace SmartAdmin.Data.Mapper
{
    public class UsuarioMapper : EntityTypeConfiguration<UsuarioDto>
    {
        public UsuarioMapper()
        {
            // Primary Key
            this.HasKey(t => t.ID);

            // Propertys
            this.Property(_ => _.NOME).IsRequired().HasMaxLength(100);
            this.Property(_ => _.USUARIO).IsRequired().HasMaxLength(30);
            this.Property(_ => _.SENHA).IsRequired().HasMaxLength(100);
            this.Property(_ => _.EMAIL).IsRequired().HasMaxLength(100);
            this.Property(_ => _.DTH_CRIACAO).IsRequired();
            this.Property(_ => _.USR_MASTER).IsRequired().HasMaxLength(1);
            this.Property(_ => _.STATUS).IsRequired().HasMaxLength(1);
            this.Property(_ => _.SEXO).IsRequired().HasMaxLength(1);

            // Table & Column Mappings
            this.ToTable("SCP_USUARIO", "cliniccenter");

            this.Property(_ => _.ID).HasColumnName("COD_USUARIO");
            this.Property(_ => _.NOME).HasColumnName("NOME");
            this.Property(_ => _.USUARIO).HasColumnName("USUARIO");
            this.Property(_ => _.SENHA).HasColumnName("SENHA");
            this.Property(_ => _.EMAIL).HasColumnName("EMAIL");
            this.Property(_ => _.DTH_CRIACAO).HasColumnName("DTH_CRIACAO");
            this.Property(_ => _.USR_MASTER).HasColumnName("USR_MASTER");
            this.Property(_ => _.STATUS).HasColumnName("STATUS");
            this.Property(_ => _.SEXO).HasColumnName("SEXO");
        }
    }
}

