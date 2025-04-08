using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StoreManagement.Models
{
    [Table("Suppliers_Tbl")]
    public class Supplier
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        [Required]
        public string Password { get; set; }
        [Required]
        public string CompanyName { get; set; }
        [Required]        
        public string RepresentativeName { get; set; }
        [Required]
        public string PhoneNumber { get; set; }
        public ICollection<SupplierProduct> SupplierProduct { get; set; } = new List<SupplierProduct>();
        public ICollection<Order> Orders { get; set; } = new List<Order>() ;
    }
}
