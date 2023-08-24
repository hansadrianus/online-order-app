using Domain.Common;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Entities
{
    public class SalesOrderDetail : AuditableEntity
    {
        public int SalesOrderHeaderID { get; set; }
        public int ProductID { get; set; }
        public int Quantity { get; set; }
        public decimal Price { get; set; }
        public bool IsDelivered { get; set; }

        [ForeignKey("SalesOrderHeaderID")]
        public SalesOrderHeader SalesOrderHeader { get; set; }
        [ForeignKey("ProductID")]
        public Product Product { get; set; }
    }
}
