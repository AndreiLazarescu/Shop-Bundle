using System;
using System.Text;
using Prism.Events;
using Prism.Services.Dialogs;
using Store.Client.Events;
using Store.Client.ViewModel.Base;
using Tiny.RestClient;

namespace Store.Client.ViewModel
{
    public class LogWindowViewModel : ViewModelBase, IDialogAware
    {
        private string messsages = string.Empty;
        public string Messages
        {
            get => messsages;
            set => SetProperty(ref messsages, value);
        }

        private readonly IEventAggregator eventAggregator;

        private StringBuilder builder;

        public event Action<IDialogResult> RequestClose;

        public LogWindowViewModel(IEventAggregator eventAggregator)
        {
            this.eventAggregator = eventAggregator;

            builder = new StringBuilder();

            eventAggregator.GetEvent<PrintLogEvent>().Subscribe(OnPrintLog);
        }

        private void OnPrintLog(string message)
        {
            if (builder != null)
            {
                if (builder.Length > 10000)
                {
                    builder = new StringBuilder();
                }

                builder.AppendLine();
                builder.AppendLine(message);

                Messages = builder.ToString();
            }
        }

        public bool CanCloseDialog()
        {
            return true;
        }

        public void OnDialogClosed()
        {
            builder = null;
            Messages = string.Empty;
        }

        public void OnDialogOpened(IDialogParameters parameters)
        {
            
        }
    }
}
