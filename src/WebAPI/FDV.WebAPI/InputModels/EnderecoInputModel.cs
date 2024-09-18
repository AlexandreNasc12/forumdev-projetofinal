using System;
using System.ComponentModel.DataAnnotations;

namespace FDV.WebAPI.InputModels;

public class EnderecoInputModel
{
    [Required(ErrorMessage = "Informe um logradouro!")]
    public string Logradouro { get; set; }

    public string Numero { get; set; }
    public string Complemento { get; set; }

    [Required(ErrorMessage = "Informe um cep!")]
    public string Cep { get; set; }

    [Required(ErrorMessage = "Informe um bairro!")]
    public string Bairro { get; set; }

    [Required(ErrorMessage = "Informe uma cidade!")]
    public string Cidade { get; set; }

    [Required(ErrorMessage = "Informe um estado!")]
    public string Estado { get; set; }
}

public class CategoriaInputModel
{
    public string Nome { get; set; }

    public string Descricao { get; set; }
}