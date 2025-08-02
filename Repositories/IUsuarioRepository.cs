using System;
using System.Collections.Generic;
using ChronoLabel.Models;

namespace ChronoLabel.Repositories
{
    public interface IUsuarioRepository
    {
        IEnumerable<Usuario> GetAllUsuarios();
        Usuario GetUsuarioByCpf(string cpf);
        void AddUsuario(Usuario usuario);
        void UpdateUsuario(Usuario usuario);
        void DeleteUsuario(string cpf);
        bool UsuarioExists(string cpf);
        IEnumerable<Usuario> SearchUsuarios(string? nome = null, string? cpf = null);
    }
}