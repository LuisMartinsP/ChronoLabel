using System;
using System.Collections.Generic;
using ChronoLabel.Models;

namespace ChronoLabel.Repositories
{
    public interface IProdutoRepository
    {
        IEnumerable<Produto> GetAllProdutos();
        Produto? GetProdutoById(string id);
        void AddProduto(Produto produto);
        void UpdateProduto(Produto produto);
        void DeleteProduto(string id);
        bool ProdutoExists(string id);
        IEnumerable<Produto> SearchProdutos(string? nome = null, string? id = null);
    }
}