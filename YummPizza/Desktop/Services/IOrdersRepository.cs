using Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Desktop.Services
{
    public interface IOrdersRepository
    {
        Task<List<Order>> GetOrdersForCustomersAsync(Guid customerId);
        Task<List<Order>> GetAllOrdersAsync();
        Task<Order> AddOrderAsync(Order order);
        Task<Order> UpdateOrderAsync(Order order);
        Task DeleteOrderAsync(int orderId);
        Task<List<Product>> GetProductsAsync();
        Task<List<ProductOption>> GetProductOptionsAsync();
        Task<List<ProductSize>> GetProductSizesAsync();
        Task<List<OrderStatus>> GetOrderStatusesAsync();
    }
}
