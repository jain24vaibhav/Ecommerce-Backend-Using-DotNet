using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Ecommerce.models
{
    public class OrderDetail
    {
        [Key]
        public int  orderIdDetail { get; set; }
        public int orderId { get; set; }
        public int productId { get; set; }
        public virtual order Order { get; set; }
    }
}
