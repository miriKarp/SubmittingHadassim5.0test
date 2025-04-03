using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StoreManagement.Models
{
    [Table("Orders_Tbl")]
    public class Order
    {
        [Key]
        public int Id { get; set; }
        [Required]
        public string Status { get; set; } // "Pending", "In Process", "Completed"
        [ForeignKey("Supplier")]
        public int SupplierId { get; set; }
        public Supplier Supplier { get; set; }

    }
}
