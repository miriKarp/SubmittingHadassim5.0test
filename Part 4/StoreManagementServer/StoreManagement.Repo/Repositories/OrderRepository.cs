using AutoMapper;
using Microsoft.EntityFrameworkCore;
using StoreManagement.DTO;
using StoreManagement.Models;
using StoreManagement.Repo.IRepositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StoreManagement.Repo.Repositories
{
    public class OrderRepository : IOrderRepository
    {
        private readonly StoreManagementContext _context;
        private readonly IMapper _mapper;

        public OrderRepository(StoreManagementContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<IEnumerable<OrderDTO>> GetAllOrdersAsync()
        {
            var orders = await _context.Orders
                .Include(o => o.OrderProducts)
                    .ThenInclude(oi => oi.Product)
                .ToListAsync();
            return _mapper.Map<IEnumerable<OrderDTO>>(orders);
        }
        public async Task<IEnumerable<OrderDTO>> GetOrdersBySupplierIdAsync(int supplierId)
        {
            var orders = await _context.Orders
                .Where(o => o.SupplierId == supplierId)
                .Include(o => o.OrderProducts)
                    .ThenInclude(op => op.Product)
                .ToListAsync();

            return _mapper.Map<IEnumerable<OrderDTO>>(orders);
        }

        public async Task<OrderDTO> GetOrderByIdAsync(int id)
        {
            var order = await _context.Orders
               .Include(o => o.OrderProducts)
                   .ThenInclude(oi => oi.Product)
               .FirstOrDefaultAsync(o => o.Id == id);

            if (order == null) 
                return null;
            return _mapper.Map<OrderDTO>(order);

            
        }

        public async Task AddOrderAsync(OrderDTO orderDTO)
        {
            var order = _mapper.Map<Order>(orderDTO);
            _context.Orders.Add(order);
            await _context.SaveChangesAsync();

           
        }

        public async Task UpdateOrderAsync(OrderDTO orderDTO)
        {
            var order = _mapper.Map<Order>(orderDTO);
            _context.Orders.Update(order);
            await _context.SaveChangesAsync();
        }
    }
}

