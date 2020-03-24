using Prism.Mvvm;

namespace Store.Client.ViewModel.Base
{
    public abstract class EntityViewModelBase : BindableBase
    {
        private int id;
        public int Id
        {
            get => id;
            set => SetProperty(ref id, value);
        }
    }
}
