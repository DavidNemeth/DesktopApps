using Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Desktop.Customers
{
    class AddEditCustomerViewModel : BindableBase
    {
        private bool _EditMode;
        public bool EditMode
        {
            get { return _EditMode; }
            set { SetProperty(ref _EditMode, value); }
        }
        private Customer _editingCustomer = null;

        public void SetCustomer(Customer customer)
        {
            _editingCustomer = customer;
        }
    }
}
