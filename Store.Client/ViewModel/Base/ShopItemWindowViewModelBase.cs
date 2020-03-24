using AutoMapper;
using Prism.Events;
using Prism.Services.Dialogs;
using Store.Interfaces.Communication;
using Store.Interfaces.ViewModel;
using Store.Model.Entities;
using System;
using System.Threading.Tasks;

namespace Store.Client.ViewModel.Base
{
    public abstract class ShopItemWindowViewModelBase<TEntity, TEntityViewModel> : ViewModelBase, IEntityWindowViewModelBase<TEntity>
        where TEntity : EntityBase
        where TEntityViewModel : EntityViewModelBase, new()
    {
        private TEntityViewModel entity;
        public TEntityViewModel Entity
        {
            get => entity;
            set => SetProperty(ref entity, value);
        }

        protected bool IsNewEntity;

        protected abstract string Endpoint { get; }

        protected readonly IMapper Mapper;
        protected readonly IRestClient RestClient;
        protected readonly IEventAggregator EventAggregator;
        protected readonly IDialogService DialogService;

        public ShopItemWindowViewModelBase(IMapper mapper, IEventAggregator eventAggregator, IRestClient restClient, IDialogService dialogService)
        {
            Mapper = mapper;
            RestClient = restClient;
            EventAggregator = eventAggregator;
            DialogService = dialogService;
        }

        public void Initilize()
        {
            IsNewEntity = true;

            Entity = new TEntityViewModel();
        }

        public void Initilize(TEntity entity)
        {
            Entity = Mapper.Map<TEntity, TEntityViewModel>(entity);
        }

        public T MapFrom<T>() where T : class
        {
            return Mapper.Map<TEntityViewModel, T>(Entity);
        }

        protected virtual async Task<bool> Save()
        {
            try
            {
                var entity = Mapper.Map<TEntityViewModel, TEntity>(Entity);

                if (IsNewEntity)
                {
                    entity = await PostEntity(entity);
                }
                else
                {
                    entity = await PutEntity(entity);
                }

                Entity = Mapper.Map<TEntity, TEntityViewModel>(entity);
            }
            catch (Exception e)
            {
                return false;
            }

            IsNewEntity = false;

            return true;
        }

        protected Task<TEntity> PostEntity(TEntity entity)
        {
            var request = RestClient.PostRequest(Endpoint).AddContent(entity);

            return request.ExecuteAsync<TEntity>();
        }

        protected Task<TEntity> PutEntity(TEntity entity)
        {
            var request = RestClient.PutRequest(Endpoint).AddContent(entity);

            return request.ExecuteAsync<TEntity>();
        }
    }
}
