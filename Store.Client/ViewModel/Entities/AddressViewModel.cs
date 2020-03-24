using Store.Client.ViewModel.Base;

namespace Store.Client.ViewModel.Entities
{
    public class AddressViewModel : EntityViewModelBase
    {
        private string street;
        public string Street
        {
            get => street;
            set => SetProperty(ref street, value);
        }

        private int cityId;
        public int CityId
        {
            get => cityId;
            set => SetProperty(ref cityId, value);
        }

        private CityViewModel city;
        public CityViewModel City
        {
            get => city;
            set => SetProperty(ref city, value);
        }

        public override string ToString()
        {
            if (City != null && City.State != null && City.State.Country != null)
            {
                return $"{Street}, {City.Name}\n{City.State.Name}, {City.State.Country.Name}";
            }

            return base.ToString();
        }
    }
}
