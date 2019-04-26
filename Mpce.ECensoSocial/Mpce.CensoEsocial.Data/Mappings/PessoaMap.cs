using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Mpce.ECensoSocial.Domain.Domain.Entities;

namespace Mpce.CensoEsocial.Data.Mappings
{
    public class PessoaMap : IEntityTypeConfiguration<Pessoa>
    {
        public void Configure(EntityTypeBuilder<Pessoa> builder)
        {
            builder.ToTable("tblPessoa");
            builder.HasKey(p => p.iCodPessoa);
        }
    }
}