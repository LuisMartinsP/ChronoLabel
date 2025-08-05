using Xunit;
using Moq;
using System.Linq;
using ChronoLabel.Services;
using ChronoLabel.Repositories;
using ChronoLabel.Models;
using System.Collections.Generic;
using System;

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

    [Fact]
    public void DeleteUsuario_DeveChamarRepositorioSeUsuarioExiste()
    {
        var mockRepository = new Mock<IUsuarioRepository>();
        mockRepository.Setup(r => r.UsuarioExists("123")).Returns(true);
        var service = new UsuarioService(mockRepository.Object);

        service.DeleteUsuario("123");

        mockRepository.Verify(r => r.DeleteUsuario("123"), Times.Once);
    }

    [Fact]
    public void DeleteUsuario_DeveLancarExcecaoSeUsuarioNaoExiste()
    {
        var mockRepository = new Mock<IUsuarioRepository>();
        mockRepository.Setup(r => r.UsuarioExists("123")).Returns(false);
        var service = new UsuarioService(mockRepository.Object);

        var ex = Assert.Throws<InvalidOperationException>(() => service.DeleteUsuario("123"));

        Assert.Equal("Usuário não encontrado.", ex.Message);
    }

    [Fact]
    public void UsuarioExists_DeveRetornarTrueSeUsuarioExiste()
    {
        var mockRepository = new Mock<IUsuarioRepository>();
        mockRepository.Setup(r => r.UsuarioExists("123")).Returns(true);
        var service = new UsuarioService(mockRepository.Object);

        Assert.True(service.UsuarioExists("123"));
    }

    [Fact]
    public void UsuarioExists_DeveRetornarFalseSeUsuarioNaoExiste()
    {
        var mockRepository = new Mock<IUsuarioRepository>();
        mockRepository.Setup(r => r.UsuarioExists("123")).Returns(false);
        var service = new UsuarioService(mockRepository.Object);

        Assert.False(service.UsuarioExists("123"));
    }

    [Fact]
    public void SearchUsuarios_DeveRetornarUsuariosComNomeECpf()
    {
        var mockRepository = new Mock<IUsuarioRepository>();
        mockRepository.Setup(r => r.SearchUsuarios("Fulano", "123"))
            .Returns(new List<Usuario> { new Usuario { Nome = "Fulano", Cpf = "123" } });

        var service = new UsuarioService(mockRepository.Object);
        var result = service.SearchUsuarios("Fulano", "123");

        Assert.Single(result);
        Assert.Equal("Fulano", result.First().Nome);
    }

    [Fact]
    public void SearchUsuarios_DeveRetornarTodosSeParametrosForemNulos()
    {
        var mockRepository = new Mock<IUsuarioRepository>();
        mockRepository.Setup(r => r.SearchUsuarios(null, null))
            .Returns(new List<Usuario> {
                new Usuario { Nome = "Fulano", Cpf = "123" },
                new Usuario { Nome = "Beltrano", Cpf = "456" }
            });

        var service = new UsuarioService(mockRepository.Object);
        var result = service.SearchUsuarios();

        Assert.Equal(2, result.Count());
    }
}
