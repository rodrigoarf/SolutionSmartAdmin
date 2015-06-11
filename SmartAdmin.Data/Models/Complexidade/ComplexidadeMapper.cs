using System;
using System.Text;
using System.Collections;
using System.Collections.Generic;
using System.Data.Entity.ModelConfiguration;
using SmartAdmin.Dto;

namespace SmartAdmin.Data.Mapper
{
    public class ComplexidadeMapper : EntityTypeConfiguration<ComplexidadeDto>
    {
        public ComplexidadeMapper()
        {
            // Primary Key
            this.HasKey(t => t.ID);

            // Propertys
            this.Property(_ => _.COD_REGISTRO).IsRequired();
            this.Property(_ => _.NOME).IsRequired().HasMaxLength(100);
            this.Property(_ => _.DTH_CADASTRO).IsRequired();
            this.Property(_ => _.ESTADO_MENTAL);
            this.Property(_ => _.OXIGENACAO);
            this.Property(_ => _.SINAIS_VITAIS);
            this.Property(_ => _.MOTILIDADE);
            this.Property(_ => _.DEAMBULACAO);
            this.Property(_ => _.ALIMENTACAO);
            this.Property(_ => _.CUIDADO_CORPORAL);
            this.Property(_ => _.ELIMINACAO);
            this.Property(_ => _.TERAPEUTICA);
            this.Property(_ => _.TOTAL);
            this.Property(_ => _.TIPO_COMPLEXIDADE);
            this.Property(_ => _.SEXO).IsRequired().HasMaxLength(1);

            // Table & Column Mappings
            this.ToTable("SCP_COMPLEXIDADE", "cliniccenter");

            this.Property(_ => _.ID).HasColumnName("COD_COMPLEXIDADE");
            this.Property(_ => _.COD_REGISTRO).HasColumnName("COD_REGISTRO");
            this.Property(_ => _.NOME).HasColumnName("NOME");
            this.Property(_ => _.DTH_CADASTRO).HasColumnName("DTH_CADASTRO");
            this.Property(_ => _.ESTADO_MENTAL).HasColumnName("ESTADO_MENTAL");
            this.Property(_ => _.OXIGENACAO).HasColumnName("OXIGENACAO");
            this.Property(_ => _.SINAIS_VITAIS).HasColumnName("SINAIS_VITAIS");
            this.Property(_ => _.MOTILIDADE).HasColumnName("MOTILIDADE");
            this.Property(_ => _.DEAMBULACAO).HasColumnName("DEAMBULACAO");
            this.Property(_ => _.ALIMENTACAO).HasColumnName("ALIMENTACAO");
            this.Property(_ => _.CUIDADO_CORPORAL).HasColumnName("CUIDADO_CORPORAL");
            this.Property(_ => _.ELIMINACAO).HasColumnName("ELIMINACAO");
            this.Property(_ => _.TERAPEUTICA).HasColumnName("TERAPEUTICA");
            this.Property(_ => _.TOTAL).HasColumnName("TOTAL");
            this.Property(_ => _.TIPO_COMPLEXIDADE).HasColumnName("TIPO_COMPLEXIDADE");
            this.Property(_ => _.SEXO).HasColumnName("SEXO");
        }
    }
}

