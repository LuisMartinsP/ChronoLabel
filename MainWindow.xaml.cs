using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using ChronoLabel.Services;

namespace ChronoLabel
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private readonly IAuthService _authService;
        public MainWindow(IAuthService authService)
        {
            InitializeComponent(); 
            _authService = authService;
        }
        private void LoginButton_Click(object sender, RoutedEventArgs e)
        {
            string cpf = CpfTextBox.Text;
            string senha = SenhaBox.Password;

            if (_authService.Login(cpf, senha))
            {
                MessageBox.Show("Login bem-sucedido!", "Sucesso", MessageBoxButton.OK, MessageBoxImage.Information);

                var usuario = _authService.CurrentUser!;

                if (usuario.Tipo == "administrador")
                {
                    //var adminWindow = new AdminWindow(_authService);
                    //adminWindow.Show();
                }
                else
                {
                    //var operadorWindow = new OperadorWindow(_authService);
                    //operadorWindow.Show();
                }

                this.Close();
            }
            else
            {
                MessageBox.Show("CPF ou senha inválidos.", "Erro", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void CpfTextBox_TextChanged(object sender, TextChangedEventArgs e)
        {

        }
    }
}
