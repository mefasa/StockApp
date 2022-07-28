using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace StockApp.Models
{
    public class CustomerViewModel
    {
        public string Id { get; set; }
        public string Name { get; set; }
        public string Contact { get; set; }
        public string City { get; set; }
        public DateTime BirthDate  { get; set; }
    }
}
