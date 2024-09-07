using System;
using FDV.Forum.Domain;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace FDV.Forum.Infra.Mappings;

public class PostagemMapping : IEntityTypeConfiguration<Postagem>
{
    public void Configure(EntityTypeBuilder<Postagem> builder)
    {
        builder.ToTable("Postagens");

        builder.HasKey(x => x.Id);

        builder.Property(c => c.Titulo).HasMaxLength(Postagem.TamanhoMaximo).IsRequired();

        builder.Property(c => c.Descricao).HasColumnName("Conteudo").IsRequired();

        builder.Property(c => c.Publicado).IsRequired();

        builder.Property(c => c.AprovadoPor).HasMaxLength(Postagem.TamanhoMaximo);

        builder.Property(c => c.QuantidadeComentario).IsRequired();

        builder.OwnsOne(c => c.Usuario, usuario =>
        {
            usuario.Property(x => x.Id).HasColumnName("UsuarioId").IsRequired();
            usuario.Property(x => x.Nome).HasColumnName("UsuarioNome").HasMaxLength(Postagem.TamanhoMaximo).IsRequired();
            usuario.Property(x => x.Foto).HasColumnName("UsuarioFoto").HasMaxLength(Postagem.TamanhoMaximo).IsRequired();
        });

        builder.OwnsMany(c => c.Comentarios, comentario =>
        {
            comentario.WithOwner().HasForeignKey("PostagemId");
            comentario.Property<Guid>("Id").ValueGeneratedOnAdd();
            comentario.HasKey("Id");

            comentario.Property(c => c.Descricao).HasMaxLength(Postagem.TamanhoMaximo);
            comentario.OwnsOne(c => c.Usuario, usuario =>
            {
                usuario.Property(x => x.Id).HasColumnName("UsuarioId").IsRequired();
                usuario.Property(x => x.Nome).HasColumnName("UsuarioNome").HasMaxLength(Postagem.TamanhoMaximo).IsRequired();
                usuario.Property(x => x.Foto).HasColumnName("UsuarioFoto").HasMaxLength(Postagem.TamanhoMaximo).IsRequired();
            });
        });

        builder.HasMany(c => c.Categorias).WithMany(c => c.Postagens);
    }
}
