using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.ViewModels
{
    public record ProductViewModel
    {
        public int ID { get; set; }
        public string Name { get; set; }
        public short Type { get; set; }
        public decimal Price { get; set; }
    }
}
