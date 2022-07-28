using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace StockApp.Context
{
    public class Customer
    {
        [MaxLength(80)]
        [Required]
        [Key]
        public string Id { get; set; }
        [StringLength(60, MinimumLength = 3)]
        public string Name { get; set; }
        [StringLength(60)]
        public string Contact { get; set; }
        public string City { get; set; }
        public DateTime BirthDate { get; set; }
    }
}
