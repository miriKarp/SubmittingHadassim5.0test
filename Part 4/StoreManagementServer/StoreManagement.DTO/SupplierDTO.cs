using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StoreManagement.DTO
{
    public class SupplierDTO
    {
        public int Id { get; set; }
        public string CompanyName { get; set; }
        public string RepresentativeName { get; set; }
        public string PhoneNumber { get; set; }
        public string Password { get; set; }
        public List<SupplierProductDTO> SupplierProducts { get; set; }
        public List<OrderDTO> Orders { get; set; } 

    }
}
