using Ecommerce.models;
using Ecommerce.models.models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;

namespace Ecommerce.DA
{
    public class UserDA
    {
        EcommerceContext _db = new EcommerceContext();

        public ICollection<User> GetAllUsers()
        {
            var res = _db.user.ToArray();
            return res;
        }

        public User UserLogin(LoginModel user)
        {
            var isUser = _db.user.Where(x => x.userEmail == user.userEmail && x.userPassword == user.userPassword).FirstOrDefault();
            if (isUser != null)
                return isUser;
            else
                return null;
    
        }

    }
}
