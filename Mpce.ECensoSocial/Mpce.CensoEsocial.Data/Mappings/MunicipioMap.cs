using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Mpce.ECensoSocial.Domain.Domain.Entities;

namespace Mpce.CensoEsocial.Data.Mappings
{
    public class MunicipioMap : IEntityTypeConfiguration<Municipio>
    {
        public void Configure(EntityTypeBuilder<Municipio> builder)
        {
            builder.ToTable("tblMunicipio");
            builder.Property(p => p.iCodigo).ValueGeneratedOnAdd();
            builder.HasKey(p => p.iCodigo);
        }
    }
}
