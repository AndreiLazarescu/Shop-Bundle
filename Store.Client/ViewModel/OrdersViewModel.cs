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
    public class OrdersViewModel : PageableShopItemsViewModelBase<OrderViewModel, Order>
    {
        protected override string Endpoint => Endpoints.Purchases;

        public OrdersViewModel(IEventAggregator eventAggregator, IRestClient restClient, IConfigurationService configurationService, IMapper mapper, IDialogService dialogService)
            : base(eventAggregator, restClient, configurationService, mapper, dialogService)
        {

        }
    }
}
