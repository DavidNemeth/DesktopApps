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
        private ICustomerRepository _repo;
        //searchinput prop
        private string _SearchInput;
        public string SearchInput
        {
            get { return _SearchInput; }
            set
            {
                SetProperty(ref _SearchInput, value);
                FilterCustomers(_SearchInput);
            }
        }

        //Customer prop
        private ObservableCollection<Customer> _customers;
        public ObservableCollection<Customer> Customers
        {
            get { return _customers; }
            set { SetProperty(ref _customers, value); }
        }
        //constructor
        public CustomerListViewModel(ICustomerRepository repo)
        {
            _repo = repo;
            PlaceOrderCommand = new RelayCommand<Customer>(OnPlaceOrder);
            EditCustomerCommand = new RelayCommand<Customer>(OnEdit);
            AddCustomerCommand = new RelayCommand(OnAdd);
            ClearSearchCommand = new RelayCommand(OnClearSearch);
        }      
          
        //all Customers
        private List<Customer> _allCustomers;
        public async void LoadCustomers()
        {
            _allCustomers = await _repo.GetCustomersAsync();
            Customers = new ObservableCollection<Customer>(_allCustomers);
        }

        //filter 
        private void FilterCustomers(string SearchInput)
        {
            if (string.IsNullOrWhiteSpace(SearchInput))
            {
                Customers = new ObservableCollection<Customer>(_allCustomers);
                return;
            }
            else
            {
                Customers = new ObservableCollection<Customer>(_allCustomers.Where(c => c.FullName.ToLower().Contains(SearchInput.ToLower())));
            }
        }

        //clearfilter
        public RelayCommand ClearSearchCommand { get; private set; }
        private void OnClearSearch()
        {
            SearchInput = null;
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
