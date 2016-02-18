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
            EditCustomerCommand = new RelayCommand<Customer>(OnEdit);
            AddCustomerCommand = new RelayCommand(OnAdd);
        }
        public ObservableCollection<Customer> Customers
        {
            get { return _customers; }
            set { SetProperty(ref _customers, value); }
        }

        //load Customers
        public async void LoadCustomers()
        {
            Customers = new ObservableCollection<Customer>(await _repo.GetCustomersAsync());
        }

        //add-order        
        public RelayCommand<Customer> PlaceOrderCommand { get; private set; }
        public event Action<string> PlaceOrderRequested = delegate { };
        private void OnPlaceOrder(Customer customer)
        {
            PlaceOrderRequested(customer.FullName);
        }

        //edit        
        public RelayCommand<Customer> EditCustomerCommand { get; private set; }
        public Action<Customer> EditCustomerRequested = delegate { };
        private void OnEdit(Customer customer)
        {
            EditCustomerRequested(customer);
        }

        //add customer
        public RelayCommand AddCustomerCommand { get; private set; }  
        public event Action<Customer> AddCustomerRequested = delegate { };
        private void OnAdd()
        {
            AddCustomerRequested(new Customer { Id = Guid.NewGuid() });
        }      
    }
}
