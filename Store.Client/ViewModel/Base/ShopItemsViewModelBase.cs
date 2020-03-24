using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Windows;
using System.Windows.Input;
using AutoMapper;
using Prism.Commands;
using Prism.Events;
using Prism.Regions;
using Prism.Services.Dialogs;
using Store.Client.Events;
using Store.Interfaces.Communication;
using Store.Interfaces.Service;
using Store.Model.Entities;

namespace Store.Client.ViewModel.Base
{
    public abstract class ShopItemsViewModelBase<TEntityViewModel, TEntity> : NavigationAwareViewModelBase
        where TEntityViewModel :  EntityViewModelBase
        where TEntity : EntityBase
    {
        public ObservableCollection<TEntityViewModel> Items { get; set; }

        private TEntityViewModel selectedItem;
        public TEntityViewModel SelectedItem
        {
            get => selectedItem;
            set
            {
                if(SetProperty(ref selectedItem, value))
                {
                    IsItemSelected = selectedItem != null;
                }
            }
        }

        private bool isItemSelected;
        public bool IsItemSelected
        {
            get => isItemSelected;
            set => SetProperty(ref isItemSelected, value);
        }

        public ICommand NewCommand { get; private set;  }
        public ICommand DeleteCommand { get; private set;  }

        protected abstract string Endpoint { get; }

        protected readonly IEventAggregator EventAggregator;
        protected readonly IRestClient RestClient;
        protected readonly IConfigurationService ConfigurationService;
        protected readonly IMapper Mapper;
        protected readonly IDialogService DialogService;

        protected ShopItemsViewModelBase(IEventAggregator eventAggregator, IRestClient restClient, IConfigurationService configurationService, IMapper mapper, IDialogService dialogService)
        {
            EventAggregator = eventAggregator;
            RestClient = restClient;
            ConfigurationService = configurationService;
            Mapper = mapper;
            DialogService = dialogService;

            Initilize();
        }


        public override void OnNavigatedTo(NavigationContext navigationContext)
        {
            base.OnNavigatedTo(navigationContext);

            Load();
        }

        public override void OnNavigatedFrom(NavigationContext navigationContext)
        {
            base.OnNavigatedFrom(navigationContext);

            EventAggregator.GetEvent<LoadingEvent>().Publish(false);
        }

        protected virtual void Initilize()
        {
            Items = new ObservableCollection<TEntityViewModel>();

            NewCommand = new DelegateCommand(OnNewItem);
            DeleteCommand = new DelegateCommand(OnDeleteItem).ObservesProperty(() => IsItemSelected);
        }

        protected virtual async void Load()
        {
            EventAggregator.GetEvent<LoadingEvent>().Publish(true);

            try
            {
                var request = RestClient.GetRequest(Endpoint);
                var response = await request.ExecuteAsync<List<TEntity>>();

                ProcessGetResponse(response);
            }
            catch (Exception e)
            {
                EventAggregator.GetEvent<ErrorMessageEvent>().Publish(e);
            }
            finally
            {
                EventAggregator.GetEvent<LoadingEvent>().Publish(false);
            }
        }

        protected virtual void ProcessGetResponse(object response)
        {
            if (response is List<TEntity> content)
            {
                var data = Mapper.Map<List<TEntity>, List<TEntityViewModel>>(content);
                if (data.Count > 0)
                {
                    Application.Current.Dispatcher.Invoke(() =>
                    {
                        Items.Clear();
                        for (int i = 0; i < data.Count; i++)
                        {
                            Items.Add(data[i]);
                        }
                    });
                }
            }
        }

        protected virtual void ProcessPostResponse(object response)
        {
            Load();
        }

        protected virtual void ProcessDeleteResponse(object response)
        {
            Load();
        }

        private void OnNewItem()
        {
            DialogService.ShowDialog(typeof(TEntity).Name, new DialogParameters(), (IDialogResult result) => 
            {
                if (result.Result == ButtonResult.OK)
                {
                    Load();
                }
            });
        }

        private async void OnDeleteItem()
        {
            EventAggregator.GetEvent<LoadingEvent>().Publish(true);

            try
            {
                var url = $"{Endpoint}/{SelectedItem.Id}";
                var response = await RestClient.DeleteRequest(url).ExecuteAsync<TEntity>();

                ProcessDeleteResponse(response);
            }
            catch (Exception e)
            {
                EventAggregator.GetEvent<ErrorMessageEvent>().Publish(e);
            }
            finally
            {
                EventAggregator.GetEvent<LoadingEvent>().Publish(false);
            }
        }
    }
}
