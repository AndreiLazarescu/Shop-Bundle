// --------------------------------------------------------------------------------------------------------------------
// <copyright file="IoC.cs" company="Web Advanced">
// Copyright 2012 Web Advanced (www.webadvanced.com)
// Licensed under the Apache License, Version 2.0 (the "License");
// you may not use this file except in compliance with the License.
// You may obtain a copy of the License at
//
// http://www.apache.org/licenses/LICENSE-2.0

// Unless required by applicable law or agreed to in writing, software
// distributed under the License is distributed on an "AS IS" BASIS,
// WITHOUT WARRANTIES OR CONDITIONS OF ANY KIND, either express or implied.
// See the License for the specific language governing permissions and
// limitations under the License.
// </copyright>
// --------------------------------------------------------------------------------------------------------------------


namespace Store.Web.DependencyResolution
{
    using Store.Common.Communication;
    using Store.Common.Constants;
    using Store.Interfaces.Communication;
    using Store.Interfaces.Service;
    using StructureMap;
    using System.Net.Http;

    public static class IoC
    {
        public static IContainer Initialize()
        {
            var container = new Container(c => c.AddRegistry<DefaultRegistry>());

            RegisterRestClient(container);

            return container;
        }

        private static void RegisterRestClient(IContainer container)
        {
            var configurationService = container.GetInstance<IConfigurationService>();

            container.Configure(x => x.For<IRestClient>().Singleton().Use<RestClient>()
                .Ctor<HttpClient>("httpClient").Is(new HttpClient())
                .Ctor<string>("serverAddress").Is(configurationService.ReadString(SettingKeys.DataAPI)));
        }
    }
}