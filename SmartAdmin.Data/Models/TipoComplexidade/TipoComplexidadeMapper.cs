using System;
using System.Text;
using System.Collections;
using System.Collections.Generic;
using System.Data.Entity.ModelConfiguration;
using SmartAdmin.Dto;

namespace SmartAdmin.Data.Mapper
{
    public class TipoComplexidadeMapper : EntityTypeConfiguration<TipoComplexidadeDto>
    {
        public TipoComplexidadeMapper()
        {
            // Primary Key
            this.HasKey(t => t.ID);

            // Propertys
            this.Property(_ => _.NOME).IsRequired().HasMaxLength(100);
            this.Property(_ => _.PARAMET_INICIAL);
            this.Property(_ => _.PARAMET_FINAL);
            this.Property(_ => _.STATUS).IsRequired().HasMaxLength(1);

            // Table & Column Mappings
            this.ToTable("SCP_TIPO_COMPLEXIDADE", "cliniccenter");

            this.Property(_ => _.ID).HasColumnName("TIPO_COMPLEXIDADE");
            this.Property(_ => _.NOME).HasColumnName("NOME");
            this.Property(_ => _.PARAMET_INICIAL).HasColumnName("PARAMET_INICIAL");
            this.Property(_ => _.PARAMET_FINAL).HasColumnName("PARAMET_FINAL");
            this.Property(_ => _.STATUS).HasColumnName("STATUS");
        }
    }
}

