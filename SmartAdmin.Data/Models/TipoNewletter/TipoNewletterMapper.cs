using System;
using System.Text;
using System.Collections;
using System.Collections.Generic;
using System.Data.Entity.ModelConfiguration;
using SmartAdmin.Dto;

namespace SmartAdmin.Data.Mapper
{
    public class TipoNewletterMapper : EntityTypeConfiguration<TipoNewletterDto>
    {
        public TipoNewletterMapper()
        {
            // Primary Key
            this.HasKey(t => t.ID);

            // Propertys
            this.Property(_ => _.NOME).IsRequired().HasMaxLength(100);
            this.Property(_ => _.DTH_CRIACAO);
            this.Property(_ => _.STATUS).IsRequired().HasMaxLength(1);

            // Table & Column Mappings
            this.ToTable("TIPO_NEWSLETTER", "agilecore1");

            this.Property(_ => _.ID).HasColumnName("COD_TIPO");
            this.Property(_ => _.NOME).HasColumnName("NOME");
            this.Property(_ => _.DTH_CRIACAO).HasColumnName("DTH_CRIACAO");
            this.Property(_ => _.STATUS).HasColumnName("STATUS");
        }
    }
}
