using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Mpce.ECensoSocial.Domain.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace Mpce.CensoEsocial.Data.Mappings
{
    public class DependenteMap : IEntityTypeConfiguration<Dependente>
    {
        public void Configure(EntityTypeBuilder<Dependente> builder)
        {
            builder.ToTable("tblDependente");
            builder.Property(p => p.iCodigo).ValueGeneratedOnAdd();
            builder.HasKey(p => p.iCodigo);

            builder.HasOne(t => t.Trabalhador)
               .WithMany(d => d.Dependentes)
               .HasForeignKey(d => d.icodTrabalhador);
        }
    }
}
