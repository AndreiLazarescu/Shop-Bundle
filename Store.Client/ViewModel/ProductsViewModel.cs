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
    public class ProductsViewModel : PageableShopItemsViewModelBase<ProductViewModel, Product>
    {
        protected override string Endpoint => Endpoints.Products;

        public ProductsViewModel(IEventAggregator eventAggregator, IRestClient restClient, IConfigurationService configurationService, IMapper mapper, IDialogService dialogService)
            : base(eventAggregator, restClient, configurationService, mapper, dialogService)
        {
        }
    }
}
