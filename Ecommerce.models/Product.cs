using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Ecommerce.models
{
    public class Product
    {
        [Key]
        public int productId { get; set; }
        public string productName { get; set; }
        public int productPrice { get; set; }
        public int productMRP { get; set; }
        public DateTime creationDate { get; set; }
        public DateTime updationDate { get; set; }
        public string productDescription { get; set; }
        public int quantitySold { get; set; }
        public int quantityAvailable { get; set; }

        public int departmentId { get; set; }

        public virtual Department Departments { get; set; }
    }
}
