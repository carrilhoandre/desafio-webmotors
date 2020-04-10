using Anuncios.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Anuncios.Infrastructure.SqlServer.Write.Mappings
{
    internal class AnuncioMap : IEntityTypeConfiguration<Anuncio>
    {
        public void Configure(EntityTypeBuilder<Anuncio> entity)
        {
            entity.ToTable("tb_AnuncioWebmotors");

            
            entity.Property(e => e.Id)
               .HasColumnName("ID");

            entity.Property(e => e.Marca)
                .IsRequired()
                .HasColumnName("marca")
                .HasMaxLength(45)
                .IsUnicode(false);

            entity.Property(e => e.Modelo)
                .IsRequired()
                .HasColumnName("modelo")
                .HasMaxLength(45)
                .IsUnicode(false);

            entity.Property(e => e.Versao)
                .IsRequired()
                .HasColumnName("versao")
                .HasMaxLength(45)
                .IsUnicode(false);

            entity.Property(e => e.Ano)
                .IsRequired()
                .HasColumnName("ano");

            entity.Property(e => e.Quilometragem)
                .IsRequired()
                .HasColumnName("quilometragem");

            entity.Property(e => e.Observacao)
                .IsRequired()
                .HasColumnName("observacao")
                .HasMaxLength(45)
                .IsUnicode(false);
        }
    }
}


