using AutoMapper;
using Prism.Commands;
using Prism.Events;
using Prism.Services.Dialogs;
using Store.Interfaces.Communication;
using Store.Model.Entities;
using System;
using System.Windows.Input;

namespace Store.Client.ViewModel.Base
{
    public abstract class ItemDialogViewModelBase<TEntity, TEntityViewModel> : ShopItemWindowViewModelBase<TEntity, TEntityViewModel>, IDialogAware
                where TEntity : EntityBase
        where TEntityViewModel : EntityViewModelBase, new()
    {
        public event Action<IDialogResult> RequestClose;

        public ICommand SaveCommand { get; set; }
        public ICommand CancelCommand { get; set; }

        public ItemDialogViewModelBase(IMapper mapper, IEventAggregator eventAggregator, IRestClient restClient, IDialogService dialogService)
            : base(mapper, eventAggregator, restClient, dialogService)
        {
            SaveCommand = new DelegateCommand(OnSave);
            CancelCommand = new DelegateCommand(OnCancel);
        }

        public virtual bool CanCloseDialog()
        {
            return true;
        }

        public virtual void OnDialogClosed()
        {
        }

        public virtual void OnDialogOpened(IDialogParameters parameters)
        {
            if (parameters.ContainsKey("Entity"))
            {
                Initilize(parameters.GetValue<TEntity>("Entity"));
            }
            else
            {
                Initilize();
            }
        }

        private async void OnSave()
        {
            var result = await Save();
            if (result)
            {
                var parameters = new DialogParameters
                {
                    { "Entity", Entity }
                };

                RequestClose(new DialogResult(ButtonResult.OK, parameters));
            }
        }

        private void OnCancel()
        {
            RequestClose(new DialogResult(ButtonResult.Cancel));
        }
    }
}
