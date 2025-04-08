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
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        [Required]
        public OrderStatus Status { get; set; } 
        [ForeignKey("Supplier")]
        public int SupplierId { get; set; }
        public Supplier Supplier { get; set; }
        public ICollection<OrderProducts> OrderProducts { get; set; } = new List<OrderProducts>();
    }
    public enum OrderStatus
    {
        Pending,
        InProcess,
        Completed
    }
}
