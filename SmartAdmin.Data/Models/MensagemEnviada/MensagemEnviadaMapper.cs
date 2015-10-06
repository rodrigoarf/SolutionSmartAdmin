using System;
using System.Text;
using System.Collections;
using System.Collections.Generic;
using System.Data.Entity.ModelConfiguration;
using SmartAdmin.Dto;

namespace SmartAdmin.Data.Mapper
{
    public class MensagemEnviadaMapper : EntityTypeConfiguration<MensagemEnviadaDto>
    {
        public MensagemEnviadaMapper()
        {
            // Primary Key
            this.HasKey(t => t.ID);

            // Propertys
            this.Property(_ => _.COD_MENSAGEM).IsRequired();
            this.Property(_ => _.COD_AUTOR).IsRequired();
            this.Property(_ => _.COD_REMETENTE).IsRequired();
            this.Property(_ => _.STATUS_AUTOR).IsRequired().HasMaxLength(1);
            this.Property(_ => _.STATUS_REMETENTE).IsRequired().HasMaxLength(1);

            // Table & Column Mappings
            this.ToTable("MENSAGEM_ENVIADA", "agilecore1");

            this.Property(_ => _.ID).HasColumnName("COD_ENVIO");
            this.Property(_ => _.COD_MENSAGEM).HasColumnName("COD_MENSAGEM");
            this.Property(_ => _.COD_AUTOR).HasColumnName("COD_AUTOR");
            this.Property(_ => _.COD_REMETENTE).HasColumnName("COD_REMETENTE");
            this.Property(_ => _.STATUS_AUTOR).HasColumnName("STATUS_AUTOR");
            this.Property(_ => _.STATUS_REMETENTE).HasColumnName("STATUS_REMETENTE");
        }
    }
}

