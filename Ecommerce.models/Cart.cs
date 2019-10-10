using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace Ecommerce.models
{
    public class Cart
    {
        [Key, Column(Order = 0)]
        public int productId { get; set; }
        [Key, Column(Order = 1)]
        public int userId { get; set; }
    
        public virtual Product Product { get; set; }
        public virtual User User { get; set; }
    }
}
