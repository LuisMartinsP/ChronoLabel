using System;
using System.Collections.Generic;

namespace ChronoLabel.Models;

public partial class Usuario
{
    public string Cpf { get; set; } = null!;

    public string Nome { get; set; } = null!;

    public string Senha { get; set; } = null!;

    public string Tipo { get; set; } = null!;

    public virtual ICollection<Relatorio> Relatorios { get; set; } = new List<Relatorio>();
}
