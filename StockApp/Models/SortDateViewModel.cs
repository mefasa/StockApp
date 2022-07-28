using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace StockApp.Models
{
    public class SortDateViewModel
    {
        public DateTime StartTime { get; set; }
        public DateTime EndTime { get; set; }
        public List<OrderViewModel> Orders { get; set; }
    }
}
