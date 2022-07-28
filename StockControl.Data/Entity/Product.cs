using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace StockApp.Context
{
    public class Product
    {
        [MaxLength(80)]
        [Required]
        [Key]
        public string Id { get; set; }
        [StringLength(60, MinimumLength = 3)]
        public string Name { get; set; }
        [DataType(DataType.Currency)]
        [Column(TypeName = "decimal(18, 2)")]
        public decimal Price { get; set; }
        [DefaultValue(0)]
        public int Piece { get; set; }
    }
}
