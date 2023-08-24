using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.ViewModels
{
    public record BillingViewModel
    {
        public int ID { get; set; }
        public int SalesOrderHeaderID { get; set; }
        public decimal TotalAmount { get; set; }
        public decimal PayAmount { get; set; }
        public decimal ChangeAmount { get; set; }
    }
}
