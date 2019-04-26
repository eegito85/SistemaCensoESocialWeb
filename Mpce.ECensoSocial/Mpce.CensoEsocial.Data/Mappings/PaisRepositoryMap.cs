using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Mpce.ECensoSocial.Domain.Domain.Entities;

namespace Mpce.CensoEsocial.Data.Mappings
{
    public class PaisRepositoryMap : IEntityTypeConfiguration<Pais>
    {
        public void Configure(EntityTypeBuilder<Pais> builder)
        {
            builder.ToTable("tblPais");
            //builder.Property(p => p.iCodigo).ValueGeneratedOnAdd();
            builder.HasKey(p => p.iCodigo);
        }
    }
}