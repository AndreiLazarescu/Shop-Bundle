using AutoMapper;
using Prism.Commands;
using Prism.Events;
using Prism.Services.Dialogs;
using Store.Client.ViewModel.Base;
using Store.Client.ViewModel.Entities;
using Store.Interfaces.Communication;
using Store.Model.Entities;
using System;
using System.Windows.Input;

namespace Store.Client.ViewModel
{
    /// <summary>
    /// The client view model.
    /// </summary>
    public class ClientWindowViewModel : ItemDialogViewModelBase<Model.Entities.Client, ClientViewModel>
    {
        protected override string Endpoint => "clients";

        public ICommand AddAddressCommand { get; set; }


        public ClientWindowViewModel(IMapper mapper, IEventAggregator eventAggregator, IRestClient restClient, IDialogService dialogService)
            : base(mapper, eventAggregator, restClient, dialogService)
        {

            AddAddressCommand = new DelegateCommand(OnAddAddress);
        }

        private void OnAddAddress()
        {
            DialogService.ShowDialog(nameof(Address), new DialogParameters(), OnAddressWindowClosed);
        }

        private void OnAddressWindowClosed(IDialogResult result)
        {
            if (result.Result == ButtonResult.OK)
            {
                var addressViewModel = result.Parameters.GetValue<AddressViewModel>("Entity");

                Entity.Address = addressViewModel;
                Entity.AddressId = addressViewModel.Id;
            }
        }
    }
}
