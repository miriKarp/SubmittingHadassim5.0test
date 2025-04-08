using StoreManagement.DTO;
using StoreManagement.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StoreManagement.Repo.IRepositories
{
    public interface IOrderRepository
    {
        public Task<IEnumerable<OrderDTO>> GetAllOrdersAsync();
        public Task<OrderDTO> GetOrderByIdAsync(int id); 
        public Task AddOrderAsync(OrderDTO orderDTO);
        public Task UpdateOrderAsync(OrderDTO orderDTO);
        Task<IEnumerable<OrderDTO>> GetOrdersBySupplierIdAsync(int supplierId);

    }
}
