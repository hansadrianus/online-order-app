using Domain.Common;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Entities
{
    public class Billing : AuditableEntity
    {
        public int SalesOrderHeaderID { get; set; }
        public decimal TotalAmount { get; set; }
        public decimal PayAmount { get; set; }
        public decimal ChangeAmount { get; set; }

        [ForeignKey("SalesOrderHeaderID")]
        public SalesOrderHeader SalesOrderHeader { get; set; }
    }
}
