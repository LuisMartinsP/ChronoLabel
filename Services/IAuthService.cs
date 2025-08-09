using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ChronoLabel.Models;

namespace ChronoLabel.Services
{
    public interface IAuthService
    {
        bool Login(string cpf, string senha);
        void Logout();
        void Cadastrar(Usuario usuario);
        Usuario? CurrentUser{get ; }
    }
}
