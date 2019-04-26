using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Mpce.ECensoSocial.Domain.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace Mpce.CensoEsocial.Data.Mappings
{
    public class EstagiarioMap : IEntityTypeConfiguration<Estagiario>
    {
        public void Configure(EntityTypeBuilder<Estagiario> builder)
        {
            builder.ToTable("tblEstagiario");
            builder.Property(p => p.iCodigo).ValueGeneratedOnAdd();
            builder.HasKey(p => p.iCodigo);

            builder.HasOne(t => t.Trabalhador)
                          .WithMany(d => d.Estagiarios)
                          .HasForeignKey(d => d.iCodTrabalhador);
        }
    }
}
