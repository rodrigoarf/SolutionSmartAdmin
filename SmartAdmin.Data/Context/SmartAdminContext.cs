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
             this.Configuration.AutoDetectChangesEnabled = false;
             this.Configuration.LazyLoadingEnabled = false;
             this.Configuration.ProxyCreationEnabled = false;
             this.Configuration.ValidateOnSaveEnabled = false;
        }

        public DbSet<UsuarioDto> Usuario { get; set; }
        public DbSet<TipoNewletterDto> TipoNewletter { get; set; }
        public DbSet<EmailNewletterDto> EmailNewletter { get; set; }
        public DbSet<EnvioNewletterDto> EnvioNewletter { get; set; }
        public DbSet<AcessoDto> Acesso { get; set; }
        public DbSet<TipoContatoDto> TipoContato { get; set; }
        public DbSet<ContatoDto> Contato { get; set; }
        public DbSet<MenuDto> Menu { get; set; }
        public DbSet<MenuUsuarioDto> MenuUsuario { get; set; }
        public DbSet<InboxDto> Inbox { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
             modelBuilder.Configurations.Add(new UsuarioMapper());
             modelBuilder.Configurations.Add(new TipoNewletterMapper());
             modelBuilder.Configurations.Add(new EmailNewletterMapper());
             modelBuilder.Configurations.Add(new EnvioNewletterMapper());
             modelBuilder.Configurations.Add(new AcessoMapper());
             modelBuilder.Configurations.Add(new TipoContatoMapper());
             modelBuilder.Configurations.Add(new ContatoMapper());
             modelBuilder.Configurations.Add(new MenuMapper());
             modelBuilder.Configurations.Add(new MenuUsuarioMapper());
             modelBuilder.Configurations.Add(new InboxMapper());
        }
    }
}

