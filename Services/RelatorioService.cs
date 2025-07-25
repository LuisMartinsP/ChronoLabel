using System.Collections.Generic;
using ChronoLabelApp.Repositories;
using ChronoLabelApp.Models;

namespace ChronoLabelApp.Services
{
    public class RelatorioService
    {
        private readonly RelatorioRepository _relatorioRepository;

        public RelatorioService(RelatorioRepository relatorioRepository)
        {
            _relatorioRepository = relatorioRepository;
        }

        public IEnumerable<Relatorio> GetAllRelatorios()
        {
            return _relatorioRepository.GetAllRelatorios();
        }

        public Relatorio GetRelatorioById(int id)
        {
            return _relatorioRepository.GetRelatorioById(id);
        }

        public void AddRelatorio(Relatorio relatorio)
        {
            _relatorioRepository.AddRelatorio(relatorio);
        }

        public void UpdateRelatorio(Relatorio relatorio)
        {
            _relatorioRepository.UpdateRelatorio(relatorio);
        }

        public void DeleteRelatorio(int id)
        {
            _relatorioRepository.DeleteRelatorio(id);
        }

        public bool RelatorioExists(int id)
        {
            return _relatorioRepository.RelatorioExists(id);
        }

        public IEnumerable<Relatorio> SearchRelatorios(string cpfUsuario = null, string idProduto = null)
        {
            return _relatorioRepository.SearchRelatorios(cpfUsuario, idProduto);
        }

        public IEnumerable<Relatorio> GetRelatoriosByCpfUsuario(string cpfUsuario)
        {
            return _relatorioRepository.GetRelatoriosByCpfUsuario(cpfUsuario);
        }

        public IEnumerable<Relatorio> GetRelatoriosByIdProduto(int idProduto)
        {
            return _relatorioRepository.GetRelatoriosByIdProduto(idProduto);
        }

        public IEnumerable<Relatorio> GetRelatoriosByNomeProduto(string nomeProduto)
        {
            return _relatorioRepository.GetRelatoriosByNomeProduto(nomeProduto);
        }

        public IEnumerable<Relatorio> GetRelatoriosByNomeUsuario(string nomeUsuario)
        {
            return _relatorioRepository.GetRelatoriosByNomeUsuario(nomeUsuario);
        }

        /*public IEnumerable<Relatorio> GetRelatoriosByData(DateTime data)
        {
            return _relatorioRepository.GetRelatoriosByData(data);
        }*/

        public IEnumerable<Relatorio> GetRelatoriosByPeriodo(DateTime dataInicio, DateTime dataFim)
        {
            return _relatorioRepository.GetRelatoriosByPeriodo(dataInicio, dataFim);
        }

        /*public IEnumerable<Relatorio> GetRelatoriosByUsuarioAndProduto(string cpfUsuario, int idProduto)
        {
            return _relatorioRepository.GetRelatoriosByUsuarioAndProduto(cpfUsuario, idProduto);
        }*/

        public int GetQuantidadeProdutosByUsuarioAndDateRange(string cpfUsuario, DateTime startDate, DateTime endDate)
        {
            return _relatorioRepository.GetQuantidadeDeProdutosPorUsuario(cpfUsuario, startDate, endDate);
        }


    }
}