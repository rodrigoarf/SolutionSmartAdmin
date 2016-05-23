using System;
using System.Text;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Data.Entity.ModelConfiguration.Conventions;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Configuration;
using System.Threading.Tasks;
using SmartAdmin.Data.Model;

namespace SmartAdmin.Data.ApplicationContext

{
    public class SmartAdminContext : DbContext
    {
        public DbSet<UsuarioDto> Usuario { get; set; }
        public DbSet<AcessoDto> Acesso { get; set; }
        public DbSet<MenuDto> Menu { get; set; }
        public DbSet<MenuUsuarioDto> MenuUsuario { get; set; }

        static SmartAdminContext()
        {
            Database.SetInitializer<SmartAdminContext>(null);
        }

        public SmartAdminContext() : base("Name=DefaultConnection")
        {
             this.Configuration.AutoDetectChangesEnabled = true;
             this.Configuration.ValidateOnSaveEnabled = false;
             this.Configuration.LazyLoadingEnabled = false;
             this.Configuration.ProxyCreationEnabled = false;
        }

        protected override void OnModelCreating(DbModelBuilder ModelBuilder)
        {
             ModelBuilder.Configurations.Add(new UsuarioMapper());
             ModelBuilder.Configurations.Add(new AcessoMapper());
             ModelBuilder.Configurations.Add(new MenuMapper());
             ModelBuilder.Configurations.Add(new MenuUsuarioMapper());
        }
    }
}

