using System.Threading.Tasks;
using AutoMapper;
using Prism.Events;
using Prism.Services.Dialogs;
using Store.Client.Events;
using Store.Client.ViewModel.Base;
using Store.Client.ViewModel.Entities;
using Store.Interfaces.Communication;
using Store.Model.Entities;

namespace Store.Client.ViewModel
{
    /// <summary>
    /// The address window view model.
    /// </summary>
    public class AddressWindowViewModel : ItemDialogViewModelBase<Address, AddressViewModel>
    {
        protected override string Endpoint => "addresses";

        private string city;
        public string City
        {
            get => city;
            set => SetProperty(ref city, value);
        }

        private string state;
        public string State
        {
            get => state;
            set => SetProperty(ref state, value);
        }

        private string country;
        public string Country
        {
            get => country;
            set => SetProperty(ref country, value);
        }

        public AddressWindowViewModel(IMapper mapper, IEventAggregator eventAggregator, IRestClient restClient, IDialogService dialogService)
            : base(mapper, eventAggregator, restClient, dialogService)
        { }

        protected override Task<bool> Save()
        {
            // Fast and dirty
            Entity.City = new CityViewModel { Name = City };
            Entity.City.State = new StateViewModel { Name = State };
            Entity.City.State.Country = new CountryViewModel { Name = Country };

            return base.Save();
        }
    }
}
