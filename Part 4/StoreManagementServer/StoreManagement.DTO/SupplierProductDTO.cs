using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StoreManagement.DTO
{
    public class SupplierProductDTO
    {
        public int ProductId { get; set; }
        public string ProductName { get; set; }
        public decimal PricePerItem { get; set; }
        public int MinimumQuantity { get; set; }
    }
}
