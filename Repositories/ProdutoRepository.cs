using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using ChronoLabel.Data;
using ChronoLabel.Models;

namespace ChronoLabel.Repositories
{
    public class ProdutoRepository
    {
        private readonly ChronoLabelContext _context;

        public ProdutoRepository(ChronoLabelContext context)
        {
            _context = context;
        }

        public IEnumerable<Produto> GetAllProdutos()
        {
            return _context.Produtos
                .Include(p => p.Relatorios)
                .ToList();
        }

        public Produto? GetProdutoById(string id)
        {
            return _context.Produtos
                .Include(p => p.Relatorios)
                .FirstOrDefault(p => p.IdProduto == id);
        }

        public void AdicionarProduto(Produto produto)
        {
            _context.Produtos.Add(produto);
            _context.SaveChanges();
        }
        public void AtualizarProduto(Produto produto)
        {
            _context.Produtos.Update(produto);
            _context.SaveChanges();
        }

        public void DeletarProduto(string id)
        {
            var produto = _context.Produtos.Find(id);
            if (produto != null)
            {
                _context.Produtos.Remove(produto);
                _context.SaveChanges();
            }
        }

        public bool ProdutoExists(string id)
        {
            return _context.Produtos.Any(p => p.IdProduto == id);
        }

        public IEnumerable<Produto> SearchProdutos(string? nome = null, string? id = null)
        {
            var query = _context.Produtos.AsQueryable();
            if (!string.IsNullOrEmpty(nome))
            {
                query = query.Where(p => p.Nome == nome);
            }
            if (!string.IsNullOrEmpty(id))
            {
                query = query.Where(p => p.IdProduto == id);
            }

            query = query.Include(p => p.Relatorios);

            return query.ToList();
        }
    }
}