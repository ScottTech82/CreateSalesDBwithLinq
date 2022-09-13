using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CreateSalesAppWithLinq.Models
{
    public class OrderLine
    {

        public int Id { get; set; }

        public int OrderId { get; set; } //FK to Orders
        public virtual Order Order { get; set; }

        public int ProductId { get; set; } //FK to Products
        public virtual Product Product { get; set; }


        public int Quantity { get; set; } = 1;

    }
}
