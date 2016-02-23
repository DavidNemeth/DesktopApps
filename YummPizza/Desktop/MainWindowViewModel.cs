using Data;
using Desktop.Customers;
using Desktop.OrderPrep;
using Desktop.Orders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Desktop
{
    class MainWindowViewModel : BindableBase
    {
        private CustomerListViewModel _customerListViewModel = new CustomerListViewModel();
        private OrderViewModel _orderViewModel = new OrderViewModel();
        private OrderPrepViewModel _orderPrepViewModel = new OrderPrepViewModel();
        private AddEditCustomerViewModel _addEditCustomerViewModel = new AddEditCustomerViewModel();

        public MainWindowViewModel()
        {
            NavCommand = new RelayCommand<string>(OnNav);
            _customerListViewModel.PlaceOrderRequested += NavToOrder;
            _customerListViewModel.EditCustomerRequested += NavToEdit;
            _customerListViewModel.AddCustomerRequested += NavToAdd;
            _addEditCustomerViewModel.Done += NavToCustomerList;
        }
        private BindableBase _CurrentViewModel;
        public BindableBase CurrentViewModel
        {
            get { return _CurrentViewModel; }
            set { SetProperty(ref _CurrentViewModel, value); }
        }
        public RelayCommand<string> NavCommand { get; private set; }
        private void OnNav(string destination)
        {
            switch (destination)
            {
                case "orderPrep":
                    CurrentViewModel = _orderPrepViewModel;
                    break;
                case "customers":
                default:
                    CurrentViewModel = _customerListViewModel;
                    break;
            }
        }
        
        private void NavToOrder(string customerName)
        {
            _orderViewModel.CustomerName = customerName;
            CurrentViewModel = _orderViewModel;
        }
        private void NavToEdit(Customer customer)
        {
            _addEditCustomerViewModel.EditMode = true;
            _addEditCustomerViewModel.SetCustomer(customer);
            CurrentViewModel = _addEditCustomerViewModel;
        }
        private void NavToAdd(Customer customer)
        {
            _addEditCustomerViewModel.EditMode = false;
            _addEditCustomerViewModel.SetCustomer(customer);
            CurrentViewModel = _addEditCustomerViewModel;
        }
        private void NavToCustomerList()
        {
            CurrentViewModel = _customerListViewModel;
        }
    }
}
