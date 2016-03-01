using Microsoft.Practices.Unity;
using ShopAssistant.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShopAssistant.Base
{
    public static class ContainerHelper
    {
        private static IUnityContainer _container;
        static ContainerHelper()
        {
            _container = new UnityContainer();
            _container.RegisterType<INapiAdatokRepository, NapiAdatokRepository>(
                new ContainerControlledLifetimeManager());
        }
        public static IUnityContainer Container
        {
            get { return _container; }
        }
    }
}