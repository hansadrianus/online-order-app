using Domain.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Entities
{
    public class SalesOrderHeader : AuditableEntity
    {
        public string OrderNumber { get; set; }
        public short Status { get; set; }

        public virtual ICollection<SalesOrderDetail> SalesOrderDetails { get; set; }
        public Billing? Billing { get; set; }
    }
}
