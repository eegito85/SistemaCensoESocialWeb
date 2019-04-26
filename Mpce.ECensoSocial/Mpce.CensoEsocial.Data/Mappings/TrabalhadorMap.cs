using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Mpce.ECensoSocial.Domain.Domain.Entities;

namespace Mpce.CensoEsocial.Data.Mappings
{
    public class TrabalhadorMap : IEntityTypeConfiguration<Trabalhador>
    {
        public void Configure(EntityTypeBuilder<Trabalhador> builder)
        {
            builder.ToTable("tblTrabalhador");
            builder.Property(p => p.iCodigo).ValueGeneratedOnAdd();
            builder.HasKey(p => p.iCodigo);

        }
    }
}