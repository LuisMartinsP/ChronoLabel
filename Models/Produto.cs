using System;
using System.Collections.Generic;

namespace ChronoLabel.Models;

public partial class Produto
{
    public string IdProduto { get; set; } = null!;

    public string Nome { get; set; } = null!;

    public int Peso { get; set; }

    public int Quantidade { get; set; }

    public virtual ICollection<Relatorio> Relatorios { get; set; } = new List<Relatorio>();
}
