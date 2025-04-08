using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StoreManagement.Models
{
    [Table("SupplierProduct_Tbl")]
    public class SupplierProduct
    {
        [Key, Column(Order = 0)] 
        [ForeignKey("Supplier")]
        public int SupplierId { get; set; }
        public Supplier Supplier { get; set; }

        [Key, Column(Order = 1)] 
        [ForeignKey("Product")]
        public int ProductId { get; set; }
        public Product Product { get; set; }
        [Required]
        public string ProductName { get; set; }
        [Column(TypeName = "decimal(18, 2)")]
        public decimal PricePerItem { get; set; }
        public int MinimumQuantity { get; set; }
    }
}
