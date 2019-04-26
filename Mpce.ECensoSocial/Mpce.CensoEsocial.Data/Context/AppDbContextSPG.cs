using System.IO;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Mpce.CensoEsocial.Data.Mappings;
using Mpce.ECensoSocial.Domain.Domain.Entities;

namespace Mpce.CensoEsocial.Data.Context
{
    public class AppDbContextSPG : DbContext
    {
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            var config = new ConfigurationBuilder().SetBasePath(Directory.GetCurrentDirectory()).AddJsonFile("appsettings.json").Build();
            optionsBuilder.UseSqlServer(config.GetConnectionString("DefaultConnectionSGP"));
        }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfiguration(new PessoaMap());
        }
        public DbSet<Pessoa> Pessoa { get; set; }
    }
}
