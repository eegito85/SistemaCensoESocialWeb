using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Mpce.ECensoSocial.Domain.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace Mpce.CensoEsocial.Data.Mappings
{
    public class DocumentoMap : IEntityTypeConfiguration<Documento>
    {
        public void Configure(EntityTypeBuilder<Documento> builder)
        {
            builder.ToTable("tblDocumentos");
            builder.Property(p => p.iCodigo).ValueGeneratedOnAdd();
            builder.HasKey(p => p.iCodigo);

        }
    }
}
