using Prism.Ioc;
using Prism.Mvvm;
using System;
using System.Reflection;
using System.Windows;
using Unity;
using Store.Client.View;
using Store.Interfaces.Service;
using Store.Common.Service;
using Prism.Regions;
using Store.Common.Constants;
using Prism.Events;
using AutoMapper;
using Store.Client.Mapping;
using System.Net.Http;
using Store.Common.Communication;
using Store.Interfaces.Communication;
using Prism.Services.Dialogs;
using Store.Client.ViewModel;
using Store.Model.Entities;
using Tiny.RestClient;
using System.Threading;
using System.Threading.Tasks;
using Store.Client.Communication;

namespace Store.Client
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Prism.Unity.PrismApplication
    {
        protected override Window CreateShell()
        {
            return Container.Resolve<MainWindowView>();
        }

        protected override void RegisterTypes(IContainerRegistry containerRegistry)
        {
            containerRegistry.RegisterForNavigation<HomeView>();
            containerRegistry.RegisterForNavigation<OrdersView>();
            containerRegistry.RegisterForNavigation<ProductsView>();
            containerRegistry.RegisterForNavigation<ClientsView>();
            containerRegistry.RegisterForNavigation<ShipmentsView>();

            containerRegistry.RegisterDialog<LogWindowView, LogWindowViewModel>(nameof(LogWindowView));
            containerRegistry.RegisterDialog<ClientWindowView, ClientWindowViewModel>(nameof(Model.Entities.Client));
            containerRegistry.RegisterDialog<AddressWindowView, AddressWindowViewModel>(nameof(Address));
            containerRegistry.RegisterDialog<OrderWindowView, OrderWindowViewModel>(nameof(Order));
            containerRegistry.RegisterDialog<ProductWindowView, ProductWindowViewModel>(nameof(Product));
            containerRegistry.RegisterDialog<ShipmentWindowView, ShipmentWindowViewModel>(nameof(Shipment));
            containerRegistry.RegisterSingleton<IDialogService, DialogService>();

            containerRegistry.RegisterSingleton<IListener, ClientListener>();
            containerRegistry.RegisterSingleton<IEventAggregator, EventAggregator>();
            containerRegistry.RegisterSingleton<IRegionManager, RegionManager>();

            containerRegistry.RegisterSingleton<IConfigurationService, ConfigurationService>();

            RegisterAutoMapper(containerRegistry);
            RegisterRestClient(containerRegistry);
        }

        private void RegisterAutoMapper(IContainerRegistry containerRegistry)
        {
            var config = new MapperConfiguration(cfg => cfg.AddProfile<ShopProfile>());

            containerRegistry.RegisterInstance(config.CreateMapper());
        }

        private void RegisterRestClient(IContainerRegistry containerRegistry)
        {
            var configurationService = Container.Resolve<IConfigurationService>();
            var listener = Container.Resolve<IListener>();
            var client = new RestClient(new HttpClient(), configurationService.ReadString(SettingKeys.DataAPI));

            client.Settings.Listeners.Add(listener);

            containerRegistry.RegisterInstance<IRestClient>(client);
        }

        protected override void ConfigureViewModelLocator()
        {
            base.ConfigureViewModelLocator();

            ViewModelLocationProvider.SetDefaultViewTypeToViewModelTypeResolver((viewType) =>
            {
                var viewName = viewType.Name;

                if (viewName.EndsWith("View"))
                    viewName = viewName.Replace("View", string.Empty);

                var viewAssemblyName = viewType.GetTypeInfo().Assembly.FullName;
                var viewModelName = $"Store.Client.ViewModel.{viewName}ViewModel, {viewAssemblyName}";

                return Type.GetType(viewModelName);
            });
        }

        protected override void OnInitialized()
        {
            base.OnInitialized();

            Container.Resolve<IRegionManager>().RequestNavigate(RegionNames.Content, nameof(HomeView));
        }
    }
}
