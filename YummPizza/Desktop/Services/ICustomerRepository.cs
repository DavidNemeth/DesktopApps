using Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Desktop.Services
{
    public interface ICustomerRepository
    {
        Task<List<Customer>> GetCustomersAsync();
        Task<Customer> GetCustomerAsync(Guid id);
        Task<Customer> AddCustomerAsync(Customer customer);
        Task<Customer> UpdateCustomerAsync(Customer customer);
        Task DeleteCustomerAsync(Guid customerId);
    }
}
