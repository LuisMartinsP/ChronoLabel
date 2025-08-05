using System;
using System.Collections.Generic;
using ChronoLabel.Models;
using ChronoLabel.Repositories;
using DocumentValidator;

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
        public Usuario? GetUsuarioByCpf(string cpf)
        {
            return _usuarioRepository.GetUsuarioByCpf(cpf);
        }
        public void AddUsuario(Usuario usuario)
        {
            if (usuario is null)
            {
                throw new ArgumentNullException(nameof(usuario), "Usuário não pode ser nulo.");
            }

            if(!CpfValidation.Validate(usuario.Cpf))
            {
                throw new ArgumentException("CPF inválido.", nameof(usuario.Cpf));
            }

            if (string.IsNullOrWhiteSpace(usuario.Cpf) ||
            string.IsNullOrWhiteSpace(usuario.Nome) ||
            string.IsNullOrWhiteSpace(usuario.Senha) ||
            string.IsNullOrWhiteSpace(usuario.Tipo))
            {
                throw new ArgumentException("Todos os campos do usuário são obrigatórios.");
            }

            if (_usuarioRepository.UsuarioExists(usuario.Cpf))
            {
                throw new InvalidOperationException("Já existe um usuário com esse CPF.");
            }
            _usuarioRepository.AddUsuario(usuario);
        }

        public void UpdateUsuario(Usuario usuario)
        {
            if (string.IsNullOrWhiteSpace(usuario.Cpf))
            {
                throw new ArgumentNullException(nameof(usuario), "CPF deve ser preenchido.");
            }

            var existingUsuario = _usuarioRepository.GetUsuarioByCpf(usuario.Cpf);

            if (existingUsuario is null)
            {
                throw new InvalidOperationException("Usuário não encontrado.");
            }

            if (!string.IsNullOrWhiteSpace(usuario.Nome))
            {
                existingUsuario.Nome = usuario.Nome;
            }

            if (!string.IsNullOrWhiteSpace(usuario.Senha))
            {
                existingUsuario.Senha = usuario.Senha;
            }

            if (!string.IsNullOrWhiteSpace(usuario.Tipo))
            {
                existingUsuario.Tipo = usuario.Tipo;
            }

            _usuarioRepository.UpdateUsuario(existingUsuario);
        }

        public void DeleteUsuario(string cpf)
        {
            if (!_usuarioRepository.UsuarioExists(cpf))
            {
                throw new InvalidOperationException("Usuário não encontrado.");
            }

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