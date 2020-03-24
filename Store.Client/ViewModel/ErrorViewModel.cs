using Prism.Commands;
using Prism.Events;
using Store.Client.Events;
using Store.Client.ViewModel.Base;
using System;
using System.Windows.Input;

namespace Store.Client.ViewModel
{
    public class ErrorViewModel : ViewModelBase
    {
        private string errorMessage;
        public string ErrorMessage
        {
            get => errorMessage;
            set => SetProperty(ref errorMessage, value);
        }

        private bool isActive;
        public bool IsActive
        {
            get => isActive;
            set => SetProperty(ref isActive, value);
        }

        public ICommand OkCommand { get; private set; }

        public ErrorViewModel(IEventAggregator eventAggregator)
        {
            eventAggregator.GetEvent<ErrorMessageEvent>().Subscribe(OnErrorMessage);

            OkCommand = new DelegateCommand(() => IsActive = false);
        }

        private void OnErrorMessage(Exception exception)
        {
            ErrorMessage = $"{exception}\n\n INNER:{exception.InnerException.Message}";

            IsActive = true;
        }
    }
}
