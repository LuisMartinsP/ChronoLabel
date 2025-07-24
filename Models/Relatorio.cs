using System;
using System.Collections.Generic;

namespace ChronoLabel.Models;

public partial class Relatorio
{
    public int IdRelatorio { get; set; }

    public string CpfUsuario { get; set; } = null!;

    public string IdProduto { get; set; } = null!;

    public int QuantidadeProdutoOperado { get; set; }

    public DateTime DataCriacao { get; set; }

    public DateTime DataTermino { get; set; }

    public TimeSpan Duracao { get; set; }

    public virtual Usuario CpfUsuarioNavigation { get; set; } = null!;

    public virtual Produto IdProdutoNavigation { get; set; } = null!;
}
