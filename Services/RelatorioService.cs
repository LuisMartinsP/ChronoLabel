using System;
using System.Collections.Generic;
using ChronoLabel.Repositories;
using ChronoLabel.Models;

namespace ChronoLabel.Services
{
    public class RelatorioService
    {
        private readonly IRelatorioRepository _relatorioRepository;

        public RelatorioService(IRelatorioRepository relatorioRepository)
        {
            _relatorioRepository = relatorioRepository;
        }

        public IEnumerable<Relatorio> GetAllRelatorios()
        {
            return _relatorioRepository.GetAllRelatorios();
        }

        public Relatorio? GetRelatorioById(int id)
        {
            if(id < 0)
            {
                throw new ArgumentOutOfRangeException(nameof(id), "ID do relatório não pode ser negativo.");
            }

            if (!_relatorioRepository.RelatorioExists(id))
            {
                throw new InvalidOperationException("Relatório não encontrado.");
            }

            return _relatorioRepository.GetRelatorioById(id);
        }

        public void AddRelatorio(Relatorio relatorio)
        {
            if (relatorio == null)
            {
                throw new ArgumentNullException(nameof(relatorio), "Relatório não pode ser nulo.");
            }
            if (string.IsNullOrWhiteSpace(relatorio.CpfUsuario) || string.IsNullOrWhiteSpace(relatorio.IdProduto))
            {
                throw new ArgumentException("CPF do usuário e ID do produto são obrigatórios.");
            }
            if (relatorio.DataCriacao == default || relatorio.DataTermino == default)
            {
                throw new ArgumentException("Datas de criação e término devem ser válidas.");
            }
            if (relatorio.DataTermino < relatorio.DataCriacao)
            {
                throw new ArgumentException("Data de término não pode ser anterior à data de criação.");
            }

            if (relatorio.Duracao <= TimeSpan.Zero)
            {
                throw new ArgumentException("Tempo total do relatório deve ser maior que zero.");
            }

            _relatorioRepository.AddRelatorio(relatorio);
        }

        public void UpdateRelatorio(Relatorio relatorio)
        {
            _relatorioRepository.UpdateRelatorio(relatorio);
        }

        public void DeleteRelatorio(int id)
        {
            if (id < 0)
            {
                throw new ArgumentOutOfRangeException(nameof(id), "ID do relatório deve ser maior ou igual a zero.");
            }
            if (!_relatorioRepository.RelatorioExists(id))
            {
                throw new InvalidOperationException("Relatório não encontrado.");
            }


            _relatorioRepository.DeleteRelatorio(id);
        }

        public bool RelatorioExists(int id)
        {
            return _relatorioRepository.RelatorioExists(id);
        }

        public IEnumerable<Relatorio> GetRelatoriosByPeriodo(DateTime dataInicio, DateTime dataFim)
        {
            if (dataInicio > dataFim)
            {
                throw new ArgumentException("A data de início não pode ser posterior à data de término.");
            }
            if (dataInicio == default || dataFim == default)
            {
                throw new ArgumentException("Datas de início e término devem ser válidas.");
            }


            return _relatorioRepository.GetRelatoriosByDateRange(dataInicio, dataFim);
        }



        public int GetQuantidadeProdutosByUsuarioAndDateRange(string cpfUsuario, DateTime startDate, DateTime endDate)
        {
            if (string.IsNullOrWhiteSpace(cpfUsuario))
            {
                throw new ArgumentException("CPF do usuário não pode ser nulo ou vazio.", nameof(cpfUsuario));
            }
            if (startDate > endDate)
            {
                throw new ArgumentException("A data de início não pode ser posterior à data de término.");
            }
            if (startDate == default || endDate == default)
            {
                throw new ArgumentException("Datas de início e término devem ser válidas.");
            }
            return _relatorioRepository.GetQuantidadeDeProdutosByUsuarioAndDataRange(cpfUsuario, startDate, endDate);
        }


    }

    /*public IEnumerable<Relatorio> SearchRelatorios(string cpfUsuario = null, string idProduto = null)
       {
           return _relatorioRepository.SearchRelatorios(cpfUsuario, idProduto);
       }*/

    /*public IEnumerable<Relatorio> GetRelatoriosByCpfUsuario(string cpfUsuario)
    {
        return _relatorioRepository.GetRelatoriosByCpfUsuario(cpfUsuario);
    }*/

    /*public IEnumerable<Relatorio> GetRelatoriosByIdProduto(string idProduto)
    {
        return _relatorioRepository.GetRelatoriosByIdProduto(idProduto);
    }*/

    /*public IEnumerable<Relatorio> GetRelatoriosByNomeProduto(string nomeProduto)
    {
        return _relatorioRepository.GetRelatoriosByNomeProduto(nomeProduto);
    }*/

    /*public IEnumerable<Relatorio> GetRelatoriosByNomeUsuario(string nomeUsuario)
    {
        return _relatorioRepository.GetRelatoriosByNomeUsuario(nomeUsuario);
    }*/

    /*public IEnumerable<Relatorio> GetRelatoriosByData(DateTime data)
    {
        return _relatorioRepository.GetRelatoriosByData(data);
    }*/
        
    /*public IEnumerable<Relatorio> GetRelatoriosByUsuarioAndProduto(string cpfUsuario, int idProduto)
    {
        return _relatorioRepository.GetRelatoriosByUsuarioAndProduto(cpfUsuario, idProduto);
    }*/
}