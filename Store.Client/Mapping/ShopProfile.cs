using AutoMapper;
using Store.Client.ViewModel.Entities;
using Store.Model.Entities;

namespace Store.Client.Mapping
{
    public class ShopProfile : Profile
    {
        public ShopProfile()
        {
            CreateMap<Model.Entities.Client, ClientViewModel>();
            CreateMap<ClientViewModel, Model.Entities.Client>();

            CreateMap<Product, ProductViewModel>();
            CreateMap<ProductViewModel, Product>();

            CreateMap<ProductCategory, ProductCategoryViewModel>();
            CreateMap<ProductCategoryViewModel, ProductCategory>();

            CreateMap<Order, OrderViewModel>();
            CreateMap<OrderViewModel, Order>();

            CreateMap<Shipment, ShipmentViewModel>();
            CreateMap<ShipmentViewModel, Shipment>();

            CreateMap<Address, AddressViewModel>();
            CreateMap<AddressViewModel, Address>();

            CreateMap<City, CityViewModel>();
            CreateMap<CityViewModel, City>();

            CreateMap<State, StateViewModel>();
            CreateMap<StateViewModel, State>();

            CreateMap<Country, CountryViewModel>();
            CreateMap<CountryViewModel, Country>();
        }

    }
}
