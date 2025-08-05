using System;
using System.Collections.Generic;
using ChronoLabel.Models;
using ChronoLabel.Repositories;
using ChronoLabel.Services;
using Moq;
using Xunit;

public class ProdutoServiceTests
    {
        private readonly ProdutoService _service;
        private readonly Mock<IProdutoRepository> _mockRepo;

        public ProdutoServiceTests()
        {
            _mockRepo = new Mock<IProdutoRepository>();
            _service = new ProdutoService(_mockRepo.Object);
        }

        [Fact]
        public void GetAllProdutos_ReturnsProdutos()
        {
            var produtos = new List<Produto> { new Produto { IdProduto = "1", Nome = "Produto A" } };
            _mockRepo.Setup(r => r.GetAllProdutos()).Returns(produtos);

            var result = _service.GetAllProdutos();

            Assert.Equal(produtos, result);
        }

        [Fact]
        public void GetProdutoById_EmptyId_ThrowsArgumentException()
        {
            Assert.Throws<ArgumentException>(() => _service.GetProdutoById(""));
        }

        [Fact]
        public void GetProdutoById_ProdutoNaoExiste_ThrowsInvalidOperationException()
        {
            _mockRepo.Setup(r => r.ProdutoExists("123")).Returns(false);

            Assert.Throws<InvalidOperationException>(() => _service.GetProdutoById("123"));
        }

        [Fact]
        public void GetProdutoById_ProdutoExiste_RetornaProduto()
        {
            var produto = new Produto { IdProduto = "1", Nome = "Teste" };
            _mockRepo.Setup(r => r.ProdutoExists("1")).Returns(true);
            _mockRepo.Setup(r => r.GetProdutoById("1")).Returns(produto);

            var result = _service.GetProdutoById("1");

            Assert.Equal(produto, result);
        }

        [Fact]
        public void AddProduto_Null_ThrowsArgumentNullException()
        {
            Assert.Throws<ArgumentNullException>(() => _service.AddProduto(null!));
        }

        [Fact]
        public void AddProduto_CamposObrigatoriosVazios_ThrowsArgumentException()
        {
            var produto = new Produto { IdProduto = "", Nome = "" };

            Assert.Throws<ArgumentException>(() => _service.AddProduto(produto));
        }

        [Fact]
        public void AddProduto_ProdutoJaExiste_ThrowsInvalidOperationException()
        {
            var produto = new Produto { IdProduto = "1", Nome = "Produto" };
            _mockRepo.Setup(r => r.ProdutoExists("1")).Returns(true);

            Assert.Throws<InvalidOperationException>(() => _service.AddProduto(produto));
        }

        [Fact]
        public void AddProduto_QuantidadeNegativa_ThrowsArgumentException()
        {
            var produto = new Produto { IdProduto = "1", Nome = "Produto", Quantidade = -5, Peso = 1 };

            Assert.Throws<ArgumentException>(() => _service.AddProduto(produto));
        }

        [Fact]
        public void AddProduto_PesoNegativo_ThrowsArgumentException()
        {
            var produto = new Produto { IdProduto = "1", Nome = "Produto", Quantidade = 1, Peso = -1 };

            Assert.Throws<ArgumentException>(() => _service.AddProduto(produto));
        }

        [Fact]
        public void UpdateProduto_ProdutoNaoExiste_ThrowsInvalidOperationException()
        {
            var produto = new Produto { IdProduto = "999", Nome = "Inexistente" };
            _mockRepo.Setup(r => r.GetProdutoById("999")).Returns((Produto?)null);

            Assert.Throws<InvalidOperationException>(() => _service.UpdateProduto(produto));
        }

        [Fact]
        public void UpdateProduto_IdVazio_ThrowsArgumentNullException()
        {
            var produto = new Produto { IdProduto = "" };

            Assert.Throws<ArgumentNullException>(() => _service.UpdateProduto(produto));
        }

        [Fact]
        public void UpdateProduto_AtualizaCamposCorretamente()
        {
            var produto = new Produto { IdProduto = "1", Nome = "Novo Nome", Quantidade = 10, Peso = 2 };
            var existente = new Produto { IdProduto = "1", Nome = "Antigo Nome", Quantidade = 5, Peso = 1 };

            _mockRepo.Setup(r => r.GetProdutoById("1")).Returns(existente);

            _service.UpdateProduto(produto);

            _mockRepo.Verify(r => r.UpdateProduto(It.Is<Produto>(p =>
                p.IdProduto == "1" &&
                p.Nome == "Novo Nome" &&
                p.Quantidade == 10 &&
                p.Peso == 2
            )), Times.Once);
        }

        [Fact]
        public void DeleteProduto_IdInvalido_ThrowsArgumentNullException()
        {
            Assert.Throws<ArgumentNullException>(() => _service.DeleteProduto(""));
        }

        [Fact]
        public void DeleteProduto_ProdutoNaoExiste_ThrowsInvalidOperationException()
        {
            _mockRepo.Setup(r => r.ProdutoExists("123")).Returns(false);

            Assert.Throws<InvalidOperationException>(() => _service.DeleteProduto("123"));
        }

        [Fact]
        public void DeleteProduto_Sucesso()
        {
            _mockRepo.Setup(r => r.ProdutoExists("1")).Returns(true);

            _service.DeleteProduto("1");

            _mockRepo.Verify(r => r.DeleteProduto("1"), Times.Once);
        }

        [Fact]
        public void ProdutoExists_IdVazio_ThrowsArgumentNullException()
        {
            Assert.Throws<ArgumentNullException>(() => _service.ProdutoExists(""));
        }

        [Fact]
        public void ProdutoExists_RetornaResultadoDoRepositorio()
        {
            _mockRepo.Setup(r => r.ProdutoExists("1")).Returns(true);

            var result = _service.ProdutoExists("1");

            Assert.True(result);
        }

        [Fact]
        public void SearchProdutos_SemCriterio_ThrowsArgumentException()
        {
            Assert.Throws<ArgumentException>(() => _service.SearchProdutos(null, null));
        }

        [Fact]
        public void SearchProdutos_UsaRepositorioComFiltro()
        {
            var produtos = new List<Produto> { new Produto { IdProduto = "1", Nome = "Filtro" } };
            _mockRepo.Setup(r => r.SearchProdutos("Filtro", null)).Returns(produtos);

            var result = _service.SearchProdutos(nome: "Filtro");

            Assert.Equal(produtos, result);
        }
    }