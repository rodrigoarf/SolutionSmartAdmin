using System;
using System.Text;
using System.Collections;
using System.Collections.Generic;
using System.Data.Entity.ModelConfiguration;
using SmartAdmin.Dto;

namespace SmartAdmin.Data.Mapper
{
    public class ContatoMapper : EntityTypeConfiguration<ContatoDto>
    {
        public ContatoMapper()
        {
            // Primary Key
            this.HasKey(t => t.ID);

            // Propertys
            this.Property(_ => _.COD_TIPO_CONTATO);
            this.Property(_ => _.NOME).IsRequired().HasMaxLength(100);
            this.Property(_ => _.EMAIL).IsRequired().HasMaxLength(100);
            this.Property(_ => _.DESCRICAO).HasMaxLength(65535);
            this.Property(_ => _.DTH_CONTATO);
            this.Property(_ => _.DTH_LEITURA);
            this.Property(_ => _.STATUS).IsRequired().HasMaxLength(1);

            // Table & Column Mappings
            this.ToTable("CONTATO", "agilecore1");

            this.Property(_ => _.ID).HasColumnName("COD_CONTATO");
            this.Property(_ => _.COD_TIPO_CONTATO).HasColumnName("COD_TIPO_CONTATO");
            this.Property(_ => _.NOME).HasColumnName("NOME");
            this.Property(_ => _.EMAIL).HasColumnName("EMAIL");
            this.Property(_ => _.DESCRICAO).HasColumnName("DESCRICAO");
            this.Property(_ => _.DTH_CONTATO).HasColumnName("DTH_CONTATO");
            this.Property(_ => _.DTH_LEITURA).HasColumnName("DTH_LEITURA");
            this.Property(_ => _.STATUS).HasColumnName("STATUS");
        }
    }
}

