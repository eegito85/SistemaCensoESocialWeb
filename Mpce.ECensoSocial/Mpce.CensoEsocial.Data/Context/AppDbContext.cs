using System.IO;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Mpce.CensoEsocial.Data.Mappings;
using Mpce.ECensoSocial.Domain.Domain.Entities;

namespace Mpce.CensoEsocial.Data.Context
{
    public class AppDbContext : DbContext
    {
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            var config = new ConfigurationBuilder().SetBasePath(Directory.GetCurrentDirectory()).AddJsonFile("appsettings.json").Build();
            optionsBuilder.UseSqlServer(config.GetConnectionString("DefaultConnection"));
        }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfiguration(new TrabalhadorMap());
            modelBuilder.ApplyConfiguration(new DependenteMap());
            modelBuilder.ApplyConfiguration(new MunicipioMap());
            modelBuilder.ApplyConfiguration(new EstagiarioMap());
            modelBuilder.ApplyConfiguration(new PaisRepositoryMap());
            modelBuilder.ApplyConfiguration(new CedidoMap());
            modelBuilder.ApplyConfiguration(new TipoLogradouroMap());
            modelBuilder.ApplyConfiguration(new DocumentoMap());
        }
        public DbSet<Trabalhador> Trabalhador { get; set; }
        public DbSet<Dependente> Dependente { get; set; }
        public DbSet<Municipio> Municipio { get; set; }
        public DbSet<Estagiario> Estagiario { get; set; }
        public DbSet<Pais> Pais { get; set; }
        public DbSet<Cedido> Cedido { get; set; }
        public DbSet<TipoLogradouro> TipoLogradouro { get; set; }
        public DbSet<Documento> Documento { get; set; }
    }
}
