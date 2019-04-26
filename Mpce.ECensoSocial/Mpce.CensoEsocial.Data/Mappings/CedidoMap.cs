using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Mpce.ECensoSocial.Domain.Domain.Entities;

namespace Mpce.CensoEsocial.Data.Mappings
{
    public class CedidoMap : IEntityTypeConfiguration<Cedido>
    {
        public void Configure(EntityTypeBuilder<Cedido> builder)
        {
            builder.ToTable("tblCedido");
            builder.Property(p => p.iCodigo).ValueGeneratedOnAdd();
            builder.HasKey(p => p.iCodigo);

            builder.HasOne(t => t.Trabalhador)
                                     .WithMany(d => d.Cedidos)
                                     .HasForeignKey(d => d.iCodTrabalhador);
        }
    }
}