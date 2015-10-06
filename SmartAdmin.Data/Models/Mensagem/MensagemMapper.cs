using System;
using System.Text;
using System.Collections;
using System.Collections.Generic;
using System.Data.Entity.ModelConfiguration;
using SmartAdmin.Dto;

namespace SmartAdmin.Data.Mapper
{
    public class MensagemMapper : EntityTypeConfiguration<MensagemDto>
    {
        public MensagemMapper()
        {
            // Primary Key
            this.HasKey(t => t.ID);

            // Propertys
            this.Property(_ => _.COD_AUTOR).IsRequired();
            this.Property(_ => _.TITULO).IsRequired().HasMaxLength(100);
            this.Property(_ => _.TEXTO).IsRequired().HasMaxLength(65535);
            this.Property(_ => _.DTH_CRIACAO);
            this.Property(_ => _.DTH_ENVIO);

            // Table & Column Mappings
            this.ToTable("MENSAGEM", "agilecore1");

            this.Property(_ => _.ID).HasColumnName("COD_MENSAGEM");
            this.Property(_ => _.COD_AUTOR).HasColumnName("COD_AUTOR");
            this.Property(_ => _.TITULO).HasColumnName("TITULO");
            this.Property(_ => _.TEXTO).HasColumnName("TEXTO");
            this.Property(_ => _.DTH_CRIACAO).HasColumnName("DTH_CRIACAO");
            this.Property(_ => _.DTH_ENVIO).HasColumnName("DTH_ENVIO");
        }
    }
}

