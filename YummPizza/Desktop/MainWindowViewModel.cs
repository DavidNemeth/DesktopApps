using Desktop.Customers;
using Desktop.OrderPrep;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Desktop
{
    class MainWindowViewModel
    {
        private CustomerListViewModel _customerListViewModel = new CustomerListViewModel();
        private OrderViewModel _orderViewModel = new OrderViewModel();
        private OrderPrepViewModel _orderPrepViewModel = new OrderPrepViewModel();

        public object CurrentViewModel { get; set; }
    }
}
