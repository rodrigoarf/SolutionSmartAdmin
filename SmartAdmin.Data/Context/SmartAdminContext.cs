using System;
using System.Text;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Data.Entity.ModelConfiguration.Conventions;
using System.Linq;
using System.Configuration;
using System.Threading.Tasks;
using SmartAdmin.Data.Mapper;
using SmartAdmin.Dto;

namespace SmartAdmin.Data.Context
{
    public partial class SmartAdminContext : DbContext
    {
        static SmartAdminContext()
        {
            Database.SetInitializer<SmartAdminContext>(null);
        }

        public SmartAdminContext() : base("Name=SmartAdminContext")
        {
        }

        public DbSet<UsuarioDto> Usuario { get; set; }
        public DbSet<MenuDto> Menu { get; set; }
        public DbSet<PermissaoDto> Permissao { get; set; }
        public DbSet<ComplexidadeDto> Complexidade { get; set; }
        public DbSet<TipoComplexidadeDto> TipoComplexidade { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
             modelBuilder.Configurations.Add(new UsuarioMapper());
             modelBuilder.Configurations.Add(new MenuMapper());
             modelBuilder.Configurations.Add(new PermissaoMapper());
             modelBuilder.Configurations.Add(new ComplexidadeMapper());
             modelBuilder.Configurations.Add(new TipoComplexidadeMapper());
        }
    }
}

