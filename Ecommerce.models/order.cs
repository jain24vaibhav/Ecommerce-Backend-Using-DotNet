using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Ecommerce.models
{
    public class order
    {
        [Key]
        public int orderId { get; set; }
        public DateTime orderDate { get; set; }
        public int userId { get; set; }

    }

}
