using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace StockApp.Context
{
    public class Order
    {
        [MaxLength(80)]
        [Required]
        [Key]
        public string Id { get; set; }
        public string ProductId { get; set; }
        public string CustomerId { get; set; }
        [DefaultValue(0)]
        public int Piece { get; set; }
        public virtual Product Product { get; set; }
        public virtual Customer Customer { get; set; }
        public DateTime OrderDate { get; set; }
    }
}
