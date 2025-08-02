using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using ChronoLabel.Data;
using ChronoLabel.Models;

namespace ChronoLabel.Repositories
{
    public class UsuarioRepository : IUsuarioRepository
    {
        private readonly ChronoLabelContext _context;
        public UsuarioRepository(ChronoLabelContext context)
        {
            _context = context;
        }
        public IEnumerable<Usuario> GetAllUsuarios()
        {
            return _context.Usuarios
                           .Include(u => u.Relatorios)
                           .ToList();
        }

        public Usuario GetUsuarioByCpf(string cpf)
        {
            return _context.Usuarios
                           .Include(u => u.Relatorios)
                           .FirstOrDefault(u => u.Cpf == cpf);
        }

        public void AddUsuario(Usuario usuario)
        {
            _context.Usuarios.Add(usuario);
            _context.SaveChanges();
        }

        public void UpdateUsuario(Usuario usuario)
        {
            _context.Usuarios.Update(usuario);
            _context.SaveChanges();
        }

        public void DeleteUsuario(string cpf)
        {
            var usuario = GetUsuarioByCpf(cpf);
            if (usuario != null)
            {
                _context.Usuarios.Remove(usuario);
                _context.SaveChanges();
            }
        }

        public bool UsuarioExists(string cpf)
        {
            return _context.Usuarios.Any(u => u.Cpf == cpf);
        }

        public IEnumerable<Usuario> SearchUsuarios(string? nome = null, string? cpf = null)
        {
            var query = _context.Usuarios.AsQueryable();

            if (!string.IsNullOrEmpty(nome))
            {
                query = query.Where(u => u.Nome.StartsWith(nome));
            }

            if (!string.IsNullOrEmpty(cpf))
            {
                query = query.Where(u => u.Cpf.StartsWith(cpf));
            }

            query = query.Include(u => u.Relatorios);

            return query.ToList();
        }

        public IEnumerable<Usuario> GetUsuariosByNome(string nome)
        {
            return _context.Usuarios.Where(u => u.Nome.StartsWith(nome)).ToList();
        }

    }
}