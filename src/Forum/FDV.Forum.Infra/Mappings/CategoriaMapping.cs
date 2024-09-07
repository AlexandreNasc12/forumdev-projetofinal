using FDV.Forum.Domain;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace FDV.Forum.Infra.Mappings;

public class CategoriaMapping : IEntityTypeConfiguration<Categoria>
{
    public void Configure(EntityTypeBuilder<Categoria> builder)
    {
        builder.ToTable("Categorias");

        builder.HasKey(c => c.Id);

        builder.Property(c => c.Descricao).HasMaxLength(Postagem.TamanhoMaximo);

        builder.HasMany(c => c.Postagens).WithMany(c => c.Categorias);
    }
}
