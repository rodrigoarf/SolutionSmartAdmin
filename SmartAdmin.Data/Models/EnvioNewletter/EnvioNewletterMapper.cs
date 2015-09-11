using System;
using System.Text;
using System.Collections;
using System.Collections.Generic;
using System.Data.Entity.ModelConfiguration;
using SmartAdmin.Dto;

namespace SmartAdmin.Data.Mapper
{
    public class EnvioNewletterMapper : EntityTypeConfiguration<EnvioNewletterDto>
    {
        public EnvioNewletterMapper()
        {
            // Primary Key
            this.HasKey(t => t.ID);

            // Propertys
            this.Property(_ => _.COD_TIPO).IsRequired();
            this.Property(_ => _.NOME).IsRequired().HasMaxLength(100);
            this.Property(_ => _.HTML_BODY).HasMaxLength(65535);
            this.Property(_ => _.HTML_LINK).HasMaxLength(1000);
            this.Property(_ => _.DTH_ENVIO);
            this.Property(_ => _.DTH_CRIACAO);
            this.Property(_ => _.EXI_DESCADASTRAR).HasMaxLength(1);
            this.Property(_ => _.TOTAL_ENVIADOS);
            this.Property(_ => _.STATUS).IsRequired().HasMaxLength(1);

            // Table & Column Mappings
            this.ToTable("ENVIO_NEWSLETTER", "agilecore1");

            this.Property(_ => _.ID).HasColumnName("COD_ENVIO");
            this.Property(_ => _.COD_TIPO).HasColumnName("COD_TIPO");
            this.Property(_ => _.NOME).HasColumnName("NOME");
            this.Property(_ => _.HTML_BODY).HasColumnName("HTML_BODY");
            this.Property(_ => _.HTML_LINK).HasColumnName("HTML_LINK");
            this.Property(_ => _.DTH_ENVIO).HasColumnName("DTH_ENVIO");
            this.Property(_ => _.DTH_CRIACAO).HasColumnName("DTH_CRIACAO");
            this.Property(_ => _.EXI_DESCADASTRAR).HasColumnName("EXI_DESCADASTRAR");
            this.Property(_ => _.TOTAL_ENVIADOS).HasColumnName("TOTAL_ENVIADOS");
            this.Property(_ => _.STATUS).HasColumnName("STATUS");
        }
    }
}

