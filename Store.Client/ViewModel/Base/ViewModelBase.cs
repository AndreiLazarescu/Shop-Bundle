using Prism.Mvvm;
using Store.Interfaces.ViewModel;

namespace Store.Client.ViewModel.Base
{
    public abstract class ViewModelBase : BindableBase, IViewModel
    {
        public string Name
        {
            get => Title;
        }

        private string title;
        public string Title
        {
            get => title;
            set
            {
                if(SetProperty(ref title, value))
                {
                    RaisePropertyChanged(Name);
                }
            }
        }

        protected ViewModelBase()
        {
            Title = GetType().Name.Replace("ViewModel", string.Empty);
        }
    }
}
