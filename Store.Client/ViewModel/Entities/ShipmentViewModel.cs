using Store.Client.ViewModel.Base;
using Store.Model.Entities;
using System.Collections.Generic;

namespace Store.Client.ViewModel.Entities
{
    public class ShipmentViewModel : EntityViewModelBase
    {
        public ShipmentViewModel()
        {
        }

        private string firstName;
        public string FirstName
        {
            get => firstName;
            set => SetProperty(ref firstName, value);
        }

        private string lastName;
        public string LastName
        {
            get => lastName;
            set => SetProperty(ref lastName, value);
        }

        private string address;
        public string Address
        {
            get => address;
            set => SetProperty(ref address, value);
        }

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

        private ICollection<ShipmentOrder> orders;
        public ICollection<ShipmentOrder> Orders
        {
            get => orders;
            set => SetProperty(ref orders, value);
        }

        private ICollection<ShipmentProduct> products;
        public ICollection<ShipmentProduct> Products
        {
            get => products;
            set => SetProperty(ref products, value);
        }
    }
}
