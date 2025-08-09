using ChronoLabel.Data;
using ChronoLabel.Repositories;
using ChronoLabel.Services;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;

namespace ChronoLabel
{
    public partial class App : Application
    {
        public IServiceProvider? ServiceProvider { get; private set; }
        public IConfiguration? Configuration { get; private set; }

        protected override void OnStartup(StartupEventArgs e)
        {
            base.OnStartup(e);



            // 1. Configura o builder para ler appsettings.json
            var builder = new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory()) // pasta onde está seu app
                .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true);

            Configuration = builder.Build();

            // 2. Configura serviços e DI (Dependency Injection)
            var serviceCollection = new ServiceCollection();

            // Registra a configuração para DI
            serviceCollection.AddSingleton<IConfiguration>(Configuration);

            // 3. Registra seu DbContext passando a connection string lida do appsettings.json|
            serviceCollection.AddDbContext<ChronoLabelContext>(options =>
                options.UseMySql(Configuration.GetConnectionString("ChronoLabelDb"), 
                ServerVersion.AutoDetect(Configuration.GetConnectionString("ChronoLabelDb"))));

            // (Opcional) registra outros serviços e repositórios aqui
            serviceCollection.AddTransient<IUsuarioRepository, UsuarioRepository>();

            serviceCollection.AddTransient<IAuthService, AuthService>();

            serviceCollection.AddTransient<MainWindow>();

            ServiceProvider = serviceCollection.BuildServiceProvider();

            // 4. Exemplo de como iniciar a MainWindow passando o ServiceProvider
            var mainWindow = ServiceProvider.GetRequiredService<MainWindow>();
            mainWindow.Show();
        }
    }
}