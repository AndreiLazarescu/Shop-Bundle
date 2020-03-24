using AutoMapper;
using Prism.Commands;
using Prism.Events;
using Prism.Services.Dialogs;
using Store.Client.ViewModel.Base;
using Store.Client.ViewModel.Entities;
using Store.Interfaces.Communication;
using Store.Model.Entities;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Input;

namespace Store.Client.ViewModel
{
    public class ShipmentWindowViewModel : ItemDialogViewModelBase<Shipment, ShipmentViewModel>
    {
        public ObservableCollection<OrderViewModel> Orders { get; set; }
        public ObservableCollection<ClientViewModel> Clients { get; set; }
        private ClientViewModel selectedClient;
        public ClientViewModel SelectedClient
        {
            get => selectedClient;
            set => SetProperty(ref selectedClient, value);
        }

        public ICommand SelectClientCommand { get; set; }

        protected override string Endpoint => "orders";
        private string purchasesEndpoint = "purchases";
        private string clientEndpoint = "clients";

        public ShipmentWindowViewModel(IMapper mapper, IEventAggregator eventAggregator, IRestClient restClient, IDialogService dialogService)
            : base(mapper, eventAggregator, restClient, dialogService)
        {
            Clients = new ObservableCollection<ClientViewModel>();
            Orders = new ObservableCollection<OrderViewModel>();

            SelectClientCommand = new DelegateCommand(OnClientSelected);
        }

        public override async void OnDialogOpened(IDialogParameters parameters)
        {
            base.OnDialogOpened(parameters);

            await InitClients();
        }

        protected override async Task<bool> Save()
        {
            try
            {
                if (Orders.Count > 0)
                {
                    var groupOrders = Orders.GroupBy(x => x.Product.Category.Name);
                    foreach (var groupOrder in groupOrders)
                    {
                        var entity = Mapper.Map<ShipmentViewModel, Shipment>(Entity);
                        foreach (var order in groupOrder)
                        {
                            entity.Orders.Add(new ShipmentOrder
                            {
                                OrderId = order.Id,
                                ShipmentId = Entity.Id
                            });
                        }

                        entity = await PostEntity(entity);
                    }
                }
            }
            catch (Exception e)
            {
                return false;
            }

            IsNewEntity = false;

            return true;
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

        private async void OnClientSelected()
        {
            var client = Mapper.Map<ClientViewModel, Model.Entities.Client>(SelectedClient);

            try
            {
                var url = $"{purchasesEndpoint}/Client/{SelectedClient.Id}";
                var request = RestClient.GetRequest(url);
                var orders = await request.ExecuteAsync<List<Order>>();

                Orders.Clear();
                var entities = Mapper.Map<List<Order>, List<OrderViewModel>>(orders);
                foreach (var entity in entities)
                {
                    Orders.Add(entity);
                }
            }
            catch (Exception e)
            {
                Orders.Clear();
            }
        }
    }
}
