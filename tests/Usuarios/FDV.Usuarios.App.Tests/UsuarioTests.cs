using FDV.Core.ValueObjects;
using FDV.Usuarios.App.Domain;

namespace FDV.Usuarios.App.Tests;

public class UsuarioTests
{
    [Fact(DisplayName ="Usuario calcular idade deve ter sucesso")]
    public void Usuario_calcular_idade_deve_ter_sucesso()
    {
        //Arrange
        var usuario = new Usuario("Alexandre",new Cpf("089.724.820-13"),"imagem.jpg",new DateTime(1985,05,10));

        //Act
        var idade = usuario.Idade;


        //Assert
        Assert.Equal(39,idade);
    }

    [Fact(DisplayName ="Usuario adicionar endereço deve ter sucesso")]
    public void Usuario_adicionar_endereco_deve_ter_sucesso()
    {
        //Arrange
        var usuario = new Usuario("Alexandre",new Cpf("089.724.820-13"),"imagem.jpg",new DateTime(1985,05,10));

        var endereco = new Endereco("Rua Doutor Abílio Gomes Cavalcanti","245","Bl 3 Ap 1204",new Cep("58038-550"),"Manaíra","João Pessoa","PB");

        //Act
        usuario.Adicionar(endereco);

        //Assert
        Assert.Single(usuario.Enderecos);
    }

    [Fact(DisplayName ="Usuario adicionar endereço duplicado deve falhar")]
    public void Usuario_adicionar_endereco_duplicado_deve_falhar()
    {
        // arrange
        var usuario = new Usuario("Alexandre",new Cpf("089.724.820-13"),"imagem.jpg",new DateTime(1985,05,10));
        
        var endereco = new Endereco("Rua Doutor Abílio Gomes Cavalcanti","245","Bl 3 Ap 1204",new Cep("58038-550"),"Manaíra","João Pessoa","PB");
        var endereco2 = new Endereco("Rua Doutor Abílio Gomes Cavalcanti","245","Bl 3 Ap 1204",new Cep("58038-550"),"Manaíra","João Pessoa","PB");

        usuario.Adicionar(endereco);
    
        // act
        usuario.Adicionar(endereco2);
    
        // Assert
        Assert.Single(usuario.Enderecos);
    }
}