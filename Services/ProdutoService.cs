using System;
using System.Collections.Generic;
using ChronoLabel.Models;
using ChronoLabel.Repositories;

namespace ChronoLabel.Services
{
    public class ProdutoService
    {
        private readonly ProdutoRepository _produtoRepository;

        public ProdutoService(ProdutoRepository produtoRepository)
        {
            _produtoRepository = produtoRepository;
        }

        public IEnumerable<Produto> GetAllProdutos()
        {
            return _produtoRepository.GetAllProdutos();
        }

        public Produto GetProdutoById(string id)
        {
            return _produtoRepository.GetProdutoById(id);
        }

        public void AdicionarProduto(Produto produto)
        {
            _produtoRepository.AdicionarProduto(produto);
        }

        public void AtualizarProduto(Produto produto)
        {
            _produtoRepository.AtualizarProduto(produto);
        }

        public void DeletarProduto(string id)
        {
            _produtoRepository.DeletarProduto(id);
        }

        public bool ProdutoExists(string id)
        {
            return _produtoRepository.ProdutoExists(id);
        }

        public IEnumerable<Produto> SearchProdutos(string? nome = null, string? id = null)
        {
            return _produtoRepository.SearchProdutos(nome, id);
        }

        /*public IEnumerable<Produto> GetProdutosByNome(string nome)
        {
            return _produtoRepository.GetProdutosByNome(nome);
        }*/

    }
}