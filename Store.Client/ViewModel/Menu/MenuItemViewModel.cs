using FontAwesome.WPF;
using Prism.Commands;
using Prism.Events;
using Prism.Mvvm;
using Store.Client.Events;
using System;
using System.Windows.Input;
using System.Windows.Media;

namespace Store.Client.ViewModel.Menu
{
    public class MenuItemViewModel : BindableBase
    {
        private string label;
        public string Label
        {
            get => label;
            set => SetProperty(ref label, value);
        }

        private FontAwesomeIcon icon;
        public FontAwesomeIcon Icon
        {
            get => icon;
            set => SetProperty(ref icon, value);
        }

        private bool isMenuOpen;
        public bool IsMenuOpen
        {
            get => isMenuOpen;
            set => SetProperty(ref isMenuOpen, value);
        }

        public ICommand ItemClickCommand { get;}

        protected readonly Type targetType;
        protected readonly IEventAggregator eventAggregator;

        public MenuItemViewModel(IEventAggregator eventAggregator, Type targetType)
        {
            this.targetType = targetType;
            this.eventAggregator = eventAggregator;

            ItemClickCommand = new DelegateCommand(OnItemClick);
        }

        protected virtual void OnItemClick()
        {
            eventAggregator.GetEvent<NavItemClickEvent>().Publish(targetType.Name);
        }
    }
}
