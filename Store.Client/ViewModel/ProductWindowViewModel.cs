using AutoMapper;
using Prism.Events;
using Prism.Services.Dialogs;
using Store.Client.ViewModel.Base;
using Store.Client.ViewModel.Entities;
using Store.Common.Constants;
using Store.Interfaces.Communication;
using Store.Model.Entities;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Threading.Tasks;

namespace Store.Client.ViewModel
{
    public class ProductWindowViewModel : ItemDialogViewModelBase<Product, ProductViewModel>
    {
        public ObservableCollection<ProductCategoryViewModel> Categories { get; set; }
        private ProductCategoryViewModel selectedCategory;
        public ProductCategoryViewModel SelectedCategory
        {
            get => selectedCategory;
            set => SetProperty(ref selectedCategory, value);
        }

        protected override string Endpoint => Endpoints.Products;

        public ProductWindowViewModel(IMapper mapper, IEventAggregator eventAggregator, IRestClient restClient, IDialogService dialogService)
            : base(mapper, eventAggregator, restClient, dialogService)
        {
            Categories = new ObservableCollection<ProductCategoryViewModel>();
        }

        public override async void OnDialogOpened(IDialogParameters parameters)
        {
            base.OnDialogOpened(parameters);

            await InitCategories();
        }

        private async Task InitCategories()
        {
            var categories = await RestClient.GetRequest(Endpoints.ProductCategory).ExecuteAsync<List<ProductCategory>>();
            var entities = Mapper.Map<List<ProductCategory>, List<ProductCategoryViewModel>>(categories);

            foreach (var entity in entities)
            {
                Categories.Add(entity);
            }
        }

        protected override Task<bool> Save()
        {
            if (SelectedCategory != null)
            {
                Entity.Category = SelectedCategory;
                Entity.CategoryId = SelectedCategory.Id;
            }

            return base.Save();
        }

    }
}
