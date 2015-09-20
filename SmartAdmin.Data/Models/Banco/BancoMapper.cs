using System;
using System.Text;
using System.Collections;
using System.Collections.Generic;
using System.Data.Entity.ModelConfiguration;
using SmartAdmin.Dto;

namespace SmartAdmin.Data.Mapper
{
    public class BancoMapper : EntityTypeConfiguration<BancoDto>
    {
        public BancoMapper()
        {
            // Primary Key
            this.HasKey(t => t.ID);

            // Propertys
            this.Property(_ => _.NOME).IsRequired().HasMaxLength(100);
            this.Property(_ => _.WEBSITE).HasMaxLength(100);
            this.Property(_ => _.SIGLA).HasMaxLength(100);
            this.Property(_ => _.FLAG_TIPO).HasMaxLength(1);
            this.Property(_ => _.STATUS).IsRequired().HasMaxLength(1);

            // Table & Column Mappings
            this.ToTable("BANCO", "agilecore1");

            this.Property(_ => _.ID).HasColumnName("COD_BANCO");
            this.Property(_ => _.NOME).HasColumnName("NOME");
            this.Property(_ => _.WEBSITE).HasColumnName("WEBSITE");
            this.Property(_ => _.SIGLA).HasColumnName("SIGLA");
            this.Property(_ => _.FLAG_TIPO).HasColumnName("FLAG_TIPO");
            this.Property(_ => _.STATUS).HasColumnName("STATUS");
        }
    }
}

