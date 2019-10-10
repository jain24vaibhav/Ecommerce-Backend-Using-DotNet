using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Ecommerce.models
{
    public class Address
    {
        [Key]
        public int addressId { get; set; }
        public int  orderId { get; set; }
        public string name { get; set; }
        public int houseno { get; set; }
        public string area { get; set; }
        public string city { get; set; }
        public string state { get; set; }
        public string pincode { get; set; }
        public int userId { get; set; }
        public virtual order Order { get; set; }
        public virtual User user { get; set; }

    }
}
