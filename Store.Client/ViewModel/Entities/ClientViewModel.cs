using Store.Client.ViewModel.Base;

namespace Store.Client.ViewModel.Entities
{
    public class ClientViewModel : EntityViewModelBase
    {
        public string FullName
        {
            get => $"{FirstName} {LastName}";
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

        private int addressId;
        public int AddressId
        {
            get => addressId;
            set => SetProperty(ref addressId, value);
        }

        private AddressViewModel address;
        public AddressViewModel Address
        {
            get => address;
            set => SetProperty(ref address, value);
        }

        public override string ToString()
        {
            if (Address != null && Address.City != null && Address.City.State != null && Address.City.State.Country != null)
            {
                return $"{FullName},\n{Address.Street}, {Address.City.Name}\n{Address.City.State.Name}, {Address.City.State.Country.Name}";
            }

            return FullName;
        }
    }
}
