using System.Collections.Generic;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using ChronoLabelApp.Models;
using ChronoLabelApp.Data;

namespace ChronoLabelApp.Repositories
{
    public class RelatorioRepository
    {
        private readonly ChronoLabelContext _context;

        public RelatorioRepository(ChronoLabelContext context)
        {
            _context = context;
        }

        public IEnumerable<Relatorio> GetAllRelatorios()
        {
            return _context.Relatorios
                           .Include(r => r.CpfUsuarioNavigation)
                           .Include(r => r.IdProdutoNavigation)
                           .ToList();
        }

        public Relatorio GetRelatorioById(int id)
        {
            return _context.Relatorios
                           .Include(r => r.CpfUsuarioNavigation)
                           .Include(r => r.IdProdutoNavigation)
                           .FirstOrDefault(r => r.IdRelatorio == id);
        }

        public void AddRelatorio(Relatorio relatorio)
        {
            _context.Relatorios.Add(relatorio);
            _context.SaveChanges();
        }

        public void UpdateRelatorio(Relatorio relatorio)
        {
            _context.Relatorios.Update(relatorio);
            _context.SaveChanges();
        }

        public void DeleteRelatorio(int id)
        {
            var relatorio = _context.Relatorios.Find(id);
            if (relatorio != null)
            {
                _context.Relatorios.Remove(relatorio);
                _context.SaveChanges();
            }
        }

        public bool RelatorioExists(int id)
        {
            return _context.Relatorios.Any(r => r.IdRelatorio == id);
        }

        public IEnumerable<Relatorio> GetRelatoriosByUsuario(string cpfUsuario)
        {
            return _context.Relatorios
                           .Include(r => r.IdProdutoNavigation)
                           .Where(r => r.CpfUsuario == cpfUsuario)
                           .ToList();
        }

        public IEnumerable<Relatorio> GetRelatoriosByProduto(string idProduto)
        {
            return _context.Relatorios
                           .Include(r => r.CpfUsuarioNavigation)
                           .Where(r => r.IdProduto == idProduto)
                           .ToList();
        }

        public IEnumerable<Relatorio> GetRelatoriosByDateRange(DateTime startDate, DateTime endDate)
        {
            return _context.Relatorios
                           .Where(r => r.DataCriacao >= startDate && r.DataCriacao <= endDate)
                           .ToList();
        }

        public void DeleteRelatoriosByUsuario(string cpfUsuario)
        {
            var relatorios = _context.Relatorios.Where(r => r.CpfUsuario == cpfUsuario).ToList();
            if (relatorios.Any())
            {
                _context.Relatorios.RemoveRange(relatorios);
                _context.SaveChanges();
            }
        }

        public int GetQuantidadeDeProdutosPorUsuario(string cpfUsuario, DateTime? startDate = null, DateTime? endDate = null)
        {
            var query = _context.Relatorios.Where(r => r.CpfUsuario == cpfUsuario);

            if (startDate.HasValue)
            {
                query = query.Where(r => r.DataCriacao >= startDate.Value);
            }

            if (endDate.HasValue)
            {
                query = query.Where(r => r.DataCriacao <= endDate.Value);
            }

            return query.Sum(r => r.QuantidadeProdutoOperado);
        }
        
    }
}