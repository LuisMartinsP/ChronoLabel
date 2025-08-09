using ChronoLabel.Repositories;
using ChronoLabel.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BCrypt.Net;

namespace ChronoLabel.Services
{
    public class AuthService : IAuthService
    {
        private readonly IUsuarioRepository _usuarioRepository;
        private Usuario? _currentUser;

        public Usuario? CurrentUser => _currentUser;

        public AuthService(IUsuarioRepository usuarioRepository)
        {
            _usuarioRepository = usuarioRepository;
        }

        public bool Login(string cpf, string senha)
        {
            var usuario = _usuarioRepository.GetUsuarioByCpf(cpf);
            if (usuario == null) return false;
            if (BCrypt.Net.BCrypt.Verify(senha, usuario.Senha))
            {
                _currentUser = usuario;
                return true;
            }
            return false;
        }

        public void Logout()
        {
            _currentUser = null;
        }

        public void Cadastrar(Usuario usuario)
        {
            usuario.Senha = BCrypt.Net.BCrypt.HashPassword(usuario.Senha);
            _usuarioRepository.AddUsuario(usuario);
        }

    }
}
