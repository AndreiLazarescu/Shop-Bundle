using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Windows;
using System.Windows.Media;
using AutoMapper;
using FontAwesome.WPF;
using Prism.Events;
using Prism.Services.Dialogs;
using Store.Client.Events;
using Store.Client.ViewModel.Base;
using Store.Client.ViewModel.Entities;
using Store.Client.ViewModel.Menu;
using Store.Common.Constants;
using Store.Interfaces.Communication;
using Store.Interfaces.Service;
using Store.Model.Entities;

namespace Store.Client.ViewModel
{
    public class HomeViewModel : ShopItemsViewModelBase<ProductViewModel, Product>
    {
        public ObservableCollection<ActionItemViewModel> ActionItems { get; set; }
        public ObservableCollection<ActionItemViewModel> ExtrasItems { get; set; }

        protected override string Endpoint => Endpoints.Products;

        private Color[] colors;

        public HomeViewModel(IEventAggregator eventAggregator, IRestClient restClient, IConfigurationService configurationService, IMapper mapper, IDialogService dialogService)
            : base(eventAggregator, restClient, configurationService, mapper, dialogService)
        {
        }

        protected override void Initilize()
        {
            base.Initilize();

            colors = new Color[]
            {
                Color.FromArgb(255,  78, 115, 223),
                Color.FromArgb(255, 102,  16, 242),
                Color.FromArgb(255, 111,  66, 193),
                Color.FromArgb(255, 232,  62, 140),
                Color.FromArgb(255, 231,  74,  59),
                Color.FromArgb(255, 253, 126,  20),
                Color.FromArgb(255, 246, 194,  62),
                Color.FromArgb(255,  28, 200, 138),
                Color.FromArgb(255,  32, 201, 166),
                Color.FromArgb(255,  54, 185, 204)
            };

            ExtrasItems = new ObservableCollection<ActionItemViewModel>()
            {
                new ActionItemViewModel(EventAggregator, typeof(object))
                {
                    Label = "Extra 1",
                    Icon = FontAwesomeIcon.Map,
                    BackgroundBrush = GetRandomBrush(),
                    TitleBrush = GetRandomBrush()
                },
                new ActionItemViewModel(EventAggregator, typeof(object))
                {
                    Label = "Extra 2",
                    Icon = FontAwesomeIcon.Money,
                    BackgroundBrush = GetRandomBrush(),
                    TitleBrush = GetRandomBrush()
                },
                new ActionItemViewModel(EventAggregator, typeof(object))
                {
                    Label = "Extra 3",
                    Icon = FontAwesomeIcon.Calendar,
                    BackgroundBrush = GetRandomBrush(),
                    TitleBrush = GetRandomBrush()
                },
                new ActionItemViewModel(EventAggregator, typeof(object))
                {
                    Label = "Extra 4",
                    Icon = FontAwesomeIcon.Key,
                    BackgroundBrush = GetRandomBrush(),
                    TitleBrush = GetRandomBrush()
                }
            };
            ActionItems = new ObservableCollection<ActionItemViewModel>
            {
                new ActionItemViewModel(EventAggregator, typeof(object))
                {
                    Label = "ORDERS",
                    Icon = FontAwesomeIcon.ShoppingBag,
                    Description = "New order",
                    BackgroundBrush = GetRandomBrush(),
                    TitleBrush = GetRandomBrush()
                },
                new ActionItemViewModel(EventAggregator, typeof(object))
                {
                    Label = "PRODUCTS",
                    Icon = FontAwesomeIcon.Car,
                    Description = "New product",
                    BackgroundBrush = GetRandomBrush(),
                    TitleBrush = GetRandomBrush()
                },
                new ActionItemViewModel(EventAggregator, typeof(object))
                {
                    Label = "CLIENTS",
                    Icon = FontAwesomeIcon.Users,
                    Description = "New client",
                    BackgroundBrush = GetRandomBrush(),
                    TitleBrush = GetRandomBrush()
                },
                new ActionItemViewModel(EventAggregator, typeof(object))
                {
                    Label = "SHIPMENTS",
                    Icon = FontAwesomeIcon.Truck,
                    Description = "New shipment",
                    BackgroundBrush = GetRandomBrush(),
                    TitleBrush = GetRandomBrush()
                }
            };
        }

        private Random generator = new Random(DateTime.Now.Millisecond);
        private SolidColorBrush GetRandomBrush()
        {
            var randomNumber = generator.Next(0, 10);

            Console.WriteLine(randomNumber);

            return new SolidColorBrush(colors[randomNumber]);
        }

        protected override void ProcessGetResponse(object response)
        {
            if (response is List<Product> content)
            {
                var data = Mapper.Map<List<Product>, List<ProductViewModel>>(content);
                if (data.Count > 0)
                {
                    var orderedData = data.OrderByDescending(x => x.Id);
                    var itemCount = data.Count > 6 ? 6 : data.Count;

                    Application.Current.Dispatcher.Invoke(() =>
                    {
                        Items.Clear();
                        var i = 0;
                        foreach (var item in orderedData)
                        {
                            if (i >= itemCount)
                            {
                                break;
                            }

                            Items.Add(item);
                            i++;
                        }
                    });
                }
            }

            EventAggregator.GetEvent<LoadingEvent>().Publish(false);
        }
    }
}
