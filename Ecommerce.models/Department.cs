using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Ecommerce.models
{
    public class Department
    {
        [Key]
        public int departmentId { get; set; }
        public string departmentName { get; set; }
        
    }
}
