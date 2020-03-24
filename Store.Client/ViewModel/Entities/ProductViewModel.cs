using Store.Client.ViewModel.Base;

namespace Store.Client.ViewModel.Entities
{
    public class ProductViewModel : EntityViewModelBase
    {
        private string name;
        public string Name
        {
            get => name;
            set => SetProperty(ref name, value);
        }

        private string description;
        public string Description
        {
            get => description;
            set => SetProperty(ref description, value);
        }

        private int categoryId;
        public int CategoryId
        {
            get => categoryId;
            set => SetProperty(ref categoryId, value);
        }

        private ProductCategoryViewModel category;
        public ProductCategoryViewModel Category
        {
            get => category;
            set => SetProperty(ref category, value);
        }

        public override string ToString()
        {
            if (Category != null)
            {
                return $"{Name}, {Category.Name}";
            }

            return Name;
        }
    }
}
