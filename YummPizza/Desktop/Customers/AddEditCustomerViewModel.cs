using Data;
using Desktop.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Desktop.Customers
{
    class AddEditCustomerViewModel : BindableBase
    {
        private ICustomerRepository _repo;
        public AddEditCustomerViewModel(ICustomerRepository repo)
        {
            _repo = repo;
            CancelCommand = new RelayCommand(OnCancel);
            SaveCommand = new RelayCommand(OnSave, CanSave);
        }       

        private bool _EditMode;
        public bool EditMode
        {
            get { return _EditMode; }
            set { SetProperty(ref _EditMode, value); }
        }
        private Customer _editingCustomer = null;
                
        private EditableCustomer _Customer;
        public EditableCustomer Customer
        {
            get { return _Customer; }
            set { SetProperty(ref _Customer, value); }
        }
        
        public void SetCustomer(Customer customer)
        {
            _editingCustomer = customer;
            Customer = new EditableCustomer();
            CopyCustomer(customer, Customer);
            if (Customer != null) Customer.ErrorsChanged -= RaiseCanExecuteChanged;
            Customer.ErrorsChanged += RaiseCanExecuteChanged;
        }
        private void RaiseCanExecuteChanged(object sender, EventArgs e)
        {
            SaveCommand.RaiseCanExecuteChanged();
        }
        public void CopyCustomer(Customer customer, EditableCustomer target)
        {
            target.Id = customer.Id;
            if (EditMode)
            {
                target.FirstName = customer.FirstName;
                target.LastName = customer.LastName;
                target.Phone = customer.Phone;
                target.Email = customer.Email;
            }
        }

        public RelayCommand CancelCommand { get; private set; }
        public RelayCommand SaveCommand { get; private set; }
        public event Action Done = delegate { };

        private void OnCancel()
        {
            Done();
        }

        private async void OnSave()
        {
            UpdateCustomer(Customer, _editingCustomer);
            if (EditMode)
                await _repo.UpdateCustomerAsync(_editingCustomer);
            else
                await _repo.AddCustomerAsync(_editingCustomer);
            Done();
        }
        private void UpdateCustomer(EditableCustomer target,Customer customer)
        {
            customer.FirstName = target.FirstName;
            customer.LastName = target.LastName;
            customer.Phone = target.Phone;
            customer.Email = target.Email;
        }

        private bool CanSave()
        {
            return !Customer.HasErrors;
        }
    }
}   
