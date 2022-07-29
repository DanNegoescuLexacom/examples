using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;
using Autofac;
using DevExpress.Mvvm;

namespace DialogService
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        private IContainer _container;

        public App()
        {
            var builder = new ContainerBuilder();
            builder.RegisterType<MainViewModel>();

            var viewModel = new ServiceViewModel();
            viewModel.RegisterServices(builder);
            _container = builder.Build();
        }

        protected override void OnStartup(StartupEventArgs e)
        {
            var vm = (ViewModelLocator)Application.Current.Resources["ViewModelLocator"];
            vm.SetLifetimeScope(_container.BeginLifetimeScope());
        }
    }

    public class ServiceViewModel : ISupportServices  
    {  
        public IServiceContainer ServiceContainer { get; private set; }  

        public ServiceViewModel()
        {
            ServiceContainer = new ServiceContainer(this);
        }

        public void RegisterServices(ContainerBuilder builder)
        {
            var dialogService = ServiceContainer.GetService<IDialogService>();
            builder.Register(c => dialogService).As<IDialogService>();  
        }
    }  
}
