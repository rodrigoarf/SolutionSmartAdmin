using System;
using System.Text;
using System.Collections;
using System.Collections.Generic;
using System.Data.Entity.ModelConfiguration;
using SmartAdmin.Dto;

namespace SmartAdmin.Data.Mapper
{
    public class UsuarioMapper : EntityTypeConfiguration<UsuarioDto>
    {
        public UsuarioMapper()
        {
            // Primary Key
            this.HasKey(t => t.ID);

            // Propertys
            this.Property(_ => _.NOME).IsRequired().HasMaxLength(200);
            this.Property(_ => _.LOGIN).IsRequired().HasMaxLength(20);
            this.Property(_ => _.SENHA).HasMaxLength(100);
            this.Property(_ => _.EMAIL).IsRequired().HasMaxLength(100);
            this.Property(_ => _.DTH_CRIACAO);
            this.Property(_ => _.DTH_CANCELAMENTO);
            this.Property(_ => _.STATUS).IsRequired().HasMaxLength(1);
            this.Property(_ => _.SEXO).IsRequired().HasMaxLength(1);
            this.Property(_ => _.ENDERECO).IsRequired().HasMaxLength(200);
            this.Property(_ => _.CEP).HasMaxLength(10);
            this.Property(_ => _.TELEFONE).HasMaxLength(15);
            this.Property(_ => _.CELULAR).HasMaxLength(15);
            this.Property(_ => _.CIDADE).IsRequired().HasMaxLength(100);
            this.Property(_ => _.UF).IsRequired().HasMaxLength(2);
            this.Property(_ => _.BAIRRO).HasMaxLength(100);
            this.Property(_ => _.NUMERO);
            this.Property(_ => _.CPF_CNPJ).HasMaxLength(30);

            // Table & Column Mappings
            this.ToTable("USUARIO", "agilecore1");

            this.Property(_ => _.ID).HasColumnName("COD_USUARIO");
            this.Property(_ => _.NOME).HasColumnName("NOME");
            this.Property(_ => _.LOGIN).HasColumnName("LOGIN");
            this.Property(_ => _.SENHA).HasColumnName("SENHA");
            this.Property(_ => _.EMAIL).HasColumnName("EMAIL");
            this.Property(_ => _.DTH_CRIACAO).HasColumnName("DTH_CRIACAO");
            this.Property(_ => _.DTH_CANCELAMENTO).HasColumnName("DTH_CANCELAMENTO");
            this.Property(_ => _.STATUS).HasColumnName("STATUS");
            this.Property(_ => _.SEXO).HasColumnName("SEXO");
            this.Property(_ => _.ENDERECO).HasColumnName("ENDERECO");
            this.Property(_ => _.CEP).HasColumnName("CEP");
            this.Property(_ => _.TELEFONE).HasColumnName("TELEFONE");
            this.Property(_ => _.CELULAR).HasColumnName("CELULAR");
            this.Property(_ => _.CIDADE).HasColumnName("CIDADE");
            this.Property(_ => _.UF).HasColumnName("UF");
            this.Property(_ => _.BAIRRO).HasColumnName("BAIRRO");
            this.Property(_ => _.NUMERO).HasColumnName("NUMERO");
            this.Property(_ => _.CPF_CNPJ).HasColumnName("CPF_CNPJ");
        }
    }
}

