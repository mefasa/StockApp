using StockApp.Context;
using System;
using System.Collections.Generic;
using System.Text;

namespace StockControl.Data.Models
{
    public class OrderModel
    {
        public string Id { get; set; }
        public string ProductId { get; set; }
        public string CustomerId { get; set; }
        public int Piece { get; set; }
        public Product Product { get; set; }
        public Customer Customer { get; set; }
        public DateTime OrderDate { get; set; }
    }
}
