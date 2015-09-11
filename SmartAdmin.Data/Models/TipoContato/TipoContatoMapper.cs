using System;
using System.Text;
using System.Collections;
using System.Collections.Generic;
using System.Data.Entity.ModelConfiguration;
using SmartAdmin.Dto;

namespace SmartAdmin.Data.Mapper
{
    public class TipoContatoMapper : EntityTypeConfiguration<TipoContatoDto>
    {
        public TipoContatoMapper()
        {
            // Primary Key
            this.HasKey(t => t.ID);

            // Propertys
            this.Property(_ => _.NOME).IsRequired().HasMaxLength(100);
            this.Property(_ => _.DESCRICAO).HasMaxLength(200);
            this.Property(_ => _.STATUS).IsRequired().HasMaxLength(1);

            // Table & Column Mappings
            this.ToTable("TIPO_CONTATO", "agilecore1");

            this.Property(_ => _.ID).HasColumnName("COD_TIPO_CONTATO");
            this.Property(_ => _.NOME).HasColumnName("NOME");
            this.Property(_ => _.DESCRICAO).HasColumnName("DESCRICAO");
            this.Property(_ => _.STATUS).HasColumnName("STATUS");
        }
    }
}

