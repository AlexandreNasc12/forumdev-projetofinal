﻿// <auto-generated />
using System;
using FDV.Forum.Infra.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

#nullable disable

namespace FDV.Forum.Infra.Migrations
{
    [DbContext(typeof(PostagensContext))]
    partial class PostagensContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "8.0.8")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder);

            modelBuilder.Entity("CategoriaPostagem", b =>
                {
                    b.Property<Guid>("CategoriasId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<Guid>("PostagensId")
                        .HasColumnType("uniqueidentifier");

                    b.HasKey("CategoriasId", "PostagensId");

                    b.HasIndex("PostagensId");

                    b.ToTable("CategoriaPostagem");
                });

            modelBuilder.Entity("FDV.Forum.Domain.Categoria", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<DateTime>("DataDeAlteracao")
                        .HasColumnType("datetime2");

                    b.Property<DateTime>("DataDeCadastro")
                        .HasColumnType("datetime2");

                    b.Property<string>("Descricao")
                        .HasMaxLength(500)
                        .HasColumnType("nvarchar(500)");

                    b.Property<Guid>("Hash")
                        .HasMaxLength(50)
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("Nome")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("Categorias", (string)null);
                });

            modelBuilder.Entity("FDV.Forum.Domain.Postagem", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<bool>("Aprovado")
                        .HasColumnType("bit");

                    b.Property<string>("AprovadoPor")
                        .HasMaxLength(500)
                        .HasColumnType("nvarchar(500)");

                    b.Property<DateTime>("DataDeAlteracao")
                        .HasColumnType("datetime2");

                    b.Property<DateTime>("DataDeCadastro")
                        .HasColumnType("datetime2");

                    b.Property<string>("Descricao")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)")
                        .HasColumnName("Conteudo");

                    b.Property<bool>("Publicado")
                        .HasColumnType("bit");

                    b.Property<int>("QuantidadeComentario")
                        .HasColumnType("int");

                    b.Property<string>("Titulo")
                        .IsRequired()
                        .HasMaxLength(500)
                        .HasColumnType("nvarchar(500)");

                    b.HasKey("Id");

                    b.ToTable("Postagens", (string)null);
                });

            modelBuilder.Entity("CategoriaPostagem", b =>
                {
                    b.HasOne("FDV.Forum.Domain.Categoria", null)
                        .WithMany()
                        .HasForeignKey("CategoriasId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("FDV.Forum.Domain.Postagem", null)
                        .WithMany()
                        .HasForeignKey("PostagensId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("FDV.Forum.Domain.Postagem", b =>
                {
                    b.OwnsMany("FDV.Forum.Domain.Comentario", "Comentarios", b1 =>
                        {
                            b1.Property<Guid>("Id")
                                .ValueGeneratedOnAdd()
                                .HasColumnType("uniqueidentifier");

                            b1.Property<DateTime>("DataDeCadastro")
                                .HasColumnType("datetime2");

                            b1.Property<string>("Descricao")
                                .HasMaxLength(500)
                                .HasColumnType("nvarchar(500)");

                            b1.Property<Guid>("PostagemId")
                                .HasColumnType("uniqueidentifier");

                            b1.HasKey("Id");

                            b1.HasIndex("PostagemId");

                            b1.ToTable("Comentario");

                            b1.WithOwner()
                                .HasForeignKey("PostagemId");

                            b1.OwnsOne("FDV.Forum.Domain.Usuario", "Usuario", b2 =>
                                {
                                    b2.Property<Guid>("ComentarioId")
                                        .HasColumnType("uniqueidentifier");

                                    b2.Property<string>("Foto")
                                        .IsRequired()
                                        .HasMaxLength(500)
                                        .HasColumnType("nvarchar(500)")
                                        .HasColumnName("UsuarioFoto");

                                    b2.Property<Guid>("Id")
                                        .HasColumnType("uniqueidentifier")
                                        .HasColumnName("UsuarioId");

                                    b2.Property<string>("Nome")
                                        .IsRequired()
                                        .HasMaxLength(500)
                                        .HasColumnType("nvarchar(500)")
                                        .HasColumnName("UsuarioNome");

                                    b2.HasKey("ComentarioId");

                                    b2.ToTable("Comentario");

                                    b2.WithOwner()
                                        .HasForeignKey("ComentarioId");
                                });

                            b1.Navigation("Usuario");
                        });

                    b.OwnsOne("FDV.Forum.Domain.Usuario", "Usuario", b1 =>
                        {
                            b1.Property<Guid>("PostagemId")
                                .HasColumnType("uniqueidentifier");

                            b1.Property<string>("Foto")
                                .IsRequired()
                                .HasMaxLength(500)
                                .HasColumnType("nvarchar(500)")
                                .HasColumnName("UsuarioFoto");

                            b1.Property<Guid>("Id")
                                .HasColumnType("uniqueidentifier")
                                .HasColumnName("UsuarioId");

                            b1.Property<string>("Nome")
                                .IsRequired()
                                .HasMaxLength(500)
                                .HasColumnType("nvarchar(500)")
                                .HasColumnName("UsuarioNome");

                            b1.HasKey("PostagemId");

                            b1.ToTable("Postagens");

                            b1.WithOwner()
                                .HasForeignKey("PostagemId");
                        });

                    b.Navigation("Comentarios");

                    b.Navigation("Usuario");
                });
#pragma warning restore 612, 618
        }
    }
}
