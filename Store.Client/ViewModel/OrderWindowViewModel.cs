using AutoMapper;
using Prism.Events;
using Prism.Services.Dialogs;
using Store.Client.ViewModel.Base;
using Store.Client.ViewModel.Entities;
using Store.Interfaces.Communication;
using Store.Model.Entities;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Threading.Tasks;

namespace Store.Client.ViewModel
{
    public class OrderWindowViewModel : ItemDialogViewModelBase<Order, OrderViewModel>
    {
        public ObservableCollection<ClientViewModel> Clients { get; set; }
        private ClientViewModel selectedClient;
        public ClientViewModel SelectedClient
        {
            get => selectedClient;
            set => SetProperty(ref selectedClient, value);
        }

        public ObservableCollection<ProductViewModel> Products { get; set; }
        private ProductViewModel selectedProduct;
        public ProductViewModel SelectedProduct
        {
            get => selectedProduct;
            set => SetProperty(ref selectedProduct, value);
        }

        protected override string Endpoint => "purchases";
        private string clientEndpoint = "clients";
        private string productEndpoint = "products";

        public OrderWindowViewModel(IMapper mapper, IEventAggregator eventAggregator, IRestClient restClient, IDialogService dialogService)
            : base(mapper, eventAggregator, restClient, dialogService)
        {
            Clients = new ObservableCollection<ClientViewModel>();
            Products = new ObservableCollection<ProductViewModel>();
        }

        public override async void OnDialogOpened(IDialogParameters parameters)
        {
            base.OnDialogOpened(parameters);

            await InitClients();
            await InitProducts();
        }

        private async Task InitClients()
        {
            var clients = await RestClient.GetRequest(clientEndpoint).ExecuteAsync<List<Model.Entities.Client>>();
            var entities = Mapper.Map<List<Model.Entities.Client>, List<ClientViewModel>>(clients);

            foreach (var entity in entities)
            {
                Clients.Add(entity);
            }
        }

        private async Task InitProducts()
        {
            var products = await RestClient.GetRequest(productEndpoint).ExecuteAsync<List<Product>>();
            var entities = Mapper.Map<List<Product>, List<ProductViewModel>>(products);

            foreach (var entity in entities)
            {
                Products.Add(entity);
            }
        }

        protected override Task<bool> Save()
        {
            if (SelectedClient != null)
            {
                Entity.Client = SelectedClient;
                Entity.ClientId = SelectedClient.Id;
            }

            if (SelectedProduct != null)
            {
                Entity.Product = SelectedProduct;
                Entity.ProductId = SelectedProduct.Id;
            }

            Entity.SKU = Guid.NewGuid().ToString();

            return base.Save();
        }

    }
}
