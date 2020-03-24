using Prism.Events;
using Prism.Mvvm;
using Store.Client.Events;

namespace Store.Client.ViewModel
{
    public class BusyViewModel : BindableBase
    {
        private bool isActive;
        public bool IsActive
        {
            get => isActive;
            set => SetProperty(ref isActive, value);
        }

        public BusyViewModel(IEventAggregator eventAggregator)
        {
            eventAggregator.GetEvent<LoadingEvent>().Subscribe((bool isLoading) => IsActive = isLoading);
        }
    }
}
