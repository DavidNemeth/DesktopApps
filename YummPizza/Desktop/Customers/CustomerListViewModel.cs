using Data;
using Desktop.Services;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Desktop.Customers
{
    class CustomerListViewModel : BindableBase
    {
        private ICustomerRepository _repo = new CustomersRepository();
        private ObservableCollection<Customer> _customers;
        public CustomerListViewModel()
        {
            PlaceOrderCommand = new RelayCommand<Customer>(OnPlaceOrder);
        }
        public ObservableCollection<Customer> Customers
        {
            get { return _customers; }
            set { SetProperty(ref _customers, value); }
        }
        public async void LoadCustomers()
        {
            Customers = new ObservableCollection<Customer>(await _repo.GetCustomersAsync());
        }
        public event Action<string> PlaceOrderRequested;
        public RelayCommand<Customer> PlaceOrderCommand { get; private set; }
        private void OnPlaceOrder(Customer customer)
        {
            PlaceOrderRequested(customer.FullName);
        }
    }
}
