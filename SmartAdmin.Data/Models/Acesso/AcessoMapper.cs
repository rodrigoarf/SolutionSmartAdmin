using System;
using System.Text;
using System.Collections;
using System.Collections.Generic;
using System.Data.Entity.ModelConfiguration;
using SmartAdmin.Dto;

namespace SmartAdmin.Data.Mapper
{
    public class AcessoMapper : EntityTypeConfiguration<AcessoDto>
    {
        public AcessoMapper()
        {
            // Primary Key
            this.HasKey(t => t.ID);

            // Propertys
            this.Property(_ => _.COD_USUARIO);
            this.Property(_ => _.TIPO_USUARIO).HasMaxLength(1);
            this.Property(_ => _.IP).HasMaxLength(100);
            this.Property(_ => _.BROWSER).HasMaxLength(100);
            this.Property(_ => _.PLATAFORMA).HasMaxLength(100);
            this.Property(_ => _.RESOLUCAO).HasMaxLength(100);
            this.Property(_ => _.DTH_ACESSO);
            this.Property(_ => _.DIA);
            this.Property(_ => _.MES);
            this.Property(_ => _.ANO);
            this.Property(_ => _.HORA).HasMaxLength(5);
            this.Property(_ => _.URL_ACESSO).HasMaxLength(100);
            this.Property(_ => _.DISPOSITIVO).HasMaxLength(100);
            this.Property(_ => _.DOMINIO).HasMaxLength(100);
            this.Property(_ => _.SUPORTA_ACTIVEX).HasMaxLength(1);
            this.Property(_ => _.SUPORTA_COOKIES).HasMaxLength(1);
            this.Property(_ => _.SUPORTA_JAVA_APPLET).HasMaxLength(1);
            this.Property(_ => _.COORD_LATITUDE).HasMaxLength(50);
            this.Property(_ => _.COORD_LONGITUDE).HasMaxLength(50);
            this.Property(_ => _.CIDADE).HasMaxLength(100);

            // Table & Column Mappings
            this.ToTable("ACESSO", "agilecore1");

            this.Property(_ => _.ID).HasColumnName("COD_ACESSO");
            this.Property(_ => _.COD_USUARIO).HasColumnName("COD_USUARIO");
            this.Property(_ => _.TIPO_USUARIO).HasColumnName("TIPO_USUARIO");
            this.Property(_ => _.IP).HasColumnName("IP");
            this.Property(_ => _.BROWSER).HasColumnName("BROWSER");
            this.Property(_ => _.PLATAFORMA).HasColumnName("PLATAFORMA");
            this.Property(_ => _.RESOLUCAO).HasColumnName("RESOLUCAO");
            this.Property(_ => _.DTH_ACESSO).HasColumnName("DTH_ACESSO");
            this.Property(_ => _.DIA).HasColumnName("DIA");
            this.Property(_ => _.MES).HasColumnName("MES");
            this.Property(_ => _.ANO).HasColumnName("ANO");
            this.Property(_ => _.HORA).HasColumnName("HORA");
            this.Property(_ => _.URL_ACESSO).HasColumnName("URL_ACESSO");
            this.Property(_ => _.DISPOSITIVO).HasColumnName("DISPOSITIVO");
            this.Property(_ => _.DOMINIO).HasColumnName("DOMINIO");
            this.Property(_ => _.SUPORTA_ACTIVEX).HasColumnName("SUPORTA_ACTIVEX");
            this.Property(_ => _.SUPORTA_COOKIES).HasColumnName("SUPORTA_COOKIES");
            this.Property(_ => _.SUPORTA_JAVA_APPLET).HasColumnName("SUPORTA_JAVA_APPLET");
            this.Property(_ => _.COORD_LATITUDE).HasColumnName("COORD_LATITUDE");
            this.Property(_ => _.COORD_LONGITUDE).HasColumnName("COORD_LONGITUDE");
            this.Property(_ => _.CIDADE).HasColumnName("CIDADE");
        }
    }
}

