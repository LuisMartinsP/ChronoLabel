using System;
using System.Collections.Generic;
using ChronoLabel.Models;
using ChronoLabel.Repositories;
using ChronoLabel.Services;
using Moq;
using Xunit;
public class RelatorioServiceTests
{
    private readonly Mock<IRelatorioRepository> _mockRepo;
    private readonly RelatorioService _service;

    public RelatorioServiceTests()
    {
        _mockRepo = new Mock<IRelatorioRepository>();
        _service = new RelatorioService(_mockRepo.Object);
    }

    [Fact]
    public void GetAllRelatorios_ReturnsList()
    {
        // Arrange
        var relatorios = new List<Relatorio> { new Relatorio { IdRelatorio = 1 } };
        _mockRepo.Setup(r => r.GetAllRelatorios()).Returns(relatorios);

        // Act
        var result = _service.GetAllRelatorios();

        // Assert
        Assert.Single(result);
    }

    [Fact]
    public void GetRelatorioById_InvalidId_ThrowsArgumentOutOfRangeException()
    {
        Assert.Throws<ArgumentOutOfRangeException>(() => _service.GetRelatorioById(-1));
    }

    [Fact]
    public void GetRelatorioById_NotFound_ThrowsInvalidOperationException()
    {
        _mockRepo.Setup(r => r.RelatorioExists(It.IsAny<int>())).Returns(false);

        Assert.Throws<InvalidOperationException>(() => _service.GetRelatorioById(1));
    }

    [Fact]
    public void AddRelatorio_ValidData_CallsRepository()
    {
        // Arrange
        var relatorio = new Relatorio
        {
            CpfUsuario = "12345678900",
            IdProduto = "PROD01",
            DataCriacao = DateTime.Now,
            DataTermino = DateTime.Now.AddHours(1),
            Duracao = TimeSpan.FromHours(1)
        };

        // Act
        _service.AddRelatorio(relatorio);

        // Assert
        _mockRepo.Verify(r => r.AddRelatorio(It.IsAny<Relatorio>()), Times.Once);
    }

    [Fact]
    public void AddRelatorio_NullRelatorio_ThrowsArgumentNullException()
    {
        Assert.Throws<ArgumentNullException>(() => _service.AddRelatorio(null));
    }

    [Fact]
    public void AddRelatorio_InvalidFields_ThrowsArgumentException()
    {
        var relatorio = new Relatorio(); // Todos campos inválidos
        Assert.Throws<ArgumentException>(() => _service.AddRelatorio(relatorio));
    }

    [Fact]
    public void UpdateRelatorio_ValidData_CallsRepository()
    {
        var relatorio = new Relatorio { IdRelatorio = 1 };
        _service.UpdateRelatorio(relatorio);
        _mockRepo.Verify(r => r.UpdateRelatorio(relatorio), Times.Once);
    }

    [Fact]
    public void DeleteRelatorio_InvalidId_ThrowsArgumentOutOfRangeException()
    {
        Assert.Throws<ArgumentOutOfRangeException>(() => _service.DeleteRelatorio(-1));
    }

    [Fact]
    public void DeleteRelatorio_NotFound_ThrowsInvalidOperationException()
    {
        _mockRepo.Setup(r => r.RelatorioExists(It.IsAny<int>())).Returns(false);

        Assert.Throws<InvalidOperationException>(() => _service.DeleteRelatorio(1));
    }

    [Fact]
    public void GetRelatoriosByPeriodo_InvalidRange_ThrowsArgumentException()
    {
        var inicio = DateTime.Now;
        var fim = inicio.AddDays(-1); // Fim antes do início

        Assert.Throws<ArgumentException>(() => _service.GetRelatoriosByPeriodo(inicio, fim));
    }

    [Fact]
    public void GetQuantidadeProdutosByUsuarioAndDateRange_ValidInput_ReturnsInt()
    {
        // Arrange
        var cpf = "12345678900";
        var start = DateTime.Now.AddDays(-1);
        var end = DateTime.Now;

        _mockRepo.Setup(r => r.GetQuantidadeDeProdutosByUsuarioAndDataRange(cpf, start, end))
                    .Returns(5);

        // Act
        var result = _service.GetQuantidadeProdutosByUsuarioAndDateRange(cpf, start, end);

        // Assert
        Assert.Equal(5, result);
    }

    [Theory]
    [InlineData(null)]
    [InlineData("")]
    [InlineData("   ")]
    public void GetQuantidadeProdutosByUsuarioAndDateRange_InvalidCpf_ThrowsException(string cpf)
    {
        var start = DateTime.Now;
        var end = DateTime.Now;

        Assert.Throws<ArgumentException>(() => _service.GetQuantidadeProdutosByUsuarioAndDateRange(cpf, start, end));
    }

    [Fact]
    public void GetQuantidadeProdutosByUsuarioAndDateRange_InvalidDateRange_ThrowsException()
    {
        var cpf = "12345678900";
        var start = DateTime.Now;
        var end = start.AddDays(-1);

        Assert.Throws<ArgumentException>(() => _service.GetQuantidadeProdutosByUsuarioAndDateRange(cpf, start, end));
    }
}