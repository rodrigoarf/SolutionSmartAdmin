using System;
using System.Text;
using System.Collections;
using System.Collections.Generic;
using System.Data.Entity.ModelConfiguration;
using SmartAdmin.Dto;

namespace SmartAdmin.Data.Mapper
{
    public class CedenteMapper : EntityTypeConfiguration<CedenteDto>
    {
        public CedenteMapper()
        {
            // Primary Key
            this.HasKey(t => t.ID);

            // Propertys
            this.Property(_ => _.COD_BANCO).IsRequired();
            this.Property(_ => _.NOME).IsRequired().HasMaxLength(100);
            this.Property(_ => _.ENDERECO).IsRequired().HasMaxLength(100);
            this.Property(_ => _.NUMERO);
            this.Property(_ => _.BAIRRO).IsRequired().HasMaxLength(100);
            this.Property(_ => _.CIDADE).IsRequired().HasMaxLength(100);
            this.Property(_ => _.UF).IsRequired().HasMaxLength(2);
            this.Property(_ => _.CEP).HasMaxLength(10);
            this.Property(_ => _.DTH_NASCIMENTO).IsRequired();
            this.Property(_ => _.CPF_CNPJ).HasMaxLength(30);
            this.Property(_ => _.INSTRUCAO_BOLETO).HasMaxLength(300);
            this.Property(_ => _.NUM_AGENCIA).IsRequired().HasMaxLength(10);
            this.Property(_ => _.NUM_CONTA_CORRENTE).IsRequired().HasMaxLength(10);
            this.Property(_ => _.STATUS).IsRequired().HasMaxLength(1);

            // Table & Column Mappings
            this.ToTable("CEDENTE", "agilecore1");

            this.Property(_ => _.ID).HasColumnName("COD_CEDENTE");
            this.Property(_ => _.COD_BANCO).HasColumnName("COD_BANCO");
            this.Property(_ => _.NOME).HasColumnName("NOME");
            this.Property(_ => _.ENDERECO).HasColumnName("ENDERECO");
            this.Property(_ => _.NUMERO).HasColumnName("NUMERO");
            this.Property(_ => _.BAIRRO).HasColumnName("BAIRRO");
            this.Property(_ => _.CIDADE).HasColumnName("CIDADE");
            this.Property(_ => _.UF).HasColumnName("UF");
            this.Property(_ => _.CEP).HasColumnName("CEP");
            this.Property(_ => _.DTH_NASCIMENTO).HasColumnName("DTH_NASCIMENTO");
            this.Property(_ => _.CPF_CNPJ).HasColumnName("CPF_CNPJ");
            this.Property(_ => _.INSTRUCAO_BOLETO).HasColumnName("INSTRUCAO_BOLETO");
            this.Property(_ => _.NUM_AGENCIA).HasColumnName("NUM_AGENCIA");
            this.Property(_ => _.NUM_CONTA_CORRENTE).HasColumnName("NUM_CONTA_CORRENTE");
            this.Property(_ => _.STATUS).HasColumnName("STATUS");
        }
    }
}

