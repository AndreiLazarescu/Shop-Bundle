using Store.Common.Constants;
using Store.Interfaces.Communication;
using Store.Model.Entities;
using Store.Web.Controllers.Base;
using Store.Web.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;

namespace Store.Web.Controllers.Web
{
    /// <summary>
    /// The clients controller.
    /// </summary>
    public class ClientsController : ShopItemsControllerBase<Client>
    {
        protected override string Endpoint => Endpoints.Clients;

        public ClientsController(IRestClient restClient)
            : base(restClient)
        {
        }

        [HttpPost]
        public async Task<ActionResult> Index(CreateClientModel viewModel)
        {
            var client = new Client
            {
                FirstName = viewModel.FirstName,
                LastName = viewModel.LastName,
                Address = new Address
                {
                    Street = viewModel.Street,
                    City = new City
                    {
                        Name = viewModel.City,
                        State = new State
                        {
                            Name = viewModel.State,
                            Country = new Country
                            {
                                Name = viewModel.Country
                            }
                        }
                    }
                }
            };

            await PostEntity(client);

            return await Index();
        }

        public override async Task<ActionResult> Create()
        {
            return await Task.Run(() => View(new CreateClientModel()));
        }
    }
}
