using Store.Client.ViewModel.Base;

namespace Store.Client.ViewModel.Entities
{
    public class CityViewModel : EntityViewModelBase
    {
        private string name;
        public string Name
        {
            get => name;
            set => SetProperty(ref name, value);
        }

        private int stateId;
        public int StateId
        {
            get => stateId;
            set => SetProperty(ref stateId, value);
        }

        private StateViewModel state;
        public StateViewModel State
        {
            get => state;
            set => SetProperty(ref state, value);
        }
    }
}
