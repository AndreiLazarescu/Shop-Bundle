using Store.Client.ViewModel.Base;

namespace Store.Client.ViewModel.Entities
{
    public class OrderViewModel : EntityViewModelBase
    {
        private string sku;
        public string SKU
        {
            get => sku;
            set => SetProperty(ref sku, value);
        }

        private int productId;
        public int ProductId
        {
            get => productId;
            set => SetProperty(ref productId, value);
        }

        private ProductViewModel product;
        public ProductViewModel Product
        {
            get => product;
            set => SetProperty(ref product, value);
        }

        private int clientId;
        public int ClientId
        {
            get => clientId;
            set => SetProperty(ref clientId, value);
        }

        private ClientViewModel client;
        public ClientViewModel Client
        {
            get => client;
            set => SetProperty(ref client, value);
        }

        private int quantity;
        public int Quantity
        {
            get => quantity;
            set => SetProperty(ref quantity, value);
        }

        public override string ToString()
        {
            if (Product != null)
            {
                return $"SKU: {SKU}, Product: {Product.Name}, Quantity:{Quantity}";
            }

            return $"SKU: {SKU}, Quantity:{Quantity}";
        }
    }
}
