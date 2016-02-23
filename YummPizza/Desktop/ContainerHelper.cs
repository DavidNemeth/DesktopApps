using Desktop.Services;
using Microsoft.Practices.Unity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Desktop
{
    public static class ContainerHelper
    {
        private static IUnityContainer _container;
        static ContainerHelper()
        {
            _container = new UnityContainer();
            _container.RegisterType<ICustomerRepository, CustomersRepository>(
                new ContainerControlledLifetimeManager());
        }
        public static IUnityContainer Container
        {
            get { return _container; }
        }
    }
}
