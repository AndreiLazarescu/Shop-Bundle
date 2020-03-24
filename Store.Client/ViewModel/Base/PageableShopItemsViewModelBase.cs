using AutoMapper;
using Prism.Commands;
using Prism.Events;
using Prism.Services.Dialogs;
using Store.Client.Events;
using Store.Interfaces.Communication;
using Store.Interfaces.Service;
using Store.Model.Entities;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Windows.Input;

namespace Store.Client.ViewModel.Base
{
    /// <summary>
    /// The pageable shop items view model base.
    /// </summary>
    public abstract class PageableShopItemsViewModelBase<TEntityViewModel, TEntity> : ShopItemsViewModelBase<TEntityViewModel, TEntity>
        where TEntityViewModel : EntityViewModelBase
        where TEntity : EntityBase
    {
        public ObservableCollection<int> PageCountValues { get; set; } = new ObservableCollection<int>
        {
            5,10,15,20,50,100
        };

        public ICommand NextCommand { get; protected set; }
        public ICommand PrevCommand { get; protected set; }

        private int currentPage = 1;
        public int CurrentPage
        {
            get => currentPage;
            set => SetProperty(ref currentPage, value);
        }

        private int pageCount = 10;
        public int PageCount
        {
            get => pageCount;
            set
            {
                if (SetProperty(ref pageCount, value))
                {
                    Load();
                }
            }

        }

        protected PageableShopItemsViewModelBase(IEventAggregator eventAggregator, IRestClient restClient, IConfigurationService configurationService, IMapper mapper, IDialogService dialogService)
            : base(eventAggregator, restClient, configurationService, mapper, dialogService)
        {

        }

        protected override void Initilize()
        {
            base.Initilize();

            NextCommand = new DelegateCommand(() => 
            {
                CurrentPage++;
                Load();
            });

            PrevCommand = new DelegateCommand(() =>
            {
                if (CurrentPage > 1)
                {
                    CurrentPage--;
                }

                Load();
            });
        }

        protected override async void Load()
        {
            EventAggregator.GetEvent<LoadingEvent>().Publish(true);

            try
            {
                var url = $"{Endpoint}/page/{(currentPage - 1) * pageCount}/{pageCount}";
                var request = RestClient.GetRequest(url);
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
    }
}
