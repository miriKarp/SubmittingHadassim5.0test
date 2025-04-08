using StoreManagement.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StoreManagement.DTO
{
    public class OrderDTO
    {
        public int Id { get; set; }
        public OrderStatus Status { get; set; }
        public int SupplierId { get; set; }
        public List<OrderProductDTO> OrderProducts { get; set; } 
    }
}
