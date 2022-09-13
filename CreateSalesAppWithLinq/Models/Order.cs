using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CreateSalesAppWithLinq.Models
{
    public class Order
    {

        [Key]
        public int Id { get; set; }

        [StringLength(100)]
        public string Description { get; set; }

        [Column(TypeName = "decimal(11,2)")]
        public decimal Total { get; set; }

        [StringLength(10)]
        public string Status { get; set; } = "NEW";

        public DateTime Date { get; set; } = DateTime.Now;
        

        public int CustomerId { get; set; } //FK to Customer
        public virtual Customer Customer { get; set; }



    }
}
