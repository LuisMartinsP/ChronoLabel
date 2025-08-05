using System;
using System.Collections.Generic;
using ChronoLabel.Models;
using ChronoLabel.Repositories;

namespace ChronoLabel.Services
{
    public class ProdutoService
    {
        private readonly IProdutoRepository _produtoRepository;

        public ProdutoService(IProdutoRepository produtoRepository)
        {
            _produtoRepository = produtoRepository;
        }

        public IEnumerable<Produto> GetAllProdutos()
        {
            return _produtoRepository.GetAllProdutos();
        }

        public Produto? GetProdutoById(string id)
        {
            if (string.IsNullOrEmpty(id))
            {
                throw new ArgumentException("ID do produto não pode ser nulo ou vazio.", nameof(id));
            }
            if (!_produtoRepository.ProdutoExists(id))
            {
                throw new InvalidOperationException("Produto não encontrado.");
            }

            return _produtoRepository.GetProdutoById(id);
        }

        public void AddProduto(Produto produto)
        {
            if (produto == null)
            {
                throw new ArgumentNullException(nameof(produto), "Produto não pode ser nulo.");
            }
            if (string.IsNullOrWhiteSpace(produto.Nome) || string.IsNullOrWhiteSpace(produto.IdProduto))
            {
                throw new ArgumentException("Nome e ID do produto são obrigatórios.");
            }
            if (_produtoRepository.ProdutoExists(produto.IdProduto))
            {
                throw new InvalidOperationException("Já existe um produto com esse ID.");
            }
            if (produto.Quantidade < 0)
            {
                throw new ArgumentException("Quantidade do produto não pode ser negativa.");
            }
            if (produto.Peso < 0)
            {
                throw new ArgumentException("Peso do produto não pode ser negativo.");
            }

            _produtoRepository.AddProduto(produto);
        }

        public void UpdateProduto(Produto produto)
        {
            if (string.IsNullOrWhiteSpace(produto.IdProduto))
            {
                throw new ArgumentNullException(nameof(produto), "ID do produto deve ser preenchido.");
            }

            var existingProduto = _produtoRepository.GetProdutoById(produto.IdProduto);

            if (existingProduto is null)
            {
                throw new InvalidOperationException("Produto não encontrado.");
            }

            if (!string.IsNullOrWhiteSpace(produto.Nome))
            {
                existingProduto.Nome = produto.Nome;
            }

            existingProduto.Quantidade = produto.Quantidade;

            existingProduto.Peso = produto.Peso;

            _produtoRepository.UpdateProduto(existingProduto);
        }

        public void DeleteProduto(string id)
        {
            if (string.IsNullOrWhiteSpace(id))
            {
                throw new ArgumentNullException(nameof(id), "ID do produto deve ser preenchido.");
            }
            if (!_produtoRepository.ProdutoExists(id))
            {
                throw new InvalidOperationException("Produto não encontrado.");
            }

            _produtoRepository.DeleteProduto(id);
        }

        public bool ProdutoExists(string id)
        {
            if (string.IsNullOrWhiteSpace(id))
            {
                throw new ArgumentNullException(nameof(id), "ID do produto deve ser preenchido.");
            }

            return _produtoRepository.ProdutoExists(id);
        }

        public IEnumerable<Produto> SearchProdutos(string? nome = null, string? id = null)
        {
            if (string.IsNullOrWhiteSpace(nome) && string.IsNullOrWhiteSpace(id))
            {
                throw new ArgumentException("Pelo menos um critério de pesquisa deve ser fornecido.");
            }

            return _produtoRepository.SearchProdutos(nome, id);
        }

        /*public IEnumerable<Produto> GetProdutosByNome(string nome)
        {
            return _produtoRepository.GetProdutosByNome(nome);
        }*/

    }
}