using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Ecommerce.models
{
    public class User
    {
        [Key]
        public int userId { get; set; }
        public string userFirstName { get; set; }
        public string userLastName { get; set; }
        public string userEmail { get; set; }
        public string userMobile { get; set; }
        public string userPassword { get; set; }
        public string userRole { get; set; }

    }
}
