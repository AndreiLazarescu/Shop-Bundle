using AutoMapper;
using Prism.Events;
using Prism.Services.Dialogs;
using Store.Client.ViewModel.Base;
using Store.Client.ViewModel.Entities;
using Store.Common.Constants;
using Store.Interfaces.Communication;
using Store.Interfaces.Service;
using Store.Model.Entities;

namespace Store.Client.ViewModel
{
    public class ShipmentsViewModel : PageableShopItemsViewModelBase<ShipmentViewModel, Shipment>
    {
        protected override string Endpoint => Endpoints.Orders;
        private string orderEndpont => Endpoints.Purchases;

        public ShipmentsViewModel(IEventAggregator eventAggregator, IRestClient restClient, IConfigurationService configurationService, IMapper mapper, IDialogService dialogService)
            : base(eventAggregator, restClient, configurationService, mapper, dialogService)
        {
        }
    }
}
