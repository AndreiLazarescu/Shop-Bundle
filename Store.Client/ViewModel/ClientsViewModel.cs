using AutoMapper;
using Prism.Events;
using Prism.Services.Dialogs;
using Store.Client.ViewModel.Base;
using Store.Client.ViewModel.Entities;
using Store.Common.Constants;
using Store.Interfaces.Communication;
using Store.Interfaces.Service;

namespace Store.Client.ViewModel
{
    public class ClientsViewModel : PageableShopItemsViewModelBase<ClientViewModel, Model.Entities.Client>
    {
        protected override string Endpoint => Endpoints.Clients;

        public ClientsViewModel(IEventAggregator eventAggregator, IRestClient restClient, IConfigurationService configurationService, IMapper mapper, IDialogService dialogService)
            : base(eventAggregator, restClient, configurationService, mapper, dialogService)
        {

        }
    }
}
