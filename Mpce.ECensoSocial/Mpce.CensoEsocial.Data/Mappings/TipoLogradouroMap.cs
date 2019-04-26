using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Mpce.ECensoSocial.Domain.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace Mpce.CensoEsocial.Data.Mappings
{
    class TipoLogradouroMap : IEntityTypeConfiguration<TipoLogradouro>
    {
        public void Configure(EntityTypeBuilder<TipoLogradouro> builder)
        {
            builder.ToTable("tblTipoLogradouro");
            builder.HasKey(p => p.sCodigo);

            //builder.HasOne(t => t.Trabalhador)
            //   .WithMany(d => d.TiposLogradouro)
            //   .HasForeignKey(d => d.sCodigo);

        }
    }

}
   
