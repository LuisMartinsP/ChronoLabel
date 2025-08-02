using System;
using System.Collections.Generic;
using ChronoLabel.Models;
using ChronoLabel.Repositories;

namespace ChronoLabel.Services
{
    public class UsuarioService
    {
        private readonly IUsuarioRepository _usuarioRepository;

        public UsuarioService(IUsuarioRepository usuarioRepository)
        {
            _usuarioRepository = usuarioRepository;
        }
        public IEnumerable<Usuario> GetAllUsuarios()
        {
            return _usuarioRepository.GetAllUsuarios();
        }
        public Usuario GetUsuarioByCpf(string cpf)
        {
            return _usuarioRepository.GetUsuarioByCpf(cpf);
        }
        public void AddUsuario(Usuario usuario)
        {
            _usuarioRepository.AddUsuario(usuario);
        }

        public void UpdateUsuario(Usuario usuario)
        {
            _usuarioRepository.UpdateUsuario(usuario);
        }

        public void DeleteUsuario(string cpf)
        {
            _usuarioRepository.DeleteUsuario(cpf);
        }

        public bool UsuarioExists(string cpf)
        {
            return _usuarioRepository.UsuarioExists(cpf);
        }

        public IEnumerable<Usuario> SearchUsuarios(string? nome = null, string? cpf = null)
        {
            return _usuarioRepository.SearchUsuarios(nome, cpf);
        }
        /*public IEnumerable<Usuario> GetUsuariosByNome(string nome)
        {
            return _usuarioRepository.GetUsuariosByNome(nome);
        }*/
    }
}