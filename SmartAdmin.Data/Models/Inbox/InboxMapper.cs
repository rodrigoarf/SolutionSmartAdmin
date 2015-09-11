using System;
using System.Text;
using System.Collections;
using System.Collections.Generic;
using System.Data.Entity.ModelConfiguration;
using SmartAdmin.Dto;

namespace SmartAdmin.Data.Mapper
{
    public class InboxMapper : EntityTypeConfiguration<InboxDto>
    {
        public InboxMapper()
        {
            // Primary Key
            this.HasKey(t => t.ID);

            // Propertys
            this.Property(_ => _.COD_USUARIO).IsRequired();
            this.Property(_ => _.COD_USUARIO_REMETENTE).IsRequired();
            this.Property(_ => _.COD_LOCAL).IsRequired();
            this.Property(_ => _.TITULO).HasMaxLength(100);
            this.Property(_ => _.MENSAGEM).HasMaxLength(65535);
            this.Property(_ => _.DTH_ENVIO);
            this.Property(_ => _.DTH_LEITURA);

            // Table & Column Mappings
            this.ToTable("INBOX", "agilecore1");

            this.Property(_ => _.ID).HasColumnName("COD_INBOX");
            this.Property(_ => _.COD_USUARIO).HasColumnName("COD_USUARIO");
            this.Property(_ => _.COD_USUARIO_REMETENTE).HasColumnName("COD_USUARIO_REMETENTE");
            this.Property(_ => _.COD_LOCAL).HasColumnName("COD_LOCAL");
            this.Property(_ => _.TITULO).HasColumnName("TITULO");
            this.Property(_ => _.MENSAGEM).HasColumnName("MENSAGEM");
            this.Property(_ => _.DTH_ENVIO).HasColumnName("DTH_ENVIO");
            this.Property(_ => _.DTH_LEITURA).HasColumnName("DTH_LEITURA");
        }
    }
}

