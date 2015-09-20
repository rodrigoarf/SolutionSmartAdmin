using System;
using System.Text;
using System.Collections;
using System.Collections.Generic;
using System.Data.Entity.ModelConfiguration;
using SmartAdmin.Dto;

namespace SmartAdmin.Data.Mapper
{
    public class BoletoMapper : EntityTypeConfiguration<BoletoDto>
    {
        public BoletoMapper()
        {
            // Primary Key
            this.HasKey(t => t.ID);

            // Propertys

            // Table & Column Mappings
            this.ToTable("BOLETO", "agilecore1");

        }
    }
}

