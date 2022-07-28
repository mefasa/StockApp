using StockApp.Context;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace StockApp.Models
{
    
    public class OrderViewModel
    {
        public OrderViewModel()
        {
        }
        public string Id { get; set; }
        public string Product { get; set; }
        public string ProductId { get; set; }
        public string Customer { get; set; }
        public string CustomerId { get; set; }
        public int Piece { get; set; }
        public decimal ProdPrice { get; set; }
        [Display(Name = "Total Price")]
        public decimal Total {
            get
            {
                var prodPrice = ProdPrice;
                if (prodPrice != 0)
                {
                    return prodPrice * Piece;
                }
                else
                {
                    return 0;
                }
            }
            
        }
        public DateTime OrderDate { get; set; }
        public DateTime StartTime { get; set; }
        public DateTime EndTime { get; set; }
    }
}
