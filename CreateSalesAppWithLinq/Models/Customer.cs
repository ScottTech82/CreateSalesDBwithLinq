using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CreateSalesAppWithLinq.Models
{
    [Index("Code", IsUnique = true, Name = "UIDX_Code")]
    public class Customer
    {
        [Key]
        public int Id { get; set; }

        [StringLength(100)]
        public string Name { get; set; }

        [StringLength(4)]
        public string Code { get; set; }

        [Column(TypeName = "decimal(11,2)")]
        public decimal Sales { get; set; }

        [StringLength(255)]
        public string? Notes { get; set; }

    }
}
