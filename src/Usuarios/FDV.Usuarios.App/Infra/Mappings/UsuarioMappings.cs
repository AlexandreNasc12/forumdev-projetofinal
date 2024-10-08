using System;
using FDV.Usuarios.App.Domain;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace FDV.Usuarios.App.Infra.Mappings;

public class UsuarioMappings : IEntityTypeConfiguration<Usuario>
{
    public void Configure(EntityTypeBuilder<Usuario> builder)
    {
        builder.ToTable("Usuarios");

        builder.HasKey(x => x.Id);

        builder.Property(c => c.Nome).HasColumnName("Nome").HasMaxLength(200).IsRequired();

        builder.Property(c => c.Foto).HasColumnName("CaminhoFoto").HasMaxLength(200).IsRequired();

        builder.Property(c => c.DataDeCadastro).HasColumnName("DataDeCadastro");

        builder.OwnsOne(c => c.Cpf, cpf => 
        {
            cpf.Property(c => c.Numero).HasMaxLength(11).HasColumnName("Cpf");
        });

        builder.OwnsOne(c => c.Login, login =>
        {
            login.Property(c => c.Hash).HasColumnName("LoginHash");

            login.OwnsOne(c => c.Email, email =>{
                email.Property(x => x.Endereco).HasMaxLength(256).HasColumnName("Email");
            });

            login.OwnsOne(c => c.Senha, senha =>
            {
                senha.Property(x => x.Valor).HasMaxLength(1000).HasColumnName("Senha");
            });
        });


        builder.OwnsMany(c => c.Enderecos, endereco =>
        {
            endereco.WithOwner().HasForeignKey("UsuarioId");
            endereco.Property<Guid>("Id").ValueGeneratedOnAdd();
            endereco.HasKey("Id");

            endereco.Property(c => c.Logradouro).HasMaxLength(200);
            endereco.Property(c => c.Bairro).HasMaxLength(200);
            endereco.Property(c => c.Estado).HasMaxLength(5);
            endereco.Property(c => c.Numero).HasMaxLength(100);

            endereco.OwnsOne(c => c.Cep, cep =>
            {
                cep.Property(c => c.Numero).HasMaxLength(8).HasColumnName("Cep");
            });

            endereco.Property(c => c.Cidade).HasMaxLength(500);
            endereco.Property(c => c.Complemento).HasMaxLength(500);
        });

        builder.Navigation(c => c.Enderecos).AutoInclude(false);
    }
}
