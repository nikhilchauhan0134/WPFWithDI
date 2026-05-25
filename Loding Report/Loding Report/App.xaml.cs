using Loding_Report.Database;
using Loding_Report.Repositories;
using Loding_Report.Services;
using Loding_Report.Views;
using Microsoft.Extensions.DependencyInjection;
using System.Configuration;
using System.Data;
using System.Windows;

namespace Loding_Report
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        public static IServiceProvider ServiceProvider { get; private set; }
        protected override void OnStartup(StartupEventArgs e)
        {
            base.OnStartup(e);

            var serviceCollection = new ServiceCollection();
            ConfigureServices(serviceCollection);

            ServiceProvider = serviceCollection.BuildServiceProvider();

            // Resolve and show the main window using DI
            var mainWindow = ServiceProvider.GetRequiredService<ListBillReport>();
            mainWindow.Show();
        }
        private void ConfigureServices(IServiceCollection services)
        {
            services.AddSingleton<ISqlDatabaseUtility, SqlDatabaseUtility>();
            // Register your data services
            services.AddSingleton<IReportDataService, ReportDataService>();

            // Windows must also be registered in DI so their parameters can be injected
            services.AddTransient<ListBillReport>();
        }
    }

}
