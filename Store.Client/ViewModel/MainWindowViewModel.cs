using FontAwesome.WPF;
using Prism.Commands;
using Prism.Events;
using Prism.Mvvm;
using Prism.Regions;
using Prism.Services.Dialogs;
using Store.Client.Events;
using Store.Client.View;
using Store.Client.ViewModel.Menu;
using Store.Common.Constants;
using System.Collections.ObjectModel;
using System.Windows.Input;

namespace Store.Client.ViewModel
{
    public class MainWindowViewModel : BindableBase
    {
        private bool isMenuOpen;
        public bool IsMenuOpen
        {
            get => isMenuOpen;
            set => SetProperty(ref isMenuOpen, value);
        }

        private double menuWidth = 104;
        public double MenuWidth
        {
            get => menuWidth;
            set => SetProperty(ref menuWidth, value);
        }

        public ObservableCollection<MenuItemViewModel> MainMenuItems { get; set; }

        public ICommand MenuCommand { get; private set; }
        public ICommand ShowLogCommand { get; private set; }

        private readonly IRegionManager regionManager;
        private readonly IEventAggregator eventAggregator;
        private readonly IDialogService dialogService;

        public MainWindowViewModel(IRegionManager regionManager, IEventAggregator eventAggregator, IDialogService dialogService)
        {
            this.regionManager = regionManager;
            this.eventAggregator = eventAggregator;
            this.dialogService = dialogService;

            Initilize();
        }

        public void Initilize()
        {
            eventAggregator.GetEvent<NavItemClickEvent>().Subscribe(OnMenuItemClick);

            ShowLogCommand = new DelegateCommand(OnShowLog);
            MenuCommand = new DelegateCommand(OnMenu);
            MainMenuItems = new ObservableCollection<MenuItemViewModel>
            {
                new MenuItemViewModel(eventAggregator, typeof(HomeView))
                {
                    Label = "Home",
                    Icon = FontAwesomeIcon.Home

                },
                new MenuItemViewModel(eventAggregator, typeof(OrdersView))
                {
                    Label = "Orders",
                    Icon = FontAwesomeIcon.ShoppingBag
                },
                new MenuItemViewModel(eventAggregator, typeof(ProductsView))
                {
                    Label = "Products",
                    Icon = FontAwesomeIcon.Car
                },
                new MenuItemViewModel(eventAggregator, typeof(ClientsView))
                {
                    Label = "Clients",
                    Icon = FontAwesomeIcon.Users
                },
                new MenuItemViewModel(eventAggregator, typeof(ShipmentsView))
                {
                    Label = "Shipments",
                    Icon = FontAwesomeIcon.Truck
                }
            };
        }

        private void OnShowLog()
        {
            dialogService.Show(nameof(LogWindowView), new DialogParameters(), (IDialogResult result) => { });
        }

        private void OnMenu()
        {
            IsMenuOpen = !IsMenuOpen;
            if (IsMenuOpen)
            {
                MenuWidth = 225;
            }
            else
            {
                MenuWidth = 104;
            }

            foreach (var item in MainMenuItems)
            {
                item.IsMenuOpen = IsMenuOpen;
            }
        }

        private void OnMenuItemClick(string targetName)
        {
            IsMenuOpen = false;

            regionManager.RequestNavigate(RegionNames.Content, targetName);
        }
    }
}
