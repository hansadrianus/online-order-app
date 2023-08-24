using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.ViewModels
{
    public record SalesOrderViewModel
    {
        public int Id { get; set; }
        public string OrderNumber { get; set; }
        public short Status { get; set; }
        public virtual ICollection<SalesOrderDetailViewModel> SalesOrderDetails { get; set; }

        public record SalesOrderDetailViewModel
        {
            public int Id { get; set; }
            public int SalesOrderHeaderID { get; set; }
            public int ProductID { get; set; }
            public int Quantity { get; set; }
            public decimal Price { get; set; }
            public bool IsDelivered { get; set; }
        }
    }
}
