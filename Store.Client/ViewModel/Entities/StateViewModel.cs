using Store.Client.ViewModel.Base;

namespace Store.Client.ViewModel.Entities
{
    public class StateViewModel : EntityViewModelBase
    {
        private string name;
        public string Name
        {
            get => name;
            set => SetProperty(ref name, value);
        }

        private int countryId;
        public int CountryId
        {
            get => countryId;
            set => SetProperty(ref countryId, value);
        }

        private CountryViewModel country;
        public CountryViewModel Country
        {
            get => country;
            set => SetProperty(ref country, value);
        }
    }
}
