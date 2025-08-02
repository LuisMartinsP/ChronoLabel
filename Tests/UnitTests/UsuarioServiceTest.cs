using Xunit;
using Moq;
using System.Linq;
using ChronoLabel.Services;
using ChronoLabel.Repositories;
using ChronoLabel.Models;
using System.Collections.Generic;

public class UsuarioServiceTest
{

    [Fact]
    public void GetAllUsuarios_DeveRetornarListaUsuarios()
    {
        var mockRepository = new Mock<IUsuarioRepository>();
        mockRepository.Setup(repo => repo.GetAllUsuarios())
        .Returns(new List<Usuario>
        {
            new Usuario { Cpf = "12345678901", Nome = "Fulano" },
            new Usuario { Cpf = "12345678902", Nome = "Beltrano" }
        });

        var service = new UsuarioService(mockRepository.Object);

        var usuarios = service.GetAllUsuarios();

        Assert.NotNull(usuarios);

        Assert.Collection(usuarios,
            usuario => Assert.Equal("12345678901", usuario.Cpf),
            usuario => Assert.Equal("12345678902", usuario.Cpf)
        );

        Assert.Equal(2, usuarios.Count());

        Assert.Contains(usuarios, u => u.Nome == "Fulano");
    }
}