using Store.Client.ViewModel.Base;

namespace Store.Client.ViewModel.Entities
{
    public class ProductCategoryViewModel : EntityViewModelBase
    {
        private string name;
        public string Name
        {
            get => name;
            set => SetProperty(ref name, value);
        }
    }
}
