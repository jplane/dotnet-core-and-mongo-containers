
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace app
{
    public class Order
    {
        public Guid Id { get; set; }
        public string CustomerName { get; set; }
        public DateTime Date { get; set; }
        public double UnitCost { get; set; }
        public int Units { get; set; }
    }
}
