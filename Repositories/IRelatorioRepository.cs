using System;
using System.Collections.Generic;
using ChronoLabel.Models;

namespace ChronoLabel.Repositories
{
    public interface IRelatorioRepository
    {
        IEnumerable<Relatorio> GetAllRelatorios();
        Relatorio? GetRelatorioById(int id);
        void AddRelatorio(Relatorio relatorio);
        void UpdateRelatorio(Relatorio relatorio);
        void DeleteRelatorio(int id);
        bool RelatorioExists(int id);
        IEnumerable<Relatorio> SearchRelatorios(string? nome = null, int? id = null);

        IEnumerable<Relatorio> GetRelatoriosByDateRange(DateTime startDate, DateTime endDate);

        IEnumerable<Relatorio> GetRelatoriosByCpfUsuario(string cpfUsuario);

        int GetQuantidadeDeProdutosByUsuarioAndDataRange(string cpfUsuario, DateTime? startDate = null, DateTime? endDate = null);
    }
}